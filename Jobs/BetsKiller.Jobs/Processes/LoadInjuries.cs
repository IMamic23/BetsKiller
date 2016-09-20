using BetsKiller.DAL;
using BetsKiller.DAL.AppData;
using BetsKiller.Helper.Constants;
using BetsKiller.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.Jobs.Processes
{
    public class LoadInjuries : Load
    {
        #region Private

        private List<Injuries> _injuries;

        private List<Sport> _sports;
        private List<TeamsNBA> _teamsNba;

        #endregion

        #region Methods

        public void Start()
        {
            // Get sports and teams NBA
            this._sports = base.GetSports();
            this._teamsNba = base.GetTeamsNBA();

            // Get NBA injuries
            this.GetParseNBAInjuries();

            // Save injuries in DB
            this.SaveInjuriesInDB();
        }

        #endregion

        #region Helper methods

        private void GetParseNBAInjuries()
        {
            this._injuries = new List<Injuries>();

            // Get nba sport
            Sport nbaSport = this._sports.Single(x => x.Name == SportConst.NBA);

            // Get injuries
            BetsKiller.API.Rotoworld.Methods.MethodInjuries methodInjuries = new BetsKiller.API.Rotoworld.Methods.MethodInjuries();
            List<BetsKiller.API.Rotoworld.Entities.Injury> dataInjuries = methodInjuries.Get();

            foreach (BetsKiller.API.Rotoworld.Entities.Injury dataInjury in dataInjuries)
            {
                Injuries injury = new Injuries();

                injury.PlayerName = dataInjury.PlayerName;
                injury.PlayerPosition = dataInjury.PlayerPosition;
                injury.PlayerStatus = dataInjury.PlayerStatus;
                injury.Date = dataInjury.Date;
                injury.Type = dataInjury.Type;
                injury.Returns = dataInjury.Returns;
                injury.Report = dataInjury.Report;
                injury.ReportUpdateDate = dataInjury.ReportUpdateDate;

                injury.Sport_Id = nbaSport.Id;

                injury.TeamNBA_Id = this._teamsNba.Single(x => x.Name.NameRotoworld == dataInjury.TeamName).Id;

                this._injuries.Add(injury);
            }

        }

        private void SaveInjuriesInDB()
        {
            // Clear injuries
            base.AppDataRepository.ClearInjuries();

            // Save injuries to the DB
            base.AppDataRepository.SaveInjuries(this._injuries);
        }

        #endregion
    }
}
