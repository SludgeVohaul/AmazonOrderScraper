using System;
using System.Threading.Tasks;
using UseCases.OrderDetails.Responses.RequestHandlers.OrderDetails;

namespace UseCases.OrderDetails.RequestHandlers.OrderDetails
{
    public interface IOrderDetailsHandler
    {
        Task<OrderDetailsHandlerResponseDto> RunAsync(Guid clientGuid, string orderNumber);
    }
}