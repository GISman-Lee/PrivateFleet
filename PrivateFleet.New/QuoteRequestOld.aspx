<%@ Page Title="Create Quote Request" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="QuoteRequestOld.aspx.cs" Inherits="QuoteRequestOld" %>

<%@ Register Src="User Controls/Request/ucRequestParameters.ascx" TagName="ucRequestParameters"
    TagPrefix="uc5" %>
<%@ Register Src="~/User Controls/Request/ucDealerSelection.ascx" TagName="ucDealerSelection"
    TagPrefix="uc4" %>
<%@ Register Src="User Controls/Request/ucRequestHeader.ascx" TagName="ucRequestHeader"
    TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function isNumberKey(evt, chk) {
            //alert("Hi");
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                alert("Please enter number only");
                return false;
            }
            return true;
        }

        function maxLength(evt, txt) {
            var charCode = (evt.which) ? evt.which : event.keyCode

            if (charCode == 8 || charCode == 27)
                return true

            var txt = document.getElementById(txt.id);
            if (txt.value.length > 999) {
                alert("Maximum Limit reached");
                return false;
            }
        }
    </script>

    <asp:UpdatePanel ID="UPQuoteRequest" runat="server">
        <ContentTemplate>
            <asp:Panel ID="QuoteRequest_1" runat="server">
                <asp:MultiView ID="mvQuoteRequest" runat="server" ActiveViewIndex="0">
                    <asp:View ID="viewCreateRequest" runat="server">
                        <asp:Panel ID="PanView1" runat="server">
                            <table width="98%" align="center" border="0" cellpadding="0" cellspacing="1">
                                <tr>
                                    <td valign="middle">
                                        <asp:Label ID="lblMsgOp" runat="server" CssClass="dbresult"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%;">
                                        <table width="100%" align="center" cellpadding="1" cellspacing="3" border="0">
                                            <tr>
                                                <td>
                                                    Make:
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlMake" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlMake_SelectedIndexChanged"
                                                        Height="21px" Width="120px">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvMake" runat="server" ControlToValidate="ddlMake"
                                                        ErrorMessage="Make Required" ValidationGroup="VGMakeModelSeries" Display="None"
                                                        InitialValue="0" SetFocusOnError="True">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    Model:
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlModel" runat="server" OnSelectedIndexChanged="ddlModel_SelectedIndexChanged"
                                                        Height="21px" Width="200px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    Series:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtSeries" runat="server" Width="200px" Height="19px">
                                                    </asp:TextBox>
                                                    <asp:DropDownList ID="ddlSeries" Visible="false" runat="server" AutoPostBack="false"
                                                        Width="190px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <%-- <tr>
                                                <td>
                                                    Series:
                                                </td>
                                                <td colspan="5">
                                                    <asp:TextBox ID="txtSeries" runat="server" Width="246px" Height="19px">
                                                    </asp:TextBox>
                                                </td>
                                            </tr>--%>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%;">
                                        <uc5:ucRequestParameters ID="UcRequestParameters1" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table width="100%" cellpadding="1" cellspacing="1" border="0">
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="chkOrderTaken" runat="server" Text="<b style='color:red; text-decoration:blink'> Order Taken</b>">
                                                    </asp:CheckBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="chkUrgent" runat="server" Text=" Some flexibility depending on delivery.Urgently required">
                                                    </asp:CheckBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="chkBuid" runat="server" Text=" Must be 2011 Build & Complied">
                                                    </asp:CheckBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table width="100%" cellpadding="1" cellspacing="1" border="0">
                                            <tr>
                                                <td>
                                                    Consultant Notes:
                                                    <asp:TextBox ID="TextBox1" runat="server" Rows="3" TextMode="MultiLine" Width="605px">
                                                    </asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Display="None"
                                                        ControlToValidate="TextBox1" ValidationGroup="VGMakeModelSeries" SetFocusOnError="True"
                                                        ssClass="gvValidationError" ValidationExpression="^([\S\s]{0,1000})$" ErrorMessage="Maximum Limit reached">
                                                    </asp:RegularExpressionValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender6"
                                                        TargetControlID="RegularExpressionValidator2" HighlightCssClass="validatorCalloutHighlight" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 10px">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server"
                                            HighlightCssClass="validatorCalloutHighlight" TargetControlID="rfvMake">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                        &nbsp;&nbsp;
                                        <asp:ImageButton ID="btnNextStep" runat="server" Text="NEXT" ImageUrl="~/Images/Next.gif"
                                            ImageAlign="AbsMiddle" onmouseout="this.src='Images/Next.gif'" onmouseover="this.src='Images/Next_hvr.gif'"
                                            OnClick="btnNextStep_Click" ValidationGroup="VGMakeModelSeries" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 10px">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </asp:View>
                    <asp:View ID="viewDealerSelection" runat="server">
                        <table width="95%" align="center" style="border: 1px solid #4A4A4A" cellpadding="1"
                            cellspacing="2">
                            <tr>
                                <td valign="middle">
                                    <asp:Label ID="lblMsg" runat="server" CssClass="dbresult"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" class="subheading">
                                    <strong>Search Dealer</strong>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 5px;">
                                    Postal Code:
                                </td>
                                <td style="height: 5px">
                                    <asp:TextBox ID="txtPCode" MaxLength="6" runat="server" onblur="javascript: __doPostBack();"
                                        OnTextChanged="txtPCode_TextChanged">
                                    </asp:TextBox>
                                    <%--    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Display="None"
                                        ControlToValidate="txtPCode" ValidationGroup="VGSubmit" SetFocusOnError="True"
                                        ssClass="gvValidationError" ValidationExpression="^[0-9]*$" ErrorMessage="<p>Enter Numbers Only</p>">
                                    </asp:RegularExpressionValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender6"
                                        TargetControlID="RegularExpressionValidator2" HighlightCssClass="validatorCalloutHighlight" />--%>
                                </td>
                                <td style="height: 5px">
                                    Suburb
                                </td>
                                <td style="height: 5px" valign="bottom">
                                    <asp:DropDownList ID="ddlLocation" runat="server" DataTextField="Suburb" DataValueField="ID"
                                        Width="249px">
                                        <asp:ListItem Value="0">-Select Suburb-</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 5px">
                                </td>
                                <td style="height: 5px">
                                    <%--  <asp:LinkButton ID="lbtGetLocations" runat="server" OnClick="lbtGetLocations_Click"
                                        ValidationGroup="VGRadius">Get Location's</asp:LinkButton>--%>
                                </td>
                                <td style="height: 5px" valign="bottom">
                                </td>
                                <td style="height: 5px" valign="bottom">
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" colspan="4" style="text-align: right; padding-right: 20px;">
                                    <asp:ImageButton ID="btnSearchDealers" runat="server" OnClick="btnSearchDealers_Click"
                                        ImageUrl="~/Images/Search_dealer.gif" onmouseout="this.src='Images/Search_dealer.gif'"
                                        onmouseover="this.src='Images/Search_dealer_hvr.gif'" ValidationGroup="VGRadius" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    &nbsp;
                                    <asp:RequiredFieldValidator ID="rfvPostalCodeOnSearch" runat="server" ControlToValidate="txtPCode"
                                        CssClass="gvValidationError" Display="None" ErrorMessage="Postal Code  Required"
                                        SetFocusOnError="True" ValidationGroup="VGRadius">
                                    </asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
                                        HighlightCssClass="validatorCalloutHighlight" TargetControlID="rfvPostalCodeOnSearch">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlLocation"
                                        CssClass="gvValidationError" Display="None" ErrorMessage="Suburb Required" SetFocusOnError="True"
                                        InitialValue="0" ValidationGroup="VGRadius">
                                    </asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                        HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator1">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </td>
                                <td valign="top" align="center">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                        <table width="95%" align="center" cellpadding="0" cellspacing="0">
                            <tr>
                                <td valign="top" colspan="4">
                                    <uc4:ucDealerSelection ID="UcDealerSelection1" runat="server" Visible="True" />
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 10px">
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="top" width="40%" style="height: 20px">
                                    <asp:ImageButton ID="btnPrevious" runat="server" OnClick="btnPrevious_Click" ImageUrl="~/Images/back.gif"
                                        onmouseout="this.src='Images/back.gif'" onmouseover="this.src='Images/back_hvr.gif'" />
                                </td>
                                <td align="left" valign="top" width="60%" style="height: 20px">
                                    <asp:ImageButton ID="btnCreateRequest" runat="server" OnClick="btnCreateRequest_Click"
                                        ImageUrl="~/Images/Create_request.gif" onmouseout="this.src='Images/Create_request.gif'"
                                        onmouseover="this.src='Images/Create_request_hvr.gif'" ValidationGroup="Create" />
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 10px">
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                    <asp:View ID="view1" runat="server">
                        <table width="100%">
                            <tr align="center">
                                <td colspan="2" style="padding: 10px; font-size: 15px; text-decoration: underline;
                                    font-weight: bolder">
                                    <asp:Label ID="lblLastMsg" runat="server" Text="Quote Request"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" width="100%" style="padding-top: 0px">
                                                <asp:DataList ID="DataList1" runat="server" RepeatDirection="Horizontal" GridLines="Both"
                                                    Width="100%" BorderStyle="solid" BorderColor="#acacac" BorderWidth="1px">
                                                    <ItemTemplate>
                                                        <table width="100%">
                                                            <tr class="ucHeader">
                                                                <td align="center">
                                                                    <asp:Label ID="lblHeader" runat="server" CssClass="gvLabel" Text='<%# Bind("Header") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="font-size: 20px; font-weight: bold;" align="center">
                                                                    <asp:Label ID="lblDetails" runat="server" CssClass="gvLabel" Text='<%# Bind("Details") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                            </td>
                                        </tr>
                                        <tr class="ucHeader" style="width: 100%; border: solid 1px #acacac;">
                                            <td align="left">
                                                <asp:Label ID="lblHeader" runat="server" CssClass="gvLabel" Text="">Request Parameter</asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" width="100%">
                                                <asp:DataList ID="DataList2" runat="server" RepeatDirection="Horizontal" GridLines="Both"
                                                    BorderStyle="solid" BorderColor="#acacac" BorderWidth="1px" Width="100%">
                                                    <ItemTemplate>
                                                        <table width="100%">
                                                            <tr>
                                                                <td align="left" style="width: 50%; background-color: #eaeaea;">
                                                                    <asp:Label ID="lblHeader1" runat="server" CssClass="gvLabel" Text='<%# Bind("Header") %>'></asp:Label>
                                                                </td>
                                                                <td style="padding-left: 10px" align="left">
                                                                    <asp:Label ID="Label1" runat="server" CssClass="gvLabel" Text='<%# Bind("Details") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                            </td>
                                        </tr>
                                        <tr id="trAddAcc" runat="server" visible="false" class="ucHeader" style="width: 100%;
                                            padding: 10px; border: solid 1px #acacac;">
                                            <td align="left">
                                                <asp:Label ID="Label3" runat="server" CssClass="gvLabel" Text="">Additional Accessories</asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" width="100%">
                                                <asp:DataList ID="DataList3" runat="server" RepeatDirection="Horizontal" GridLines="Both"
                                                    BorderStyle="solid" BorderColor="#acacac" BorderWidth="1px" Width="100%">
                                                    <ItemTemplate>
                                                        <table width="100%">
                                                            <tr>
                                                                <td align="left" style="width: 50%; background-color: #eaeaea; font-weight: bold;">
                                                                    <asp:Label ID="lblHeader2" runat="server" CssClass="gvLabel" Text='<%# Bind("Header") %>'></asp:Label>
                                                                </td>
                                                                <td style="padding-left: 10px" align="left">
                                                                    <asp:Label ID="Label2" runat="server" CssClass="gvLabel" Text='<%# Bind("Details") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="trConsNotes" runat="Server" visible="false" align="left">
                                <td colspan="2">
                                    <table style="border: solid 1px #acacac;">
                                        <tr>
                                            <td valign="top">
                                                <asp:Label ID="Label4" runat="server" Width="120px" Font-Bold="true" Text="Consultant Notes :"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label5" Width="680px" runat="server">
                                                    <asp:Literal ID="lit" runat="server">
                                                    </asp:Literal></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr align="left">
                                <td colspan="2" style="padding-top: 25px; font-size: 15px; font-weight: bolder">
                                    <asp:Label ID="Label2" runat="server" Text="You Created the Quote Request for the Dealers - "></asp:Label>
                                </td>
                            </tr>
                            <tr align="center">
                                <td colspan="2" style="padding: 10px">
                                    <asp:GridView ID="gvSelectedDealers1" runat="server" DataKeyNames="ID" Width="100%"
                                        AllowPaging="true" PageSize="10" AutoGenerateColumns="false" BorderColor="#acacac"
                                        CellPadding="1" CellSpacing="3" EmptyDataText="No records found">
                                        <Columns>
                                            <asp:BoundField DataField="Name" HeaderText="Dealer Name" />
                                            <asp:BoundField DataField="Email" HeaderText="Email" />
                                            <asp:BoundField DataField="Phone" HeaderText="Phone Number" />
                                            <asp:BoundField DataField="Fax" HeaderText="Fax" Visible="False" />
                                            <%-- <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkRemove" runat="server" Text="Remove Dealer" CommandName="RemoveDealer"
                                                        CssClass="activeLink">
                                                    </asp:LinkButton>
                                                    <asp:HiddenField ID="hdfID" runat="server" Value='<%# Bind("ID") %>' />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>--%>
                                        </Columns>
                                        <RowStyle Width="22px" />
                                        <HeaderStyle BackColor="#0A73A2" Font-Bold="True" CssClass="gvHeader" Width="25px" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr align="left">
                                <td colspan="2" style="padding: 10px; font-size: 15px; font-weight: bolder">
                                    <asp:Label ID="lblMasg1" runat="server" Text="Do you want to create it?"></asp:Label>
                                </td>
                            </tr>
                            <tr id="trQRSend" align="left" runat="server" visible="false">
                                <td colspan="2" style="padding: 10px; font-size: 15px; font-weight: bolder">
                                    <asp:Label ID="lblQRSend" runat="server" ForeColor="Red" Text="QR Details Send to Customer"></asp:Label>
                                </td>
                            </tr>
                            <tr style="padding: 10px">
                                <td align="left" valign="top" width="40%" style="height: 20px">
                                    <asp:ImageButton ID="imgbutPre" runat="server" ImageUrl="~/Images/back.gif" onmouseout="this.src='Images/back.gif'"
                                        onmouseover="this.src='Images/back_hvr.gif'" OnClick="imgbutPre_Click" />
                                </td>
                                <td align="left" valign="top" width="60%" style="height: 20px">
                                    <asp:ImageButton ID="imgbutCreate" runat="server" ImageUrl="~/Images/Create_request.gif"
                                        onmouseout="this.src='Images/Create_request.gif'" onmouseover="this.src='Images/Create_request_hvr.gif'"
                                        ValidationGroup="Create" OnClick="imgbutCreate_Click" />
                                    <asp:ImageButton ID="imgbtnAddDealer" runat="server" ImageUrl="~/Images/Create_request.gif"
                                        onmouseout="this.src='Images/Create_request.gif'" onmouseover="this.src='Images/Create_request_hvr.gif'"
                                        ValidationGroup="Create" Visible="false" OnClick="imgbtnAddDealer_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                </asp:MultiView>
            </asp:Panel>
            <div id="divOrderCancelConfirm" runat="server" style="display: none; width: 400px;">
                <div id="progressBackgroundFilterOrderConfrm">
                </div>
                <div id="processMessageOrderConfirm" style="width: 400px; height: auto; padding: 5px !important;
                    left: 35%;">
                    <asp:Panel runat="server" ID="Panel1" BackColor="White">
                        <table width="400px" cellpadding="0" cellspacing="0">
                            <tr>
                                <td colspan="2" style="background-color: #0A73A2; color: White; font-weight: bold;
                                    padding-left: 5px; height: 30px; font-size: large">
                                    Private Fleet
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="height: 35px" align="center">
                                    <span style="font-size: 14px; font-weight: bold;">Send Quote Request Templete to Customer.</span>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-left: 5px" align="left">
                                    <asp:Label runat="server" ID="lblCustomerEmail" Text="Customer Email :"></asp:Label>
                                </td>
                                <td style="padding-left: 5px" align="left">
                                    <asp:TextBox ID="txtCustomerEmail" Width="300px" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfv_CustEmail" runat="server" ControlToValidate="txtCustomerEmail"
                                        CssClass="gvValidationError" Display="None" ErrorMessage="Email  Required" SetFocusOnError="True"
                                        ValidationGroup="CustEmail">
                                    </asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="vce_CustEmail" runat="server" HighlightCssClass="validatorCalloutHighlight"
                                        TargetControlID="rfv_CustEmail">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <asp:RegularExpressionValidator ID="rev_CustEmail" runat="server" ErrorMessage="Invalid EmailId"
                                        Display="None" SetFocusOnError="true" ControlToValidate="txtCustomerEmail" ValidationGroup="CustEmail"
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="vce_CustEmail1" runat="server" TargetControlID="rev_CustEmail"
                                        PopupPosition="Right">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="height: 15px">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <asp:ImageButton ID="imgbtnSendCustomerMail" runat="server" ImageUrl="~/Images/send_QR_hr.gif"
                                        onmouseout="this.src='Images/send_QR_hr.gif'" onmouseover="this.src='Images/send_QR.gif'"
                                        ValidationGroup="CustEmail" OnClick="imgbtnSendCustomerMail_Click" />
                                    <asp:ImageButton ID="imgbtnCancel" runat="server" ImageUrl="~/Images/continue_without_Sending_u.gif"
                                        onmouseout="this.src='Images/continue_without_Sending_u.gif'" onmouseover="this.src='Images/continue_without_Sending_d.gif'"
                                        OnClick="imgbtnCancel_Click" CausesValidation="false" />
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 15px">
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
