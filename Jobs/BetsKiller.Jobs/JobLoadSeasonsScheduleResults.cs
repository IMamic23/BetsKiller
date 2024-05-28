using BetsKiller.Jobs.Processes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

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
            var currentSeason = Convert.ToInt32(Load.GetCurrentSeasonErikberg());

            var seasons = new List<string>()
                {
                    currentSeason.ToString(),
                    (--currentSeason).ToString(),
                    (--currentSeason).ToString()
                };

            foreach (var season in seasons.OrderBy(x => x))
            {
                var loadScheduleResultsNBA = new LoadScheduleResultsNBA(season);
                loadScheduleResultsNBA.Start();

                Thread.Sleep(base.WAIT_TIME);
            }
        }

        #endregion
    }
}
