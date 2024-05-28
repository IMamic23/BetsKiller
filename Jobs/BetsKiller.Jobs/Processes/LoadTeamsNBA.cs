using System.Collections.Generic;
using System.Linq;
using System.Threading;
using BetsKiller.Model;

namespace BetsKiller.Jobs.Processes
{
    public class LoadTeamsNBA : Load
    {
        #region Private

        private List<TeamsNBA> _teams;

        private List<TeamsNBANames> _teamsNbaNames;
        private string _season;

        #endregion

        #region Methods

        public void Start()
        {
            // Get teams NBA names and season
            this._teamsNbaNames = base.GetTeamsNBANames();
            this._season = base.GetCurrentSeasonSportsDatabase();

            // Create teams
            this.GetCreateTeams();

            // Save teams in DB
            this.SaveTeams();

            // Refresh teams
            this._teams = base.GetTeamsNBA();

            // Get stats
            this.GetParseTeamsStats();
            this.GetParseTeamsStatsAverage();

            // Update teams
            this.SaveTeams();
        }

        #endregion

        #region Helper methods

        private void GetCreateTeams()
        {
            // Get teams informations from Erikberg
            BetsKiller.API.Erikberg.Methods.MethodTeams methodTeams = new BetsKiller.API.Erikberg.Methods.MethodTeams();
            IEnumerable<BetsKiller.API.Erikberg.Entities.Team> teamsInfo = methodTeams.Get(API.Erikberg.Enums.SportEnum.nba);

            // Get teams
            this._teams = new List<TeamsNBA>();

            // Parse Erikbergs teams informations and create teams collection for DB
            foreach (BetsKiller.API.Erikberg.Entities.Team teamInfo in teamsInfo)
            {
                TeamsNBA team = new TeamsNBA();

                team.Abbreviation = teamInfo.Abbreviation;
                team.Active = teamInfo.Active;
                team.FirstName = teamInfo.FirstName;
                team.LastName = teamInfo.LastName;
                team.Conference = teamInfo.Conference;
                team.Division = teamInfo.Division;
                team.SiteName = teamInfo.SiteName;
                team.City = teamInfo.City;
                team.State = teamInfo.State;

                TeamsNBANames teamName = this._teamsNbaNames.Single(x => x.NameErikberg == teamInfo.TeamId);

                team.Name_Id = teamName.Id;

                this._teams.Add(team);
            }
        }

        private void SaveTeams()
        {
            // Save teams with both sides data
            base.AppDataRepository.SaveTeamsNBA(this._teams);
        }

        private void GetParseTeamsStats()
        {
            #region Both

            BetsKiller.API.SportsDatabase.Methods.MethodTeamsStats bothMethodTeamStats = new BetsKiller.API.SportsDatabase.Methods.MethodTeamsStats(BetsKiller.API.SportsDatabase.Enums.SportEnum.nba, BetsKiller.API.SportsDatabase.Enums.PlayingSideEnum.Both, this._season);
            List<BetsKiller.API.SportsDatabase.Entities.TeamStat> bothTeamStats = bothMethodTeamStats.Get();

            foreach (BetsKiller.API.SportsDatabase.Entities.TeamStat teamStat in bothTeamStats)
            {
                TeamsNBA team = this._teams.Single(x => x.Name.NameSportsdatabase == teamStat.TeamName);

                team.GamesInSeason = teamStat.Games;
                team.BothSU = teamStat.SU;
                team.BothATS = teamStat.ATS;
                team.BothOU = teamStat.OU;
                team.BothAvgLine = teamStat.AvgLine;
                team.BothAvgTotal = teamStat.AvgTotal;
            }

            #endregion

            Thread.Sleep(base.WAIT_TIME);

            #region Home

            BetsKiller.API.SportsDatabase.Methods.MethodTeamsStats homeMethodTeamStats = new BetsKiller.API.SportsDatabase.Methods.MethodTeamsStats(BetsKiller.API.SportsDatabase.Enums.SportEnum.nba, BetsKiller.API.SportsDatabase.Enums.PlayingSideEnum.Home, this._season);
            List<BetsKiller.API.SportsDatabase.Entities.TeamStat> homeTeamStats = homeMethodTeamStats.Get();

            foreach (BetsKiller.API.SportsDatabase.Entities.TeamStat teamStat in homeTeamStats)
            {
                TeamsNBA team = this._teams.Single(x => x.Name.NameSportsdatabase == teamStat.TeamName);

                team.HomeSU = teamStat.SU;
                team.HomeATS = teamStat.ATS;
                team.HomeOU = teamStat.OU;
                team.HomeAvgLine = teamStat.AvgLine;
                team.HomeAvgTotal = teamStat.AvgTotal;
            }

            #endregion

            Thread.Sleep(base.WAIT_TIME);

            #region Away

            BetsKiller.API.SportsDatabase.Methods.MethodTeamsStats awayMethodTeamStats = new BetsKiller.API.SportsDatabase.Methods.MethodTeamsStats(BetsKiller.API.SportsDatabase.Enums.SportEnum.nba, BetsKiller.API.SportsDatabase.Enums.PlayingSideEnum.Away, this._season);
            List<BetsKiller.API.SportsDatabase.Entities.TeamStat> awayTeamStats = awayMethodTeamStats.Get();

            foreach (BetsKiller.API.SportsDatabase.Entities.TeamStat teamStat in awayTeamStats)
            {
                TeamsNBA team = this._teams.Single(x => x.Name.NameSportsdatabase == teamStat.TeamName);

                team.AwaySU = teamStat.SU;
                team.AwayATS = teamStat.ATS;
                team.AwayOU = teamStat.OU;
                team.AwayAvgLine = teamStat.AvgLine;
                team.AwayAvgTotal = teamStat.AvgTotal;
            }

            #endregion

            Thread.Sleep(base.WAIT_TIME);
        }

