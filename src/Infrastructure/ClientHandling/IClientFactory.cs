namespace Infrastructure.ClientHandling
{
    public interface IClientFactory<out TClient> where TClient : class, IClient
    {
        TClient CreateClient();
    }
}