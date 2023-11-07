
using System.Net;
using System.Net.Mail;

namespace API.Helpers.Utilities
{
    public interface IMailUtility
    {
        void SendMail(string toMail, string subject, string content, bool isHtml = false, string filePath = "");
        Task SendMailAsync(string toMail, string subject, string content, bool isHtml = false, string filePath = "");
        Task SendListMailAsync(List<string> toMail, string subject, string content, bool isHtml = false, string ccMail = "", string filePath = "");
    }
    
    public class MailUtility : IMailUtility
    {
        private readonly IConfiguration _configuration;
        protected MailSettingServer _mailSettings;
        public MailUtility(IConfiguration configuration)
        {
            _configuration = configuration;
            _mailSettings = _configuration.GetSection("MailSettingServer").Get<MailSettingServer>();
        }

        public void SendMail(string toMail, string subject, string content, bool isHtml = false, string filePath = "")
        {
            MailMessage mail = GetMailMessage(subject, content, isHtml, filePath);
            mail.To.Add(toMail);

            SmtpClient smtpServer = SettingSmtpClient();
            smtpServer.Send(mail);
        }

        public async Task SendMailAsync(string toMail, string subject, string content, bool isHtml = false, string filePath = "")
        {
            MailMessage mail = GetMailMessage(subject, content, isHtml, filePath);
            mail.To.Add(toMail);

            SmtpClient smtpServer = SettingSmtpClient();
            await smtpServer.SendMailAsync(mail);
        }

        public async Task SendListMailAsync(List<string> toMail, string subject, string content, bool isHtml = false, string ccMail = "", string filePath = "")
        {
            MailMessage mail = GetMailMessage(subject, content, isHtml, filePath);
            foreach (var item in toMail)
            {
                mail.To.Add(item);
            }
            if (!string.IsNullOrWhiteSpace(ccMail))
                mail.CC.Add(ccMail);

            SmtpClient smtpServer = SettingSmtpClient();
            await smtpServer.SendMailAsync(mail);
        }

        private MailMessage GetMailMessage(string subject, string content, bool isHtml, string filePath = "")
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(_mailSettings.FromEmail, _mailSettings.FromName);
            mail.Subject = subject;
            mail.Body = content;
            if (isHtml)
            {
                mail.IsBodyHtml = true;
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.Priority = MailPriority.High;
            }

            if (!string.IsNullOrWhiteSpace(filePath))
            {
                Attachment attachment;
                attachment = new Attachment(filePath);
                mail.Attachments.Add(attachment);
            }
            return mail;
        }

        private SmtpClient SettingSmtpClient()
        {
            SmtpClient smtpServer = new SmtpClient(_mailSettings.Server);
            smtpServer.Port = Convert.ToInt32(_mailSettings.Port);
            smtpServer.Credentials = new NetworkCredential(_mailSettings.UserName, _mailSettings.Password);
            smtpServer.EnableSsl = Convert.ToBoolean(_mailSettings.EnableSsl);
            return smtpServer;
        }
    }
}