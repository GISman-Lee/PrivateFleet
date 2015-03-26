<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCDealerCRUD.ascx.cs"
    Inherits="User_Controls_UCDealerCRUD" %>
<link href="../CSS/stylesheet.css" rel="stylesheet" type="text/css" />
<asp:Panel ID="DealerMasterPanel" runat="server">
    <table>
        <tr>
            <td colspan="4" height="30px" valign="middle">
                <asp:Label ID="lblResult" runat="server" CssClass="dbresult"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 108px">
                <asp:Label ID="Label1" runat="server" CssClass="label"><span style="color:Red">*</span>Dealer Name :</asp:Label>
            </td>
            <td style="width: 100px">
                <asp:TextBox ID="txtName" runat="server" Width="217px" CssClass="gvtextbox"></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName"
                    CssClass="gvValidationError" Display="None" ErrorMessage="Please Enter The Name"
                    ValidationGroup="VGSubmit" Width="203px" SetFocusOnError="True"></asp:RequiredFieldValidator>
                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2_ucDealerCRUD"
                    runat="server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator1">
                </ajaxToolkit:ValidatorCalloutExtender>
            </td>
            <td style="width: 76px; padding-left: 20px;">
                <asp:Label ID="Label9" runat="server" CssClass="label"><span>&nbsp;&nbsp;Company :</span></asp:Label>
            </td>
            <td style="width: 100px">
                <asp:TextBox ID="txtCompany" runat="server" Width="217px" CssClass="gvtextbox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 108px; height: 24px;">
                <asp:Label ID="Label2" runat="server" CssClass="label"><span style="color:Red">*</span>Email :</asp:Label>
            </td>
            <td style="width: 100px; height: 24px;">
                <asp:TextBox ID="txtEmail" runat="server" Width="217px" CssClass="gvtextbox"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="Please enter email address"
                    ControlToValidate="txtEmail" Display="None" ValidationGroup="VGSubmit" SetFocusOnError="True"></asp:RequiredFieldValidator>
                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                    HighlightCssClass="validatorCalloutHighlight" TargetControlID="rfvEmail">
                </ajaxToolkit:ValidatorCalloutExtender>
                <asp:RegularExpressionValidator ID="revEmail" runat="server" Width="63px" ValidationGroup="VGSubmit"
                    ErrorMessage="Invalid Email Address" Display="None" ControlToValidate="txtEmail"
                    SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
                    HighlightCssClass="validatorCalloutHighlight" TargetControlID="revEmail">
                </ajaxToolkit:ValidatorCalloutExtender>
            </td>
            <td style="width: 76px; padding-left: 20px;">
                <asp:Label ID="Label8" runat="server" CssClass="label"><span>&nbsp;&nbsp;Fax :</span></asp:Label>
            </td>
            <td style="width: 100px; height: 24px;">
                <asp:TextBox ID="txtFax" runat="server" Width="217px" CssClass="gvtextbox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 108px">
                <asp:Label ID="Label3" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Phone :</asp:Label>
            </td>
            <td style="width: 100px">
                <asp:TextBox ID="txtPhone" runat="server" Width="217px" CssClass="gvtextbox"></asp:TextBox>
            </td>
            <td style="width: 76px; padding-left: 20px;">
                <asp:Label ID="Label7" runat="server" CssClass="label"><span style="color:Red">*</span>State :</asp:Label>
            </td>
            <td style="width: 100px">
                <asp:DropDownList ID="ddlState" runat="server" AppendDataBoundItems="True" AutoPostBack="false"
                    DataTextField="State" DataValueField="ID" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"
                    Width="217px" CssClass="gvtextbox">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="ddlStateReq" runat="server" ControlToValidate="ddlState"
                    Display="None" ErrorMessage="Please Select the State" InitialValue="-Select-"
                    ValidationGroup="VGSubmit" Width="203px" CssClass="gvValidationError" SetFocusOnError="True"></asp:RequiredFieldValidator>
                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server"
                    HighlightCssClass="validatorCalloutHighlight" TargetControlID="ddlStateReq">
                </ajaxToolkit:ValidatorCalloutExtender>
            </td>
        </tr>
        <tr>
            <td style="width: 108px">
                <asp:Label ID="Label5" runat="server" CssClass="label"><span>&nbsp;&nbsp;Postal Code :</span></asp:Label>
            </td>
            <td style="width: 100px">
                <asp:TextBox ID="txtPCode" runat="server" Width="217px" CssClass="gvtextbox"></asp:TextBox>
                <asp:LinkButton ID="lbtGetLocations" runat="server" OnClick="lbtGetLocations_Click"
                    ValidationGroup="VGForLocation" Width="103px">Get Location's</asp:LinkButton>
                <asp:RequiredFieldValidator ID="rfvPostalCode" runat="server" ControlToValidate="txtPCode"
                    CssClass="gvValidationError" Display="None" ErrorMessage="Postal Code  Required"
                    SetFocusOnError="True" ValidationGroup="VGSubmit"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revPostalCode" runat="server" ControlToValidate="txtPcode"
                    Display="None" ErrorMessage="Enter Correct Postal Code" ValidationExpression="^(0[289][0-9]{2})|([1345689][0-9]{3})|(2[0-8][0-9]{2})|(290[0-9])|(291[0-4])|(7[0-4][0-9]{2})|(7[8-9][0-9]{2})$"
                    ValidationGroup="VGSubmit">
                </asp:RegularExpressionValidator><ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender9"
                    runat="server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="revPostalCode">
                </ajaxToolkit:ValidatorCalloutExtender>
                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server"
                    HighlightCssClass="validatorCalloutHighlight" TargetControlID="rfvPostalCode">
                </ajaxToolkit:ValidatorCalloutExtender>
            </td>
            <td style="width: 76px; padding-left: 20px;">
                <asp:Label ID="Label6" runat="server" CssClass="label" Visible="false"><span>&nbsp;&nbsp;Location :</span></asp:Label>
            </td>
            <td style="width: 100px">
                <asp:DropDownList ID="ddlLocation" runat="server" AppendDataBoundItems="True" DataTextField="Suburb"
                    DataValueField="ID" Width="217px" Visible="false">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 108px" visible="false">
                <asp:Label ID="CarMake" runat="server" CssClass="label"><span style="color:Red">*</span>Car Make :</asp:Label>
            </td>
            <td style="width: 100px" visible="false">
                <asp:DropDownList ID="ddlCarMake" runat="server" Height="16px"
                 width="217px" DataTextField="Make" DataValueField="ID" CssClass="gvtextbox" AppendDataBoundItems="True" AutoPostBack="false" 
                 >
                </asp:DropDownList>
            </td>
            <td style="width: 76px">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label4" runat="server" CssClass="label" Visible="true"><span 
                    style="color:Red">*</span>City :</asp:Label>
            </td>
            <td style="width: 100px">
                <asp:DropDownList ID="ddlCity" runat="server" AppendDataBoundItems="True" 
                    DataTextField="City" DataValueField="City" 
                    OnSelectedIndexChanged="ddlCity_SelectedIndexChanged" Visible="true" 
                    Width="217px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
          <!--  <td style="width: 108px">
                &nbsp;
            </td>-->
            <td  colspan ="2" align="right">
                <asp:HiddenField ID="hdfDBOperation" runat="server" />
                &nbsp;
                <%-- Added code by Amol --%>
                <asp:ImageButton ID="ImagebtnSearch" runat="server" ImageUrl="~/Images/Search_dealer.gif"
                    OnClick="ImagebtnSearch_Click" onmouseout="this.src='Images/Search_dealer.gif'"
                    onmouseover="this.src='Images/Search_dealer_hvr.gif'" />&nbsp;
                <%-- Added code by Amol --%>
                <asp:ImageButton ID="imgbtnAdd" runat="server" ImageUrl="~/Images/Submit.gif" OnClick="imgbtnAdd_Click"
                    onmouseout="this.src='Images/Submit.gif'" onmouseover="this.src='Images/Submit_hvr.gif'"
                    ValidationGroup="VGSubmit" />&nbsp;<asp:ImageButton ID="imgbtnCancel" runat="server"
                        ImageUrl="~/Images/Cancel.gif" OnClick="imgbtnCancel_Click" onmouseout="this.src='Images/Cancel.gif'"
                        onmouseover="this.src='Images/Cancel_hvr.gif'" CausesValidation="False" />
            </td>
            <td align="left" style="width: 76px">
            </td>
            <td style="width: 100px">
                <asp:HiddenField ID="hdfID" runat="server" />
            </td>
        </tr>
    </table>
    <asp:RegularExpressionValidator ID="revForSubmit" runat="server" ControlToValidate="txtPCode"
        Display="None" ErrorMessage="Enter Correct Postal Code" ValidationExpression="^(0[289][0-9]{2})|([1345689][0-9]{3})|(2[0-8][0-9]{2})|(290[0-9])|(291[0-4])|(7[0-4][0-9]{2})|(7[8-9][0-9]{2})$"
        ValidationGroup="VGSubmit"></asp:RegularExpressionValidator>
    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server"
        HighlightCssClass="validatorCalloutHighlight" TargetControlID="revForSubmit">
    </ajaxToolkit:ValidatorCalloutExtender>
</asp:Panel>
<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>

