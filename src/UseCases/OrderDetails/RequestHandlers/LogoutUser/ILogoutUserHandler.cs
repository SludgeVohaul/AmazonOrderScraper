using System;
using System.Threading.Tasks;
using UseCases.OrderDetails.Responses.RequestHandlers.LogoutUser;

namespace UseCases.OrderDetails.RequestHandlers.LogoutUser
{
    public interface ILogoutUserHandler
    {
        Task<LogoutUserHandlerResponseDto> RunAsync(Guid clientGuid);
    }
}