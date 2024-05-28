using System;
using System.Configuration;
using System.Net.Mail;
using System.Net.Mime;

namespace BetsKiller.Jobs.Mail
{
    public class MailNotifier : MailNotifierBase, IMailNotifier
    {
        #region Consts

        private string _noReplyMailAddress;
        private string _adminMailAddress;

        #endregion

        #region Constructors

        public MailNotifier()
            : base()
        {
            this._noReplyMailAddress = ConfigurationManager.AppSettings["MailNoReplyUsername"];
            this._adminMailAddress = ConfigurationManager.AppSettings["MailAdminUsername"];
        }

        #endregion

        #region Methods

        public void SendServiceJobStatus(string jobName, string status, string message)
        {
            using (MailMessage mail = new MailMessage(this._adminMailAddress, this._adminMailAddress))
            {
                mail.Subject = "Job " + jobName + ": " + status + " at " + DateTime.Now.ToShortTimeString();
                mail.Body = message;

                base.Send(MailAddressesEnum.Admin, mail);
            }
        }

        public void SendMailConfirmation(string link, string mailAddressTo)
        {
            using (MailMessage mail = new MailMessage(this._noReplyMailAddress, mailAddressTo))
            {
                mail.Subject = "Confirm your account";
                mail.Body = "Please confirm your account by clicking this <a href=\"" + link + "\">link</a>.<br/><br/>BetsKiller team";

                mail.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(mail.Body, null, MediaTypeNames.Text.Html));

                base.Send(MailAddressesEnum.NoReply, mail);
            }
        }

        public void SendPasswordReset(string link, string mailAddressTo)
        {
            using (MailMessage mail = new MailMessage(this._noReplyMailAddress, mailAddressTo))
            {
                mail.Subject = "Password reset";
                mail.Body = "Please reset your password by clicking this <a href=\"" + link + "\">link</a>.<br/><br/>BetsKiller team";

                mail.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(mail.Body, null, MediaTypeNames.Text.Html));

                base.Send(MailAddressesEnum.NoReply, mail);
            }
        }

        #endregion
    }
}
