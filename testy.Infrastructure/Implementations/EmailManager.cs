using System.Threading.Tasks;
using testy.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using testy.Infrastructure.Attributes;
using System.Net.Mail;
using Microsoft.Extensions.Logging;
using System.IO;

namespace lbp.Infraestructure.Implementations
{
    [ScopedService]
    public class EmailManager : IEmailManager
    {
        public readonly IConfiguration _config;
        public readonly ILogger<EmailManager> _logger;
        public SmtpClient client;

        public EmailManager(IConfiguration config, ILogger<EmailManager> logger)
        {
            _config = config;
            _logger = logger;
            var mailingSection = _config.GetSection("Mailing");
            var username = mailingSection.GetSection("Username").Value;
            var pass = mailingSection.GetSection("Password").Value;

            var host = mailingSection.GetSection("Host").Value;
            var port = System.Int32.Parse(mailingSection.GetSection("Port").Value);
            client = new SmtpClient(host, port);
            
            // client.UseDefaultCredentials = true;
            client.Credentials = new System.Net.NetworkCredential(username, pass);
        }

        public async Task<bool> SendEmail(string Email,
                                          string EmailSubject,
                                          string EmailBody,
                                          string AttachmentPath = "",
                                          byte[] AttachmentBytes = null,
                                          bool HtmlBody = true)
        {
            try{
                var from = _config.GetSection("Mailing").GetSection("DefaultFrom").Value;

                MailMessage message = new MailMessage();
                message.From = new MailAddress(from);
                message.To.Add(new MailAddress(Email));
                message.Subject = EmailSubject;
                message.Body = EmailBody;
                message.IsBodyHtml = HtmlBody;

                if (AttachmentPath != "")
                {
                    // var _array = EmailBody.Split(' ');
                    // var _id = _array[_array.Length - 1];
                    // string pdfPot = _config.GetSection("PDFPot").Value;
                    // string fullAddress = pdfPot + "\\GR_" + _id + ".pdf";
                    Attachment file;
                    file = new Attachment(AttachmentPath);
                    message.Attachments.Add(file);
                }

                if (AttachmentBytes != null)
                {
                    Attachment file;
                    file = new Attachment(new MemoryStream(AttachmentBytes), "qrcode.png", "image/png");
                    message.Attachments.Add(file);
                }
                await client.SendMailAsync(message);
                return true;
            }
            catch (System.Exception ex)
            {
                _logger.LogInformation(ex.ToString());
                return false;
            }
        }
    }
}