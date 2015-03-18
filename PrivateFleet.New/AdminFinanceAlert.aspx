<%@ Page Title="Finance Alerts" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="AdminFinanceAlert.aspx.cs" Inherits="AdminFinanceAlert" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <table width="99%">
        <tr id="trPaging" runat="server" visible="false">
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
                <asp:GridView runat="server" ID="gvAdminFinAlert" Width="100%" AutoGenerateColumns="false"
                    AllowPaging="true" AllowSorting="true" OnPageIndexChanging="gvAdminFinAlert_PageIndexChanging"
                    OnSorting="gvAdminFinAlert_Sorting" EmptyDataText="No Recrods Found." RowStyle-Height="30px">
                    <FooterStyle CssClass="gvFooterrow" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <PagerStyle CssClass="pgr" />
                    <HeaderStyle BackColor="#0A73A2" Font-Bold="True" CssClass="gvHeader" Height="30px" />
                    <Columns>
                        <asp:TemplateField HeaderText="Alert Send on" SortExpression="Date1" ItemStyle-CssClass="grid_padding">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblDate" Text='<%# bind("Date") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Customer Name" SortExpression="customerName" ItemStyle-HorizontalAlign="Left"
                            ItemStyle-CssClass="grid_padding">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblCustomerName" Text='<%# bind("CustName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="New Car" SortExpression="Make" ItemStyle-HorizontalAlign="Left"
                            ItemStyle-CssClass="grid_padding">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblmake" Text='<%# bind("Make") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Supplying DealerShip" SortExpression="Company" ItemStyle-HorizontalAlign="Left"
                            ItemStyle-CssClass="grid_padding">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblDealerShip" Text='<%# bind("Company") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Dealer Name" SortExpression="DealerName" ItemStyle-HorizontalAlign="Left"
                            ItemStyle-CssClass="grid_padding">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblDealerName" Text='<%# bind("DealerName") %>'></asp:Label>
                                <asp:HiddenField ID="hdfDealerId" runat="server" Value='<%# Eval("DealerID") %>' />
                                <asp:HiddenField ID="hdfDealerEmail" runat="server" Value='<%# Eval("DealerEmail") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ETA" SortExpression="ETASort" ItemStyle-CssClass="grid_padding">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblETA" Text='<%# bind("ETA") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
