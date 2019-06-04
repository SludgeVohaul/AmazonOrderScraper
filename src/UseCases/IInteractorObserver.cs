using UseCases.OrderDetails.Responses.Interactor.Completed;
using UseCases.OrderDetails.Responses.Interactor.Error;
using UseCases.OrderDetails.Responses.Interactor.LoginUser;
using UseCases.OrderDetails.Responses.Interactor.LogoutUser;
using UseCases.OrderDetails.Responses.Interactor.OrderDetails;

namespace UseCases
{
    public interface IOrderDetailsInteractorObserver
    {
        void OnError(ErrorInteractorResponseDto response);
        void OnCompleted(CompletedInteractorResponseDto response);
        void OnNext(OrderDetailsInteractorResponseDto response);
        void OnNext(LoginUserInteractorResponseDto response);
        void OnNext(LogoutUserInteractorResponseDto response);
    }
}