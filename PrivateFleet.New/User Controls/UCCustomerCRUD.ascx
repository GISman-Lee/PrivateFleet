<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCCustomerCRUD.ascx.cs"
    Inherits="User_Controls_UCCustomerCRUD" %>
<link href="../CSS/stylesheet.css" rel="stylesheet" type="text/css" />
<asp:UpdatePanel ID="UP2" runat="server">
    <ContentTemplate>
        <table width="100%">
            <tr>
                <td colspan="4" height="30px" valign="middle">
                    <asp:Label ID="lblResult" runat="server" CssClass="dbresult"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 20%">
                    <asp:Label ID="Label1" runat="server" CssClass="label"><span style="color:Red">*</span>First Name :</asp:Label></td>
                <td style="width: 25%">
                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="gvtextbox"></asp:TextBox></td>
                <td rowspan="2" style="padding-left: 10px; width: 25%;">
                    <asp:Label ID="Label11" runat="server" CssClass="label">&nbsp;&nbsp;Address :</asp:Label></td>
                <td rowspan="2" style="width: 30%">
                    <asp:TextBox ID="txtAddress" runat="server" CssClass="gvtextbox" Height="71px" TextMode="MultiLine"
                        Width="277px"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 20%">
                    <asp:Label ID="Label9" runat="server" CssClass="label" Width="104px"><span style="color:Red">*</span>Middle Name :</asp:Label></td>
                <td style="width: 25%">
                    <asp:TextBox ID="txtMiddleName" runat="server" CssClass="gvtextbox"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 20%">
                    <asp:Label ID="Label10" runat="server" CssClass="label"><span style="color:Red">*</span>Last Name :</asp:Label></td>
                <td style="width: 25%">
                    <asp:TextBox ID="txtLastName" runat="server" CssClass="gvtextbox"></asp:TextBox>
                </td>
                <td style="padding-left: 10px; width: 25%;">
                </td>
                <td style="width: 30%">
                </td>
            </tr>
            <tr>
                <td style="width: 20%" valign="top">
                    <asp:Label ID="Label5" runat="server" CssClass="label" Width="94px"><span style="color:Red">*</span>Postal Code :</asp:Label></td>
                <td style="width: 25%">
                    <asp:TextBox ID="txtPCode" runat="server" CssClass="gvtextbox"></asp:TextBox>
                    <br />
                    <asp:LinkButton ID="lbtGetLocations" runat="server" OnClick="lbtGetLocations_Click"
                        ValidationGroup="VGForLocation">Get Location's</asp:LinkButton></td>
                <td style="padding-left: 10px; width: 25%;">
                    <asp:Label ID="Label4" runat="server" CssClass="label" Width="123px"><span style="color:Red">*</span>Select Location :</asp:Label></td>
                <td style="width: 30%" valign="middle">
                    <asp:DropDownList ID="ddlLocation" runat="server" DataTextField="Suburb" DataValueField="ID"
                        Width="249px">
                    </asp:DropDownList>
                    <ajaxToolkit:ListSearchExtender ID="ListSearchExtender1" runat="server" TargetControlID="ddlLocation"
                        PromptCssClass="ListSearchExtenderPrompt" PromptText="Type Here For Location">
                    </ajaxToolkit:ListSearchExtender>
                </td>
            </tr>
            <tr>
                <td style="width: 20%">
                    <asp:Label ID="Label3" runat="server" CssClass="label"><span style="color:Red">*</span>Phone :</asp:Label></td>
                <td style="width: 25%">
                    <asp:TextBox ID="txtPhone" runat="server" CssClass="gvtextbox"></asp:TextBox></td>
                <td style="padding-left: 10px; width: 25%">
                    <asp:Label ID="Label2" runat="server" CssClass="label"><span style="color:Red">*</span>Email :</asp:Label></td>
                <td style="width: 30%">
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="gvtextbox" Width="272px"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 20%">
                    &nbsp;</td>
                <td align="right" style="width: 25%">
                    <asp:HiddenField ID="hdfDBOperation" runat="server" />
                    &nbsp;
                    <asp:ImageButton ID="imgbtnAdd" runat="server" ImageUrl="~/Images/Submit.gif" OnClick="imgbtnAdd_Click"
                        onmouseout="this.src='Images/Submit.gif'" onmouseover="this.src='Images/Submit_hvr.gif'"
                        ValidationGroup="VGSubmit" />&nbsp;<asp:ImageButton ID="imgbtnCancel" runat="server"
                            ImageUrl="~/Images/Cancel.gif" OnClick="imgbtnCancel_Click" onmouseout="this.src='Images/Cancel.gif'"
                            onmouseover="this.src='Images/Cancel_hvr.gif'" CausesValidation="False" /></td>
                <td align="left" style="padding-left: 10px; width: 25%">
                </td>
                <td style="width: 30%">
                    <asp:HiddenField ID="hdfID" runat="server" />
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress ID="upProcess" runat="server" DisplayAfter="500">
    <ProgressTemplate>
        <div id="progressBackgroundFilter">
        </div>
        <div id="processMessage">
            <span style="text-align: center;">
                <img src="../images/loading.gif" /><br />
                Loading...Please wait...</span></div>
    </ProgressTemplate>
