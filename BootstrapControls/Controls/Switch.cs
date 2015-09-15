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
    [ToolboxData("<{0}:Switch runat=\"server\" />")]
    public class Switch : CheckBox
    {
        [Category("Appearance")]
        [Browsable(true)]
        [DefaultValue("")]
        [Description("The label for this text. Ex: Name.")]
        [Localizable(true)]
        public string Label
        {
            get
            {
                return ViewState.GetPropertyValue("Label", "");
            }
            set
            {
                ViewState.SetPropertyValue("Label", value);
            }
        }

        [Category("Appearance")]
        [Browsable(true)]
        [DefaultValue("Normal")]
        [Description("The size of this switch element")]
        [Localizable(false)]
        public Enumerations.SwitchSize Size
        {
            get
            {
                return ViewState.GetPropertyValue("Size", Enumerations.SwitchSize.Normal);
            }
            set
            {
                ViewState.SetPropertyValue("Size", value);
            }
        }

        [Category("Appearance")]
        [Browsable(true)]
        [DefaultValue("On")]
        [Description("The text to display when the control is checked")]
        [Localizable(true)]
        public string OnText
        {
            get
            {
                return ViewState.GetPropertyValue("OnText", "On");
            }
            set
            {
                ViewState.SetPropertyValue("OnText", value);
            }
        }

        [Category("Appearance")]
        [Browsable(true)]
        [DefaultValue("Off")]
        [Description("The text to display when the control is unchecked")]
        [Localizable(true)]
        public string OffText
        {
            get
            {
                return ViewState.GetPropertyValue("OffText", "Off");
            }
            set
            {
                ViewState.SetPropertyValue("OffText", value);
            }
        }

        [Category("Appearance")]
        [Browsable(true)]
        [DefaultValue("False")]
        [Description("Is this switch disabled?")]
        [Localizable(false)]
        public bool Disabled
        {
            get
            {
                return ViewState.GetPropertyValue("Disabled", false);
            }
            set
            {
                ViewState.SetPropertyValue("Disabled", value);
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {

            writer.WriteLine("<div class=\"form-group\">");

            if (!string.IsNullOrEmpty(this.Label))
            {
                writer.WriteLine("<label class=\"control-label\" for=\"" + this.ClientID + "\">");
                writer.WriteLine(this.Label);
                writer.WriteLine("</label>");
            }

            writer.WriteLine("<div class=\"input-group\">");

            writer.AddAttribute("data-size", this.Size.ToString().ToLower());
            writer.AddAttribute("data-on-text", this.OnText);
            writer.AddAttribute("data-off-text", this.OffText);
            writer.AddAttribute("data-label-text", this.Text);
            if (this.Disabled)
            {
                writer.AddAttribute("disabled", null);
            }
            base.Render(writer);

            writer.WriteLine("</div>");
            writer.WriteLine("</div>");

            writer.WriteLine("<script type=\"text/javascript\">$(window).load(function() { $(\"#" + this.ClientID +
                "\").bootstrapSwitch(); })</script>");
        }
    }
}
