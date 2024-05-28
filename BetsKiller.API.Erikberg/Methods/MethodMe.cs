using BetsKiller.API.Erikberg.Entities;
using Newtonsoft.Json;

namespace BetsKiller.API.Erikberg.Methods
{
    public class MethodMe : Method
    {
        #region Properties

        protected override string Url
        {
            get { return "me.json"; }
        }

        #endregion

        #region Methods

        public Me Get()
        {
            GetData();

            return JsonConvert.DeserializeObject<Me>(ResponseString);
        }

        #endregion
    }
}
