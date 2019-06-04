using System;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Infrastructure.DataCollectors.WebScraping.AngleSharp.WebClient;
using UseCases.OrderDetails.RequestHandlers.LogoutUser;
using UseCases.OrderDetails.Responses;
using UseCases.OrderDetails.Responses.RequestHandlers.LogoutUser;

namespace Infrastructure.DataCollectors.WebScraping.AngleSharp.RequestHandlers.LogoutUser
{
    public class LogoutUserHandler : ILogoutUserHandler
    {
        private readonly IClientProvider<AngleSharpClient> _clientProvider;

        public LogoutUserHandler(IClientProvider<AngleSharpClient> clientProvider)
        {
            _clientProvider = clientProvider;
        }

        public async Task<LogoutUserHandlerResponseDto> RunAsync(Guid clientGuid)
        {
            var client = _clientProvider.GetClient(clientGuid);

            // FYI: This would be the preferred way of fetching the logout link, if AngleSharp was able to run the JS on the page...
            //      var logoutLink = _client.Context.Active.QuerySelector<IHtmlAnchorElement>("a[id=nav-item-signout]");
            var logoutLink = client.Context.Active.QuerySelector<IHtmlAnchorElement>(AmazonConstants.LogoutHrefSelector);

            if (logoutLink == null)
            {
                return new LogoutUserHandlerResponseDto(new ErrorDto("Failed to locate the logout link.", client.Context.Active.Source.Text));
            }

            await logoutLink.NavigateAsync();

            return client.Context.Active.Title == AmazonConstants.LoginPageTitle
                       ? new LogoutUserHandlerResponseDto()
                       : new LogoutUserHandlerResponseDto(new ErrorDto("Logout failed.", client.Context.Active.Source.Text));
        }
    }
}