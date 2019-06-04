using System;
using UseCases;
using UseCases.OrderDetails.Responses.Interactor.Completed;
using UseCases.OrderDetails.Responses.Interactor.Error;
using UseCases.OrderDetails.Responses.Interactor.LoginUser;
using UseCases.OrderDetails.Responses.Interactor.LogoutUser;
using UseCases.OrderDetails.Responses.Interactor.OrderDetails;


namespace InterfaceAdapters.Processors.Responses
{
    public class ResponseProcessor : IOrderDetailsInteractorObserver
    {
        public void OnError(ErrorInteractorResponseDto response)
        {
            Console.WriteLine($"{response.Username} failed: {response.Error.ErrorMessage}");
        }

        public void OnCompleted(CompletedInteractorResponseDto response)
        {
            Console.WriteLine($"{response.Username} completed.");
        }

        public void OnNext(OrderDetailsInteractorResponseDto response)
        {
            Console.WriteLine($"Order '{response.Order.Number}' (TID '{response.Order.Id}') has the following items:");
            foreach (var item in response.Items)
            {
                Console.WriteLine($"Description: {item.Description}");
                Console.WriteLine($"Price      : {item.Price}");
            }
        }

        public void OnNext(LoginUserInteractorResponseDto response)
        {
            Console.WriteLine($"{response.Username} logged in.");
        }

        public void OnNext(LogoutUserInteractorResponseDto response)
        {
            Console.WriteLine($"{response.Username} logged out.");
        }
    }
}