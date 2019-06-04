using UseCases.OrderDetails.Responses.RequestHandlers.OrderDetails;

namespace UseCases.OrderDetails.Responses.Interactor.OrderDetails
{
    public class OrderDetailsInteractorResponseDto : InteractorResponseDto
    {
        public OrderDto Order { get; }
        public ItemDto[] Items { get; }

        public OrderDetailsInteractorResponseDto(string username, OrderDto order, ItemDto[] items) : base(username)
        {
            Order = order;
            Items = items;
        }
    }
}