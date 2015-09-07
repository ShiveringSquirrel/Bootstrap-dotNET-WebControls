
using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BootstrapControls.Controls
{
    [ToolboxData("<{0}:TextInput runat=\"server\" Text=\"\" Label=\"\" Placeholder=\"\" State=\"Normal\" />")]
    [DefaultProperty("Text")]
    [Serializable]
    [ValidationProperty("Text")] //Enables validation on "Text" property
    public class TextInput : TextBox
    {
        [Category("Appearance")]
        [Browsable(true)]
        [DefaultValue("Text label")]
        [Description("The label for this text. Ex: Name.")]
        [Localizable(true)]
        public string Label
        {
            get
            {
                if (ViewState["Label"] != null &&
                    !string.IsNullOrEmpty(ViewState["Label"].ToString()))
                {
                    return ViewState["Label"].ToString();
                }
                return "";
            }
            set
            {
                ViewState["Label"] = value;
            }
        }

        [Category("Appearance")]
        [Browsable(true)]
        [Description("The help text to display. Ex: please fill in a name.")]
        [Localizable(true)]
        [DefaultValue("")]
        public string HelpText
        {
            get
            {
                if (ViewState["HelpText"] != null &&
                    !string.IsNullOrEmpty(ViewState["HelpText"].ToString()))
                {
                    return ViewState["HelpText"].ToString();
                }
                return "";
            }
            set
            {
                ViewState["HelpText"] = value;
            }
        }

        [Category("Appearance")]
        [Browsable(true)]
        [Description("The placeholder to display inside the text. Ex. The name")]
        [Localizable(true)]
        [DefaultValue("")]
        public string Placeholder
        {
            get
            {
                if (ViewState["Placeholder"] != null &&
                    !string.IsNullOrEmpty(ViewState["Placeholder"].ToString()))
                {
                    return ViewState["Placeholder"].ToString();
                }
                return "";
            }
            set
            {
                ViewState["Placeholder"] = value;
            }
        }

        [Category("Appearance")]
        [Browsable(true)]
        [DefaultValue("Normal")]
        [Description("What is the state of this control. Ex Disabled")]
        [Localizable(false)]
        public Enumerations.States State
        {
            get
            {
                if (ViewState["Type"] is Enumerations.States)
                {
                    return (Enumerations.States)ViewState["Type"];
                }
                return Enumerations.States.Normal;
            }
            set
            {
                ViewState["Type"] = value;
            }
        }

        [Category("Appearance")]
        [Browsable(true)]
        [DefaultValue("Normal")]
        [Description("The style of the form-group element.")]
        [Localizable(false)]
        public Enumerations.FormGroupStyle GroupStyle
        {
            get
            {
                if (ViewState["GroupStyle"] is Enumerations.FormGroupStyle)
                {
                    return (Enumerations.FormGroupStyle)ViewState["GroupStyle"];
                }
                return Enumerations.FormGroupStyle.Normal;
            }
            set
            {
                ViewState["GroupStyle"] = value;
            }
        }

        [Category("Prefix")]
        [Browsable(true)]
        [DefaultValue("")]
        [Description("The text to display in a group addon as a prefix")]
        [Localizable(true)]
        public string PrefixText
        {
            get
            {
                if (ViewState["PrefixText"] != null &&
                    !string.IsNullOrEmpty(ViewState["PrefixText"].ToString()))
                {
                    return ViewState["PrefixText"].ToString();
                }
                return "";
            }
            set
            {
                ViewState["PrefixText"] = value;
            }
        }

        [Category("Prefix")]
        [Browsable(true)]
        [DefaultValue("")]
        [Description("The image class(es) to display in a group addon as a prefix")]
        [Localizable(false)]
        public string PrefixImageClass
        {
            get
            {
                if (ViewState["PrefixImageClass"] != null &&
                    !string.IsNullOrEmpty(ViewState["PrefixImageClass"].ToString()))
                {
                    return ViewState["PrefixImageClass"].ToString();
                }
                return "";
            }
            set
            {
                ViewState["PrefixImageClass"] = value;
            }
        }

        [Category("Postfix")]
        [Browsable(true)]
        [DefaultValue("")]
        [Description("The text to display in a group addon as a postfix")]
        [Localizable(true)]
        public string PostfixText
        {
            get
            {
                if (ViewState["PostfixText"] != null &&
                    !string.IsNullOrEmpty(ViewState["PostfixText"].ToString()))
                {
                    return ViewState["PostfixText"].ToString();
                }
                return "";
            }
            set
            {
                ViewState["PostfixText"] = value;
            }
        }

        [Category("Postfix")]
        [Browsable(true)]
        [DefaultValue("")]
        [Description("The image class(es) to display in a group addon as a postfix")]
        [Localizable(false)]
        public string PostfixImageClass
        {
            get
            {
                if (ViewState["PostfixImageClass"] != null &&
                    !string.IsNullOrEmpty(ViewState["PostfixImageClass"].ToString()))
                {
                    return ViewState["PostfixImageClass"].ToString();
                }
                return "";
            }
            set
            {
                ViewState["PostfixImageClass"] = value;
            }
        }

        public TextInput()
        {
            this.Placeholder = "";
        }

        protected override void Render(HtmlTextWriter writer)
        {
            bool renderPrefix = (!string.IsNullOrEmpty(this.PrefixText) ||
                !string.IsNullOrEmpty(this.PrefixImageClass)) && (this.State != Enumerations.States.Static);

            bool renderPostix = (!string.IsNullOrEmpty(this.PostfixText) ||
               !string.IsNullOrEmpty(this.PostfixImageClass)) && (this.State != Enumerations.States.Static);

            bool renderInnerGroup = (renderPrefix || renderPostix);

            //This will hold all the HTML we want to write as output.
            StringBuilder sb = new StringBuilder();

            //Holds the group class(es)
            string cssGroupClass = "form-group";

            if (!this.IsValid()) //Call the extention method in base to see if this control is valid
            {
                cssGroupClass += " has-error";
            }
            else if (this.GroupStyle != Enumerations.FormGroupStyle.Normal)
            {
                switch (this.GroupStyle)
                {
                    case Enumerations.FormGroupStyle.Success: cssGroupClass += " has-success";
                        break;
                    case Enumerations.FormGroupStyle.Warning: cssGroupClass += " has-warning";
                        break;
                    case Enumerations.FormGroupStyle.Error: cssGroupClass += " has-error";
                        break;
                }
            }

            sb.Append("<div class=\"");
            sb.Append(cssGroupClass);
            sb.Append("\">");
            sb.Append(Environment.NewLine);

            if (!string.IsNullOrEmpty(this.Label))
            {
                sb.Append("<label class=\"control-label\" for=\"");
                sb.Append(this.ClientID);
                sb.Append("\">");
                sb.Append(this.Label);
                sb.Append("</label>");
                sb.Append(Environment.NewLine);
            }

            // Should I start an inner group ...
            if (renderInnerGroup)
            {
                sb.Append("<div class=\"input-group\">");
                sb.Append(Environment.NewLine);

                // Should I render a prefix ...
                if (renderPrefix)
                {
                    sb.Append("<span class=\"input-group-addon\">");

                    if (!string.IsNullOrEmpty(this.PrefixText))
                    {
                        sb.Append(this.PrefixText);
                    }
                    else if (!string.IsNullOrEmpty(this.PrefixImageClass))
                    {
                        sb.Append("<i class=\"");
                        sb.Append(this.PrefixImageClass);
                        sb.Append("\"></i>");
                    }

                    sb.Append("</span>");
                    sb.Append(Environment.NewLine);
                }
            }

            if (this.State == Enumerations.States.Static)
            {
                sb.Append("<p class=\"form-control-static\" ");
                sb.Append("name=\"");
                sb.Append(this.ClientID);
                sb.Append("\" id=\"");
                sb.Append(this.ClientID);
                sb.Append("\">");
                sb.Append(this.Text);
                sb.Append("</p>");
                sb.Append(Environment.NewLine);
            }
            else
            {
                //Render the base control using a new html writer, which in turn uses a 
                //text writer so we can capture the html and store it in our string builder

                TextWriter txtWriter = new StringWriter();
                HtmlTextWriter htmlWriter = new HtmlTextWriter(txtWriter);

                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Class, "form-control");

                if (!String.IsNullOrEmpty(this.Placeholder))
                {
                    htmlWriter.AddAttribute("placeholder", this.Placeholder);
                }

                if (this.State == Enumerations.States.Disabled)
                {
                    htmlWriter.AddAttribute("disabled", "");
                }

                base.Render(htmlWriter);
                sb.Append(txtWriter);
                sb.Append(Environment.NewLine);
            }

            // Should I render an inner group (used to close it if it was started) ...
            if (renderInnerGroup)
            {
                if (renderPostix) //Before closing the inner group, should I write a postfix ...
                {
                    sb.Append("<span class=\"input-group-addon\">");

                    if (!string.IsNullOrEmpty(this.PostfixText))
                    {
                        sb.Append(this.PostfixText);
                    }
                    else if (!string.IsNullOrEmpty(this.PostfixImageClass))
                    {
                        sb.Append("<i class=\"");
                        sb.Append(this.PostfixImageClass);
                        sb.Append("\"></i>");
                    }

                    sb.Append("</span>");
                    sb.Append(Environment.NewLine);
                }

                sb.Append("</div>");
                sb.Append(Environment.NewLine);
            }

            if (!string.IsNullOrEmpty(this.HelpText))
            {
                sb.Append("<p class=\"help-block\">");
                sb.Append(this.HelpText);
                sb.Append("</p>");
                sb.Append(Environment.NewLine);
            }

            sb.Append("</div>");
            sb.Append(Environment.NewLine);

            Literal litEnd = new Literal();
            litEnd.Text = sb.ToString();
            litEnd.RenderControl(writer);
        }
    }
}
