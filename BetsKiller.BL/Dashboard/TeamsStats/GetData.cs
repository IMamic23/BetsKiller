using BetsKiller.DAL;
using BetsKiller.DAL.AppData;
using BetsKiller.Helper.Constants;
using BetsKiller.Model;
using BetsKiller.ViewModel.Dashboard.TeamsStats;
using System.Collections.Generic;
using System.Linq;

namespace BetsKiller.BL.Dashboard.TeamsStats
{
    public class GetData : ProcessBase
    {
        #region Private

        private TeamsStatsViewModel _teamsStatsViewModel;

        private IAppDataRepository _appDataRepository;

        #endregion

        #region Properties

        public TeamsStatsViewModel TeamsStatsViewModel
        {
            get { return this._teamsStatsViewModel; }
        }

        #endregion

        #region Properties - override

        protected override string _successMessage
        {
            get { return "Teams stats data retrived successfully."; }
        }

        protected override string _failMessage
        {
            get { return "Teams stats data retrive failed."; }
        }

        #endregion

        #region Constructors

        public GetData()
        {
            this._appDataRepository = new AppDataRepository();
            this._teamsStatsViewModel = new TeamsStatsViewModel();
        }

        #endregion

        #region Methods

        protected override void Process()
        {
            // Get and parse NBA teams
            this.GetParseTeamsNBA();
        }

        #endregion

        #region Helper methods

        private void GetParseTeamsNBA()
        {
            this._teamsStatsViewModel.Sides = new List<TeamsStatsSideViewModel>();
            this._teamsStatsViewModel.Sides.Add(new TeamsStatsSideViewModel()
            {
                SideName = TeamsStatsConst.BOTH,
                SideLabel = TeamsStatsConst.BOTHLabel,
                Stats = new List<TeamStatViewModel>()
            });
            this._teamsStatsViewModel.Sides.Add(new TeamsStatsSideViewModel()
            {
                SideName = TeamsStatsConst.HOME,
                SideLabel = TeamsStatsConst.HOMELabel,
                Stats = new List<TeamStatViewModel>()
            });
            this._teamsStatsViewModel.Sides.Add(new TeamsStatsSideViewModel()
            {
                SideName = TeamsStatsConst.AWAY,
                SideLabel = TeamsStatsConst.AWAYLabel,
                Stats = new List<TeamStatViewModel>()
            });

            List<TeamsNBA> teams = this._appDataRepository.GetAllTeamsNBA().ToList();

            foreach (TeamsNBA team in teams)
            {
                #region Both

                TeamStatViewModel both = new TeamStatViewModel();

                both.TeamName = team.Abbreviation;
                both.GamesInSeason = team.GamesInSeason;
                both.SU = team.BothSU;
                both.ATS = team.BothATS;
                both.OU = team.BothOU;
                both.AvgLine = team.BothAvgLine;
                both.AvgTotal = team.BothAvgTotal;
                both.AvgPoints = team.BothAvgPoints;
                both.AvgAssists = team.BothAvgAssists;
                both.AvgRebounds = team.BothAvgRebounds;
                both.AvgBlocks = team.BothAvgBlocks;
                both.AvgFouls = team.BothAvgFouls;
                both.AvgTurnovers = team.BothAvgTurnovers;

                this._teamsStatsViewModel.Sides.Single(x => x.SideName == TeamsStatsConst.BOTH).Stats.Add(both);

                #endregion

                #region Home

                TeamStatViewModel home = new TeamStatViewModel();

                home.TeamName = team.Abbreviation;
                home.GamesInSeason = team.GamesInSeason;
                home.SU = team.HomeSU;
                home.ATS = team.HomeATS;
                home.OU = team.HomeOU;
                home.AvgLine = team.HomeAvgLine;
                home.AvgTotal = team.HomeAvgTotal;
                home.AvgPoints = team.HomeAvgPoints;
                home.AvgAssists = team.HomeAvgAssists;
                home.AvgRebounds = team.HomeAvgRebounds;
                home.AvgBlocks = team.HomeAvgBlocks;
                home.AvgFouls = team.HomeAvgFouls;
                home.AvgTurnovers = team.HomeAvgTurnovers;

                this._teamsStatsViewModel.Sides.Single(x => x.SideName == TeamsStatsConst.HOME).Stats.Add(home);

                #endregion

                #region Away

                TeamStatViewModel away = new TeamStatViewModel();

                away.TeamName = team.Abbreviation;
                away.GamesInSeason = team.GamesInSeason;
                away.SU = team.AwaySU;
                away.ATS = team.AwayATS;
                away.OU = team.AwayOU;
                away.AvgLine = team.AwayAvgLine;
                away.AvgTotal = team.AwayAvgTotal;
                away.AvgPoints = team.AwayAvgPoints;
                away.AvgAssists = team.AwayAvgAssists;
                away.AvgRebounds = team.AwayAvgRebounds;
                away.AvgBlocks = team.AwayAvgBlocks;
                away.AvgFouls = team.AwayAvgFouls;
                away.AvgTurnovers = team.AwayAvgTurnovers;

                this._teamsStatsViewModel.Sides.Single(x => x.SideName == TeamsStatsConst.AWAY).Stats.Add(away);

                #endregion
            }
        }

        #endregion
    }
}
