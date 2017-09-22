using OrdersNetwork.Core.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Streams
{
    public class UserMessageStreamWriter : IStreamWriter<UserMessage>
    {
        private readonly IStreamWriter<OrderRequested> _orderRequestedStreamWriter;
        private readonly IStreamWriter<OrdersAssigned> _orderAssignedStreamWriter;

        public UserMessageStreamWriter(IStreamWriter<OrderRequested> orderRequestedStreamWriter, IStreamWriter<OrdersAssigned> orderAssignedStreamWriter)
        {
            _orderRequestedStreamWriter = orderRequestedStreamWriter;
            _orderAssignedStreamWriter = orderAssignedStreamWriter;
        }

        public void Write(IStream stream, UserMessage obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            var orderRequested = obj as OrderRequested;
            if (orderRequested != null)
            {
                _orderRequestedStreamWriter.Write(stream, orderRequested);
            }
            else
            {
                var orderAssigned = obj as OrdersAssigned;
                if (orderAssigned != null)
                {
                    _orderAssignedStreamWriter.Write(stream, orderAssigned);
                }
                else
                {
                    throw new NotSupportedException(string.Format("Unknown obj type {0}", obj.GetType()));
                }
            }

        }
    }
}
