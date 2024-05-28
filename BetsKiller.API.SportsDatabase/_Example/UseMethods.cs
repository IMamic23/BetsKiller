using BetsKiller.API.SportsDatabase.Entities;
using BetsKiller.API.SportsDatabase.Enums;
using BetsKiller.API.SportsDatabase.Methods;
using System.Collections.Generic;

namespace BetsKiller.API.SportsDatabase._Example
{
    public class UseMethods
    {
        public static void Start()
        {
            MethodTeamsStats methodTeamStats = new MethodTeamsStats(SportEnum.nba, PlayingSideEnum.Both, "2015");
            List<TeamStat> teamStats = methodTeamStats.Get();
        }
    }
}
