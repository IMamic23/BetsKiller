using BetsKiller.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.DAL
{
    public interface IAppDataRepository : IDisposable
    {
        #region TeamsNBA

        void SaveTeamsNBA(IEnumerable<TeamsNBA> teams);

        IQueryable<TeamsNBA> GetAllTeamsNBA();

        #endregion

        #region TeamsNBANames

        IQueryable<TeamsNBANames> GetTeamsNBANames();

        #endregion

        #region LeadersNBACategories

        IQueryable<LeadersNBACategories> GetLeadersNBACategories();

        #endregion

        #region LeadersNBA

        void ClearLeadersNBA();

        void SaveLeadersNBA(IEnumerable<LeadersNBA> leaders);

        IQueryable<LeadersNBA> GetAllLeadersNBA();

        #endregion

        #region RostersNBA

        void ClearRostersNBA();

        void SaveRostersNBA(IEnumerable<RostersNBA> rosters);

        #endregion

        #region DraftsNBA

        void ClearDraftsNBA();

        void SaveDraftsNBA(IEnumerable<DraftsNBA> drafts);

        #endregion

        #region ScheduleResultsNBA

        void SaveScheduleResultsNBA(IEnumerable<ScheduleResultsNBA> scheduleResults);

        IQueryable<ScheduleResultsNBA> GetAllScheduleResultsNBA();

        #endregion

        #region Sports

        IQueryable<Sport> GetAllSports();

        #endregion

        #region NewsFeed

        void ClearNewsFeed();

        void DeleteNewsPublish(int id);

        void SaveNewsFeed(IEnumerable<NewsFeed> newsFeedList);

        IQueryable<NewsFeed> GetAllNewsFeed();

        #endregion

        #region NewsPublish

        void DeleteNewsFeed(int id);

        void SaveNewsPublish(NewsPublish newsPublish);

        IQueryable<NewsPublish> GetAllNewsPublish();

        #endregion

        #region Injuries

        void ClearInjuries();

        void SaveInjuries(IEnumerable<Injuries> injuries);

        #endregion

        #region PowerRankingsNBA

        void ClearPowerRankingsNBA();

        void SavePowerRankingsNBA(IEnumerable<PowerRankingsNBA> powerRankings);

        #endregion

        #region Analysis

        IQueryable<Analysis> GetAllAnalysis();

        void SaveAnalysis(IEnumerable<Analysis> analysis);

        void DeleteAnalysis(int id);

        #endregion

        #region AnalysisProfit

        IQueryable<AnalysisProfit> GetAllAnalysisProfit();

        void SaveAnalysisProfit(IEnumerable<AnalysisProfit> analysisProfit);

        #endregion

        #region BetStatus

        IQueryable<BetStatus> GetAllBetStatus();

        #endregion

        #region BetType

        IQueryable<BetType> GetAllBetType();

        #endregion

        #region AnalyseType

        IQueryable<AnalyseType> GetAllAnalyseType();

        #endregion

        #region Standings

        void ClearStandings();

        void SaveStandings(IEnumerable<Standings> standings);

        IQueryable<Standings> GetAllStandings();

        #endregion

        #region Rosters

        IQueryable<RostersNBA> GetAllRostersNBA();

        #endregion

        #region Injuries

        IQueryable<Injuries> GetAllInjuries();

        #endregion

        #region PowerRankingsNBA

        IQueryable<PowerRankingsNBA> GetAllPowerRankingsNBA();

        #endregion
    }
}