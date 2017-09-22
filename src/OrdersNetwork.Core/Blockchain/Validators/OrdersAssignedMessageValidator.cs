using OrdersNetwork.Core.Blockchain.Index;
using OrdersNetwork.Core.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Blockchain.Validators
{
    public class OrdersAssignedMessageValidator : INewUserMessageValidator<OrdersAssigned>
    {
        private readonly IBestPrizeTransactionsSelector _bestPrizeTransactionsSelector;

        public OrdersAssignedMessageValidator(IBestPrizeTransactionsSelector bestPrizeTransactionsSelector)
        {
            _bestPrizeTransactionsSelector = bestPrizeTransactionsSelector;
        }

        public NewTransactionIncludeResult Validate(NodeIndex nodeIndex, UserIndex userIndex, OngoingBlock ongoingBlock, OrdersAssigned message)
        {
            if (message.Fee < 0 || message.Fee > 1)
            {
                throw new TransactionDeclinedException("Invalid Fee.");
            }

            if (message.Orders.Count == 0)
            {
                throw new TransactionDeclinedException("Empty orders.");
            }

            return _bestPrizeTransactionsSelector.Calculate(nodeIndex, ongoingBlock, message);
        }
        
    }
}
