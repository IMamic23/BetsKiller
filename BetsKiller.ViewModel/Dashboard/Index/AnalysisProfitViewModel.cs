﻿using BetsKiller.Helper.HTML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.ViewModel.Dashboard.Index
{
    public class AnalysisProfitViewModel
    {
        public string TotalBets { get; set; }
        public CustomHtmlElement TotalBetsPctFromLastWeek { get; set; }

        public string Wins { get; set; }
        public CustomHtmlElement WinsPctFromLastWeek { get; set; }

        public string Losses { get; set; }
        public CustomHtmlElement LossesPctFromLastWeek { get; set; }

        public string TotalInvested { get; set; }
        public CustomHtmlElement TotalInvestedPctFromLastWeek { get; set; }

        public string ROI { get; set; }
        public CustomHtmlElement ROIPctFromLastWeek { get; set; }

        public string Profit { get; set; }
        public CustomHtmlElement ProfitPctFromLastWeek { get; set; }
    }
}