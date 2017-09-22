using OrdersNetwork.Core.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Client
{
    public interface IClientInstanceFactory
    {
        IClient Create(IUser user, INode userNode);
    }
}
