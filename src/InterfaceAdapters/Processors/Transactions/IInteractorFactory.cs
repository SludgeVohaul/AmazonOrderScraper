using UseCases;

namespace InterfaceAdapters.Processors.Transactions
{
    public interface IInteractorFactory
    {
        TInteractor CreateInteractor<TInteractor>() where TInteractor : IInteractor;
    }
}