using System;
using System.ComponentModel;
using System.IO;
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
        private Switch bSwitch;

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

        [Category("Appearance")]
        [Browsable(true)]
        [DefaultValue(false)]
        [Description("Does this panel have a button to collapse it.")]
        [Localizable(false)]
        public bool CreateHideButton
        {
            get
            {
                return ViewState.GetPropertyValue("CreateHideButton", false);
            }
            set
            {
                ViewState.SetPropertyValue("CreateHideButton", value);
            }
        }

        [Category("Appearance")]
        [Browsable(true)]
        [DefaultValue(false)]
        [Description("Only works with hide button enabled. This specifies if the panel is initially hidden.")]
        [Localizable(false)]
        public bool PanelIsInitiallyHidden
        {
            get
            {
                return ViewState.GetPropertyValue("PanelIsInitiallyHidden", false);
            }
            set
            {
                ViewState.SetPropertyValue("PanelIsInitiallyHidden", value);
            }
        }

        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class=\"panel panel-default\">");
            sb.Append(Environment.NewLine);

            sb.Append("<div class=\"panel-heading\">");
            sb.Append(Environment.NewLine);

            Guid gPanel = Guid.NewGuid();
            //Guid gSwitch = Guid.NewGuid();

            if (CreateHideButton)
            {
                sb.Append(this.Title);
                sb.Append("<div class=\"btn-group pull-right\"  style=\"margin-top: -5px\">");

                bSwitch = new Switch();
                bSwitch.Size = Enumerations.SwitchSize.Small;
                bSwitch.Checked = !PanelIsInitiallyHidden;

                bSwitch.OffText = "<i class=\"glyphicon glyphicon-remove\">";
                bSwitch.OnText = "<i class=\"glyphicon glyphicon-ok\">";
                this.Controls.Add(bSwitch);

                TextWriter txtWriter = new StringWriter();
                HtmlTextWriter htmlWriter = new HtmlTextWriter(txtWriter);
                bSwitch.RenderControl(htmlWriter);
                sb.Append(txtWriter);

                sb.Append("<script type=\"text/javascript\">");
                sb.Append("$(window).load(function() {");
                sb.Append("if($('#" + bSwitch.ClientID + "').attr('checked')){");
                sb.Append("$('#" + gPanel + "').show();");
                sb.Append("}");
                sb.Append("else{");
                sb.Append("$('#" + gPanel + "').hide();");
                sb.Append("}");
                sb.Append("});");
                sb.Append("$('#" + bSwitch.ClientID + "').on('switchChange.bootstrapSwitch', function(event, state) {");
                sb.Append("if(state){");
                sb.Append("$('#" + gPanel + "').slideDown(500);");
                sb.Append("}");
                sb.Append("else{");
                sb.Append("$('#" + gPanel + "').slideUp(500);");
                sb.Append("}");
                sb.Append("});");
                sb.Append("</script>");

                sb.Append("</div>");
            }
            else
            {
                sb.Append(this.Title);
            }

            sb.Append(Environment.NewLine);
            sb.Append("</div>");
            sb.Append(Environment.NewLine);

            sb.Append("<div class=\"panel-body\" id=\"" + gPanel + "\">");
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

        protected override void RenderChildren(HtmlTextWriter writer)
        {
            if (bSwitch != null)
            {
                this.Controls.Remove(bSwitch);
                base.RenderChildren(writer);
                this.Controls.Add(bSwitch);
            }
            else
            {
                base.RenderChildren(writer);
            }
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
