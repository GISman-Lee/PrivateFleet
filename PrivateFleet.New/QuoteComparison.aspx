<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="QuoteComparison.aspx.cs" Inherits="QuoteComparison" Title="Quote Comparison" %>

<%@ Register Src="User Controls/Request/ucRequestHeader.ascx" TagName="ucRequestHeader"
    TagPrefix="uc1" %>
    
    
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpPanQoateCom" runat="server">
        <ContentTemplate>
            <table style="width: 100%">
                <tr>
                    <td align="right" style="width: 100%">
                        <%--  <asp:ImageButton ID="btnPrint" runat="server" ImageUrl="~/Images/print_hvr.gif" onmouseout="this.src='Images/print_hvr.gif'"
                            onmouseover="this.src='Images/print.gif'" OnClick="btnPrint_Click" />--%>
                        <asp:ImageButton ID="btnBack" runat="server" ImageUrl="~/Images/back.gif" OnClick="btnBack_Click"
                            onmouseout="this.src='Images/back.gif'" onmouseover="this.src='Images/back_hvr.gif'" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%; height: 17px;">
                        <uc1:ucRequestHeader ID="UcRequestHeader1" runat="server" />
                    </td>
                </tr>
                <tr runat="server" id="trDealerInfo" visible="true">
                    <td>
                        <table width="100%">
                            <tr class="ucHeader" style="width: 100%; border: solid 1px #acacac;">
                                <td align="left">
                                    <asp:Label ID="lblDealer" runat="server" CssClass="gvLabel" Text="">Dealer</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100%">
                                    <asp:GridView ID="gvDealerInfo" runat="server" AllowPaging="false" GridLines="Both"
                                        AutoGenerateColumns="False" BackColor="White" BorderColor="#acacac" BorderStyle="Solid"
                                        BorderWidth="1px" CellPadding="3" PageSize="5" Width="100%" DataKeyNames="Reminder"
                                        OnRowDataBound="gvDealerInfo_RowDataBound" OnRowCommand="gvDealerInfo_RowCommand">
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle CssClass="pgr" />
                                        <HeaderStyle BackColor="#eaeaea" ForeColor="Black" CssClass="gvHeader" Font-Bold="True"
                                            Height="20px" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Dealer Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDealerName" runat="server" Text='<%# Bind("[Dealer Name]") %>'>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Company">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCompany" runat="server" Text='<%# Bind("Company") %>'>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Contacts">
                                                <ItemTemplate>
                                                    <%--<asp:Label ID="Email"  runat="server" Text='<%# Bind("Email") %>'>'></asp:Label>--%>
                                                    Email : <a href='<%# "mailto:"+ Eval("Email") %>' style="color: Blue; text-decoration: underline;">
                                                        <asp:Label ID="Email" runat="server" Text='<%#Bind("Email") %>'></asp:Label></a>
                                                    <br />
                                                    Mobile :
                                                    <asp:Label ID="lblFax" runat="server" Text='<%#Bind("Mobile") %>'></asp:Label>
                                                    <br />
                                                    Phone :
                                                    <asp:Label ID="lblPhone" runat="server" Text='<%#Bind("Phone") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Reminder">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkRemind" runat="server" CommandArgument='<%# Bind("DealerID") %>'
                                                        CommandName="RemindDealer" CssClass="activeLink">
                                                        <asp:Literal ID="litlnk" Text="Quotation Pending <br /> Remind Dealer" runat="server"></asp:Literal>
                                                    </asp:LinkButton>
                                                    <asp:HiddenField runat="server" ID="LastRemindDateTime" Value='<% #Bind("LastRemindDateTime")%>' />
                                                    <asp:HiddenField runat="server" ID="QuotationID" Value='<% #Bind("QoutaionExist")%>' />
                                                    <asp:HiddenField runat="server" ID="LastDate" Value='<% #Bind("LastRemindDateTime")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Revisions">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="inkRevision" runat="server" CommandArgument='<%# Bind("DealerID") %>'
                                                        CommandName="ReviseQuote" CssClass="activeLink" Enabled="false" Text="View">
                                                <%--<asp:Literal ID="litlnk" Text="Quotation Pending <br /> Remind Dealer" runat="server"></asp:Literal>--%>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkBtnEmailWinningQuote" runat="server" Text="Email Winning Quote"
                                                        CommandArgument='<%# Bind("DealerID") %>' CommandName="SendWinningQuote" CssClass="activeLink"
                                                        Enabled="false"></asp:LinkButton><asp:Label ID="lblMailSent" ForeColor="Red" Font-Names="Verdana" Font-Size="11px" Text=""
                                                            runat="server"></asp:Label>
                                                    <asp:HiddenField ID="hdfIsShortlisted" runat="server" Value='<%# Bind("IsShortlisted") %>' />
                                                    <asp:HiddenField ID="hdfIsShortlisted1" runat="server" Value='<%# Bind("IsShortlisted1") %>' />
                                                    <asp:HiddenField ID="hdnIsQuoteSent" runat="server" Value='<%# Bind("IsQuoteSent") %>' />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        <asp:GridView ID="gvMakeDetails" runat="server" AutoGenerateColumns="False" BackColor="White"
                            BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" OnPageIndexChanging="gvMakeDetails_PageIndexChanging"
                            OnRowDataBound="gvMakeDetails_RowDataBound" PageSize="30" Width="100%" OnRowCreated="gvMakeDetails_RowCreated"
                            ShowFooter="True" OnRowCommand="gvMakeDetails_RowCommand">
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <PagerStyle CssClass="pgr" />
                            <HeaderStyle BackColor="#0A73A2" CssClass="gvHeader" Font-Bold="True" Height="30px" />
                            <Columns>
                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("Description") %>'>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:HiddenField ID="hdnDealerID" runat="server" Value="" />
                        <asp:HiddenField ID="hdnIsQuoteSent" runat="server" Value="" />
                    </td>
                </tr>
                <tr>
                    <td align="center" style="width: 100%">
                        <asp:ImageButton ID="btnBack_Copy" runat="server" ImageUrl="~/Images/back.gif" OnClick="btnBack_Click"
                            onmouseout="this.src='Images/back.gif'" onmouseover="this.src='Images/back_hvr.gif'" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
