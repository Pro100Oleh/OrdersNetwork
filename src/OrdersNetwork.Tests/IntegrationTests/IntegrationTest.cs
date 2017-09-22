using Castle.Windsor;
using OrdersNetwork.Core.Blockchain;
using OrdersNetwork.Core.Client;
using OrdersNetwork.Core.DI;
using OrdersNetwork.Core.Server;
using OrdersNetwork.WalkDogModule.DI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Tests.IntegrationTests
{
    public class IntegrationTest : IDisposable
    {
        public IntegrationTest()
        {
            Container = ContainerFactory.Create(new RegistrationModule());
            Settings = new NodeSettings { NextHashFactor = "50000000" };
        }
        

        public IWindsorContainer Container { get; }
        public NodeSettings Settings { get; }

        public IServer StartServer(string name)
        {
            var factory = Container.Resolve<IServerFactory>();
            return factory.Create(name, Settings);
        }

        public IClient CreateClient(string name)
        {
            var factory = Container.Resolve<IClientFactory>();
            return factory.Create(name);
        }

        public void Dispose()
        {
            Container.Dispose();
        }
    }
}
