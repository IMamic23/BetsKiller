using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.API.Rotoworld.Entities
{
    public class Injury
    {
        public string TeamName { get; set; }

        public string PlayerName { get; set; }

        public string PlayerPosition { get; set; }

        public string PlayerStatus { get; set; }

        public string Date { get; set; }

        public string Type { get; set; }

        public string Returns { get; set; }

        public string Report { get; set; }

        public string ReportUpdateDate { get; set; }
    }
}
