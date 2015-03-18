<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCFinanceReferralDetails.ascx.cs"
    Inherits="User_Controls_UCFinanceReferralDetails" %>
   
<div>
    <asp:GridView ID="gvFinanceReferral" runat="server" AllowPaging="True" OnPageIndexChanging="gvFinanceReferral_PageIndexChanging"
        AutoGenerateColumns="False" AllowSorting="true" PageSize="15" Visible="true"
        Width="100%" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
        CellPadding="3" OnSorting="gvFinanceReferral_Sorting">
        <Columns>
            <asp:BoundField DataField="Name" HeaderText="User Name" SortExpression="Name" ItemStyle-HorizontalAlign="Center" />
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
</div>
