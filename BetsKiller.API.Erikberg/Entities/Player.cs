using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.API.Erikberg.Entities
{
    public class Player
    {
        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("birthdate")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime Birthday { get; set; }

        [JsonProperty("age")]
        public int Age { get; set; }

        [JsonProperty("birthplace")]
        public string Birthplace { get; set; }

        [JsonProperty("position")]
        public string Position { get; set; }

        [JsonProperty("height_in")]
        public float HeightIn { get; set; }

        [JsonProperty("height_cm")]
        public float HeightCm { get; set; }

        [JsonProperty("height_m")]
        public float HeightM { get; set; }

        [JsonProperty("weight_lb")]
        public float WeightLb { get; set; }

        [JsonProperty("weight_kg")]
        public float WeightKg { get; set; }

        [JsonProperty("height_formatted")]
        public string HeightFormatted { get; set; }

        [JsonProperty("uniform_number")]
        public int UniformNumber { get; set; }

        [JsonProperty("roster_status")]
        public string RosterStatus { get; set; }
    }
}
