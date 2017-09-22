using OrdersNetwork.Core.Network;
using OrdersNetwork.Core.Security;
using OrdersNetwork.Core.Streams;
using OrdersNetwork.Core.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Blockchain
{
    public class BlockSigner : IBlockSigner
    {
        private readonly IHashProvider _hashProvider;
        private readonly IStreamFactory _streamFactory;

        public BlockSigner(IHashProvider hashProvider, IStreamFactory streamFactory)
        {
            _hashProvider = hashProvider;
            _streamFactory = streamFactory;
        }

        public HashValue Sign(long index, INode node, string rnd, HashValue previousHash, IReadOnlyList<Transaction> transactions)
        {
            using (var stream = _streamFactory.Create())
            {
                stream.Write("Block {0} signed by {1} after {2}, Based on {3} and contains next transactions: ", index, node, previousHash, rnd);
                var first = true;
                foreach(var transaction in transactions)
                {
                    if (first)
                    {
                        first = false;
                    }
                    else
                    {
                        stream.Write(", ");
                    }
                    stream.Write(transaction.Signature.ToString());
                }
                stream.Write(".");
                var hash = _hashProvider.GetHashOfStream(stream);
                return hash;
            }
        }
    }
}
