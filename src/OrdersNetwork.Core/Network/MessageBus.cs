using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Network
{
    public class MessageBus : IMessageBus
    {
        private IImmutableList<Subscriber> _subscribers;

        private struct Subscriber
        {
            public INode Node;
            public Action<IMessage> Handler;
        }

        public MessageBus()
        {
            _subscribers = ImmutableList.Create<Subscriber>();
        }

        public void Push(IMessage message)
        {
            Trace.TraceInformation("[network]\tpush message {0}", message);
            foreach(var subscriber in _subscribers)
            {
                if (!subscriber.Node.Equals(message.Sender))
                {
                    subscriber.Handler(message);
                }
            }
        }

        public void Subscribe(INode subscriberNode, Action<IMessage> handler)
        {
            _subscribers = _subscribers.Add(new Subscriber { Node = subscriberNode, Handler = handler });
        }

    }
}
