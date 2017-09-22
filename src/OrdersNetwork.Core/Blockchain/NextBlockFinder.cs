using OrdersNetwork.Core.Transactions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Blockchain
{
    public class NextBlockFinder : INextBlockFinder
    {
        private readonly IBlockFactory _blockFactory;
        private readonly IHashFilter _hashFilter;
        private readonly NodeSettings _nodeSettings;

        public NextBlockFinder(IBlockFactory blockFactory, IHashFilter hashFilter, NodeSettings nodeSettings)
        {
            _blockFactory = blockFactory;
            _hashFilter = hashFilter;
            _nodeSettings = nodeSettings;
        }

        public bool TryFindNext(NodeState state, out NodeState nextState)
        {
            Block nextBlock;
            if(!TryFindNextBlock(state.OngoingBlock, out nextBlock))
            {
                nextState = default(NodeState);
                return false;
            }

            nextState = state.Add(nextBlock);
            return true;
        }

        private bool TryFindNextBlock(OngoingBlock ongoingBlock, out Block nextBlock)
        {
            var rnd = Guid.NewGuid().ToString("D");
            
            nextBlock = _blockFactory.Create(ongoingBlock.Index, ongoingBlock.Node, rnd, ongoingBlock.PreviousHash, ongoingBlock.Transactions);
            Trace.TraceInformation("[node]\t\tCreated next block candidate {0}", nextBlock);

            return _hashFilter.IsSatisfy(nextBlock.Signature, _nodeSettings.NextHashFactor);
        }
    }
}
