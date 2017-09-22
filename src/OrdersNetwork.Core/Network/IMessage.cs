using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Network
{
    /// <summary>
    /// Message that published in network and shared between all members
    /// </summary>
    public interface IMessage
    {
        /// <summary>
        /// Sender info
        /// </summary>
        INode Sender { get; }
    }
}
