<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="DealerReport.aspx.cs" Inherits="DealerReport" Title="Dealer Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" align="center" cellpadding="1" cellspacing="3" border="0">
        <tr>
            <td align="right">
                <strong>Search Criteria:</strong>
            </td>
            <td align="left">
                <asp:DropDownList ID="ddlSearch" runat="server" Height="21px" Width="119px" Display="None"
                    InitialValue="-Select-" AppendDataBoundItems="True">
                    <asp:ListItem Value="0">-Select Criteria-</asp:ListItem>
                    <asp:ListItem Value="1">Today</asp:ListItem>
                    <asp:ListItem Value="2">Yesterday</asp:ListItem>
                    <asp:ListItem Value="3">Last 7 days</asp:ListItem>
                    <asp:ListItem Value="4">This month</asp:ListItem>
                    <asp:ListItem Value="5">Last Month</asp:ListItem>
                    <asp:ListItem Value="6">All Time</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvSearch" runat="server" ControlToValidate="ddlSearch"
                    ErrorMessage="Search Criteria Required" ValidationGroup="VGSearch" Display="None"
                    InitialValue="0" SetFocusOnError="True">
                </asp:RequiredFieldValidator>
                <ajaxToolkit:ValidatorCalloutExtender ID="ValidateSearch" runat="server" TargetControlID="rfvSearch"
                    HighlightCssClass="validatorCalloutHighlight">
                </ajaxToolkit:ValidatorCalloutExtender>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:ImageButton ID="btnGenerateReport" runat="server" ImageUrl="~/Images/Generate_report_hvr.gif"
                    onmouseout="this.src='Images/Generate_report_hvr.gif'" onmouseover="this.src='Images/Generate_report.gif'"
                    ValidationGroup="VGSearch" OnClick="btnGenerateReport_Click" />
            </td>
            <td>
                <span style="color: White">***</span>
                <asp:ImageButton ID="btnCancel" runat="server" ImageUrl="~/Images/Cancel.gif" onmouseout="this.src='Images/Cancel.gif'"
                    onmouseover="this.src='Images/Cancel_hvr.gif'" ValidationGroup="VGMakeModelSeries"
                    OnClick="btnCancel_Click" />
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Label ID="lblMsg" runat="server" Visible="false">
               <strong>No record to display.</strong>
                </asp:Label>
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
                <asp:GridView ID="gvDealerReport" runat="server" AllowPaging="True" AllowSorting="True"
                    PageSize="15" AutoGenerateColumns="False" Width="100%" BackColor="White" BorderColor="#CCCCCC"
                    BorderStyle="None" BorderWidth="1px" CellPadding="3" OnRowDeleting="gvDealerReport_RowDeleting"
                    OnPageIndexChanging="gvDealerReport_PageIndexChanging" OnSorting="gvDealerReport_Sorting">
                    <Columns>
                        <asp:BoundField DataField="Dealer" HeaderText="Dealer" SortExpression="Dealer" />
                        <asp:BoundField DataField="QuotesRecieved" SortExpression="QuotesRecieved" HeaderText="Quotes Recived">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="QuotesReturned" SortExpression="QuotesReturned" HeaderText="Quotes Returned">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="WinningQuotes" SortExpression="WinningQuotes" HeaderText="Winning Quotes">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DoneDeals" SortExpression="DoneDeals" HeaderText="Done Deals">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PWinning" SortExpression="PWinning" HeaderText="Winning Quotes %">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="WinDeals" SortExpression="WinDeals" HeaderText="Winning Deals %">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ResopnseTime" SortExpression="ResopnseTime" HeaderText="Response Time(Hrs)">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                    </Columns>
                    
                    <FooterStyle CssClass="gvFooterrow" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <PagerStyle CssClass="pgr" />
                    <HeaderStyle BackColor="#0A73A2" Font-Bold="True" CssClass="gvHeader" Height="30px" />
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
