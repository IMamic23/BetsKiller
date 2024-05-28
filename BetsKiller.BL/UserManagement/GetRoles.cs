using BetsKiller.DAL;
using BetsKiller.DAL.UserManagement;
using BetsKiller.Model.UserManagement;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BetsKiller.BL.UserManagement
{
    public class GetRoles : ProcessBase
    {
        #region Private

        private AddPredefinedType _predefinedType;

        private string _userName;
        private List<SelectListItem> _roles;
        private IUserManagementRepository _userManagementRepository;

        #endregion

        #region Properties

        public List<SelectListItem> Roles
        {
            get { return _roles; }
        }

        #endregion

        #region Properties - override

        protected override string _successMessage
        {
            get { return "Get roles successfully."; }
        }

        protected override string _failMessage
        {
            get { return "Get roles failed."; }
        }

        #endregion

        #region Constructors

        public GetRoles(AddPredefinedType predefinedType)
        {
            _predefinedType = predefinedType;
            _roles = new List<SelectListItem>();
            _userManagementRepository = new UserManagementRepository();
        }

        public GetRoles(AddPredefinedType predefinedType, string userName)
        {
            _predefinedType = predefinedType;
            _userName = userName;
            _roles = new List<SelectListItem>();
            _userManagementRepository = new UserManagementRepository();
        }

        #endregion

        #region Methods

        protected override void Process()
        {
            AddPredefinedRole();

            if (string.IsNullOrEmpty(_userName))
            {
                GetAllRoles();
            }
            else
            {
                GetUserRoles();
            }

        }

        #endregion

        #region Helper

        private void AddPredefinedRole()
        {
            if (_predefinedType != AddPredefinedType.None)
            {
                SelectListItem roleItem = new SelectListItem();
                roleItem.Selected = true;

                if (_predefinedType == AddPredefinedType.Any)
                {
                    roleItem.Text = "Any";
                }
                else if (_predefinedType == AddPredefinedType.Blank)
                {
                    roleItem.Text = string.Empty;
                }

                roleItem.Value = string.Empty;

                _roles.Add(roleItem);
            }
        }

        private void GetAllRoles()
        {
            IEnumerable<Role> roles = _userManagementRepository.SelectRoles();

            _roles.AddRange(ParseRoles(roles));
        }

        private void GetUserRoles()
        {
            IEnumerable<UserProfile> users = _userManagementRepository.SelectUserProfiles(_userName, null, null);

            _roles.AddRange(ParseRoles(users.First().Roles));
        }

        private List<SelectListItem> ParseRoles(IEnumerable<Role> roles)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            foreach (Role role in roles)
            {
                SelectListItem roleItem = new SelectListItem();
                roleItem.Selected = false;
                roleItem.Text = role.RoleName;
                roleItem.Value = role.RoleName;

                list.Add(roleItem);
            }

            return list;
        }

        #endregion

        #region Enums

        public enum AddPredefinedType
        {
            None,
            Any,
            Blank
        }

        #endregion
    }
}
