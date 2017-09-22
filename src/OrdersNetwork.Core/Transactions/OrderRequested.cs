using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Transactions
{
    /// <summary>
    /// Network user message about new order
    /// </summary>
    public class OrderRequested : UserMessage
    {
        public OrderRequested(IOrder order)
        {
            Order = order ?? throw new NullReferenceException("order");
        }

        public IOrder Order { get; }
    }
}
