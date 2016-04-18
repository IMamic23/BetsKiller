using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.Helper.HTML
{
    public class CustomHtmlElement
    {
        #region Private

        private string _classBelow;
        private string _classAbove;
        private string _styleBelow;
        private string _styleAbove;

        #endregion

        #region Propeties

        public string Type { get; set; }

        public string Class { get; set; }

        public string Style { get; set; }

        public string Value { get; set; }

        public List<CustomHtmlElement> SubElements { get; set; }

        #endregion

        #region Constructors

        public CustomHtmlElement()
        { }

        public CustomHtmlElement(string elementType, string elementClass, string elementStyle, string elementValue, List<CustomHtmlElement> elementSubElements)
        {
            this.Class = elementClass;
            this.Style = elementStyle;
            this.Value = elementValue;
            this.SubElements = elementSubElements;
        }

        public CustomHtmlElement(string classBelow, string classAbove, string styleBelow, string styleAbove)
        {
            this._classBelow = classBelow;
            this._classAbove = classAbove;
            this._styleBelow = styleBelow;
            this._styleAbove = styleAbove;
        }

        #endregion

        #region Methods

        public CustomHtmlElement GetElementByValue(decimal value, bool invertedStyle = false)
        {
            CustomHtmlElement element = new CustomHtmlElement();

            element.Value = value.ToString(CultureInfo.InvariantCulture);

            if (value < 0)
            {
                element.Class = this._classBelow;

                if (invertedStyle)
                {
                    element.Style = this._styleAbove;
                }
                else
                {
                    element.Style = this._styleBelow;
                }
            }
            else
            {
                element.Class = this._classAbove;

                if (invertedStyle)
                {
                    element.Style = this._styleBelow;
                }
                else
                {
                    element.Style = this._styleAbove;
                }
            }

            return element;
        }

        public CustomHtmlElement GetElementDifference(decimal valueSource, decimal valueDestination, bool invertedStyles, int decimalPlaces = 2)
        {
            CustomHtmlElement element = new CustomHtmlElement();

            element.Value = Math.Round(valueSource, decimalPlaces).ToString(CultureInfo.InvariantCulture);

            if (valueSource == valueDestination)
            {
                element.Class = string.Empty;
                element.Style = string.Empty;
            }
            else if (valueSource > valueDestination)
            {
                if (invertedStyles)
                {
                    element.Class = this._classBelow;
                    element.Style = this._styleBelow;
                }
                else
                {
                    element.Class = this._classAbove;
                    element.Style = this._styleAbove;
                }
            }
            else
            {
                if (invertedStyles)
                {
                    element.Class = this._classAbove;
                    element.Style = this._styleAbove;
                }
                else
                {
                    element.Class = this._classBelow;
                    element.Style = this._styleBelow;
                }
            }

            return element;

        }

        #endregion
    }
}
