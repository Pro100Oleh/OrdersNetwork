using OrdersNetwork.Core.Blockchain.Index;
using OrdersNetwork.Core.Blockchain.Validators;
using OrdersNetwork.Core.Transactions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Blockchain
{
    public class NewTransactionHandler : INewTransactionHandler
    {
        private readonly INewTransactionValidator _newTransactionValidator;

        public NewTransactionHandler(INewTransactionValidator newTransactionValidator)
        {
            _newTransactionValidator = newTransactionValidator;
        }

        public bool TryCreateNext(NodeState state, Transaction newTransaction, out NodeState newState)
        {
            OngoingBlock newOngoingBlock;
            if (!TryIncludeNewTransaction(state.NodeIndex, state.OngoingBlock, newTransaction, out newOngoingBlock))
            {
                newState = default(NodeState);
                return false;
            }

            newState = state.UpdateOngoingBlock(newOngoingBlock);
            return true;
        }

        private bool TryIncludeNewTransaction(NodeIndex nodeIndex, OngoingBlock ongoingBlock, Transaction newTransaction, out OngoingBlock newOngoingBlock)
        {
            NewTransactionIncludeResult includeResult;

            try
            {
                includeResult = _newTransactionValidator.Validate(nodeIndex, ongoingBlock, newTransaction);
            }
            catch(TransactionDeclinedException ex)
            {
                Trace.TraceWarning("Transaction message {0} will be declined: {1}", newTransaction.Signature, ex.Message);
                newOngoingBlock = default(OngoingBlock);
                return false;
            }
            catch (Exception ex)
            {
                Trace.TraceWarning("Transaction message {0} will be ignored because of unexpected error: {1}", newTransaction.Signature, ex);
                newOngoingBlock = default(OngoingBlock);
                return false;
            }

            if (includeResult.IsNone)
            {
                Trace.TraceWarning("Transaction message {0} will be declined: try to set a better fee", newTransaction.Signature);
                newOngoingBlock = default(OngoingBlock);
                return false;
            }

            newOngoingBlock = ongoingBlock.Aggregate(newTransaction, includeResult.ToRemoveTransactions);
            return true;
        }
    }
}
