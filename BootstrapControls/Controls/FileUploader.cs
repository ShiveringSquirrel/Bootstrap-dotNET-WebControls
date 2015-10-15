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
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace BootstrapControls.Controls
{
    [ToolboxData("<{0}:FileUploader runat=\"server\" Label=\"\" />")]
    [DefaultProperty("Label")]
    [Serializable]
    public class FileUploader : WebControl
    {
        private HtmlInputFile fileControl;

        [Category("Appearance")]
        [Browsable(true)]
        [DefaultValue("")]
        [Description("The label for this file uploader.")]
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

        public byte[] GetData()
        {
            byte[] myData = new byte[0];
            if (this.fileControl != null && this.fileControl.PostedFile != null)
            {
                var postedFile = this.fileControl.PostedFile;
                int dataLength = postedFile.ContentLength;
                myData = new byte[dataLength];
                postedFile.InputStream.Read(myData, 0, dataLength);
            }
            return myData;
        }

        public FileUploader()
        {
            string resource = "BootstrapControls.Resources.FileUploader.js";
            var _assembly = Assembly.GetExecutingAssembly();

            var _textStreamReader = new StreamReader(_assembly.GetManifestResourceStream(resource));
            string a = _textStreamReader.ReadToEnd();

            ClientScriptManager manager = (HttpContext.Current.Handler as Page).ClientScript;
            if (!manager.IsClientScriptBlockRegistered(manager.GetType(), resource))
            {
                manager.RegisterClientScriptBlock(manager.GetType(), resource, a, true);
            }
        }

        protected override void CreateChildControls()
        {
            fileControl = new HtmlInputFile();
            fileControl.ID = "filecontrol-" + this.ID;
            fileControl.Attributes.Add("type", "file");
            fileControl.Name = "input-file-preview-" + this.ID;
            fileControl.Accept = "image/png, image/jpeg, image/gif";
            fileControl.Attributes.Add("style", "position: absolute; top: 0; right: 0; margin: 0; padding: 0; font-size: 20px; cursor: pointer; opacity: 0; filter: alpha(opacity = 0);");
            Controls.Add(fileControl);
            base.CreateChildControls();
        }

        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            //This will hold all the HTML we want to write as output.
            StringBuilder sb = new StringBuilder();

            //Holds the group class(es)
            string cssGroupClass = "form-group";

            /*if (!this.IsValid()) //Call the extention method in base to see if this control is valid
            {
                cssGroupClass += " has-error";
            }*/

            sb.Append("<div class=\"");
            sb.Append(cssGroupClass);
            sb.Append("\">");

            if (!string.IsNullOrEmpty(this.Label))
            {
                sb.Append("<label class=\"control-label\" for=\"");
                sb.Append(this.ClientID);
                sb.Append("\">");
                sb.Append(this.Label);
                sb.Append("</label>");
                sb.Append(Environment.NewLine);
            }

            sb.Append("<div class=\"input-group image-preview\">");
            sb.Append("<input type=\"text\" class=\"form-control image-preview-filename\" disabled=\"disabled\">");

            sb.Append("<span class=\"input-group-btn\">");
            sb.Append("<button type=\"button\" class=\"btn btn-default image-preview-clear\" style=\"display: none;\">");
            sb.Append("<span class=\"glyphicon glyphicon-remove\"></span>&nbsp;Clear");
            sb.Append("</button > ");

            //style=\"position: relative; overflow: hidden; margin: 0px; color: #333; background-color: #fff; border-color: #ccc;\"
            sb.Append("<div class=\"btn btn-default image-preview-input\">");
            sb.Append("<span class=\"glyphicon glyphicon-folder-open\"></span>");
            sb.Append("<span class=\"image-preview-input-title\" style=\"margin-left: 2px;\">&nbsp;Browse</span>");

            var litEnd = new Literal();
            litEnd.Text = sb.ToString();
            litEnd.RenderControl(writer);
        }

        public override void RenderEndTag(HtmlTextWriter writer)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("</div>");
            sb.Append("</span>");

            sb.Append("</div>");
            sb.Append("</div>");

            var litEnd = new Literal();
            litEnd.Text = sb.ToString();
            litEnd.RenderControl(writer);
        }

        protected override void RenderContents(HtmlTextWriter output)
        {
            this.RenderChildren(output);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Initialize all child controls.
            this.CreateChildControls();
            this.ChildControlsCreated = true;
        }

    }
}
