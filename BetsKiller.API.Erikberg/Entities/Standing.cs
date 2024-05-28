using Newtonsoft.Json;

namespace BetsKiller.API.Erikberg.Entities
{
    public class Standing
    {
        [JsonProperty("team_id")]
        public string TeamId { get; set; }

        [JsonProperty("rank")]
        public int Rank { get; set; }

        [JsonProperty("ordinal_rank")]
        public string OrdinalRank { get; set; }

        [JsonProperty("won")]
        public int Won { get; set; }

        [JsonProperty("lost")]
        public int Lost { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("away_won")]
        public int AwayWon { get; set; }

        [JsonProperty("away_lost")]
        public int AwayLost { get; set; }

        [JsonProperty("conference")]
        public string Conference { get; set; }

        [JsonProperty("conference_won")]
        public int ConferenceWon { get; set; }

        [JsonProperty("conference_lost")]
        public int ConferenceLost { get; set; }

        [JsonProperty("division")]
        public string Division { get; set; }

        [JsonProperty("games_back")]
        public float GamesBack { get; set; }

        [JsonProperty("games_played")]
        public int GamesPlayed { get; set; }

        [JsonProperty("home_won")]
        public int HomeWon { get; set; }

        [JsonProperty("home_lost")]
        public int HomeLost { get; set; }

        [JsonProperty("last_five")]
        public string LastFive { get; set; }

        [JsonProperty("last_ten")]
        public string LastTen { get; set; }

        [JsonProperty("playoff_seed")]
        public int PlayoffSeed { get; set; }

        [JsonProperty("point_differential")]
        public int PointDifferential { get; set; }

        [JsonProperty("point_differential_per_game")]
        public string PointDifferentialPerGame { get; set; }

        [JsonProperty("points_against")]
        public int PointsAgainst { get; set; }

        [JsonProperty("points_for")]
        public int PointsFor { get; set; }

        [JsonProperty("points_allowed_per_game")]
        public string PointsAllowedPerGame { get; set; }

        [JsonProperty("points_scored_per_game")]
        public string PointsScoredPerGame { get; set; }

        [JsonProperty("streak_total")]
        public int StreakTotal { get; set; }

        [JsonProperty("streak")]
        public string Streak { get; set; }

        [JsonProperty("streak_type")]
        public string StreakType { get; set; }

        [JsonProperty("win_percentage")]
        public string WinPercentage { get; set; }
    }
}
