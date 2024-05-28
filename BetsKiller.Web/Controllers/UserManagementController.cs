using BetsKiller.BL.UserManagement;
using BetsKiller.Helper.Constants;
using BetsKiller.ViewModel.UserManagement;
using System.Web.Mvc;

namespace BetsKiller.Web.Controllers
{
    [Authorize(Roles = RolesConst.Admin)]
    public class UserManagementController : Controller
    {
        #region Index

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        #endregion

        #region UserList

        [HttpGet]
        public ActionResult UserList()
        {
            GetUsers getUsers = new GetUsers();
            getUsers.Start();

            if (getUsers.Response.Success)
            {
                return View(getUsers.UsersListViewModels);
            }
            else //Error
            {
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult UserList(UsersListViewModel usersListViewModel)
        {
            GetUsers getUsers = new GetUsers(usersListViewModel.UserProfileSearchViewModel);
            getUsers.Start();

            if (getUsers.Response.Success)
            {
                return View(getUsers.UsersListViewModels);
            }
            else //Error
            {
                return View("Error");
            }
        }

        #endregion

        #region UserAdd

        [HttpGet]
        public ActionResult UserAdd()
        {
            AddUser addUser = new AddUser();

            return View(addUser.UserAddViewModel);
        }

        [HttpPost]
        public ActionResult UserAdd(UserAddViewModel userAddViewModel)
        {
            AddUser addUser = new AddUser(userAddViewModel);

            if (ModelState.IsValid)
            {
                addUser.Start();

                return RedirectToAction("UserList");
            }

            return View(userAddViewModel);
        }

        #endregion

        #region UserEdit

        [HttpGet]
        public ActionResult UserEdit(string username)
        {
            EditUser editUser = new EditUser(username);
            if (!string.IsNullOrEmpty(editUser.UserEditViewModel.Email))
            {
                return View(editUser.UserEditViewModel);
            }
            else
            {
                // Sending user back to user-list page because username not found.
                return RedirectToAction("UserList");
            }
        }

        [HttpPost]
        public ActionResult UserEdit(UserEditViewModel userEditViewModel)
        {
            EditUser editUser = new EditUser(userEditViewModel);
            editUser.Start();

            if (editUser.Response.Success)
            {
                // Update success, return user to list
                return RedirectToAction("UserList");
            }
            else
            {
                this.ModelState.AddModelError(string.Empty, editUser.Response.Message);
                return View(userEditViewModel);
            }
        }

        #endregion
    }
}