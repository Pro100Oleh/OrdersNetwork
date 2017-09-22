using FluentAssertions;
using OrdersNetwork.Core;
using OrdersNetwork.Core.Blockchain.Index;
using OrdersNetwork.Core.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Tests
{
    public static class AssertExtensions
    {
        public static TestOrder AsTestOrder(this IOrder order)
        {
            order.Should().BeOfType<TestOrder>();
            return (TestOrder)order;
        }

        public static IOrder ShouldBeOrder(this IOrder order, string name)
        {
            order.AsTestOrder().Name.Should().Be(name);
            return order;
        }

        public static OrderRequested AsOrderRequested(this UserMessage message)
        {
            message.Should().BeOfType<OrderRequested>();
            return (OrderRequested)message;
        }

        public static OrdersAssigned AsOrdersAssigned(this UserMessage message)
        {
            message.Should().BeOfType<OrdersAssigned>();
            return (OrdersAssigned)message;
        }


        public static OrdersIndex ShouldHaveTransactions(this OrdersIndex index, params Transaction[] transactions)
        {
            index.AllOrders.Count().Should().Be(transactions.Length);

            foreach(var transaction in transactions)
            {
                index.AllOrders[transaction.Signature].Transaction.Should().BeSameAs(transaction);
            }

            return index;
        }

        public static OrdersIndex ShouldHaveUnassignedOrders(this OrdersIndex index, params Transaction[] transactions)
        {
            index.UnassignedOrders.Count().Should().Be(transactions.Length);

            foreach (var transaction in transactions)
            {
                index.UnassignedOrders[transaction.Signature].Should().BeSameAs(transaction.Message.AsOrderRequested().Order);
            }

            return index;
        }


        public static UsersIndex ShouldHaveUsers(this UsersIndex index, params IUser[] users)
        {
            users = users ?? Array.Empty<IUser>();

            index.AllUsers.Should().HaveCount(users.Length);
            foreach(var user in users)
            {
                index.AllUsers.ContainsKey(user).Should().BeTrue();
            }

            return index;
        }

        public static void ShouldHaveOrders(this IReadOnlyList<IOrder> orders, params IOrder[] expectedOrders)
        {
            expectedOrders = expectedOrders ?? Array.Empty<IOrder>();

            orders.Count.Should().Be(expectedOrders.Length);

            for(var index = 0;index<expectedOrders.Length;index++)
            {
                orders[index].Should().BeSameAs(expectedOrders[index]);
            }

        }
    }

}
