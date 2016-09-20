using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.Jobs.EveryHours8
{
    class Program
    {
        static void Main(string[] args)
        {
            JobEveryHours2 job = new JobEveryHours2();
            job.StartJob();
        }
    }
}
