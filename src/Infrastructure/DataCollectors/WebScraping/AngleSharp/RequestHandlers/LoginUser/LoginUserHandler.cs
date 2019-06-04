using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Infrastructure.DataCollectors.WebScraping.AngleSharp.WebClient;
using UseCases.OrderDetails.RequestHandlers.LoginUser;
using UseCases.OrderDetails.Responses;
using UseCases.OrderDetails.Responses.RequestHandlers.LoginUser;

namespace Infrastructure.DataCollectors.WebScraping.AngleSharp.RequestHandlers.LoginUser
{
    public class LoginUserHandler : ILoginUserHandler
    {
        private readonly IClientProvider<AngleSharpClient> _clientProvider;

        public LoginUserHandler(IClientProvider<AngleSharpClient> clientProvider)
        {
            _clientProvider = clientProvider;
        }

        public async Task<LoginUserHandlerResponseDto> RunAsync(Guid clientGuid, string username, string password)
        {
            var client = _clientProvider.GetClient(clientGuid);
            await client.Context.OpenAsync(new Url(AmazonConstants.LoginPageUrl));
            var loginForm = client.Context.Active.QuerySelector<IHtmlFormElement>(AmazonConstants.LoginFormSelector);

            if (loginForm == null)
            {
                return new LoginUserHandlerResponseDto(new ErrorDto("Failed to locate the login form.", client.Context.Active.Source.Text));
            }

            var postData = new Dictionary<string, string>
            {
                {"email", username},
                {"password", password}
            };
            await loginForm.SubmitAsync(postData);

            return client.Context.Active.Title == AmazonConstants.HomePageTitle
                       ? new LoginUserHandlerResponseDto()
                       : new LoginUserHandlerResponseDto(new ErrorDto("Login failed.", client.Context.Active.Source.Text));
        }
    }
}