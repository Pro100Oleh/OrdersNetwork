using OrdersNetwork.Core.Blockchain;
using OrdersNetwork.Core.Blockchain.Index;
using OrdersNetwork.Core.Blockchain.Validators;
using OrdersNetwork.Core.Network;
using OrdersNetwork.Core.Transactions;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Client
{
    public class BlockchainClient : IClient
    {
        private readonly ITransactionFactory _transactionFactory;
        private readonly INextBlockValidator _nextBlockValidator;
        private readonly IMessageBus _messageBus;

        private Transaction _lastTransaction;
        private Transaction _lastCommitedTransaction;

        private NodeIndex _index;

        public BlockchainClient(IUser user, INode userNode, IMessageBus messageBus, ITransactionFactory transactionFactory, INextBlockValidator nextBlockValidator)
        {
            User = user;
            UserNode = userNode;

            _transactionFactory = transactionFactory;
            _nextBlockValidator = nextBlockValidator;
            _messageBus = messageBus;
            _lastTransaction = null;
            _lastCommitedTransaction = null;

            _index = NodeIndex.Empty;
            messageBus.Subscribe(UserNode, NewMessage);
        }

        public IUser User { get; }
        public INode UserNode { get; }

        private void Log(string format, params object[] args)
        {
            var message = string.Format("[client {0}]\t", User) + string.Format(format, args);
            Trace.TraceInformation(message);
        }

        public UserIndex GetMyInfo()
        {
            if (_index.IsEmpty)
            {
                return null;
            }

            UserIndex index;
            if (!_index.Users.AllUsers.TryGetValue(User, out index))
            {
                //blockhain does not contain yet info about me
                return null;
            }

            return index;
        }

        private void SendMessage(UserMessage userMessage)
        {
            if (_lastTransaction != _lastCommitedTransaction)
            {
                throw new InvalidOperationException("Can't send new transaction before previos was not confirmed");
            }

            Log("Sending new {0}", userMessage);
            var transaction = _transactionFactory.Create(User, _lastTransaction?.Signature ?? HashValue.Empty, userMessage);
            var message = new NewTransactionMessage(UserNode, transaction);
            _messageBus.Push(message);
        }

        public void NewOrder(IOrder order)
        {
            SendMessage(new OrderRequested(order));
        }

        public void AssignOrdersToMe(double fee, params IUserOrder[] orders)
        {
            if (fee < 0)
            {
                throw new ArgumentException("fee");
            }

            if (orders == null || orders.Length == 0)
            {
                throw new ArgumentException("orders");
            }

            if (orders.Any(o => o.IsAssigned))
            {
                throw new ArgumentException("orders contain already assigned order.");
            }

            var assigedOrder = new OrdersAssigned(fee, orders.Select(o => o.Signature).ToImmutableList());
            SendMessage(assigedOrder);
        }

        private void NewMessage(IMessage message)
        {
            if (!(message is NewBlockMessage))
            {
                return;
            }

            var newBlock = ((NewBlockMessage)message).Block;
            AddNewBlock(newBlock);
        }

        private void AddNewBlock(Block newBlock)
        {
            try
            {
                _nextBlockValidator.Validate(_index.Blocks, newBlock);
                _index = _index.Add(newBlock);
            }
            catch(BlockDeclinedException ex)
            {
                Log("Block {0} was declieds: {1}", newBlock, ex.Message);
                return;
            }
            catch(Exception ex)
            {
                Log("Block {0} was ignored: {1}", newBlock, ex);
                return;
            }

            Log("Received new block {0}", newBlock);

            UserIndex userIndex;
            if (_index.Users.AllUsers.TryGetValue(User, out userIndex))
            {
                _lastCommitedTransaction = userIndex.LastTransaction;
            }
        }

        public IImmutableDictionary<HashValue, IUserOrder> GetUnassignedOrders()
        {
            if (_index.IsEmpty)
            {
                return ImmutableDictionary.Create<HashValue, IUserOrder>();
            }

            return _index.Orders.UnassignedOrders;
        }
    }
}
