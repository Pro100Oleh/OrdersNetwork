using OrdersNetwork.Core.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Blockchain.Validators
{
    public class NewTransactionIncludeResult
    {
        private NewTransactionIncludeResult()
        {
        }

        public NewTransactionIncludeResult(Transaction[] toRemoveTransactions)
        {
            if (toRemoveTransactions == null)
            {
                throw new ArgumentNullException("toRemoveTransactions");
            }

            ToRemoveTransactions = toRemoveTransactions;
        }

        /// <summary>
        /// List of ongoing transaction that should be removed to include a new one
        /// </summary>
        public Transaction[] ToRemoveTransactions { get; }

        public static NewTransactionIncludeResult None { get; } = new NewTransactionIncludeResult();

        public static NewTransactionIncludeResult Include { get; } = new NewTransactionIncludeResult(new Transaction[0]);

        public bool IsNone
        {
            get { return ToRemoveTransactions == null; }
        }
    }
}
