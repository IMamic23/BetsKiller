using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
