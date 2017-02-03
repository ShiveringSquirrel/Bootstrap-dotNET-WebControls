using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BootstrapControls;

namespace BootstrapControls.Controls
{
    [ToolboxData("<{0}:Modal runat=\"server\"></{0}:Modal>")]
    [ToolboxBitmap(typeof(System.Web.UI.WebControls.Panel))]
    public class Modal : Panel, INamingContainer
    {
        //private ButtonInput btnSubmit;

        protected override void OnLoad(EventArgs e)
        {
            Control ctrl = this.Page.GetPostBackControl();
            if (ctrl != null)
            {
                if (ctrl.ClientID.Contains(this.ID)) //This is needed for buttons inside the modal form that cause a postback
                {
                    if (!ctrl.ClientID.EndsWith("btnSubmit")) //It is not the sumbit button of this modal that caused the postback
                    {
                        ShowModal();
                    }
                }
            }
            base.OnLoad(e);
        }

        public delegate void SubmitClickedHandler();

        [Category("Action")]
        [Description("Fires when the Submit button is clicked.")]
        public event SubmitClickedHandler SubmitClicked;

        // Add a protected method called OnSubmitClicked().
        // You may use this in child classes instead of adding
        // event handlers.
        protected virtual void OnSubmitClicked()
        {
            // If an event has no subscribers registered, it will
            // evaluate to null. The test checks that the value is not
            // null, ensuring that there are subscribers before
            // calling the event itself.
            if (SubmitClicked != null)
            {
                SubmitClicked();  // Notify Subscribers
            }
        }

        private void ShowModal()
        {
            string resource = "BootstrapControls.Resources.ModalFormShowOnError." + this.ClientID;
            string script = "$(window).load(function() { $('#" + this.ClientID + "').modal('show'); })";

            ClientScriptManager manager = (HttpContext.Current.Handler as Page).ClientScript;
            if (!manager.IsClientScriptBlockRegistered(manager.GetType(), resource))
            {
                manager.RegisterClientScriptBlock(manager.GetType(), resource, script, true);
            }
        }

        // Handler for Submit Button. Do some validation before
        // calling the event.
        void btnSubmit_Click(object sender, System.EventArgs e)
        {
            if (CausesValidation)
            {
                if (!string.IsNullOrEmpty(ValidationGroup))
                {
                    Page.Validate(ValidationGroup);
                }
                if (!Page.IsValid)
                {
                    ShowModal();
                }
            }

            OnSubmitClicked();

            if (CausesValidation && Page.IsValid)
            {
                ClearControl(this);
            }
        }

        private void ClearControl(Control control)
        {
            var textbox = control as TextBox;
            if (textbox != null)
                textbox.Text = string.Empty;

            var dropDownList = control as DropDownList;
            if (dropDownList != null && dropDownList.Items.Count > 0)
                dropDownList.SelectedIndex = 0;

            var list = control as ListBox;
            if (list != null && list.Items.Count > 0)
                list.SelectedIndex = 0;

            /*var file = control as FileUploader;
            if (file != null)
                file.cl;*/

            foreach (Control childControl in control.Controls)
            {
                ClearControl(childControl);
            }
        }

        ///// <summary>
        ///// This will be used to list all the inner controls of this panel.
        ///// </summary>
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        //[PersistenceMode(PersistenceMode.InnerProperty)]
        //public ControlCollection Children
        //{
        //    get;
        //    set;
        //    /*set
        //    {
        //        this.Controls.Add(value);
        //    }*/
        //}

        [Category("Appearance")]
        [Browsable(true)]
        [DefaultValue("")]
        [Description("The title of this modal form to display.")]
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
        [DefaultValue(true)]
        [Description("Is there a submit button on this modal.")]
        [Localizable(true)]
        public bool HasSubmitButton
        {
            get
            {
                return ViewState.GetPropertyValue("HasSubmitButton", true);
            }
            set
            {
                ViewState.SetPropertyValue("HasSubmitButton", value);
            }
        }

        [Category("Appearance")]
        [Browsable(true)]
        [DefaultValue("Submit")]
        [Description("The text to display on the submit button of this modal.")]
        [Localizable(true)]
        public string SubmitButtonText
        {
            get
            {
                return ViewState.GetPropertyValue("SubmitButtonText", "Submit");
            }
            set
            {
                ViewState.SetPropertyValue("SubmitButtonText", value);
            }
        }

        [Category("Appearance")]
        [Browsable(true)]
        [DefaultValue("True")]
        [Description("Can the user close this window?")]
        [Localizable(true)]
        public bool CanClose
        {
            get
            {
                return ViewState.GetPropertyValue("CanClose", true);
            }
            set
            {
                ViewState.SetPropertyValue("CanClose", value);
            }
        }

