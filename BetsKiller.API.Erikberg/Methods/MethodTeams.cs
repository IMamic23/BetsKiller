using BetsKiller.API.Erikberg.Entities;
using BetsKiller.API.Erikberg.Enums;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BetsKiller.API.Erikberg.Methods
{
    /// <summary>
    /// Params:
    /// (R) sport - For which sport is query
    /// </summary>
    public class MethodTeams : Method
    {
        #region Private

        private SportEnum _sport;

        #endregion

        #region Properties

        protected override string Url
        {
            get
            {
                string url = "${sport}/teams.json";

                url = url.Replace("${sport}", _sport.ToString());

                return url;
            }
        }

        #endregion

        #region Methods

        public List<Team> Get(SportEnum sport)
        {
            _sport = sport;

            GetData();

            return JsonConvert.DeserializeObject<List<Team>>(ResponseString);
        }

        #endregion
    }
}
