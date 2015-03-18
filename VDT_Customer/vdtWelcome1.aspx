<%@ Page Language="C#" AutoEventWireup="true" CodeFile="vdtWelcome1.aspx.cs" Inherits="VDT_Customer_Welcome" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Vehicle Delivery Tracking</title>
    <link href="../CSS/stylesheet.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../js/fleetvalidation.js"></script>

    <style type="text/css">
        .formInfo a, .formInfo a:active, formInfo a:visited
        {
            background-color: #FF0000;
            font-size: 1.3em;
            font-weight: bold;
            padding: 1px 2px;
            margin-left: 5px;
            color: #FFFFFF;
            text-decoration: none;
        }
        .formInfo a:hover
        {
            color: #660000;
            text-decoration: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div>
        <table width="1000" style="background-color: White" border="0" align="center" cellpadding="0"
            cellspacing="0" class="mainbdr">
            <tr>
                <td align="left" valign="top">
                    <div class="logo">
                        <img src="../images/Private_fleet_logo.jpg" alt="" width="298" height="113" /></div>
                    <div class="banner">
                        <img src="../images/Banner.jpg" alt="" width="700" height="113" /></div>
                </td>
            </tr>
            <tr>
                <td style="height: 53px">
                    <div class="topnavd">
                        <div style="float: left; margin: 8px 0 0 20px; font-weight: bold; color: White">
                            Welcome :
                            <asp:Label runat="server" ID="lblusername"></asp:Label></div>
                        <div style="float: right;">
                            <asp:ImageButton ID="imgbtnLogOut" runat="server" ImageUrl="~/Images/Logout.gif"
                                OnClick="imgbtnLogOut_Click" ValidationGroup="_logout" />
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <table width="960" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF"
                        class="mainbdr" style="height: 100%">
                        <tr>
                            <td align="left" valign="top">
                                <div class="maincontaint">
                                    <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#fafbfb"
                                        class="mainbdr">
                                        <tr>
                                            <td style="font-weight: bold; font-size: larger; color: #606060; font-size: 25px"
                                                align="center">
                                                <table width="100%">
                                                    <tr>
                                                        <td align="center">
                                                            Vehicle Delivery Tracker-
                                                            <asp:Label runat="server" ID="lblCustomerHeading"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 10px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-weight: bold; font-size: larger; color: #606060; font-size: 20px"
                                                            align="center">
                                                            <asp:Label runat="server" ID="lblVechicleName"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="2" valign="middle">
                                                <div align="center" style="vertical-align: middle; width: 100%; min-height: 400px">
                                                    <table width="100%" cellspacing="5" style="padding-left: 100px; padding-right: 100px;">
                                                        <tr>
                                                            <td colspan="4">
                                                                <asp:Label runat="server" ID="lblmsg"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4" align="right">
                                                                <asp:ImageButton runat="server" ID="ImageButton2" CausesValidation="true" onmouseout="this.src='../Images/Contact-Private-Fleet_hover.png'"
                                                                    onmouseover="this.src='../Images/Contact-Private-Fleet.gif'" ImageUrl="~/Images/Contact-Private-Fleet_hover.png"
                                                                    OnClick="btnhelp_Click" ToolTip="Need help with delivery - Contact Private Fleet" /><br />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 10px;">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" style="width: 10%; padding-left: 10px; font-weight: bold">
                                                                Dealer Name :
                                                            </td>
                                                            <td style="width: 14%;">
                                                                <asp:Label runat="server" ID="lblDealerName"></asp:Label>
                                                            </td>
                                                            <td align="left" style="width: 10%; padding-left: 10px; font-weight: bold">
                                                                Email :
                                                            </td>
                                                            <td>
                                                                <asp:HyperLink runat="server" ID="hypEmail"></asp:HyperLink>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" style="width: 10%; padding-left: 10px; font-weight: bold">
                                                                Company :
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="lblCompany"></asp:Label>
                                                            </td>
                                                            <td align="left" style="width: 10%; padding-left: 10px; font-weight: bold">
                                                                State :
                                                            </td>
                                                            <td align="left">
                                                                <asp:Label runat="server" ID="lblstate"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" style="width: 10%; padding-left: 10px; font-weight: bold">
                                                                Dealer Mobile :
                                                            </td>
                                                            <td align="left">
                                                                <asp:Label runat="server" ID="lblmobile"></asp:Label>
                                                            </td>
                                                            <td align="left" style="width: 10%; padding-left: 10px; font-weight: bold">
                                                                Dealer Phone :
                                                            </td>
                                                            <td align="left">
                                                                <asp:Label runat="server" ID="lblphone"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" style="width: 10%; padding-left: 10px; font-weight: bold" colspan="4">
                                                                Vehicle Commission Number(if applicable) <span class="formInfo"><a href="#" class="jTip"
                                                                    id="one" name="questionmark" title="The Commission Number is a number specific to a factory ordered car used to help track delivery">
                                                                    ?</a></span> -&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblVehicleComissionNo"
                                                                        Font-Bold="false"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4">
                                                                <asp:GridView runat="server" ID="grdDealerStatus" AutoGenerateColumns="false" Width="100%"
                                                                    RowStyle-Height="30px" AlternatingRowStyle-BackColor="#d5ecfd">
                                                                    <FooterStyle CssClass="gvFooterrow" />
                                                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                                    <PagerStyle CssClass="pgr" />
                                                                    <HeaderStyle BackColor="#0A73A2" Font-Bold="True" CssClass="gvHeader" Height="30px" />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Updated On" ItemStyle-Width="90px" HeaderStyle-Width="90px"
                                                                            ItemStyle-CssClass="grid_padding">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lbldate" Text='<%# bind("date","{0: dd MMM yyyy}") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Delivery Status" ItemStyle-Width="200px" HeaderStyle-Width="200px"
                                                                            ItemStyle-CssClass="grid_padding">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblStatus" Text='<%# bind("Status") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="ETA" ItemStyle-Width="90px" HeaderStyle-Width="90px"
                                                                            ItemStyle-CssClass="grid_padding">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblETA" Text='<%# bind("ETA","{0: dd MMM yyyy}") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Dealer Notes" ItemStyle-Width="360px" HeaderStyle-Width="400px"
                                                                            ItemStyle-CssClass="grid_padding">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblNotes" Text='<%# bind("DealerNotes") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" style="width: 10%; padding-left: 10px; font-weight: bold; font-size: x-small;
                                                                color: Black" colspan="4">
                                                                * ETA= Estimated Time of Arrival to Customer
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" colspan="4">
                                                                <p>
                                                                    The supplying dealer has committed to give delivery updates on an weekly basis.
                                                                    The latest update is recorded above however if you require an update between the
                                                                    scheduled updates, please either contact the dealer directly using the details above
                                                                    or click the button below. If you have any problems getting a response, please contact
                                                                    Private Fleet directly and we will follow up with the dealer.
                                                                </p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" style="width: 10%; padding-left: 10px;" colspan="4">
                                                                <b>Please Note</b>: All dates are approximate and may vary especially if they are
                                                                some time off.
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 7px">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4" align="right" style="width: 60%">
                                                                <asp:ImageButton runat="server" ID="btnSend" CausesValidation="true" onmouseout="this.src='../Images/Status-Update_hover.png'"
                                                                    onmouseover="this.src='../Images/Status-Update.png'" ImageUrl="~/Images/Status-Update_hover.png"
                                                                    OnClick="btnSend_Click" ToolTip="Send Request to Dealer for update its Status" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4" align="left">
                                                                <table>
                                                                    <tr>
                                                                        <td style="height: 3px">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <p>
                                                                                This system is currently being used on a trial basis. If you need any help or have
                                                                                any feedback, queries or bug reports please let David Lye know on <a href="mailto:davidlye@privatefleet.com.au">
                                                                                    davidlye@privatefleet.com.au</a> or (02) 9411 6777 ext 236. Thanks for your
                                                                                patience!</p>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server" ID="lbltarget"></asp:Label>
                    <asp:Label runat="server" ID="lblcancel"></asp:Label>
                    <ajaxToolkit:ModalPopupExtender runat="server" ID="modalSendMail" PopupControlID="pnlSend"
                        TargetControlID="lbltarget" CancelControlID="lblcancel" BackgroundCssClass="ModalCSS"
                        Enabled="false">
                    </ajaxToolkit:ModalPopupExtender>
                    <asp:Panel runat="server" ID="pnlSend" Visible="false" BackColor="White">
                        <table style="padding-left: 5px; padding-right: 5px; padding-top: 5px; padding-bottom: 5px">
                            <tr>
                                <td style="background-color: #0A73A2; color: White; height: 30px; font-weight: bold;
                                    padding-left: 5px" align="left">
                                    Message for Private Fleet
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox runat="server" ID="txtDesc" TextMode="MultiLine" Width="400px" Height="200px"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="requiredtxtDesc" ControlToValidate="txtDesc"
                                        ErrorMessage="Enter Message" Display="None" ValidationGroup="send"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="callouttxtDesc" TargetControlID="requiredtxtDesc">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <table>
                                        <tr>
                                            <td>
                                                <%--  <asp:Button runat="server" ID="btnTempSend" Text="Send" OnClick="btnTempSend_Click"  />--%>
                                                <asp:ImageButton ID="ImageButton1" runat="server" Text="Send" OnClick="btnTempSend_Click"
                                                    ImageUrl="~/Images/send_mail_hvr.gif" onmouseout="this.src='../Images/send_mail_hvr.gif'"
                                                    onmouseover="this.src='../Images/send_mail.gif'" ValidationGroup="send" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancels_Click"
                                                    ImageUrl="~/Images/Cancel.gif" onmouseout="this.src='../Images/Cancel.gif'" onmouseover="this.src='../Images/Cancel_hvr.gif'" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
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
                                                            OnClick="btnConfirm_Click" onmouseout="this.src='../Images/Confirm_hover.gif'"
                                                            onmouseover="this.src='../Images/Confirm.gif'" />
                                                    </td>
                                                    <td align="left">
                                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.gif" OnClick="btnEdit_Click"
                                                            onmouseout="this.src='../Images/edit.gif'" onmouseover="this.src='../Images/edit_hover.gif'" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </div>
    <div id="msgpop" runat="server" style="display: none; width: 400px;">
        <div id="progressBackgroundFilter1">
        </div>
        <div id="processMessage1" style="width: 400px; height: auto; padding: 5px !important;">
            <asp:Panel runat="server" ID="pnlmodal" BackColor="White">
                <table width="400px" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="background-color: #0A73A2; color: White; font-weight: bold; padding-left: 5px;
                            height: 30px; font-size: large">
                            Private Fleet
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px">
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 5px">
                            <p>
                                <asp:Label runat="server" ID="lblMessageForModal"></asp:Label>
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px">
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Button runat="server" ID="btnClose" Text="Ok" OnClick="btnClose_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px">
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </div>
    </form>
</body>
</html>
