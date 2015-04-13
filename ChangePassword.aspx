<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="ChangePassword" Title="Untitled Page" %>

<%@ Register Src="User Controls/ucChangePassword.ascx" TagName="ucChangePassword"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    &nbsp;<table style="width: 100%">
        <tr>
            <td align="center" style="width: 100px">
    <uc1:ucChangePassword id="UcChangePassword1" runat="server">
    </uc1:ucChangePassword></td>
        </tr>
    </table>
</asp:Content>

