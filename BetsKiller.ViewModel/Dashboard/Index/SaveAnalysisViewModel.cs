using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BetsKiller.ViewModel.Dashboard.Index
{
    public class SaveAnalysisViewModel
    {
        [Required]
        public int AnalyseType { get; set; }

        [Required]
        public string Game { get; set; }

        [Required]
        public int BetType { get; set; }

        [Required]
        public decimal Pick { get; set; }

        public decimal? OfferTotal { get; set; }

        public decimal? OfferLine { get; set; }

        [Required]
        public decimal Confidence { get; set; }

        [Required]
        public decimal Odd { get; set; }

        [Required]
        public decimal Invested { get; set; }

        [Required]
        [AllowHtml]
        public string Details { get; set; }
    }
}
