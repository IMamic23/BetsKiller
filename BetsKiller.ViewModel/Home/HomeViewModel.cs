using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.ViewModel.Home
{
    public class HomeViewModel
    {
        public string Profit { get; set; }

        public string Predictions { get; set; }

        public string ROI { get; set; }

        public string Quote { get; set; }

        public List<HomeImageViewModel> Images { get; set; }
    }
}
