using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BetsKiller.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            BetsKiller.BL.Home.GetData getData = new BL.Home.GetData();
            getData.Start();

            return View(getData.HomeViewModel);
        }
    }
}