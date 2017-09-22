using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Transactions
{
    /// <summary>
    /// Network user message about orders assignation
    /// </summary>
    public class OrdersAssigned : UserMessage
    {
        public OrdersAssigned(double fee, IImmutableList<HashValue> orders)
        {
            Fee = fee;
            Orders = orders ?? throw new NullReferenceException("orders");
        }

        /// <summary>
        /// Fee (Part of orders payment) that will be a prize for node that include this message to blockchain
        /// </summary>
        public double Fee { get; }

        /// <summary>
        /// References to assigned orders
        /// </summary>
        public IImmutableList<HashValue> Orders { get; }

    }
}
