using System;
using System.Threading.Tasks;
using UseCases.OrderDetails.RequestHandlers.LoginUser;
using UseCases.OrderDetails.RequestHandlers.LogoutUser;
using UseCases.OrderDetails.RequestHandlers.OrderDetails;
using UseCases.OrderDetails.Responses;
using UseCases.OrderDetails.Responses.Interactor.Completed;
using UseCases.OrderDetails.Responses.Interactor.Error;
using UseCases.OrderDetails.Responses.Interactor.LoginUser;
using UseCases.OrderDetails.Responses.Interactor.LogoutUser;
using UseCases.OrderDetails.Responses.Interactor.OrderDetails;

namespace UseCases.OrderDetails
{
    public class OrderDetailsInteractor : IOrderDetailsInteractor
    {
        private readonly IDocumentAnonymizer             _documentAnonymizer;
        private readonly ILoginUserHandler               _loginUserHandler;
        private readonly ILogoutUserHandler              _logoutUserHandler;
        private readonly IOrderDetailsHandler            _orderDetailsHandler;
        private readonly IOrderDetailsInteractorObserver _observer;

        private readonly Guid _clientGuid;

        public OrderDetailsInteractor(IClientAllocator clientAllocator,
                                      IDocumentAnonymizer documentAnonymizer,
                                      ILoginUserHandler loginUserHandler,
                                      ILogoutUserHandler logoutUserHandler,
                                      IOrderDetailsHandler orderDetailsHandler,
                                      IOrderDetailsInteractorObserver observer)
        {
            try
            {
                _clientGuid = clientAllocator.AllocateClient();
            }
            catch (InvalidOperationException e)
            {
                // TODO - report error
                Console.WriteLine(e);

                _clientGuid = default(Guid);
            }

            _documentAnonymizer  = documentAnonymizer;
            _loginUserHandler    = loginUserHandler;
            _logoutUserHandler   = logoutUserHandler;
            _orderDetailsHandler = orderDetailsHandler;
            _observer            = observer;
        }

        public async Task<bool> RunAsync(string username, string password, OrderDto[] orders)
        {
            if (_clientGuid == default(Guid))
            {
                _observer.OnError(new ErrorInteractorResponseDto(username, new ErrorDto("A client could not be allocated!")));
                return false;
            }

            var loginResponse = await _loginUserHandler.RunAsync(_clientGuid, username, password);
            if (loginResponse.IsSuccess == false)
            {
                _observer.OnError(new ErrorInteractorResponseDto(username, loginResponse.Error));
                return false;
            }

            _observer.OnNext(new LoginUserInteractorResponseDto(username));

            var isSuccess = true;
            foreach (var order in orders)
            {
                var orderDetailsResponse = await _orderDetailsHandler.RunAsync(_clientGuid, order.Number);
                if (orderDetailsResponse.IsSuccess)
                {
                    _observer.OnNext(new OrderDetailsInteractorResponseDto(username, order, orderDetailsResponse.Items));
                    continue;
                }

                _observer.OnError(new ErrorInteractorResponseDto(username, orderDetailsResponse.Error));
                isSuccess = false;
            }

            var logoutResponse = await _logoutUserHandler.RunAsync(_clientGuid);
            if (logoutResponse.IsSuccess == false)
            {
                _observer.OnError(new ErrorInteractorResponseDto(username, logoutResponse.Error));
                return false;
            }

            _observer.OnNext(new LogoutUserInteractorResponseDto(username));

            _observer.OnCompleted(new CompletedInteractorResponseDto(username));
            return isSuccess;
        }
    }
}