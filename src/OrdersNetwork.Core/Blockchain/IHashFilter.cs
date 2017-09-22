using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Blockchain
{
    public interface IHashFilter
    {
        bool IsSatisfy(HashValue hash, string factor);
    }
}
