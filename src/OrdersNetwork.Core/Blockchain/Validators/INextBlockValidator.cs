using OrdersNetwork.Core.Blockchain.Index;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Blockchain.Validators
{
    public interface INextBlockValidator
    {
        void Validate(BlocksIndex index, Block nextBlock);
    }
}
