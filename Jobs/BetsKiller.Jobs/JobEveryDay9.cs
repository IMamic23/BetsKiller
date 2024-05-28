using BetsKiller.Jobs.Processes;
using System;
using System.Collections.Generic;
using System.Threading;

namespace BetsKiller.Jobs
{
    public class JobEveryDay9 : JobBase
    {
        #region Constructors

        public JobEveryDay9()
            : base()
        { }

        #endregion

        #region Methods

        protected override void Job()
        {
            /*
             * Update NBA teams.
             */

            var loadTeamsNba = new LoadTeamsNBA();
            loadTeamsNba.Start();

            Thread.Sleep(base.WAIT_TIME);
            Thread.Sleep(base.WAIT_TIME); // Beacause of error "Too many requests"

            /*
             * Load new NBA leaders.
             */

            var loadLeadersNba = new LoadLeadersNBA();
            loadLeadersNba.Start();

            Thread.Sleep(base.WAIT_TIME);
            Thread.Sleep(base.WAIT_TIME); // Beacause of error "Too many requests"

            /*
             * Load new NBA rosters.
             */

            var loadRostersNba = new LoadRostersNBA();
            loadRostersNba.Start();

            Thread.Sleep(base.WAIT_TIME);
            Thread.Sleep(base.WAIT_TIME); // Beacause of error "Too many requests"

            /*
             * Load new NBA draft data.
             */

            var loadDrafNba = new LoadDraftNBA();
            loadDrafNba.Start();

            Thread.Sleep(base.WAIT_TIME);

            /*
             * Update events data from past days.
             */
            var today = DateTime.Now;
            var dateToday = today.Year.ToString() + today.Month.ToString().PadLeft(2, '0') + today.Day.ToString().PadLeft(2, '0');

            var yesterday = DateTime.Now.AddDays(-1);
            var dateYesterday = yesterday.Year.ToString() + yesterday.Month.ToString().PadLeft(2, '0') + yesterday.Day.ToString().PadLeft(2, '0');

            var loadEventsNba = new LoadEventsNBA(new List<string>() { dateToday, dateYesterday });
            loadEventsNba.Start();

            Thread.Sleep(base.WAIT_TIME);

            /*
             * Load standings
             */
            var loadStandings = new LoadStandings();
            loadStandings.Start();
        }

        #endregion
    }
}