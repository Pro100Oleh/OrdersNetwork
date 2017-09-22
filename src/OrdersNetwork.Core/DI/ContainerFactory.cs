using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using OrdersNetwork.Core.Blockchain;
using OrdersNetwork.Core.Blockchain.Validators;
using OrdersNetwork.Core.Client;
using OrdersNetwork.Core.Network;
using OrdersNetwork.Core.Security;
using OrdersNetwork.Core.Server;
using OrdersNetwork.Core.Streams;
using OrdersNetwork.Core.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.DI
{
    public static class ContainerFactory
    {
        public static IWindsorContainer Create(params IModule[] modules)
        {
            var container = new WindsorContainer();
            container.AddFacility<TypedFactoryFacility>();
            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel, false));

            container.Register(
                //security
                Component.For<IHashProvider>().ImplementedBy<Sha256HashProvider>().LifeStyle.Singleton,

                //streams
                Component.For<IStream>().ImplementedBy<StringStream>().LifeStyle.Transient,
                Component.For<IStreamFactory>().AsFactory().LifeStyle.Singleton,
                Component.For<IStreamWriter<OrderRequested>>().ImplementedBy<OrderRequestedStreamWriter>().LifeStyle.Transient,
                Component.For<IStreamWriter<OrdersAssigned>>().ImplementedBy<OrdersAssignedStreamWriter>().LifeStyle.Transient,
                Component.For<IStreamWriter<UserMessage>>().ImplementedBy<UserMessageStreamWriter>().LifeStyle.Transient,

                //transactions
                Component.For<ITransactionSigner>().ImplementedBy<TransactionSigner>().LifeStyle.Transient,
                Component.For<ITransactionFactory>().ImplementedBy<TransactionFactory>().LifeStyle.Transient,

                //blockchain validators
                Component.For<ITransactionPrizeCalculator>().ImplementedBy<TransactionPrizeCalculator>().LifeStyle.Transient,
                Component.For<IBestPrizeTransactionsSelector>().ImplementedBy<BestPrizeTransactionsSelector>().LifeStyle.Transient,
                Component.For<INewUserMessageValidator<OrderRequested>>().ImplementedBy<OrderRequestedMessageValidator>().LifeStyle.Transient,
                Component.For<INewUserMessageValidator<OrdersAssigned>>().ImplementedBy<OrdersAssignedMessageValidator>().LifeStyle.Transient,
                Component.For<INewTransactionValidator>().ImplementedBy<NewTransactionValidator>().LifeStyle.Transient,
                Component.For<INextBlockValidator>().ImplementedBy<NextBlockValidator>().LifeStyle.Transient,

                //blockchain
                Component.For<IHashFilter>().ImplementedBy<HashFilter>().LifeStyle.Transient,
                Component.For<IBlockSigner>().ImplementedBy<BlockSigner>().LifeStyle.Transient,
                Component.For<IBlockFactory>().ImplementedBy<BlockFactory>().LifeStyle.Transient,
                Component.For<INewTransactionHandler>().ImplementedBy<NewTransactionHandler>().LifeStyle.Transient,
                Component.For<INextBlockFinder>().ImplementedBy<NextBlockFinder>().LifeStyle.Transient,
                Component.For<INextBlockFinderFactory>().AsFactory().LifeStyle.Singleton,

                Component.For<IBlockchainNode>().ImplementedBy<BlockchainNode>().LifeStyle.Transient,
                Component.For<IBlockchainNodeFactory>().AsFactory().LifeStyle.Singleton,

                //network
                Component.For<IMessageBus>().ImplementedBy<MessageBus>().LifeStyle.Singleton,

                //server
                Component.For<IServerFactory>().ImplementedBy<ServerFactory>().LifeStyle.Singleton,

                //client
                Component.For<IClient>().ImplementedBy<BlockchainClient>().LifeStyle.Transient,
                Component.For<IClientInstanceFactory>().AsFactory().LifeStyle.Singleton,
                Component.For<IClientFactory>().ImplementedBy<ClientFactory>().LifeStyle.Singleton
            );

            foreach(var module in modules)
            {
                module.Setup(container);
            }

            return container;
        }
    }
}
