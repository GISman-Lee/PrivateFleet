<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UC_VDT_Loginl.ascx.cs" Inherits="VDT_Customer_User_Controls_UC_VDT_Loginl" %>
<link href="..../../CSS/stylesheet.css" rel="stylesheet" type="text/css" />
<script src="../../js/fleetvalidation.js" type ="text/javascript"></script>
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

            if (x == "VDTUName" && y != null) {
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
                        if (x1 == "VDTPwd" && y1 != null) {
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
                    <table>
                        <tr>
                            <td align="right">
                                <asp:Label ID="Label1" runat="server" CssClass="label" Text="Surname :"></asp:Label>
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
                                <asp:Label ID="Label2" runat="server" CssClass="label" Text="Member Number : "></asp:Label>
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
                                    OnClick="imgbtnAdd_Click" onmouseout="this.src='../../Images/Submit.gif'" onmouseover="this.src='../../Images/Submit_hvr.gif'"
                                    ValidationGroup="VGSubmit" />&nbsp;
                            </td>
                            <td>
                            </td>
                        </tr>
                        
                        <tr>
                            <td align="center" colspan="3" style="height: 22px">
                                <asp:Label ID="lblError" runat="server" CssClass="dbresult"></asp:Label>
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
                            <td style=" text-align: right; vertical-align: top; padding-left: 40px;">
                               <b><span style="font-size: 17px"></span>Notes&nbsp;:</b>
                            </td>
                            <td style="padding-left: 10px;">
                                If you do not know <b style="font-size: 14px;">what your password is,</b> please
                                <asp:LinkButton runat ="server" ID="lnkClickHere" Text ="Click Here" CausesValidation ="false" OnClick ="lnkClickHere_Click"  style="color: #055f86; text-align: left;"></asp:LinkButton>
                              <%--  <a id="A1" href="#" runat="server" style="color: #055f86; text-align: left;" onclick ="return show_info()">
                                    <b>click here.</b></a>--%>
                            </td>
                        </tr>
                      
                    </table>
                    
                      <asp:Label runat ="server" ID="lblCancelid"></asp:Label>
            <asp:Label runat ="server" ID="lblinvoke"></asp:Label>
          
            
                <ajaxToolkit:ModalPopupExtender runat ="server" ID="modal" TargetControlID ="lblinvoke" CancelControlID ="lblCancelid" PopupControlID ="pnlmodal" BackgroundCssClass ="ModalCSS"  Enabled ="false">
                </ajaxToolkit:ModalPopupExtender>
                <asp:Panel runat ="server" ID ="pnlmodal" BackColor ="White" Visible ="false">
                  <table width="400px">
                    <tr>
                        <td style ="background-color :#0A73A2;color :White; font-weight :bold;padding-left :5px;height:30px;font-size:large" >
                            Private Fleet
                        </td>
                    </tr>
                    <tr>
                        <td style ="height:5px">
                        
                        </td>
                    </tr>
                    <tr>
                        <td style ="padding-left:5px">
                            Your password is your member number and can be found on your original paperwork.  Please call us on 1300 303 181 if you have any trouble.
                        </td>
                    </tr>
                      <tr>
                        <td style ="height:5px">
                        
                        </td>
                    </tr>
                    <tr>
                        <td align ="center">
                        <asp:Button runat ="server" ID="btnClose" Text ="Ok" OnClick ="btnClose_Click" />        
                        </td>
                    </tr>
                     <tr>
                        <td style ="height:5px">
                        
                        </td>
                    </tr>
                   </table>
                    
                </asp:Panel>
                  
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
                            <td align="center" colspan="2">
                                <asp:Label ID="lblMsg" runat="server"  Text="" CssClass="dbresult"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td colspan="0" align="left">
                                <asp:ImageButton ID="imgbut_Email" runat="server" ImageUrl="~/Images/Submit.gif"
                                    OnClick="imgbut_Email_Click" onmouseout="this.src='../../Images/Submit.gif'" onmouseover="this.src='../../Images/Submit_hvr.gif'"
                                    ValidationGroup="VGSubmit" />&nbsp; &nbsp;<asp:ImageButton ID="imgbut_Back" CausesValidation="false"
                                        runat="server" ImageUrl="~/Images/back.gif" OnClick="imgbut_Back_Click" onmouseout="this.src='Images/back.gif'"
                                        onmouseover="this.src='Images/back_hvr.gif'" ValidationGroup="VGSubmit" />
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
