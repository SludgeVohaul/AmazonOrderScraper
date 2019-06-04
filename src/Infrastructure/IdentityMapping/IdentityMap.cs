using System.Linq;

namespace Infrastructure.IdentityMapping
{
    public class IdentityMap
    {
        public IdentityMapping[] IdentityMappings { get; set; }

        public (string Username, string Password) GetUsernameAndPassword(string userId)
        {
            var accountMapping = IdentityMappings.FirstOrDefault(x => x.UserId == userId);
            return accountMapping == null
                       ? (null, null)
                       : (accountMapping.Username, accountMapping.Password);
        }
    }

    public class IdentityMapping
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}