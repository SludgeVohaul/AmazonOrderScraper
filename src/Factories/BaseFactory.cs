using System;
using Microsoft.Extensions.DependencyInjection;

namespace Factories
{
    public abstract class BaseFactory
    {
        private readonly IServiceProvider _provider;

        protected BaseFactory(IServiceProvider provider)
        {
            _provider = provider;
        }

        protected TType Get<TType>()
        {
            return _provider.GetRequiredService<TType>();
        }
    }
}