        private void GetParseTeamsStatsAverage()
        {
            #region Both

            BetsKiller.API.SportsDatabase.Methods.MethodTeamsStatsAverage bothMethodTeamStatsAverage = new BetsKiller.API.SportsDatabase.Methods.MethodTeamsStatsAverage(BetsKiller.API.SportsDatabase.Enums.SportEnum.nba, BetsKiller.API.SportsDatabase.Enums.PlayingSideEnum.Both, this._season);
            List<BetsKiller.API.SportsDatabase.Entities.TeamStatAverage> bothTeamsStatsAverage = bothMethodTeamStatsAverage.Get();

            foreach (BetsKiller.API.SportsDatabase.Entities.TeamStatAverage teamStat in bothTeamsStatsAverage)
            {
                TeamsNBA team = this._teams.Single(x => x.Name.NameSportsdatabase == teamStat.TeamName);

                team.BothAvgPoints = teamStat.AvgPoints;
                team.BothAvgAssists = teamStat.AvgAssists;
                team.BothAvgRebounds = teamStat.AvgRebounds;
                team.BothAvgBlocks = teamStat.AvgBlocks;
                team.BothAvgFouls = teamStat.AvgFouls;
                team.BothAvgTurnovers = teamStat.AvgTurnovers;
            }

            #endregion

            Thread.Sleep(base.WAIT_TIME);

            #region Home

            BetsKiller.API.SportsDatabase.Methods.MethodTeamsStatsAverage homeMethodTeamStatsAverage = new BetsKiller.API.SportsDatabase.Methods.MethodTeamsStatsAverage(BetsKiller.API.SportsDatabase.Enums.SportEnum.nba, BetsKiller.API.SportsDatabase.Enums.PlayingSideEnum.Home, this._season);
            List<BetsKiller.API.SportsDatabase.Entities.TeamStatAverage> homeTeamsStatsAverage = homeMethodTeamStatsAverage.Get();

            foreach (BetsKiller.API.SportsDatabase.Entities.TeamStatAverage teamStat in homeTeamsStatsAverage)
            {
                TeamsNBA team = this._teams.Single(x => x.Name.NameSportsdatabase == teamStat.TeamName);

                team.HomeAvgPoints = teamStat.AvgPoints;
                team.HomeAvgAssists = teamStat.AvgAssists;
                team.HomeAvgRebounds = teamStat.AvgRebounds;
                team.HomeAvgBlocks = teamStat.AvgBlocks;
                team.HomeAvgFouls = teamStat.AvgFouls;
                team.HomeAvgTurnovers = teamStat.AvgTurnovers;
            }

            #endregion

            Thread.Sleep(base.WAIT_TIME);

            #region Away

            BetsKiller.API.SportsDatabase.Methods.MethodTeamsStatsAverage awayMethodTeamStatsAverage = new BetsKiller.API.SportsDatabase.Methods.MethodTeamsStatsAverage(BetsKiller.API.SportsDatabase.Enums.SportEnum.nba, BetsKiller.API.SportsDatabase.Enums.PlayingSideEnum.Away, this._season);
            List<BetsKiller.API.SportsDatabase.Entities.TeamStatAverage> awayTeamsStatsAverage = awayMethodTeamStatsAverage.Get();

            foreach (BetsKiller.API.SportsDatabase.Entities.TeamStatAverage teamStat in awayTeamsStatsAverage)
            {
                TeamsNBA team = this._teams.Single(x => x.Name.NameSportsdatabase == teamStat.TeamName);

                team.AwayAvgPoints = teamStat.AvgPoints;
                team.AwayAvgAssists = teamStat.AvgAssists;
                team.AwayAvgRebounds = teamStat.AvgRebounds;
                team.AwayAvgBlocks = teamStat.AvgBlocks;
                team.AwayAvgFouls = teamStat.AvgFouls;
                team.AwayAvgTurnovers = teamStat.AvgTurnovers;
            }

            #endregion
        }

        #endregion
    }
}