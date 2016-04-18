using BetsKiller.Jobs.Processes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            DateTime today = DateTime.Now;
            string since = today.Year.ToString() + today.Month.ToString().PadLeft(2, '0') + today.Day.ToString().PadLeft(2, '0');

            DateTime finish = today.AddDays(7);
            string until = finish.Year.ToString() + finish.Month.ToString().PadLeft(2, '0') + finish.Day.ToString().PadLeft(2, '0');

            LoadScheduleResultsNBA loadScheduleResultsNBA = new LoadScheduleResultsNBA(since, until);
            loadScheduleResultsNBA.Start();
        }

        #endregion
    }
}
