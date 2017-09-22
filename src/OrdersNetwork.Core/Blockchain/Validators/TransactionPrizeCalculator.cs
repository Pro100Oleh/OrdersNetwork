using OrdersNetwork.Core.Blockchain.Index;
using OrdersNetwork.Core.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Blockchain.Validators
{
    public class TransactionPrizeCalculator : ITransactionPrizeCalculator
    {
        public double GetPrize(NodeIndex nodeIndex, OrdersAssigned message)
        {
            if (nodeIndex.IsEmpty)
            {
                throw new TransactionDeclinedException("index is empty");
            }

            var prize = nodeIndex.Orders.SelectUnassignedOrders(message.Orders).Sum(o => o.Order.Payment * message.Fee);

            return prize;
        }
    }
}
