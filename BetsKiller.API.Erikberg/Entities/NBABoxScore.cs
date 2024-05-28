using Newtonsoft.Json;
using System.Collections.Generic;

namespace BetsKiller.API.Erikberg.Entities
{
    public class NBABoxScore
    {
        [JsonProperty("away_team")]
        public Team AwayTeam { get; set; }

        [JsonProperty("home_team")]
        public Team HomeTeam { get; set; }

        [JsonProperty("away_period_scores")]
        public int[] AwayPeriodScores { get; set; }

        [JsonProperty("home_period_scores")]
        public int[] HomePeriodScores { get; set; }

        [JsonProperty("away_stats")]
        public List<StatBasketball> AwayStats { get; set; }

        [JsonProperty("home_stats")]
        public List<StatBasketball> HomeStats { get; set; }

        [JsonProperty("officials")]
        public List<Official> Officials { get; set; }

        [JsonProperty("event_information")]
        public EventInformation EventInformation { get; set; }

        [JsonProperty("away_totals")]
        public StatBasketball AwayTotals { get; set; }

        [JsonProperty("home_totals")]
        public StatBasketball HomeTotals { get; set; }
    }
}
