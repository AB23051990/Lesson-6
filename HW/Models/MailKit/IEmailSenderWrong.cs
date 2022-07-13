using MimeKit;
using System.Collections.Concurrent;

namespace HW.Models.MailKit
{
    public interface IEmailSender1
    {
        void Send(MimeMessage message);
    }
}
