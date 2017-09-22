using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Blockchain.Index
{
    public class UsersIndex
    {
        private UsersIndex()
        {

        }

        private UsersIndex(IImmutableDictionary<IUser, UserIndex> users)
        {
            AllUsers = users ?? throw new ArgumentNullException("users");
        }

        public IImmutableDictionary<IUser, UserIndex> AllUsers { get; }

        public static UsersIndex Empty { get; } = new UsersIndex();

        public bool IsEmpty
        {
            get { return AllUsers == null; }
        }


        public UsersIndex Add(IReadOnlyList<IUserOrder> newOrders, IReadOnlyList<IAssigningUserOrder> assigningOrders)
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("Index in empty");
            }

            ValidateUsers(newOrders, assigningOrders);

            var updates = new Dictionary<IUser, UserIndex>();

            foreach(var newOrder in newOrders)
            {
                UserIndex oldUser;
                UserIndex newUser;
                if (!AllUsers.TryGetValue(newOrder.Transaction.User, out oldUser))
                {
                    newUser = UserIndex.New(newOrder);
                }
                else
                {
                    newUser = oldUser.Update(newOrder);
                }
                updates.Add(newUser.User, newUser);
            }

            foreach (var assigningOrder in assigningOrders)
            {
                //update assigning user
                UserIndex oldUser;

                if (!updates.TryGetValue(assigningOrder.Transaction.User, out oldUser))
                {
                    if (!AllUsers.TryGetValue(assigningOrder.Transaction.User, out oldUser))
                    {
                        oldUser = null;
                    }
                }

                UserIndex newUser;
                if (oldUser == null)
                {
                    newUser = UserIndex.New(assigningOrder);
                }
                else
                {
                    newUser = oldUser.Update(assigningOrder);
                }

                updates.Add(newUser.User, newUser);

                //update assigned users
                foreach (var assignedOrder in assigningOrder.Orders)
                {
                    UserIndex oldAssignedUser;

                    if (!updates.TryGetValue(assignedOrder.Transaction.User, out oldAssignedUser))
                    {
                        oldAssignedUser = AllUsers[assignedOrder.Transaction.User];
                    }

                    var newAssignedUser = oldAssignedUser.Update(assignedOrder);
                    updates[newAssignedUser.User] = newAssignedUser;
                }
            }

            var newUsersDic = AllUsers.SetItems(updates);

            return new UsersIndex(newUsersDic);
        }

        public static UsersIndex New(IReadOnlyList<IUserOrder> newOrders)
        {
            ValidateUsers(newOrders, Array.Empty<IAssigningUserOrder>());
            var users = newOrders.Select(o => UserIndex.New(o)).ToImmutableDictionary(u => u.User);
            
            return new UsersIndex(users);
        }

        private static void ValidateUsers(IReadOnlyList<IUserOrder> newOrders, IReadOnlyList<IAssigningUserOrder> assigningOrders)
        {
            var usedUsers = new HashSet<IUser>();

            foreach(var newOrder in newOrders)
            {
                var user = newOrder.Transaction.User;
                if (!usedUsers.Add(user))
                {
                    throw new BlockDeclinedException(string.Format("Can't use several transactions from one user {0} in one block.", user));
                }
            }

            foreach (var assigningOrder in assigningOrders)
            {
                var user = assigningOrder.Transaction.User;
                if (!usedUsers.Add(user))
                {
                    throw new BlockDeclinedException(string.Format("Can't use several transactions from one user {0} in one block.", user));
                }
            }
        }
    }
}
