using System.Threading.Tasks;
using Infrastructure.IdentityMapping;
using Infrastructure.UserCredentials;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InfrastructureTests.UserCredentials
{
    [TestClass]
    public class CredentialsCollectorTests
    {
        private static IdentityMap _identityMap;

        [ClassInitialize]
        public static void SetupTests(TestContext context)
        {
            _identityMap = new IdentityMap
            {
                IdentityMappings = new[]
                {
                    new IdentityMapping {UserId = "123", Username = "user1", Password = "passw1"},
                    new IdentityMapping {UserId = "456", Username = "user2", Password = "passw2"},
                    new IdentityMapping {UserId = "789", Username = "user3"}
                }
            };
        }

        [TestMethod]
        public async Task GetUserCredentials_Username_Password()
        {
            var expected = (Username : "user1", Password : "passw1");

            var sut = new CredentialsCollector(_identityMap);
            var actual = await sut.GetUserCredentialsAsync("123");

            Assert.AreEqual(expected.Username, actual.Username);
            Assert.AreEqual(expected.Password, actual.Password);
        }
    }
}