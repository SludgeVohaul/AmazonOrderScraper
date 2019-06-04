using System;
using Infrastructure.ClientHandling;

namespace Factories
{
    public class ClientFactory<TClient> : BaseFactory, IClientFactory<TClient> where TClient : class, IClient
    {

        public ClientFactory(IServiceProvider provider) : base(provider)
        {
        }

        public TClient CreateClient()
        {
            return Get<TClient>();
        }
    }
}