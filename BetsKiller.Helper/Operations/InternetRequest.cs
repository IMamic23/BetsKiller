using System.Net;

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
