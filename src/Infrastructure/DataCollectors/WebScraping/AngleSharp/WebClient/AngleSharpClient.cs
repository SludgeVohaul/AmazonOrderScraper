using System.Net.Http;
using AngleSharp;
using AngleSharp.Io.Network;
using Infrastructure.ClientHandling;

namespace Infrastructure.DataCollectors.WebScraping.AngleSharp.WebClient
{
    public class AngleSharpClient : IClient
    {
        public IBrowsingContext Context { get; }

        public AngleSharpClient(HttpClient client)
        {
            var configuration = Configuration.Default.WithDefaultLoader().With(new HttpClientRequester(client));
            //var configuration = Configuration.Default.WithDefaultLoader().With(new FileRequester());
            Context = BrowsingContext.New(configuration);
        }
    }
}
