<%@ Page Title="Send SMS" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="SendSMS.aspx.cs" Inherits="SendSMS" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">

        //        window.onload = function() {
        //            var txt = document.getElementById('<%=txtDesc.ClientID%>');
        //            txt.value = "<br/>Best no. is <Phone> thanks <First Name> <Surname>, Private Fleet. PS please don’t reply to this number";
        //          //  txt.value = str_replace("<br>", "\n", txt.value);

        //        }

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

        function chkLength(evt, th) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            //alert(charCode);
            var lbl = document.getElementById('<%=lblSMS.ClientID%>');
            var txt = document.getElementById(th.id);
            if (charCode == 9) // for tab
                return true;
            if (charCode == 8 || charCode == 27 || charCode == 16 || charCode == 46 || charCode == 37 || charCode == 38 || charCode == 39 || charCode == 40) {
                if (charCode == 8 || charCode == 46) {
                    //alert(parseInt(lbl.innerHTML));
                    if (parseInt(lbl.innerHTML) < 160)
                        lbl.innerHTML = txt.value.length;
                    //lbl.innerHTML = parseInt(lbl.innerHTML) + 1;
                }
                return true;
            }


            //            if (txt.value.length >= 160) {
            //                alert("Maximum limit reached");
            //                lbl.innerHTML = 160 - (txt.value.length + 1);
            //                return false;
            //            }
            //            else {
            //                // alert(lbl.innerHTML);
            //                lbl.innerHTML = 160 - (txt.value.length + 1);
            //            }

            lbl.innerHTML = txt.value.length;
        }

        function chkLimit() {
            var lbl = document.getElementById('<%=lblSMS.ClientID%>');
            var txt = document.getElementById('<%=txtDesc.ClientID %>');
            if (txt.value.length >= 160) {
                lbl.innerHTML = 160 - (txt.value.length + 1);
                alert("SMS length Exceed Maximun Limit (i.e. 160 Character).");
                return false;
            }
        }
    </script>

    <div id="divPopup" runat="server" align="center" style="width: 75%; height: 90%;">
        <div id="dvEnq" runat="server" align="center" style="width: 100%;">
            <table width="100%">
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblErrMsg" Font-Bold="true" ForeColor="Red" align="Center" runat="server"
                            Visible="false"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblSMSTo" Width="120" runat="server" Text="Mobile Number"></asp:Label>
                    </td>
                    <td align="left">
                        <%-- +&nbsp;<asp:Label ID="lblCountryCode" runat="server" Text="61"></asp:Label>&nbsp;---%>
                        <asp:TextBox ID="txtSMSTo_1" ReadOnly="true" runat="server" Text="04" Width="25"
                            Visible="false"></asp:TextBox>
                        <asp:TextBox ID="txtSMSTo" onkeypress="return isNumberKey(event,this);" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvSMSTo" runat="server" ControlToValidate="txtSMSTo"
                            CssClass="gvValidationError" Display="None" ErrorMessage="Required" ValidationGroup="VGSubmit"
                            Width="145px" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        <ajaxToolkit:ValidatorCalloutExtender ID="vceSMSTo" runat="server" HighlightCssClass="validatorCalloutHighlight"
                            TargetControlID="rfvSMSTo">
                        </ajaxToolkit:ValidatorCalloutExtender>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        Messege (<asp:Label ID="lblSMS" runat="server" Text="160"></asp:Label>)
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtDesc" onkeypress="return chkLength(event,this);" runat="server"
                            TextMode="MultiLine" MaxLength="160" Width="480px" Height="100px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvSMSText" runat="server" ControlToValidate="txtDesc"
                            CssClass="gvValidationError" Display="None" ErrorMessage="Required" ValidationGroup="VGSubmit"
                            Width="145px" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        <ajaxToolkit:ValidatorCalloutExtender ID="vceSMSText" runat="server" HighlightCssClass="validatorCalloutHighlight"
                            TargetControlID="rfvSMSText">
                        </ajaxToolkit:ValidatorCalloutExtender>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                    </td>
                    <td align="left">
                        <asp:RadioButtonList RepeatDirection="Vertical" Width="500px" ID="radioPreset" AutoPostBack="true"
                            TextAlign="Right" runat="server" OnSelectedIndexChanged="radioPreset_SelectedIndexChanged">
                            <asp:ListItem Value="1" Text=" Hi when you get a chance could you pls give me a call?"></asp:ListItem>
                            <asp:ListItem Value="2" Text=" Hi just tried 2 call re ur new car but couldn’t get through. Pls call when u get a chance"></asp:ListItem>
                            <asp:ListItem Value="3" Text=" Hi ***, Paperwork has been sent to you to confirm your *** purchase, please call *** on 1300 303 181 ext "></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:ImageButton ID="btnSend" runat="server" Text="Send" ImageUrl="~/Images/send_sms_hvr.gif"
                            onmouseout="this.src='Images/send_sms_hvr.gif'" onmouseover="this.src='Images/send_sms.gif'"
                            OnClick="btnSend_Click" ValidationGroup="VGSubmit" OnClientClick="javascript:return chkLimit()" />
                        <asp:ImageButton ID="btnCancel" runat="server" Text="Cancel" ImageUrl="~/Images/Cancel.gif"
                            onmouseout="this.src='Images/Cancel.gif'" onmouseover="this.src='Images/Cancel_hvr.gif'"
                            OnClick="btnCancel_Click" />
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
                            Are you sure you want to send SMS?
                        </td>
                    </tr>
                    <tr style="padding-top: 15px">
                        <td align="right">
                            <asp:ImageButton ID="btnConfirm" runat="server" ImageUrl="~/Images/Confirm_hover.gif"
                                onmouseout="this.src='Images/Confirm_hover.gif'" onmouseover="this.src='Images/Confirm.gif'"
                                OnClick="btnConfirm_Click" />
                        </td>
                        <td align="left">
                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.gif" onmouseout="this.src='Images/edit.gif'"
                                onmouseover="this.src='Images/edit_hover.gif'" OnClick="btnEdit_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div>
        </div>
    </div>
</asp:Content>
