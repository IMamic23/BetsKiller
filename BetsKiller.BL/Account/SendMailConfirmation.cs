using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetsKiller.Jobs.Mail;

namespace BetsKiller.BL.Account
{
    public class SendMailConfirmation : ProcessBase
    {
        #region Private

        private string _confirmationToken;
        private string _userMailAddress;

        private string _link;
        private IMailNotifier _mailNotifier;

        #endregion

        #region Properties - override

        protected override string _successMessage
        {
            get { return "Send mail confirmation successfully."; }
        }

        protected override string _failMessage
        {
            get { return "Send mail confirmation failed."; }
        }

        #endregion

        #region Constructors

        public SendMailConfirmation(string confirmationToken, string userMailAddress)
        {
            this._confirmationToken = confirmationToken;
            this._userMailAddress = userMailAddress;
            this._mailNotifier = new MailNotifier();
        }

        #endregion

        #region Methods

        protected override void Process()
        {
            this.CreateLink();

            this.SendMail();
        }

        #endregion

        #region Helper methods

        private void CreateLink()
        {
            this._link = "https://www.betskiller.com/Account/Confirmed?mail=" + this._userMailAddress + "&token=" + this._confirmationToken;
        }

        private void SendMail()
        {
            this._mailNotifier.SendMailConfirmation(this._link, this._userMailAddress);
        }

        #endregion
    }
}
