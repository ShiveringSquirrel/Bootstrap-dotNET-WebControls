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
        //private ITemplate Tabs;

        public TabControl()
        {
            //this.Tabs = new List<TabPage>();
        }

        [TemplateContainer(typeof(TabPage))]
        [TemplateInstance(TemplateInstance.Single)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public ITemplate TabPages
        {
            get; /*{ return Tabs; }*/
            set; /*{ this.Tabs = value; }*/
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

        /*protected override void RenderContents(HtmlTextWriter output)
        {
            this.RenderChildren(output);
            foreach (var tab in this.Tabs)
            {
                tab.RenderControl(output);
            }
        }*/

        protected override void CreateChildControls()
        {
            // Remove any controls
            this.Controls.Clear();

            lblActiveTab = new TextBox();
            lblActiveTab.ID = "lblActiveTab_" + this.ClientID;
            lblActiveTab.Style.Add("display", "none");
            this.Controls.Add(lblActiveTab);

            // Add all content to a container.
            var container = new PlaceHolder();
            //container.ID = Guid.NewGuid().ToString("N");
            this.TabPages.InstantiateIn(container);

            // Add container to the control collection.
            this.Controls.Add(container);
        }

        public override void RenderBeginTag(HtmlTextWriter writer)
        {
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

            //The first control is our textbox, the second our placeholder
            PlaceHolder container = Controls[1] as PlaceHolder;
            var pControls = container.Controls;

            if (!string.IsNullOrEmpty(lblActiveTab.Text))
            {
                foreach (Control c in pControls)
                {
                    if (c is TabPage)
                    {
                        var tab = (c as TabPage);
                        tab.IsActive = false;
                        if (tab.ClientID == lblActiveTab.Text)
                        {
                            tab.IsActive = true;
                        }
                    }
                }
            }
            else
            {
                foreach (Control c in pControls)
                {
                    if (c is TabPage)
                    {
                        (c as TabPage).IsActive = true;
                        lblActiveTab.Text = c.ClientID;
                        break;
                    }
                }
            }

            foreach (Control c in pControls)
            {
                if (c is TabPage)
                {
                    var tab = (c as TabPage);
                    string cssClass = "";
                    if (tab.IsActive)
                    {
                        cssClass = " class=\"active\" ";
                    }
                    sb.Append("<li" + cssClass + ">");

                    sb.Append("<a href=\"#" + tab.ClientID + "\" role=\"tab\" data-toggle=\"tab\" onclick='$(\"#" +
                              lblActiveTab.ClientID + "\").val(\"" + tab.ClientID + "\");' aria-controls=\"" +
                              tab.ClientID + "\">");

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
            }

            sb.Append("</ul>");
            sb.Append("<div class=\"tab-content\" style=\"padding-top: 20px\">");

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

        protected override void OnInit(System.EventArgs e)
        {
            base.OnInit(e);

            // Initialize all child controls.
            this.CreateChildControls();
            this.ChildControlsCreated = true;
        }
    }
}
