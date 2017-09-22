using OrdersNetwork.Core.Blockchain.Index;
using OrdersNetwork.Core.Network;
using OrdersNetwork.Core.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Blockchain
{

    /// <summary>
    /// State of Blockchain node that contains past (NodeIndex) and future (OngoingBlock)
    /// </summary>
    public class NodeState
    {
        public NodeState(INode node)
        {
            NodeIndex = NodeIndex.Empty;
            OngoingBlock= new OngoingBlock(1, node, HashValue.Empty, Array.Empty<Transaction>());
        }

        internal NodeState(NodeIndex nodeIndex, OngoingBlock ongoingBlock)
        {
            NodeIndex = nodeIndex ?? throw new NullReferenceException("nodeIndex");
            OngoingBlock = ongoingBlock ?? throw new NullReferenceException("ongoingBlock");
        }

        public NodeIndex NodeIndex { get; }
        public OngoingBlock OngoingBlock { get; }

        public long Index
        {
            get
            {
                if (NodeIndex.IsEmpty)
                {
                    return 0;
                }
                return NodeIndex.Blocks.Last.Index;
            }
        }


        public NodeState Add(Block nextBlock)
        {
            var newNodeIndex = NodeIndex.Add(nextBlock);
            var includedTransactions = nextBlock.Transactions.Select(t => t.Signature).ToHashSet();
            var notIncludedTransactios = OngoingBlock.Transactions.Where(t => !includedTransactions.Contains(t.Signature)).ToArray();
            var newOngoingBlock = new OngoingBlock(nextBlock.Index + 1, OngoingBlock.Node, nextBlock.Signature, notIncludedTransactios);

            return new NodeState(newNodeIndex, newOngoingBlock);
        }

        public NodeState UpdateOngoingBlock(OngoingBlock newOngoingBlock)
        {
            return new NodeState(NodeIndex, newOngoingBlock);
        }

    }
}
