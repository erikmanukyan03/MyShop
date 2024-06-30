using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public interface IEmailService
    {
        Task Send(string to,string body, string subject);
    }
}
