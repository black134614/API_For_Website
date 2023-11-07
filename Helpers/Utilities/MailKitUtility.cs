using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace API.Helpers.Utilities
{
    public interface IMailKitUtility
    {
        Task SendMailAsync(string toMail, string subject, string content);
    }

    public class MailKitUtility : IMailKitUtility
    {
        private readonly IConfiguration _configuration;

        public MailKitUtility(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendMailAsync(string toMail, string subject, string content)
        {
            var mailSetting = _configuration.GetSection("MailSettingServer").Get<MailSettingServer>();
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(mailSetting.FromEmail));
            email.To.Add(MailboxAddress.Parse(toMail));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = content };

            // send email
            var smtp = new SmtpClient();
            smtp.Timeout = 36000;
            await smtp.ConnectAsync(mailSetting.Server, Convert.ToInt32(mailSetting.Port), SecureSocketOptions.Auto);
            await smtp.AuthenticateAsync(mailSetting.UserName, mailSetting.Password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}