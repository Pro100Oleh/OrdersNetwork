using OrdersNetwork.Core.Blockchain.Index;
using OrdersNetwork.Core.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Blockchain.Validators
{
    public class NewTransactionValidator : INewTransactionValidator
    {
        private readonly INewUserMessageValidator<OrderRequested> _orderRequestedValidator;
        private readonly INewUserMessageValidator<OrdersAssigned> _ordersAssignedValidator;

        public NewTransactionValidator(INewUserMessageValidator<OrderRequested> orderValidator, INewUserMessageValidator<OrdersAssigned> ordersAssignedValidator)
        {
            _orderRequestedValidator = orderValidator;
            _ordersAssignedValidator = ordersAssignedValidator;
        }

        public NewTransactionIncludeResult Validate(NodeIndex nodeIndex, OngoingBlock ongoingBlock, Transaction transaction)
        {
            if (!nodeIndex.IsEmpty && nodeIndex.Orders.AllOrders.ContainsKey(transaction.Signature))
            {
                throw new TransactionDeclinedException("Already processed.");
            }

            if (ongoingBlock.Transactions.Any(t => t.Signature == transaction.Signature))
            {
                throw new TransactionDeclinedException("Already received.");
            }

            if (ongoingBlock.Transactions.Any(t => t.User == transaction.User))
            {
                throw new TransactionDeclinedException("Not alowed to request many transaction for one block.");
            }

            UserIndex existUser;
            if (!nodeIndex.IsEmpty && nodeIndex.Users.AllUsers.TryGetValue(transaction.User, out existUser))
            {
                //check that user does not spam transactions. Only 1 transaction per block.
                //This requirement simplify transaction consistency validation
                if (existUser.LastTransaction.Signature != transaction.Previous)
                {
                    throw new TransactionDeclinedException("Wrong user transactions order.");
                }
            }
            else
            {
                existUser = null;
            }

            if (transaction.Message is OrderRequested)
            {
                return _orderRequestedValidator.Validate(nodeIndex, existUser, ongoingBlock, (OrderRequested)transaction.Message);
            }
            else
            {
                if (transaction.Message is OrdersAssigned)
                {
                    return _ordersAssignedValidator.Validate(nodeIndex, existUser, ongoingBlock, (OrdersAssigned)transaction.Message);
                }
                else
                {
                    throw new TransactionDeclinedException("Unkown user message type.");
                }
            }
        }
    }
}
