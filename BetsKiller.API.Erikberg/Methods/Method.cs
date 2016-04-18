using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
            get { return this._response; }
        }

        #endregion

        #region Methods - protected

        protected void GetData()
        {
            using (WebClient webClient = new WebClient())
            {
                webClient.Headers.Add("Authorization", "Bearer " + this._apiToken);
                webClient.Headers.Add("User-agent", this._userAgentInfo);
                webClient.Headers.Add("Content-Type", "charset=utf-8");
                //webClient.Headers.Add("Accept-encoding", "gzip"); //daje neke hieroglife kao response sa upita.

                this._response = webClient.DownloadString(this._host + this.Url + this.UrlParsedParameters());
            }
        }

        protected void AddParameterToDict(string parameterName, string parameterValue)
        {
            if (this._parameters == null)
            {
                this._parameters = new Dictionary<string, string>();
            }

            if (!string.IsNullOrEmpty(parameterValue))
            {
                this._parameters.Add(parameterName, parameterValue);
            }
        }

        #endregion

        #region Helper methods

        private string UrlParsedParameters()
        {
            string urlParsedParameters = string.Empty;

            if (this._parameters != null && this._parameters.Count > 0)
            {
                urlParsedParameters += "?";

                foreach (KeyValuePair<string, string> param in this._parameters)
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
