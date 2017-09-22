using OrdersNetwork.Core.Blockchain;
using OrdersNetwork.Core.Network;
using System;

namespace OrdersNetwork.Core.Server
{
    /// <summary>
    /// Server that listen for a new client transactions and sign next blocks
    /// </summary>
    public class BlockchainServer : IServer
    {
        private readonly IBlockchainNode _blockchainNode;
        private readonly IMessageBus _messageBus;

        public BlockchainServer(IBlockchainNode blockchainNode, IMessageBus messageBus)
        {
            _blockchainNode = blockchainNode;
            _messageBus = messageBus;

            messageBus.Subscribe(_blockchainNode.Node, Handle);
        }

        /// <summary>
        /// Handle new message from message bus
        /// </summary>
        /// <param name="message"></param>
        public void Handle(IMessage message)
        {
            if (message is NewTransactionMessage)
            {
                var newTransaction = ((NewTransactionMessage)message).Transaction;
                _blockchainNode.TryAddNewTransaction(newTransaction);
            }
            else if (message is NewBlockMessage)
            {
                throw new NotSupportedException("only one server can be in network (me) that can publish new blocks");
            }
        }
        

        private void TryFindNextBlock()
        {
            Block nextBlock;
            if (_blockchainNode.TryFindNextBlock(out nextBlock))
            {
                _messageBus.Push(new NewBlockMessage(_blockchainNode.Node, nextBlock));
            }
        }

        /// <summary>
        /// Calculate next blocks until get block with asked index
        /// </summary>
        /// <param name="index">started from 1 number</param>
        /// <returns></returns>
        public Block WaitForBlock(int index)
        {
            if (index< 0)
            {
                throw new ArgumentOutOfRangeException("index");
            }

            NodeState state;
            while(true)
            {
                state = _blockchainNode.GetState();
                if (state.Index >= index)
                {
                    return state.NodeIndex.Blocks.AllBlocks[index - 1];
                }
                TryFindNextBlock();
            }
        }
        
    }
}
