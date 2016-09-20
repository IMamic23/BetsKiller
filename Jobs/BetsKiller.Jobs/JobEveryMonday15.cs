using BetsKiller.Jobs.Processes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
