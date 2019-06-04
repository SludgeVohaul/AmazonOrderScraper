using System.Threading.Tasks;

namespace InterfaceAdapters.Processors.Transactions
{
    public interface ICredentialsCollector
    {
        Task<(string Username, string Password)> GetUserCredentialsAsync(string userId);
    }
}