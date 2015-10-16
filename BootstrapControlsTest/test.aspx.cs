using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CentUA
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TextInput1.Text = "AAAAA";
                TextInput2.Text = "BBBBB";

                dtpBirthDate.DateTimeValue = DateTime.Now.AddYears(-20);
                switch1.Checked = true;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                Button1.Text = TextInput1.Text + " " + TextInput2.Text + " Country:" +
                               SelectInput1.SelectedValue + " " +
                               TextInput1.Label + " " + TextInput2.Label + " Birthdate: " + dtpBirthDate.DateTimeValue
                               + " Switch value = " + this.switch1.Checked + " From date: " + DateTimePickerInputMin.DateTimeValue
                               ;

                this.lblAnimalsSelected.Text = "Your selected animal(s)<br>";
                foreach (string animal in this.SelectListInput1.GetSelected(false))
                {
                    this.lblAnimalsSelected.Text += animal + "<br>";
                }
            }
        }

        protected void Modal1_SubmitClicked()
        {
            if (Page.IsValid)
            {
                this.Button1.Text = "Hi " + this.modalTxtName.Text + "!!!";
            }
        }

        protected void Modal2_SubmitClicked()
        {
            if (Page.IsValid)
            {
                this.Button1.Text = "Hi " + this.modalTxtName2.Text + "!!!";
            }
        }

        protected void btnSaveUpload_Click(object sender, EventArgs e)
        {
            byte[] myData = this.FileUploader1.GetData();
            if(myData.Length > 0)
            {
                this.imgFileUploader1.Src = "data:image/jpg;base64," + Convert.ToBase64String(myData);
            }

            byte[] myData2 = this.FileUploader2.GetData();
            if (myData2.Length > 0)
            {
                this.imgFileUploader2.Src = "data:image/jpg;base64," + Convert.ToBase64String(myData2);
            }
        }
    }
}