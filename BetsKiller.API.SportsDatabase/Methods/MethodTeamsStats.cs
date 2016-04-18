using BetsKiller.API.SportsDatabase.Entities;
using BetsKiller.API.SportsDatabase.Enums;
using BetsKiller.API.SportsDatabase.Helpers;
using HtmlAgilityPack;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.API.SportsDatabase.Methods
{
    public class MethodTeamsStats : Method
    {
        #region Private

        private string _query;
        private SportEnum _sport;

        #endregion

        #region Properties

        protected override string Query
        {
            get { return this._query; }
        }

        protected override SportEnum Sport
        {
            get { return this._sport; }
        }

        #endregion

        #region Constructors

        public MethodTeamsStats(SportEnum sport, PlayingSideEnum side, string season)
        {
            this._sport = sport;

            // Example: season+%3D+2015+and+team
            this._query = string.Empty;

            // Parsing season parameter.
            this._query = "season+%3D+" + season + "+and+team";

            // Parsing side parameter.
            // If we are using analyse for both sides, then we don't need extra parameter.
            if (side == PlayingSideEnum.Home)
            {
                this._query += "+and+H";
            }
            else if (side == PlayingSideEnum.Away)
            {
                this._query += "+and+A";
            }
        }

        #endregion

        #region Methods

        public List<TeamStat> Get()
        {
            base.GetData();

            List<TeamStat> teamStats = new List<TeamStat>();

            #region Summary

            HtmlNodeCollection rowsStat = base.HtmlDocument.DocumentNode.SelectNodes("/html[1]/body[1]/table[3]/tr");

            foreach (HtmlNode rowStat in rowsStat)
            {
                TeamStat teamStat = new TeamStat();

                teamStat.TeamName = StringTransformator.RemoveSpecialCharacters(rowStat.SelectSingleNode("td[7]").InnerText);
                teamStat.Games = StringTransformator.RemoveSpecialCharacters(WebUtility.HtmlDecode(rowStat.SelectSingleNode("td[1]").InnerText));
                teamStat.SU = WebUtility.HtmlDecode(rowStat.SelectSingleNode("td[6]").InnerText).Trim('\n');
                teamStat.ATS = WebUtility.HtmlDecode(rowStat.SelectSingleNode("td[2]").InnerText).Trim('\n');
                teamStat.OU = WebUtility.HtmlDecode(rowStat.SelectSingleNode("td[4]").InnerText).Trim('\n');
                teamStat.AvgLine = StringTransformator.RemoveSpecialCharacters(WebUtility.HtmlDecode(rowStat.SelectSingleNode("td[3]").InnerText));
                teamStat.AvgTotal = StringTransformator.RemoveSpecialCharacters(WebUtility.HtmlDecode(rowStat.SelectSingleNode("td[5]").InnerText));

                teamStats.Add(teamStat);
            }
            #endregion

            return teamStats;
        }

        #endregion
    }
}
