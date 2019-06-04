namespace UseCases.OrderDetails.Responses.RequestHandlers.LogoutUser
{
    public class LogoutUserHandlerResponseDto : HandlerResponseDto
    {
        public LogoutUserHandlerResponseDto()
        {
        }

        public LogoutUserHandlerResponseDto(ErrorDto error) : base(error)
        {
        }
    }
}