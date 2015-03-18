<%@ Page Title="Leads Report" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="DiscountLeadsReport.aspx.cs" Inherits="DiscountLeadsReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pnlLeadRpt" DefaultButton="btnGenerateReport" runat="server">
        <table width="100%" cellpadding="2" cellspacing="2">
            <tr>
                <td style="width: 60px">
                    <asp:Label ID="lblLeadType" runat="server" Text="Lead Type"></asp:Label>
                </td>
                <td style="width: 60px">
                    <asp:DropDownList ID="ddlLeadType" AutoPostBack="true" Width="119" runat="server"
                        OnSelectedIndexChanged="ddlLeadType_SelectedIndexChanged">
                        <asp:ListItem Selected="True" Text="--Select--" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Leads" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Error Leads" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfv_LeadType" runat="server" ControlToValidate="ddlLeadType"
                        CssClass="gvValidationError" Display="None" ErrorMessage="Please Select" SetFocusOnError="True"
                        ValidationGroup="LeadRpt" InitialValue="0">
                    </asp:RequiredFieldValidator>
                    <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="vce_LeadType" TargetControlID="rfv_LeadType"
                        HighlightCssClass="validatorCalloutHighlight" />
                </td>
                <td style="width: 60px">
                </td>
                <td style="width: 60px">
                </td>
            </tr>
            <tr id="trCompany" runat="server" visible="false">
                <td>
                    <asp:Label ID="lblCompany" runat="server" Text="Company"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlCompany" Width="119" runat="server">
                        <asp:ListItem Selected="True" Text="--Select--" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Carmony" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Privatefleet" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfv_Company" runat="server" ControlToValidate="ddlCompany"
                        CssClass="gvValidationError" Display="None" ErrorMessage="Please Select" SetFocusOnError="True"
                        ValidationGroup="LeadRpt" InitialValue="0">
                    </asp:RequiredFieldValidator>
                    <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="vce_Company" TargetControlID="rfv_Company"
                        HighlightCssClass="validatorCalloutHighlight" />
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <span style="color: Red">*</span> From Date
                </td>
                <td>
                    <asp:TextBox ID="txtCalenderFrom" runat="server" Width="119px" MaxLength="10"></asp:TextBox>
                    <asp:Image ID="imgCal" runat="server" ImageUrl="~/Images/Calendar.gif" />
                    <ajaxToolkit:CalendarExtender ID="calFrom" runat="server" TargetControlID="txtCalenderFrom"
                        Format="dd/MM/yyyy" PopupButtonID="imgCal">
                    </ajaxToolkit:CalendarExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCalenderFrom"
                        CssClass="gvValidationError" Display="None" ErrorMessage="Please Enter the from Date"
                        SetFocusOnError="True" ValidationGroup="LeadRpt">
                    </asp:RequiredFieldValidator>
                    <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender2_ViewSentRequest"
                        TargetControlID="RequiredFieldValidator1" HighlightCssClass="validatorCalloutHighlight" />
                    <%--    validation for date format check  dd/mm/yy--%>
                    <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidatorDatecheck"
                        Display="None" ControlToValidate="txtCalenderFrom" ErrorMessage="Enter proper date in dd/mm/yyyy format."
                        ValidationGroup="LeadRpt" SetFocusOnError="False" ValidationExpression='^(((0?[1-9]|[12]\d|3[01])\/(0?[13578]|1[02])\/((1[6-9]|[2-9]\d)\d{2}))|((0?[1-9]|[12]\d|30)\/(0?[13456789]|1[012])\/((1[6-9]|[2-9]\d)\d{2}))|((0?[1-9]|1\d|2[0-8])\/0?2\/((1[6-9]|[2-9]\d)\d{2}))|(29\/0?2\/((1[6-9]|[2-9]\d)(0?[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$'></asp:RegularExpressionValidator>
                    <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender2"
                        TargetControlID="RegularExpressionValidatorDatecheck" HighlightCssClass="validatorCalloutHighlight" />
                </td>
                <td>
                    <span style="color: Red">*</span> To Date
                </td>
                <td>
                    <asp:TextBox ID="TxtToDate" runat="server" Width="119px" MaxLength="10"></asp:TextBox>
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Calendar.gif" />
                    <ajaxToolkit:CalendarExtender ID="calTO" runat="server" TargetControlID="TxtToDate"
                        Format="dd/MM/yyyy" PopupButtonID="Image1">
                    </ajaxToolkit:CalendarExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtToDate"
                        CssClass="gvValidationError" Display="None" ErrorMessage="Please Enter the to Date"
                        SetFocusOnError="True" ValidationGroup="LeadRpt">
                    </asp:RequiredFieldValidator>
                    <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender1"
                        TargetControlID="RequiredFieldValidator2" PopupPosition="Left" HighlightCssClass="validatorCalloutHighlight" />
                    <%-- validation for date format check in dd/mm/yyyy--%>
                    <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1" Display="None"
                        ControlToValidate="TxtToDate" ErrorMessage="Enter proper date in dd/mm/yyyy format."
                        ValidationGroup="LeadRpt" SetFocusOnError="False" ValidationExpression='^(((0?[1-9]|[12]\d|3[01])\/(0?[13578]|1[02])\/((1[6-9]|[2-9]\d)\d{2}))|((0?[1-9]|[12]\d|30)\/(0?[13456789]|1[012])\/((1[6-9]|[2-9]\d)\d{2}))|((0?[1-9]|1\d|2[0-8])\/0?2\/((1[6-9]|[2-9]\d)\d{2}))|(29\/0?2\/((1[6-9]|[2-9]\d)(0?[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$'></asp:RegularExpressionValidator>
                    <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender3"
                        TargetControlID="RegularExpressionValidator1" PopupPosition="Left" HighlightCssClass="validatorCalloutHighlight" />
                    <%--validation for date comparision --%>
                    <asp:CustomValidator runat="server" ID="cust1" ClientValidationFunction="compaire_dates_ViewSentRequests"
                        ValidationGroup="LeadRpt" ErrorMessage="From date can not be greater than to date."
                        Display="None" ControlToValidate="txtCalenderFrom"> </asp:CustomValidator>
                    <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="validatorcallourdatecomparision"
                        TargetControlID="cust1" HighlightCssClass="validatorCalloutHighlight" PopupPosition="Left">
                    </ajaxToolkit:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr>
                <td align="right" colspan="2">
                    <asp:ImageButton ID="btnGenerateReport" runat="server" ImageUrl="~/Images/Generate_report_hvr.gif"
                        onmouseout="this.src='Images/Generate_report_hvr.gif'" onmouseover="this.src='Images/Generate_report.gif'"
                        ValidationGroup="LeadRpt" OnClick="btnGenerateReport_Click" />
                </td>
                <td align="left" colspan="2">
                    <asp:ImageButton ID="btnCancel" runat="server" ImageUrl="~/Images/Cancel.gif" onmouseout="this.src='Images/Cancel.gif'"
                        onmouseover="this.src='Images/Cancel_hvr.gif'" ValidationGroup="VGMakeModelSeries"
                        CausesValidation="false" OnClick="btnCancel_Click" />
                </td>
            </tr>
            <tr>
                <td align="right" colspan="4">
                    <asp:Label ID="lblRowsToDisplay" runat="server" Visible="false">Rows To Display</asp:Label>
                    <asp:DropDownList ID="ddl_NoRecords" runat="server" AutoPostBack="true" Visible="false"
                        Width="50px" OnSelectedIndexChanged="ddl_NoRecords_SelectedIndexChanged">
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
                    <asp:GridView ID="gvLeadReport" AllowPaging="true" AutoGenerateColumns="false" runat="server"
                        DataKeyNames="ID" Width="100%" PageSize="10" BackColor="White" BorderColor="#CCCCCC"
                        BorderStyle="None" BorderWidth="1px" CellPadding="3" AllowSorting="True" OnPageIndexChanging="gvLeadReport_PageIndexChanging"
                        OnSorting="gvLeadReport_Sorting" EmptyDataText="No Records Found">
                        <Columns>
                            <asp:BoundField DataField="LeadDateShow" HeaderText="Date" SortExpression="LeadDate" ItemStyle-Width="80" />
                            <asp:BoundField DataField="LeadName" HeaderText="Name" SortExpression="LeadName" ItemStyle-Width="100" />
                            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" ItemStyle-Width="120" />
                            <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone" />
                            <asp:BoundField DataField="PostCode" HeaderText="Post Code" SortExpression="PostCode"
                                ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40" />
                            <asp:BoundField DataField="State" HeaderText="State" SortExpression="State" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Make" HeaderText="Make" SortExpression="Make" />
                            <asp:BoundField DataField="Model" HeaderText="Model" SortExpression="Model" />
                            <%-- <asp:BoundField DataField="Status" HeaderText="Status" ItemStyle-HorizontalAlign="Center" />--%>
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
    </asp:Panel>
</asp:Content>
