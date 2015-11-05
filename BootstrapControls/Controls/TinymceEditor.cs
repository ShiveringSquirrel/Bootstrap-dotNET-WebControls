using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BootstrapControls.Controls
{
    [ToolboxData("<{0}:TinymceEditor runat=\"server\" Text=\"\" />")]
    [DefaultProperty("Text")]
    [Serializable]
    [ValidationProperty("Text")] //Enables validation on "Text" property
    public class TinymceEditor : TextBox
    {
        [Category("Appearance")]
        [Browsable(true)]
        [DefaultValue("")]
        [Description("The label for this editor.")]
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
        [DefaultValue("")]
        [Description("The text to display in this editor")]
        [Localizable(true)]
        public string Text
        {
            get
            {
                return ViewState.GetPropertyValue("Text", "");
            }
            set
            {
                ViewState.SetPropertyValue("Text", value);
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
        [Description("The theme for this editor.")]
        [Localizable(true)]
        [DefaultValue("modern")]
        public string Theme
        {
            get
            {
                return ViewState.GetPropertyValue("Theme", "modern");
            }
            set
            {
                ViewState.SetPropertyValue("Theme", value);
            }
        }

        [Category("Appearance")]
        [Browsable(true)]
        [Description("The skin to use for this editor.")]
        [Localizable(true)]
        [DefaultValue("lightgray")]
        public string Skin
        {
            get
            {
                return ViewState.GetPropertyValue("Skin", "lightgray");
            }
            set
            {
                ViewState.SetPropertyValue("Skin", value);
            }
        }

        [Category("Appearance")]
        [Browsable(true)]
        [Description("The plugins to load in this editor.")]
        [Localizable(true)]
        [DefaultValue("")]
        public string Plugins
        {
            get
            {
                return ViewState.GetPropertyValue("Plugins", "");
            }
            set
            {
                ViewState.SetPropertyValue("Plugins", value);
            }
        }

        [Category("Appearance")]
        [Browsable(true)]
        [Description("The language to use in this editor.")]
        [Localizable(true)]
        [DefaultValue("en")]
        public string Language
        {
            get
            {
                return ViewState.GetPropertyValue("Language", "en");
            }
            set
            {
                ViewState.SetPropertyValue("Language", value);
            }
        }

        [Category("Appearance")]
        [Browsable(true)]
        [Description("Hides or shows the menu.")]
        [Localizable(false)]
        [DefaultValue("")]
        public bool ShowMenuBar
        {
            get
            {
                return ViewState.GetPropertyValue("ShowMenuBar", true);
            }
            set
            {
                ViewState.SetPropertyValue("ShowMenuBar", value);
            }
        }

        public TinymceEditor()
        {
            this.TextMode = TextBoxMode.MultiLine; //Renders text area
        }

        protected override void Render(HtmlTextWriter writer)
        {
            StringBuilder sb = new StringBuilder();

            //Holds the group class(es)
            string cssGroupClass = "form-group";

            if (!this.IsValid()) //Call the extention method in base to see if this control is valid
            {
                cssGroupClass += " has-error";
            }

            sb.Append("<div class=\"");
            sb.Append(cssGroupClass);
            sb.Append("\">");
            sb.Append(Environment.NewLine);

            if (!string.IsNullOrEmpty(this.Label))
            {
                sb.Append("<label class=\"control-label\"");

                sb.Append(" for=\"");
                sb.Append(this.ClientID);
                sb.Append("\"");

                sb.Append(">");
                sb.Append(this.Label);
                sb.Append("</label>");
                sb.Append(Environment.NewLine);
            }

            TextWriter txtWriter = new StringWriter();
            HtmlTextWriter htmlWriter = new HtmlTextWriter(txtWriter);
            this.Text = System.Web.HttpUtility.HtmlDecode(this.Text); // Fix to show the decoded text
            base.Render(htmlWriter);
            sb.Append(txtWriter);
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

            string initScript = @"
                    tinymce.init(
                    {
                        selector: '#" + this.ClientID + @"',
                        encoding: 'xml',
                        forced_root_block: '',
                        menubar: " + this.ShowMenuBar.ToString().ToLower() + @",
                        theme: '" + this.Theme + @"',
                        plugins: '" + this.Plugins + @"',
                        language: '" + this.Language + @"',
                        skin: '" + this.Skin + @"'
                    });";

            sb.Append("<script>");
            sb.Append(initScript);
            sb.Append("</script>");
            sb.Append(Environment.NewLine);

            Literal litEnd = new Literal();
            litEnd.Text = sb.ToString();
            litEnd.RenderControl(writer);
        }
    }
}
