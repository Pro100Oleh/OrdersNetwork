using OrdersNetwork.Core.Network;
using OrdersNetwork.Core.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Blockchain
{
    public interface IBlockSigner
    {
        HashValue Sign(long index, INode node, string rnd, HashValue previousHash, IReadOnlyList<Transaction> transactions);
    }
}
