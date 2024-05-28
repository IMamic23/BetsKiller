using Newtonsoft.Json;

namespace BetsKiller.API.Erikberg.Entities
{
    public class StatBasketball
    {
        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("position")]
        public string Position { get; set; }

        [JsonProperty("minutes")]
        public int Minutes { get; set; }

        [JsonProperty("is_starter")]
        public bool IsStarter { get; set; }

        [JsonProperty("team_abbreviation")]
        public string TeamAbbreviation { get; set; }

        [JsonProperty("points")]
        public int Points { get; set; }

        [JsonProperty("assists")]
        public int Assists { get; set; }

        [JsonProperty("turnovers")]
        public int Turnovers { get; set; }

        [JsonProperty("steals")]
        public int Steals { get; set; }

        [JsonProperty("blocks")]
        public int Blocks { get; set; }

        [JsonProperty("field_goals_attempted")]
        public int FieldGoalsAttempted { get; set; }

        [JsonProperty("field_goals_made")]
        public int FieldGoalsMade { get; set; }

        [JsonProperty("three_point_field_goals_attempted")]
        public int ThreePointFieldGoalsAttempted { get; set; }

        [JsonProperty("three_point_field_goals_made")]
        public int ThreePointFieldGoalsMade { get; set; }

        [JsonProperty("free_throws_attempted")]
        public int FreeThrowsAttempted { get; set; }

        [JsonProperty("free_throws_made")]
        public int FreeThrowsMade { get; set; }

        [JsonProperty("defensive_rebounds")]
        public int DefensiveRebounds { get; set; }

        [JsonProperty("offensive_rebounds")]
        public int OffensiveRebounds { get; set; }

        [JsonProperty("rebounds")]
        public int Rebounds { get; set; }

        [JsonProperty("personal_fouls")]
        public int PersonalFouls { get; set; }

        [JsonProperty("field_goal_percentage")]
        public float FieldGoalPercentage { get; set; }

        [JsonProperty("three_point_percentage")]
        public float ThreePointPercentage { get; set; }

        [JsonProperty("free_throw_percentage")]
        public float FreeThrowPercentage { get; set; }

        [JsonProperty("field_goal_percentage_string")]
        public string FieldGoalPercentageString { get; set; }

        [JsonProperty("three_point_percentage_string")]
        public string ThreePointPercentageString { get; set; }

        [JsonProperty("free_throw_percentage_string")]
        public string FreeThrowPercentageString { get; set; }
    }
}
