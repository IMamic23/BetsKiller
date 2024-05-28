using System.Collections.Generic;
using System.Configuration;
using System.Net;

namespace BetsKiller.API.Erikberg.Methods
{
    public abstract class Method
    {
        #region Private

        private readonly string _apiToken = ConfigurationManager.AppSettings["Erikberg_apiToken"];
        private readonly string _userAgentInfo = ConfigurationManager.AppSettings["Erikberg_userAgentInfo"];
        private readonly string _host = ConfigurationManager.AppSettings["Erikberg_host"];

        private Dictionary<string, string> _parameters;
        private string _response;

        #endregion

        #region Properties - protected

        protected abstract string Url { get; }

        protected string ResponseString
        {
            get { return _response; }
        }

        #endregion

        #region Methods - protected

        protected void GetData()
        {
            using (WebClient webClient = new WebClient())
            {
                webClient.Headers.Add("Authorization", "Bearer " + _apiToken);
                webClient.Headers.Add("User-agent", _userAgentInfo);
                webClient.Headers.Add("Content-Type", "charset=utf-8");
                //webClient.Headers.Add("Accept-encoding", "gzip"); //daje neke hieroglife kao response sa upita.

                _response = webClient.DownloadString(_host + Url + UrlParsedParameters());
            }
        }

        protected void AddParameterToDict(string parameterName, string parameterValue)
        {
            if (_parameters == null)
            {
                _parameters = new Dictionary<string, string>();
            }

            if (!string.IsNullOrEmpty(parameterValue))
            {
                _parameters.Add(parameterName, parameterValue);
            }
        }

        #endregion

        #region Helper methods

        private string UrlParsedParameters()
        {
            string urlParsedParameters = string.Empty;

            if (_parameters != null && _parameters.Count > 0)
            {
                urlParsedParameters += "?";

                foreach (KeyValuePair<string, string> param in _parameters)
                {
                    urlParsedParameters += param.Key + "=" + param.Value + "&";
                }

                urlParsedParameters = urlParsedParameters.Trim('&');
            }

            return urlParsedParameters;
        }

        #endregion
    }
}
