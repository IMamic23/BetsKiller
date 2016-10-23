using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.ViewModel.Dashboard.BetsTracker
{
    public class BetsTrackerBettingProfileDataViewModel
    {
        public List<BetsTrackerProfileBetViewModel> Bets { get; set; }

        public List<BetsTrackerProfileStatisticViewModel> Statistics { get; set; }

        public List<BetsTrackerProfileChartViewModel> Charts { get; set; }

        public List<string> InfoBoxesValues { get; set; }
    }
}
