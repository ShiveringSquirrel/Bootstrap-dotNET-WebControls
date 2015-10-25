using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BootstrapControls.Controls
{
    [ToolboxData("<{0}:ButtonInput runat=\"server\" Text=\"\" ButtonStyle=\"Default\" />")]
    [DefaultProperty("Text")]
    [ToolboxBitmap(typeof(System.Web.UI.WebControls.Button))]
    public class ButtonInput : Button, IButtonControl
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
                return ViewState.GetPropertyValue("ButtonStyle", Enumerations.ButtonStyle.Default);
            }
            set
            {
                ViewState.SetPropertyValue("ButtonStyle", value);
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
                return ViewState.GetPropertyValue("IsBlock", false);
            }
            set
            {
                ViewState.SetPropertyValue("IsBlock", value);
            }
        }

        [Category("Appearance")]
        [Browsable(true)]
        [DefaultValue("")]
        [Description("The name of the modal control you wish to link to this button")]
        [Localizable(false)]
        public string ModalWindowIdToOpen
        {
            get
            {
                return ViewState.GetPropertyValue("Modal", "");
            }
            set
            {
                ViewState.SetPropertyValue("Modal", value);
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

            string modalLink = "";
            if (this.ModalWindowIdToOpen != null && !string.IsNullOrEmpty(this.ModalWindowIdToOpen))
            {
                if (!this.DesignMode)
                {
                    var mControl = this.Page.FindControlRecursive(this.ModalWindowIdToOpen);
                    if (mControl is Modal)
                    {
                        modalLink = "#" + mControl.ClientID;
                    }
                    else
                    {
                        throw new Exception("Error rendering ButtonInput control: " + this.ModalWindowIdToOpen + " could not be found or is not of the correct Type.");
                    }
                }
            }

            if (!string.IsNullOrEmpty(modalLink))
            {
                this.Attributes.Add("data-toggle", "modal");
                this.Attributes.Add("data-target", modalLink);
                this.OnClientClick = "return false;";
            }

            this.CssClass = cssClass;
            base.Render(writer);

        }

    }
}
