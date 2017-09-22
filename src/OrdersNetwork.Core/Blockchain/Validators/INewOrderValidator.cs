using OrdersNetwork.Core.Blockchain.Index;
using OrdersNetwork.Core.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Blockchain.Validators
{
    public interface INewOrderValidator
    {
        string OrderType { get; }

        void Validate(NodeIndex nodeIndex, UserIndex userIndex, IOrder order);
    }
}
