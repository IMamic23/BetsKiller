using BetsKiller.Helper.HTML;

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
