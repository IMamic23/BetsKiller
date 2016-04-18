using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.API.Erikberg.Entities
{
    public class Events
    {
        [JsonProperty("events_date")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime EventsDate { get; set; }

        [JsonProperty("event")]
        public List<Event> Event { get; set; }
    }
}
