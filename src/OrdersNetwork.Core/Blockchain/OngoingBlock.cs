using OrdersNetwork.Core.Network;
using OrdersNetwork.Core.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Blockchain
{
    /// <summary>
    /// Describe a new not confirmed block that probably will be signed in future
    /// </summary>
    public class OngoingBlock
    {
        public OngoingBlock(long index, INode node, HashValue previousHash, IReadOnlyList<Transaction> transactions)
        {
            Index = index;
            Node = node;
            PreviousHash = previousHash;
            Transactions = transactions;
        }

        public long Index { get; private set; }
        public INode Node { get; }

        public HashValue PreviousHash { get; private set; }

        public IReadOnlyList<Transaction> Transactions { get; private set; }

        public OngoingBlock Aggregate(Transaction addTransaction, Transaction[] removeTransactions)
        {
            var toRemove = new HashSet<Transaction>(removeTransactions);
            var newTransactions = Transactions
                .Where(t => !toRemove.Contains(t))
                .Union(new[] { addTransaction })
                .ToList();


            return new OngoingBlock(Index, Node, PreviousHash, newTransactions);
        }

    }
}
