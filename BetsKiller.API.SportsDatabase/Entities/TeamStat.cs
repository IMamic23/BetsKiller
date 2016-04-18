using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.API.SportsDatabase.Entities
{
    public class TeamStat
    {
        public string TeamName { get; set; }

        public string Games { get; set; }
        public string SU { get; set; }
        public string ATS { get; set; }
        public string OU { get; set; }
        public string AvgLine { get; set; }
        public string AvgTotal { get; set; }
    }
}
