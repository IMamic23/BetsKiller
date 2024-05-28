using Newtonsoft.Json;

namespace BetsKiller.API.Erikberg.Entities
{
    public class TeamStatBasketball : StatBasketball
    {
        [JsonProperty("assists_per_game_string")]
        public string AssistsPerGameString { get; set; }

        [JsonProperty("blocks_per_game_string")]
        public string BlocksPerGameString { get; set; }

        [JsonProperty("defensive_rebounds_per_game")]
        public float DefensiveReboundsPerGame { get; set; }

        [JsonProperty("defensive_rebounds_per_game_string")]
        public string DefensiveReboundsPerGameString { get; set; }

        [JsonProperty("field_goals_attempted_per_game")]
        public float FieldGoalsAttemptedPerGame { get; set; }

        [JsonProperty("field_goals_attempted_per_game_string")]
        public string FieldGoalsAttemptedPerGameString { get; set; }

        [JsonProperty("field_goals_made_per_game")]
        public float FieldGoalsMadePerGame { get; set; }

        [JsonProperty("field_goals_made_per_game_string")]
        public string FieldGoalsMadePerGameString { get; set; }

        [JsonProperty("free_throws_attempted_per_game")]
        public float FreeThrowsAttemptedPerGame { get; set; }

        [JsonProperty("free_throws_attempted_per_game_string")]
        public string FreeThrowsAttemptedPerGameString { get; set; }

        [JsonProperty("free_throws_made_per_game")]
        public float FreeThrowsMadePerGame { get; set; }

        [JsonProperty("free_throws_made_per_game_string")]
        public string FreeThrowsMadePerGameString { get; set; }

        [JsonProperty("offensive_rebounds_per_game")]
        public float OffensiveReboundsPerGame { get; set; }

        [JsonProperty("offensive_rebounds_per_game_string")]
        public string OffensiveReboundsPerGameString { get; set; }

        [JsonProperty("personal_fouls_per_game_string")]
        public string PersonalFoulsPerGameString { get; set; }

        [JsonProperty("points_per_game_string")]
        public string PointsPerGameString { get; set; }

        [JsonProperty("rebounds_per_game")]
        public float ReboundsPerGame { get; set; }

        [JsonProperty("rebounds_per_game_string")]
        public string ReboundsPerGameString { get; set; }

        [JsonProperty("steals_per_game_string")]
        public string StealsPerGameString { get; set; }

        [JsonProperty("three_point_field_goal_percentage_string")]
        public string ThreePointFieldGoalPercentageString { get; set; }

        [JsonProperty("three_point_field_goals_attempted_per_game")]
        public float ThreePointFieldGoalsAttemptedPerGame { get; set; }

        [JsonProperty("three_point_field_goals_attempted_per_game_string")]
        public string ThreePointFieldGoalsAttemptedPerGameString { get; set; }

        [JsonProperty("three_point_field_goals_made_per_game")]
        public float ThreePointFieldGoalsMadePerGame { get; set; }

        [JsonProperty("three_point_field_goals_made_per_game_string")]
        public string ThreePointFieldGoalsMadePerGameString { get; set; }

        [JsonProperty("turnovers_per_game_string")]
        public string TunroversPerGameString { get; set; }
    }
}
