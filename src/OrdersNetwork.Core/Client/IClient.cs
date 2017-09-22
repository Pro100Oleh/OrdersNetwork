using OrdersNetwork.Core.Blockchain.Index;
using OrdersNetwork.Core.Network;
using OrdersNetwork.Core.Transactions;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Client
{
    /// <summary>
    /// Client API for OrdersNetwork
    /// </summary>
    public interface IClient
    {
        /// <summary>
        /// My name
        /// </summary>
        IUser User { get; }

        /// <summary>
        /// My network name
        /// </summary>
        INode UserNode { get; }

        /// <summary>
        /// Get index of my orders
        /// </summary>
        /// <returns></returns>
        UserIndex GetMyInfo();

        /// <summary>
        /// Publish a new order to network.
        /// Other clients will see order only when it will be included to new block by server.
        /// </summary>
        /// <param name="order"></param>
        void NewOrder(IOrder order);

        /// <summary>
        /// Publish message with your intent to execute client orders.
        /// List of orders should be from not assigned orders.
        /// When server has several messages from different clients to execute same order, Server a free to choose best liked message.
        /// </summary>
        /// <param name="fee">What part of clients payments you will share with server which will include you message to block. Value in range [0..1].</param>
        /// <param name="orders"></param>
        void AssignOrdersToMe(double fee, params IUserOrder[] orders);

        /// <summary>
        /// Return current list of unassigned orders.
        /// </summary>
        /// <returns></returns>
        IImmutableDictionary<HashValue, IUserOrder> GetUnassignedOrders();
    }
}
