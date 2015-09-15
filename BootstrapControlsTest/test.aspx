<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="CentUA.test" %>

<%@ Register Assembly="BootstrapControls" Namespace="BootstrapControls.Controls" TagPrefix="cc1" %>






<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/bootstrap-theme.min.css" rel="stylesheet" />
    <link href="Content/font-awesome.min.css" rel="stylesheet" />
    <link href="Content/bootstrap-datetimepicker.min.css" rel="stylesheet" />
    <link href="Content/bootstrap-switch/bootstrap3/bootstrap-switch.min.css" rel="stylesheet" />

    <link href="Content/bootstrap-chosen.css" rel="stylesheet" />

    <script src="Scripts/jquery-2.1.4.min.js"></script>

    <script src="Scripts/bootstrap.min.js"></script>
    <script src="Scripts/bootstrap-switch.min.js"></script>

    <%--    <script src="Scripts/moment.min.js"></script>--%>
    <script src="Scripts/moment-with-locales.min.js"></script>
    <script src="Scripts/bootstrap-datetimepicker.min.js"></script>
    <script src="Scripts/chosen.jquery.min.js"></script>



    <title>Test page - custom controls</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="row">
                <div class="col-md-1">
                </div>
                <div class="col-md-3">


                    <asp:ValidationSummary ID="ValidationSummary1"
                        CssClass="alert alert-danger"
                        HeaderText="Please correct the following errors"
                        runat="server" />

                    <div>
                        <cc1:TextInput ID="TextInput1" runat="server" Label="Name"
                            Placeholder="Your name" Text="" State="Normal" PrefixImageClass="fa fa-bolt"
                            HelpText="Enter your name. Ex: Smith"></cc1:TextInput>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                            ControlToValidate="TextInput1"
                            Text="*"
                            EnableClientScript="False"
                            Display="None"
                            runat="server"
                            ErrorMessage="Name is required"></asp:RequiredFieldValidator>

                        <cc1:TextInput ID="TextInput2" runat="server" Label="First name"
                            HelpText="Enter your first name. Ex: John"
                            Placeholder="Your first name" Text="" State="Normal" GroupStyle="Success"
                            PostfixImageClass="fa fa-cubes"></cc1:TextInput>

                        <cc1:TextInput ID="TextInput5" runat="server"
                            Label="Password" HelpText=""
                            Placeholder="Enter your password" Text=""
                            Type="Password"
                            State="Normal"
                            PrefixImageClass="fa fa-key"></cc1:TextInput>

                        <cc1:TextInput ID="TextInput3" runat="server" Label="E-Mail"
                            Text="test@test.com" State="Disabled" GroupStyle="Warning"></cc1:TextInput>

                        <cc1:TextInput ID="TextInput4" runat="server" Label="Address"
                            Text="Street 1" State="Static"></cc1:TextInput>

                        <cc1:DateTimePickerInput ID="dtpBirthDate"
                            runat="server"
                            Placeholder="Your birthdate"
                            Language="en-GB"
                            Label="Birthdate">
                        </cc1:DateTimePickerInput>

                        <cc1:SelectInput ID="SelectInput1" runat="server" Label="Country"
                            HelpText="Select your country in the selection box"
                            Placeholder="Select your country"
                            AddChznClass="True">

                            <asp:ListItem Text="Belgium" Value="BE"></asp:ListItem>
                            <asp:ListItem Text="The Netherlands" Value="NL"></asp:ListItem>
                            <asp:ListItem Text="France" Value="FR"></asp:ListItem>
                            <asp:ListItem Text="United Kingdom" Value="UK"></asp:ListItem>
                            <asp:ListItem Text="Spain" Value="ES"></asp:ListItem>
                            <asp:ListItem Text="Germany" Value="GR"></asp:ListItem>

                        </cc1:SelectInput>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                            ControlToValidate="SelectInput1"
                            Text="*"
                            EnableClientScript="False"
                            Display="None"
                            runat="server"
                            ErrorMessage="Country is required"></asp:RequiredFieldValidator>

                        <cc1:SelectListInput runat="server"
                            ID="SelectListInput1" SelectionMode="Multiple" Placeholder="Select your animal(s)"
                            Label="Animals" AddChznClass="True"
                            HelpText="Select your favorite animal(s). Hold shift to select multiple values.">
                            <asp:ListItem Text="Monkey" Value="m"></asp:ListItem>
                            <asp:ListItem Text="Koala" Value="k"></asp:ListItem>
                            <asp:ListItem Text="Elephant" Value="e"></asp:ListItem>
                            <asp:ListItem Text="Girafe" Value="g"></asp:ListItem>
                            <asp:ListItem Text="Hippopotamus" Value="h"></asp:ListItem>
                            <asp:ListItem Text="Lion" Value="l"></asp:ListItem>
                        </cc1:SelectListInput>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                            ControlToValidate="SelectListInput1"
                            Text="*"
                            EnableClientScript="False"
                            Display="None"
                            runat="server"
                            ErrorMessage="Animal is required"></asp:RequiredFieldValidator>

                    </div>

                </div>

                <div class="col-md-3">
                    <br />
                    <cc1:Alert runat="server" ID="alert1" AlertStyle="Error" ImageClass="glyphicon glyphicon-exclamation-sign" Title="Error" Text="Enter a valid email address"></cc1:Alert>
                    <cc1:Alert runat="server" ID="alert2" AlertStyle="Success" ImageClass="glyphicon glyphicon-ok-sign" Title="Success" Text="All is well!!"></cc1:Alert>
                    <br />

                    <cc1:ButtonInput ID="Button1" runat="server" Text="Cancel" />
                    <cc1:ButtonInput ID="ButtonInput1" runat="server" Text="Submit" ButtonStyle="Primary" OnClick="Button1_Click" />

                    <br />
                    <br />
                    <asp:Label ID="lblAnimalsSelected" runat="server" Text="Your selected animals..."></asp:Label>
                    <br />

                    <cc1:BootstrapPanel ID="bPanel" Title="Range test" runat="server">

                        <cc1:DateTimePickerInput ID="DateTimePickerInputMin"
                            runat="server"
                            Language="en-GB"
                            DateTimePickerUsedAsMax="DateTimePickerInputMax"
                            Label="From date">
                        </cc1:DateTimePickerInput>

                        <cc1:DateTimePickerInput ID="DateTimePickerInputMax"
                            runat="server"
                            Language="en-GB"
                            Label="To date">
                        </cc1:DateTimePickerInput>

                    </cc1:BootstrapPanel>

                    <cc1:Switch runat="server" ID="switch1" OnText="Yes" OffText="No" Label="Some checkbox" />
                    <cc1:Switch runat="server" ID="switch2" Disabled="True" Label="Some disabled checkbox" />

                </div>

                <div class="col-md-3">

                    <br />

                    <cc1:ButtonInput ID="ButtonInput2"
                        runat="server"
                        Text="Show modal window"
                        ModalWindowIdToOpen="Modal1"
                        ButtonStyle="Success" />

                    <cc1:Modal runat="server"
                        CausesValidation="True"
                        ValidationGroup="MODALVALIDATION"
                        CanClose="True"
                        ID="Modal1" Title="This is a modal window"
                        OnSubmitClicked="Modal1_SubmitClicked">

                        <asp:ValidationSummary
                            CssClass="alert alert-danger"
                            HeaderText="Please correct the following errors"
                            ID="ValidationSummary2" ValidationGroup="MODALVALIDATION" runat="server" />

                        Lorem Ipsum is simply dummy text of the printing and typesetting industry. 
                        Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, 
                        when an unknown printer took a galley of type and scrambled it to make a type 
                        specimen book. It has survived not only five centuries, but also the leap into 
                        electronic typesetting, remaining essentially unchanged. It was popularised in 
                        the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, 
                        and more recently with desktop publishing software like Aldus PageMaker including 
                        versions of Lorem Ipsum.
                        
                        <cc1:TextInput ID="modalTxtName" runat="server" ValidationGroup="MODALVALIDATION" Label="Name" Placeholder="Please enter your name..." State="Normal" Text="">
                        </cc1:TextInput>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                            ControlToValidate="modalTxtName"
                            Text="*"
                            EnableClientScript="False"
                            Display="None"
                            ValidationGroup="MODALVALIDATION"
                            ErrorMessage="Name is reguired (modal)"></asp:RequiredFieldValidator>

                    </cc1:Modal>


                    <cc1:ButtonInput ID="ButtonInput3"
                        runat="server"
                        Text="Show modal window (2)"
                        ModalWindowIdToOpen="Modal2"
                        ButtonStyle="Danger" />

                    <cc1:Modal runat="server"
                        CausesValidation="True"
                        ValidationGroup="MODALVALIDATION_2"
                        CanClose="True"
                        UseFade="False"
                        ID="Modal2" Title="This is a modal window (2)" OnSubmitClicked="Modal2_SubmitClicked">

                        <asp:ValidationSummary
                            CssClass="alert alert-danger"
                            HeaderText="Please correct the following errors"
                            ID="ValidationSummary3" ValidationGroup="MODALVALIDATION_2" runat="server" />

                        This is the second modal window, this modal window uses the no fading...
                        
                        <cc1:TextInput ID="modalTxtName2" runat="server" ValidationGroup="MODALVALIDATION_2" Label="Name" Placeholder="Please enter your name..." State="Normal" Text="">
                        </cc1:TextInput>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                            ControlToValidate="modalTxtName2"
                            Text="*"
                            EnableClientScript="False"
                            Display="None"
                            ValidationGroup="MODALVALIDATION_2"
                            ErrorMessage="Name is reguired (modal)"></asp:RequiredFieldValidator>

                    </cc1:Modal>


                </div>

            </div>
        </div>
    </form>
</body>
</html>
