<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="StateMaster.aspx.cs" Inherits="StateMaster" Title="State Master" %>

<%@ Register Src="User Controls/UCState.ascx" TagName="UCState" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:UCState ID="UCState1" runat="server" />
</asp:Content>

