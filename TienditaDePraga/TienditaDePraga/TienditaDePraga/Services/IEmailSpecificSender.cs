using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TienditaDePraga
{
    public interface IEmailSpecificSender
    {
        Task<bool> SendEmailWithAttachment(string to, string subject, string body, string attachmentPath);
    }
}
