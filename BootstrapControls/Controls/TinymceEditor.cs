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
        public override string Text
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
        [Description("The html elements that are kept after saving in this editor.")]
        [Localizable(true)]
        [DefaultValue("")]
        public string ValidElements
        {
            get
            {
                return ViewState.GetPropertyValue("ValidElements", "");
            }
            set
            {
                ViewState.SetPropertyValue("ValidElements", value);
            }
        }

        [Category("Appearance")]
        [Browsable(true)]
        [Description("The toolbar items to display.")]
        [Localizable(true)]
        [DefaultValue("")]
        public string Toolbar
        {
            get
            {
                return ViewState.GetPropertyValue("Toolbar", "");
            }
            set
            {
                ViewState.SetPropertyValue("Toolbar", value);
            }
        }

        [Category("Appearance")]
        [Browsable(true)]
        [Description("The second toolbar items to display.")]
        [Localizable(true)]
        [DefaultValue("")]
        public string Toolbar2
        {
            get
            {
                return ViewState.GetPropertyValue("Toolbar2", "");
            }
            set
            {
                ViewState.SetPropertyValue("Toolbar2", value);
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

        [Category("Appearance")]
        [Browsable(true)]
        [Description("Paste as plain text (strip HTML).")]
        [Localizable(false)]
        [DefaultValue("")]
        public bool PasteAsPlainText
        {
            get
            {
                return ViewState.GetPropertyValue("PasteAsPlainText", false);
            }
            set
            {
                ViewState.SetPropertyValue("PasteAsPlainText", value);
            }
        }

        [Category("Appearance")]
        [Browsable(true)]
        [Description("This option allows you to disable the \"Powered by TinyMCE\" branding.")]
        [Localizable(false)]
        [DefaultValue(false)]
        public bool DisableBranding
        {
            get
            {
                return ViewState.GetPropertyValue("DisableBranding", false);
            }
            set
            {
                ViewState.SetPropertyValue("DisableBranding", value);
            }
        }

        [Category("Uploads")]
        [Browsable(true)]
        [Description("Image upload URL.")]
        [Localizable(false)]
        [DefaultValue("")]
        public string ImageUploadUrl
        {
            get
            {
                return ViewState.GetPropertyValue("ImageUploadUrl", "");
            }
            set
            {
                ViewState.SetPropertyValue("ImageUploadUrl", value);
            }
        }

        [Category("Uploads")]
        [Browsable(true)]
        [Description("Disable converting of URLs")]
        [Localizable(false)]
        [DefaultValue(false)]
        public bool ConvertUrls
        {
            get
            {
                return ViewState.GetPropertyValue("ConvertUrls", false);
            }
            set
            {
                ViewState.SetPropertyValue("ConvertUrls", value);
            }
        }        

        [Category("Uploads")]
        [Browsable(true)]
        [Description("This option lets you specify a basepath to prepend to URLs returned from the configured ImageUploadUrl page.")]
        [Localizable(false)]
        [DefaultValue("")]
        public string ImageUploadBasePath
        {
            get
            {
                return ViewState.GetPropertyValue("ImageUploadBasePath", "");
            }
            set
            {
                ViewState.SetPropertyValue("ImageUploadBasePath", value);
            }
        }

        [Category("Uploads")]
        [Browsable(true)]
        [Description("File Picker type ('file image media').")]
        [Localizable(false)]
        [DefaultValue("image")]
        public string FilePickerTypes
        {
            get
            {
                return ViewState.GetPropertyValue("FilePickerTypes", "image");
            }
            set
            {
                ViewState.SetPropertyValue("FilePickerTypes", value);
            }
        }

        [Category("Uploads")]
        [Browsable(true)]
        [Description("Enable or disable automatic upload of images represented by data URLs or blob URIs.")]
        [Localizable(false)]
        [DefaultValue(false)]
        public bool AutomaticUploads
        {
            get
            {
                return ViewState.GetPropertyValue("AutomaticUploads", false);
            }
            set
            {
                ViewState.SetPropertyValue("AutomaticUploads", value);
            }
        }

        [Category("Uploads")]
        [Browsable(true)]
        [Description("his option enables you to add your own file or image browser to TinyMCE.")]
        [Localizable(false)]
        [DefaultValue("")]
        public string FileBrowserCallback
        {
            get
            {
                return ViewState.GetPropertyValue("FileBrowserCallback", "");
            }
            set
            {
                ViewState.SetPropertyValue("FileBrowserCallback", value);
            }
        }

        [Category("Templating")]
        [Browsable(true)]
        [Description("This option lets you specify a predefined list of templates to be inserted by the user into the editable area. ")]
        [Localizable(false)]
        [DefaultValue(default(List<TinymceTemplate>))]
        public List<TinymceTemplate> Templates
        {
            get
            {
                return ViewState.GetPropertyValue("Templates", new List<TinymceTemplate>());
            }
            set
            {
                ViewState.SetPropertyValue("Templates", value);
            }
        }

        [Category("Editing")]
        [Browsable(true)]
        [Description("This option allows you to force 'br' tag as a new line.")]
        [Localizable(false)]
        [DefaultValue(true)]
        public bool ForceBRNewlines
        {
            get
            {
                return ViewState.GetPropertyValue("ForceBRNewlines", true);
            }
            set
            {
                ViewState.SetPropertyValue("ForceBRNewlines", value);
            }
        }

        [Category("Editing")]
        [Browsable(true)]
        [Description("This option allows you to force 'p' tag as a new line.")]
        [Localizable(false)]
        [DefaultValue(false)]
        public bool ForcePNewlines
        {
            get
            {
                return ViewState.GetPropertyValue("ForcePNewlines", false);
            }
            set
            {
                ViewState.SetPropertyValue("ForcePNewlines", value);
            }
        }

        [Category("Configuration")]
        [Browsable(true)]
        [Description("Add a custom configuration script to the init function.")]
        [Localizable(false)]
        [DefaultValue("")]
        public string CustomSetupScript
        {
            get
            {
                return ViewState.GetPropertyValue("CustomSetupScript", "");
            }
            set
            {
                ViewState.SetPropertyValue("CustomSetupScript", value);
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

                if (this.Required && !string.IsNullOrEmpty(this.RequiredIconClass))
                {
                    sb.Append("&nbsp;<i class=\"");
                    sb.Append(this.RequiredIconClass);
                    sb.Append("\"></i>");
                }

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

            var templateStr = "";
            if (this.Templates.Count > 0)
            {
                foreach (var template in this.Templates)
                {
                    templateStr += template.ToString() + ",";
                }
                templateStr = templateStr.Trim(',');
                templateStr = "[" + templateStr + "]";
            }

            string initScript = @"
                    tinymce.init(
                    {
                        selector: '#" + this.ClientID + @"',
                        encoding: 'xml',
                        entity_encoding : 'raw',
                        convert_urls: " + this.ConvertUrls.ToString().ToLower() + @",
                        force_br_newlines : " + this.ForceBRNewlines.ToString().ToLower() + @",
                        force_p_newlines : " + this.ForcePNewlines.ToString().ToLower() + @",
                        fix_list_elements : true,"
                        + ((!string.IsNullOrEmpty(this.Toolbar)) ? "toolbar1: '" + this.Toolbar + "'," : "")
                        + ((!string.IsNullOrEmpty(this.Toolbar2)) ? "toolbar2: '" + this.Toolbar2 + "'," : "")
                        + ((!string.IsNullOrEmpty(this.CustomSetupScript)) ? "setup: " + this.CustomSetupScript + "," : "") +
                        @"forced_root_block: '',"
                        + ((!string.IsNullOrEmpty(this.ValidElements)) ? "valid_elements : '" + this.ValidElements + "'," : "") +
                        @"menubar: " + this.ShowMenuBar.ToString().ToLower() + @",
                        theme: '" + this.Theme + @"',
                        plugins: '" + this.Plugins + @"',
                        language: '" + this.Language + @"',
                        file_picker_types: '" + this.FilePickerTypes + @"', 
                        branding: " + (!this.DisableBranding).ToString().ToLower() + @",
                        automatic_uploads: '" + this.AutomaticUploads.ToString().ToLower() + @"',
                        paste_as_text: " + this.PasteAsPlainText.ToString().ToLower() + @", "
                        + ((!string.IsNullOrEmpty(this.FileBrowserCallback)) ? "file_browser_callback: " + this.FileBrowserCallback + ", " : "")
                        + ((!string.IsNullOrEmpty(this.ImageUploadUrl)) ? "images_upload_url: '" + this.ImageUploadUrl + "', " : "")
                        + ((!string.IsNullOrEmpty(templateStr)) ? "templates: " + templateStr + ", " : "")
                        + ((!string.IsNullOrEmpty(ImageUploadBasePath)) ? "images_upload_base_path: '" + this.ImageUploadBasePath + "', " : "") +
                        @"skin: '" + this.Skin + @"'
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
