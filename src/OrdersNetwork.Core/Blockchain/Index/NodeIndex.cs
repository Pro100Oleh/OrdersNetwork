using OrdersNetwork.Core.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Blockchain.Index
{
    /// <summary>
    /// describe index of blockchain
    /// </summary>
    public class NodeIndex
    {
        private NodeIndex()
        {
            Blocks = BlocksIndex.Empty;
            Orders = OrdersIndex.Empty;
            Users = UsersIndex.Empty;
        }

        private NodeIndex(BlocksIndex blocks, OrdersIndex orders, UsersIndex users)
        {
            Blocks = blocks ?? throw new ArgumentNullException("blocks");
            Orders = orders ?? throw new ArgumentNullException("orders");
            Users = users ?? throw new ArgumentNullException("users");
        }


        public BlocksIndex Blocks { get; }
        public OrdersIndex Orders { get; }
        public UsersIndex Users { get; }

        public static NodeIndex Empty { get; } = new NodeIndex ();

        public bool IsEmpty
        {
            get { return Blocks.IsEmpty; }
        }

        /// <summary>
        /// Add next valid block
        /// </summary>
        /// <param name="block"></param>
        /// <returns></returns>
        public NodeIndex Add(Block block)
        {
            IReadOnlyList<IUserOrder> newOrders;
            IReadOnlyList<IAssigningUserOrder> assigningOrders;
            GetOngoingUserOrders(block.Transactions, out newOrders, out assigningOrders);

            if (IsEmpty)
            {
                return new NodeIndex(BlocksIndex.New(block), OrdersIndex.New(newOrders), UsersIndex.New(newOrders));
            }

            var newBlocks = Blocks.Add(block);
            var newTransactions = Orders.Add(newOrders, assigningOrders);
            var newUsers = Users.Add(newOrders, assigningOrders);

            return new NodeIndex(newBlocks, newTransactions, newUsers);
        }

        private void GetOngoingUserOrders(IReadOnlyList<Transaction> newTransactions, out IReadOnlyList<IUserOrder> newOrders, out IReadOnlyList<IAssigningUserOrder> assigningOrders)
        {
            newOrders = newTransactions
                .Where(t => t.Message is OrderRequested)
                .Select(t => new UnassignedUserOrder(t))
                .ToArray();

            if (IsEmpty)
            {
                if (newOrders.Count != newTransactions.Count)
                {
                    throw new BlockDeclinedException("Can't use assignment orders for first block.");
                }

                assigningOrders = default(IAssigningUserOrder[]);
            }
            else
            {
                assigningOrders = newTransactions
                    .Where(t => t.Message is OrdersAssigned)
                    .Select(t => new AssigningUserOrder(t,
                        orders: Orders.SelectUnassignedOrders(((OrdersAssigned)t.Message).Orders))
                    )
                    .ToArray();

                //validate or dublicates
                var referencedOrders = new HashSet<HashValue>();
                foreach(var assigningOrder in assigningOrders)
                {
                    foreach(var assignedOrder in assigningOrder.Orders)
                    {
                        if (!referencedOrders.Add(assignedOrder.Transaction.Signature))
                        {
                            throw new BlockDeclinedException("Detected that several transactions assigned same order.");
                        }
                    }
                }
            }
        }
        
    }
}
