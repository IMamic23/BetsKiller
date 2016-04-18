using BetsKiller.API.Erikberg.Entities;
using BetsKiller.API.Erikberg.Enums;
using BetsKiller.API.Erikberg.Methods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.API.Erikberg._Examples
{
    public class UseMethods
    {
        public static void Start()
        {
            MethodMe methodMe = new MethodMe();
            Me me = methodMe.Get();

            MethodTeams methodTeams = new MethodTeams();
            List<Team> listTeams = methodTeams.Get(SportEnum.nba);

            MethodTeamScheduleResults methodTeamScheduleResults = new MethodTeamScheduleResults();
            List<TeamScheduleResults> listTeamScheduleResults = methodTeamScheduleResults.Get(SportEnum.nba, "boston-celtics", null, null, null, null);

            MethodRoster methodRoster = new MethodRoster();
            Roster roster = methodRoster.Get(SportEnum.nba, "san-antonio-spurs", null);

            MethodStandings methodStandings = new MethodStandings();
            Standings standings = methodStandings.Get(SportEnum.nba, null);

            MethodNBATeamStats methodNbaTeamStats = new MethodNBATeamStats();
            NBATeamStats teamStats = methodNbaTeamStats.Get(SportEnum.nba, null, "boston-celtics");

            MethodNBALeaders methodNbaLeaders = new MethodNBALeaders();
            List<NBALeader> leaders = methodNbaLeaders.Get(SportEnum.nba, "assists_per_game", null, null, null);

            MethodNBABoxScore methodBoxScore = new MethodNBABoxScore();
            NBABoxScore boxScore = methodBoxScore.Get(SportEnum.nba, "20120621-oklahoma-city-thunder-at-miami-heat");

            MethodEvents methodEvents = new MethodEvents();
            Events events = methodEvents.Get("20151215", "nba");

            MethodNBADraft methodNbaDraft = new MethodNBADraft();
            List<NBADraft> nbaDraft = methodNbaDraft.Get(SportEnum.nba, null, null);
        }
    }
}
