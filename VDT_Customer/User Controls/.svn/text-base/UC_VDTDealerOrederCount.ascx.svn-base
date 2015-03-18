<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UC_VDTDealerOrederCount.ascx.cs"
    Inherits="VDT_Customer_User_Controls_UC_VDTDealerOrederCountl" %>



<table width="100%">
    <tr>
        <td align="center">
            <table width="100%">
                <tr>
                    <td align="center">
                        <table>
                            <tr>
                                <td>
                                    Make
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="drpMake" Width="350">
                                    </asp:DropDownList>
                                </td>
                                <%--</tr>
                            <tr>
                            <td></td>--%>
                            </tr>
                            <tr>
                                <td>
                                    Company
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlCompany" Width="350">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    <asp:ImageButton ID="btnGenerateReport" runat="server" ImageUrl="~/Images/Submit.gif"
                                        onmouseout="this.src='Images/Submit.gif'" onmouseover="this.src='Images/Submit_hvr.gif'"
                                        ValidationGroup="AdminHelp" OnClick="btnGenerateReport_Click" />
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
                    <td colspan="2">
                        <asp:GridView runat="server" ID="grdDealerOrder" Width="100%" AutoGenerateColumns="false"
                            AllowPaging="true" AllowSorting="true" OnPageIndexChanging="grdDealerOrder_PageIndexChanging"
                            OnSorting="grdDealerOrder_Sorting" OnRowDataBound="grdDealerOrder_RowDataBound"
                            EmptyDataText="No Recrods Found." RowStyle-Height="30px">
                            <FooterStyle CssClass="gvFooterrow" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <PagerStyle CssClass="pgr" />
                            <HeaderStyle BackColor="#0A73A2" Font-Bold="True" CssClass="gvHeader" Height="30px" />
                            <Columns>
                                <asp:TemplateField HeaderText="Company" SortExpression="company"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="grid_padding">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblCompany" Text='<%# bind("company") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dealer Name" SortExpression="name" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="grid_padding">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblDealerName" Text='<%# bind("name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dealer Email" ItemStyle-CssClass="grid_padding" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblDealerEmail" Text='<%# bind("email") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Make" SortExpression="make" ItemStyle-CssClass="grid_padding" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblMake" Text='<%# bind("make") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Order" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"
                                    SortExpression="orderTotal" ItemStyle-CssClass="grid_padding">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblTotalOrder" Text='<%# bind("orderTotal") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Complete Orders" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-VerticalAlign="Middle" SortExpression="ordercomplete" ItemStyle-CssClass="grid_padding">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblOrderComplete" Text='<%# bind("ordercomplete") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="InComplete Orders" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-VerticalAlign="Middle" SortExpression="IncompleteOrder">
                                    <ItemTemplate>
                                         <asp:Label runat="server" ID="lblInOrderComplete" Text='<%# bind("IncompleteOrder") %>'></asp:Label>
                                        <%-- <asp:HyperLink ID="hypInCompleteOrders" runat="server" Text='<%# bind("IncompleteOrder") %>'
                                            ToolTip="View Details" Target="_self"></asp:HyperLink>--%>
                                        <%--   <asp:HyperLink ID="HypGiveMark" Target="_blank" Style="border: none; text-decoration: none;"
                                            runat="server">
                                            <asp:Literal ID="Literal3" Text="<%$ Resources:MechResource,GiveMarks %>" runat="server"></asp:Literal>
                                        </asp:HyperLink>--%>
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
