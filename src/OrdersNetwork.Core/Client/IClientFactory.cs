using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Client
{
    public interface IClientFactory
    {
        IClient Create(string name);
    }
}
