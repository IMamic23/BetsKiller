using BetsKiller.API.Erikberg.Entities;
using BetsKiller.API.Erikberg.Enums;
using Newtonsoft.Json;

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

                url = url.Replace("${sport}", _sport.ToString());
                url = url.Replace("${event_id}", _eventId);

                return url;
            }
        }

        #endregion

        #region Methods

        public NBABoxScore Get(SportEnum sport, string eventId)
        {
            _sport = sport;
            _eventId = eventId;

            GetData();

            return JsonConvert.DeserializeObject<NBABoxScore>(ResponseString);
        }

        #endregion
    }
}
