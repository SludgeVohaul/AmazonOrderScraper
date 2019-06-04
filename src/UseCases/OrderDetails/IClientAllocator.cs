using System;

namespace UseCases.OrderDetails
{
    public interface IClientAllocator
    {
        Guid AllocateClient();
    }
}