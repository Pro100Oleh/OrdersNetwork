using OrdersNetwork.Core;
using OrdersNetwork.Core.Streams;
using OrdersNetwork.Core.Transactions;
using OrdersNetwork.WalkDogModule.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.WalkDogModule.StreamBuilders
{
    public class WalkDogOrderStreamWriter : IOrderStreamWriter
    {
        public string OrderType { get; } = "Walk dog";

        public void Write(IStream stream, IOrder order)
        {
            var walkDogOrder = order as WalkDogOrder;

            if (walkDogOrder == null)
            {
                throw new ArgumentException("wrong order");
            }


            stream.Write(walkDogOrder.Dog.Name);
            if (walkDogOrder.Dog.Small)
            {
                stream.Write(" (small)");
            }
            if (walkDogOrder.Dog.Aggresive)
            {
                stream.Write(" (aggresive)");
            }

            if (walkDogOrder.Location != null)
            {
                stream.Write(" in ");
                stream.Write(walkDogOrder.Location);
            }

            stream.Write(" at ");
            stream.Write(walkDogOrder.StartTime.ToString("O"));

            if (walkDogOrder.Payment > 0)
            {
                stream.Write(string.Format(" for {0:F2}$", walkDogOrder.Payment));
            }
        }
    }
}
