using System.Net.Mail;

namespace eCommerce.Domain.Email;

public interface IEmailDispatcher
{
    void Dispatch(MailMessage mailMessage);
}
