using OrdersNetwork.Core.Network;
using OrdersNetwork.Core.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Blockchain
{
    public class BlockFactory : IBlockFactory
    {
        private readonly IBlockSigner _blockSigner;

        public BlockFactory(IBlockSigner blockSigner)
        {
            _blockSigner = blockSigner;
        }

        public Block Create(long index, INode node, string rnd, HashValue previousBlock, IReadOnlyList<Transaction> transactions)
        {
            var hash = _blockSigner.Sign(index, node, rnd, previousBlock, transactions);
            return new Block(index, node, rnd, previousBlock, hash, transactions);
        }
    }
}
