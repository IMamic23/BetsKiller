using BetsKiller.API.Erikberg.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            base.AddParameterToDict("date", date);
            base.AddParameterToDict("sport", sport);

            base.GetData();

            return JsonConvert.DeserializeObject<Events>(base.ResponseString);
        }

        #endregion
    }
}
