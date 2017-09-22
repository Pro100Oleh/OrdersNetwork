using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Transactions
{
    public class TransactionFactory : ITransactionFactory
    {
        private readonly ITransactionSigner _transactionSigner;

        public TransactionFactory(ITransactionSigner transactionSigner)
        {
            _transactionSigner = transactionSigner;
        }

        public Transaction Create(IUser user, HashValue previous, UserMessage message)
        {
            var hash = _transactionSigner.Sign(user, previous, message);
            return new Transaction(user, previous, hash, message);
        }
    }
}
