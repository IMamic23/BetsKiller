using BetsKiller.API.Rotoworld.Entities;
using BetsKiller.API.Rotoworld.Helpers;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Xml;

namespace BetsKiller.API.Rotoworld.Methods
{
    public class MethodNews : MethodXml
    {
        #region Properties

        protected override string Url
        {
            get
            {
                return ConfigurationManager.AppSettings["Rotoworld_news_players_nba"];
            }
        }

        #endregion

        #region Constructors

        public MethodNews()
        { }

        #endregion

        #region Methods

        public List<News> Get()
        {
            base.GetData();

            List<News> listNews = new List<News>();

            XmlNodeList items = base.XmlDocument.SelectNodes("//channel/item");

            foreach (XmlNode item in items)
            {
                News news = new News();

                news.Title = StringTransformator.RemoveSpecialCharacters(WebUtility.HtmlDecode(item.SelectSingleNode("title").InnerText));
                news.Link = StringTransformator.RemoveSpecialCharacters(WebUtility.HtmlDecode(item.SelectSingleNode("link").InnerText));
                news.Description = StringTransformator.RemoveSpecialCharacters(WebUtility.HtmlDecode(item.SelectSingleNode("description").InnerText));
                news.Updated = WebUtility.HtmlDecode(item.SelectSingleNode("*[local-name()='updated']").InnerText);

                listNews.Add(news);
            }

            return listNews;
        }

        #endregion
    }
}
