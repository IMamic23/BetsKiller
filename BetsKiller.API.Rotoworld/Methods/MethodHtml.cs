using HtmlAgilityPack;
using System.Net;

namespace BetsKiller.API.Rotoworld.Methods
{
    public abstract class MethodHtml
    {
        #region Private

        private HtmlDocument _htmlDocument;

        #endregion

        #region Properties - protected

        protected abstract string Url { get; }

        protected HtmlDocument HtmlDocument
        {
            get { return _htmlDocument; }
        }

        #endregion

        #region Methods - protected

        protected void GetData()
        {
            using (var webClient = new WebClient())
            {
                var response = webClient.DownloadString(Url);

                _htmlDocument = new HtmlDocument();
                _htmlDocument.LoadHtml(response);
            }
        }

        #endregion
    }
}
