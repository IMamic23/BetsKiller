using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.Jobs.Mail
{
    public class MailNotifier : MailNotifierBase, IMailNotifier
    {
        #region Consts

        private string _serviceMailAddress;
        private string _infoMailAddress;

        #endregion

        #region Constructors

        public MailNotifier()
            : base()
        {
            this._serviceMailAddress = ConfigurationManager.AppSettings["MailService"];
            this._infoMailAddress = ConfigurationManager.AppSettings["MailInfo"];
        }

        #endregion

        #region Methods

        public void SendServiceJobStatus(string jobName, string status)
        {
            string subject = "Job " + jobName + ": " + status + " at " + DateTime.Now.ToShortTimeString();
            string body = "Job " + jobName + ": " + status + " at " + DateTime.Now.ToString();

            base.Send(MailAddressesEnum.Service, this._serviceMailAddress, this._infoMailAddress, subject, body);
        }

        #endregion
    }
}
