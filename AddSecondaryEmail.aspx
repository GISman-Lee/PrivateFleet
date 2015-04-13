<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="AddSecondaryEmail.aspx.cs" Inherits="AddSecondaryEmail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <br />
    <table cellpadding="2" cellspacing="2" align="center" width="70%">
        <tr style="height: 30px;">
            <td colspan="2" align="Center">
                <asp:Label ID="lblMsg" Font-Bold="true" ForeColor="Red" runat="server" Visible="false"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 125px;">
                <asp:Label ID="lblSecondaryMail" runat="server" Text="Secondary Email - "></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtSecondaryMail" runat="server" Width="350px"></asp:TextBox>
                <asp:Label ID="lblSecondaryMail_1" Font-Bold="true" runat="server"></asp:Label>
                &nbsp;
                <asp:LinkButton ID="lnkUpdate" runat="server" Text="update" Font-Size="Smaller" OnClick="lnkUpdate_Click"></asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="right" style="padding-right: 36px;">
                <asp:ImageButton ID="imgbtnSubmit" runat="server" ImageUrl="~/Images/Submit.gif"
                    OnClick="imgbtnSubmit_Click" onmouseout="this.src='Images/Submit.gif'" onmouseover="this.src='Images/Submit_hvr.gif'"
                    ValidationGroup="VGSubmit" />
            </td>
        </tr>
    </table>
</asp:Content>
