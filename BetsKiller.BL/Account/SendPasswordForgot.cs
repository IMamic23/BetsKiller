using BetsKiller.Jobs.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.BL.Account
{
    public class SendPasswordForgot : ProcessBase
    {
        #region Private

        private string _forgotToken;
        private string _userMailAddress;

        private string _link;
        private IMailNotifier _mailNotifier;

        #endregion

        #region Properties - override

        protected override string _successMessage
        {
            get { return "Send password forgot successfully."; }
        }

        protected override string _failMessage
        {
            get { return "Send password forgot failed."; }
        }

        #endregion

        #region Constructors

        public SendPasswordForgot(string forgotToken, string userMailAddress)
        {
            this._forgotToken = forgotToken;
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
            this._link = "https://www.betskiller.com/Account/PasswordReset?mail=" + this._userMailAddress + "&token=" + this._forgotToken;
        }

        private void SendMail()
        {
            this._mailNotifier.SendPasswordReset(this._link, this._userMailAddress);
        }

        #endregion
    }
}
