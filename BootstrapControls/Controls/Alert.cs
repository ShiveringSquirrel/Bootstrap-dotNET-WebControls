using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BootstrapControls.Controls
{
    [ToolboxData("<{0}:Alert runat=\"server\" Text=\"\" Title=\"Error\" ImageClass=\"glyphicon glyphicon-exclamation-sign\" AlertStyle=\"Error\" />")]
    [DefaultProperty("Text")]
    [Serializable]
    public class Alert : Label
    {
        [Category("Appearance")]
        [Browsable(true)]
        [DefaultValue("Info")]
        [Description("The style of the alert element.")]
        [Localizable(false)]
        public Enumerations.AlertStyle AlertStyle
        {
            get
            {
                if (ViewState["AlertStyle"] is Enumerations.AlertStyle)
                {
                    return (Enumerations.AlertStyle)ViewState["AlertStyle"];
                }
                return Enumerations.AlertStyle.Info;
            }
            set
            {
                ViewState["AlertStyle"] = value;
            }
        }

        [Category("Appearance")]
        [Browsable(true)]
        [DefaultValue("")]
        [Description("The image class(es) to display in this alert")]
        [Localizable(false)]
        public string ImageClass
        {
            get
            {
                if (ViewState["ImageClass"] != null &&
                    !string.IsNullOrEmpty(ViewState["ImageClass"].ToString()))
                {
                    return ViewState["ImageClass"].ToString();
                }
                return "";
            }
            set
            {
                ViewState["ImageClass"] = value;
            }
        }

        [Category("Appearance")]
        [Browsable(true)]
        [DefaultValue("")]
        [Description("The title to display in this alert")]
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

        protected override void Render(HtmlTextWriter writer)
        {
            string cssClass = "alert ";

            switch (AlertStyle)
            {
                case Enumerations.AlertStyle.Info:
                    cssClass += "alert-info";
                    break;
                case Enumerations.AlertStyle.Error:
                    cssClass += "alert-danger";
                    break;
                case Enumerations.AlertStyle.Success:
                    cssClass += "alert-success";
                    break;
                case Enumerations.AlertStyle.Warning:
                    cssClass += "alert-warning";
                    break;
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("<div class=\"");
            sb.Append(cssClass);
            sb.Append("\" role=\"alert\">");

            if (!string.IsNullOrEmpty(this.ImageClass))
            {
                sb.Append("<span class=\"");
                sb.Append(this.ImageClass);
                sb.Append("\" aria-hidden=\"true\"></span>");
            }

            sb.Append("<span class=\"sr-only\">");
            sb.Append(this.Title);
            sb.Append("</span>");

            sb.Append("&nbsp;");
            sb.Append(this.Text.Trim());

            sb.Append("</div>");

            Literal litEnd = new Literal();
            litEnd.Text = sb.ToString();
            litEnd.RenderControl(writer);
        }



    }
}
