using BetsKiller.Jobs.Processes;
using System;

namespace BetsKiller.Jobs
{
    public class JobEveryMonday9 : JobBase
    {
        #region Constructor

        public JobEveryMonday9()
            : base()
        { }

        #endregion

        #region Methods

        protected override void Job()
        {
            /*
             * LoadScheduleResultsNBA for the next one week to refresh data.
             */
            var today = DateTime.Now;
            var since = today.Year.ToString() + today.Month.ToString().PadLeft(2, '0') + today.Day.ToString().PadLeft(2, '0');

            var finish = today.AddDays(7);
            var until = finish.Year.ToString() + finish.Month.ToString().PadLeft(2, '0') + finish.Day.ToString().PadLeft(2, '0');

            var loadScheduleResultsNBA = new LoadScheduleResultsNBA(since, until);
            loadScheduleResultsNBA.Start();
        }

        #endregion
    }
}