</asp:UpdateProgress>
<asp:RequiredFieldValidator ID="rfvPhone" runat="server" ErrorMessage="Phone Required"
    ControlToValidate="txtPhone" Display="None" ValidationGroup="VGSubmit" SetFocusOnError="True"></asp:RequiredFieldValidator><asp:RequiredFieldValidator
        ID="rfvPostalCode" runat="server" ControlToValidate="txtPCode" CssClass="gvValidationError"
        Display="None" ErrorMessage="Postal Code  Required" SetFocusOnError="True" ValidationGroup="VGSubmit"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
            ID="revEmail" runat="server" Width="63px" ValidationGroup="VGSubmit" ErrorMessage="Invalid Email Address"
            Display="None" ControlToValidate="txtEmail" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
<asp:RequiredFieldValidator ID="rfvLocation" runat="server" ControlToValidate="ddlLocation"
    CssClass="gvValidationError" Display="None" ErrorMessage="Please select the location"
    InitialValue="-Select-" SetFocusOnError="True" ValidationGroup="VGSubmit"></asp:RequiredFieldValidator>
<asp:RequiredFieldValidator ID="rfvPostalCodeForLocations" runat="server" ControlToValidate="txtPCode"
    CssClass="gvValidationError" Display="None" ErrorMessage="Postal Code  Required"
    SetFocusOnError="True" ValidationGroup="VGForLocation"></asp:RequiredFieldValidator>
<ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server"
    HighlightCssClass="validatorCalloutHighlight" TargetControlID="rfvPostalCode">
</ajaxToolkit:ValidatorCalloutExtender>
<ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server"
    HighlightCssClass="validatorCalloutHighlight" TargetControlID="rfvMiddleName">
</ajaxToolkit:ValidatorCalloutExtender>
<ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server"
    HighlightCssClass="validatorCalloutHighlight" TargetControlID="rfvLastName">
</ajaxToolkit:ValidatorCalloutExtender>
<ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
    HighlightCssClass="validatorCalloutHighlight" TargetControlID="rfvPhone">
</ajaxToolkit:ValidatorCalloutExtender>
<ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
    HighlightCssClass="validatorCalloutHighlight" TargetControlID="revEmail">
</ajaxToolkit:ValidatorCalloutExtender>
<ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2_ucCustomerCRUD" runat="server"
    HighlightCssClass="validatorCalloutHighlight" TargetControlID="rfvFirstName">
</ajaxToolkit:ValidatorCalloutExtender>
<asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="txtLastName"
    CssClass="gvValidationError" Display="None" ErrorMessage="Last Name Required"
    SetFocusOnError="True" ValidationGroup="VGSubmit"></asp:RequiredFieldValidator><asp:RequiredFieldValidator
        ID="rfvMiddleName" runat="server" ControlToValidate="txtMiddleName" CssClass="gvValidationError"
        Display="None" ErrorMessage="Middle Name Required" SetFocusOnError="True" ValidationGroup="VGSubmit"></asp:RequiredFieldValidator><asp:RequiredFieldValidator
            ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName" CssClass="gvValidationError"
            Display="None" ErrorMessage="First Name Required" ValidationGroup="VGSubmit"
            SetFocusOnError="True"></asp:RequiredFieldValidator>
<ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server"
    HighlightCssClass="validatorCalloutHighlight" TargetControlID="rfvLocation">
</ajaxToolkit:ValidatorCalloutExtender>
<ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server"
    HighlightCssClass="validatorCalloutHighlight" TargetControlID="rfvPostalCodeForLocations">
</ajaxToolkit:ValidatorCalloutExtender>
<asp:RegularExpressionValidator ID="revPostalCode" runat="server" ControlToValidate="txtPcode"
    ErrorMessage="Enter Correct Postal Code" Display="None" ValidationGroup="VGForLocation"
    ValidationExpression="^(0[289][0-9]{2})|([1345689][0-9]{3})|(2[0-8][0-9]{2})|(290[0-9])|(291[0-4])|(7[0-4][0-9]{2})|(7[8-9][0-9]{2})$">
</asp:RegularExpressionValidator>
<ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server"
    HighlightCssClass="validatorCalloutHighlight" TargetControlID="revPostalCode">
</ajaxToolkit:ValidatorCalloutExtender>
<asp:RegularExpressionValidator ID="revForSubmit" runat="server" ControlToValidate="txtPCode"
    Display="None" ErrorMessage="Enter Correct Postal Code" ValidationExpression="^(0[289][0-9]{2})|([1345689][0-9]{3})|(2[0-8][0-9]{2})|(290[0-9])|(291[0-4])|(7[0-4][0-9]{2})|(7[8-9][0-9]{2})$"
    ValidationGroup="VGSubmit"></asp:RegularExpressionValidator>
<ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server"
    HighlightCssClass="validatorCalloutHighlight" TargetControlID="revForSubmit">
</ajaxToolkit:ValidatorCalloutExtender>
