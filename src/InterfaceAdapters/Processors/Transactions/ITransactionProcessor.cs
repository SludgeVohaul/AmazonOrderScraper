using System.Threading.Tasks;

namespace InterfaceAdapters.Processors.Transactions
{
    public interface ITransactionProcessor
    {
        Task<bool> ProcessAsync(TransactionDto[] transactions);
    }
}