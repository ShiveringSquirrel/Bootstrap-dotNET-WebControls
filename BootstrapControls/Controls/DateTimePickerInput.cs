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
        [DefaultValue("")]
        [Description("The label for this text. Ex: Birthdate.")]
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
        [Description("The help text to display. Ex: Please fill in a Birthdate.")]
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
        [Description("The placeholder to display inside the text. Ex. Your birthdate")]
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

        public Guid InternalId
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
            internal set
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
                return ViewState.GetPropertyValue("DateTimeValue", new DateTime?());
            }
            set
            {
                if (value.HasValue)
                {
                    this.Text = value.Value.ToString(this.DateTimeMask.Replace("D", "d").Replace("Y", "y"));
                    ViewState.SetPropertyValue("DateTimeValue", value);
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
                return ViewState.GetPropertyValue("DateTimeMask", "DD/MM/YYYY HH:mm:ss");
            }
            set
            {
                ViewState.SetPropertyValue("DateTimeMask", value);
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
                return ViewState.GetPropertyValue("Language", "nl-BE");
            }
            set
            {
                ViewState.SetPropertyValue("Language", value);
            }
        }

        [Category("Date and Time")]
        [Browsable(true)]
        [DefaultValue(null)]
        [Description("The linked DateTimePicker. This reference will be used as the maximum date while the current DateTimePicker will be used as the minimum date.")]
        [Localizable(false)]
        [ThemeableAttribute(false)]
        //[TypeConverterAttribute(typeof(ValidatedControlConverter))]
        public string DateTimePickerUsedAsMax
        {
            get
            {                
                return ViewState.GetPropertyValue<string>("DateTimePickerUsedAsMax", null);
            }
            set
            {
                ViewState.SetPropertyValue("DateTimePickerUsedAsMax", value);
            }
        }

        public DateTimePickerInput()
        {
            string resource = "BootstrapControls.Resources.DateTimePickerInput.js";
            var _assembly = Assembly.GetExecutingAssembly();
            var _textStreamReader = new StreamReader(_assembly.GetManifestResourceStream(resource));
            string a = _textStreamReader.ReadToEnd();

            ClientScriptManager manager = (HttpContext.Current.Handler as Page).ClientScript;
            if (!manager.IsClientScriptBlockRegistered(manager.GetType(), resource))
            {
                manager.RegisterClientScriptBlock(manager.GetType(), resource, a, true);
            }
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

            string linkedDtp = "null";
            if(this.DateTimePickerUsedAsMax != null && !string.IsNullOrEmpty(this.DateTimePickerUsedAsMax))
            {
                var dtpControl = this.Page.FindControlRecursive(this.DateTimePickerUsedAsMax);
                if (dtpControl is DateTimePickerInput)
                {
                    linkedDtp = "$(\"#" + (dtpControl as DateTimePickerInput).InternalId + "\")";
                }
                else
                {
                    throw new Exception("Error rendering DateTimePicker control: " + this.DateTimePickerUsedAsMax + " could not be found or is not of the correct Type.");
                }
            }

            sb.Append("<script type=\"text/javascript\">");
            sb.Append("$(window).load(function() { CreateDateTimePicker($(\"#" + this.InternalId + "\"),\"" + this.Language + "\", \"" + this.DateTimeMask + "\", " + linkedDtp + "); })");
            sb.Append("</script>");

            Literal litEnd = new Literal();
            litEnd.Text = sb.ToString();
            litEnd.RenderControl(writer);


        }
    }
}
