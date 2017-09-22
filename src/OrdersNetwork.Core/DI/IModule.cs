using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.DI
{
    public interface IModule
    {
        void Setup(IWindsorContainer container);
    }
}
