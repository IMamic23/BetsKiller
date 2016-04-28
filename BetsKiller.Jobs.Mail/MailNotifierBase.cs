using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

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

        protected void Send(MailAddressesEnum address, string from, string to, string subject, string body)
        {
            this.SetCredentials(address);

            using (MailMessage mail = new MailMessage(from, to))
            {
                mail.Subject = subject;
                mail.Body = body;

                this._client.Send(mail);
            }
        }

        #endregion

        #region Helper methods

        private void SetCredentials(MailAddressesEnum address)
        {
            if (address == MailAddressesEnum.Service)
            {
                this._client.UseDefaultCredentials = false;
                this._client.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["MailServiceUsername"], ConfigurationManager.AppSettings["MailServicePassword"]);
                this._client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            }
        }

        #endregion
    }
}
