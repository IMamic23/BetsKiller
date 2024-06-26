﻿using System;
using System.Collections.Generic;
using System.Linq;
using BetsKiller.DAL;
using BetsKiller.DAL.AppData;
using BetsKiller.Model;

namespace BetsKiller.Jobs.Processes
{
    public class Load
    {
        #region Properties - protected

        protected IAppDataRepository AppDataRepository;

        protected readonly int WAIT_TIME = 15000;

        #endregion

        #region Constructors

        public Load()
        {
            this.AppDataRepository = new AppDataRepository();
        }

        #endregion

        #region Methods - protected

        /// <summary>
        /// Get NBA teams list.
        /// </summary>
        protected List<TeamsNBA> GetTeamsNBA()
        {
            var teams = this.AppDataRepository.GetAllTeamsNBA().ToList();
            
            return teams;
        }

        /// <summary>
        /// Get NBA leaders categories.
        /// </summary>
        protected List<LeadersNBACategories> GetLeadersCategories()
        {
            var leadersCategories = this.AppDataRepository.GetLeadersNBACategories().ToList();

            return leadersCategories;
        }

        /// <summary>
        /// Get NBA current season for SportsDatabase.
        /// </summary>
        protected string GetCurrentSeasonSportsDatabase()
        {
            var season = string.Empty;

            if (DateTime.Now < new DateTime(DateTime.Now.Year, 7, 1))
            {
                season = (DateTime.Now.Year - 1).ToString();
            }
            else
            {
                season = DateTime.Now.Year.ToString();
            }

            return season;
        }

        /// <summary>
        /// Get current date in Erikberg format.
        /// </summary>
        protected string GetCurrentDateErikberg()
        {
            var dateTime = DateTime.Now;

            var date = dateTime.Year.ToString() + dateTime.Month.ToString().PadLeft(2, '0') + dateTime.Day.ToString().PadLeft(2, '0');

            return date;
        }

        /// <summary>
        /// Get sports.
        /// </summary>
        protected List<Sport> GetSports()
        {
            var sports = this.AppDataRepository.GetAllSports().ToList();

            return sports;
        }

        /// <summary>
        /// Get NBA teams names.
        /// </summary>
        protected List<TeamsNBANames> GetTeamsNBANames()
        {
            var names = this.AppDataRepository.GetTeamsNBANames().ToList();

            return names;
        }

        /// <summary>
        /// Get bet statuses.
        /// </summary>
        protected List<BetStatus> GetBetStatuses()
        {
            var betStatuses = this.AppDataRepository.GetAllBetStatus().ToList();

            return betStatuses;
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Get NBA current season for Erikberg.
        /// </summary>
        public static string GetCurrentSeasonErikberg()
        {
            var season = string.Empty;

            if (DateTime.Now > new DateTime(DateTime.Now.Year, 7, 1))
            {
                season = (DateTime.Now.Year + 1).ToString();
            }
            else
            {
                season = DateTime.Now.Year.ToString();
            }

            return season;
        }

        #endregion
    }
}
