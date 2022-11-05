using eCommerce.Domain.Customers;

namespace eCommerce.Domain.Newsletter;

public interface INewsletterSubscriber
{
    void Subscribe(Customer customer);
}
