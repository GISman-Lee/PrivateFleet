<%@ Page Title="Announcement" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeFile="AdminAnnouncement.aspx.cs" Inherits="AdminAnnouncement"
    ValidateRequest="false" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellpadding="0" cellspacing="0" width="98%" align="center">
        <tr style="height: 20px;">
            <td>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblAdminAnnMsg" runat="server" ForeColor="Red" Font-Bold="true" Visible="false"></asp:Label>
            </td>
        </tr>
        <tr style="height: 20px;">
            <td>
            </td>
        </tr>
        <tr>
            <td style="width: 15%; vertical-align: top; font: ">
                <asp:Label ID="lblAnn" runat="server" Text="Announcement :"></asp:Label>
            </td>
            <td style="width: 85%;">
                <asp:TextBox ID="txtAnn" runat="server" TextMode="MultiLine" Width="550px" Height="120px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td style="width: 85%;">
                <asp:Label ID="lblMsg" runat="server" Font-Size="11px" Text="* To apply bold style to word use < b > sample word < / b >"></asp:Label>
                <asp:Label ID="lblMsg_1" runat="server" Font-Size="11px" Text="<br/>* similarly for Itelice use < i > sample word < / i >, for Underline < u > sample word < / i >"></asp:Label>
            </td>
        </tr>
        <tr style="height: 8px;">
            <td>
            </td>
        </tr>
        <tr align="center">
            <td colspan="2">
                <asp:ImageButton ID="imgbtnSave" runat="server" ImageUrl="~/Images/Submit.gif" onmouseout="this.src='Images/Submit.gif'"
                    onmouseover="this.src='Images/Submit_hvr.gif'" OnClick="imgbtnSave_Click" />
                <asp:ImageButton ID="imgbtnCancel" runat="server" ImageUrl="~/Images/Cancel.gif"
                    onmouseout="this.src='Images/Cancel.gif'" onmouseover="this.src='Images/Cancel_hvr.gif'"
                    OnClick="imgbtnCancel_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
