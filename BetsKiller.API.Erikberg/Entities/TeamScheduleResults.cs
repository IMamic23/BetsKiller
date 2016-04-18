using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.API.Erikberg.Entities
{
    public class TeamScheduleResults
    {
        [JsonProperty("event_id")]
        public string EventId { get; set; }

        [JsonProperty("event_status")]
        public string EventStatus { get; set; }

        [JsonProperty("event_start_date_time")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime EventStartDateTime { get; set; }

        [JsonProperty("event_season_type")]
        public string EventSeasonType { get; set; }

        [JsonProperty("team_event_number_in_season")]
        public int TeamEventNumberInSeason { get; set; }

        [JsonProperty("team_event_location_type")]
        public string TeamEventLocationType { get; set; }

        [JsonProperty("team_event_result")]
        public string TeamEventResult { get; set; }

        [JsonProperty("team_points_scored")]
        public int TeamPointsScored { get; set; }

        [JsonProperty("team_events_won")]
        public int TeamEventsWon { get; set; }

        [JsonProperty("team_events_lost")]
        public int TeamEventsLost { get; set; }

        [JsonProperty("team")]
        public Team Team { get; set; }

        [JsonProperty("opponent_points_scored")]
        public int OpponentPointsScored { get; set; }

        [JsonProperty("opponent_events_won")]
        public int OpponentEventsWon { get; set; }

        [JsonProperty("opponent_events_lost")]
        public int OpponentEventsLost { get; set; }

        [JsonProperty("opponent")]
        public Team Opponent { get; set; }

        [JsonProperty("site")]
        public Site Site { get; set; }
    }
}
