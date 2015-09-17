using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms.VisualStyles;

namespace BootstrapControls.Controls
{
    [ToolboxData("<{0}:TabPage runat=\"server\" Title=\"\"></{0}:TabPage>")]
    [ToolboxItem(false)]
    [DefaultProperty("Title")]
    [ParseChildren(true, "Content")]
    public class TabPage : WebControl, INamingContainer
    {
        private ITemplate contentTemplate = null;

        [TemplateContainer(typeof(TabPage))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TemplateInstance(TemplateInstance.Single)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual ITemplate Content
        {
            get;
            set;
        }

        [NotifyParentProperty(true)]
        [Browsable(true)]
        [Localizable(true)]
        [DefaultValue("")]
        [Description("The title to display of this tab.")]
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

        [NotifyParentProperty(true)]
        [Browsable(true)]
        [Localizable(false)]
        [DefaultValue(false)]
        public bool IsActive
        {
            get
            {
                return ViewState.GetPropertyValue("IsActive", false);
            }
            set
            {
                ViewState.SetPropertyValue("IsActive", value);
            }
        }

        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            string cssClasss = "tab-pane";

            if (IsActive)
            {
                cssClasss += " active";
            }

            StringBuilder sb = new StringBuilder();

            sb.Append("<div role=\"tabpanel\" class=\"" + cssClasss + "\" id=\"" + this.ClientID + "\">");
            sb.Append(Environment.NewLine);

            Literal litBegin = new Literal();
            litBegin.Text = sb.ToString();
            litBegin.RenderControl(writer);
        }

        public override void RenderEndTag(HtmlTextWriter writer)
        {
            StringBuilder sb = new StringBuilder();

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
