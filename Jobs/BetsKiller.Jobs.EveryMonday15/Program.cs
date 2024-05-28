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
