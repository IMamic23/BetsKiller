using BetsKiller.API.Erikberg.Entities;
using BetsKiller.API.Erikberg.Enums;
using Newtonsoft.Json;

namespace BetsKiller.API.Erikberg.Methods
{
    /// <summary>
    /// Params:
    /// (R) sport - For which sport is query
    /// (O) date - This method returns the current standings table or, if specified, the standings table on yyyyMMdd (Eastern time). 
    ///            Currently, the earliest standings table for MLB is 2008‑03‑25, the earliest for NBA is 2011‑12‑25.
    /// </summary>
    public class MethodStandings : Method
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
                string url = "${sport}/standings${date}.json";

                url = url.Replace("${sport}", _sport.ToString());
                url = url.Replace("${date}", !string.IsNullOrEmpty(_date) ? "/" + _date : string.Empty);

                return url;
            }
        }

        #endregion

        #region Methods

        public Standings Get(SportEnum sport, string date)
        {
            _sport = sport;
            _date = date;

            GetData();

            return JsonConvert.DeserializeObject<Standings>(ResponseString);
        }

        #endregion
    }
}
