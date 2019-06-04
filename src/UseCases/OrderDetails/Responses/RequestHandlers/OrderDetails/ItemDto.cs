namespace UseCases.OrderDetails.Responses.RequestHandlers.OrderDetails
{
    public class ItemDto
    {
        public string Description { get; }
        public string Price       { get; }

        public ItemDto(string description, string price)
        {
            Description = description;
            Price       = price;
        }

    }
}