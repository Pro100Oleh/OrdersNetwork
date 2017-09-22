using OrdersNetwork.Core.Blockchain.Index;
using OrdersNetwork.Core.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Blockchain.Validators
{
    public interface ITransactionPrizeCalculator
    {
        double GetPrize(NodeIndex nodeIndex, OrdersAssigned message);
    }
}
