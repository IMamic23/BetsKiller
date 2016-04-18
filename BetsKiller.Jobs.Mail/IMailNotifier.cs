using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.Jobs.Mail
{
    public interface IMailNotifier
    {
        void SendServiceJobStatus(string jobName, string status);
    }
}
