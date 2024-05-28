using BetsKiller.API.Erikberg.Entities;
using BetsKiller.API.Erikberg.Enums;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BetsKiller.API.Erikberg.Methods
{
    /// <summary>
    /// Params:
    /// (R) sport - For which sport is query.
    /// (O) season - For which season is query (format yyyy).
    /// (O) team_id - Query for specified team (team_id from Teams).
    /// </summary>
    public class MethodNBADraft : Method
    {
        #region Private

        private SportEnum _sport;

        #endregion

        #region Properties

        protected override string Url
        {
            get
            {
                string url = "${sport}/draft.json";

                url = url.Replace("${sport}", _sport.ToString());

                return url;
            }
        }

        #endregion

        #region Methods

        public List<NBADraft> Get(SportEnum sport, string season, string team_id)
        {
            _sport = sport;

            AddParameterToDict("season", season);
            AddParameterToDict("team_id", team_id);

            GetData();

            return JsonConvert.DeserializeObject<List<NBADraft>>(ResponseString);
        }

        #endregion
    }
}
