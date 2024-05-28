using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace BetsKiller.API.Erikberg.Entities
{
    public class Event
    {
        [JsonProperty("event_id")]
        public string EventId { get; set; }

        [JsonProperty("event_status")]
        public string EventStatus { get; set; }

        [JsonProperty("sport")]
        public string Sport { get; set; }

        [JsonProperty("start_date_time")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime StartDateTime { get; set; }

        [JsonProperty("season_type")]
        public string SeasonType { get; set; }

        [JsonProperty("away_team")]
        public Team AwayTeam { get; set; }

        [JsonProperty("home_team")]
        public Team HomeTeam { get; set; }

        [JsonProperty("site")]
        public Site Site { get; set; }

        [JsonProperty("away_period_scores")]
        public int[] AwayPeriodScores { get; set; }

        [JsonProperty("home_period_scores")]
        public int[] HomePeriodScores { get; set; }

        [JsonProperty("away_points_scored")]
        public int AwayPointsScored { get; set; }

        [JsonProperty("home_points_scored")]
        public int HomePointsScored { get; set; }
    }
}
