
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
        [DefaultValue("")]
        [Description("Show a required asterik after the label.")]
        [Localizable(false)]
        public bool Required
        {
            get
            {
                return ViewState.GetPropertyValue("Required", false);
            }
            set
            {
                ViewState.SetPropertyValue("Required", value);
            }
        }

        [Category("Appearance")]
        [Browsable(true)]
        [DefaultValue("fa fa-asterisk text-danger")]
        [Description("What is the icon class to use when field is required.")]
        [Localizable(false)]
        public string RequiredIconClass
        {
            get
            {
                return ViewState.GetPropertyValue("RequiredIconClass", "fa fa-asterisk text-danger");
            }
            set
            {
                ViewState.SetPropertyValue("RequiredIconClass", value);
            }
        }

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
        [Description("The help text to display. Ex: Please fill in a name.")]
        [Localizable(true)]
        [DefaultValue("")]
        public string HelpText
        {
            get
            {
                return ViewState.GetPropertyValue("HelpText", "");
            }
            set
            {
                ViewState.SetPropertyValue("HelpText", value);
            }
        }

        [Category("Appearance")]
        [Browsable(true)]
        [Description("The placeholder to display inside the text. Ex. Your name")]
        [Localizable(true)]
        [DefaultValue("")]
        public string Placeholder
        {
            get
            {
                return ViewState.GetPropertyValue("Placeholder", "");
            }
            set
            {
                ViewState.SetPropertyValue("Placeholder", value);
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
                return ViewState.GetPropertyValue("State", Enumerations.States.Normal);
            }
            set
            {
                ViewState.SetPropertyValue("State", value);
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
                return ViewState.GetPropertyValue("GroupStyle", Enumerations.FormGroupStyle.Normal);
            }
            set
            {
                ViewState.SetPropertyValue("GroupStyle", value);
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
                return ViewState.GetPropertyValue("PrefixText", "");
            }
            set
            {
                ViewState.SetPropertyValue("PrefixText", value);
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
                return ViewState.GetPropertyValue("PrefixImageClass", "");
            }
            set
            {
                ViewState.SetPropertyValue("PrefixImageClass", value);
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
                return ViewState.GetPropertyValue("PostfixText", "");
            }
            set
            {
                ViewState.SetPropertyValue("PostfixText", value);
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
                return ViewState.GetPropertyValue("PostfixImageClass", "");
            }
            set
            {
                ViewState.SetPropertyValue("PostfixImageClass", value);
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
                sb.Append("<label class=\"control-label\"");

                if (this.State != Enumerations.States.Static) //W3C: static will be rendered as a p, lbl cannot have a for property.
                {
                    sb.Append(" for=\"");
                    sb.Append(this.ClientID);
                    sb.Append("\"");
                }

                sb.Append(">");
                sb.Append(this.Label);

                if (this.Required && !string.IsNullOrEmpty(this.RequiredIconClass))
                {
                    sb.Append("&nbsp;<i class=\"");
                    sb.Append(this.RequiredIconClass);
                    sb.Append("\"></i>");
                }

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
