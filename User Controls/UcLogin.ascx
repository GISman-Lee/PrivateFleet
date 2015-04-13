<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UcLogin.ascx.cs" Inherits="User_Controls_UcLogin" %>
<link href="../CSS/stylesheet.css" rel="stylesheet" type="text/css" />
<%--<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>--%>
<ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</ajaxToolkit:ToolkitScriptManager>

<script type="text/javascript" language="javascript">
    function chkCookies(id) {
        // alert("hi")

        var gotCookie = (navigator.cookieEnabled) ? true : false;

        if (typeof navigator.cookieEnabled == 'undefined' && !gotCookie) {
            document.cookie = 'test';
            gotCookie = (document.cookie.indexOf('test') != -1) ? true : false;
        }

        //        alert(document.cookie)
        //        alert(document.domain)
        var txtUN = document.getElementById('<%=txtUserName.ClientID%>');
        var txtPass = document.getElementById('<%=txtPassword.ClientID%>');
        var chk = document.getElementById('<%=chkRememberMe.ClientID%>');
        //  alert(txtUN + " - " + txtPass + " - " + chk);
        //alert(txtUN.value + " - " + txtPass.value + " - " + chk.value);

        var i, x, y, x1, y1, j, ARRcookies = document.cookie.split(";");
        for (i = 0; i < ARRcookies.length; i++) {
            x = ARRcookies[i].substr(0, ARRcookies[i].indexOf("="));
            y = ARRcookies[i].substr(ARRcookies[i].indexOf("=") + 1);
            x = x.replace(/^\s+|\s+$/g, "");
            // alert(x + " - " + y);

            if (x == "UName" && y != null) {
                if (txtUN.value != y) {
                    txtPass.value = "";
                    chk.checked = false;
                }
                else {
                    for (j = 0; j < ARRcookies.length; j++) {
                        x1 = ARRcookies[j].substr(0, ARRcookies[j].indexOf("="));
                        y1 = ARRcookies[j].substr(ARRcookies[j].indexOf("=") + 1);
                        x1 = x1.replace(/^\s+|\s+$/g, "");
                        //alert(x1 + " - " + y1);
                        if (x1 == "Pwd" && y1 != null) {
                            txtPass.value = y1;
                            chk.checked = true;
                        }
                    }
                }
            }
        }
    }
</script>

