namespace UseCases.OrderDetails.Responses.Interactor
{
    public abstract class InteractorResponseDto
    {
        public string Username { get; }

        public InteractorResponseDto(string username)
        {
            Username = username;
        }

    }
}