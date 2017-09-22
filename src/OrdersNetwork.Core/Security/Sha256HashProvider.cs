using OrdersNetwork.Core.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Security
{
    public class Sha256HashProvider : IHashProvider
    {
        private readonly SHA256 _sha = SHA256.Create();

        public HashValue GetHashOfStream(IStream stream)
        {
            var stringStream = (StringStream)stream;
            var content = stringStream.GetContent();
            var bytes = Encoding.UTF8.GetBytes(content);
            var hash = _sha.ComputeHash(bytes);

            //use only first 8 symbols to make short and more readable hashes
            return hash.ToHexString().Substring(0, 8);
        }
    }
}
