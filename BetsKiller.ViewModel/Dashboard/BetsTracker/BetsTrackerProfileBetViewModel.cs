using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.ViewModel.Dashboard.BetsTracker
{
    public class BetsTrackerProfileBetViewModel
    {
        public string Type { get; set; }

        public string Id { get; set; }

        public string Timestamp { get; set; }

        public string Game { get; set; }

        public string BetAmount { get; set; }

        public string Odds { get; set; }

        public string BetValue { get; set; }

        public string BetStatus { get; set; }

        public string ProfitLoss { get; set; }
    }
}
