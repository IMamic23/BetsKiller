using Newtonsoft.Json;

namespace BetsKiller.API.Erikberg.Entities
{
    public class Site
    {
        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("capacity")]
        public int Capacity { get; set; }

        [JsonProperty("surface")]
        public string Surface { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
