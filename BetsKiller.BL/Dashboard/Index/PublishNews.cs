using BetsKiller.DAL;
using BetsKiller.DAL.AppData;
using BetsKiller.Model;
using BetsKiller.ViewModel.Dashboard.Index;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.BL.Dashboard.Index
{
    public class PublishNews : ProcessBase
    {
        #region Private

        private HeadlineNBAViewModel _headlineNbaViewModel;

        private IAppDataRepository _appDataRepository;

        private NewsFeed _newsFeed;

        #endregion

        #region Properties - override

        protected override string _successMessage
        {
            get { return "News published successfully."; }
        }

        protected override string _failMessage
        {
            get { return "News publish failed."; }
        }

        #endregion

        #region Constructors

        public PublishNews(HeadlineNBAViewModel headlineNbaViewModel)
        {
            this._headlineNbaViewModel = headlineNbaViewModel;

            this._appDataRepository = new AppDataRepository();
        }

        #endregion

        #region Methods

        protected override void Process()
        {
            this.ParseData();

            this.SaveData();
        }

        #endregion

        #region Helper methods

        private void ParseData()
        {
            // Get news current data from DB
            this._newsFeed = this._appDataRepository.GetAllNewsFeed().Single(x => x.Id.ToString() == this._headlineNbaViewModel.Id);

            // Update current values and set news published
            this._newsFeed.Title = this._headlineNbaViewModel.Title;
            this._newsFeed.Description = this._headlineNbaViewModel.Description;
        }

        private void SaveData()
        {
            // Save news in NewsPublish table
            NewsPublish newsPublish = new NewsPublish()
            {
                Title = this._newsFeed.Title,
                Description = this._newsFeed.Description,
                Link = this._newsFeed.Link,
                Published = DateTime.Now,
                Sport_Id = this._newsFeed.Sport_Id
            };

            // Get current published news
            List<NewsPublish> currentPublishednews = this._appDataRepository.GetAllNewsPublish().ToList();

            // Get number of current published news
            int numberOfPublishedNews = currentPublishednews.Count;
            numberOfPublishedNews -= 8;

            // Delete the oldest published news
            if (numberOfPublishedNews > 0)
            {
                for (int i = 0; i < numberOfPublishedNews; i++)
                {
                    NewsPublish oldestNewsPublish = currentPublishednews.OrderBy(x => x.Published).First();
                    this._appDataRepository.DeleteNewsPublish(oldestNewsPublish.Id);
                }
            }

            // Save published news
            this._appDataRepository.SaveNewsPublish(newsPublish);

            // Delete news from feed
            this._appDataRepository.DeleteNewsFeed(this._newsFeed.Id);
        }

        #endregion
    }
}
