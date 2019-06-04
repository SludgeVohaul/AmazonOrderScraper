namespace UseCases.OrderDetails.Responses.RequestHandlers
{
    public abstract class HandlerResponseDto
    {
        public bool     IsSuccess { get; }
        public ErrorDto Error     { get; }

        protected HandlerResponseDto()
        {
            IsSuccess = true;
        }

        protected HandlerResponseDto(ErrorDto error)
        {
            IsSuccess = false;
            Error     = error;
        }
    }
}