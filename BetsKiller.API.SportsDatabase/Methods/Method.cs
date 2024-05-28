using BetsKiller.API.SportsDatabase.Enums;
using HtmlAgilityPack;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace BetsKiller.API.SportsDatabase.Methods
{
    public abstract class Method
    {
        #region Private

        private readonly string _link = ConfigurationManager.AppSettings["SportsDatabase_queryUrl"] + "output=default&submit=++S+D+Q+L+%21++&sdql=";

        private HtmlDocument _htmlDocument;

        #endregion

        #region Properties - protected

        protected abstract SportEnum Sport { get; }

        protected abstract string Query { get; }

        protected HtmlDocument HtmlDocument
        {
            get { return this._htmlDocument; }
        }

        #endregion

        #region Methods - protected

        protected void GetData()
        {
            using (WebClient webClient = new WebClient())
            {
                string link = this._link.Replace("{sport}", this.Sport.ToString()) + this.Query;

                webClient.Headers.Add("Accept:text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
                webClient.Headers.Add("Accept-Language:en-US,en;q=0.8");
                webClient.Headers.Add("Cache-Control:no-cache");
                webClient.Headers.Add("Host:sportsdatabase.com");
                webClient.Headers.Add("Pragma:no-cache");
                webClient.Headers.Add("Referer:" + link);
                webClient.Headers.Add("Upgrade-Insecure-Requests:1");
                webClient.Headers.Add("User-Agent:Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/48.0.2564.116 Safari/537.36");

                string response = webClient.DownloadString(link);

                this._htmlDocument = new HtmlDocument();
                this._htmlDocument.LoadHtml(response);
            }
        }

        protected string GetValueFromNode(HtmlNode node, string selector)
        {
            return Regex.Replace(WebUtility.HtmlDecode(node.SelectNodes(selector).First().InnerText), @"\t|\n|\r", "");
        }

        #endregion
    }
}