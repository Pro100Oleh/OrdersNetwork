using OrdersNetwork.Core.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.WalkDogModule.Transactions
{
    public class WalkDogOrder : IOrder
    {
        public WalkDogOrder(IDog dog, string location, DateTime startTime, double payment)
        {
            Dog = dog;

            Location = location;
            StartTime = startTime;
            Payment = payment;
        }

        public string OrderType { get; } = "Walk dog";

        public IDog Dog { get; }

        public string Location { get; }
        public DateTime StartTime { get; }
        public double Payment { get; }
    }
}
