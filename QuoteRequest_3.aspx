<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="QuoteRequest_3.aspx.cs" Inherits="QuoteRequest_3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UPQuoteRequest" runat="server">
        <ContentTemplate>
            <asp:Panel ID="QuoteRequest3" runat="server">
                <table width="100%">
                    <tr align="center">
                        <td colspan="2" style="padding: 10px; font-size: 15px; text-decoration: underline;
                            font-weight: bolder">
                            <asp:Label ID="lblMsg" runat="server" Text="Quote Request"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table width="100%">
                                <tr>
                                    <td colspan="2" width="100%" style="padding-top: 0px">
                                        <asp:DataList ID="DataList1" runat="server" RepeatDirection="Horizontal" GridLines="Both"
                                            Width="100%" BorderStyle="solid" BorderColor="#acacac" BorderWidth="1px">
                                            <ItemTemplate>
                                                <table width="100%">
                                                    <tr class="ucHeader">
                                                        <td align="center">
                                                            <asp:Label ID="lblHeader" runat="server" CssClass="gvLabel" Text='<%# Bind("Header") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-size: 20px; font-weight: bold;" align="center">
                                                            <asp:Label ID="lblDetails" runat="server" CssClass="gvLabel" Text='<%# Bind("Details") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </td>
                                </tr>
                                <tr class="ucHeader" style="width: 100%; border: solid 1px #acacac;">
                                    <td align="left">
                                        <asp:Label ID="lblHeader" runat="server" CssClass="gvLabel" Text="">Request Parameter</asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" width="100%">
                                        <asp:DataList ID="DataList2" runat="server" RepeatDirection="Horizontal" GridLines="Both"
                                            BorderStyle="solid" BorderColor="#acacac" BorderWidth="1px" Width="100%">
                                            <ItemTemplate>
                                                <table width="100%">
                                                    <tr>
                                                        <td align="left" style="width: 50%; background-color: #eaeaea;">
                                                            <asp:Label ID="lblHeader1" runat="server" CssClass="gvLabel" Text='<%# Bind("Header") %>'></asp:Label>
                                                        </td>
                                                        <td style="padding-left: 10px" align="left">
                                                            <asp:Label ID="Label1" runat="server" CssClass="gvLabel" Text='<%# Bind("Details") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </td>
                                </tr>
                                <tr id="trAddAcc" runat="server" visible="false" class="ucHeader" style="width: 100%;
                                    padding: 10px; border: solid 1px #acacac;">
                                    <td align="left">
                                        <asp:Label ID="Label3" runat="server" CssClass="gvLabel" Text="">Additional Accessories</asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" width="100%">
                                        <asp:DataList ID="DataList3" runat="server" RepeatDirection="Horizontal" GridLines="Both"
                                            BorderStyle="solid" BorderColor="#acacac" BorderWidth="1px" Width="100%">
                                            <ItemTemplate>
                                                <table width="100%">
                                                    <tr>
                                                        <td align="left" style="width: 50%; background-color: #eaeaea; font-weight: bold;">
                                                            <asp:Label ID="lblHeader2" runat="server" CssClass="gvLabel" Text='<%# Bind("Header") %>'></asp:Label>
                                                        </td>
                                                        <td style="padding-left: 10px" align="left">
                                                            <asp:Label ID="Label2" runat="server" CssClass="gvLabel" Text='<%# Bind("Details") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr id="trConsNotes" runat="Server" visible="false" align="left">
                        <td colspan="2">
                            <table style="border: solid 1px #acacac;">
                                <tr>
                                    <td valign="top">
                                        <asp:Label ID="Label4" runat="server" Width="120px" Font-Bold="true" Text="Consultant Notes :"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label5" Width="680px" runat="server">
                                            <asp:Literal ID="lit" runat="server">
                                            </asp:Literal></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr align="left">
                        <td colspan="2" style="padding-top: 25px; font-size: 15px; font-weight: bolder">
                            <asp:Label ID="Label2" runat="server" Text="You Created the Quote Request for the Dealers - "></asp:Label>
                        </td>
                    </tr>
                    <tr align="center">
                        <td colspan="2" style="padding: 10px">
                            <asp:GridView ID="gvSelectedDealers1" runat="server" DataKeyNames="ID" Width="100%"
                                AllowPaging="true" PageSize="10" AutoGenerateColumns="false" BorderColor="#acacac"
                                CellPadding="1" CellSpacing="3" EmptyDataText="No records found">
                                <Columns>
                                    <asp:BoundField DataField="Name" HeaderText="Dealer Name" />
                                    <asp:BoundField DataField="Email" HeaderText="Email" />
                                    <asp:BoundField DataField="Phone" HeaderText="Phone Number" />
                                    <asp:BoundField DataField="Fax" HeaderText="Fax" Visible="False" />
                                    <%-- <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkRemove" runat="server" Text="Remove Dealer" CommandName="RemoveDealer"
                                                        CssClass="activeLink">
                                                    </asp:LinkButton>
                                                    <asp:HiddenField ID="hdfID" runat="server" Value='<%# Bind("ID") %>' />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>--%>
                                </Columns>
                                <RowStyle Width="22px" />
                                <HeaderStyle BackColor="#0A73A2" Font-Bold="True" CssClass="gvHeader" Width="25px" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr align="left">
                        <td colspan="2" style="padding: 10px; font-size: 15px; font-weight: bolder">
                            <asp:Label ID="lblMasg1" runat="server" Text="Do you want to create it?"></asp:Label>
                        </td>
                    </tr>
                    <tr style="padding: 10px">
                        <td align="left" valign="top" width="40%" style="height: 20px">
                            <asp:ImageButton ID="imgbutPre" runat="server" ImageUrl="~/Images/back.gif" onmouseout="this.src='Images/back.gif'"
                                onmouseover="this.src='Images/back_hvr.gif'" OnClick="imgbutPre_Click" />
                        </td>
                        <td align="left" valign="top" width="60%" style="height: 20px">
                            <asp:ImageButton ID="imgbutCreate" runat="server" ImageUrl="~/Images/Create_request.gif"
                                onmouseout="this.src='Images/Create_request.gif'" onmouseover="this.src='Images/Create_request_hvr.gif'"
                                ValidationGroup="Create" OnClick="imgbutCreate_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
