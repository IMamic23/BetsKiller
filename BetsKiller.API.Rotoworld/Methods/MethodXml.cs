using System.Net;
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
            get { return _xmlDocument; }
        }

        #endregion

        #region Methods - protected

        protected void GetData()
        {
            using (var webClient = new WebClient())
            {
                var response = webClient.DownloadString(Url);

                _xmlDocument = new XmlDocument();
                _xmlDocument.LoadXml(response);
            }
        }

        #endregion
    }
}
