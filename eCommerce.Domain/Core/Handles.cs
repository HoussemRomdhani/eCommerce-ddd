namespace eCommerce.Domain.Core;

public interface Handles<T>
   where T : DomainEvent
{
    void Handle(T args);
}