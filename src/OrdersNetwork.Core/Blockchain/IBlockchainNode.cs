using OrdersNetwork.Core.Network;
using OrdersNetwork.Core.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Blockchain
{
    public interface IBlockchainNode
    {
        INode Node { get; }

        NodeState GetState();

        bool TryAddNewTransaction(Transaction newTransaction);
        bool TryFindNextBlock(out Block nextBlock);
    }
}
