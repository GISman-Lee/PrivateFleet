<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ViewInCompleteRequest.aspx.cs" Inherits="ViewInCompleteRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="viewSentReq" runat="server">
        <table width="95%" align="center">
            <tr>
                <td align="right">
                    <span style="color: Red">*</span> From Date
                    <asp:TextBox ID="txtCalenderFrom" runat="server" Width="119px" MaxLength="10"></asp:TextBox>
                    <asp:Image ID="imgCal" runat="server" ImageUrl="~/Images/Calendar.gif" />
                    <ajaxToolkit:CalendarExtender ID="calFrom" runat="server" TargetControlID="txtCalenderFrom"
                        Format="dd/MM/yyyy" PopupButtonID="imgCal">
                    </ajaxToolkit:CalendarExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCalenderFrom"
                        CssClass="gvValidationError" Display="None" ErrorMessage="Please Enter the from Date"
                        SetFocusOnError="True" ValidationGroup="VGMakeModelSeries">
                    </asp:RequiredFieldValidator>
                    <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender2_ViewSentRequest"
                        TargetControlID="RequiredFieldValidator1" HighlightCssClass="validatorCalloutHighlight" />
                    <%--    validation for date format check  dd/mm/yy--%>
                    <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidatorDatecheck"
                        Display="None" ControlToValidate="txtCalenderFrom" ErrorMessage="Enter proper date in dd/mm/yyyy format."
                        ValidationGroup="VGMakeModelSeries" SetFocusOnError="False" ValidationExpression='^(((0?[1-9]|[12]\d|3[01])\/(0?[13578]|1[02])\/((1[6-9]|[2-9]\d)\d{2}))|((0?[1-9]|[12]\d|30)\/(0?[13456789]|1[012])\/((1[6-9]|[2-9]\d)\d{2}))|((0?[1-9]|1\d|2[0-8])\/0?2\/((1[6-9]|[2-9]\d)\d{2}))|(29\/0?2\/((1[6-9]|[2-9]\d)(0?[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$'></asp:RegularExpressionValidator>
                    <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender2"
                        TargetControlID="RegularExpressionValidatorDatecheck" HighlightCssClass="validatorCalloutHighlight" />
                </td>
                <td align="left">
                    <span style="color: Red">*</span> To Date
                    <asp:TextBox ID="TxtToDate" runat="server" Width="119px" MaxLength="10"></asp:TextBox>
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Calendar.gif" />
                    <ajaxToolkit:CalendarExtender ID="calTO" runat="server" TargetControlID="TxtToDate"
                        Format="dd/MM/yyyy" PopupButtonID="Image1">
                    </ajaxToolkit:CalendarExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtToDate"
                        CssClass="gvValidationError" Display="None" ErrorMessage="Please Enter the to Date"
                        SetFocusOnError="True" ValidationGroup="VGMakeModelSeries">
                    
                    </asp:RequiredFieldValidator><ajaxToolkit:ValidatorCalloutExtender runat="server"
                        ID="ValidatorCalloutExtender1" TargetControlID="RequiredFieldValidator2" HighlightCssClass="validatorCalloutHighlight" />
                    <%-- validation for date format check in dd/mm/yyyy--%>
                    <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1" Display="None"
                        ControlToValidate="TxtToDate" ErrorMessage="Enter proper date in dd/mm/yyyy format."
                        ValidationGroup="VGMakeModelSeries" SetFocusOnError="False" ValidationExpression='^(((0?[1-9]|[12]\d|3[01])\/(0?[13578]|1[02])\/((1[6-9]|[2-9]\d)\d{2}))|((0?[1-9]|[12]\d|30)\/(0?[13456789]|1[012])\/((1[6-9]|[2-9]\d)\d{2}))|((0?[1-9]|1\d|2[0-8])\/0?2\/((1[6-9]|[2-9]\d)\d{2}))|(29\/0?2\/((1[6-9]|[2-9]\d)(0?[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$'></asp:RegularExpressionValidator>
                    <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender3"
                        TargetControlID="RegularExpressionValidator1" HighlightCssClass="validatorCalloutHighlight" />
                    <%--validation for date comparision --%>
                    <asp:CustomValidator runat="server" ID="cust1" ClientValidationFunction="compaire_dates_ViewSentRequests"
                        ValidationGroup="VGMakeModelSeries" ErrorMessage="From date can not be greater than to date."
                        Display="None" ControlToValidate="txtCalenderFrom"> </asp:CustomValidator>
                    <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="validatorcallourdatecomparision"
                        TargetControlID="cust1" HighlightCssClass="validatorCalloutHighlight">
                    </ajaxToolkit:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr>
                <td align="right" colspan="1">
                    <asp:ImageButton ID="btnGenerateReport" runat="server" ImageUrl="~/Images/Generate_report_hvr.gif"
                        onmouseout="this.src='Images/Generate_report_hvr.gif'" onmouseover="this.src='Images/Generate_report.gif'"
                        ValidationGroup="VGMakeModelSeries" OnClick="btnGenerateReport_Click" />
                </td>
                <td align="left" colspan="1">
                    <asp:ImageButton ID="btnCancel" runat="server" ImageUrl="~/Images/Cancel.gif" onmouseout="this.src='Images/Cancel.gif'"
                        onmouseover="this.src='Images/Cancel_hvr.gif'" ValidationGroup="VGMakeModelSeries"
                        OnClick="btnCancel_Click" CausesValidation="false" />
                </td>
            </tr>
            <tr>
                <td style="height: 20px;" colspan="2">
                </td>
            </tr>
            <tr>
                <td align="right" colspan="2">
                    <asp:Label ID="lblRowsToDisplay" runat="server">Rows To Display</asp:Label>
                    <asp:DropDownList ID="ddl_NoRecords" runat="server" AutoPostBack="true" Width="50px"
                        OnSelectedIndexChanged="ddl_NoRecords_SelectedIndexChanged">
                        <asp:ListItem Value="10">10</asp:ListItem>
                        <asp:ListItem Value="20">20</asp:ListItem>
                        <asp:ListItem Value="30">30</asp:ListItem>
                        <asp:ListItem Value="50">50</asp:ListItem>
                        <asp:ListItem Value="All">All</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="gvRequests" runat="server" AutoGenerateColumns="False" CellPadding="3"
                        CellSpacing="1" Width="100%" BorderColor="#ACACAC" DataKeyNames="ID" EmptyDataText="No Requests Found"
                        OnRowCommand="gvRequests_RowCommand" OnRowDataBound="gvRequests_RowDataBound"
                        AllowSorting="True" OnSorting="gv_Sorting" PageSize="15" AllowPaging="True" OnPageIndexChanging="gvRequests_PageIndexChanging">
                        <Columns>
                            <asp:BoundField HeaderText="Make" DataField="Make" SortExpression="Make" />
                            <asp:BoundField HeaderText="Model" DataField="Model" SortExpression="Model" />
                            <asp:BoundField HeaderText="Series" DataField="Series" SortExpression="Series" />
                            <asp:BoundField HeaderText="Status" DataField="RequestStatus" SortExpression="Status" />
                            <asp:BoundField HeaderText="Request Date" DataField="RequestDate" SortExpression="RequestDate1" />
                            <%--    <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkCompare" runat="server" CommandName="CompareQuotations" Text="Compare Quotations"
                                        CommandArgument='<%# Eval("QuotationID") %>' CssClass="activeLink"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <%-- <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkShortListed" runat="server" CommandName="ViewShortlistedQuote"
                                        Text="ShortListed Quotation" CommandArgument='<%# Eval("QuotationID") %>' Enabled="false"
                                        CssClass=""></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDetails" runat="server" CommandName="ViewDetails" Text="View Details"
                                        CommandArgument='<%# Eval("QuotationID") %>' CssClass="activeLink"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <%--added on 30 Jun 2012--%>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkComplete" runat="server" CommandName="Complete" Text="Complete"
                                        CssClass="activeLink"></asp:LinkButton>
                                    <asp:HiddenField ID="hdfQRStatus" runat="server" Value='<%#Eval("IsQRCancel") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <%--added on 6 sept 2012--%>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkCancelQR" runat="server" CommandName="CancelQR" Text="Cancel Quotation"
                                        CssClass="activeLink"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDeal" runat="server" CommandName="DealDone" Text="Deal Done" CssClass="activeLink"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        </Columns>
                        <FooterStyle CssClass="gvFooterrow" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <PagerStyle CssClass="pgr" />
                        <HeaderStyle BackColor="#0A73A2" Font-Bold="True" CssClass="gvHeader" Height="25px" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <%-- On 6 sept 2012 for cancel requst --%>
    <div id="msgpop" runat="server" style="display: none; width: 300px;">
        <div id="progressBackgroundFilter1">
        </div>
        <div id="processMessage1" style="width: 300px; height: auto; padding: 5px !important;">
            <asp:Panel runat="server" ID="pnlmodal" BackColor="White">
                <table width="300px" cellpadding="0" cellspacing="0">
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
                        <td style="padding-left: 5px" align="center">
                            <p>
                                <asp:Label runat="server" ID="lblMessageForModal" Text="Are you want to Cancel this Quote Request?"></asp:Label>
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:ImageButton ID="imgbtnQRCancelYes" Style="float: left; margin-left: 25%;" runat="server"
                                ImageUrl="~/Images/yes_d.png" onmouseout="this.src='Images/yes_d.png'" onmouseover="this.src='Images/yes_u.png'"
                                AlternateText="Yes" OnClick="imgbtnQRCancelYes_Click" />
                            <asp:ImageButton ID="imgbtnQRCancelNo" Style="float: left; margin-left: 5px;" runat="server"
                                ImageUrl="~/Images/no_d.png" ImageAlign="AbsMiddle" onmouseout="this.src='Images/no_d.png'"
                                onmouseover="this.src='Images/no_u.png'" AlternateText="No" OnClick="imgbtnQRCancelNo_Click" />
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
</asp:Content>
