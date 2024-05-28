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
