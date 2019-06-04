using System.Threading.Tasks;

namespace UseCases.OrderDetails
{
    public interface IOrderDetailsInteractor : IInteractor
    {
        Task<bool> RunAsync(string username, string password, OrderDto[] orders);
    }
}