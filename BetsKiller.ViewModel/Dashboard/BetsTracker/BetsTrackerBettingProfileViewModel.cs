using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.ViewModel.Dashboard.BetsTracker
{
    public class BetsTrackerBettingProfileViewModel
    {
        public bool ActiveTab { get; set; }

        public string Id { get; set; }

        public string Label { get; set; }

        public string OpenBets { get; set; }

        public string LastBetsStatus { get; set; }
    }
}
