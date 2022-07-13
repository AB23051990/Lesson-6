using MailKit;
using System.Text;

namespace HW.Models.MailKit
{
    public class MELProtocolLogger : IProtocolLogger
    {
        private readonly ILogger _logger;        
        public MELProtocolLogger(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger("SmtpMimeKit");
        }

        public void LogConnect(Uri uri)
        {
            if (uri == null)
                throw new ArgumentNullException(nameof(uri));

            _logger.LogDebug("Connected to {Uri}", uri);
        }

        public void LogClient(byte[] buffer, int offset, int count)
        {
            var message = Encoding.UTF8
                .GetString(buffer)
                .TrimEnd('\0')
                .Replace(Environment.NewLine, "; ");

            _logger.LogTrace("Client: {Message}", message);
        }

        public void LogServer(byte[] buffer, int offset, int count)
        {
            var message = Encoding.UTF8
                .GetString(buffer)
                .TrimEnd('\0')
                .Replace(Environment.NewLine, "; ");

            _logger.LogTrace("Server: {Message}", message);
        }

        public IAuthenticationSecretDetector? AuthenticationSecretDetector { get; set; }

        public void Dispose()
        {
            _logger.LogTrace("Logger disposed");
        }
    }
}
