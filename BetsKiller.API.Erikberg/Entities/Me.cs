using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.API.Erikberg.Entities
{
    public class Me
    {
        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("email_address")]
        public string EmailAddress { get; set; }

        [JsonProperty("created_on")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime CreatedOn { get; set; }

        [JsonProperty("token_expiration_seconds")]
        public int TokenExpirationSeconds { get; set; }

        [JsonProperty("token_expiration_date")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime TokenExpirationDate { get; set; }

        [JsonProperty("token_time_remaining")]
        public string TokenTimeRemaining { get; set; }
    }
}
