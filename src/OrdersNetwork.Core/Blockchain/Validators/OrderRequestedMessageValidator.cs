using OrdersNetwork.Core.Blockchain.Index;
using OrdersNetwork.Core.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core.Blockchain.Validators
{
    public class OrderRequestedMessageValidator : INewUserMessageValidator<OrderRequested>
    {
        private readonly Dictionary<string, INewOrderValidator> _validators;

        public OrderRequestedMessageValidator(IEnumerable<INewOrderValidator> validators)
        {
            _validators = validators.ToDictionary(v => v.OrderType);
        }

        public NewTransactionIncludeResult Validate(NodeIndex nodeIndex, UserIndex userIndex, OngoingBlock ongoingBlock, OrderRequested message)
        {
            var order = message.Order;

            if (order.Payment <= 0)
            {
                throw new TransactionDeclinedException("Invalid order Payment.");
            }

            var validator = _validators[order.OrderType];
            validator.Validate(nodeIndex, userIndex, order);

            return NewTransactionIncludeResult.Include;
        }
    }
}
