using System.Collections.Generic;
using System.Linq;

namespace UseCases.OrderDetails.Responses.RequestHandlers.OrderDetails
{
    public class OrderDetailsHandlerResponseDto : HandlerResponseDto
    {
        public ItemDto[] Items  { get; } = { };

        public OrderDetailsHandlerResponseDto(IEnumerable<ItemDto> orderItems)
        {
            Items  = orderItems.ToArray();
        }

        public OrderDetailsHandlerResponseDto(ErrorDto error) : base(error)
        {
        }
    }
}