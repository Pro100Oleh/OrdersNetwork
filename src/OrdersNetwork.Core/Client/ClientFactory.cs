using OrdersNetwork.Core.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Client
{
    public class ClientFactory : IClientFactory
    {
        private readonly IClientInstanceFactory _clientInstanceFactory;

        public ClientFactory(IClientInstanceFactory clientInstanceFactory)
        {
            _clientInstanceFactory = clientInstanceFactory;
        }

        public IClient Create(string name)
        {
            var user = new NetworkUser(name);
            var node = new NetworkNode(name);

            return _clientInstanceFactory.Create(user, node);
        }
    }
}
