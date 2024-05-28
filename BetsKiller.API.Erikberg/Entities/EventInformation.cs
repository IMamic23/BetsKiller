using Newtonsoft.Json;

namespace BetsKiller.API.Erikberg.Entities
{
    public class EventInformation
    {
        [JsonProperty("temperature")]
        public int Temperature { get; set; }

        [JsonProperty("site")]
        public Site Site { get; set; }

        [JsonProperty("attendance")]
        public int Attendance { get; set; }

        [JsonProperty("duration")]
        public string Duration { get; set; }

        [JsonProperty("season_type")]
        public string SeasonType { get; set; }

        [JsonProperty("start_date_time")]
        public string StartDateTime { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
