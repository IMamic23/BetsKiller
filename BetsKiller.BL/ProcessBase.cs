using BetsKiller.DAL;
using BetsKiller.DAL.AppData;
using BetsKiller.Helper.Entities;
using BetsKiller.Model;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.BL
{
    public abstract class ProcessBase
    {
        #region Private

        private ProcessResponse _response;

        #endregion

        #region Properties

        public ProcessResponse Response
        {
            get { return this._response; }
        }

        #endregion

        #region Properties - abstract

        protected abstract string _successMessage { get; }
        protected abstract string _failMessage { get; }

        #endregion

        #region Methods

        public void Start()
        {
            try
            {
                this.Process();

                this.SetResponse(true);
            }
            catch (Exception ex)
            {
                LogManager.GetCurrentClassLogger().Error(ex, "BL - " + this.GetType().Name);

                this.SetResponse(false);
            }
        }

        #endregion

        #region Methods - abstract

        protected abstract void Process();

        #endregion

        #region Helper methods

        private void SetResponse(bool success)
        {
            this._response = new ProcessResponse()
            {
                Success = success,
                Message = success ? this._successMessage : this._failMessage
            };
        }

        #endregion
    }
}
