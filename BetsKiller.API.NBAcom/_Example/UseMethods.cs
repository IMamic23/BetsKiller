using BetsKiller.API.NBAcom.Entities;
using BetsKiller.API.NBAcom.Methods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.API.NBAcom._Example
{
    public class UseMethods
    {
        public static void Start()
        {
            MethodNews methodNews = new MethodNews();
            List<News> news = methodNews.Get();
        }
    }
}
