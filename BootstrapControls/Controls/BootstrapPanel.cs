using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BootstrapControls.Controls
{
    //INamingContainer: Any control that implements this interface creates a new namespace in which all child control ID attributes are guaranteed to be unique within an entire application.  

    [ToolboxData("<{0}:BootstrapPanel runat=\"server\"></{0}:BootstrapPanel")]
    [DefaultProperty("Text")]
    [Serializable]
    [ParseChildren(true, "InnerChild")]
    public class BootstrapPanel : WebControl, INamingContainer
    {
        /// <summary>
        /// This will be used to list all the inner controls of this panel.
        /// </summary>
        public Control InnerChild
        {
            set
            {
                this.Controls.Add(value);
            }
        }

        [Category("Appearance")]
        [Browsable(true)]
        [DefaultValue("")]
        [Description("The title to display in this panel.")]
        [Localizable(true)]
        public string Title
        {
            get
            {
                if (ViewState["Title"] != null &&
                    !string.IsNullOrEmpty(ViewState["Title"].ToString()))
                {
                    return ViewState["Title"].ToString();
                }
                return "";
            }
            set
            {
                ViewState["Title"] = value;
            }
        }

        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class=\"panel panel-default\">");
            sb.Append(Environment.NewLine);

            sb.Append("<div class=\"panel-heading\">");
            sb.Append(Environment.NewLine);
            sb.Append(this.Title);
            sb.Append(Environment.NewLine);
            sb.Append("</div>");
            sb.Append(Environment.NewLine);

            sb.Append("<div class=\"panel-body\">");
            sb.Append(Environment.NewLine);

            Literal litEnd = new Literal();
            litEnd.Text = sb.ToString();
            litEnd.RenderControl(writer);
        }

        public override void RenderEndTag(HtmlTextWriter writer)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("</div>");
            sb.Append(Environment.NewLine);
            sb.Append("</div>");
            sb.Append(Environment.NewLine);

            Literal litEnd = new Literal();
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
