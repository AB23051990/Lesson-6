using MailKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace HW.Models.MailKit
{
    public class ConnectionException : Exception
    {
        public ConnectionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
