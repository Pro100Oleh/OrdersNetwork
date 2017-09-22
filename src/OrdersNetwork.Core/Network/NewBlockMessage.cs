using OrdersNetwork.Core.Blockchain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Network
{
    /// <summary>
    /// Network message with event about new block
    /// </summary>
    public class NewBlockMessage : IMessage
    {
        public NewBlockMessage(INode sender, Block block)
        {
            Sender = sender;
            Block = block;
        }

        public INode Sender { get; }

        /// <summary>
        /// A new signed block of blockchain
        /// </summary>
        public Block Block { get; }
    }
}
