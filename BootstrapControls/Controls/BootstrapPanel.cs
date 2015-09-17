using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BootstrapControls.Controls
{
    //INamingContainer: Any control that implements this interface creates a new namespace in which all child control ID attributes are guaranteed to be unique within an entire application.  

    [ToolboxData("<{0}:BootstrapPanel runat=\"server\"></{0}:BootstrapPanel")]
    [DefaultProperty("Title")]
    [ParseChildren(true, "Content")]
    public class BootstrapPanel : WebControl, INamingContainer
    {
        [TemplateContainer(typeof(BootstrapPanel))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TemplateInstance(TemplateInstance.Single)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual ITemplate Content
        {
            get;
            set;
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
                return ViewState.GetPropertyValue("Title", "");
            }
            set
            {
                ViewState.SetPropertyValue("Title", value);
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

        protected override void CreateChildControls()
        {
            // Remove any controls
            this.Controls.Clear();

            // Add all content to a container.
            var container = new Panel();
            //container.ID = Guid.NewGuid().ToString("N");
            this.Content.InstantiateIn(container);

            // Add container to the control collection.
            this.Controls.Add(container);
        }

        protected override void OnInit(System.EventArgs e)
        {
            base.OnInit(e);

            // Initialize all child controls.
            this.CreateChildControls();
            this.ChildControlsCreated = true;
        }
    }
}
