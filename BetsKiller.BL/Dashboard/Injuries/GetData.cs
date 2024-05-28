using BetsKiller.DAL;
using BetsKiller.DAL.AppData;
using BetsKiller.Helper.Constants;
using BetsKiller.Model;
using BetsKiller.ViewModel.Dashboard.Injuries;
using System.Collections.Generic;
using System.Linq;

namespace BetsKiller.BL.Dashboard.Injuries
{
    public class GetData : ProcessBase
    {
        #region Private

        private InjuriesViewModel _injuriesViewModel;

        private IAppDataRepository _appDataRepository;

        #endregion

        #region Properties

        public InjuriesViewModel InjuriesViewModel
        {
            get { return this._injuriesViewModel; }
        }

        #endregion

        #region Properties - override

        protected override string _successMessage
        {
            get { return "Injuries data retrived successfully."; }
        }

        protected override string _failMessage
        {
            get { return "Injuries data retrive failed."; }
        }

        #endregion

        #region Constructors

        public GetData()
        {
            this._injuriesViewModel = new InjuriesViewModel();
            
            this._appDataRepository = new AppDataRepository();
        }

        #endregion

        #region Methods

        protected override void Process()
        {
            // Get and parse NBA injuries
            this.GetParseInjuriesNBA();
        }

        #endregion

        #region Helper methods

        private void GetParseInjuriesNBA()
        {
            List<TeamsNBA> nbaTeams = this._appDataRepository.GetAllTeamsNBA().ToList();
            List<Model.Injuries> injuries = this._appDataRepository.GetAllInjuries().Where(x => x.Sport.Name == SportConst.NBA).ToList();

            this._injuriesViewModel.Teams = new List<InjuriesTeamViewModel>();

            foreach (TeamsNBA team in nbaTeams)
            {
                InjuriesTeamViewModel teamVM = new InjuriesTeamViewModel();
                teamVM.TeamName = team.Abbreviation;
                teamVM.Players = new List<InjuriesPlayerViewModel>();

                foreach (Model.Injuries injury in injuries.Where(x => x.TeamNBA.Id == team.Id))
                {
                    InjuriesPlayerViewModel playerVM = new InjuriesPlayerViewModel();
                    playerVM.Name = injury.PlayerName;
                    playerVM.Position = injury.PlayerPosition;
                    playerVM.Date = injury.Date;
                    playerVM.Type = injury.Type;
                    playerVM.Returns = injury.Returns;

                    teamVM.Players.Add(playerVM);
                }

                this._injuriesViewModel.Teams.Add(teamVM);
            }
        }

        #endregion
    }
}
