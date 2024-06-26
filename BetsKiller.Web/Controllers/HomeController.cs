﻿using System.Web.Mvc;

namespace BetsKiller.Web.Controllers
{
    public class HomeController : Controller
    {
        #region Index

        public ActionResult Index()
        {
            BetsKiller.BL.Home.GetData getData = new BL.Home.GetData();
            getData.Start();

            return View(getData.HomeViewModel);
        }

        #endregion

        #region TermsOfUse

        public ActionResult TermsOfUse()
        {
            return View();
        }

        #endregion
    }
}