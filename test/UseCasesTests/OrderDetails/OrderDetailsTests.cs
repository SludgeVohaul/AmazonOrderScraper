using System;
using System.Threading.Tasks;
using Infrastructure.Anonymizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UseCases;
using UseCases.OrderDetails;
using UseCases.OrderDetails.RequestHandlers.LoginUser;
using UseCases.OrderDetails.RequestHandlers.LogoutUser;
using UseCases.OrderDetails.RequestHandlers.OrderDetails;
using UseCases.OrderDetails.Responses;
using UseCases.OrderDetails.Responses.Interactor.Completed;
using UseCases.OrderDetails.Responses.Interactor.Error;
using UseCases.OrderDetails.Responses.Interactor.LoginUser;
using UseCases.OrderDetails.Responses.Interactor.LogoutUser;
using UseCases.OrderDetails.Responses.Interactor.OrderDetails;
using UseCases.OrderDetails.Responses.RequestHandlers.LoginUser;
using UseCases.OrderDetails.Responses.RequestHandlers.LogoutUser;
using UseCases.OrderDetails.Responses.RequestHandlers.OrderDetails;

namespace UseCasesTests.OrderDetails
{
    [TestClass]
    public class OrderDetailsTests
    {
        [TestMethod]
        public async Task OrderDetails_ClientAllocationFails()
        {
            const bool expected = false;
            var orderNumbers = new[]
            {
                new OrderDto("1234", "abc123"),
                new OrderDto("1234", "bcd345")
            };

            var sut = new OrderDetailsInteractor(new ClientAllocatorMock(default(Guid)),
                                                 new DocumentAnonymizer(),
                                                 new LoginUserHandlerMock(true),
                                                 new LogoutUserHandlerMock(true),
                                                 new OrderDetailsHandlerMock(true),
                                                 new ObserverMock());
            var actual = await sut.RunAsync("username", "password", orderNumbers);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task OrderDetails_LoginFails()
        {
            const bool expected = false;
            var orderNumbers = new[]
            {
                new OrderDto("1234", "abc123"),
                new OrderDto("1234", "bcd345")
            };

            var sut = new OrderDetailsInteractor(new ClientAllocatorMock(Guid.NewGuid()),
                                                 new DocumentAnonymizer(),
                                                 new LoginUserHandlerMock(false),
                                                 new LogoutUserHandlerMock(true),
                                                 new OrderDetailsHandlerMock(true),
                                                 new ObserverMock());
            var actual = await sut.RunAsync("username", "password", orderNumbers);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task OrderDetails_LogoutFails()
        {
            const bool expected = false;
            var orderNumbers = new[]
            {
                new OrderDto("1234", "abc123"),
                new OrderDto("1234", "bcd345")
            };

            var sut = new OrderDetailsInteractor(new ClientAllocatorMock(Guid.NewGuid()),
                                                 new DocumentAnonymizer(),
                                                 new LoginUserHandlerMock(true),
                                                 new LogoutUserHandlerMock(false),
                                                 new OrderDetailsHandlerMock(true),
                                                 new ObserverMock());
            var actual = await sut.RunAsync("username", "password", orderNumbers);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task OrderDetails_OrderDetailsFails()
        {
            const bool expected = false;
            var orderNumbers = new[]
            {
                new OrderDto("1234", "abc123"),
                new OrderDto("1234", "bcd345")
            };

            var sut = new OrderDetailsInteractor(new ClientAllocatorMock(Guid.NewGuid()),
                                                 new DocumentAnonymizer(),
                                                 new LoginUserHandlerMock(true),
                                                 new LogoutUserHandlerMock(true),
                                                 new OrderDetailsHandlerMock(false),
                                                 new ObserverMock());
            var actual = await sut.RunAsync("username", "password", orderNumbers);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task OrderDetails_AllSucceed()
        {
            const bool expected = true;
            var orderNumbers = new[]
            {
                new OrderDto("1234", "abc123"),
                new OrderDto("1234", "bcd345")
            };

            var sut = new OrderDetailsInteractor(new ClientAllocatorMock(Guid.NewGuid()),
                                                 new DocumentAnonymizer(),
                                                 new LoginUserHandlerMock(true),
                                                 new LogoutUserHandlerMock(true),
                                                 new OrderDetailsHandlerMock(true),
                                                 new ObserverMock());
            var actual = await sut.RunAsync("username", "password", orderNumbers);

            Assert.AreEqual(expected, actual);
        }

        private class LoginUserHandlerMock : ILoginUserHandler
        {
            private readonly bool _isSuccess;

            public LoginUserHandlerMock(bool isSuccess)
            {
                _isSuccess = isSuccess;
            }

            public async Task<LoginUserHandlerResponseDto> RunAsync(Guid clientGuid, string username, string password)
            {
                return await Task.Run(() => _isSuccess
                                                ? new LoginUserHandlerResponseDto()
                                                : new LoginUserHandlerResponseDto(new ErrorDto("Login failed...", "<html><body></body></html>")));
            }
        }

        private class LogoutUserHandlerMock : ILogoutUserHandler
        {
            private readonly bool _isSuccess;

            public LogoutUserHandlerMock(bool isSuccess)
            {
                _isSuccess = isSuccess;
            }


            public async Task<LogoutUserHandlerResponseDto> RunAsync(Guid clientGuid)
            {
                return await Task.Run(() => _isSuccess
                                                ? new LogoutUserHandlerResponseDto()
                                                : new LogoutUserHandlerResponseDto(new ErrorDto("Logout failed...", "<html><body></body></html>")));
            }
        }

        private class OrderDetailsHandlerMock : IOrderDetailsHandler
        {
            private readonly bool _isSuccess;

            public OrderDetailsHandlerMock(bool isSuccess)
            {
                _isSuccess = isSuccess;
            }

            public async Task<OrderDetailsHandlerResponseDto> RunAsync(Guid clientGuid, string orderNumber)
            {
                var orderItems = new[]
                {
                    new ItemDto("description 1", "16.65"),
                    new ItemDto("description 2", "0.65")
                };
                return await Task.Run(() => _isSuccess
                                                ? new OrderDetailsHandlerResponseDto(orderItems)
                                                : new OrderDetailsHandlerResponseDto(new ErrorDto("Failed to fetch order details", "<html><body></body></html>")));
            }
        }

        private class ClientAllocatorMock : IClientAllocator
        {
            private readonly Guid _guid;

            public ClientAllocatorMock(Guid guid)
            {
                _guid = guid;
            }

            public Guid AllocateClient()
            {
                return _guid;
            }
        }

        private class ObserverMock : IOrderDetailsInteractorObserver
        {
            public void OnError(ErrorInteractorResponseDto response)
            {
            }

            public void OnCompleted(CompletedInteractorResponseDto response)
            {
            }

            public void OnNext(OrderDetailsInteractorResponseDto response)
            {
            }

            public void OnNext(LoginUserInteractorResponseDto response)
            {
            }

            public void OnNext(LogoutUserInteractorResponseDto response)
            {
            }
        }
    }
}