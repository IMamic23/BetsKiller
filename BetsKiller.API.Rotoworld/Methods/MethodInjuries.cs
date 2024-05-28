using BetsKiller.API.Rotoworld.Entities;
using BetsKiller.API.Rotoworld.Helpers;
using System.Collections.Generic;
using System.Configuration;
using System.Net;

namespace BetsKiller.API.Rotoworld.Methods
{
    public class MethodInjuries : MethodHtml
    {
        #region Properties

        protected override string Url
        {
            get { return ConfigurationManager.AppSettings["Rotoworld_injuries_nba"]; }
        }

        #endregion

        #region Constructors

        public MethodInjuries()
        { }

        #endregion

        #region Methods

        public List<Injury> Get()
        {
            GetData();

            var injuries = new List<Injury>();

            // Iterate teams
            var teams = HtmlDocument.DocumentNode.SelectNodes("//div[@class='pb']");
            foreach (var team in teams)
            {
                var teamName = team.SelectSingleNode("div").InnerText;

                // Iterate players
                var players = team.SelectNodes("table/tr");
                for (var i = 1; i < players.Count; i++)
                {
                    var playerData = players[i].SelectNodes("td");

                    var injury = new Injury();

                    injury.TeamName = teamName;
                    injury.PlayerName = StringTransformator.RemoveSpecialCharacters(WebUtility.HtmlDecode(playerData[0].InnerText));
                    injury.PlayerPosition = StringTransformator.RemoveSpecialCharacters(WebUtility.HtmlDecode(playerData[2].InnerText));
                    injury.PlayerStatus = StringTransformator.RemoveSpecialCharacters(WebUtility.HtmlDecode(playerData[3].InnerText));
                    injury.Date = WebUtility.HtmlDecode(playerData[4].InnerText);
                    injury.Type = StringTransformator.RemoveSpecialCharacters(WebUtility.HtmlDecode(playerData[5].InnerText));
                    injury.Returns = StringTransformator.RemoveSpecialCharacters(WebUtility.HtmlDecode(playerData[6].InnerText));

                    var report = playerData[1].SelectSingleNode("div/div[1]").InnerText;
                    injury.Report = StringTransformator.RemoveSpecialCharacters(WebUtility.HtmlDecode(report));

                    var reportUpdateDate = playerData[1].SelectSingleNode("div/div[3]").InnerText;
                    injury.ReportUpdateDate = WebUtility.HtmlDecode(reportUpdateDate);

                    injuries.Add(injury);
                }
            }

            return injuries;
        }

        #endregion
    }
}
