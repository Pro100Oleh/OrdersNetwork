using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Network
{
    /// <summary>
    /// Network layer abstraction.
    /// Message Bus is shared between all members.
    /// </summary>
    public interface IMessageBus
    {
        /// <summary>
        /// Push message to all network nodes (like broadcast).
        /// Message will receive all subscribed nodes.
        /// </summary>
        /// <param name="message"></param>
        void Push(IMessage message);

        /// <summary>
        /// Subscribe to receive published messages.
        /// </summary>
        /// <param name="subscriberNode">Reference to subscriber. Used to exclude from notifications of self published messages</param>
        /// <param name="handler"></param>
        void Subscribe(INode subscriberNode, Action<IMessage> handler);
    }
}
