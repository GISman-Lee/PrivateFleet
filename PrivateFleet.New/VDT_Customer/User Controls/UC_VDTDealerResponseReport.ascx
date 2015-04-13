<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UC_VDTDealerResponseReport.ascx.cs"
    Inherits="VDT_Customer_User_Controls_UC_VDTDealerResponseReport" %>
<table width="100%">
    <tr>
        <td align="center">
            <table width="100%">
                <tr>
                    <td>
                        <table width="100%">
                            <tr>
                                <td runat="server" id="DealerNonResponseLowervalue">
                                    <div style="width: 20px; height: 7px; background-color: #ffc3c3;">
                                    </div>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblDealerNonResponseLowervalue"></asp:Label>
                                </td>
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
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:GridView runat="server" ID="grdDealerResponse" Width="100%" AutoGenerateColumns="false"
                            AllowPaging="true" AllowSorting="true" OnPageIndexChanging="grdDealerResponse_PageIndexChanging"
                            OnSorting="grdDealerResponse_Sorting" EmptyDataText="No Recrods Found." RowStyle-Height="30px"
                            OnRowDataBound="grdDealerResponse_RowDataBound">
                            <FooterStyle CssClass="gvFooterrow" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <PagerStyle CssClass="pgr" />
                            <HeaderStyle BackColor="#0A73A2" Font-Bold="True" CssClass="gvHeader" Height="30px" />
                            <Columns>
                                <asp:TemplateField HeaderText="Customer Service Rep." SortExpression="CustomerSerRep"
                                    ItemStyle-CssClass="grid_padding">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblCustomerSerRep" Text='<%# bind("CustomerSerRep") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dealer Name" SortExpression="name" ItemStyle-CssClass="grid_padding">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblDealerName" Text='<%# bind("name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Make" SortExpression="Make" ItemStyle-CssClass="grid_padding">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblmake" Text='<%# bind("make") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dealer Email" ItemStyle-CssClass="grid_padding">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblDealerEmail" Text='<%# bind("email") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Customer Name" SortExpression="fullname" ItemStyle-CssClass="grid_padding">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblCustomer" Text='<%# bind("fullname") %>'></asp:Label>
                                        <asp:HiddenField runat="server" ID="hiddencsss" Value='<%# bind("css") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Last Updated On" SortExpression="lastupdate" ItemStyle-CssClass="grid_padding"
                                    ItemStyle-Width="80">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lbldate" Text='<%# bind("lastupdate","{0: dd MMM yyyy }") %>'></asp:Label>
                                        <asp:HiddenField runat="server" ID="hdfDateDiff" Value='<%# bind("Diff") %>' />
                                        
                                        <asp:HiddenField runat="server" ID="hdfeta" Value='<%# bind("ETA") %>' />
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