<asp:UpdatePanel ID="UP2" runat="server">
    <ContentTemplate>
        <div id="progressBackgroundFilter12" class="divChangePass" visible="false" runat="server">
            New Password is send to your<br />
            registered Email ID.
            <p>
                <asp:ImageButton ID="but_ok" runat="server" ImageUrl="~/Images/ok_hvr.gif" OnClick="but_ok_Click"
                    onmouseout="this.src='Images/ok_hvr.gif'" onmouseover="this.src='Images/ok.gif'"
                    ValidationGroup="VGSubmit" />
            </p>
        </div>
        <asp:Panel ID="PanLogin" runat="server">
            <asp:MultiView ID="MultiViewLogin" runat="server" ActiveViewIndex="0">
                <asp:View ID="ViewLogin" runat="server">
                    <table width="80%" style="text-align: center;" cellpadding="1" cellspacing="2">
                        <tr>
                            <td align="center" style="height: 22px">
                                <asp:Label ID="lblError" runat="server" CssClass="dbresult"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table align="center">
                        <tr>
                            <td align="right">
                                <asp:Label ID="Label1" runat="server" CssClass="label" Text="Email :"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtUserName" runat="server" onchange="javascript:chkCookies('txtUserName');"
                                    TabIndex="1"></asp:TextBox>
                                <br />
                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2_ucLogin" runat="server"
                                    HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator2">
                                </ajaxToolkit:ValidatorCalloutExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtUserName"
                                    CssClass="gvValidationError" Display="None" ErrorMessage="User Name Required"
                                    ValidationGroup="VGSubmit" Width="145px" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="Label2" runat="server" CssClass="label" Text="Password : "></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" TabIndex="2"></asp:TextBox>
                                <%--<a id="A1" href="../Index.aspx?id=4got" runat="server" style="font-size: 11px; color: #055f86;
                                text-decoration: none; text-align: left;">Forgot Password?</a>--%>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPassword"
                                    CssClass="gvValidationError" Display="None" ErrorMessage="Password Required"
                                    ValidationGroup="VGSubmit" Width="145px" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                    HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator1">
                                </ajaxToolkit:ValidatorCalloutExtender>
                            </td>
                            <td align="left" style="font-size: 11px; color: #055f86;">
                                <%--  If you <b>Forgot Password</b>, please <a id="A2" href="../Index.aspx?id=4got" runat="server"
                                    style="font-size: 11px; color: #055f86; text-align: left;">click here</a>--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td align="left" style="font-size: 12px; color: #055f86;">
                                <%--   <a id="A2" href="../Index.aspx?id=4got" runat="server"
                                    style="font-size: 11px; font-weight:bolder; color: #055f86; text-align: left;">Forgot Password</a>
                              --%>
                                <asp:CheckBox ID="chkRememberMe" TabIndex="3" runat="server" AutoPostBack="false"
                                    Text="Remember me" TextAlign="Right" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td align="left" colspan="0">
                                <asp:ImageButton ID="imgbtnAdd" TabIndex="4" runat="server" ImageUrl="~/Images/Submit.gif"
                                    OnClick="imgbtnAdd_Click" onmouseout="this.src='Images/Submit.gif'" onmouseover="this.src='Images/Submit_hvr.gif'"
                                    ValidationGroup="VGSubmit" />&nbsp;
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                    <table width="80%" style="text-align: left; font-size: 13px; color: #055f86;" cellpadding="1"
                        cellspacing="2">
                        <%--   <td colspan="2" align="center" style="font-size: 12px; color: #055f86;">
                            <b><span style="font-size: 16px">* </span>Note-</b> If you are a multi-franchised
                            dealer, please make sure you use the password that relates to the brand of car you
                            are quoting.
                            <br />
                            If you don’t know what this is, please <a id="A1" href="../Index.aspx?id=4got" runat="server"
                                style="font-size: 11px; text-align: left;">click here</a>
                        </td>--%>
                        <tr>
                            <td colspan="2" align="left" style="padding: 0 0 10px 0;">
                                <b><span style="font-size: 17px">* </span>Notes&nbsp;:</b>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 5px; text-align: right; vertical-align: top; padding-left: 40px;">
                                <b>1)</b>
                            </td>
                            <td style="padding-left: 10px;">
                                If you are a <b style="font-size: 14px;">multi-franchised dealer,</b> you will have
                                a different password for each brand (and the same email). So, for your convenience,
                                please change each password to reflect the brand. Eg ‘honda42’, ‘nissan42’ etc
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 5px; text-align: right; vertical-align: top; padding-left: 40px;">
                                <b>2)</b>
                            </td>
                            <td style="padding-left: 10px;">
                                If you do not know <b style="font-size: 14px;">what your password is,</b> please
                                <a id="A1" href="../Index.aspx?id=4got" runat="server" style="color: #055f86; text-align: left;">
                                    <b>click here.</b></a> Please ensure that you use the same email address where
                                you received notification of an outstanding Quote Request
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 5px; text-align: right; vertical-align: top; padding-left: 40px;">
                                <b>3)</b>
                            </td>
                            <td style="padding-left: 10px;">
                                If you log in and <b style="font-size: 14px;">can not see an expected Quote Request,</b>
                                it is likely that you have logged in under a different franchise. Please ensure
                                you log in using the email to which the Quote Request notification was sent and
                                the password relating to that brand. If you do not know what that password is please
                                <a id="A3" href="../Index.aspx?id=4got" runat="server" style="color: #055f86; text-align: left;">
                                    <b>click here.</b></a>
                            </td>
                        </tr>
                    </table>
                </asp:View>
                <asp:View ID="ViewForgotPass" runat="server">
                    <table>
                        <tr>
                            <td align="right">
                                <asp:Label ID="lbl_Role" runat="server" Text="Please Select your Role :"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddl_Role" AutoPostBack="true" OnSelectedIndexChanged="ddl_Role_SelectedIndexChanged"
                                    runat="server" Width="220px">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_Role" runat="server" ControlToValidate="ddl_Role"
                                    CssClass="gvValidationError" Display="None" ErrorMessage="Select your Role" ValidationGroup="VGSubmit"
                                    InitialValue="0" Width="145px" SetFocusOnError="True">
                                </asp:RequiredFieldValidator>
                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_Role" runat="server"
                                    HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_Role">
                                </ajaxToolkit:ValidatorCalloutExtender>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="Label3" runat="server" Text="Please enter your Email :"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFEmail" runat="server" Width="220px"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator_txtEmail" runat="server"
                                    ControlToValidate="txtFEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                    ErrorMessage="Invalid Email id." Display="None" SetFocusOnError="true">
                                </asp:RegularExpressionValidator>
                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_txtEmail" runat="server"
                                    HighlightCssClass="validatorCalloutHighlight" TargetControlID="RegularExpressionValidator_txtEmail">
                                </ajaxToolkit:ValidatorCalloutExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_txtEmail" runat="server" ControlToValidate="txtFEmail"
                                    CssClass="gvValidationError" Display="None" ErrorMessage="Email Required" ValidationGroup="VGSubmit"
                                    SetFocusOnError="True">
                                </asp:RequiredFieldValidator>
                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_txtEmail1" runat="server"
                                    HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_txtEmail">
                                </ajaxToolkit:ValidatorCalloutExtender>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="lbl_make" runat="server" Text="Please Select Make :"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddl_make" runat="server" Width="220px">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_Make" runat="server" ControlToValidate="ddl_make"
                                    CssClass="gvValidationError" Display="None" ErrorMessage="Make Required" ValidationGroup="VGSubmit"
                                    InitialValue="0" Width="145px" SetFocusOnError="True">
                                </asp:RequiredFieldValidator>
                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_Make" runat="server"
                                    HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_Make">
                                </ajaxToolkit:ValidatorCalloutExtender>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2" style="padding: 15px 5px; width: 600px;">
                                <asp:Label ID="lblMsg" runat="server" Text="" CssClass="dbresult"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td colspan="0" align="left">
                                <asp:ImageButton ID="imgbut_Email" runat="server" ImageUrl="~/Images/Submit.gif"
                                    OnClick="imgbut_Email_Click" onmouseout="this.src='Images/Submit.gif'" onmouseover="this.src='Images/Submit_hvr.gif'"
                                    ValidationGroup="VGSubmit" />&nbsp; &nbsp;
                                <asp:ImageButton ID="imgbut_Back" CausesValidation="false" runat="server" ImageUrl="~/Images/back.gif"
                                    OnClick="imgbut_Back_Click" onmouseout="this.src='Images/back.gif'" onmouseover="this.src='Images/back_hvr.gif'"
                                    ValidationGroup="VGSubmit" />
                            </td>
                        </tr>
                    </table>
                </asp:View>
            </asp:MultiView>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress ID="upProcess" runat="server" DisplayAfter="10" AssociatedUpdatePanelID="UP2">
    <ProgressTemplate>
        <div id="progressBackgroundFilter">
        </div>
        <div id="processMessage">
            <span style="text-align: center;">
                <img alt="" src="Images/loading.gif" /><br />
                Loading...Please wait...</span></div>
    </ProgressTemplate>
</asp:UpdateProgress>
