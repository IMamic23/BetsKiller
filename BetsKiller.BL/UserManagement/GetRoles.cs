﻿using BetsKiller.DAL;
using BetsKiller.DAL.UserManagement;
using BetsKiller.Model.UserManagement;
using BetsKiller.ViewModel.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            get { return this._roles; }
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
            this._predefinedType = predefinedType;
            this._roles = new List<SelectListItem>();
            this._userManagementRepository = new UserManagementRepository();
        }

        public GetRoles(AddPredefinedType predefinedType, string userName)
        {
            this._predefinedType = predefinedType;
            this._userName = userName;
            this._roles = new List<SelectListItem>();
            this._userManagementRepository = new UserManagementRepository();
        }

        #endregion

        #region Methods

        protected override void Process()
        {
            this.AddPredefinedRole();

            if (string.IsNullOrEmpty(this._userName))
            {
                this.GetAllRoles();
            }
            else
            {
                this.GetUserRoles();
            }

        }

        #endregion

        #region Helper

        private void AddPredefinedRole()
        {
            if (this._predefinedType != AddPredefinedType.None)
            {
                SelectListItem roleItem = new SelectListItem();
                roleItem.Selected = true;

                if (this._predefinedType == AddPredefinedType.Any)
                {
                    roleItem.Text = "Any";
                }
                else if (this._predefinedType == AddPredefinedType.Blank)
                {
                    roleItem.Text = string.Empty;
                }

                roleItem.Value = string.Empty;

                this._roles.Add(roleItem);
            }
        }

        private void GetAllRoles()
        {
            IEnumerable<Role> roles = this._userManagementRepository.SelectRoles();

            this._roles.AddRange(this.ParseRoles(roles));
        }

        private void GetUserRoles()
        {
            IEnumerable<UserProfile> users = this._userManagementRepository.SelectUserProfiles(this._userName, null, null);

            this._roles.AddRange(this.ParseRoles(users.First().Roles));
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
