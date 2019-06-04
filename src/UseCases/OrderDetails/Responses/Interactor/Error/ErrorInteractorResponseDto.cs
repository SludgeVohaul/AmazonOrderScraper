namespace UseCases.OrderDetails.Responses.Interactor.Error
{
    public class ErrorInteractorResponseDto : InteractorResponseDto
    {
        public ErrorDto Error { get; }

        public ErrorInteractorResponseDto(string username, ErrorDto error) : base(username)
        {
            Error = error;
        }
    }
}