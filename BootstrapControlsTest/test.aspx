<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="CentUA.test" %>

<%@ Register Assembly="BootstrapControls" Namespace="BootstrapControls.Controls" TagPrefix="cc1" %>






<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/bootstrap-theme.min.css" rel="stylesheet" />
    <link href="Content/font-awesome.min.css" rel="stylesheet" />
    <link href="Content/bootstrap-datetimepicker.min.css" rel="stylesheet" />
    <%--<link href="Content/bootstrap-switch/bootstrap3/bootstrap-switch.min.css" rel="stylesheet" />--%>

    <script src="Scripts/jquery-2.1.4.min.js"></script>

    <script src="Scripts/bootstrap.min.js"></script>
    <%--    <script src="Scripts/bootstrap-switch.min.js"></script>--%>

    <%--    <script src="Scripts/moment.min.js"></script>--%>
    <script src="Scripts/moment-with-locales.min.js"></script>
    <script src="Scripts/bootstrap-datetimepicker.min.js"></script>

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

                        <cc1:DateTimePickerInput ID="DateTimePickerInput1"
                            runat="server"
                            Placeholder="Your birthdate"
                            Language="en-GB"
                            Label="Birthdate">
                        </cc1:DateTimePickerInput>

                        <cc1:SelectInput ID="SelectInput1" runat="server" Label="Country"
                            HelpText="Select your country in the selection box">

                            <asp:ListItem Text="Select your coutry" Value="" Selected="True" disabled></asp:ListItem>
                            <asp:ListItem Text="Belgium" Value="BE"></asp:ListItem>
                            <asp:ListItem Text="The Netherlands" Value="NL"></asp:ListItem>
                            <asp:ListItem Text="France" Value="FR"></asp:ListItem>
                            <asp:ListItem Text="United Kingdom" Value="UK"></asp:ListItem>
                            <asp:ListItem Text="Spain" Value="ES"></asp:ListItem>
                            <asp:ListItem Text="Germany" Value="GR"></asp:ListItem>

                        </cc1:SelectInput>

                    </div>

                </div>

                <div class="col-md-3">
                    <br />
                    <cc1:Alert runat="server" ID="alert1" AlertStyle="Error" ImageClass="glyphicon glyphicon-exclamation-sign" Title="Error" Text="Enter a valid email address"></cc1:Alert>
                    <cc1:Alert runat="server" ID="alert2" AlertStyle="Success" ImageClass="glyphicon glyphicon-ok-sign" Title="Success" Text="All is well!!"></cc1:Alert>
                    <br />

                    <cc1:ButtonInput ID="Button1" runat="server" Text="Cancel" />
                    <cc1:ButtonInput ID="ButtonInput1" runat="server" Text="Submit" ButtonStyle="Primary" OnClick="Button1_Click" />

                </div>

            </div>
        </div>

    </form>
</body>
</html>
