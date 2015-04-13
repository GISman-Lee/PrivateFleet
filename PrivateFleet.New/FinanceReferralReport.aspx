<%@ Page Title="Finance Referral Report" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeFile="FinanceReferralReport.aspx.cs" Inherits="FinanceReferralReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlFinRefReport" runat="server">
                <table width="100%">
                    <tr>
                        <td colspan="2">
                            <span style="text-align: center;"><b>
                                <asp:Label ID="lblTitle" runat="server" Text="Finance Referral Details" ForeColor="White"></asp:Label></b>
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Consultant Name &nbsp;&nbsp;&nbsp; &nbsp;
                            <asp:TextBox ID="txtConsultantName" runat="server" Width="119px"></asp:TextBox>
                        </td>
                        <td align="left" style="padding-left: 15px;">
                            Surname&nbsp;&nbsp;&nbsp; &nbsp;
                            <asp:TextBox ID="txtSurname" runat="server" Width="119px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <span style="color: Red">*</span> From Date&nbsp;&nbsp;&nbsp; &nbsp;
                            <asp:TextBox ID="txtCalenderFrom" runat="server" Width="119px" MaxLength="10"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="calFrom" runat="server" TargetControlID="txtCalenderFrom"
                                Format="dd/MM/yyyy" PopupButtonID="txtCalenderFrom">
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
                        <td align="left" style="padding-left: 15px;">
                            <span style="color: Red">*</span> To Date&nbsp;&nbsp;&nbsp; &nbsp;
                            <asp:TextBox ID="TxtToDate" runat="server" Width="119px" MaxLength="10"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="calTO" runat="server" TargetControlID="TxtToDate"
                                Format="dd/MM/yyyy" PopupButtonID="TxtToDate">
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
                    <tr style="height: 5px;">
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" colspan="1">
                            <asp:ImageButton ID="btnGenerateReport" runat="server" ImageUrl="~/Images/Generate_report_hvr.gif"
                                onmouseout="this.src='Images/Generate_report_hvr.gif'" onmouseover="this.src='Images/Generate_report.gif'"
                                ValidationGroup="FinRefReport" OnClick="btnGenerateReport_Click" />
                        </td>
                        <td align="left" colspan="1" style="padding-left: 5px;">
                            <asp:ImageButton ID="btnCancel" runat="server" ImageUrl="~/Images/Cancel.gif" onmouseout="this.src='Images/Cancel.gif'"
                                onmouseover="this.src='Images/Cancel_hvr.gif'" ValidationGroup="VGMakeModelSeries"
                                CausesValidation="false" OnClick="btnCancel_Click" />
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
                        <td align="center" colspan="2">
                            <asp:GridView BorderColor="Gray" ID="gvFinanceReferralReport" runat="server" AllowPaging="True"
                                AutoGenerateColumns="False" AllowSorting="true" PageSize="15" Visible="true"
                                Width="100%" BackColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                                OnPageIndexChanging="gvFinanceReferralReport_PageIndexChanging" OnSorting="gvFinanceReferralReport_Sorting"
                                EmptyDataText="No Record Found">
                                <Columns>
                                    <asp:BoundField DataField="ConsultantName" HeaderStyle-Wrap="false" HeaderText="Consultant Name"
                                        SortExpression="ConsultantName" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%" />
                                    <asp:BoundField DataField="Surname" HeaderText="Surname" SortExpression="Surname"
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%" />
                                    <asp:BoundField DataField="Details" HeaderText="Finance Referral Details" SortExpression="Details"
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50%" />
                                    <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="10%" />
                                </Columns>
                                <FooterStyle CssClass="gvFooterrow" />
                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                <PagerStyle CssClass="pgr" />
                                <HeaderStyle BackColor="#0A73A2" Font-Bold="True" CssClass="gvHeader" Height="30px" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
