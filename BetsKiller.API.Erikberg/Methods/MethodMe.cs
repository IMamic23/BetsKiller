using BetsKiller.API.Erikberg.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            base.GetData();

            return JsonConvert.DeserializeObject<Me>(base.ResponseString);
        }

        #endregion
    }
}
