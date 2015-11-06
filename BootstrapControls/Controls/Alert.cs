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
    public class Alert : WebControl
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
                return ViewState.GetPropertyValue("AlertStyle", Enumerations.AlertStyle.Info);
            }
            set
            {
                ViewState.SetPropertyValue("AlertStyle", value);
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
                return ViewState.GetPropertyValue("ImageClass", "");
            }
            set
            {
                ViewState.SetPropertyValue("ImageClass", value);
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
                return ViewState.GetPropertyValue("Title", "");
            }
            set
            {
                ViewState.SetPropertyValue("Title", value);
            }
        }

        [Category("Appearance")]
        [Browsable(true)]
        [DefaultValue("")]
        [Description("The text/message to display in this alert")]
        [Localizable(true)]
        public string Text
        {
            get
            {
                return ViewState.GetPropertyValue("Text", "");
            }
            set
            {
                ViewState.SetPropertyValue("Text", value);
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
            sb.Append("\"");
            sb.Append(" id=\"");
            sb.Append(this.ClientID);
            sb.Append("\"");
            sb.Append(" role =\"alert\">");

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
