using OrdersNetwork.Core.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Streams
{
    /// <summary>
    /// Stream writer of one specific type of order
    /// </summary>
    public interface IOrderStreamWriter : IStreamWriter<IOrder>
    {
        string OrderType { get; }
    }
}
