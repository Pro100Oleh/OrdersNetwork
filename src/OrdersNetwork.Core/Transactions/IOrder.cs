using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Transactions
{
    /// <summary>
    /// Client order for execution.
    /// </summary>
    public interface IOrder
    {
        string OrderType { get; }

        /// <summary>
        /// Your payment to user who will execute your order.
        /// </summary>
        double Payment { get; }
    }
}
