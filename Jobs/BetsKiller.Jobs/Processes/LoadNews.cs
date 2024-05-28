using System;
using System.Collections.Generic;
using System.Linq;
using BetsKiller.Helper.Constants;
using BetsKiller.Helper.Operations;
using BetsKiller.Model;

namespace BetsKiller.Jobs.Processes
{
    public class LoadNews : Load
    {
        #region Private

        private List<NewsFeed> _news;

        private List<Sport> _sports;
        private List<NewsPublish> _newsPublished;

        #endregion

        #region Methods

        public void Start()
        {
            this._news = new List<NewsFeed>();

            // Get sports.
            this._sports = base.GetSports();
            this._newsPublished = base.AppDataRepository.GetAllNewsPublish().ToList();

            // Get NBAcom news
            this.GetParseNBAcomNews();

            // Get Rotoworld news
            this.GetParseRotoworldNewsNBA();

            // Save news in DB
            this.SaveNewsInDB();
        }

        #endregion

        #region Helper methods

        private void GetParseNBAcomNews()
        {
            // Get news
            var methodNews = new BetsKiller.API.NBAcom.Methods.MethodNews();
            var dataListNews = methodNews.Get();

            var randomNumbers = RandomElement.FromList(dataListNews.Count, 9);
            for (var i = 0; i < 9; i++)
            {
                var dataNews = dataListNews[randomNumbers[i]];

                var news = new NewsFeed();
                news.Title = dataNews.Title;
                news.Link = dataNews.Link;
                news.Description = dataNews.Description;
                news.Sport_Id = this._sports.Single(x => x.Name == SportConst.NBA).Id;

                this._news.Add(news);
            }
        }

        private void GetParseRotoworldNewsNBA()
        {
            // Get news
            var methodNews = new BetsKiller.API.Rotoworld.Methods.MethodNews();
            var dataListNews = methodNews.Get();

            var randomNumbers = RandomElement.FromList(dataListNews.Count, 9);
            for (var i = 0; i < 9; i++)
            {
                var dataNews = dataListNews[randomNumbers[i]];

                var news = new NewsFeed();
                news.Title = dataNews.Title;
                news.Link = dataNews.Link;
                news.Description = dataNews.Description;
                news.Sport_Id = this._sports.Single(x => x.Name == SportConst.NBA).Id;

                this._news.Add(news);
            }
        }

        private void SaveNewsInDB()
        {
            // Get not published news
            var currentNews = new NewsFeed[18];

            // Get current news and insert them in array
            base.AppDataRepository.GetAllNewsFeed().ToList().CopyTo(currentNews, 0);

            // Iterate through new news
            foreach (var dataNews in this._news)
            {
                if (!currentNews.Any(x => x != null && x.Link == dataNews.Link) && !this._newsPublished.Any(x => x.Link == dataNews.Link)) // Only if news not exists in DB, use it
                {
                    int elementIndex;

                    if (currentNews.Any(x => x == null)) // If there is empty space, put it there
                    {
                        elementIndex = Array.IndexOf<NewsFeed>(currentNews, currentNews.Where(x => x == null).First());
                    }
                    else // Get the oldest news
                    {
                        elementIndex = Array.IndexOf<NewsFeed>(currentNews, currentNews.OrderBy(x => x.Created).First());
                    }

                    currentNews[elementIndex] = new NewsFeed();
                    currentNews[elementIndex].Title = dataNews.Title;
                    currentNews[elementIndex].Description = dataNews.Description;
                    currentNews[elementIndex].Link = dataNews.Link;
                    currentNews[elementIndex].Created = DateTime.Now;
                    currentNews[elementIndex].Sport_Id = dataNews.Sport_Id;
                }
            }

            // Clear old news
            base.AppDataRepository.ClearNewsFeed();

            // Save news to the DB
            base.AppDataRepository.SaveNewsFeed(currentNews.Where(x => x != null));
        }

        #endregion
    }
}
