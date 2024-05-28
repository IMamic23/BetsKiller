using BetsKiller.DAL;
using BetsKiller.DAL.UserManagement;
using BetsKiller.Model.UserManagement;
using System.Collections.Generic;
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
            get { return _paymentSources; }
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
            _paymentSources = new List<SelectListItem>();
            _userManagementRepository = new UserManagementRepository();
        }

        #endregion

        #region Methods

        protected override void Process()
        {
            GetSources();
        }

        #endregion

        #region Helper methods

        private void GetSources()
        {
            IEnumerable<PaymentSource> paymentSources = _userManagementRepository.GetPaymentSources();
            _paymentSources.AddRange(ParsePaymentSources(paymentSources));
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
