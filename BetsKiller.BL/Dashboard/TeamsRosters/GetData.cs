using BetsKiller.DAL;
using BetsKiller.DAL.AppData;
using BetsKiller.Helper.Types;
using BetsKiller.Model;
using BetsKiller.ViewModel.Dashboard.TeamsRosters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace BetsKiller.BL.Dashboard.TeamsRosters
{
    public class GetData : ProcessBase
    {
        #region Private

        private TeamsRostersViewModel _teamsRostersViewModel;

        private IAppDataRepository _appDataRepository;

        #endregion

        #region Properties

        public TeamsRostersViewModel TeamsRostersViewModel
        {
            get { return this._teamsRostersViewModel; }
        }

        #endregion

        #region Properties - override

        protected override string _successMessage
        {
            get { return "Teams and rosters data retrived successfully."; }
        }

        protected override string _failMessage
        {
            get { return "Teams and rosters data retrive failed."; }
        }

        #endregion

        #region Constructors

        public GetData()
        {
            this._appDataRepository = new AppDataRepository();
        }

        #endregion

        #region Methods

        protected override void Process()
        {
            this._teamsRostersViewModel = new TeamsRostersViewModel();

            // Get and parse NBA teams and rosters
            this.GetParseTeamsRostersNBA();
        }

        #endregion

        #region Helper methods

        private void GetParseTeamsRostersNBA()
        {
            List<TeamsNBA> nbaTeams = this._appDataRepository.GetAllTeamsNBA().ToList();
            List<RostersNBA> nbaRosters = this._appDataRepository.GetAllRostersNBA().ToList();

            this._teamsRostersViewModel.Teams = nbaTeams.Select(x => x.Abbreviation).ToList();
            this._teamsRostersViewModel.Rosters = new List<TeamsRostersTeamViewModel>();

            foreach (TeamsNBA team in nbaTeams)
            {
                TeamsRostersTeamViewModel teamVM = new TeamsRostersTeamViewModel();
                teamVM.TeamName = team.Abbreviation;
                teamVM.Players = new List<TeamsRostersPlayerViewModel>();

                foreach (RostersNBA player in nbaRosters.Where(x => x.Team.Id == team.Id))
                {
                    TeamsRostersPlayerViewModel playerVM = new TeamsRostersPlayerViewModel();
                    playerVM.UniformNumber = player.UniformNumber == -1 ? string.Empty : player.UniformNumber.ToString();
                    playerVM.Position = player.Position;
                    playerVM.Name = player.FirstName + " " + player.LastName;
                    playerVM.Birthday = TypeDateTime.ParseDateTime(player.Birthday);
                    playerVM.Age = player.Age.ToString();
                    playerVM.Birthplace = player.Birthplace;
                    playerVM.Height = player.HeightFormatted + " / " + Math.Round(player.HeightM, 2).ToString(CultureInfo.InvariantCulture) + " m";
                    playerVM.Weight = Math.Round(player.WeightLb, 0).ToString(CultureInfo.InvariantCulture) + " lb / " + Math.Round(player.WeightKg, 2).ToString(CultureInfo.InvariantCulture) + " kg";

                    teamVM.Players.Add(playerVM);
                }

                this._teamsRostersViewModel.Rosters.Add(teamVM);
            }
        }

        #endregion
    }
}
