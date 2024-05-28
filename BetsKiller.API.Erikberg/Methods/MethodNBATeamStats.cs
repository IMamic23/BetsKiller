using BetsKiller.API.Erikberg.Entities;
using BetsKiller.API.Erikberg.Enums;
using Newtonsoft.Json;

namespace BetsKiller.API.Erikberg.Methods
{
    /// <summary>
    /// Prams:
    /// (R) sport - For which sport is query
    /// (O) date - stats accumulated through "yyyyMMdd"
    /// (O) team_id - The team_id is the full name of the team, all lowercase with spaces replaced by dashes (e.g., boston‑celtics, portland‑trail‑blazers).
    /// </summary>
    public class MethodNBATeamStats : Method
    {
        #region Private

        private SportEnum _sport;
        private string _date;

        #endregion

        #region Properties

        protected override string Url
        {
            get 
            {
                string url = "${sport}/team-stats${date}.json";

                url = url.Replace("${sport}", _sport.ToString());
                url = url.Replace("${date}", !string.IsNullOrEmpty(_date) ? "/" + _date : string.Empty);

                return url;
            }
        }

        #endregion

        #region Methods

        public NBATeamStats Get(SportEnum sport, string date, string team_id)
        {
            _sport = sport;
            _date = date;

            AddParameterToDict("team_id", team_id);

            GetData();

            return JsonConvert.DeserializeObject<NBATeamStats>(ResponseString);
        }

        #endregion
    }
}
