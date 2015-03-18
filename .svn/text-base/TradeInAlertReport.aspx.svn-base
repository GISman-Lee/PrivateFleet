<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="TradeInAlertReport.aspx.cs" Inherits="TradeInAlertReport" %>

<%@ Register Src="~/User Controls/ucTradeInData.ascx" TagName="ucTradeInData" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pnlTradeInAlert" runat="server" align="center">
        <br />
        <table align="center" width="98%" cellpadding="0" cellspacing="0">
            <tr id="trNoRows" runat="server" visible="false">
                <td align="right">
                    <asp:Label ID="lblRowsToDisplay" runat="server">Rows To Display</asp:Label>
                    <asp:DropDownList ID="ddl_NoRecords" runat="server" AutoPostBack="true" Width="50px"
                        OnSelectedIndexChanged="ddl_NoRecords_SelectedIndexChanged">
                        <asp:ListItem Value="10">10</asp:ListItem>
                        <asp:ListItem Value="20" Selected="True">20</asp:ListItem>
                        <asp:ListItem Value="30">30</asp:ListItem>
                        <asp:ListItem Value="50">50</asp:ListItem>
                        <asp:ListItem Value="All">All</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvTradeInAlert" runat="server" AllowPaging="true" AllowSorting="true"
                        PageSize="15" DataKeyNames="ID" AutoGenerateColumns="false" Width="100%" BackColor="White"
                        ShowFooter="false" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                        CellPadding="3" EmptyDataText="No Records Found" OnPageIndexChanging="gvTradeInAlert_PageIndexChanging"
                        OnRowCommand="gvTradeInAlert_RowCommand" OnSorting="gvTradeInAlert_Sorting">
                        <Columns>
                            <asp:BoundField DataField="ConsultantName" HeaderText="Consultant Name" SortExpression="ConsultantName" />
                            <asp:BoundField DataField="CreatedDate" HeaderText="Date" SortExpression="CreatedDate1"
                                ItemStyle-Width="80" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="CustName" HeaderText="Customer Name" SortExpression="CustName"
                                ItemStyle-Width="80" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Contact" HeaderText="Contact" SortExpression="Contact"
                                ItemStyle-Width="80" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="AlertPeriod" HeaderText="Alert Days" SortExpression="AlertPeriod"
                                ItemStyle-Width="80" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Notes" HeaderText="Notes" ItemStyle-Width="80" ItemStyle-HorizontalAlign="Left" />
                            <%--    <asp:BoundField DataField="Make" NullDisplayText="--" HeaderText="Make" SortExpression="Make"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Model" NullDisplayText="--" HeaderText="Model" SortExpression="Model"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="T1Transmission" NullDisplayText="--" HeaderText="Transmission"
                    SortExpression="T1Transmission" ItemStyle-HorizontalAlign="Center" />
               <asp:BoundField HeaderText="From Year" NullDisplayText="--" DataField="FromYear"
                    SortExpression="FromYear" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="ToYear" NullDisplayText="--" HeaderText="To Year" SortExpression="ToYear"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="MinValue" NullDisplayText="--" HeaderText="Min Value"
                    SortExpression="MinValue" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="MaxValue" NullDisplayText="--" HeaderText="Max Value"
                    SortExpression="MaxValue" ItemStyle-HorizontalAlign="Center" />--%>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkView" runat="server" CommandArgument="view" CommandName="view"
                                        Text="View Details" CssClass="activeLink"></asp:LinkButton>
                                    <%--<asp:HiddenField ID="hdfMakeID" runat="server" Value='<%# Bind("MakeID") %>' />
                        <asp:HiddenField ID="hdfTrans" runat="server" Value='<%# Bind("TransmissionID") %>' />
                        <asp:HiddenField ID="hdfState" runat="server" Value='<%# Bind("StateID") %>' />--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle CssClass="gvFooterrow" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <PagerStyle CssClass="pgr" />
                        <HeaderStyle BackColor="#0A73A2" Font-Bold="True" CssClass="gvHeader" HorizontalAlign="Center"
                            Height="30px" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnl1" runat="server" Visible="false">
        <br />
        <table width="100%">
            <tr>
                <td align="right" style="padding-bottom: 10px">
                    <asp:ImageButton ID="btnBack" runat="server" ImageUrl="~/Images/back.gif" onmouseout="this.src='Images/back.gif'"
                        onmouseover="this.src='Images/back_hvr.gif'" OnClick="btnBack_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <uc1:ucTradeInData ID="ucTradeInData1" runat="server" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
