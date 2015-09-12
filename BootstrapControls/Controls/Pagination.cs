using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BootstrapControls.Controls
{
    [ToolboxData("<{0}:Pagination runat=\"server\"></{0}:Pagination>")]
    [Serializable]
    public class Pagination : DataPager
    {
        protected override HtmlTextWriterTag TagKey
        {
            get { return HtmlTextWriterTag.Ul; }
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            //Taken from: http://stackoverflow.com/questions/13394686/formatting-an-aspdatapager-to-show-in-ul-li

            if (HasControls())
            {
                foreach (Control child in Controls)
                {
                    var item = child as DataPagerFieldItem;
                    if (item == null || !item.HasControls())
                    {
                        child.RenderControl(writer);
                        continue;
                    }

                    foreach (Control ctrl in item.Controls)
                    {
                        var space = ctrl as LiteralControl;
                        if (space != null && space.Text == "&nbsp;") continue;

                        bool isCurrentPage = false;

                        if (ctrl is WebControl)
                        {
                            // Enabled = false -> "disabled"
                            if (!((WebControl)ctrl).Enabled)
                                writer.AddAttribute(HtmlTextWriterAttribute.Class, "disabled");

                            // there can only be one Label in the datapager -> "active"
                            if (ctrl is Label)
                            {
                                isCurrentPage = true;
                                writer.AddAttribute(HtmlTextWriterAttribute.Class, "active");
                            }
                        }

                        writer.RenderBeginTag(HtmlTextWriterTag.Li);
                        if (isCurrentPage)
                        {
                            writer.AddAttribute(HtmlTextWriterAttribute.Href, "#");
                            writer.RenderBeginTag(HtmlTextWriterTag.A);
                        }
                        ctrl.RenderControl(writer);
                        if (isCurrentPage)
                        {
                            writer.RenderEndTag();
                        }
                        writer.RenderEndTag();
                    }
                }
            }
        }
    }
}
