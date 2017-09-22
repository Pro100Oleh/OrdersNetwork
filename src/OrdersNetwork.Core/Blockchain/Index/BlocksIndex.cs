using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Blockchain.Index
{
    /// <summary>
    /// History of all blocks
    /// </summary>
    public class BlocksIndex
    {
        private BlocksIndex()
        {
            AllBlocks = null;
        }

        private BlocksIndex(IImmutableList<Block> allBlocks, IImmutableDictionary<HashValue, Block> blocksByHash)
        {
            AllBlocks = allBlocks ?? throw new ArgumentNullException("allBlocks");
            BlocksByHash = blocksByHash ?? throw new ArgumentNullException("blocksByHash");
        }

        public static BlocksIndex Empty { get; } = new BlocksIndex();

        public IImmutableList<Block> AllBlocks { get; }
        public IImmutableDictionary<HashValue, Block> BlocksByHash { get; }

        public bool IsEmpty
        {
            get { return AllBlocks == null; }
        }

        public int Count { get { return AllBlocks.Count; } }
        public Block Last { get { return AllBlocks.Last(); } }

        /// <summary>
        /// Aggregate current index with next block
        /// </summary>
        /// <param name="nextBlock">nextBlock arg should be validated before</param>
        /// <returns></returns>
        public BlocksIndex Add(Block nextBlock)
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("Index in empty");
            }

            var allBlocks = AllBlocks.Add(nextBlock);
            var blocksByHash = BlocksByHash.Add(nextBlock.Signature, nextBlock);

            return new BlocksIndex(allBlocks, BlocksByHash);
        }

        public static BlocksIndex New(Block nextBlock)
        {
            return new BlocksIndex(
                    ImmutableList.Create(nextBlock),
                    new[] { nextBlock }.ToImmutableDictionary());
        }
    }
}
