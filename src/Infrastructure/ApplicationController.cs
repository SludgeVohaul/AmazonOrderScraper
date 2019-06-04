using System.Threading.Tasks;
using InterfaceAdapters.Processors.Transactions;

namespace Infrastructure
{
    public class ApplicationController
    {
        private readonly ITransactionProcessor _transactionProcessor;

        public ApplicationController(ITransactionProcessor transactionProcessor)
        {
            _transactionProcessor = transactionProcessor;
        }
        public async Task<bool> RunAsync()
        {
           await _transactionProcessor.ProcessAsync(new [] {new TransactionDto("abc", "id_martin", "302-7854018-4046743")});


            return true;


        }
    }
}