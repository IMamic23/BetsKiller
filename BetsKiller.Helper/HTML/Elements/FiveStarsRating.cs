using System.Collections.Generic;

namespace BetsKiller.Helper.HTML.Elements
{
    public class FiveStarsRating
    {
        #region Methods

        public static List<CustomHtmlElement> GetElements(decimal confidence)
        {
            List<CustomHtmlElement> list;

            if (confidence >= 0.00m && confidence <= 15.00m)
            {
                list = new List<CustomHtmlElement>()
                {
                    HalfStar(),
                    EmptyStar(),
                    EmptyStar(),
                    EmptyStar(),
                    EmptyStar()
                };
            }
            else if (confidence > 15.00m && confidence <= 25.00m)
            {
                list = new List<CustomHtmlElement>()
                {
                    FullStar(),
                    EmptyStar(),
                    EmptyStar(),
                    EmptyStar(),
                    EmptyStar()
                };
            }
            else if (confidence > 25.00m && confidence <= 35.00m)
            {
                list = new List<CustomHtmlElement>()
                {
                    FullStar(),
                    HalfStar(),
                    EmptyStar(),
                    EmptyStar(),
                    EmptyStar()
                };
            }
            else if (confidence > 35.00m && confidence <= 45.00m)
            {
                list = new List<CustomHtmlElement>()
                {
                    FullStar(),
                    FullStar(),
                    EmptyStar(),
                    EmptyStar(),
                    EmptyStar()
                };
            }
            else if (confidence > 45.00m && confidence <= 55.00m)
            {
                list = new List<CustomHtmlElement>()
                {
                    FullStar(),
                    FullStar(),
                    HalfStar(),
                    EmptyStar(),
                    EmptyStar()
                };
            }
            else if (confidence > 55.00m && confidence <= 65.00m)
            {
                list = new List<CustomHtmlElement>()
                {
                    FullStar(),
                    FullStar(),
                    FullStar(),
                    EmptyStar(),
                    EmptyStar()
                };
            }
            else if (confidence > 65.00m && confidence <= 75.00m)
            {
                list = new List<CustomHtmlElement>()
                {
                    FullStar(),
                    FullStar(),
                    FullStar(),
                    HalfStar(),
                    EmptyStar()
                };
            }
            else if (confidence > 75.00m && confidence <= 85.00m)
            {
                list = new List<CustomHtmlElement>()
                {
                    FullStar(),
                    FullStar(),
                    FullStar(),
                    FullStar(),
                    EmptyStar()
                };
            }
            else if (confidence > 85.00m && confidence <= 95.00m)
            {
                list = new List<CustomHtmlElement>()
                {
                    FullStar(),
                    FullStar(),
                    FullStar(),
                    FullStar(),
                    HalfStar()
                };
            }
            else
            {
                list = new List<CustomHtmlElement>()
                {
                    FullStar(),
                    FullStar(),
                    FullStar(),
                    FullStar(),
                    FullStar()
                };
            }

            return list;
        }

        #endregion

        #region Helper methods

        private static CustomHtmlElement FullStar()
        {
            return new CustomHtmlElement()
            {
                Type = "i",
                Class = "fa fa-star"
            };
        }

        private static CustomHtmlElement HalfStar()
        {
            return new CustomHtmlElement()
            {
                Type = "i",
                Class = "fa fa-star-half-o"
            };
        }

        private static CustomHtmlElement EmptyStar()
        {
            return new CustomHtmlElement()
            {
                Type = "i",
                Class = "fa fa-star-o"
            };
        }

        #endregion
    }
}
