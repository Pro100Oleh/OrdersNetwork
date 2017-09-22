using OrdersNetwork.Core.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Blockchain.Index
{
    /// <summary>
    /// Transaction from user with requested order.
    /// </summary>
    public interface IUserOrder : ISigned
    {
        /// <summary>
        /// Transaction with requested order
        /// </summary>
        Transaction Transaction { get; }

        /// <summary>
        /// Reguested order
        /// </summary>
        IOrder Order { get; }

        /// <summary>
        /// Is order was assigned for implementation
        /// </summary>
        bool IsAssigned { get;  }
    }
}
