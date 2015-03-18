<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCUserMasterCRUD.ascx.cs"
    Inherits="User_Controls_UCUserMasterCRUD" %>
<link href="../CSS/stylesheet.css" rel="stylesheet" type="text/css" />
<asp:Panel ID="UserMasterPanel" runat="server">
    <table style="padding-top: 20px;">
        <tr>
            <td colspan="4">
                <asp:Label ID="lblResult" runat="server" CssClass="dbresult"></asp:Label>
            </td>
        </tr>
        <tr>
            <td valign="top" align="left" width="108px">
                <span style="color: Red">*</span> Role :
            </td>
            <td align="left" width="100px">
                <asp:DropDownList ID="ddlRoles" runat="server" AutoPostBack="true" DataValueField="ID"
                    DataTextField="Role" Width="154px" OnSelectedIndexChanged="ddlRoles_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvRole" runat="server" ControlToValidate="ddlRoles"
                    Display="None" ErrorMessage="Please Select the Role" InitialValue="-Select-"
                    SetFocusOnError="True" ValidationGroup="VGSubmit" Width="152px" CssClass="gvValidationError"></asp:RequiredFieldValidator>
                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server"
                    HighlightCssClass="validatorCalloutHighlight" TargetControlID="rfvRole">
                </ajaxToolkit:ValidatorCalloutExtender>
            </td>
            <td valign="top" align="left" width="76px">
                &nbsp;&nbsp;&nbsp;Dealer :
            </td>
            <td align="left" width="100px">
                <asp:DropDownList ID="ddlDealer" runat="server" AutoPostBack="true" Width="200px"
                    AppendDataBoundItems="True" OnSelectedIndexChanged="ddlDealer_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td valign="top" style="height: 23px" align="left" width="108px">
                <span style="color: Red">*</span> Name :
            </td>
            <td style="height: 23px" align="left" width="100px">
                <asp:TextBox ID="txtName" runat="server" Width="150px"></asp:TextBox>
                <br />
                <asp:RequiredFieldValidator ID="rfvName" runat="server" ErrorMessage="Please enter name"
                    ControlToValidate="txtName" ValidationGroup="VGSubmit" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                    HighlightCssClass="validatorCalloutHighlight" TargetControlID="rfvName">
                </ajaxToolkit:ValidatorCalloutExtender>
            </td>
            <td valign="top" align="left" width="76px">
                <span style="color: Red">*</span> Email :
            </td>
            <td align="left" width="100px">
                <asp:TextBox ID="txtEmail" runat="server" Width="200px"></asp:TextBox>
                <br />
                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="Please enter email"
                    ControlToValidate="txtEmail" ValidationGroup="VGSubmit" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="REVEMail" runat="server" ControlToValidate="txtEmail"
                    Display="None" ErrorMessage="Please enter correct email address" SetFocusOnError="True"
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="VGSubmit"></asp:RegularExpressionValidator>
                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2_ucUserMasterCRUD" runat="server"
                    HighlightCssClass="validatorCalloutHighlight" TargetControlID="rfvEmail">
                </ajaxToolkit:ValidatorCalloutExtender>
                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server"
                    HighlightCssClass="validatorCalloutHighlight" TargetControlID="REVEMail">
                </ajaxToolkit:ValidatorCalloutExtender>
            </td>
        </tr>
        <tr>
            <td valign="top" align="left" width="108px">
                &nbsp;&nbsp;&nbsp;Phone :
            </td>
            <td align="left" valign="top" width="100px">
                <asp:TextBox ID="txtPhone" runat="server" Width="150px"></asp:TextBox>
            </td>
            <td valign="top" align="left" width="76px">
                &nbsp;&nbsp;&nbsp;Address :
            </td>
            <td align="left" width="100px">
                <asp:TextBox ID="txtAddress" runat="server" Width="200px" TextMode="MultiLine" Rows="2"></asp:TextBox>
            </td>
        </tr>
        <tr runat="server" id="trUserName">
            <td valign="top" align="left" width="108px">
                <%-- <span style="color: Red">*</span> User Name :--%>
            </td>
            <td align="left" width="100px">
                <asp:TextBox ID="txtUserName" runat="server" Width="150px"></asp:TextBox>
                <br />
                <%--  <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ErrorMessage="Please enter username"
                                        ControlToValidate="txtUserName" ValidationGroup="VGSubmit" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
                                        HighlightCssClass="validatorCalloutHighlight" TargetControlID="rfvUsername">
                                    </ajaxToolkit:ValidatorCalloutExtender>--%>
            </td>
            <td valign="top" align="left" width="76px">
                <asp:Label ID="Label8" runat="server" CssClass="label" Visible="false">Username Expiry Date</asp:Label>
            </td>
            <td align="left" width="100px">
                <asp:TextBox ID="txtExpiryDate" runat="server" Visible="false"></asp:TextBox>
            </td>
        </tr>
        <tr id="trPassword" runat="server">
            <td valign="top" align="left" width="108px">
                <%--   <span style="color: Red">*</span> Password :--%>
            </td>
            <td align="left" width="100px">
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="150px"></asp:TextBox>
                <br />
                <%--<asp:RequiredFieldValidator ID="rfvPassword" runat="server" ErrorMessage="Please enter password"
                                        ControlToValidate="txtPassword" ValidationGroup="VGSubmit" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server"
                                        HighlightCssClass="validatorCalloutHighlight" TargetControlID="rfvPassword">
                                    </ajaxToolkit:ValidatorCalloutExtender>--%>
            </td>
            <td width="76px">
            </td>
            <td width="100px">
            </td>
        </tr>
        <tr>
            <td align="center" colspan="4">
                <asp:ImageButton ID="imgbtnAdd" runat="server" ImageUrl="~/Images/AddUser.gif" OnClick="imgbtnAdd_Click"
                    onmouseout="this.src='Images/AddUser.gif'" onmouseover="this.src='Images/AddUser_hvr.gif'"
                    ValidationGroup="VGSubmit" />
                <asp:ImageButton ID="imgbtnSearch" runat="server" ImageUrl="~/Images/searchUser.gif"
                    OnClick="imgbtnSearch_Click" onmouseout="this.src='Images/searchUser.gif'" onmouseover="this.src='Images/searchUser_hvr.gif'"
                    CausesValidation="False" />
                <asp:ImageButton ID="imgbtnCancel" runat="server" ImageUrl="~/Images/Cancel.gif"
                    OnClick="imgbtnCancel_Click" onmouseout="this.src='Images/Cancel.gif'" onmouseover="this.src='Images/Cancel_hvr.gif'"
                    CausesValidation="False" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hdfID" runat="server" />
    <%--<ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Animated="true"
        checkdate="true" Format="MM/dd/yyyy" TargetControlID="txtExpiryDate" PopupButtonID="txtExpiryDate">
    </ajaxToolkit:CalendarExtender>--%>
</asp:Panel>
