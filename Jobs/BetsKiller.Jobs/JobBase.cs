using BetsKiller.Jobs.Mail;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace BetsKiller.Jobs
{
    public abstract class JobBase
    {
        #region Private

        private IMailNotifier _mailNotifier;

        #endregion

        #region Constants

        private const string STARTED_MESSAGE = "STARTED";
        private const string SUCCESS_MESSAGE = "SUCCESS";
        private const string ERROR_MESSAGE = "FAILED";
        private const int NUMBER_OF_TRY = 3;
        private const int ERROR_WAIT_TIME = 600000;

        #endregion

        #region Private

        private int _tryCounter = NUMBER_OF_TRY;

        #endregion

        #region Properties - protected

        protected readonly int WAIT_TIME = 15000;

        #endregion

        #region Constructors

        public JobBase()
        {
            this._mailNotifier = new MailNotifier();
        }

        #endregion

        #region Methods

        public void StartJob()
        {
            while (this._tryCounter > 0)
            {
                try
                {
                    this.Start();

                    this.Job();

                    this.Finish();
                }
                catch (Exception ex)
                {
                    this.Error(ex);
                }
            }
        }

        protected abstract void Job();

        #endregion

        #region Helper methods

        private void Start()
        {
            // Log info - job successfully started
            LogManager.GetCurrentClassLogger().Info(this.GetType().Name + " " + STARTED_MESSAGE);

            // Send email with status
            //this._mailNotifier.SendServiceJobStatus(this.GetType().Name, STARTED_MESSAGE, string.Empty);
        }

        private void Finish()
        {
            // Log info - job successfully finished
            LogManager.GetCurrentClassLogger().Info(this.GetType().Name + " " + SUCCESS_MESSAGE);

            // Send email with status
            //this._mailNotifier.SendServiceJobStatus(this.GetType().Name, SUCCESS_MESSAGE, string.Empty);

            // Set counter to zero, so while loop can finish
            this._tryCounter = 0;
        }

        private void Error(Exception ex)
        {
            // Log error - job failed
            LogManager.GetCurrentClassLogger().Error(ex, this.GetType().Name + " " + ERROR_MESSAGE);

            // Send email with status
            this._mailNotifier.SendServiceJobStatus(this.GetType().Name, ERROR_MESSAGE, "\r\nMessage:\r\n" + ex.Message + "\r\nStackTrace:\r\n" + ex.StackTrace);

            // Reduce counter by one
            this._tryCounter--;

            // Wait some time until next try
            Thread.Sleep(ERROR_WAIT_TIME);
        }

        #endregion
    }
}