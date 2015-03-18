<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Uc_VDT_CustomerHelp.ascx.cs"
    Inherits="User_Controls_Uc_VDT_CustomerHelp" %>
<style type="text/css">
    .style1
    {
    }
</style>
<asp:Panel runat="server" ID="pnlCustomerHelp_1" Visible="true" DefaultButton="btnGenerateReport">
    <table width="100%">
        <tr>
            <td align="center">
                <table width="100%" cellpadding="0px" cellspacing="5px">
                    <tr>
                        <td align="center" class="style1">
                            <table>
                                <tr>
                                    <td>
                                        From Date
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtFromDate" MaxLength="10" Width="90px"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender runat="server" ID="FromDateCalender" TargetControlID="txtFromDate"
                                            Format="dd/MM/yyyy ">
                                        </ajaxToolkit:CalendarExtender>
                                        <asp:RequiredFieldValidator runat="server" ID="requiredFromDate" ControlToValidate="txtFromDate"
                                            ErrorMessage="Enter From Date." Display="None" ValidationGroup="AdminHelp"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="calloutFromdate" TargetControlID="requiredFromDate">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                        <%--   <asp:RegularExpressionValidator runat="server" ID="regExprFromDate" Display="None"
                                            ControlToValidate="txtFromDate" ErrorMessage="Enter proper date in dd/mm/yyyy format."
                                            ValidationGroup="AdminHelp" SetFocusOnError="False" ValidationExpression='^(((0?[1-9]|[12]\d|3[01])\/(0?[13578]|1[02])\/((1[6-9]|[2-9]\d)\d{2}))|((0?[1-9]|[12]\d|30)\/(0?[13456789]|1[012])\/((1[6-9]|[2-9]\d)\d{2}))|((0?[1-9]|1\d|2[0-8])\/0?2\/((1[6-9]|[2-9]\d)\d{2}))|(29\/0?2\/((1[6-9]|[2-9]\d)(0?[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$'></asp:RegularExpressionValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="valFrmDate" TargetControlID="regExprFromDate"
                                            HighlightCssClass="validatorCalloutHighlight" />--%>
                                    </td>
                                    <%--  </tr>
                                    <tr>--%>
                                    <td>
                                        To Date
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtToDate" MaxLength="10" Width="90px"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender runat="server" ID="TodateCalender" TargetControlID="txtToDate"
                                            Format="dd/MM/yyyy">
                                        </ajaxToolkit:CalendarExtender>
                                        <asp:RequiredFieldValidator runat="server" ID="requiredToDate" ControlToValidate="txtToDate"
                                            ErrorMessage="Enter To Date." Display="None" ValidationGroup="AdminHelp"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="CalloutToDate" TargetControlID="requiredToDate">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                        <%--   <asp:CompareValidator ID="cmpValTodate" runat="server" ControlToCompare="txtFromDate"
                                            ValidationGroup="AdminHelp" ControlToValidate="txtToDate" Display="None" ErrorMessage="To Date should not be less than From date"
                                            Operator="GreaterThanEqual" SetFocusOnError="true" Type="Date"></asp:CompareValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="valCmpToDate" TargetControlID="cmpValTodate">
                                        </ajaxToolkit:ValidatorCalloutExtender>--%>
                                        <asp:RegularExpressionValidator runat="server" ID="regExpreToDate" Display="None"
                                            ControlToValidate="txtToDate" ErrorMessage="Enter proper date in dd/mm/yyyy format."
                                            ValidationGroup="AdminHelp" SetFocusOnError="False" ValidationExpression='^(((0?[1-9]|[12]\d|3[01])\/(0?[13578]|1[02])\/((1[6-9]|[2-9]\d)\d{2}))|((0?[1-9]|[12]\d|30)\/(0?[13456789]|1[012])\/((1[6-9]|[2-9]\d)\d{2}))|((0?[1-9]|1\d|2[0-8])\/0?2\/((1[6-9]|[2-9]\d)\d{2}))|(29\/0?2\/((1[6-9]|[2-9]\d)(0?[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$'></asp:RegularExpressionValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="valExtToDate" TargetControlID="regExpreToDate"
                                            HighlightCssClass="validatorCalloutHighlight" />
                                        <asp:CustomValidator runat="server" ID="cust1" ClientValidationFunction="compaire_dates_ViewSentRequests1"
                                            ValidationGroup="AdminHelp" ErrorMessage="From date can not be greater than to date."
                                            Display="None" ControlToValidate="txtFromDate"> </asp:CustomValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="valCmpToDate" TargetControlID="cust1"
                                            HighlightCssClass="validatorCalloutHighlight">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td align="right" colspan="3">
                                        <table>
                                            <tr>
                                                <td align="left">
                                                    <asp:ImageButton ID="btnGenerateReport" runat="server" ImageUrl="~/Images/Submit.gif"
                                                        onmouseout="this.src='Images/Submit.gif'" onmouseover="this.src='Images/Submit_hvr.gif'"
                                                        ValidationGroup="AdminHelp" OnClick="btnGenerateReport_Click" />
                                                </td>
                                                <td align="left">
                                                    <asp:ImageButton ID="btnCancel" Visible="false" runat="server" ImageUrl="~/Images/Cancel.gif"
                                                        onmouseout="this.src='Images/Cancel.gif'" onmouseover="this.src='Images/Cancel_hvr.gif'"
                                                        ValidationGroup="VGMakeModelSeries" OnClick="btnCancel_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="padding-right: 10px">
                            <asp:Label ID="lblRowsToDisplay2" runat="server">Rows To Display:</asp:Label>
                            <asp:DropDownList ID="ddl_NoRecords2" runat="server" AutoPostBack="true" Width="50px"
                                OnSelectedIndexChanged="ddl_NoRecords2_SelectedIndexChanged">
                                <asp:ListItem Value="10">10</asp:ListItem>
                                <asp:ListItem Value="20">20</asp:ListItem>
                                <asp:ListItem Value="30">30</asp:ListItem>
                                <asp:ListItem Value="50">50</asp:ListItem>
                                <asp:ListItem Value="All">All</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView runat="server" ID="grdAdminHelp" Width="100%" AutoGenerateColumns="false"
                                AllowPaging="true" AllowSorting="true" OnPageIndexChanging="grdAdminHelp_PageIndexChanging"
                                OnRowDataBound="grdAdminHelp_RowDataBound" OnSorting="grdAdminHelp_Sorting" EmptyDataText="No Recrods Found."
                                RowStyle-Height="30px">
                                <FooterStyle CssClass="gvFooterrow" />
                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                <PagerStyle CssClass="pgr" />
                                <HeaderStyle BackColor="#0A73A2" Font-Bold="True" CssClass="gvHeader" Height="30px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Customer Name" SortExpression="fullname" ItemStyle-CssClass="grid_padding">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblCustomerName" Text='<%# bind("fullname") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Dealer Name" SortExpression="Name" ItemStyle-CssClass="grid_padding">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblDealerName" Text='<%# bind("Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description" ItemStyle-CssClass="grid_padding">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblDescription" Text='<%# bind("description") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Make" SortExpression="make" ItemStyle-CssClass="grid_padding">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblmake" Text='<%# bind("make") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Requested Date" SortExpression="date" ItemStyle-CssClass="grid_padding">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblRequestedDate" Text='<%# bind("date","{0: dd MMM yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Panel>
