using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
            get { return this._htmlDocument; }
        }

        #endregion

        #region Methods - protected

        protected void GetData()
        {
            using (WebClient webClient = new WebClient())
            {
                string response = webClient.DownloadString(this.Url);

                this._htmlDocument = new HtmlDocument();
                this._htmlDocument.LoadHtml(response);
            }
        }

        #endregion
    }
}
