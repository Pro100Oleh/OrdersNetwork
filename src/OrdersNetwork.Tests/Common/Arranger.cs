using OrdersNetwork.Core;
using OrdersNetwork.Core.Blockchain.Index;
using OrdersNetwork.Core.Transactions;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Tests
{
    public static class Arranger
    {
        public static string RndString()
        {
            return Guid.NewGuid().ToString("D");
        }

        public static HashValue RndHashValue()
        {
            return RndString();
        }

        private static string Value(this string value)
        {
            return value ?? RndString();
        }

        private static HashValue Value(this HashValue? value)
        {
            return value ?? RndHashValue();
        }

        public static T[] ArrayValue<T>(this IEnumerable<T> items)
        {
            if (items == null)
            {
                return Array.Empty<T>();
            }
            return items.ToArray();
        }

        public static IImmutableList<T> ImmutableListValue<T>(this IEnumerable<T> items)
        {
            return ImmutableList.Create(items.ArrayValue());
        }

        public static IUser User(string name = null)
        {
            return new NetworkUser(name.Value());
        }

        public static IOrder Order(string name = null, double? payment = null)
        {
            return new TestOrder(name.Value(), payment ?? 1.01);
        }

        //public static UserIndex UserIndex(string user = null, HashValue? lastTransaction = null, IEnumerable<IOrder> myOrders = null, IEnumerable<IOrder> assignedToMeOrders = null)
        //{
        //    return new UserIndex(
        //        User(user),
        //        lastTransaction.Value(),
        //        myOrders.ImmutableListValue(),
        //        assignedToMeOrders.ImmutableListValue()
        //        );
        //}


        public static OrderRequested OrderRequested(string name = null, double? payment = null)
        {
            return new OrderRequested(Order(name, payment));
        }

        public static OrdersAssigned OrdersAssigned(double? fee = null, IEnumerable<HashValue> orders = null)
        {
            var immutableOrders = ImmutableList.Create((orders ?? new[] { RndHashValue() }).ToArray());
            return new OrdersAssigned(fee ?? 0.1, immutableOrders);
        }



        public static Transaction Transaction(string user = null, HashValue? previous = null, HashValue? hash = null, UserMessage message = null)
        {
            return new Transaction(User(user), previous.Value(), hash.Value(), message ?? OrderRequested());
        }
    }
}
