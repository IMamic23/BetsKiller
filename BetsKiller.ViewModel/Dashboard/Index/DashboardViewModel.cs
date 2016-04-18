using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.ViewModel.Dashboard.Index
{
    public class DashboardViewModel
    {
        public List<HeadlineNBAViewModel> HeadlinesFeedNBA { get; set; }
        public List<HeadlineNBAViewModel> HeadlinesPublishedNBA { get; set; }

        public List<TodaysGamesNBAViewModel> TodaysGamesNBA { get; set; }

        public List<AnalysisViewModel> TodaysFreeAnalysisNBA { get; set; }
        public List<AnalysisViewModel> TodaysPremiumAnalysisNBA { get; set; }

        public List<AnalysisViewModel> LastFreeAnalysisNBA { get; set; }
        public List<AnalysisViewModel> LastPremiumAnalysisNBA { get; set; }

        public AnalysisProfitViewModel AnalysisProfitNBA { get; set; }

        public List<ProfitChartViewModel> LastThreeMonthsProfitChart { get; set; }

        public List<ProfitChartViewModel> LastYearProfitChart { get; set; }

        public List<AnalyseTypeViewModel> AnalyseTypes { get; set; }

        public List<BetTypeViewModel> BetTypes { get; set; }
    }
}
