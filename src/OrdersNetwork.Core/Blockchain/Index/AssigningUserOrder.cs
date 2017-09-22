using OrdersNetwork.Core.Transactions;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Blockchain.Index
{
    internal class AssigningUserOrder : IAssigningUserOrder
    {
        public AssigningUserOrder(Transaction transaction, IEnumerable<IUserOrder> orders)
        {
            Transaction = transaction ?? throw new ArgumentNullException("transaction");
            if (!(Transaction.Message is OrdersAssigned))
            {
                throw new ArgumentException("transaction");
            }
            Orders = ImmutableList.Create<IAssignedUserOrder>((orders ?? throw new ArgumentNullException("orders"))
                .Select(o => new AssignedUserOrder(o.Transaction, this))
               .ToArray()
                );

        }

        public Transaction Transaction { get; }

        public HashValue Signature { get { return Transaction.Signature; } }

        public OrdersAssigned OrdersAssigned
        {
            get { return (OrdersAssigned)Transaction.Message; }
        }

        public IImmutableList<IAssignedUserOrder> Orders { get; }


        public override int GetHashCode()
        {
            return Signature.GetHashCode();
        }

        public bool Equals(ISigned signed)
        {
            if (ReferenceEquals(signed, null))
            {
                return false;
            }

            return Signature.Equals(signed.Signature);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is AssigningUserOrder))
            {
                return false;
            }

            return Equals((AssigningUserOrder)obj);
        }


        public override string ToString()
        {
            return Transaction.ToString();
        }
    }
}
