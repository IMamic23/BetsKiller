using Newtonsoft.Json;

namespace BetsKiller.API.Erikberg.Entities
{
    public class NBADraft
    {
        [JsonProperty("sport")]
        public string Sport { get; set; }

        [JsonProperty("season")]
        public string Season { get; set; }

        [JsonProperty("round")]
        public int Round { get; set; }

        [JsonProperty("pick")]
        public int Pick { get; set; }

        [JsonProperty("ordinal_pick")]
        public string OrdinalPick { get; set; }

        [JsonProperty("overall_pick")]
        public int OverallPick { get; set; }

        [JsonProperty("ordinal_overall_pick")]
        public string OrdinalOverallPick { get; set; }

        [JsonProperty("player")]
        public Player Player { get; set; }

        [JsonProperty("team")]
        public Team Team { get; set; }

        [JsonProperty("games_played")]
        public int GamesPlayed { get; set; }

        [JsonProperty("points")]
        public int Points { get; set; }

        [JsonProperty("assists")]
        public int Assists { get; set; }

        [JsonProperty("defensive_rebounds")]
        public int DefensiveRebounds { get; set; }

        [JsonProperty("offensive_rebounds")]
        public int OffensiveRebounds { get; set; }

        [JsonProperty("steals")]
        public int Steals { get; set; }

        [JsonProperty("blocks")]
        public int Blocks { get; set; }
    }
}
