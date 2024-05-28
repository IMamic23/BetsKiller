using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace BetsKiller.API.Erikberg.Entities
{
    public class Standings
    {
        [JsonProperty("standings_date")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime StandingsDate { get; set; }

        [JsonProperty("standing")]
        public List<Standing> Standing { get; set; }
    }
}
