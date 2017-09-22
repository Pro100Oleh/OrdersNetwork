using FluentAssertions;
using OrdersNetwork.Core.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Tests
{
    public class TestOrder : IOrder
    {
        public TestOrder(string name, double payment)
        {
            Name = name;
            Payment = payment;
        }

        public string Name { get; }

        public string OrderType { get; } = "test order";

        public double Payment { get; }
    }
    
}
