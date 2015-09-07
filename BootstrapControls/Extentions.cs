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
    }
}
