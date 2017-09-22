using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Blockchain
{
    public class BlockDeclinedException : Exception
    {
        public BlockDeclinedException(string message)
            : base(message)
        {

        }
    }
}
