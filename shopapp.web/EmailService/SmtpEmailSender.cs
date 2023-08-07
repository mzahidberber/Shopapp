using System.Net;
using System.Net.Mail;

namespace shopapp.web.EmailService
{
    public class SmtpEmailSender : IEmailSender
    {
        private string _host { get; set; }
        private int _port { get; set; }
        private bool _enabledSSL { get; set; }
        private string _username { get; set; }
        private string _password { get; set; }
        public SmtpEmailSender(string host, int port, bool enabledSSL, string username, string password)
        {
            _host = host;
            _port = port;
            _enabledSSL = enabledSSL;
            _username = username;
            _password = password;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SmtpClient(this._host, this._port)
            {
                Credentials = new NetworkCredential(this._username, this._password),
                EnableSsl = this._enabledSSL


            };

            return client.SendMailAsync(new MailMessage(this._username, email, subject, htmlMessage)
            {
                IsBodyHtml = true
            });
        }
    }
}
