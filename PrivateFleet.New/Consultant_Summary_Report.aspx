<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Consultant_Summary_Report.aspx.cs" Title="Consultant Summary Report" Inherits="Consultant_Summary_Report" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlConsultantSR" runat="server" DefaultButton="btnGenerateReport">
                <div style="width: 100%;" align="center">
                    <table cellpadding="3" cellspacing="3" style="border: solid 1px White;" width="95%">
                        <tr align="left">
                            <td>
                                <strong>Search Criteria:</strong>
                            </td>
                            <td colspan="2">
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" align="center">
                                <table>
                                    <tr id="trConsultant" runat="server">
                                        <td>
                                            <asp:Label ID="lblConsultant" runat="server" Text="Consultant"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlConsultantLst" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblFromDate" runat="server" Text="From Date"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCalenderFrom" runat="server" Width="119px" MaxLength="10"></asp:TextBox>
                                            <asp:Image ID="imgCal" runat="server" ImageUrl="~/Images/Calendar.gif" />
                                            <ajaxToolkit:CalendarExtender ID="calFrom" runat="server" TargetControlID="txtCalenderFrom"
                                                PopupButtonID="imgCal" Format="dd/MM/yyyy">
                                            </ajaxToolkit:CalendarExtender>
                                            <%--    validation for date format check  dd/mm/yy--%>
                                            <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidatorDatecheck"
                                                Display="None" ControlToValidate="txtCalenderFrom" ErrorMessage="Enter proper date in dd/mm/yyyy format."
                                                ValidationGroup="VGMakeModelSeries" SetFocusOnError="False" ValidationExpression='^(((0?[1-9]|[12]\d|3[01])\/(0?[13578]|1[02])\/((1[6-9]|[2-9]\d)\d{2}))|((0?[1-9]|[12]\d|30)\/(0?[13456789]|1[012])\/((1[6-9]|[2-9]\d)\d{2}))|((0?[1-9]|1\d|2[0-8])\/0?2\/((1[6-9]|[2-9]\d)\d{2}))|(29\/0?2\/((1[6-9]|[2-9]\d)(0?[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$'></asp:RegularExpressionValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender2"
                                                TargetControlID="RegularExpressionValidatorDatecheck" HighlightCssClass="validatorCalloutHighlight" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblToDate" runat="server" Text="To Date"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCalenderToDate" runat="server" Width="119px" MaxLength="10"></asp:TextBox>
                                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Calendar.gif" />
                                            <ajaxToolkit:CalendarExtender ID="calTO" runat="server" TargetControlID="txtCalenderToDate"
                                                PopupButtonID="Image1" Format="dd/MM/yyyy">
                                            </ajaxToolkit:CalendarExtender>
                                            <%-- validation for date format check in dd/mm/yyyy--%>
                                            <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1" Display="None"
                                                ControlToValidate="txtCalenderToDate" ErrorMessage="Enter proper date in dd/mm/yyyy format."
                                                ValidationGroup="VGMakeModelSeries" SetFocusOnError="False" ValidationExpression='^(((0?[1-9]|[12]\d|3[01])\/(0?[13578]|1[02])\/((1[6-9]|[2-9]\d)\d{2}))|((0?[1-9]|[12]\d|30)\/(0?[13456789]|1[012])\/((1[6-9]|[2-9]\d)\d{2}))|((0?[1-9]|1\d|2[0-8])\/0?2\/((1[6-9]|[2-9]\d)\d{2}))|(29\/0?2\/((1[6-9]|[2-9]\d)(0?[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$'></asp:RegularExpressionValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender3"
                                                TargetControlID="RegularExpressionValidator1" HighlightCssClass="validatorCalloutHighlight" />
                                            <%--validation for date comparision --%>
                                            <asp:CustomValidator runat="server" ID="cust1" ClientValidationFunction="compaire_dates_Consultant_Summary_Report"
                                                ValidationGroup="VGMakeModelSeries" ErrorMessage="From date can not be greater than to date."
                                                Display="None" ControlToValidate="txtCalenderFrom"> </asp:CustomValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="validatorcallourdatecomparision"
                                                TargetControlID="cust1" HighlightCssClass="validatorCalloutHighlight">
                                            </ajaxToolkit:ValidatorCalloutExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td align="left">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="btnGenerateReport" runat="server" ImageUrl="~/Images/Generate_report_hvr.gif"
                                                            onmouseout="this.src='Images/Generate_report_hvr.gif'" onmouseover="this.src='Images/Generate_report.gif'"
                                                            ValidationGroup="VGMakeModelSeries" OnClick="btnGenerateReport_Click" />
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="btnCancel" runat="server" ImageUrl="~/Images/Cancel.gif" onmouseout="this.src='Images/Cancel.gif'"
                                                            onmouseover="this.src='Images/Cancel_hvr.gif'" ValidationGroup="VGMakeModelSeries"
                                                            OnClick="btnCancel_Click" CausesValidation="false" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr align="right">
                            <td>
                            </td>
                            <td>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblTotal" runat="server"></asp:Label>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblRowsToDisplay2" runat="server">Rows To Display:</asp:Label>
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
                            <td colspan="4">
                                <asp:GridView ID="gvAllConsultant" runat="server" AllowPaging="True" OnPageIndexChanging="gvAllConsultant_PageIndexChanging"
                                    AutoGenerateColumns="False" AllowSorting="true" PageSize="15" Visible="true"
                                    Width="100%" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                    CellPadding="3" OnSorting="gvAllConsultant_Sorting" 
                                    EmptyDataText="No Records Found" onrowdatabound="gvAllConsultant_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="Consultant" HeaderText="Consultant Name" SortExpression="Consultant"
                                            ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="QuotesSent" HeaderText="Quote Requests Sent " SortExpression="QuotesSent"
                                            ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="QuotationReceived" HeaderText="Quote Requests Received "
                                            SortExpression="QuotationReceived" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="WinningQuotes" HeaderText="Winning quotes selected" SortExpression="WinningQuotes"
                                            ItemStyle-HorizontalAlign="Center" Visible="true">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Finance Referral" SortExpression="FinanceReferral"
                                            ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkFR" runat="server" Text="<%# Bind('FinanceReferral') %>" OnClick="lnkFR_Click"
                                                    CommandArgument="<%# Bind('ID')%>"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="gvFooterrow" />
                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle CssClass="pgr" />
                                    <HeaderStyle BackColor="#0A73A2" Font-Bold="True" CssClass="gvHeader" Height="30px" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                    <asp:HiddenField ID="hdFinanceReferral" runat="server" />
                </div>
                <div id="divFinRef" runat="server" visible="false">
                    <div id="progressBackgroundFilter">
                    </div>
                    <div id="processMessage1" style="width: 60% !important;">
                        <table>
                            <tr>
                                <td style="background-color: #BEE6FF; padding: 5px;" colspan="2">
                                    <%--<span style="text-align: center;"><b>
                                    <asp:Label ID="lblTitle" runat="server" Text="Finance Referral Report" ForeColor="White"></asp:Label></b></span>--%>
                                    <span style="float: right; margin-top: -1px; margin-right: 8px; z-index: 2;">
                                        <asp:ImageButton ID="btnPopClose" runat="server" ImageUrl="~/Images/cancel.png" OnClick="btnPopClose_Click" />
                                    </span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="gvFinanceReferral" runat="server" AllowPaging="True" OnPageIndexChanging="gvFinanceReferral_PageIndexChanging"
                                        AutoGenerateColumns="False" AllowSorting="true" PageSize="15" Visible="true"
                                        Width="100%" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                        CellPadding="3" OnSorting="gvFinanceReferral_Sorting">
                                        <Columns>
                                            <asp:BoundField DataField="Name" HeaderText="User Name" SortExpression="Name" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Details" HeaderText="Finance Referral Details" SortExpression="Details"
                                                ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="true" />
                                            <asp:BoundField DataField="EmailTo" HeaderText="Email Sent To " SortExpression="EmailTo"
                                                ItemStyle-HorizontalAlign="Center" />
                                        </Columns>
                                        <FooterStyle CssClass="gvFooterrow" />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle CssClass="pgr" />
                                        <HeaderStyle BackColor="#0A73A2" Font-Bold="True" HorizontalAlign="Center" CssClass="gvHeader"
                                            Height="30px" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
