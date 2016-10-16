using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.ViewModel.Dashboard.BetsTracker
{
    public class BetsTrackerAnalysisProfitViewModel
    {
        public string Name { get; set; }

        public string Label { get; set; }

        public string TotalBets { get; set; }

        public string Wins { get; set; }

        public string Losses { get; set; }

        public string AverageOdds { get; set; }

        public string ROI { get; set; }

        public string TotalProfit { get; set; }
    }
}
