using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.ViewModel.Dashboard.BetsTracker
{
    public class BetsTrackerProfileChartViewModel
    {
        public bool ActiveTab { get; set; }

        public string Name { get; set; }

        public string Label { get; set; }

        public string TotalValue { get; set; }
        public string MoneylineValue { get; set; }
        public string Handicap { get; set; }
        public string Parlay { get; set; }
    }
}
