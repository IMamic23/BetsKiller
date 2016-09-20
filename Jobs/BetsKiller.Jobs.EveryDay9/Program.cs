using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.Jobs.EveryDay9
{
    class Program
    {
        static void Main(string[] args)
        {
            JobEveryDay9 job = new JobEveryDay9();
            job.StartJob();
        }
    }
}
