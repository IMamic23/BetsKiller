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

                url = url.Replace("${sport}", this._sport.ToString());
                url = url.Replace("${date}", !string.IsNullOrEmpty(this._date) ? "/" + this._date : string.Empty);

                return url;
            }
        }

        #endregion

        #region Methods

        public Standings Get(SportEnum sport, string date)
        {
            this._sport = sport;
            this._date = date;

            base.GetData();

            return JsonConvert.DeserializeObject<Standings>(base.ResponseString);
        }

        #endregion
    }
}
