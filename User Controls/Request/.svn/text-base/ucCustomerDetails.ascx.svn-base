<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucCustomerDetails.ascx.cs" Inherits="User_Controls_Request_ucCustomerDetails" %>

<table width="90%">
    
    <tr>
            <td colspan="2" width="100%">
                <asp:DataList ID="DataList1" runat="server" RepeatDirection="Horizontal" GridLines="Both"
                    Width="100%" BorderStyle="solid" BorderColor="#acacac" BorderWidth="1px">
                    <ItemTemplate>
                        <table width="100%">
                            <tr style ="color:White; background-color :#0A73A2; font-weight :bold">
                                <td align="center" bgcolor="">
                                    <asp:Label ID="lblHeader" runat="server" CssClass="gvLabel" Text='<%# Bind("Header") %>'></asp:Label></td>
                            </tr>
                            <tr>
                                <td style="height: 21px" align="center">
                                    <asp:Label ID="lblDetails" runat="server" CssClass="gvLabel" Text='<%# Bind("Details") %>'></asp:Label></td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList></td>
        </tr>
</table>