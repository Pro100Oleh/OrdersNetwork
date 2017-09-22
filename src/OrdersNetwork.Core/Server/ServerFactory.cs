using OrdersNetwork.Core.Blockchain;
using OrdersNetwork.Core.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Server
{
    public class ServerFactory : IServerFactory
    {
        private readonly IMessageBus _messageBus;
        private readonly IBlockchainNodeFactory _blockchainNodeFactory;

        public ServerFactory(IMessageBus messageBus, IBlockchainNodeFactory blockchainNodeFactory)
        {
            _messageBus = messageBus;
            _blockchainNodeFactory = blockchainNodeFactory;
        }

        public IServer Create(string name, NodeSettings nodeSettings)
        {
            var node = new NetworkNode(name);
            var blockchainNode = _blockchainNodeFactory.Create(node, nodeSettings);

            return new BlockchainServer(blockchainNode, _messageBus);
        }
    }
}
