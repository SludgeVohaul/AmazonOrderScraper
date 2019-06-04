using System;
using Infrastructure.ClientHandling;

namespace Infrastructure.DataCollectors
{
    public interface IClientProvider<out TClient> where TClient : class, IClient
    {
        TClient GetClient(Guid clientGuid);
    }
}