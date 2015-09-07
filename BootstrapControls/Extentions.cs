using System.Web.UI;
using System.Web.UI.WebControls;

namespace BootstrapControls
{
    /// <summary>
    /// Extentions class which holds all the extention methods.
    /// </summary>
    public static class Extentions
    {
        internal static bool IsValid(this WebControl control)
        {
            bool ret = true;
            if (control.Page != null)
            {
                foreach (BaseValidator validator in control.Page.Validators)
                {
                    if (validator.ControlToValidate == control.ID && !validator.IsValid)
                    {
                        ret = false;
                        break;
                    }
                }
            }
            return ret;
        }

        /// Credits for GetPropertyValue and SetPropertyValue
        /// http://www.infinitezest.com/articles/a-generic-way-of-adding-properties-to-viewstate-in-a-custom-control.aspx
        /// I changed them to extention methods, but the original code wasn't changed.

        /// <summary> 
        /// Returns the property stored in the ViewState. 
        /// </summary> 
        /// <typeparam name="V">Type of the property: String, Integer, etc.</typeparam> 
        /// <param name="propertyName">Name of the property</param> 
        /// <param name="nullValue">The value to return in case the property value is null</param> 
        /// <returns>The value of the property stored in the ViewState</returns> 
        internal static V GetPropertyValue<V>(this StateBag viewState, string propertyName, V nullValue)
        {
            if (viewState[propertyName] == null)
            {
                return nullValue;
            }
            return (V)viewState[propertyName];
        }

        /// <summary> 
        /// Saves the property value in the ViewState 
        /// </summary> 
        /// <typeparam name="V">Type of the property</typeparam> 
        /// <param name="propertyName">Name of the property</param> 
        /// <param name="value">Value of the property</param> 
        internal static void SetPropertyValue<V>(this StateBag viewState, string propertyName, V value)
        {
            viewState[propertyName] = value;
        }
    }
}
