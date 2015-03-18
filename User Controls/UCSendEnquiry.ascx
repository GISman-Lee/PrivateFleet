<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCSendEnquiry.ascx.cs"
    Inherits="User_Controls_UCSendEnquiry" %>

<script type="text/javascript" language="javascript">
    function isNumberKey(evt, chk) {
        var charCode = (evt.which) ? evt.which : evt.keyCode
        // alert(charCode);

        // 9- tab  46 del
        if (charCode == 9 || charCode == 8 || charCode == 27 || charCode == 46 || charCode == 37 || charCode == 39)   // 8 - backspace 27 - Escape
            return true;

        if (chk.value.length > 9) {
            return false;
        }

        if (charCode == 13) {
            return false;
        }

        //for numbers only
        if (charCode > 31 && (charCode < 48 || charCode > 57))
            return false;
        else
            return true;
    }
</script>

<div id="divPopup" runat="server" align="center" style="width: 90%; height: 90%;">
    <div id="dvEnq" runat="server" align="center" style="width: 100%;">
        <table width="100%" cellpadding="2" cellspacing="2">
            <tr id="trTitle" runat="server">
                <td style="background-color: #17608C; padding: 5px;" colspan="2">
                    <span style="text-align: center;"><b>
                        <asp:Label ID="lblTitle" runat="server" ForeColor="White"></asp:Label></b></span>
                    <span style="float: right; margin-top: -1px; margin-right: 8px; z-index: 2;">
                        <asp:ImageButton Visible="false" ID="btnPopClose" runat="server" ImageUrl="~/Images/cancel.png"
                            OnClick="btnPopClose_Click" />
                    </span>
                </td>
            </tr>
            <tr id="trSurname" runat="server">
                <td style="width: 20%;">
                    <asp:Label ID="lblSurname" runat="server" Text="Surname"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSurname" runat="server" MaxLength="250"></asp:TextBox>
                </td>
            </tr>
            <tr id="trPhone" runat="server">
                <td>
                    <asp:Label ID="lblPhone" runat="server" Text="Phone No."></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPhone" runat="server" onkeypress="return isNumberKey(event,this);"></asp:TextBox>
                    <asp:CompareValidator ID="cmpVal" runat="server" ControlToValidate="txtPhone" Operator="DataTypeCheck"
                        Type="Integer" ValidationGroup="SendMail"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
            </tr>
            <tr>
                <td valign="top">
                    <asp:Label ID="lblComment" runat="server" Text="Comments"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDesc" runat="server" TextMode="MultiLine" MaxLength="5000" Width="490px"
                        Height="170px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                </td>
                <td align="left">
                    <asp:ImageButton ID="btnSend" runat="server" Text="Send" OnClick="btnSend_Click"
                        ImageUrl="~/Images/send_mail_hvr.gif" onmouseout="this.src='Images/send_mail_hvr.gif'"
                        onmouseover="this.src='Images/send_mail.gif'" />
                    <asp:ImageButton ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                        ImageUrl="~/Images/Cancel.gif" onmouseout="this.src='Images/Cancel.gif'" onmouseover="this.src='Images/Cancel_hvr.gif'" />
                </td>
            </tr>
        </table>
    </div>
    <div id="divConfirm" runat="server" visible="false">
        <div id="progressBackgroundFilter">
        </div>
        <div id="processMessage2">
            <table cellpadding="4" cellspacing="4" width="250px" height="50px">
                <tr style="background-color: #0265FF;">
                    <td colspan="2" align="right">
                        <asp:ImageButton ID="imgBtnConfirm" runat="server" ImageUrl="~/Images/cancel.png"
                            OnClick="imgBtnConfirm_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        Are you sure you want to send mail?
                    </td>
                </tr>
                <tr style="padding-top: 15px">
                    <td align="right">
                        <asp:ImageButton ID="btnConfirm" runat="server" ImageUrl="~/Images/Confirm_hover.gif"
                            OnClick="btnConfirm_Click" onmouseout="this.src='Images/Confirm_hover.gif'" onmouseover="this.src='Images/Confirm.gif'" />
                    </td>
                    <td align="left">
                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.gif" OnClick="btnEdit_Click"
                            onmouseout="this.src='Images/edit.gif'" onmouseover="this.src='Images/edit_hover.gif'" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div>
    </div>
</div>
