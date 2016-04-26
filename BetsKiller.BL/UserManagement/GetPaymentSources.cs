using BetsKiller.DAL;
using BetsKiller.DAL.UserManagement;
using BetsKiller.Model.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BetsKiller.BL.UserManagement
{
    public class GetPaymentSources : ProcessBase
    {
        #region Private

        private List<SelectListItem> _paymentSources;

        private IUserManagementRepository _userManagementRepository;

        #endregion

        #region Properties

        public List<SelectListItem> PaymentSources
        {
            get { return this._paymentSources; }
        }

        #endregion

        #region Properties - override

        protected override string _successMessage
        {
            get { return "Get payment sources successfully."; }
        }

        protected override string _failMessage
        {
            get { return "Get payment sources failed."; }
        }

        #endregion

        #region Constructors

        public GetPaymentSources()
        {
            this._paymentSources = new List<SelectListItem>();
            this._userManagementRepository = new UserManagementRepository();
        }

        #endregion

        #region Methods

        protected override void Process()
        {
            this.GetSources();
        }

        #endregion

        #region Helper methods

        private void GetSources()
        {
            IEnumerable<PaymentSource> paymentSources = this._userManagementRepository.GetPaymentSources();
            this._paymentSources.AddRange(this.ParsePaymentSources(paymentSources));
        }

        private List<SelectListItem> ParsePaymentSources(IEnumerable<PaymentSource> paymentSources)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            foreach (PaymentSource paymentSource in paymentSources)
            {
                SelectListItem paymentSourceItem = new SelectListItem();
                paymentSourceItem.Selected = false;
                paymentSourceItem.Text = paymentSource.Name;
                paymentSourceItem.Value = paymentSource.Name;

                list.Add(paymentSourceItem);
            }

            return list;
        }

        #endregion
    }
}
