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
            var methodTeams = new BetsKiller.API.Erikberg.Methods.MethodTeams();
            IEnumerable<BetsKiller.API.Erikberg.Entities.Team> teamsInfo = methodTeams.Get(API.Erikberg.Enums.SportEnum.nba);

            // Get teams
            this._teams = new List<TeamsNBA>();

            // Parse Erikbergs teams informations and create teams collection for DB
            foreach (var teamInfo in teamsInfo)
            {
                var team = new TeamsNBA();

                team.Abbreviation = teamInfo.Abbreviation;
                team.Active = teamInfo.Active;
                team.FirstName = teamInfo.FirstName;
                team.LastName = teamInfo.LastName;
                team.Conference = teamInfo.Conference;
                team.Division = teamInfo.Division;
                team.SiteName = teamInfo.SiteName;
                team.City = teamInfo.City;
                team.State = teamInfo.State;

                var teamName = this._teamsNbaNames.Single(x => x.NameErikberg == teamInfo.TeamId);

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

            var bothMethodTeamStats = new BetsKiller.API.SportsDatabase.Methods.MethodTeamsStats(BetsKiller.API.SportsDatabase.Enums.SportEnum.nba, BetsKiller.API.SportsDatabase.Enums.PlayingSideEnum.Both, this._season);
            var bothTeamStats = bothMethodTeamStats.Get();

            foreach (var teamStat in bothTeamStats)
            {
                var team = this._teams.Single(x => x.Name.NameSportsdatabase == teamStat.TeamName);

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

            var homeMethodTeamStats = new BetsKiller.API.SportsDatabase.Methods.MethodTeamsStats(BetsKiller.API.SportsDatabase.Enums.SportEnum.nba, BetsKiller.API.SportsDatabase.Enums.PlayingSideEnum.Home, this._season);
            var homeTeamStats = homeMethodTeamStats.Get();

            foreach (var teamStat in homeTeamStats)
            {
                var team = this._teams.Single(x => x.Name.NameSportsdatabase == teamStat.TeamName);

                team.HomeSU = teamStat.SU;
                team.HomeATS = teamStat.ATS;
                team.HomeOU = teamStat.OU;
                team.HomeAvgLine = teamStat.AvgLine;
                team.HomeAvgTotal = teamStat.AvgTotal;
            }

            #endregion

            Thread.Sleep(base.WAIT_TIME);

            #region Away

            var awayMethodTeamStats = new BetsKiller.API.SportsDatabase.Methods.MethodTeamsStats(BetsKiller.API.SportsDatabase.Enums.SportEnum.nba, BetsKiller.API.SportsDatabase.Enums.PlayingSideEnum.Away, this._season);
            var awayTeamStats = awayMethodTeamStats.Get();

            foreach (var teamStat in awayTeamStats)
            {
                var team = this._teams.Single(x => x.Name.NameSportsdatabase == teamStat.TeamName);

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

            var bothMethodTeamStatsAverage = new BetsKiller.API.SportsDatabase.Methods.MethodTeamsStatsAverage(BetsKiller.API.SportsDatabase.Enums.SportEnum.nba, BetsKiller.API.SportsDatabase.Enums.PlayingSideEnum.Both, this._season);
            var bothTeamsStatsAverage = bothMethodTeamStatsAverage.Get();

            foreach (var teamStat in bothTeamsStatsAverage)
            {
                var team = this._teams.Single(x => x.Name.NameSportsdatabase == teamStat.TeamName);

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

            var homeMethodTeamStatsAverage = new BetsKiller.API.SportsDatabase.Methods.MethodTeamsStatsAverage(BetsKiller.API.SportsDatabase.Enums.SportEnum.nba, BetsKiller.API.SportsDatabase.Enums.PlayingSideEnum.Home, this._season);
            var homeTeamsStatsAverage = homeMethodTeamStatsAverage.Get();

            foreach (var teamStat in homeTeamsStatsAverage)
            {
                var team = this._teams.Single(x => x.Name.NameSportsdatabase == teamStat.TeamName);

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

            var awayMethodTeamStatsAverage = new BetsKiller.API.SportsDatabase.Methods.MethodTeamsStatsAverage(BetsKiller.API.SportsDatabase.Enums.SportEnum.nba, BetsKiller.API.SportsDatabase.Enums.PlayingSideEnum.Away, this._season);
            var awayTeamsStatsAverage = awayMethodTeamStatsAverage.Get();

            foreach (var teamStat in awayTeamsStatsAverage)
            {
                var team = this._teams.Single(x => x.Name.NameSportsdatabase == teamStat.TeamName);

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