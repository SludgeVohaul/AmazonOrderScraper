namespace InterfaceAdapters.Processors.Transactions
{
    public class TransactionDto
    {
        public string TransactionId { get; }
        public string UserId        { get; }
        public string OrderNumber   { get; }

        public TransactionDto(string transactionId, string userId, string orderNumber)
        {
            TransactionId = transactionId;
            UserId        = userId;
            OrderNumber   = orderNumber;
        }
    }
}