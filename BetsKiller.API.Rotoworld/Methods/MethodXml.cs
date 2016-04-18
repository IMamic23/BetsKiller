using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BetsKiller.API.Rotoworld.Methods
{
    public abstract class MethodXml
    {
        #region Private

        private XmlDocument _xmlDocument;

        #endregion

        #region Properties - protected

        protected abstract string Url { get; }

        protected XmlDocument XmlDocument
        {
            get { return this._xmlDocument; }
        }

        #endregion

        #region Methods - protected

        protected void GetData()
        {
            using (WebClient webClient = new WebClient())
            {
                string response = webClient.DownloadString(this.Url);

                this._xmlDocument = new XmlDocument();
                this._xmlDocument.LoadXml(response);
            }
        }

        #endregion
    }
}
