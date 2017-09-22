using OrdersNetwork.Core.Network;
using OrdersNetwork.Core.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Blockchain
{
    public interface IBlockFactory
    {
        Block Create(long index, INode node, string rnd, HashValue previousBlock, IReadOnlyList<Transaction> transactions);
    }
}
