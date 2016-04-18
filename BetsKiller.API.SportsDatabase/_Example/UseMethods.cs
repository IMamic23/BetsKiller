using BetsKiller.API.SportsDatabase.Entities;
using BetsKiller.API.SportsDatabase.Enums;
using BetsKiller.API.SportsDatabase.Methods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
