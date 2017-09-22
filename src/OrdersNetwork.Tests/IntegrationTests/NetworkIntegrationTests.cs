using FluentAssertions;
using NUnit.Framework;
using OrdersNetwork.Core.Blockchain.Index;
using OrdersNetwork.Core.Client;
using OrdersNetwork.Core.Server;
using OrdersNetwork.WalkDogModule.Transactions;
using System;
using System.Linq;

namespace OrdersNetwork.Tests.IntegrationTests
{
    [TestFixture]
    class NetworkIntegrationTests
    {
        private IntegrationTest _test;
            

        [OneTimeSetUp]
        public void Setup()
        {
            _test = new IntegrationTest();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            if (_test != null)
            {
                _test.Dispose();
            }
        }

        [Test]
        public void Publish_Order_And_Assign()
        {
            //init
            var server = _test.StartServer("server");

            var dogLover1 = _test.CreateClient("dogLover1");

            var walker1 = _test.CreateClient("walker1");

            //dogLover1 make a WalkDogOrder
            var walkDog1Order = new WalkDogOrder(new Dog("dog1"), "nyk", DateTime.Now, 17.9);
            dogLover1.NewOrder(walkDog1Order);

            //server make and publish block #1
            server.WaitForBlock(1);

            //walker1 can see a new order
            var unassignedOrders = walker1.GetUnassignedOrders();
            unassignedOrders.Count.Should().Be(1);
            var unassignedOrder = unassignedOrders.Values.Single();

            unassignedOrder.Transaction.User.Should().BeSameAs(dogLover1.User);
            unassignedOrder.Order.Should().BeSameAs(walkDog1Order);

            //walker1 make assigned order
            walker1.AssignOrdersToMe(0.1, unassignedOrder);

            server.WaitForBlock(2);

            //dogLover1 can see his order was assigned
            var myOrder = dogLover1.GetMyInfo().MyOrders.Single();
            myOrder.IsAssigned.Should().BeTrue();
            myOrder.Order.Should().BeSameAs(walkDog1Order);

            //dogLover1 can see executor
            ((IAssignedUserOrder)myOrder).AssigningUserOrder.Transaction.User.Should().BeSameAs(walker1.User);

            //walker1 can see hist assigment orders
            var myAssigningOrders = walker1.GetMyInfo().AssigningOrders.Single();
            myAssigningOrders.Transaction.User.Should().BeSameAs(walker1.User);
            myAssigningOrders.Orders.Single().Should().Be(myOrder);
        }

        [Test]
        public void Publish_2_Orders_And_Select_Best_Fee()
        {
            //init
            var server = _test.StartServer("server");

            var dogLover1 = _test.CreateClient("dogLover1");
            var dogLover2 = _test.CreateClient("dogLover2");

            var walker1 = _test.CreateClient("walker1");
            var walker2 = _test.CreateClient("walker2");

            //each dog lover make a WalkDogOrder
            var walkDog1Order = new WalkDogOrder(new Dog("dog1"), "nyk", DateTime.Now, 14.0);
            dogLover1.NewOrder(walkDog1Order);

            var walkDog2Order = new WalkDogOrder(new Dog("dog2"), "nyk", DateTime.Now, 20.0);
            dogLover2.NewOrder(walkDog2Order);

            server.WaitForBlock(1);

            //walker1 says he want to execute only walkDog1OrderSecond
            var unassignedOrderForWalker1 = walker1.GetUnassignedOrders().Values.Single(o => o.Order == walkDog1Order);
            walker1.AssignOrdersToMe(0.2, unassignedOrderForWalker1);

            //walker2 want to execute both orders
            walker2.AssignOrdersToMe(0.1, walker2.GetUnassignedOrders().Values.ToArray());

            //server will choose walker2 proposal
            server.WaitForBlock(2);

            var myAssigningOrder = walker2.GetMyInfo().AssigningOrders.Single();
            myAssigningOrder.Orders.Count.Should().Be(2);

            //walker1 was excluded from execution
            walker1.GetMyInfo().Should().BeNull();
        }
    }
}
