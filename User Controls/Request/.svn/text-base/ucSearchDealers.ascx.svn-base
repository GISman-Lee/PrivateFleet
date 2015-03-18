<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucSearchDealers.ascx.cs"
    Inherits="User_Controls_Request_ucSearchDealers" %>
<%@ Register Src="ucDealerSelection.ascx" TagName="ucDealerSelection" TagPrefix="uc1" %>
<table width="100%" align="center">
    <tr>
        <td colspan="4">
            <strong>Search Dealer</strong>
        </td>
    </tr>
    <tr>
        <td valign="top">
            State:
            <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
            </asp:DropDownList>&nbsp;</td>
        <td valign="top">
            <asp:Label ID="lblCity" runat="server" style="vertical-align:top;" Text="City :"></asp:Label>
            <asp:ListBox ID="lstCity" runat="server" style="vertical-align:top;" SelectionMode="Multiple"></asp:ListBox></td>
        <td valign="top">
            <asp:LinkButton ID="lnkGetLocations" Text="Get Locations" runat="server" OnClick="lnkGetLocations_Click"></asp:LinkButton>
        </td>
        <td valign="top">
            <asp:Label ID="lblLocation" runat="server" style="vertical-align:top;" Text="Location :"></asp:Label>
            <asp:ListBox ID="lstLocation" runat="server" style="vertical-align:top;" SelectionMode="Multiple"></asp:ListBox></td>
        
    </tr>
    <tr>
        <td valign="top" colspan="4">
            <asp:Button ID="btnSearchDealers" Text="Search Dealers" runat="server" OnClick="btnSearchDealers_Click" />
        </td>
    </tr>
    <tr>
        <td valign="top" colspan="4">
            <uc1:ucDealerSelection ID="UcDealerSelection1" runat="server" />
            
        </td>
    </tr>
</table>
