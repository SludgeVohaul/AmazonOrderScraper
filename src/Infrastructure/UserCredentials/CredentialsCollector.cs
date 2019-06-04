using System.Threading.Tasks;
using Infrastructure.IdentityMapping;
using InterfaceAdapters.Processors.Transactions;

namespace Infrastructure.UserCredentials
{
    public class CredentialsCollector : ICredentialsCollector
    {
        private readonly IdentityMap _identityMap;

        public CredentialsCollector(IdentityMap identityMap)
        {
            _identityMap = identityMap;
        }

        public async Task<(string Username, string Password)> GetUserCredentialsAsync(string userId)
        {
            var credentials = _identityMap.GetUsernameAndPassword(userId);
            if (credentials.Username == null) return await GetUsernameAndPasswordAsync(userId);

            if (credentials.Password == null) return await GetPasswordAsync(credentials.Username);

            return credentials;
        }

        private async Task<(string Username, string Password)> GetPasswordAsync(string username)
        {
            throw new System.NotImplementedException();
        }

        private async Task<(string Username, string Password)> GetUsernameAndPasswordAsync(string userId)
        {
            return ("a@a", "1234");
        }
    }
}