using OrdersNetwork.Core.Transactions;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Blockchain.Index
{
    public class OrdersIndex
    {
        private OrdersIndex()
        {
        }

        private OrdersIndex(IImmutableDictionary<HashValue, IUserOrder> allOrders, IImmutableDictionary<HashValue, IUserOrder> unassignedOrders, IImmutableDictionary<HashValue, IAssigningUserOrder> assigningOrders)
        {
            AllOrders = allOrders ?? throw new ArgumentNullException("allOrders");
            UnassignedOrders = unassignedOrders ?? throw new ArgumentNullException("unassignedOrders");
            AssigningOrders = assigningOrders ?? throw new ArgumentNullException("assigningOrders");
        }


        public static OrdersIndex Empty { get; } = new OrdersIndex();

        public IImmutableDictionary<HashValue, IUserOrder> AllOrders { get; }
        public IImmutableDictionary<HashValue, IUserOrder> UnassignedOrders { get; }
        public IImmutableDictionary<HashValue, IAssigningUserOrder> AssigningOrders { get; }

        public bool IsEmpty
        {
            get { return AllOrders == null; }
        }

        internal IEnumerable<IUserOrder> SelectUnassignedOrders(IEnumerable<HashValue> orderHashes)
        {
            foreach(var orderHash in orderHashes)
            {
                IUserOrder userOrder;
                if (!UnassignedOrders.TryGetValue(orderHash, out userOrder))
                {
                    throw new TransactionDeclinedException(string.Format("Order {0} is not from unassigned set.", orderHash));
                }

                yield return userOrder;
            }
        }

        public OrdersIndex Add(IReadOnlyList<IUserOrder> newOrders, IReadOnlyList<IAssigningUserOrder> assigningOrders)
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("Index in empty");
            }

            var allOrders = AllOrders.AddRange(newOrders);
            var newUnassignedOrdersDic = UnassignedOrders.RemoveRange(assigningOrders.SelectMany(o => o.Orders)).AddRange(newOrders);
            var allAssigningOrders = AssigningOrders.AddRange(assigningOrders);

            return new OrdersIndex(allOrders, newUnassignedOrdersDic, allAssigningOrders);
        }

        public static OrdersIndex New(IReadOnlyList<IUserOrder> newOrders)
        {
            if (newOrders.Any(o => o.IsAssigned))
            {
                throw new ArgumentException("Can't use assigned orders in new index.");
            }

            var allOrders = newOrders.ToImmutableDictionary();
            return new OrdersIndex(allOrders, allOrders, ImmutableDictionary.Create<HashValue, IAssigningUserOrder>());
        }
    }
}
