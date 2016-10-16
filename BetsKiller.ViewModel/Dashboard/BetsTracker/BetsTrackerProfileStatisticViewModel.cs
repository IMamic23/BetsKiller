using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.ViewModel.Dashboard.BetsTracker
{
    public class BetsTrackerProfileStatisticViewModel
    {
        public bool ActiveTab { get; set; }

        public string Name { get; set; }

        public string Label { get; set; }

        public string Date { get; set; }

        public string Wins { get; set; }

        public string Losses { get; set; }

        public string Invested { get; set; }

        public string Profit { get; set; }
    }
}
