using System;
using InterfaceAdapters.Processors.Transactions;
using UseCases;

namespace Factories
{
    public class InteractorFactory : BaseFactory, IInteractorFactory
    {
        public InteractorFactory(IServiceProvider provider) : base(provider)
        {
        }

        public TInteractor CreateInteractor<TInteractor>() where TInteractor : IInteractor
        {
            return Get<TInteractor>();
        }
    }
}