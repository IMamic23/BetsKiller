using Newtonsoft.Json;
using System.Collections.Generic;

namespace BetsKiller.API.Erikberg.Entities
{
    public class Roster
    {
        [JsonProperty("team")]
        public Team Team { get; set; }

        [JsonProperty("players")]
        public List<Player> Players { get; set; }
    }
}
