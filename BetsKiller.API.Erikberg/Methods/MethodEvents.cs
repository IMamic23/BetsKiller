using BetsKiller.API.Erikberg.Entities;
using Newtonsoft.Json;

namespace BetsKiller.API.Erikberg.Methods
{
    /// <summary>
    /// Params:
    /// (O) date - The date using yyyyMMdd format
    /// (O) sport - Specified sport nba/mlb
    /// </summary>
    public class MethodEvents : Method
    {
        #region Properties

        protected override string Url
        {
            get
            {
                return "events.json";
            }
        }

        #endregion

        #region Methods

        public Events Get(string date, string sport)
        {
            AddParameterToDict("date", date);
            AddParameterToDict("sport", sport);

            GetData();

            return JsonConvert.DeserializeObject<Events>(ResponseString);
        }

        #endregion
    }
}
