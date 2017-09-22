using OrdersNetwork.Core.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Security
{
    public interface IHashProvider
    {
        HashValue GetHashOfStream(IStream stream);
    }
}
