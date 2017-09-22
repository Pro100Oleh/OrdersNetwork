using OrdersNetwork.Core.Blockchain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Server
{
    /// <summary>
    /// Server that listen for a new client transactions and sign next blocks
    /// </summary>
    public interface IServer
    {
        /// <summary>
        /// What when server sign next block and publish it to all subscribes
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        Block WaitForBlock(int index);
        
    }
}
