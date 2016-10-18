using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.ViewModel.Dashboard.BetsTracker
{
    public class BetsTrackerViewModel
    {
        public List<BetsTrackerGameScoreViewModel> GameScores { get; set; }

        public List<BetsTrackerAnalysisProfitViewModel> AnalysisProfit { get; set; }

        public List<BetsTrackerBettingProfileViewModel> BettingProfiles { get; set; }

        public List<BetsTrackerPerformanceViewModel> Performances { get; set; }

        public BetsTrackerBettingProfileDataViewModel DefaultProfile { get; set; }
    }
}
