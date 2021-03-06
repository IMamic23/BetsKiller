﻿using BetsKiller.Jobs.Processes;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

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

            LoadNews loadNews = new LoadNews();
            loadNews.Start();

            Thread.Sleep(base.WAIT_TIME);

            /*
             * Load new injuries
             */

            LoadInjuries loadInjuries = new LoadInjuries();
            loadInjuries.Start();
        }

        #endregion
    }
}