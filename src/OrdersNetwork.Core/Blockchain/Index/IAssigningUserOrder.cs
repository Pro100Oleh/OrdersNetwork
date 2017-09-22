using OrdersNetwork.Core.Transactions;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Blockchain.Index
{
    /// <summary>
    /// Transaction from user with orders assignment
    /// </summary>
    public interface IAssigningUserOrder : ISigned
    {
        /// <summary>
        /// Transaction that confirms orders assignment
        /// </summary>
        Transaction Transaction { get; }

        /// <summary>
        /// Transaction OrdersAssigned details
        /// </summary>
        OrdersAssigned OrdersAssigned { get; }

        /// <summary>
        /// List of assignment user orders
        /// </summary>
        IImmutableList<IAssignedUserOrder> Orders { get; }
    }
}
