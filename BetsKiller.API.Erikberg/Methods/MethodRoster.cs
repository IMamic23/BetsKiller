using BetsKiller.API.Erikberg.Entities;
using BetsKiller.API.Erikberg.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.API.Erikberg.Methods
{
    /// <summary>
    /// Params:
    /// (R) sport - For which sport is query
    /// (R) team_id - The team_id is the full name of the team, all lowercase with spaces replaced by dashes (e.g., los‑angeles‑dodgers, oakland‑athletics, san‑antonio‑spurs).
    /// (O) status - To return an MLB club's 40‑man roster, use expanded. If this parameter is omitted or contains an invalid value, the active (i.e., 25‑man) roster is returned.
    /// </summary>
    public class MethodRoster : Method
    {
        #region Private

        private SportEnum _sport;
        private string _teamId;

        #endregion

        #region Properties

        protected override string Url
        {
            get
            {
                string url = "${sport}/roster/${team_id}.json";

                url = url.Replace("${sport}", this._sport.ToString());
                url = url.Replace("${team_id}", this._teamId);

                return url;
            }
        }

        #endregion

        #region Methods

        public Roster Get(SportEnum sport, string teamId, string status)
        {
            this._sport = sport;
            this._teamId = teamId;

            base.AddParameterToDict("status", status);

            base.GetData();

            return JsonConvert.DeserializeObject<Roster>(base.ResponseString);
        }

        #endregion
    }
}
