using BetsKiller.API.SportsDatabase.Entities;
using BetsKiller.API.SportsDatabase.Enums;
using BetsKiller.API.SportsDatabase.Helpers;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;

namespace BetsKiller.API.SportsDatabase.Methods
{
    public class MethodTeamsStatsAverage : Method
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

        public MethodTeamsStatsAverage(SportEnum sport, PlayingSideEnum side, string season)
        {
            this._sport = sport;

            // Example: Average%28points%29%2C+Average%28assists%29%2C+Average%28rebounds%29%2C+Average%28blocks%29%2C+Average%28fouls%29%2C+Average%28turnovers%29+%40+team+and+season+%3D+2015
            this._query = "Average%28points%29%2C+Average%28assists%29%2C+Average%28rebounds%29%2C+Average%28blocks%29%2C+Average%28fouls%29%2C+Average%28turnovers%29+%40+team";

            // Parsing season parameter.
            this._query += "+and+season+%3D+" + season;

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

        public List<TeamStatAverage> Get()
        {
            base.GetData();

            List<TeamStatAverage> teamsStatsAverage = new List<TeamStatAverage>();

            HtmlNodeCollection stats = base.HtmlDocument.DocumentNode.SelectNodes("/html[1]/body[1]/table[3]/tr");

            foreach (HtmlNode stat in stats)
            {
                TeamStatAverage teamStatAverage = new TeamStatAverage();

                teamStatAverage.TeamName = StringTransformator.RemoveSpecialCharacters(stat.SelectSingleNode("td[7]").InnerText.Split(new string[] { " and " }, StringSplitOptions.None)[0]);
                teamStatAverage.AvgPoints = StringTransformator.RemoveSpecialCharacters(stat.SelectSingleNode("td[1]").InnerText);
                teamStatAverage.AvgAssists = StringTransformator.RemoveSpecialCharacters(stat.SelectSingleNode("td[2]").InnerText);
                teamStatAverage.AvgRebounds = StringTransformator.RemoveSpecialCharacters(stat.SelectSingleNode("td[3]").InnerText);
                teamStatAverage.AvgBlocks = StringTransformator.RemoveSpecialCharacters(stat.SelectSingleNode("td[4]").InnerText);
                teamStatAverage.AvgFouls = StringTransformator.RemoveSpecialCharacters(stat.SelectSingleNode("td[5]").InnerText);
                teamStatAverage.AvgTurnovers = StringTransformator.RemoveSpecialCharacters(stat.SelectSingleNode("td[6]").InnerText);

                teamsStatsAverage.Add(teamStatAverage);
            }

            return teamsStatsAverage;
        }

        #endregion
    }
}