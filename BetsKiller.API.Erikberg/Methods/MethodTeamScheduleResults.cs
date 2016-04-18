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
    /// (O) team_id - The team_id is the full name of the team, all lowercase with spaces replaced by dashes (e.g., los‑angeles‑dodgers, oakland‑athletics, san‑antonio‑spurs).
    /// (O) season - Return results for specified season using yyyy format. If omitted, defaults to current season. The earliest season for MLB results is 2008.
    ///              The earliest season for NBA is 2012. NBA seasons are designated by the year in which the season ends (e.g., to return results for the 2011-2012 season, specify 2012).
    /// (O) since - Returns events that occur on or after date (i.e., inclusive) using yyyyMMdd format. This only filters dates within the specified season, it will not return events that span multiple seasons.
    ///             Example: season=2014&since=20140101 would return all events that occurred on or after January 1, 2014.
    /// (O) until - Returns events that occur before date (i.e., exclusive) using yyyyMMdd format for season specified.
    ///             Example: season=2014& since=20140101& until=20140201 would return all events that occurred in January 2014.
    /// (O) order - Possible values desc for results to be returned from latest to earliest or asc for results to be returned from earliest to latest. If omitted, defaults to desc.
    /// </summary>
    public class MethodTeamScheduleResults : Method
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
                string url = "${sport}/results/${team_id}.json";

                url = url.Replace("${sport}", this._sport.ToString());
                url = url.Replace("${team_id}", this._teamId);

                return url;
            }
        }

        #endregion

        #region Methods

        public List<TeamScheduleResults> Get(SportEnum sport, string teamId, string season, string since, string until, string order)
        {
            this._sport = sport;
            this._teamId = teamId;

            base.AddParameterToDict("season", season);
            base.AddParameterToDict("since", since);
            base.AddParameterToDict("until", until);
            base.AddParameterToDict("order", order);

            base.GetData();

            return JsonConvert.DeserializeObject<List<TeamScheduleResults>>(base.ResponseString);
        }

        #endregion
    }
}
