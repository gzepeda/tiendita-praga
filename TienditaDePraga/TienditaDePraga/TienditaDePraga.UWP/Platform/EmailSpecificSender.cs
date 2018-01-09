using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Messaging;

namespace TienditaDePraga
{
    public class EmailSpecificSender : IEmailSpecificSender
    {
        public Task<bool> SendEmailWithAttachment(string to, string subject, string body, string attachmentPath)
        {
            return Task.FromResult(true);
        }
    }
}
