using Newtonsoft.Json;

namespace BetsKiller.API.Erikberg.Entities
{
    public class NBATeamStat
    {
        [JsonProperty("team")]
        public Team Team { get; set; }

        [JsonProperty("games_played")]
        public int GamesPlayed { get; set; }

        [JsonProperty("won")]
        public int Won { get; set; }

        [JsonProperty("lost")]
        public int Lost { get; set; }

        [JsonProperty("stats")]
        public TeamStatBasketball BasketballStats { get; set; }

        [JsonProperty("stats_opponent")]
        public TeamStatBasketball BasketballStatsOpponent { get; set; }
    }
}
