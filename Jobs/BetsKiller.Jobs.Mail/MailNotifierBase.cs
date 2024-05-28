using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace BetsKiller.Jobs.Mail
{
    public class MailNotifierBase
    {
        #region Private

        private SmtpClient _client;

        #endregion

        #region Constructor

        public MailNotifierBase()
        {
            this._client = new SmtpClient(ConfigurationManager.AppSettings["MailHost"], Convert.ToInt32(ConfigurationManager.AppSettings["MailPort"]));
        }

        #endregion

        #region Methods

        protected void Send(MailAddressesEnum address, MailMessage mail)
        {
            this.SetCredentials(address);
            
            this._client.Send(mail);
        }

        #endregion

        #region Helper methods

        private void SetCredentials(MailAddressesEnum address)
        {
            this._client.UseDefaultCredentials = false;

            if (address == MailAddressesEnum.Admin)
            {
                this._client.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["MailAdminUsername"], ConfigurationManager.AppSettings["MailAdminPassword"]);
            }
            else // address == MailAddressesEnum.NoReply
            {
                this._client.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["MailNoReplyUsername"], ConfigurationManager.AppSettings["MailNoReplyPassword"]);
            }

            this._client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
        }

        #endregion
    }
}
