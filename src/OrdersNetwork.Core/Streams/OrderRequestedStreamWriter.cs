using OrdersNetwork.Core.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Streams
{
    public class OrderRequestedStreamWriter : IStreamWriter<OrderRequested>
    {
        private readonly Dictionary<string, IOrderStreamWriter> _writers;

        public OrderRequestedStreamWriter(IEnumerable<IOrderStreamWriter> writers)
        {
            _writers = writers.ToDictionary(b => b.OrderType);
        }

        public void Write(IStream stream, OrderRequested orderRequested)
        {
            var order = orderRequested.Order;
            var writer = _writers[order.OrderType];

            stream.Write(order.OrderType);
            stream.Write(" ");
            writer.Write(stream, order);
        }
    }
}
