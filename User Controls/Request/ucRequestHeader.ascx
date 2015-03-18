<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucRequestHeader.ascx.cs"
    Inherits="User_Controls_Request_ucRequestHeader" %>
<table width="100%">
    <tr>
        <td width="100%" style="padding-top: 0px;">
            <asp:DataList ID="DataList1" GridLines="Both" runat="server" RepeatDirection="Horizontal" 
                Width="100%">
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
    <%--<tr class="ucHeader" style="width: 100%; border: solid 1px #acacac;">
        <td align="left">
            <asp:Label ID="lblHeader" runat="server" CssClass="gvLabel" Text="">Request Parameter</asp:Label>
        </td>
    </tr>--%>
    <tr>
        <td style="width:100%; border:solid 1px black;" >
            <table width="100%">
                <tr class="ucHeader" style="width: 100%; border: solid 1px #acacac;">
                    <td align="left">
                        <asp:Label ID="lblHeader" runat="server" CssClass="gvLabel" Text="">Request Parameter</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width:100%" align="right">
                        <asp:DataList ID="DataList2" runat="server" RepeatDirection="Horizontal" GridLines="Both"
                            Width="85%">
                            <ItemTemplate>
                                <table width="100%">
                                    <tr>
                                        <td align="left" style="width: 50%; background-color: #eaeaea; font-weight: bold;">
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
            </table>
        </td>
    </tr>
</table>
