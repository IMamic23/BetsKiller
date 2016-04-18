using BetsKiller.Helper.HTML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.ViewModel.Dashboard.ScheduleResults
{
    public class ScheduleResultsGameViewModel
    {
        public string Date { get; set; }

        public string Side { get; set; }

        public string Opponent { get; set; }

        public CustomHtmlElement Result { get; set; }

        public string Location { get; set; }
    }
}
