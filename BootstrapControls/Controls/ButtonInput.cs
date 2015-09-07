using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BootstrapControls.Controls
{
    [ToolboxData("<{0}:ButtonInput runat=\"server\" Text=\"\" ButtonStyle=\"Default\" />")]
    [DefaultProperty("Text")]
    [Serializable]
    public class ButtonInput : Button
    {
        [Category("Appearance")]
        [Browsable(true)]
        [DefaultValue("Default")]
        [Description("The style of the button.")]
        [Localizable(false)]
        public Enumerations.ButtonStyle ButtonStyle
        {
            get
            {
                if (ViewState["ButtonStyle"] is Enumerations.ButtonStyle)
                {
                    return (Enumerations.ButtonStyle)ViewState["ButtonStyle"];
                }
                return Enumerations.ButtonStyle.Default;
            }
            set
            {
                ViewState["ButtonStyle"] = value;
            }
        }

        [Category("Appearance")]
        [Browsable(true)]
        [DefaultValue("False")]
        [Description("Does this button have a block style (btn-block)")]
        [Localizable(false)]
        public bool IsBlock
        {
            get
            {
                if (ViewState["IsBlock"] is bool)
                {
                    return (bool)ViewState["IsBlock"];
                }
                return false;
            }
            set
            {
                ViewState["IsBlock"] = value;

            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            string cssClass = "btn ";

            switch (ButtonStyle)
            {
                case Enumerations.ButtonStyle.Default:
                    cssClass += "btn-default";
                    break;
                case Enumerations.ButtonStyle.Primary:
                    cssClass += "btn-primary";
                    break;
                case Enumerations.ButtonStyle.Success:
                    cssClass += "btn-success";
                    break;
                case Enumerations.ButtonStyle.Info:
                    cssClass += "btn-info";
                    break;
                case Enumerations.ButtonStyle.Warning:
                    cssClass += "btn-warning";
                    break;
                case Enumerations.ButtonStyle.Danger:
                    cssClass += "btn-danger";
                    break;
                case Enumerations.ButtonStyle.Link:
                    cssClass += "btn-link";
                    break;
            }

            if (IsBlock)
            {
                cssClass += " btn-block";
            }

            this.CssClass = cssClass;
            base.Render(writer);

        }

    }
}
