<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucChangePassword.ascx.cs"
    Inherits="User_Controls_ucChangePassword" %>
<link href="../CSS/stylesheet.css" rel="stylesheet" type="text/css" />
<asp:UpdatePanel ID="UP2" runat="server">
    <ContentTemplate>
        <table>
         <tr>
                <td align="center"  style="height: 34px">
                    <asp:Label ID="lblError" runat="server" CssClass="dbresult"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    <table>
            
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" CssClass="label" Text="Current  Password :"></asp:Label></td>
                <td style="width: 100px; height: 26px">
                    <asp:TextBox ID="txtoldPassword" runat="server" TextMode="Password"></asp:TextBox><br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtoldPassword"
                        CssClass="gvValidationError" Display="None" ErrorMessage="Current Password  Required"
                        ValidationGroup="VGSubmit" Width="145px" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2_ucchangePass" runat="server"
                        HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator2">
                    </ajaxToolkit:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" CssClass="label" Text="New Password : "></asp:Label>
                <td style="width: 100px">
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPassword"
                        CssClass="gvValidationError" Display="None" ErrorMessage="New Password Required"
                        SetFocusOnError="True" ValidationGroup="VGSubmit" Width="145px"></asp:RequiredFieldValidator>
                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                        HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator1">
                    </ajaxToolkit:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" CssClass="label" Text="Confirm Password : "></asp:Label></td>
                <td style="width: 100px">
                    <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtConfirmPassword"
                        CssClass="gvValidationError" Display="None" ErrorMessage="New Password and Confirm password should be same"
                        SetFocusOnError="True" ValidationGroup="VGSubmit" Width="145px"></asp:RequiredFieldValidator>
                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
                        HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator3">
                    </ajaxToolkit:ValidatorCalloutExtender>
                    <asp:CompareValidator ID="cvPassword" runat="server" ControlToCompare="txtPassword"
                        ControlToValidate="txtConfirmPassword" CssClass="gvValidationError" Display="None"
                        ErrorMessage="New Password and Confirm password should be same" SetFocusOnError="True"
                        ValidationGroup="VGSubmit"></asp:CompareValidator>
                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server"
                        HighlightCssClass="validatorCalloutHighlight" TargetControlID="cvPassword">
                    </ajaxToolkit:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr>
                <td align="right" colspan="2">
                    &nbsp;<asp:ImageButton ID="imgbtnChangePassword" runat="server" ImageUrl="~/Images/Submit.gif"
                        OnClick="imgbtnChangePassword_Click" onmouseout="this.src='Images/Submit.gif'"
                        onmouseover="this.src='Images/Submit_hvr.gif'" ValidationGroup="VGSubmit" />&nbsp;</td>
            </tr>
            </table>
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
