﻿using BetsKiller.API.NBAcom.Entities;
using BetsKiller.API.NBAcom.Helpers;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Text.RegularExpressions;

namespace BetsKiller.API.NBAcom.Methods
{
    public class MethodPowerRankings : Method
    {
        #region Properties

        protected override string Url
        {
            get
            {
                return ConfigurationManager.AppSettings["NBAcom_powerrankings_rss"];
            }
        }

        #endregion

        #region Constructors

        public MethodPowerRankings()
        { }

        #endregion

        #region Methods

        public List<PowerRankings> Get()
        {
            GetData();

            var powerRankings = new List<PowerRankings>();

            // Get current power ranking link
            var items = XmlDocument.SelectNodes("//channel/item");
            var currentPowerRankingLink = StringTransformator.RemoveSpecialCharacters(items[0].SelectSingleNode("link").InnerText);

            // Load HTML document from link
            var htmlDocument = new HtmlDocument();
            using (var webClient = new WebClient())
            {
                var response = webClient.DownloadString(currentPowerRankingLink);
                htmlDocument.LoadHtml(response);
            }

            // Parse HTML document
            var teams = htmlDocument.DocumentNode.SelectNodes("//div[@class='nbaArticlePRItem']");
            foreach (var team in teams)
            {
                var powerRanking = new PowerRankings();

                powerRanking.TeamName = team.SelectSingleNode("div[2]/b[1]").InnerText.Trim();
                powerRanking.Rank = team.SelectSingleNode("div[1]/div[1]/div[1]/p").InnerHtml.Trim();
                powerRanking.RankLastWeek = team.SelectSingleNode("div[1]/div[1]/div[4]").InnerText.Trim().Split(':')[1].Trim();
                powerRanking.Score = team.SelectNodes("div[2]")[0].ChildNodes[2].InnerText.Trim();
                powerRanking.Pace = team.SelectSingleNode("div[2]/i[1]").InnerText.Trim();
                powerRanking.OffRtg = team.SelectSingleNode("div[2]/i[2]").InnerText.Trim();
                powerRanking.DefRtg = team.SelectSingleNode("div[2]/i[3]").InnerText.Trim();
                powerRanking.NetRtg = team.SelectSingleNode("div[2]/i[4]").InnerText.Trim();
                powerRanking.Description = Regex.Matches(team.SelectSingleNode("div[2]").InnerHtml, @"\<br\>(.+?)\<\/span\>")[1].Value.Replace("<br>", string.Empty).Replace("<span>", string.Empty).Replace("</span>", string.Empty).Trim();
                powerRanking.GamesThisWeek = Regex.Matches(team.SelectSingleNode("div[2]").InnerHtml, @"This week:</b>.*")[0].Value.Replace("This week:</b>", string.Empty).Trim();

                powerRankings.Add(powerRanking);
            }

            return powerRankings;
        }

        #endregion
    }
}
