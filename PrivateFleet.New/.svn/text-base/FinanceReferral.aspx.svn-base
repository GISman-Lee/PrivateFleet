<%@ Page Title="Finance Referral Details" Language="C#" AutoEventWireup="true" CodeFile="FinanceReferral.aspx.cs"
    Inherits="FinanceReferral" MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table  Width="100%">
                <tr>
                    <td>
                        <span style="text-align: center;"><b>
                            <asp:Label ID="lblTitle" runat="server" Text="Finance Referral Details" ForeColor="White"></asp:Label></b></span>
                        <span style="float: right; margin-top: -1px; margin-right: 8px; z-index: 2;">
                            <asp:ImageButton ID="btnBack" runat="server" ImageUrl="~/Images/back.gif" onmouseout="this.src='Images/back.gif'"
                                onmouseover="this.src='Images/back_hvr.gif'" OnClick="btnBack_Click" />
                        </span>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:GridView BorderColor="Gray" ID="gvFinanceReferral" runat="server" AllowPaging="True"
                            OnPageIndexChanging="gvFinanceReferral_PageIndexChanging" AutoGenerateColumns="False"
                            AllowSorting="true" PageSize="15" Visible="true" Width="100%" BackColor="White"
                            BorderStyle="Solid" BorderWidth="1px" CellPadding="3" OnSorting="gvFinanceReferral_Sorting">
                            <Columns>
                                <asp:BoundField DataField="Name" HeaderStyle-Wrap="false" HeaderText="User Name"
                                    SortExpression="Name" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="Details" HeaderText="Finance Referral Details" SortExpression="Details"
                                    ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="EmailTo" HeaderText="Email Sent To " SortExpression="EmailTo"
                                    ItemStyle-HorizontalAlign="Center" />
                            </Columns>
                            <FooterStyle CssClass="gvFooterrow" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <PagerStyle CssClass="pgr" />
                            <HeaderStyle BackColor="#0A73A2" Font-Bold="True" CssClass="gvHeader" Height="30px" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
