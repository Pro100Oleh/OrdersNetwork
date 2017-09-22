using OrdersNetwork.Core.Blockchain.Index;
using OrdersNetwork.Core.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Blockchain.Validators
{
    public class NextBlockValidator : INextBlockValidator
    {
        private readonly IBlockSigner _blockSigner;

        public NextBlockValidator(IBlockSigner blockSigner)
        {
            _blockSigner = blockSigner;
        }

        public void Validate(BlocksIndex index, Block nextBlock)
        {
            var expectedIndex = index.IsEmpty ? 1 : index.Last.Index + 1; ;
            if (nextBlock.Index != expectedIndex)
            {
                throw new ArgumentException("Invalid Index value");
            }

            var expectedSignature = index.IsEmpty ? HashValue.Empty : index.Last.Signature; ;
            if (nextBlock.PreviousBlock != expectedSignature)
            {
                throw new ArgumentException("Invalid PreviousBlock value");
            }

            var realHash = _blockSigner.Sign(nextBlock.Index, nextBlock.Node, nextBlock.Rnd, nextBlock.PreviousBlock, nextBlock.Transactions);
            if (realHash != nextBlock.Signature)
            {
                throw new ArgumentException("Invalid Signature");
            }
        }
    }
}
