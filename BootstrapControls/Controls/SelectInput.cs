using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BootstrapControls.Controls
{
    [ToolboxData("<{0}:SelectInput runat=\"server\" Label=\"\" Placeholder=\"\" />")]
    [Serializable]
    [ValidationProperty("SelectedValue")]
    public class SelectInput : DropDownList
    {
        [Category("Appearance")]
        [Browsable(true)]
        [DefaultValue("")]
        [Description("The label for this select. Ex: Country.")]
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
            }
        }

        [Category("Appearance")]
        [Browsable(true)]
        [DefaultValue("")]
        [Description("The group field for this select. When filled in you also have to set Chzn for it to work. Then it will render optgroups.")]
        [Localizable(true)]
        public string DataGroupField
        {
            get
            {
                return ViewState.GetPropertyValue("DataGroupField", "");
            }
            set
            {
                ViewState.SetPropertyValue("DataGroupField", value);
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
        /// if a group doesn't has any enabled items,there is no need
        /// to render the group too
        /// Credite: http://weblogs.asp.net/alaaalnajjar/group-options-in-dropdownlist
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        private bool IsGroupHasEnabledItems(string groupName)
        {
            ListItemCollection items = Items;
            for (int i = 0; i < items.Count; i++)
            {
                ListItem item = items[i];
                if (item.Attributes["DataGroupField"] != null && item.Attributes["DataGroupField"].Equals(groupName) && item.Enabled)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Render this control to the output parameter specified.
        /// Based on the source code of the original DropDownList method
        /// </summary>
        /// <param name="writer"> The HTML writer to write out to </param>
        protected override void RenderContents(HtmlTextWriter writer)
        {
            if (!string.IsNullOrEmpty(this.DataGroupField))
            {
                ListItemCollection items = Items;
                int itemCount = Items.Count;
                string curGroup = String.Empty;
                bool bSelected = false;

                if (itemCount <= 0)
                {
                    return;
                }

                for (int i = 0; i < itemCount; i++)
                {
                    ListItem item = items[i];
                    string itemGroup = item.Attributes["DataGroupField"];
                    if (itemGroup != null && itemGroup != curGroup && IsGroupHasEnabledItems(itemGroup))
                    {
                        if (curGroup != String.Empty)
                        {
                            writer.WriteEndTag("optgroup");
                            writer.WriteLine();
                        }

                        curGroup = itemGroup;
                        writer.WriteBeginTag("optgroup");
                        writer.WriteAttribute("label", curGroup, true);
                        writer.Write('>');
                        writer.WriteLine();
                    }
                    // we don't want to render disabled items
                    if (item.Enabled)
                    {
                        writer.WriteBeginTag("option");
                        if (item.Selected)
                        {
                            if (bSelected)
                            {
                                throw new HttpException("Cant_Multiselect_In_DropDownList");
                            }
                            bSelected = true;
                            writer.WriteAttribute("selected", "selected", false);
                        }

                        /*if (i == 0 && !string.IsNullOrEmpty(this.Placeholder))
                        {
                            writer.Write(" value");
                        }
                        else
                        {*/
                        writer.WriteAttribute("value", item.Value, true);
                        //}

                        writer.Write('>');
                        HttpUtility.HtmlEncode(item.Text, writer);
                        writer.WriteEndTag("option");
                        writer.WriteLine();
                    }
                }
                if (curGroup != String.Empty)
                {
                    writer.WriteEndTag("optgroup");
                    writer.WriteLine();
                }
            }
            else
            {
                base.RenderContents(writer);
            }
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
            if (!string.IsNullOrEmpty(this.Placeholder))
            {
                ListItem listItem = new ListItem(this.Placeholder, "");
                listItem.Attributes.Add("disabled", null);
                this.Items.Insert(0, listItem);
                this.SelectedIndex = 0;
                if (AddChznClass)
                {
                    htmlWriter.AddAttribute("data-placeholder", this.Placeholder);
                    this.Items[0].Text = "";
                }
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
                sb.Append(Environment.NewLine);
                sb.Append("$(window).load(function() { ");
                sb.Append(Environment.NewLine);
                sb.Append("$(\"#" + this.ClientID + "\").chosen({allow_single_deselect: true});");
                sb.Append(Environment.NewLine);
                sb.Append(" })");
                sb.Append(Environment.NewLine);
                sb.Append("</script>");
                sb.Append(Environment.NewLine);
            }

            Literal litEnd = new Literal();
            litEnd.Text = sb.ToString();
            litEnd.RenderControl(writer);
        }

        /// <summary>
        /// Perform data binding logic that is associated with the control
        /// Credits: http://weblogs.asp.net/alaaalnajjar/group-options-in-dropdownlist
        /// </summary>
        /// <param name="e">An EventArgs object that contains the event data</param>
        protected override void OnDataBinding(EventArgs e)
        {
            // Call base method to bind data
            base.OnDataBinding(e);

            if (!string.IsNullOrEmpty(this.DataGroupField))
            {
                // For each Item add the attribute "DataGroupField" with value from the datasource
                IEnumerable dataSource = GetResolvedDataSource(DataSource, DataMember);
                if (dataSource != null)
                {
                    ListItemCollection items = Items;
                    int i = 0;

                    string groupField = DataGroupField;
                    foreach (object obj in dataSource)
                    {
                        string groupFieldValue = DataBinder.GetPropertyValue(obj, groupField, null);
                        ListItem item = items[i];
                        item.Attributes.Add("DataGroupField", groupFieldValue);
                        i++;
                    }
                }
            }
        }

        /// <summary>
        /// This is copy of the internal ListControl method
        /// Credits: http://weblogs.asp.net/alaaalnajjar/group-options-in-dropdownlist
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="dataMember"></param>
        /// <returns></returns>
        private IEnumerable GetResolvedDataSource(object dataSource, string dataMember)
        {
            if (dataSource != null)
            {
                var source1 = dataSource as IListSource;
                if (source1 != null)
                {
                    IList list1 = source1.GetList();
                    if (!source1.ContainsListCollection)
                    {
                        return list1;
                    }
                    var list = list1 as ITypedList;
                    if (list != null)
                    {
                        var list2 = list;
                        PropertyDescriptorCollection collection1 = list2.GetItemProperties(new PropertyDescriptor[0]);
                        if ((collection1 == null) || (collection1.Count == 0))
                        {
                            throw new HttpException("ListSource_Without_DataMembers");
                        }

                        PropertyDescriptor descriptor1 = collection1[0];

                        if (!string.IsNullOrWhiteSpace(dataMember))
                        {
                            descriptor1 = collection1.Find(dataMember, true);
                        }

                        if (descriptor1 != null)
                        {
                            object obj1 = list1[0];
                            object obj2 = descriptor1.GetValue(obj1);
                            var enumerable = obj2 as IEnumerable;
                            if (enumerable != null)
                            {
                                return enumerable;
                            }
                        }
                        throw new HttpException("ListSource_Missing_DataMember");
                    }
                }
                var source = dataSource as IEnumerable;
                if (source != null)
                {
                    return source;
                }
            }
            return null;
        }

        #region Internal behaviour
        /// <summary>
        /// Saves the state of the view.
        /// Credits: http://weblogs.asp.net/alaaalnajjar/group-options-in-dropdownlist
        /// </summary>
        protected override object SaveViewState()
        {
            // Create an object array with one element for the CheckBoxList's
            // ViewState contents, and one element for each ListItem in skmCheckBoxList
            var state = new object[Items.Count + 1];

            object baseState = base.SaveViewState();
            state[0] = baseState;

            // Now, see if we even need to save the view state
            bool itemHasAttributes = false;
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].Attributes.Count == 0) continue;

                itemHasAttributes = true;

                // Create an array of the item's Attribute's keys and values
                var attribKv = new object[Items[i].Attributes.Count * 2];
                int k = 0;
                foreach (string key in Items[i].Attributes.Keys)
                {
                    attribKv[k++] = key;
                    attribKv[k++] = Items[i].Attributes[key];
                }

                state[i + 1] = attribKv;
            }

            // return either baseState or state, depending on if any ListItems had attributes
            return itemHasAttributes ? state : baseState;
        }

        /// <summary>
        /// Loads the state of the view.
        /// Credits: http://weblogs.asp.net/alaaalnajjar/group-options-in-dropdownlist
        /// </summary>
        /// <param name="savedState">State of the saved.</param>
        protected override void LoadViewState(object savedState)
        {
            if (savedState == null) return;

            // see if savedState is an object or object array
            var objects = savedState as object[];
            if (objects != null)
            {
                // we have an array of items with attributes
                object[] state = objects;
                base.LoadViewState(state[0]); // load the base state

                for (int i = 1; i < state.Length; i++)
                {
                    if (state[i] != null)
                    {
                        // Load back in the attributes
                        var attribKv = (object[])state[i];
                        for (int k = 0; k < attribKv.Length; k += 2)
                            Items[i - 1].Attributes.Add(attribKv[k].ToString(),
                                attribKv[k + 1].ToString());
                    }
                }
            }
            else
            {
                // we have just the base state
                base.LoadViewState(savedState);
            }
        }
        #endregion
    }
}
