namespace UseCases.OrderDetails.Responses
{
    public class ErrorDto
    {
        public string ErrorMessage { get; }
        public string PageSource   { get; }

        public ErrorDto(string errorMessage, string pageSource = null)
        {
            ErrorMessage = errorMessage;
            PageSource   = pageSource;
        }
    }
}