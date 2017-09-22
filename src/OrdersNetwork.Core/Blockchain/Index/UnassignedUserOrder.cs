using OrdersNetwork.Core.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Blockchain.Index
{
    internal class UnassignedUserOrder : IUserOrder
    {
        public UnassignedUserOrder(Transaction transaction)
        {
            Transaction = transaction ?? throw new ArgumentNullException("transaction");
            if (!(Transaction.Message is OrderRequested))
            {
                throw new ArgumentException("transaction");
            }
        }

        public Transaction Transaction { get; }

        public HashValue Signature { get { return Transaction.Signature; } }

        public IOrder Order
        {
            get { return ((OrderRequested)Transaction.Message).Order; }
        }

        public bool IsAssigned { get { return false; } }


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
            if (!(obj is ISigned))
            {
                return false;
            }

            return Equals((ISigned)obj);
        }


        public override string ToString()
        {
            return Transaction.ToString();
        }
    }
}
