using BetsKiller.Helper.HTML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.ViewModel.Dashboard.BetsTracker
{
    public class BetsTrackerGameScoreViewModel
    {
        public string Date { get; set; }

        public CustomHtmlElement Status { get; set; }

        public string HomeTeam { get; set; }

        public string AwayTeam { get; set; }

        public string HomeScore { get; set; }

        public string AwayScore { get; set; }
    }
}
