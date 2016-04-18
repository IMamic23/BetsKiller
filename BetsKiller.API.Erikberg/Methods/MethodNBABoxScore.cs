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
    /// (R) event_id - The event_id is a string that uniquely identifies a box score. The format is yyyyMMdd-awayTeamId-at-homeTeamId.
    /// </summary>
    public class MethodNBABoxScore : Method
    {
        #region Private

        private SportEnum _sport;
        private string _eventId;

        #endregion

        #region Properties

        protected override string Url
        {
            get
            {
                string url = "${sport}/boxscore/${event_id}.json";

                url = url.Replace("${sport}", this._sport.ToString());
                url = url.Replace("${event_id}", this._eventId);

                return url;
            }
        }

        #endregion

        #region Methods

        public NBABoxScore Get(SportEnum sport, string eventId)
        {
            this._sport = sport;
            this._eventId = eventId;

            base.GetData();

            return JsonConvert.DeserializeObject<NBABoxScore>(base.ResponseString);
        }

        #endregion
    }
}
