using OrdersNetwork.Core.Network;
using OrdersNetwork.Core.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Blockchain
{
    public class Block : ISigned
    {
        public Block(long index, INode node, string rnd, HashValue previousBlock, HashValue signature, IReadOnlyList<Transaction> transactions)
        {
            Index = index;
            Node = node;
            Rnd = rnd;
            PreviousBlock = previousBlock;
            Signature = signature;
            Transactions = transactions;
        }

        public long Index { get; set; }

        /// <summary>
        /// Name of node generated this block
        /// </summary>
        public INode Node { get; }

        public string Rnd { get; }
        public HashValue PreviousBlock { get; }
        public HashValue Signature { get; }

        public IReadOnlyList<Transaction> Transactions { get; }

        public override string ToString()
        {
            return string.Format("#{0}:{1}", Index, Signature);
        }


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
            if (!(obj is Block))
            {
                return false;
            }

            return Equals((Block)obj);
        }
    }
}
