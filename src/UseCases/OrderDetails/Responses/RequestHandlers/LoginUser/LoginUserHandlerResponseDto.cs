namespace UseCases.OrderDetails.Responses.RequestHandlers.LoginUser
{
    public class LoginUserHandlerResponseDto : HandlerResponseDto
    {
        public LoginUserHandlerResponseDto()
        {
        }

        public LoginUserHandlerResponseDto(ErrorDto error) : base(error)
        {
        }
    }
}