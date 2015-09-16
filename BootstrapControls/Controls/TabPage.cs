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
    [Serializable]
    //[ParseChildren(ChildrenAsProperties = true)]
    //[ParseChildren(true, "TabControls")]
    //[ParseChildren(typeof(Control), DefaultProperty = "TabContent", ChildrenAsProperties = true)]
    public class TabPage : Panel, INamingContainer
    {
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        //[PersistenceMode(PersistenceMode.InnerProperty)]
        //public Control TabControls { get; set; }

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

        public override void RenderControl(HtmlTextWriter writer)
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

            /*foreach (Control c in InnerControls)
            {
                TabControls.RenderControl(htmlWriter);
            }*/

            base.RenderChildren(writer);

            sb = new StringBuilder();

            sb.Append("</div>");
            sb.Append(Environment.NewLine);

            Literal litEnd = new Literal();
            litEnd.Text = sb.ToString();
            litEnd.RenderControl(writer);
        }

        //public override void RenderBeginTag(HtmlTextWriter writer)
        //{
        //    string cssClasss = "tab-pane";

        //    if (IsActive)
        //    {
        //        cssClasss += " active";
        //    }

        //    StringBuilder sb = new StringBuilder();

        //    sb.Append("<div role=\"tabpanel\" class=\"" + cssClasss + "\" id=\"" + this.ClientID + "\">");
        //    sb.Append(Environment.NewLine);

        //    Literal litBegin = new Literal();
        //    litBegin.Text = sb.ToString();
        //    litBegin.RenderControl(writer);
        //}

        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        //[PersistenceMode(PersistenceMode.InnerProperty)]
        //public List<Control> TabContent
        //{
        //    get
        //    {
        //        List<Control> controls = new List<Control>();
        //        foreach (Control control in this.Controls)
        //        {
        //            controls.Add(control);
        //        }
        //        return controls;
        //    }
        //    set
        //    {
        //        foreach (var control in value)
        //        {
        //            this.Controls.Add(control);
        //        }
        //    }
        //}

        ////[TemplateContainer(typeof(TemplateControl))]
        ////[PersistenceMode(PersistenceMode.InnerProperty)]
        ////[TemplateInstance(TemplateInstance.Single)]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        //[PersistenceMode(PersistenceMode.InnerProperty)]
        //public List<Control> TabContent
        //{
        //    get { return this.InnerControls; }
        //    set { this.InnerControls = value; }
        //    //{
        //    //    if (value is Control)
        //    //    {
        //    //        this.Controls.Add(value.Controls[0]);
        //    //        EnsureChildControls();
        //    //    }
        //    //}
        //}

        //public override void RenderEndTag(HtmlTextWriter writer)
        //{
        //    StringBuilder sb = new StringBuilder();

        //    sb.Append("</div>");
        //    sb.Append(Environment.NewLine);

        //    Literal litEnd = new Literal();
        //    litEnd.Text = sb.ToString();
        //    litEnd.RenderControl(writer);
        //}

        //protected override void RenderContents(HtmlTextWriter output)
        //{
        //    this.RenderChildren(output);
        //}
    }
}
