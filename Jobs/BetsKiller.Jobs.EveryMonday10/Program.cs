using BetsKiller.Jobs.Processes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.Jobs.EveryMonday10
{
    class Program
    {
        static void Main(string[] args)
        {
            JobEveryMonday9 job = new JobEveryMonday9();
            job.StartJob();
        }
    }
}
