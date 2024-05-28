using BetsKiller.Jobs.Processes;

namespace BetsKiller.Jobs
{
    public class JobEveryMonday15 : JobBase
    {
        #region Constructors

        public JobEveryMonday15()
            : base()
        { }

        #endregion

        #region Methods

        protected override void Job()
        {
            /*
             * Load new NBA power rankings
             */

            LoadPowerRankingsNBA loadPowerRankingNBA = new LoadPowerRankingsNBA();
            loadPowerRankingNBA.Start();
        }

        #endregion
    }
}
