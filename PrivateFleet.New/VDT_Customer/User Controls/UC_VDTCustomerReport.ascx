<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UC_VDTCustomerReport.ascx.cs"
    Inherits="VDT_Customer_UC_VDTCustomerReport" %>
<%@ Register Src="~/User Controls/UCDealers_ClientList.ascx" TagName="ucClient" TagPrefix="uc1" %>
<asp:Panel runat="server" ID="pnlCustomerRepor_1" Visible="true" DefaultButton="btnGenerateReport">
    <table width="100%">
        <tr>
            <td align="center">
                <table cellpadding="5px" cellspacing="2px">
                    <tr>
                        <td>
                            Customer Name
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtFullName"></asp:TextBox>
                        </td>
                        <td>
                            Dealer Name
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="drpDealer" Width="155px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Customer Email
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtEmail"></asp:TextBox>
                        </td>
                        <td>
                            Phone
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtphone"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Make
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="drpMake" Width="155px">
                            </asp:DropDownList>
                        </td>
                        <td>
                            Order Status
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="drpOrderStatus" Width="155px">
                                <asp:ListItem Text="All" Value="All"></asp:ListItem>
                                <asp:ListItem Text="Complete" Value="Complete"></asp:ListItem>
                                <asp:ListItem Text="InProcess" Value="InProcess"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <table width="100%">
                                <tr>
                                    <td align="right">
                                        <asp:ImageButton ID="btnGenerateReport" runat="server" ImageUrl="~/Images/Submit.gif"
                                            onmouseout="this.src='Images/Submit.gif'" onmouseover="this.src='Images/Submit_hvr.gif'"
                                            ValidationGroup="AdminHelp" OnClick="btnGenerateReport_Click" />
                                    </td>
                                    <td align="left">
                                        <asp:ImageButton ID="btnCancel" runat="server" ImageUrl="~/Images/Cancel.gif" onmouseout="this.src='Images/Cancel.gif'"
                                            onmouseover="this.src='Images/Cancel_hvr.gif'" OnClick="btnCancel_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        </tr __designer:mapid="113">
        <tr>
            <td align="center">
                <table width="100%">
                    <tr>
                        <td align="right" style="padding-right: 10px">
                            <asp:Label ID="lblRowsToDisplay2" runat="server">Rows To Display:</asp:Label>
                            <asp:DropDownList ID="ddl_NoRecords2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_NoRecords2_SelectedIndexChanged"
                                Width="50px">
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
                            <asp:GridView ID="grdCustomer" runat="server" AllowPaging="true" AllowSorting="true"
                                AutoGenerateColumns="false" EmptyDataText="No Recrods Found." OnPageIndexChanging="grdCustomer_PageIndexChanging"
                                OnRowCommand="grdCustomer_RowCommand" OnRowDataBound="grdCustomer_RowDataBound"
                                OnSorting="grdCustomer_Sorting" RowStyle-Height="30px" Width="100%">
                                <FooterStyle CssClass="gvFooterrow" />
                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                <PagerStyle CssClass="pgr" />
                                <HeaderStyle BackColor="#0A73A2" CssClass="gvHeader" Font-Bold="True" Height="30px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Customer Name" SortExpression="fullname" ItemStyle-CssClass="grid_padding">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCustomerName" runat="server" Text='<%# bind("fullname") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Customer Email" SortExpression="email" ItemStyle-CssClass="grid_padding">
                                        <ItemTemplate>
                                            <asp:Label ID="lblemail" runat="server" Text='<%# bind("email") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Phone" SortExpression="Phone" ItemStyle-CssClass="grid_padding">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPhone" runat="server" Text='<%# bind("Phone") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Dealer Name" SortExpression="name" ItemStyle-CssClass="grid_padding">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDealerName" runat="server" Text='<%# bind("name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Make" ItemStyle-CssClass="grid_padding">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMake" runat="server" Text='<%# bind("Make") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Order Status" SortExpression="orderStatus" ItemStyle-CssClass="grid_padding">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%# bind("orderStatus") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-CssClass="grid_padding">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkbtnView" runat="server" AlternateText="View" CommandArgument='<%# bind("dealeremail") %>'
                                                CommandName="show" CssClass="activeLink" Text="View" />
                                            <asp:HiddenField ID="hiddencustomerid" runat="server" Value='<%# bind("id") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
            <tr>
                <td>
                    <asp:Label ID="lblshow" runat="server"></asp:Label>
                    <asp:Label ID="lblCancel" runat="server"></asp:Label>
                    <ajaxToolkit:ModalPopupExtender ID="modal" runat="server" BackgroundCssClass="ModalCSS"
                        CancelControlID="lblCancel" Enabled="false" PopupControlID="pnlClientView" TargetControlID="lblshow">
                    </ajaxToolkit:ModalPopupExtender>
                    <asp:Panel ID="pnlClientView" runat="server" BackColor="White" Visible="false">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td align="left" style="background-color: #0A73A2; color: White; padding-left: 10px;
                                    font-weight: bold; height: 30px">
                                    Customer Order Status Detail
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div style="overflow: auto; height: 400px; width: 800px;">
                                        <uc1:ucClient ID="ucClient" runat="server" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="padding-right: 10px">
                                    <asp:ImageButton ID="imgbtnModalCancel" runat="server" ImageUrl="~/Images/Cancel.gif"
                                        OnClick="imgbtnModalCancel_Click" onmouseout="this.src='Images/Cancel.gif'" onmouseover="this.src='Images/Cancel_hvr.gif'" />
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 5px">
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </tr>
    </table>
</asp:Panel>