        [Category("Appearance")]
        [Browsable(true)]
        [DefaultValue("True")]
        [Description("Does the window use a fade in/out effect?")]
        [Localizable(true)]
        public bool UseFade
        {
            get
            {
                return ViewState.GetPropertyValue("UseFade", true);
            }
            set
            {
                ViewState.SetPropertyValue("UseFade", value);
            }
        }

        [Category("Validation")]
        [Browsable(true)]
        [DefaultValue("")]
        [Description("The ValidationGroup of this modal form.")]
        [Localizable(false)]
        public string ValidationGroup
        {
            get
            {
                return ViewState.GetPropertyValue("ValidationGroup", "");
            }
            set
            {
                ViewState.SetPropertyValue("ValidationGroup", value);
            }
        }


        [Category("Validation")]
        [Browsable(true)]
        [DefaultValue("True")]
        [Description("Does this modal form (submit button) cause validation?")]
        [Localizable(false)]
        public bool CausesValidation
        {
            get
            {
                return ViewState.GetPropertyValue("CausesValidation", true);
            }
            set
            {
                ViewState.SetPropertyValue("CausesValidation", value);
            }
        }

        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            StringBuilder sb = new StringBuilder();

            if (UseFade)
            {
                sb.Append("<div class=\"modal fade\" ");
            }
            else
            {
                sb.Append("<div class=\"modal\" ");
            }

            if (!CanClose)
            {
                sb.Append("data-keyboard=\"false\" data-backdrop=\"static\" ");
            }
            sb.Append("id=\"" + this.ClientID + "\">");
            sb.Append(Environment.NewLine);
            sb.Append("<div class=\"modal-dialog\">");
            sb.Append(Environment.NewLine);
            sb.Append("<div class=\"modal-content\">");
            sb.Append(Environment.NewLine);
            sb.Append("<div class=\"modal-header\">");
            sb.Append(Environment.NewLine);

            if (CanClose)
            {
                sb.Append(
                    "<button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-label=\"Close\"><span aria-hidden=\"true\">&times;</span></button>");
                sb.Append(Environment.NewLine);
            }

            sb.Append("<h4 class=\"modal-title\">" + this.Title + "</h4>");
            sb.Append(Environment.NewLine);
            sb.Append("</div>");
            sb.Append(Environment.NewLine);
            sb.Append("<div class=\"modal-body\">");
            sb.Append(Environment.NewLine);

            Literal litEnd = new Literal();
            litEnd.Text = sb.ToString();
            litEnd.RenderControl(writer);
        }

        protected override void CreateChildControls()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<div class=\"modal-footer\">");
            sb.Append(Environment.NewLine);
            sb.Append("<div class=\"row\">");
            sb.Append(Environment.NewLine);
            sb.Append("<div class=\"col-lg-12 text-right\">");
            sb.Append(Environment.NewLine);

            if (CanClose)
            {
                sb.Append("<button type=\"button\" class=\"btn btn-default\" data-dismiss=\"modal\">Close</button>");
                sb.Append(Environment.NewLine);
            }

            Literal litFooter = new Literal();
            litFooter.Text = sb.ToString();
            Controls.Add(litFooter);

            sb = new StringBuilder();

            if (this.HasSubmitButton)
            {
                var btnSubmit = new ButtonInput();
                btnSubmit.ID = "btnSubmit";
                btnSubmit.ButtonStyle = Enumerations.ButtonStyle.Primary;
                btnSubmit.Text = this.SubmitButtonText;
                btnSubmit.CausesValidation = this.CausesValidation;
                if (!string.IsNullOrEmpty(this.ValidationGroup))
                {
                    btnSubmit.ValidationGroup = this.ValidationGroup;
                }
                btnSubmit.Click += new EventHandler(btnSubmit_Click);
                //btnSubmit.OnClientClick = Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this) { Argument = "SubmitClicked" });
                Controls.Add(btnSubmit);
            }

            sb.Append(Environment.NewLine);

            sb.Append("</div>");
            sb.Append(Environment.NewLine);
            sb.Append("</div>");
            sb.Append(Environment.NewLine);
            sb.Append("</div>");
            sb.Append(Environment.NewLine);

            Literal litFooterEnd = new Literal();
            litFooterEnd.Text = sb.ToString();
            Controls.Add(litFooterEnd);

            base.CreateChildControls();
        }

        public override void RenderEndTag(HtmlTextWriter writer)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("</div>");
            sb.Append(Environment.NewLine);

            sb.Append("</div>");
            sb.Append(Environment.NewLine);

            sb.Append("</div>");
            sb.Append(Environment.NewLine);

            sb.Append("</div>");
            sb.Append(Environment.NewLine);

            Literal litEnd = new Literal();
            litEnd.Text = sb.ToString();
            litEnd.RenderControl(writer);
        }

        /*protected override void RenderContents(HtmlTextWriter output)
        {
            this.RenderChildren(output);
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            // Initialize all child controls.
            this.CreateChildControls();
            this.ChildControlsCreated = true;
        }*/
    }
}
