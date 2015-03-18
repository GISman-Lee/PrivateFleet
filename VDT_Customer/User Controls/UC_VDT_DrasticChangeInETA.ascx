<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UC_VDT_DrasticChangeInETA.ascx.cs"
    Inherits="VDT_Customer_UC_VDT_DrasticChangeInETA" %>
<asp:Panel runat="server" ID="pnlAutomaticMailSendReport_1" Visible="true">
    <table width="100%">
        <tr>
            <td align="center">
                <table width="100%">
                    <tr>
                        <td colspan="2" align="center">
                            <table>
                                <tr>
                                    <td>
                                        Dealer Name
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="drpDealer" Width="350px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <%--   <tr>
                                    <td>
                                        Company
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="ddlCompany" Width="150px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>--%>
                                <%-- </tr>
                           <tr>--%>
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
                        <td align="right" style="padding-right: 10px" colspan="2">
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
                            <asp:GridView runat="server" ID="grdDrasticETAChange" Width="100%" AutoGenerateColumns="false"
                                AllowPaging="true" AllowSorting="true" OnPageIndexChanging="grdDrasticETAChange_PageIndexChanging"
                                OnSorting="grdDrasticETAChange_Sorting" EmptyDataText="No Recrods Found." RowStyle-Height="30px">
                                <FooterStyle CssClass="gvFooterrow" />
                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                <PagerStyle CssClass="pgr" />
                                <HeaderStyle BackColor="#0A73A2" Font-Bold="True" CssClass="gvHeader" Height="30px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Company" SortExpression="company" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-CssClass="grid_padding">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblCompany" Text='<%# bind("company") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Dealer Name" SortExpression="name" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-CssClass="grid_padding">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lnlDealerName" Text='<%# bind("name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Make" SortExpression="make" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-CssClass="grid_padding">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lnlmake" Text='<%# bind("make") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Customer Name" SortExpression="fullname" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-CssClass="grid_padding">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lnlcustomername" Text='<%# bind("fullname") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ETA" SortExpression="eta" ItemStyle-CssClass="grid_padding"
                                        ItemStyle-Width="80">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblcurrenteta" Text='<%# bind("eta","{0:dd MMM yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Change by Days" SortExpression="diff" ItemStyle-CssClass="grid_padding">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblDiff" Text='<%# bind("diff") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Dealer Notes" SortExpression="DealerNotes" ItemStyle-CssClass="grid_padding">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblDealerNotes" Text='<%# bind("DealerNotes") %>'></asp:Label>
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
