<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCCustStatusUpForTrade2.ascx.cs" Inherits="User_Controls_UCCustStatusUpForTrade2" %>
<asp:Panel runat="server" ID="pnlAutomaticMailSendReport_1" Visible="true">
    <table width="100%">
        <tr>
            <td align="center">
                <table width="100%">
                    <tr>
                        <td align="center">
                            <table>
                                <tr>
                                    <td>
                                        Primary Contact
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="ddlPrimaryContact" Width="150px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2">
                                        <asp:ImageButton ID="btnGenerateReport" runat="server" ImageUrl="~/Images/Submit.gif"
                                            onmouseout="this.src='Images/Submit.gif'" onmouseover="this.src='Images/Submit_hvr.gif'"
                                            OnClick="btnGenerateReport_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
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
                            <asp:GridView runat="server" ID="grdDeliveryReport" Width="100%" AutoGenerateColumns="false"
                                AllowPaging="true" AllowSorting="true" OnPageIndexChanging="grdDeliveryReport_PageIndexChanging"
                                OnSorting="grdDeliveryReport_Sorting" EmptyDataText="No Recrods Found." RowStyle-Height="30px"
                                OnRowCommand="grdDeliveryReport_RowCommand">
                                <FooterStyle CssClass="gvFooterrow" />
                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                <PagerStyle CssClass="pgr" />
                                <HeaderStyle BackColor="#0A73A2" Font-Bold="True" CssClass="gvHeader" Height="30px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Primary Contact" ItemStyle-HorizontalAlign="Left"
                                        SortExpression="PrimaryContact" ItemStyle-CssClass="grid_padding">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblPrimaryContact" Text='<%# bind("PrimaryContact") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Customer Name" SortExpression="customerName" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-CssClass="grid_padding">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblCustomerName" Text='<%# bind("customerName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="New Car" SortExpression="Make" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-CssClass="grid_padding">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblmake" Text='<%# bind("Make") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- <asp:TemplateField HeaderText="Customer Email" SortExpression="CustomerEmail" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="grid_padding">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblCustomerEmail" Text='<%# bind("CustomerEmail") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="phone" SortExpression="phone"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="grid_padding">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblphone" Text='<%# bind("phone") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
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
                                    <asp:TemplateField HeaderText="Trade In Status" SortExpression="Tradestatus" ItemStyle-CssClass="grid_padding">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTradeStatus" Text='<%# bind("Tradestatus") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-CssClass="grid_padding">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkViewDetails" runat="server" CommandName="View" Text="View Details"
                                                CommandArgument='<%#Eval("CustomerID") %>' CssClass="activeLink"></asp:LinkButton>
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