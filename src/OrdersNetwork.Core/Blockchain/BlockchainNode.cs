using OrdersNetwork.Core.Network;
using OrdersNetwork.Core.Transactions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Blockchain
{
    /// <summary>
    /// Responsible to include new transactions to new blocks
    /// </summary>
    public class BlockchainNode : IBlockchainNode
    {
        private NodeState _state;

        private readonly object _syncObj = new object();

        private readonly INewTransactionHandler _newTransactionHandler;
        private readonly NodeSettings _nodeSettings;
        private readonly INextBlockFinder _nextBlockFinder;

        public BlockchainNode(INode node, NodeSettings nodeSettings, INewTransactionHandler newTransactionHandler, INextBlockFinderFactory nextBlockFinderFactory)
        {
            _state = new NodeState(node);

            Node = node;
            _newTransactionHandler = newTransactionHandler;
            _nodeSettings = nodeSettings;
            _nextBlockFinder = nextBlockFinderFactory.Create(nodeSettings);
        }

        public INode Node { get; }

        public NodeState GetState()
        {
            return _state;
        }
        

        public bool TryAddNewTransaction(Transaction newTransaction)
        {
            NodeState newState;
            bool processed;
            bool added;
            do
            {
                //atomic action, no need to lock _syncObj
                var currentState = _state;

                if (!_newTransactionHandler.TryCreateNext(currentState, newTransaction, out newState))
                {
                    //ignore new transaction
                    processed = true;
                    added = false;
                }
                else
                {
                    //try add
                    lock (_syncObj)
                    {
                        if (_state.Index == newState.Index)
                        {
                            //was nothing happen
                            //update
                            _state = newState;
                            processed = true;
                            added = true;
                        }
                        else
                        {
                            //was updated blockchain
                            //try to include new transaction again
                            processed = false;
                            added = false;
                        }
                    }
                }
            }
            while (!processed);

            Trace.TraceInformation("[node]\tTransaction {0} was processed. Added: {1}", newTransaction, added);

            return added;
        }
        
        

        public bool TryFindNextBlock(out Block nextBlock)
        {
            //atomic action, no need to lock _syncObj
            var currentState = _state;
            

            NodeState nextState;
            if (!_nextBlockFinder.TryFindNext(currentState, out nextState))
            {
                //bad block, ignore
                nextBlock = default(Block);
                return false;
            }

            nextBlock = nextState.NodeIndex.Blocks.Last;

            lock (_syncObj)
            {
                if (_state.Index != nextState.Index - 1)
                {
                    //too late, ignore
                    Trace.TraceInformation("[node]\tNext block {0} will be ignored: index was changed", nextBlock);
                    nextBlock = default(Block);
                    return false;
                }

                //include new block
                _state = nextState;
                Trace.TraceInformation("[node]\tWas found next block {0}", nextBlock);
                return true;
            }
        }


    }
}
