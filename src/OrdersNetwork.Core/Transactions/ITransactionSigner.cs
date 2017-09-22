using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Transactions
{
    public interface ITransactionSigner
    {
        HashValue Sign(IUser user, HashValue previousHash, UserMessage message);
    }
}
