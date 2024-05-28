using BetsKiller.Jobs.Processes;
using System.Threading;

namespace BetsKiller.Jobs
{
    public class JobEveryHours2 : JobBase
    {
        #region Constructors

        public JobEveryHours2()
            : base()
        { }

        #endregion

        #region Methods

        protected override void Job()
        {
            /*
             * Load new news
             */

            var loadNews = new LoadNews();
            loadNews.Start();

            Thread.Sleep(base.WAIT_TIME);

            /*
             * Load new injuries
             */

            var loadInjuries = new LoadInjuries();
            loadInjuries.Start();
        }

        #endregion
    }
}