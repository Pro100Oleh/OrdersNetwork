using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Streams
{
    public interface IStreamFactory
    {
        IStream Create();
    }
}
