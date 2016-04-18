using BetsKiller.BL.UserManagement;
using BetsKiller.Helper.Constants;
using BetsKiller.ViewModel.UserManagement;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace BetsKiller.Web.App_Start
{
    public class StartupConfig
    {
        public static void Configure()
        {
            // Initialize database
            GetUserProfiles getProfiles = new GetUserProfiles(new UsersProfilesSearchViewModel() { ResultLimit = 1 });
            getProfiles.Start();

            // Initialize database web security
            WebSecurity.InitializeDatabaseConnection("BetsKiller.DAL.UserManagement.UserManagementContext", "UserProfile", "UserId", "UserName", false);

            // Create admin user
            if (!WebSecurity.UserExists(UsersConst.AdminUsername))
            {
                WebSecurity.CreateUserAndAccount(UsersConst.AdminUsername, UsersConst.AdminPassword, new { FullName = "ADMIN", RoleActiveFrom = DateTime.Now.Date });
                Roles.AddUserToRole(UsersConst.AdminUsername, RolesConst.Admin);
            }

            // HACK: Accept both decimal separators point and comma
            ModelBinders.Binders.Add(typeof(decimal), new DecimalModelBinder());
            ModelBinders.Binders.Add(typeof(decimal?), new DecimalModelBinder());
        }
    }

    #region Helper

    public class DecimalModelBinder : IModelBinder
    {
        public object BindModel(System.Web.Mvc.ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            object result = null;

            string modelName = bindingContext.ModelName;
            string attemptedValue = bindingContext.ValueProvider.GetValue(modelName).AttemptedValue;

            // Depending on CultureInfo, the NumberDecimalSeparator can be "," or "."
            // Both "." and "," should be accepted, but aren't.
            string wantedSeperator = NumberFormatInfo.CurrentInfo.NumberDecimalSeparator;
            string alternateSeperator = (wantedSeperator == "," ? "." : ",");

            if (attemptedValue.IndexOf(wantedSeperator) == -1
                && attemptedValue.IndexOf(alternateSeperator) != -1)
            {
                attemptedValue =
                    attemptedValue.Replace(alternateSeperator, wantedSeperator);
            }

            try
            {
                if (bindingContext.ModelMetadata.IsNullableValueType
                    && string.IsNullOrWhiteSpace(attemptedValue))
                {
                    return null;
                }
                else if (string.IsNullOrWhiteSpace(attemptedValue))
                {
                    return 0M;
                }

                result = decimal.Parse(attemptedValue, NumberStyles.Any);
            }
            catch (FormatException e)
            {
                bindingContext.ModelState.AddModelError(modelName, e);
            }

            return result;
        }
    }

    #endregion
}