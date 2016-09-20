using BetsKiller.Jobs.Processes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BetsKiller.Jobs
{
    public class JobLoadSeasonsScheduleResults : JobBase
    {
        #region Constructors

        public JobLoadSeasonsScheduleResults()
            : base()
        { }

        #endregion

        #region Methods

        protected override void Job()
        {
            /*
            * LoadScheduleResultsNBA for past two seasons and current season.
            */
            int currentSeason = Convert.ToInt32(Load.GetCurrentSeasonErikberg());

            List<string> seasons = new List<string>()
                {
                    currentSeason.ToString(),
                    (--currentSeason).ToString(),
                    (--currentSeason).ToString()
                };

            foreach (string season in seasons.OrderBy(x => x))
            {
                LoadScheduleResultsNBA loadScheduleResultsNBA = new LoadScheduleResultsNBA(season);
                loadScheduleResultsNBA.Start();

                Thread.Sleep(base.WAIT_TIME);
            }
        }

        #endregion
    }
}
