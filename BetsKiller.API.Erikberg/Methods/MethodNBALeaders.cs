using BetsKiller.API.Erikberg.Entities;
using BetsKiller.API.Erikberg.Enums;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BetsKiller.API.Erikberg.Methods
{
    /// <summary>
    /// Prams:
    /// (R) sport - For which sport is query
    /// (R) category_id - For which category is query
    /// (O) limit - Return player's ranked less than or equal to specified limit. If limit is omitted or limit > 15, player's ranked 1-15 will be returned.
    ///             When multiple players have the same value and they are ranked LE limit, all of those players will be returned. For example, if limit=5
    ///             is specified and three players are all ranked fifth, a total of eight players will be returned.
    /// (O) qualified - When season_type is regular, this parameter determines whether players who meet the NBA's minimum qualifications in the statistical 
    ///                 category are returned or whether all players are returned regardless if they qualify. Possible values are true and false. By default,
    ///                 this value is true. If season_type is anything other than regular, this parameter has no meaning.
    /// (O) season_type - Specify the season type for the statistics to be returned. Acceptable values are regular and post.
    ///                   Defaults to regular if this parameter is not specified.
    /// </summary>
    public class MethodNBALeaders : Method
    {
        #region Private

        private SportEnum _sport;
        private string _categoryId;

        #endregion

        #region Properties

        protected override string Url
        {
            get
            {
                string url = "${sport}/leaders/${category_id}.json";

                url = url.Replace("${sport}", _sport.ToString());
                url = url.Replace("${category_id}", _categoryId);

                return url;
            }
        }

        #endregion

        #region Methods

        public List<NBALeader> Get(SportEnum sport, string categoryId, string limit, string qualified, string seasonType)
        {
            _sport = sport;
            _categoryId = categoryId;

            AddParameterToDict("limit", limit);
            AddParameterToDict("qualified", qualified);
            AddParameterToDict("season_type", seasonType);

            GetData();

            return JsonConvert.DeserializeObject<List<NBALeader>>(ResponseString);
        }

        #endregion
    }
}
