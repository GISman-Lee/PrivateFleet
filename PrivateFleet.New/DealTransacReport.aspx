<%@ Page Title="Dealer Topup Report" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="DealTransacReport.aspx.cs" Inherits="DealTransacReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upnTransaction" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hdfDealerConId" runat="server" Value="" />
            <table width="98%" style="border: 1px solid #CCC;" align="center">
                <tr>
                    <td style="height: 3px;">
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="2">
                        <asp:Label ID="lblFromDate" runat="server" Text="From Date :"></asp:Label>
                        <asp:TextBox ID="txtFromDate" runat="server" Text="" class="inputClass"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="calFromDate" runat="server" TargetControlID="txtFromDate"
                            PopupButtonID="txtFromDate" PopupPosition="BottomLeft">
                        </ajaxToolkit:CalendarExtender>
                        <%--  <asp:CompareValidator ID="CompValFromDate" runat="server" ControlToValidate="txtFromDate"
                                            ErrorMessage="FromDate Should be less than To Date" SetFocusOnError="true" ValueToCompare=""
                                            Operator="LessThanEqual" Type="Date" Display="None" ValidationGroup="search">
                                        </asp:CompareValidator>
                                        <ajax:ValidatorCalloutExtender ID="vceFromDate" runat="server" TargetControlID="CompValFromDate"
                                            PopupPosition="TopLeft">
                                       </ajax:ValidatorCalloutExtender>--%>
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="2">
                        <asp:Label ID="lblToDate" runat="server" Text="To Date :"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtToDate" runat="server" Text="" class="inputClass"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="calToDate" runat="server" TargetControlID="txtToDate"
                            PopupButtonID="txtToDate" PopupPosition="BottomLeft">
                        </ajaxToolkit:CalendarExtender>
                        <%--                                        <asp:CompareValidator ID="CompValTodate" runat="server" Display="None" ControlToValidate="txtToDate"
                                            ErrorMessage="To Date Should be greater than From Date" SetFocusOnError="true"
                                            ControlToCompare="txtFromDate" Operator="GreaterThanEqual" Type="Date" ValidationGroup="search">
                                        </asp:CompareValidator>
                                        <ajax:ValidatorCalloutExtender ID="vceToDate" runat="server" TargetControlID="CompValTodate"
                                            PopupPosition="TopLeft">
                                        </ajax:ValidatorCalloutExtender>--%>
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="2">
                        <asp:Label ID="lblDealer" runat="server" Text="Dealer :"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlDealer" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDealer_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="height: 20px; padding-left: 178px;" align="left" colspan="2">
                        <asp:ImageButton ID="lnkSearch" runat="server" AlternateText="Search" OnClick="lnkSearch_Click"
                            ValidationGroup="search" ImageUrl="~/Images/Submit.gif" onmouseout="this.src='Images/Submit.gif'"
                            onmouseover="this.src='Images/Submit.gif'" Style="float: left; margin-right: 5px;">
                        </asp:ImageButton>
                        <%--<asp:ImageButton ID="lnkClear" runat="server" AlternateText="Clear" OnClick="lnkClear_Click"
                            ImageUrl="~/images/clear_btnover.gif" onmouseout="this.src='images/clear_btnover.gif'"
                            onmouseover="this.src='images/clear_btn.jpg'" Style="float: left;"></asp:ImageButton>
                        <asp:ImageButton ID="btnPrint" runat="server" ImageUrl="~/Images/print_hvr.gif" onmouseout="this.src='Images/print_hvr.gif'"
                                            onmouseover="this.src='Images/print.gif'" OnClientClick="javascript:CallPrint()"
                                            Style="float: right; padding-left: 5px;" Visible="false" />
                                        <asp:ImageButton ID="lnkBack" runat="server" AlternateText="Back" OnClick="lnkBack_Click"
                                            ImageUrl="~/images/back.gif" onmouseout="this.src='images/back.gif'" onmouseover="this.src='images/back_hvr.gif'"
                                            Visible="false" Style="float: right;"></asp:ImageButton>
                                        <asp:ImageButton ID="imgbtnExportCSV" runat="server" ImageUrl="~/Images/xls.png"
                                            ToolTip="Export to Excel" Style="float: right; padding-left: 5px;" Visible="false"
                                            OnClick="imgbtnExportCSV_Click" AlternateText="Export To Excel" />--%>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" width="100%">
                        <table align="center" width="100%">
                            <tr>
                                <td align="left">
                                    <asp:Label ID="lblTotal" runat="server"></asp:Label>
                                </td>
                                <td align="right">
                                    <asp:Label ID="lblAdminMsg" runat="server" Text=""></asp:Label>
                                    <asp:Label ID="lblRowsToDisplay" runat="server">Rows To Display:</asp:Label>
                                    <asp:DropDownList ID="ddl_NoRecords" runat="server" AutoPostBack="true" Width="50px"
                                        OnSelectedIndexChanged="ddl_NoRecords2_SelectedIndexChanged">
                                        <asp:ListItem Value="10" Selected="True">10</asp:ListItem>
                                        <asp:ListItem Value="20">20</asp:ListItem>
                                        <asp:ListItem Value="30">30</asp:ListItem>
                                        <asp:ListItem Value="50">50</asp:ListItem>
                                        <asp:ListItem Value="All">All</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:GridView ID="gvDealTopUp" runat="server" Width="100%" BackColor="White" BorderColor="#CCCCCC"
                                        BorderStyle="None" AutoGenerateColumns="false" BorderWidth="1px" CellPadding="3" AllowPaging="true" AllowSorting="true"
                                        PageSize="10" OnSorting="gvDealTopUp_Sorting" OnPageIndexChanging="gvDealTopUp_PageIndexChanging"
                                        OnRowDataBound="gvDealTopUp_RowDataBound" ShowFooter="true">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Dealer Company" SortExpression="CompanyName">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCompanyName" runat="server" Text='<% #Bind("CompanyName") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblFooterCompanyName" runat="server" Text="Total -"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Contact Name" SortExpression="DealerName">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblName" runat="server" Text='<% #Bind("DealerName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount Deposited($)" SortExpression="AmountDeposited">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAmountCredited" runat="server" Text='<% #Bind("AmountDeposited") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblFooterAmtDeposited" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount Spent($)" SortExpression="AmountSpent">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAmountSpent" runat="server" Text='<% #Bind("AmountSpent") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblFooterAmtSpent" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Balance($)" SortExpression="Balance">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBalance" runat="server" Text='<% #Bind("Balance") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblFooterAmtBalance" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Postal Code" SortExpression="PostalCode">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPostalCode" runat="server" Text='<% #Bind("PostalCode") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Leads Sold" SortExpression="LeadsSold">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLeadsSold" runat="server" Text='<% #Bind("LeadsSold") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblFooterLeadSolds" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Radius" SortExpression="Distance">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDistance" runat="server" Text='<% #Bind("Distance") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle CssClass="gvFooterrow" />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle CssClass="pgr" />
                                        <HeaderStyle BackColor="#0A73A2" CssClass="gvHeader" Font-Bold="True" Height="25px" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
