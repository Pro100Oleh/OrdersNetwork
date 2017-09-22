using OrdersNetwork.Core.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Blockchain
{
    public interface IBlockchainNodeFactory
    {
        IBlockchainNode Create(INode node, NodeSettings nodeSettings);
    }
}
