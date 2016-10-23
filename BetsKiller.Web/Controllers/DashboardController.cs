using BetsKiller.Helper.Constants;
using BetsKiller.Helper.Entities;
using BetsKiller.ViewModel.Dashboard.Index;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BetsKiller.Web.Controllers
{
    public class DashboardController : Controller
    {
        #region Dashboard

        public ActionResult Index()
        {
            BL.Dashboard.Index.GetData getData = new BL.Dashboard.Index.GetData();
            getData.Start();

            return View(getData.DashboardViewModel);
        }

        [Authorize(Roles = RolesConst.Admin + ", " + RolesConst.ContentManager)]
        [HttpPost]
        public ActionResult SaveAnalysis(SaveAnalysisViewModel saveAnalysisViewModel)
        {
            if (ModelState.IsValid)
            {
                BL.Dashboard.Index.SaveAnalysis saveAnalysis = new BL.Dashboard.Index.SaveAnalysis(saveAnalysisViewModel);
                saveAnalysis.Start();

                // TODO: 
                //if (saveAnalysis.Response.Success)
                //{
                //    return Json(saveAnalysis.Response);
                //}
                //else
                //{
                //    return Json(saveAnalysis.Response);
                //}
            }
            // TODO:
            //else
            //{
            //    return Json(new ProcessResponse() { Success = false, Message = "Please, correct fields data and try again!" });
            //}

            return base.RedirectToAction("Index");
        }

        [Authorize(Roles = RolesConst.Admin + ", " + RolesConst.ContentManager)]
        [HttpGet]
        public ActionResult DeleteAnalysis(int idAnalysis)
        {
            if (idAnalysis > 0)
            {
                BL.Dashboard.Index.DeleteAnalysis deleteAnalysis = new BL.Dashboard.Index.DeleteAnalysis(idAnalysis);
                deleteAnalysis.Start();
            }

            return base.RedirectToAction("Index");
        }

        [Authorize(Roles = RolesConst.Admin + ", " + RolesConst.ContentManager)]
        [HttpPost]
        public ActionResult PublishNews(HeadlineNBAViewModel headlineNbaViewModel)
        {
            if (ModelState.IsValid)
            {
                BL.Dashboard.Index.PublishNews publishNews = new BL.Dashboard.Index.PublishNews(headlineNbaViewModel);
                publishNews.Start();

                // TODO: 
                //if (saveAnalysis.Response.Success)
                //{
                //    return Json(saveAnalysis.Response);
                //}
                //else
                //{
                //    return Json(saveAnalysis.Response);
                //}
            }
            // TODO:
            //else
            //{
            //    return Json(new ProcessResponse() { Success = false, Message = "Please, correct fields data and try again!" });
            //}

            return base.RedirectToAction("Index");
        }

        #endregion

        #region BetsTracker

        //[Authorize]
        public ActionResult BetsTracker()
        {
            BL.Dashboard.BetsTracker.GetData getData = new BL.Dashboard.BetsTracker.GetData();
            getData.Start();

            return View(getData.BetsTrackerViewModel);
        }

        [HttpPost]
        //[Authorize]
        public JsonResult BetsTrackerGetProfile(string id)
        {
            BL.Dashboard.BetsTracker.GetProfile getProfile = new BL.Dashboard.BetsTracker.GetProfile(id);
            getProfile.Start();

            return Json(getProfile);
        }

        [HttpPost]
        //[Authorize]
        public JsonResult BetsTrackerDeleteProfile(string id)
        {
            BL.Dashboard.BetsTracker.DeleteProfile deleteProfile = new BL.Dashboard.BetsTracker.DeleteProfile(id);
            deleteProfile.Start();

            return Json(deleteProfile);
        }

        #endregion

        #region TeamsStats

        public ActionResult TeamsStats()
        {
            BL.Dashboard.TeamsStats.GetData getData = new BL.Dashboard.TeamsStats.GetData();
            getData.Start();

            return View(getData.TeamsStatsViewModel);
        }

        #endregion

        #region Leaders

        public ActionResult Leaders()
        {
            BL.Dashboard.Leaders.GetData getData = new BL.Dashboard.Leaders.GetData();
            getData.Start();

            return View(getData.LeadersViewModel);
        }

        #endregion

        #region Standings

        public ActionResult Standings()
        {
            BL.Dashboard.Standings.GetData getData = new BL.Dashboard.Standings.GetData();
            getData.Start();

            return View(getData.StandingsViewModel);
        }

        #endregion

        #region TeamsRosters

        public ActionResult TeamsRosters()
        {
            BL.Dashboard.TeamsRosters.GetData getData = new BL.Dashboard.TeamsRosters.GetData();
            getData.Start();

            return View(getData.TeamsRostersViewModel);
        }

        #endregion

        #region ScheduleResuts

        public ActionResult ScheduleResults()
        {
            BL.Dashboard.ScheduleResults.GetData getData = new BL.Dashboard.ScheduleResults.GetData();
            getData.Start();

            return View(getData.ScheduleResultsViewModel);
        }

        #endregion

        #region Injuries

        public ActionResult Injuries()
        {
            BL.Dashboard.Injuries.GetData getData = new BL.Dashboard.Injuries.GetData();
            getData.Start();

            return View(getData.InjuriesViewModel);
        }

        #endregion

        #region PowerRankings

        public ActionResult PowerRankings()
        {
            BL.Dashboard.PowerRankings.GetData getData = new BL.Dashboard.PowerRankings.GetData();
            getData.Start();

            return View(getData.PowerRankingsViewModel);
        }

        #endregion

        #region SportsPicks

        public ActionResult SportsPicks()
        {
            BL.Dashboard.SportsPicks.GetData getData = new BL.Dashboard.SportsPicks.GetData();
            getData.Start();

            return View(getData.SportsPicksViewModel);
        }

        #endregion

        #region Terminology

        public ActionResult Terminology()
        {
            return View();
        }

        #endregion
    }
}