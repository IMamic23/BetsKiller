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

        public List<BetsTrackerProfileStatisticElementViewModel> Data { get; set; }
    }
}
