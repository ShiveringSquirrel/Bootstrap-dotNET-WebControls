using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BootstrapControls.Controls
{
    [ToolboxData("<{0}:SelectListInput runat=\"server\" Label=\"\" Placeholder=\"\" />")]
    [Serializable]
    [ValidationProperty("SelectedValue")]
    public class SelectListInput : ListBox
    {
        [Category("Appearance")]
        [Browsable(true)]
        [DefaultValue("")]
        [Description("The label for this list. Ex: Country.")]
        [Localizable(true)]
        public string Label
        {
            get
            {
                return ViewState.GetPropertyValue("Label", "");
            }
            set
            {
                ViewState.SetPropertyValue("Label", value);
            }
        }

        [Category("Appearance")]
        [Browsable(true)]
        [Description("The help text to display. Ex: Please select your country.")]
        [Localizable(true)]
        [DefaultValue("")]
        public string HelpText
        {
            get
            {
                return ViewState.GetPropertyValue("HelpText", "");
            }
            set
            {
                ViewState.SetPropertyValue("HelpText", value);
            }
        }

        [Category("Appearance")]
        [Browsable(true)]
        [DefaultValue("")]
        [Description("The placeholder to use for this select. Ex: Select your country.")]
        [Localizable(true)]
        public string Placeholder
        {
            get
            {
                return ViewState.GetPropertyValue("Placeholder", "");
            }
            set
            {
                ViewState.SetPropertyValue("Placeholder", value);
                if (AddChznClass)
                {
                    ListItem listItem = new ListItem(value, "");
                    listItem.Attributes.Add("disabled", "");
                    this.Items.Insert(0, listItem);
                }
            }
        }

        [Category("Appearance")]
        [Browsable(true)]
        [Description("Add chzn-select class for the chosen framework")]
        [Localizable(false)]
        [DefaultValue("False")]
        public bool AddChznClass
        {
            get
            {
                return ViewState.GetPropertyValue("AddChznClass", false);
            }
            set
            {
                ViewState.SetPropertyValue("AddChznClass", value);
            }
        }


        /// <summary>
        /// Get a list of all selected items
        /// </summary>
        /// <param name="ifNoneSelectedReturnAll">If nothing is selected in this list, retun all the items or not?</param>
        /// <returns>A list of the selected items</returns>
        public List<string> GetSelected(bool ifNoneSelectedReturnAll = false)
        {
            List<string> ret = new List<string>();
            bool everyone = (string.IsNullOrEmpty(this.SelectedValue) && ifNoneSelectedReturnAll);

            foreach (ListItem item in this.Items)
            {
                if (item.Selected || everyone)
                {
                    ret.Add(item.Value);
                }
            }
            return ret;
        }

        public SelectListInput()
        {
            //SelectedValue = "";
        }

        protected override void Render(HtmlTextWriter writer)
        {
            //This will hold all the HTML we want to write as output.
            StringBuilder sb = new StringBuilder();

            //Holds the group class(es)
            string cssGroupClass = "form-group";

            if (!this.IsValid()) //Call the extention method in base to see if this control is valid
            {
                cssGroupClass += " has-error";
            }

            sb.Append("<div class=\"");
            sb.Append(cssGroupClass);
            sb.Append("\">");
            sb.Append(Environment.NewLine);

            if (!string.IsNullOrEmpty(this.Label))
            {
                sb.Append("<label class=\"control-label\" for=\"");
                sb.Append(this.ClientID);
                sb.Append("\">");
                sb.Append(this.Label);
                sb.Append("</label>");
                sb.Append(Environment.NewLine);
            }

            //Render the base control using a new html writer, which in turn uses a 
            //text writer so we can capture the html and store it in our string builder
            TextWriter txtWriter = new StringWriter();
            HtmlTextWriter htmlWriter = new HtmlTextWriter(txtWriter);

            string cssSelectClass = "form-control";
            if (AddChznClass)
            {
                cssSelectClass += " chzn-select";
            }
            htmlWriter.AddAttribute(HtmlTextWriterAttribute.Class, cssSelectClass);

            //If we have a placeholder and its a "chosen" dropdown, remove the text from the first item
            //Item is added when setting the placeholder property.
            if (!string.IsNullOrEmpty(this.Placeholder) && AddChznClass)
            {
                htmlWriter.AddAttribute("data-placeholder", this.Placeholder);
            }

            if (this.SelectionMode == ListSelectionMode.Multiple)
            {
                htmlWriter.AddAttribute("multiple", null);
            }

            base.Render(htmlWriter);
            sb.Append(txtWriter);
            sb.Append(Environment.NewLine);

            if (!string.IsNullOrEmpty(this.HelpText))
            {
                sb.Append("<p class=\"help-block\">");
                sb.Append(this.HelpText);
                sb.Append("</p>");
                sb.Append(Environment.NewLine);
            }

            sb.Append("</div>");
            sb.Append(Environment.NewLine);

            if (AddChznClass)
            {
                sb.Append("<script type=\"text/javascript\">");
                sb.Append("$(\"#" + this.ClientID + "\").chosen();");
                sb.Append("</script>");
            }

            Literal litEnd = new Literal();
            litEnd.Text = sb.ToString();
            litEnd.RenderControl(writer);
        }
    }
}
