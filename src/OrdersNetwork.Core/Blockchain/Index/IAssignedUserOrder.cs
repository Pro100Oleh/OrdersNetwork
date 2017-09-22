using OrdersNetwork.Core.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Blockchain.Index
{
    /// <summary>
    /// Transaction from user with requested order that was assigned
    /// </summary>
    public interface IAssignedUserOrder : IUserOrder
    {
        /// <summary>
        /// Transaction that confirms orders assignment
        /// </summary>
        IAssigningUserOrder AssigningUserOrder { get; }
    }
}
