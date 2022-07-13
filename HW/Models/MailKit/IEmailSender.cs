using MailKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace HW.Models.MailKit
{
    public interface IEmailSender
    {
        /// <exception cref="ConnectionException">This method has to throw this exception due to connection errors</exception>
        void Send(string senderName,
            string to,
            string subject,
            string htmlBody,
            string? senderEmail = null,
            CancellationToken cancellationToken = default
        );

        /// <exception cref="ConnectionException">This method has to throw this exception due to connection errors</exception>
        Task SendAsync(string senderName,
            string to,
            string subject,
            string htmlBody,
            string? senderEmail = null,
            CancellationToken cancellationToken = default
        );
    }
}
