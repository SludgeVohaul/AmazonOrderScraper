namespace UseCases.OrderDetails
{
    public class OrderDto
    {
        public string Number { get; }
        public string Id     { get; }

        public OrderDto(string orderId, string orderNumber)
        {
            Id     = orderId;
            Number = orderNumber;
        }
    }
}