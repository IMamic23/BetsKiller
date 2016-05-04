using BetsKiller.Jobs.Processes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

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

            LoadTeamsNBA loadTeamsNba = new LoadTeamsNBA();
            loadTeamsNba.Start();

            Thread.Sleep(base.WAIT_TIME);
            Thread.Sleep(base.WAIT_TIME); // Beacause of error "Too many requests"

            /*
             * Load new NBA leaders.
             */

            LoadLeadersNBA loadLeadersNba = new LoadLeadersNBA();
            loadLeadersNba.Start();

            Thread.Sleep(base.WAIT_TIME);
            Thread.Sleep(base.WAIT_TIME); // Beacause of error "Too many requests"

            /*
             * Load new NBA rosters.
             */

            LoadRostersNBA loadRostersNba = new LoadRostersNBA();
            loadRostersNba.Start();

            Thread.Sleep(base.WAIT_TIME);
            Thread.Sleep(base.WAIT_TIME); // Beacause of error "Too many requests"

            /*
             * Load new NBA draft data.
             */

            LoadDraftNBA loadDrafNba = new LoadDraftNBA();
            loadDrafNba.Start();

            Thread.Sleep(base.WAIT_TIME);

            /*
             * Update events data from past days.
             */
            DateTime today = DateTime.Now;
            string dateToday = today.Year.ToString() + today.Month.ToString().PadLeft(2, '0') + today.Day.ToString().PadLeft(2, '0');

            DateTime yesterday = DateTime.Now.AddDays(-1);
            string dateYesterday = yesterday.Year.ToString() + yesterday.Month.ToString().PadLeft(2, '0') + yesterday.Day.ToString().PadLeft(2, '0');

            LoadEventsNBA loadEventsNba = new LoadEventsNBA(new List<string>() { dateToday, dateYesterday });
            loadEventsNba.Start();

            Thread.Sleep(base.WAIT_TIME);

            /*
             * Load standings
             */
            LoadStandings loadStandings = new LoadStandings();
            loadStandings.Start();
        }

        #endregion
    }
}