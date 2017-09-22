using OrdersNetwork.Core.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Blockchain
{
    public interface INewTransactionHandler
    {
        bool TryCreateNext(NodeState state, Transaction newTransaction, out NodeState newState);
    }
}
