using OrdersNetwork.Core.Blockchain;
using OrdersNetwork.Core.Blockchain.Index;
using OrdersNetwork.Core.Blockchain.Validators;
using OrdersNetwork.Core.Transactions;
using OrdersNetwork.WalkDogModule.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.WalkDogModule.Blockchain
{
    public class NewWalkDogOrderValidator : INewOrderValidator
    {
        public string OrderType { get; } = "Walk dog";

        public void Validate(NodeIndex nodeIndex, UserIndex userIndex, IOrder order)
        {
            var walkDogOrder = order as WalkDogOrder;

            if (walkDogOrder == null)
            {
                throw new ArgumentException("wrong order");
            }

            if (userIndex != null)
            {
                var lastDogOrder = userIndex.MyOrders
                    .Where(uo => uo.Order is WalkDogOrder)
                    .Select(uo => (WalkDogOrder)uo.Order)
                    .Where(o => o.Dog == walkDogOrder.Dog)
                    .LastOrDefault();

                if (lastDogOrder != null)
                {
                    ValidateNextOrder(lastDogOrder, walkDogOrder);
                }
            }
        }

        private void ValidateNextOrder(WalkDogOrder lastOrder, WalkDogOrder nextOrder)
        {
            if (nextOrder.StartTime <= lastOrder.StartTime)
            {
                throw new TransactionDeclinedException("Wrong order StartTime.");
            }
            
        }
    }
}
