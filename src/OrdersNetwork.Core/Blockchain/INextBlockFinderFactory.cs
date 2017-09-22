using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Blockchain
{
    public interface INextBlockFinderFactory
    {
        INextBlockFinder Create(NodeSettings nodeSettings);
    }
}
