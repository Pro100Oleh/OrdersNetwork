using Castle.MicroKernel.Registration;
using Castle.Windsor;
using OrdersNetwork.Core.Blockchain.Validators;
using OrdersNetwork.Core.DI;
using OrdersNetwork.Core.Streams;
using OrdersNetwork.WalkDogModule.Blockchain;
using OrdersNetwork.WalkDogModule.StreamBuilders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.WalkDogModule.DI
{
    public class RegistrationModule : IModule
    {
        public void Setup(IWindsorContainer container)
        {
            container.Register(
                Component.For<INewOrderValidator>().ImplementedBy<NewWalkDogOrderValidator>().LifeStyle.Transient,
                Component.For<IOrderStreamWriter>().ImplementedBy<WalkDogOrderStreamWriter>().LifeStyle.Transient
            );
        }
    }
}
