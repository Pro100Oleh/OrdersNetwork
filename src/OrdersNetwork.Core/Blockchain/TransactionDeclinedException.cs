using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Blockchain
{
    public class TransactionDeclinedException : Exception
    {
        public TransactionDeclinedException(string message)
            : base(message)
        {

        }
    }
}
