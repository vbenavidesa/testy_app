using System.Threading.Tasks;

namespace testy.Application.Common.Interfaces
{
    public interface IEmailManager {
        Task<bool> SendEmail(
            string Email,
            string EmailSubject,
            string EmailBody,
            string AttachmentPath = "",
            byte[] AttachmentBytes = null,
            bool HtmlBody = true);
    }
}
