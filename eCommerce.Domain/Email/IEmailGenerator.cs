using System.Net.Mail;

namespace eCommerce.Domain.Email;

public interface IEmailGenerator
{
    MailMessage Generate(object objHolder, EmailTemplate emailTemplate);
}
