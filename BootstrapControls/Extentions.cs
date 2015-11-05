using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BootstrapControls
{
    /// <summary>
    /// Extentions class which holds all the extention methods.
    /// </summary>
    public static class Extentions
    {
        internal static Control FindControlRecursive(this Control root, string id)
        {
            if (root.ID == id)
            {
                return root;
            }
            foreach (Control c in root.Controls)
            {
                Control t = c.FindControlRecursive(id);
                if (t != null)
                {
                    return t;
                }
            }
            return null;
        }


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

        /// <summary>
        /// Gets the ID of the post back control.
        /// 
        /// See: http://geekswithblogs.net/mahesh/archive/2006/06/27/83264.aspx
        /// </summary>
        /// <param name = "page">The page.</param>
        /// <returns></returns>
        internal static Control GetPostBackControl(this Page page)
        {
            if (!page.IsPostBack)
                return null;

            Control control = null;

            try
            {
                // first we will check the "__EVENTTARGET" because if post back made by the controls
                // which used "_doPostBack" function also available in Request.Form collection.
                string controlName = page.Request.Params["__EVENTTARGET"];
                if (!string.IsNullOrEmpty(controlName))
                {
                    control = page.FindControl(controlName);
                }
                else
                {
                    // if __EVENTTARGET is null, the control is a button type and we need to
                    // iterate over the form collection to find it

                    // ReSharper disable TooWideLocalVariableScope
                    string controlId;
                    Control foundControl;
                    // ReSharper restore TooWideLocalVariableScope

                    foreach (string ctl in page.Request.Form)
                    {
                        // handle ImageButton they having an additional "quasi-property" 
                        // in their Id which identifies mouse x and y coordinates
                        if (ctl.EndsWith(".x") || ctl.EndsWith(".y"))
                        {
                            controlId = ctl.Substring(0, ctl.Length - 2);
                            foundControl = page.FindControl(controlId);
                        }
                        else
                        {
                            foundControl = page.FindControl(ctl);
                        }

                        if (foundControl is System.Web.UI.WebControls.Button || foundControl is System.Web.UI.WebControls.ImageButton)
                        {
                            control = foundControl;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            { }

            return control;
            //return control == null ? string.Empty : control.ID;
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
