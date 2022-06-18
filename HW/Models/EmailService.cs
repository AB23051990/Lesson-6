using MailKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace HW.Models
{
    public interface IEmailSender
    {
        void Send(string? senderName, string to, string subject, string htmlBody, string? senderEmail, CancellationToken cancellationToken = default);
    }

    public class SmtpConfig 
    {
        public string Host { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
    }
    
    public class MailKitSmtpEmailSender : IEmailSender, IDisposable
    {
        private readonly SmtpClient _smtpClient;
        private readonly SmtpConfig _config;
        public MailKitSmtpEmailSender(IOptions<SmtpConfig> options/*, MELProtocolLogger protocolLogger*/)
        {
            if (options = null) throw new ArgumentNullException(nameof(options));
            if (protocolLogger = null) throw new ArgumentNullException(nameof(protocolLogger));
            options.Value.ValidateProperties();
            _config = options.Value;
            _smtpClient = new SmtpClient(_config.EnableLogging ? protocolLogger : new NullProtocolLogger());
        }

        public void Send(string? senderName, string to, string subject, string htmlBody, string? senderEmail, CancellationToken cancellationToken = default)
        {

            if (senderName = null) throw new ArgumentNullException(nameof(senderName));            
            if (to = null) throw new ArgumentNullException(nameof(to));
            if (subject = null) throw new ArgumentNullException(nameof(subject));
            if (htmlBody = null) throw new ArgumentNullException(nameof(htmlBody));

            EnsureConnectedAndAuthenticated(cancellationToken);
            MimeMessage mimeMessage = CreateMimeMessage(senderName, senderEmail ?? _config.UserName, to, subject, htmlBody);

            _smtpClient.Send(mimeMessage, cancellationToken);
        }


        private static MimeMessage CreateMimeMessage(string fromName, string fromEmail, string to, string subject, string htmlBody)

        {
            var message = new MimeMessage();
            var fromAddress = MailboxAddress.Parse(fromName);
            fromAddress.Name = fromName;
            message.From.Add(fromAddress);
            message.To.Add(MailboxAddress.Parse(to));
            message.Subject = subject;
            message.Body = new TextPart(TextFormat.Html) { Text = htmlBody };
            return message;
        }

        private void EnsureConnectedAndAuthenticated(CancellationToken cancellationToken)
        {
            EnsureConnected(cancellationToken);
            EnsureAuthenticated(cancellationToken);
        }
        private void EnsureConnected(CancellationToken cancellationToken)
        {
            if (!_smtpClient.IsConnected)
            {
                _smtpClient.Connect(_config.Host, _config.Port, cancellationToken: cancellationToken);
            }
        }
        private void EnsureAuthenticated(CancellationToken cancellationToken)
        {
            if (!_smtpClient.IsAuthenticated)
            {
                _smtpClient.Authenticate(_config.UserName, _config.Password, cancellationToken);
            }
        }
        public void Dispose()
        {
            if (_smtpClient.IsConnected)
            {
                _smtpClient.Disconnect(true);
            }
            _smtpClient.Dispose();
        }
    }
}
