using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.Helper.Operations
{
    public static class InternetRequest
    {
        public static string GetResponse(string url)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            using (WebClient client = new WebClient())
            {
                return client.DownloadString(url);
            }
        }
    }
}
