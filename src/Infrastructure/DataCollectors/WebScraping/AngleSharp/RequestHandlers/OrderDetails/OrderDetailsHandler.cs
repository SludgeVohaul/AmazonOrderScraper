using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Infrastructure.DataCollectors.WebScraping.AngleSharp.WebClient;
using UseCases.OrderDetails.RequestHandlers.OrderDetails;
using UseCases.OrderDetails.Responses;
using UseCases.OrderDetails.Responses.RequestHandlers.OrderDetails;

namespace Infrastructure.DataCollectors.WebScraping.AngleSharp.RequestHandlers.OrderDetails
{
    public class OrderDetailsHandler : IOrderDetailsHandler
    {
        private readonly IClientProvider<AngleSharpClient> _clientProvider;

        public OrderDetailsHandler(IClientProvider<AngleSharpClient> clientProvider)
        {
            _clientProvider = clientProvider;
        }

        public async Task<OrderDetailsHandlerResponseDto> RunAsync(Guid clientGuid, string orderNumber)
        {
            var client = _clientProvider.GetClient(clientGuid);

            // TODO - hier "orderId" statt "orderID" schreiben, um eine Amazon-Seite durch den Anonymizer zu jagen....
            var uri = new UriBuilder(AmazonConstants.OrderDetailsPageUrl) {Query = $"orderID={orderNumber}"};
            await client.Context.OpenAsync(new Url(uri.ToString()));

            if (client.Context.Active.Title != AmazonConstants.OrderDetailsPageTitle)
            {
                return new OrderDetailsHandlerResponseDto(new ErrorDto("Failed to open the order details page.", client.Context.Active.Source.Text));
            }

            var orderItems = new List<ItemDto>();
            // The order details page has infos about each ordered item.
            foreach (var orderItemElement in client.Context.Active.QuerySelectorAll<IHtmlDivElement>("div[class='a-fixed-left-grid-col a-col-right']")
            )
            {
                // Each ordered item must have 6 property elements.
                var propertyElements = orderItemElement.QuerySelectorAll<IHtmlDivElement>("div[class=a-row]").ToArray();
                if (propertyElements.Length != 6)
                {
                    return new OrderDetailsHandlerResponseDto(new ErrorDto("Failed to parse the order details page.", client.Context.Active.Source.Text));
                }

                var itemDescription = propertyElements[0].Text().Trim();
                var itemPrice       = propertyElements[3].Text().Trim();
                orderItems.Add(new ItemDto(itemDescription, itemPrice));
            }

            return new OrderDetailsHandlerResponseDto(orderItems);
        }
    }
}