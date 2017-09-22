using OrdersNetwork.Core.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Streams
{
    class OrdersAssignedStreamWriter : IStreamWriter<OrdersAssigned>
    {
        private readonly Dictionary<string, IOrderStreamWriter> _writers;

        public OrdersAssignedStreamWriter(IEnumerable<IOrderStreamWriter> writers)
        {
        }

        public void Write(IStream stream, OrdersAssigned ordersAssigned)
        {

            stream.Write("Assigned with fee {0:F2} orders ", ordersAssigned.Fee);
            foreach(var order in ordersAssigned.Orders)
            {
                stream.Write(" {0}", order);
            }
            stream.Write(".");
        }
    }
}
