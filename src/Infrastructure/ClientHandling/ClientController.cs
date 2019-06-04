using System;
using System.Collections.Concurrent;
using Infrastructure.DataCollectors;
using UseCases.OrderDetails;

namespace Infrastructure.ClientHandling
{
    public class ClientController<TClient> : IClientProvider<TClient>, IClientAllocator where TClient : class, IClient
    {
        private readonly IClientFactory<TClient>             _clientFactory;
        private readonly ConcurrentDictionary<Guid, TClient> _clients = new ConcurrentDictionary<Guid, TClient>();

        public ClientController(IClientFactory<TClient> clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public TClient GetClient(Guid clientGuid)
        {
            if (_clients.TryGetValue(clientGuid, out var client) == false)
            {
                // TODO MN - handle error
                throw new Exception("Invalid Guid!");
            }

            return client;
        }

        public Guid AllocateClient()
        {
            var guid = Guid.NewGuid();

            // TODO MN - handle null
            _clients[guid] = _clientFactory.CreateClient();

            return guid;
        }
    }
}