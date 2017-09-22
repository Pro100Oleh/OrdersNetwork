using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Network
{
    /// <summary>
    /// Describe blockchain network node that listen/publish messages. Can be server or client.
    /// </summary>
    public interface INode : IEquatable<INode>
    {
        string Name { get; }
    }
}
