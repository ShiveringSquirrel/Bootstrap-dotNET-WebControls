using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BootstrapControls.Controls
{
    [ToolboxData("<{0}:TabControl runat=server></{0}:TabControl>")]
    [ToolboxItem(true)]
    //[ToolboxBitmap(typeof(System.Web.UI.WebControls.Image))]
    [ParseChildren(true, "TabPages")]
    //[PersistChildren(true)]
    public class TabControl : WebControl, INamingContainer
    {
        private TextBox lblActiveTab;
        private List<TabPage> Tabs;

        public TabControl()
        {
            this.Tabs = new List<TabPage>();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public List<TabPage> TabPages
        {
            get { return Tabs; }
            set { this.Tabs = value; }
        }

        [Browsable(true)]
        [Localizable(false)]
        [DefaultValue(false)]
        public bool UsePillsInsteadOfTabs
        {
            get
            {
                return ViewState.GetPropertyValue("UsePillsInsteadOfTabs", false);
            }
            set
            {
                ViewState.SetPropertyValue("UsePillsInsteadOfTabs", value);
            }
        }

        protected override void RenderContents(HtmlTextWriter output)
        {
            this.RenderChildren(output);
            foreach (var tab in this.Tabs)
            {
                tab.RenderControl(output);
            }
        }

        protected override void CreateChildControls()
        {
            lblActiveTab = new TextBox();
            lblActiveTab.ID = "lblActiveTab_" + this.ClientID;
            lblActiveTab.Style.Add("display", "none");
            this.Controls.Add(lblActiveTab);
            base.CreateChildControls();
        }

        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            //base.RenderBeginTag(writer);

            StringBuilder sb = new StringBuilder();
            sb.Append("<div id=\"Tabs\" role=\"tabpanel\">");

            if (UsePillsInsteadOfTabs)
            {
                sb.Append("<ul class=\"nav nav-pills\" role=\"tablist\">");
            }
            else
            {
                sb.Append("<ul class=\"nav nav-tabs\" role=\"tablist\">");
            }

            if (!string.IsNullOrEmpty(lblActiveTab.Text))
            {
                foreach (var tab in Tabs)
                {
                    tab.IsActive = false;
                    if (tab.ClientID == lblActiveTab.Text)
                    {
                        tab.IsActive = true;
                    }
                }
            }
            else if (Tabs.Count > 0)
            {
                Tabs[0].IsActive = true;
                lblActiveTab.Text = Tabs[0].ClientID;
            }

            foreach (var tab in this.Tabs)
            {
                string cssClass = "";
                if (tab.IsActive)
                {
                    cssClass = " class=\"active\" ";
                }
                sb.Append("<li" + cssClass + ">");

                sb.Append("<a href=\"#" + tab.ClientID + "\" role=\"tab\" data-toggle=\"tab\" onclick='$(\"#" + lblActiveTab.ClientID + "\").val(\"" + tab.ClientID + "\");' aria-controls=\"" + tab.ClientID + "\">");

                if (!string.IsNullOrEmpty(tab.Title))
                {
                    sb.Append(tab.Title);
                }
                else
                {
                    sb.Append(tab.ID);
                }

                sb.Append("</a>");

                sb.Append("</li>");
            }

            sb.Append("</ul>");
            sb.Append("<div class=\"tab-content\">");

            Literal litTop = new Literal();
            litTop.Text = sb.ToString();
            litTop.RenderControl(writer);
        }

        public override void RenderEndTag(HtmlTextWriter writer)
        {
            writer.WriteLine("</div>");
            writer.WriteLine("</div>");

            //writer.WriteLine("<script type=\"text/javascript\">");
            //writer.WriteLine("$(window).load(function() { var activeTabId = $(\""+lblActiveTab.ClientID+"\").text();  })");
            //writer.WriteLine("</script>");

            //base.RenderEndTag(writer);
        }
    }
}
