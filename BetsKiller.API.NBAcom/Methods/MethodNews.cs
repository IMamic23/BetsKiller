using BetsKiller.API.NBAcom.Entities;
using BetsKiller.API.NBAcom.Helpers;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Xml;

namespace BetsKiller.API.NBAcom.Methods
{
    public class MethodNews : Method
    {
        #region Properties

        protected override string Url
        {
            get
            {
                return ConfigurationManager.AppSettings["NBAcom_news_rss"];
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
                news.PublishDate = WebUtility.HtmlDecode(item.SelectSingleNode("pubDate").InnerText);

                listNews.Add(news);
            }
            
            return listNews;
        }

        #endregion
    }
}
