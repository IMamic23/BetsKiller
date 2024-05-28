using BetsKiller.Helper.HTML;
using System.Collections.Generic;

namespace BetsKiller.ViewModel.Dashboard.SportsPicks
{
    public class SportsPicksTypeViewModel
    {
        public CustomHtmlElement Caption { get; set; }

        public SportsPicksStatsViewModel Stats { get; set; }

        public List<SportsPicksAnalyseViewModel> Analysis { get; set; }
    }
}
