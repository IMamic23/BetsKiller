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
