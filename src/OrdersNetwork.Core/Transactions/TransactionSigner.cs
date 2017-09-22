using OrdersNetwork.Core.Security;
using OrdersNetwork.Core.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Transactions
{
    public class TransactionSigner : ITransactionSigner
    {
        private readonly IHashProvider _hashProvider;
        private readonly IStreamFactory _streamFactory;
        private readonly IStreamWriter<UserMessage> _messageStreamWriter;

        public TransactionSigner(IHashProvider hashProvider, IStreamFactory streamFactory, IStreamWriter<UserMessage> messageStreamWriter)
        {
            _hashProvider = hashProvider;
            _streamFactory = streamFactory;
            _messageStreamWriter = messageStreamWriter;
        }

        public HashValue Sign(IUser user, HashValue previousHash, UserMessage message)
        {
            using (var stream = _streamFactory.Create())
            {
                stream.Write("Client {0} after {1} ask for ", user, previousHash);
                _messageStreamWriter.Write(stream, message);

                var hash = _hashProvider.GetHashOfStream(stream);
                return hash;
            }
        }
    }
}
