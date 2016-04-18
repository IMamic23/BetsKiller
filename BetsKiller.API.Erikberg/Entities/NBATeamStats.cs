using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.API.Erikberg.Entities
{
    public class NBATeamStats
    {
        [JsonProperty("sport")]
        public string Sport { get; set; }

        [JsonProperty("team_stats_date")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime TeamStatsDate { get; set; }

        [JsonProperty("team_stats")]
        public List<NBATeamStat> TeamsStats { get; set; }
    }
}
