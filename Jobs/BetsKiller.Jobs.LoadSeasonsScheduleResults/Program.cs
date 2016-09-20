using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.Jobs.LoadSeasonsScheduleResults
{
    class Program
    {
        static void Main(string[] args)
        {
            JobLoadSeasonsScheduleResults job = new JobLoadSeasonsScheduleResults();
            job.StartJob();
        }
    }
}
