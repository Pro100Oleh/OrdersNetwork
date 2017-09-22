using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Transactions
{
    /// <summary>
    /// Signed user message.
    /// </summary>
    public class Transaction : ISigned
    {
        public Transaction(IUser user, HashValue previous, HashValue signature, UserMessage message)
        {
            User = user;
            Previous = previous;
            Signature = signature;
            Message = message;
        }

        /// <summary>
        /// Owner of message
        /// </summary>
        public IUser User { get; }

        /// <summary>
        /// Link to previous user transaction
        /// </summary>
        public HashValue Previous { get; }

        /// <summary>
        /// Transaction signature
        /// </summary>
        public HashValue Signature { get; }

        /// <summary>
        /// User message "body"
        /// </summary>
        public UserMessage Message { get; }



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
            if (!(obj is Transaction))
            {
                return false;
            }

            return Equals((Transaction)obj);
        }


        public static bool operator ==(Transaction a, Transaction b)
        {
            if (ReferenceEquals(a, null))
            {
                return ReferenceEquals(b, null);
            }

            return a.Equals(b);
        }

        public static bool operator !=(Transaction a, Transaction b)
        {
            return !(a == b);
        }


        public override string ToString()
        {
            return Signature.ToString();
        }
    }
}
