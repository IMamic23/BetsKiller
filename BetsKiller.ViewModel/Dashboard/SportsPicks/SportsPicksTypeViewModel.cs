using BetsKiller.Helper.HTML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.ViewModel.Dashboard.SportsPicks
{
    public class SportsPicksTypeViewModel
    {
        public CustomHtmlElement Caption { get; set; }

        public SportsPicksStatsViewModel Stats { get; set; }

        public List<SportsPicksAnalyseViewModel> Analysis { get; set; }
    }
}
