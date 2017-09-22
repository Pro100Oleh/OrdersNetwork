using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Transactions
{
    public interface ITransactionFactory
    {
        Transaction Create(IUser user, HashValue previous, UserMessage message);
    }
}
