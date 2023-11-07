namespace API.Helpers.Utilities
{
    public class MailSettingServer
    {
        public string Server { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FromEmail { get; set; }
        public string FromName { get; set; }
        public string Port { get; set; }
        public string EnableSsl { get; set; }
        public string DefaultCredentials { get; set; }
    }
}