using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BootstrapControls.Controls
{
    [ToolboxData("<{0}:DateTimePickerInput runat=\"server\" Text=\"\" Label=\"\" Placeholder=\"\" State=\"Normal\" />")]
    [DefaultProperty("DateTimeValue")]
    [Serializable]
    [ValidationProperty("DateTimeValue")] //Enables validation on "Text" property
    public class DateTimePickerInput : TextBox
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
        [Localizable(true)]
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
        [Localizable(true)]
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

        private Guid InternalId
        {
            get
            {
                if (ViewState["InternalId"] != null &&
                    ViewState["InternalId"] is Guid)
                {
                    return (Guid)ViewState["InternalId"];
                }
                ViewState["InternalId"] = Guid.NewGuid();
                return this.InternalId;
            }
            set
            {
                ViewState["InternalId"] = value;
            }
        }

        [Category("Date and Time")]
        [Browsable(true)]
        [DefaultValue("")]
        [Description("The date to show in the control")]
        [Localizable(false)]
        public DateTime? DateTimeValue
        {
            get
            {
                if (!string.IsNullOrEmpty(this.Text))
                {
                    return DateTime.Parse(this.Text);
                }
                return null;
            }
            set
            {
                if (value.HasValue)
                {
                    this.Text = value.Value.ToString(this.DateTimeMask.Replace("D", "d").Replace("Y", "y"));
                }
            }
        }

        [Category("Date and Time")]
        [Browsable(true)]
        [DefaultValue("DD/MM/YYYY HH:mm:ss")]
        [Description("The mask to apply to the datetime value")]
        [Localizable(false)]
        public string DateTimeMask
        {
            get
            {
                if (ViewState["DateTimeMask"] != null &&
                    !string.IsNullOrEmpty(ViewState["DateTimeMask"].ToString()))
                {
                    return ViewState["DateTimeMask"].ToString();
                }
                return "DD/MM/YYYY HH:mm:ss";
            }
            set
            {
                ViewState["DateTimeMask"] = value;
            }
        }

        [Category("Date and Time")]
        [Browsable(true)]
        [DefaultValue("nl-BE")]
        [Description("The local language to apply to the date time picker")]
        [Localizable(false)]
        public string Language
        {
            get
            {
                if (ViewState["Language"] != null &&
                    !string.IsNullOrEmpty(ViewState["Language"].ToString()))
                {
                    return ViewState["Language"].ToString();
                }
                return "nl-BE";
            }
            set
            {
                ViewState["Language"] = value;
            }
        }

        //[Category("Date and Time")]
        //[Browsable(true)]
        //[DefaultValue("True")]
        //[Description("Do we offer a time selection?")]
        //[Localizable(false)]
        //public bool PickTime
        //{
        //    get
        //    {
        //        if (ViewState["PickTime"] != null &&
        //            ViewState["PickTime"] is bool)
        //        {
        //            return (bool)ViewState["PickTime"];
        //        }
        //        return true;
        //    }
        //    set
        //    {
        //        ViewState["PickTime"] = value;
        //    }
        //}

        //[Category("Date and Time")]
        //[Browsable(true)]
        //[DefaultValue("True")]
        //[Description("Do we offer a date selection?")]
        //[Localizable(false)]
        //public bool PickDate
        //{
        //    get
        //    {
        //        if (ViewState["PickDate"] != null &&
        //            ViewState["PickDate"] is bool)
        //        {
        //            return (bool)ViewState["PickDate"];
        //        }
        //        return true;
        //    }
        //    set
        //    {
        //        ViewState["PickDate"] = value;
        //    }
        //}

        public DateTimePickerInput()
        {
            string resource = "BootstrapControls.Resources.DateTimePickerInput.js";
            var _assembly = Assembly.GetExecutingAssembly();
            var _textStreamReader = new StreamReader(_assembly.GetManifestResourceStream(resource));
            string a = _textStreamReader.ReadToEnd();

            ClientScriptManager manager = (HttpContext.Current.Handler as Page).ClientScript;
            if (!manager.IsClientScriptBlockRegistered(manager.GetType(), resource))
                manager.RegisterClientScriptBlock(manager.GetType(), resource, a, true);
        }

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
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

            sb.Append("<div class=\"input-group date\" id=\"");
            sb.Append(this.InternalId);
            sb.Append("\">");
            sb.Append(Environment.NewLine);

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

            sb.Append("<span class=\"input-group-addon\">");
            sb.Append("<span class=\"glyphicon glyphicon-calendar\"></span>");
            sb.Append("</span>");

            sb.Append("</div>");
            sb.Append(Environment.NewLine);

            if (!string.IsNullOrEmpty(this.HelpText))
            {
                sb.Append("<p class=\"help-block\">");
                sb.Append(this.HelpText);
                sb.Append("</p>");
                sb.Append(Environment.NewLine);
            }

            sb.Append("</div>");
            sb.Append(Environment.NewLine);

            sb.Append("<script type=\"text/javascript\">");
            sb.Append("CreateDateTimePicker($(\"#" + this.InternalId + "\"),\"" + this.Language + "\", \"" + this.DateTimeMask + "\");");
            sb.Append("</script>");

            Literal litEnd = new Literal();
            litEnd.Text = sb.ToString();
            litEnd.RenderControl(writer);


        }
    }
}
