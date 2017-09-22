using OrdersNetwork.Core.Blockchain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Server
{
    public interface IServerFactory
    {
        IServer Create(string name, NodeSettings nodeSettings);
    }
}
