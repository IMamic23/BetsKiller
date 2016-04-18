using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BetsKiller.Model;

namespace BetsKiller.DAL.AppData
{
    public class AppDataInitializer : CreateDatabaseIfNotExists<AppDataContext>
    {
        #region Protected

        /// <summary>
        /// Adding test data when DB is creating.
        /// </summary>
        protected override void Seed(AppDataContext context)
        {
            #region Sport

            context.Sport.AddRange(new List<Sport>()
            {
                new Sport()
                {
                    Id = 1,
                    Name = "nba"
                },
                new Sport()
                {
                    Id = 2,
                    Name = "mlb"
                }
            });

            #endregion

            #region BetType
            
            context.BetType.AddRange(new List<BetType>()
            {
                new BetType()
                {
                    Id = 1,
                    Name = "total - over"
                },
                new BetType()
                {
                    Id = 2,
                    Name = "total - under"
                },
                new BetType()
                {
                    Id = 3,
                    Name = "team"
                },
                new BetType()
                {
                    Id = 4,
                    Name = "team - home - handicap"
                },
                new BetType()
                {
                    Id = 5,
                    Name = "team - away - handicap"
                }
            });

            #endregion

            #region BetStatus

            context.BetStatus.AddRange(new List<BetStatus>()
            {
                new BetStatus()
                {
                    Id = 1,
                    Name = "WIN"
                },
                new BetStatus()
                {
                    Id = 2,
                    Name = "PUSH"
                },
                new BetStatus()
                {
                    Id = 3,
                    Name = "LOSS"
                },
                new BetStatus()
                {
                    Id = 4,
                    Name = "SCHEDULED"
                }
            });

            #endregion

            #region AnalyseType

            context.AnalyseType.AddRange(new List<AnalyseType>()
            {
                new AnalyseType()
                {
                    Id = 1,
                    Name = "free"
                },
                new AnalyseType()
                {
                    Id = 2,
                    Name = "paid"
                }
            });

            #endregion

            #region LeadersNBACategories

            context.LeadersNBACategories.AddRange(new List<LeadersNBACategories>()
            {
                new LeadersNBACategories()
                {
                    Id = 1,
                    Name = "Points Per Game",
                    NameErikberg = "points_per_game"
                },
                new LeadersNBACategories()
                {
                    Id = 2,
                    Name = "Assists Per Game",
                    NameErikberg = "assists_per_game"
                },
                new LeadersNBACategories()
                {
                    Id = 3,
                    Name = "Rebounds Per Game",
                    NameErikberg = "rebounds_per_game"
                },
                new LeadersNBACategories()
                {
                    Id = 4,
                    Name = "Field Goal %",
                    NameErikberg = "field_goal_pct"
                },
                new LeadersNBACategories()
                {
                    Id = 5,
                    Name = "Free Throw %",
                    NameErikberg = "free_throw_pct"
                },
                new LeadersNBACategories()
                {
                    Id = 6,
                    Name = "3-Point Field Goal %",
                    NameErikberg = "three_point_pct"
                },
                new LeadersNBACategories()
                {
                    Id = 7,
                    Name = "Offensive Rebounds Per Game",
                    NameErikberg = "off_rebounds_per_game"
                },
                new LeadersNBACategories()
                {
                    Id = 8,
                    Name = "Defensive Rebounds Per Game",
                    NameErikberg = "def_rebounds_per_game"
                },
                new LeadersNBACategories()
                {
                    Id = 9,
                    Name = "Blocks Per Game",
                    NameErikberg = "blocks_per_game"
                },
                new LeadersNBACategories()
                {
                    Id = 10,
                    Name = "Steals Per Game",
                    NameErikberg = "steals_per_game"
                },
                new LeadersNBACategories()
                {
                    Id = 11,
                    Name = "Assists to Turnovers",
                    NameErikberg = "assists_to_turnovers_per_game"
                },
                new LeadersNBACategories()
                {
                    Id = 12,
                    Name = "Steals to Turnovers",
                    NameErikberg = "steals_to_turnovers_per_game"
                },
                new LeadersNBACategories()
                {
                    Id = 13,
                    Name = "Minutes Per Game",
                    NameErikberg = "minutes_per_game"
                },
                new LeadersNBACategories()
                {
                    Id = 14,
                    Name = "Games Played",
                    NameErikberg = "games_played"
                },
                new LeadersNBACategories()
                {
                    Id = 15,
                    Name = "Double Doubles",
                    NameErikberg = "double_doubles"
                },
                new LeadersNBACategories()
                {
                    Id = 16,
                    Name = "Triple Doubles",
                    NameErikberg = "triple_doubles"
                }
            });

            #endregion

            #region TeamsNBANames

            context.TeamsNBANames.AddRange(new List<TeamsNBANames>()
            {
                new TeamsNBANames()
                {
                    Id = 1,
                    Name = "Washington Wizards",
                    NameErikberg = "washington-wizards",
                    NameSportsdatabase = "Wizards",
                    NameRotoworld = "Washington Wizards",
                    NameNBAcom = "Washington"
                },
                new TeamsNBANames()
                {
                    Id = 2,
                    Name = "Golden State Warriors",
                    NameErikberg = "golden-state-warriors",
                    NameSportsdatabase = "Warriors",
                    NameRotoworld = "Golden State Warriors",
                    NameNBAcom = "Golden state"
                },
                new TeamsNBANames()
                {
                    Id = 3,
                    Name = "Portland Trail Blazers",
                    NameErikberg = "portland-trail-blazers",
                    NameSportsdatabase = "Trailblazers",
                    NameRotoworld = "Portland Trail Blazers",
                    NameNBAcom = "Portland"
                },
                new TeamsNBANames()
                {
                    Id = 4,
                    Name = "Minnesota Timberwolves",
                    NameErikberg = "minnesota-timberwolves",
                    NameSportsdatabase = "Timberwolves",
                    NameRotoworld = "Minnesota Timberwolves",
                    NameNBAcom = "Minnesota"
                },
                new TeamsNBANames()
                {
                    Id = 5,
                    Name = "Oklahoma City Thunder",
                    NameErikberg = "oklahoma-city-thunder",
                    NameSportsdatabase = "Thunder",
                    NameRotoworld = "Oklahoma City Thunder",
                    NameNBAcom = "Oklahoma City"
                },
                new TeamsNBANames()
                {
                    Id = 6,
                    Name = "Phoenix Suns",
                    NameErikberg = "phoenix-suns",
                    NameSportsdatabase = "Suns",
                    NameRotoworld = "Phoenix Suns",
                    NameNBAcom = "Phoenix"
                },
                new TeamsNBANames()
                {
                    Id = 7,
                    Name = "San Antonio Spurs",
                    NameErikberg = "san-antonio-spurs",
                    NameSportsdatabase = "Spurs",
                    NameRotoworld = "San Antonio Spurs",
                    NameNBAcom = "San Antonio"
                },
                new TeamsNBANames()
                {
                    Id = 8,
                    Name = "Philadelphia 76ers",
                    NameErikberg = "philadelphia-76ers",
                    NameSportsdatabase = "Seventysixers",
                    NameRotoworld = "Philadelphia 76ers",
                    NameNBAcom = "Philadelphia"
                },
                new TeamsNBANames()
                {
                    Id = 9,
                    Name = "Houston Rockets",
                    NameErikberg = "houston-rockets",
                    NameSportsdatabase = "Rockets",
                    NameRotoworld = "Houston Rockets",
                    NameNBAcom = "Houston"
                },
                new TeamsNBANames()
                {
                    Id = 10,
                    Name = "Toronto Raptors",
                    NameErikberg = "toronto-raptors",
                    NameSportsdatabase = "Raptors",
                    NameRotoworld = "Toronto Raptors",
                    NameNBAcom = "Toronto"
                },
                new TeamsNBANames()
                {
                    Id = 11,
                    Name = "Detroit Pistons",
                    NameErikberg = "detroit-pistons",
                    NameSportsdatabase = "Pistons",
                    NameRotoworld = "Detroit Pistons",
                    NameNBAcom = "Detroit"
                },
                new TeamsNBANames()
                {
                    Id = 12,
                    Name = "New Orleans Pelicans",
                    NameErikberg = "new-orleans-pelicans",
                    NameSportsdatabase = "Pelicans",
                    NameRotoworld = "New Orleans Pelicans",
                    NameNBAcom = "New Orleans"
                },
                new TeamsNBANames()
                {
                    Id = 13,
                    Name = "Indiana Pacers",
                    NameErikberg = "indiana-pacers",
                    NameSportsdatabase = "Pacers",
                    NameRotoworld = "Indiana Pacers",
                    NameNBAcom = "Indiana"
                },
                new TeamsNBANames()
                {
                    Id = 14,
                    Name = "Denver Nuggets",
                    NameErikberg = "denver-nuggets",
                    NameSportsdatabase = "Nuggets",
                    NameRotoworld = "Denver Nuggets",
                    NameNBAcom = "Denver"
                },
                new TeamsNBANames()
                {
                    Id = 15,
                    Name = "Brooklyn Nets",
                    NameErikberg = "brooklyn-nets",
                    NameSportsdatabase = "Nets",
                    NameRotoworld = "Brooklyn Nets",
                    NameNBAcom = "Brooklyn"
                },
                new TeamsNBANames()
                {
                    Id = 16,
                    Name = "Dallas Mavericks",
                    NameErikberg = "dallas-mavericks",
                    NameSportsdatabase = "Mavericks",
                    NameRotoworld = "Dallas Mavericks",
                    NameNBAcom = "Dallas"
                },
                new TeamsNBANames()
                {
                    Id = 17,
                    Name = "Orlando Magic",
                    NameErikberg = "orlando-magic",
                    NameSportsdatabase = "Magic",
                    NameRotoworld = "Orlando Magic",
                    NameNBAcom = "Orlando"
                },
                new TeamsNBANames()
                {
                    Id = 18,
                    Name = "Los Angeles Lakers",
                    NameErikberg = "los-angeles-lakers",
                    NameSportsdatabase = "Lakers",
                    NameRotoworld = "Los Angeles Lakers",
                    NameNBAcom = "L.A. Lakers"
                },
                new TeamsNBANames()
                {
                    Id = 19,
                    Name = "New York Knicks",
                    NameErikberg = "new-york-knicks",
                    NameSportsdatabase = "Knicks",
                    NameRotoworld = "New York Knicks",
                    NameNBAcom = "New York"
                },
                new TeamsNBANames()
                {
                    Id = 20,
                    Name = "Sacramento Kings",
                    NameErikberg = "sacramento-kings",
                    NameSportsdatabase = "Kings",
                    NameRotoworld = "Sacramento Kings",
                    NameNBAcom = "Sacramento"
                },
                new TeamsNBANames()
                {
                    Id = 21,
                    Name = "Utah Jazz",
                    NameErikberg = "utah-jazz",
                    NameSportsdatabase = "Jazz",
                    NameRotoworld = "Utah Jazz",
                    NameNBAcom = "Utah"
                },
                new TeamsNBANames()
                {
                    Id = 22,
                    Name = "Charlotte Hornets",
                    NameErikberg = "charlotte-hornets",
                    NameSportsdatabase = "Hornets",
                    NameRotoworld = "Charlotte Hornets",
                    NameNBAcom = "Charlotte"
                },
                new TeamsNBANames()
                {
                    Id = 23,
                    Name = "Miami Heat",
                    NameErikberg = "miami-heat",
                    NameSportsdatabase = "Heat",
                    NameRotoworld = "Miami Heat",
                    NameNBAcom = "Miami"
                },
                new TeamsNBANames()
                {
                    Id = 24,
                    Name = "Atlanta Hawks",
                    NameErikberg = "atlanta-hawks",
                    NameSportsdatabase = "Hawks",
                    NameRotoworld = "Atlanta Hawks",
                    NameNBAcom = "Atlanta"
                },
                new TeamsNBANames()
                {
                    Id = 25,
                    Name = "Memphis Grizzlies",
                    NameErikberg = "memphis-grizzlies",
                    NameSportsdatabase = "Grizzlies",
                    NameRotoworld = "Memphis Grizzlies",
                    NameNBAcom = "Memphis"
                },
                new TeamsNBANames()
                {
                    Id = 26,
                    Name = "Los Angeles Clippers",
                    NameErikberg = "los-angeles-clippers",
                    NameSportsdatabase = "Clippers",
                    NameRotoworld = "Los Angeles Clippers",
                    NameNBAcom = "L.A. Clippers"
                },
                new TeamsNBANames()
                {
                    Id = 27,
                    Name = "Boston Celtics",
                    NameErikberg = "boston-celtics",
                    NameSportsdatabase = "Celtics",
                    NameRotoworld = "Boston Celtics",
                    NameNBAcom = "Boston"
                },
                new TeamsNBANames()
                {
                    Id = 28,
                    Name = "Cleveland Cavaliers",
                    NameErikberg = "cleveland-cavaliers",
                    NameSportsdatabase = "Cavaliers",
                    NameRotoworld = "Cleveland Cavaliers",
                    NameNBAcom = "Cleveland"
                },
                new TeamsNBANames()
                {
                    Id = 29,
                    Name = "Chicago Bulls",
                    NameErikberg = "chicago-bulls",
                    NameSportsdatabase = "Bulls",
                    NameRotoworld = "Chicago Bulls",
                    NameNBAcom = "Chicago"
                },
                new TeamsNBANames()
                {
                    Id = 30,
                    Name = "Milwaukee Bucks",
                    NameErikberg = "milwaukee-bucks",
                    NameSportsdatabase = "Bucks",
                    NameRotoworld = "Milwaukee Bucks",
                    NameNBAcom = "Milwaukee"
                },
            });

            #endregion

            base.Seed(context);
        }

        #endregion
    }
}
