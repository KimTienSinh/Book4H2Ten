using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book4H2Ten.Services.Emails
{
    public interface IEmailService
    {
        void Send(string to, string subject, string html, string from = null);
    }
}
