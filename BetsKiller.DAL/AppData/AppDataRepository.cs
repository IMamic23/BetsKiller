using BetsKiller.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.DAL.AppData
{
    public class AppDataRepository : IAppDataRepository, IDisposable
    {
        #region Private

        /// <summary>
        /// Disposing state.
        /// </summary>
        private bool _disposed = false;

        /// <summary>
        /// AppData DB context.
        /// </summary>
        private readonly AppDataContext _context;

        #endregion

        #region Constructors

        public AppDataRepository()
        {
            this._context = new AppDataContext();
        }

        #endregion

        #region Methods

        #region TeamsNBA

        public void SaveTeamsNBA(IEnumerable<TeamsNBA> teams)
        {
            foreach (TeamsNBA team in teams)
            {
                TeamsNBA entity = this._context.TeamsNBA.FirstOrDefault(x => x.Name_Id == team.Name_Id);

                if (entity != null)
                {
                    team.Id = entity.Id;
                    this._context.Entry(entity).CurrentValues.SetValues(team);
                }
                else
                {
                    this._context.TeamsNBA.Add(team);
                }
            }

            this._context.SaveChanges();
        }

        public IQueryable<TeamsNBA> GetAllTeamsNBA()
        {
            return this._context.TeamsNBA.Include(x => x.Name);
        }

        #endregion

        #region TeamsNBANames

        public IQueryable<TeamsNBANames> GetTeamsNBANames()
        {
            return this._context.TeamsNBANames;
        }

        #endregion

        #region LeadersNBACategories

        public IQueryable<LeadersNBACategories> GetLeadersNBACategories()
        {
            return this._context.LeadersNBACategories;
        }

        #endregion

        #region LeadersNBA

        public void ClearLeadersNBA()
        {
            this._context.LeadersNBA.RemoveRange(this._context.LeadersNBA);
            this._context.SaveChanges();

            this._context.Database.ExecuteSqlCommand("TRUNCATE TABLE LeadersNBA");
        }

        public void SaveLeadersNBA(IEnumerable<LeadersNBA> leaders)
        {
            foreach (LeadersNBA leader in leaders)
            {
                this._context.LeadersNBA.Add(leader);
            }

            this._context.SaveChanges();
        }

        public IQueryable<LeadersNBA> GetAllLeadersNBA()
        {
            return this._context.LeadersNBA.Include(x => x.Category);
        }

        #endregion

        #region RostersNBA

        public void ClearRostersNBA()
        {
            this._context.RostersNBA.RemoveRange(this._context.RostersNBA);
            this._context.SaveChanges();

            this._context.Database.ExecuteSqlCommand("TRUNCATE TABLE RostersNBA");
        }

        public void SaveRostersNBA(IEnumerable<RostersNBA> rosters)
        {
            foreach (RostersNBA roster in rosters)
            {
                this._context.RostersNBA.Add(roster);
            }

            this._context.SaveChanges();
        }

        #endregion

        #region DraftsNBA

        public void ClearDraftsNBA()
        {
            this._context.DraftsNBA.RemoveRange(this._context.DraftsNBA);
            this._context.SaveChanges();

            this._context.Database.ExecuteSqlCommand("TRUNCATE TABLE DraftsNBA");
        }

        public void SaveDraftsNBA(IEnumerable<DraftsNBA> drafts)
        {
            foreach (DraftsNBA draft in drafts)
            {
                this._context.DraftsNBA.Add(draft);
            }

            this._context.SaveChanges();
        }

        #endregion

        #region ScheduleResultsNBA

        public void SaveScheduleResultsNBA(IEnumerable<ScheduleResultsNBA> scheduleResults)
        {
            foreach (ScheduleResultsNBA scheduleResult in scheduleResults)
            {
                ScheduleResultsNBA entity = this._context.ScheduleResultsNBA.FirstOrDefault(x => x.EventId == scheduleResult.EventId);

                if (entity != null)
                {
                    scheduleResult.Id = entity.Id;
                    this._context.Entry(entity).CurrentValues.SetValues(scheduleResult);
                }
                else
                {
                    this._context.ScheduleResultsNBA.Add(scheduleResult);
                }
            }

            this._context.SaveChanges();
        }

        public IQueryable<ScheduleResultsNBA> GetAllScheduleResultsNBA()
        {
            return this._context.ScheduleResultsNBA.Include(x => x.Team).Include(x => x.Opponent).Include(x => x.Team.Name).Include(x => x.Opponent.Name);
        }

        #endregion

        #region Sports

        public IQueryable<Sport> GetAllSports()
        {
            return this._context.Sport;
        }

        #endregion

        #region NewsFeed

        public void ClearNewsFeed()
        {
            this._context.NewsFeed.RemoveRange(this._context.NewsFeed);
            this._context.SaveChanges();

            this._context.Database.ExecuteSqlCommand("TRUNCATE TABLE NewsFeed");
        }

        public void DeleteNewsFeed(int id)
        {
            NewsFeed entity = this._context.NewsFeed.Single(x => x.Id == id);

            this._context.NewsFeed.Remove(entity);

            this._context.SaveChanges();
        }

        public void SaveNewsFeed(IEnumerable<NewsFeed> newsFeedList)
        {
            foreach (NewsFeed newsFeed in newsFeedList)
            {
                NewsFeed entity = this._context.NewsFeed.FirstOrDefault(x => x.Id == newsFeed.Id);

                if (entity != null)
                {
                    this._context.Entry(entity).CurrentValues.SetValues(newsFeed);
                }
                else
                {
                    this._context.NewsFeed.Add(newsFeed);
                }
            }

            this._context.SaveChanges();
        }

        public IQueryable<NewsFeed> GetAllNewsFeed()
        {
            return this._context.NewsFeed.Include(x => x.Sport);
        }

        #endregion

        #region NewsPublish

        public void DeleteNewsPublish(int id)
        {
            NewsPublish entity = this._context.NewsPublish.Single(x => x.Id == id);

            this._context.NewsPublish.Remove(entity);

            this._context.SaveChanges();
        }

        public void SaveNewsPublish(NewsPublish newsPublish)
        {
            NewsPublish entity = this._context.NewsPublish.FirstOrDefault(x => x.Id == newsPublish.Id);

            if (entity != null)
            {
                this._context.Entry(entity).CurrentValues.SetValues(newsPublish);
            }
            else
            {
                this._context.NewsPublish.Add(newsPublish);
            }

            this._context.SaveChanges();
        }

        public IQueryable<NewsPublish> GetAllNewsPublish()
        {
            return this._context.NewsPublish.Include(x => x.Sport);
        }

        #endregion

        #region Injuries

        public void ClearInjuries()
        {
            this._context.Injuries.RemoveRange(this._context.Injuries);
            this._context.SaveChanges();

            this._context.Database.ExecuteSqlCommand("TRUNCATE TABLE Injuries");
        }

        public void SaveInjuries(IEnumerable<Injuries> injuries)
        {
            foreach (Injuries injury in injuries)
            {
                this._context.Injuries.Add(injury);
            }

            this._context.SaveChanges();
        }

        #endregion

        #region PowerRankingsNBA

        public void ClearPowerRankingsNBA()
        {
            this._context.PowerRankingsNBA.RemoveRange(this._context.PowerRankingsNBA);
            this._context.SaveChanges();

            this._context.Database.ExecuteSqlCommand("TRUNCATE TABLE PowerRankingsNBA");
        }

        public void SavePowerRankingsNBA(IEnumerable<PowerRankingsNBA> powerRankings)
        {
            foreach (PowerRankingsNBA powerRanking in powerRankings)
            {
                this._context.PowerRankingsNBA.Add(powerRanking);
            }

            this._context.SaveChanges();
        }

        #endregion

        #region Analysis

        public IQueryable<Analysis> GetAllAnalysis()
        {
            return this._context.Analysis.Include(x => x.EventNBA).Include(x => x.EventNBA.Team).Include(x => x.EventNBA.Opponent).Include(x => x.EventNBA.Team.Name).Include(x => x.EventNBA.Opponent.Name).Include(x => x.AnalyseType).Include(x => x.BetStatus).Include(x => x.BetType).Include(x => x.Sport);
        }

        public void SaveAnalysis(IEnumerable<Analysis> analysis)
        {
            foreach (Analysis analyse in analysis)
            {
                Analysis entity = this._context.Analysis.FirstOrDefault(x => x.Id == analyse.Id);

                if (entity != null)
                {
                    analyse.Id = entity.Id;
                    this._context.Entry(entity).CurrentValues.SetValues(analyse);
                }
                else
                {
                    this._context.Analysis.Add(analyse);
                }
            }

            this._context.SaveChanges();
        }

        public void DeleteAnalysis(int id)
        {
            Analysis entity = this._context.Analysis.Single(x => x.Id == id);

            this._context.Analysis.Remove(entity);

            this._context.SaveChanges();
        }

        #endregion

        #region AnalysisProfit

        public IQueryable<AnalysisProfit> GetAllAnalysisProfit()
        {
            return this._context.AnalysisProfit.Include(x => x.Sport);
        }

        public void SaveAnalysisProfit(IEnumerable<AnalysisProfit> analysisProfit)
        {
            foreach (AnalysisProfit analyse in analysisProfit)
            {
                AnalysisProfit entity = this._context.AnalysisProfit.FirstOrDefault(x => x.Id == analyse.Id);

                if (entity != null)
                {
                    analyse.Id = entity.Id;
                    this._context.Entry(entity).CurrentValues.SetValues(analyse);
                }
                else
                {
                    this._context.AnalysisProfit.Add(analyse);
                }
            }

            this._context.SaveChanges();
        }

        #endregion

        #region BetStatus

        public IQueryable<BetStatus> GetAllBetStatus()
        {
            return this._context.BetStatus;
        }

        #endregion

        #region BetType

        public IQueryable<BetType> GetAllBetType()
        {
            return this._context.BetType;
        }

        #endregion

        #region AnalyseType

        public IQueryable<AnalyseType> GetAllAnalyseType()
        {
            return this._context.AnalyseType;
        }

        #endregion

        #region Standings

        public void ClearStandings()
        {
            this._context.Standings.RemoveRange(this._context.Standings);
            this._context.SaveChanges();

            this._context.Database.ExecuteSqlCommand("TRUNCATE TABLE Standings");
        }

        public void SaveStandings(IEnumerable<Standings> standings)
        {
            foreach (Standings standing in standings)
            {
                this._context.Standings.Add(standing);
            }

            this._context.SaveChanges();
        }

        public IQueryable<Standings> GetAllStandings()
        {
            return this._context.Standings;
        }

        #endregion

        #region RostersNBA

        public IQueryable<RostersNBA> GetAllRostersNBA()
        {
            return this._context.RostersNBA;
        }

        #endregion

        #region Injuries

        public IQueryable<Injuries> GetAllInjuries()
        {
            return this._context.Injuries;
        }

        #endregion

        #region PowerRankingsNBA

        public IQueryable<PowerRankingsNBA> GetAllPowerRankingsNBA()
        {
            return this._context.PowerRankingsNBA;
        }

        #endregion

        #endregion

        #region Dispose

        /// <summary>
        /// Disposing method.
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    this._context.Dispose();
                }
            }
            this._disposed = true;
        }

        /// <summary>
        /// Disposing method.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
