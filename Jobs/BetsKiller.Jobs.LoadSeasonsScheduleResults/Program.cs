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
