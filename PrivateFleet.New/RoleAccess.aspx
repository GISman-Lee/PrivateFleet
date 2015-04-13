<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
CodeFile="RoleAccess.aspx.cs" Inherits="RoleAccess" Title="Role-Access Management" %>

<%@ Register Src="User Controls/UserRoleAccess/ucRoleAccess.ascx" TagName="ucRoleAccess"
    TagPrefix="uc1" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ucRoleAccess ID="UcRoleAccess1" runat="server" />

</asp:Content>
