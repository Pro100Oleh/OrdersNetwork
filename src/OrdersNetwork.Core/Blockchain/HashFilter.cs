using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Blockchain
{
    public class HashFilter : IHashFilter
    {
        public bool IsSatisfy(HashValue hash, string factor)
        {
            var hashValue = hash.Value;
            //align size
            if(factor.Length < hashValue.Length)
            {
                factor = "00000000000000".Substring(0, hashValue.Length - factor.Length) + factor;
            }

            return hashValue.CompareTo(factor) <= 0;
        }
    }
}
