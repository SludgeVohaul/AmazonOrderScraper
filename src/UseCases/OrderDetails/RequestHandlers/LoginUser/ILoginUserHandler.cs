using System;
using System.Threading.Tasks;
using UseCases.OrderDetails.Responses.RequestHandlers.LoginUser;

namespace UseCases.OrderDetails.RequestHandlers.LoginUser
{
    public interface ILoginUserHandler
    {
        Task<LoginUserHandlerResponseDto> RunAsync(Guid clientGuid, string username, string password);
    }
}