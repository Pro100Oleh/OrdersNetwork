using OrdersNetwork.Core.Blockchain.Index;
using OrdersNetwork.Core.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Blockchain.Validators
{
    public class BestPrizeTransactionsSelector : IBestPrizeTransactionsSelector
    {
        private readonly ITransactionPrizeCalculator _transactionPrizeCalculator;

        public BestPrizeTransactionsSelector(ITransactionPrizeCalculator transactionPrizeCalculator)
        {
            _transactionPrizeCalculator = transactionPrizeCalculator;
        }

        public NewTransactionIncludeResult Calculate(NodeIndex nodeIndex, OngoingBlock ongoingBlock, OrdersAssigned newOrder)
        {
            var newPrize = _transactionPrizeCalculator.GetPrize(nodeIndex, newOrder);

            var usedOrders = new HashSet<HashValue>(newOrder.Orders);

            var toRemoveTransactionsDic = new Dictionary<HashValue, Transaction>();

            foreach(var ongoingTransaction in ongoingBlock.Transactions)
            {
                if (!(ongoingTransaction.Message is OrdersAssigned))
                {
                    continue;
                }

                var ongoingOrders = ((OrdersAssigned)ongoingTransaction.Message).Orders;
                foreach (var ongoingOrder in ongoingOrders)
                {
                    if (usedOrders.Contains(ongoingOrder))
                    {
                        toRemoveTransactionsDic[ongoingTransaction.Signature] = ongoingTransaction;
                        break;
                    }
                }
            }

            double lostPrize = 0;
            foreach(var t in toRemoveTransactionsDic.Values)
            {
                lostPrize += _transactionPrizeCalculator.GetPrize(nodeIndex, (OrdersAssigned)t.Message);
            }

            if (newPrize <= lostPrize)
            {
                return NewTransactionIncludeResult.None;
            }

            return new NewTransactionIncludeResult(toRemoveTransactionsDic.Values.ToArray());
        }
    }
}
