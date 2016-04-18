using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.API.SportsDatabase.Entities
{
    public class TeamStatAverage
    {
        public string TeamName { get; set; }

        public string AvgPoints { get; set; }
        public string AvgAssists { get; set; }
        public string AvgRebounds { get; set; }
        public string AvgBlocks { get; set; }
        public string AvgFouls { get; set; }
        public string AvgTurnovers { get; set; }
    }
}
