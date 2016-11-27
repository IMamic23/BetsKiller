using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.ViewModel.Dashboard.BetsTracker
{
    public class BetsTrackerBettingProfileDataViewModel
    {
        public string ProfileName { get; set; }

        public List<BetsTrackerProfileBetViewModel> Bets { get; set; }

        public List<BetsTrackerProfileStatisticViewModel> Statistics { get; set; }

        public List<BetsTrackerProfileChartViewModel> Charts { get; set; }

        public List<string> InfoBoxesValues { get; set; }

        public BetsTrackerBettingProfileDataViewModel()
        {
            this.Bets = new List<BetsTrackerProfileBetViewModel>();
            this.Statistics = new List<BetsTrackerProfileStatisticViewModel>();
            this.Charts = new List<BetsTrackerProfileChartViewModel>();
            this.InfoBoxesValues = new List<string>();
        }
    }
}
