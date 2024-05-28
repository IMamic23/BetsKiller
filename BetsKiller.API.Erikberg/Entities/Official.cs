using Newtonsoft.Json;

namespace BetsKiller.API.Erikberg.Entities
{
    public class Official
    {
        [JsonProperty("position")]
        public string Position { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }
    }
}
