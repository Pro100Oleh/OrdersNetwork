using OrdersNetwork.Core.Transactions;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Blockchain.Index
{

    public class UserIndex
    {
        public UserIndex(IUser user, Transaction lastTransaction, IImmutableList<IUserOrder> myOrders, IImmutableList<IAssigningUserOrder> assigningOrders)
        {
            User = user ?? throw new ArgumentNullException("user");
            LastTransaction = lastTransaction;
            MyOrders = myOrders ?? throw new ArgumentNullException("myOrders");
            AssigningOrders = assigningOrders ?? throw new ArgumentNullException("assignedToMeOrders");
        }

        public IUser User { get; }

        /// <summary>
        /// Last transaction of current user
        /// </summary>
        public Transaction LastTransaction { get; }

        public IImmutableList<IUserOrder> MyOrders { get; }
        public IImmutableList<IAssigningUserOrder> AssigningOrders { get; }


        public static UserIndex New(IUserOrder newOrder)
        {
            return new UserIndex(newOrder.Transaction.User, newOrder.Transaction, ImmutableList.Create(newOrder), ImmutableList.Create<IAssigningUserOrder>());
        }

        public static UserIndex New(IAssigningUserOrder assigningOrder)
        {
            return new UserIndex(assigningOrder.Transaction.User, assigningOrder.Transaction, ImmutableList.Create<IUserOrder>(), ImmutableList.Create(assigningOrder));
        }

        public UserIndex Update(IUserOrder newUserOrder)
        {
            var index = MyOrders.IndexOf(newUserOrder);
            if (index >= 0)
            {
                throw new InvalidOperationException(string.Format("MyOrders already contains user order {0}.", newUserOrder));
            }

            var newMyOrder = MyOrders.Add(newUserOrder);
            return new UserIndex(User, newUserOrder.Transaction, newMyOrder, AssigningOrders);
        }

        public UserIndex Update(IAssignedUserOrder assignedUserOrder)
        {
            var index = MyOrders.IndexOf(assignedUserOrder);
            if (index < 0)
            {
                throw new InvalidOperationException(string.Format("MyOrders does not contains user order {0}.", assignedUserOrder));
            }

            var newMyOrders = MyOrders.SetItem(index, assignedUserOrder);
            return new UserIndex(User, LastTransaction, newMyOrders, AssigningOrders);
        }

        public UserIndex Update(IAssigningUserOrder assigningUserOrder)
        {
            var index = AssigningOrders.IndexOf(assigningUserOrder);
            if (index < 0)
            {
                throw new InvalidOperationException(string.Format("AssigningOrders does not contains assigning order {0}.", assigningUserOrder));
            }

            var newAssigningOrders = AssigningOrders.SetItem(index, assigningUserOrder);
            return new UserIndex(User, LastTransaction, MyOrders, newAssigningOrders);
        }

        /// <summary>
        /// Aggreagte current user index with index from last transaction
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public UserIndex Aggregate(UserIndex user)
        {
            return new UserIndex(User, user.LastTransaction, MyOrders.AddRange(user.MyOrders), AssigningOrders.AddRange(user.AssigningOrders));
        }
    }

    
}
