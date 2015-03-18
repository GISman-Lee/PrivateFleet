<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MakeMaster.aspx.cs" Inherits="MakeMaster" Title="Make Master" %>

<%@ Register Src="User Controls/UCMake.ascx" TagName="UCMake" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:UCMake ID="UCMake1" runat="server" />
</asp:Content>

