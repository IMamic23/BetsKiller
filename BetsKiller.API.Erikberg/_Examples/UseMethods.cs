using BetsKiller.API.Erikberg.Enums;
using BetsKiller.API.Erikberg.Methods;

namespace BetsKiller.API.Erikberg._Examples
{
    public class UseMethods
    {
        public static void Start()
        {
            var methodMe = new MethodMe();
            var me = methodMe.Get();

            var methodTeams = new MethodTeams();
            var listTeams = methodTeams.Get(SportEnum.nba);

            var methodTeamScheduleResults = new MethodTeamScheduleResults();
            var listTeamScheduleResults = methodTeamScheduleResults
                .Get(SportEnum.nba, "boston-celtics", null, null, null, null);

            var methodRoster = new MethodRoster();
            var roster = methodRoster.Get(SportEnum.nba, "san-antonio-spurs", null);

            var methodStandings = new MethodStandings();
            var standings = methodStandings.Get(SportEnum.nba, null);

            var methodNbaTeamStats = new MethodNBATeamStats();
            var teamStats = methodNbaTeamStats.Get(SportEnum.nba, null, "boston-celtics");

            var methodNbaLeaders = new MethodNBALeaders();
            var leaders = methodNbaLeaders.Get(SportEnum.nba, "assists_per_game", null, null, null);

            var methodBoxScore = new MethodNBABoxScore();
            var boxScore = methodBoxScore.Get(SportEnum.nba, "20120621-oklahoma-city-thunder-at-miami-heat");

            var methodEvents = new MethodEvents();
            var events = methodEvents.Get("20151215", "nba");

            var methodNbaDraft = new MethodNBADraft();
            var nbaDraft = methodNbaDraft.Get(SportEnum.nba, null, null);
        }
    }
}
