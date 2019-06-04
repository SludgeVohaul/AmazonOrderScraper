using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UseCases.OrderDetails;

namespace InterfaceAdapters.Processors.Transactions
{
    public class TransactionProcessor : ITransactionProcessor
    {
        private readonly IInteractorFactory    _interactorFactory;
        private readonly ICredentialsCollector _credentialsCollector;

        public TransactionProcessor(IInteractorFactory interactorFactory,
                                    ICredentialsCollector credentialsCollector)
        {
            _interactorFactory    = interactorFactory;
            _credentialsCollector = credentialsCollector;
        }

        public async Task<bool> ProcessAsync(TransactionDto[] transactions)
        {
            var userTransactionsTasks = new List<Task<bool>>();
            var transactionsByUsers   = transactions.GroupBy(x => x.UserId);
            foreach (var userTransactions in transactionsByUsers)
            {
                var (username, password) = await _credentialsCollector.GetUserCredentialsAsync(userTransactions.Key);
                var orders                 = userTransactions.Select(x => new OrderDto(x.TransactionId, x.OrderNumber)).ToArray();
                var orderDetailsInteractor = _interactorFactory.CreateInteractor<IOrderDetailsInteractor>();

                userTransactionsTasks.Add(orderDetailsInteractor.RunAsync(username, password, orders));
            }

            await Task.WhenAll(userTransactionsTasks);

            return false;
        }
    }
}