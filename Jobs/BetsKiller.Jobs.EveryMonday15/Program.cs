using BetsKiller.Jobs.Processes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.Jobs.EveryMonday15
{
    class Program
    {
        static void Main(string[] args)
        {
            JobEveryMonday15 job = new JobEveryMonday15();
            job.StartJob();
        }
    }
}
