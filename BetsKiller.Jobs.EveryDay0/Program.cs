using BetsKiller.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.Jobs.EveryDay0
{
    class Program
    {
        static void Main(string[] args)
        {
            JobEveryDay0 job = new JobEveryDay0();
            job.StartJob();
        }
    }
}
