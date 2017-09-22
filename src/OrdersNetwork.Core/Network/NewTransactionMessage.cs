using OrdersNetwork.Core.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Network
{
    /// <summary>
    /// Network message with event about new user transaction
    /// </summary>
    public class NewTransactionMessage : IMessage
    {
        public NewTransactionMessage(INode sender, Transaction transaction)
        {
            Sender = sender;
            Transaction = transaction;
        }

        public INode Sender { get; }
        public Transaction Transaction { get; }

    }
}
