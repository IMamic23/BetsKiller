using BetsKiller.API.NBAcom.Methods;

namespace BetsKiller.API.NBAcom._Example
{
    public class UseMethods
    {
        public static void Start()
        {
            var methodNews = new MethodNews();
            var news = methodNews.Get();
        }
    }
}
