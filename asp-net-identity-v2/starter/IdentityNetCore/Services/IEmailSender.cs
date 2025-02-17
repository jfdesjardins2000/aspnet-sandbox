using System.Threading.Tasks;

namespace IdentityNetCore.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string fromAddress, string toAddress, string subject, string message);
    }
}
