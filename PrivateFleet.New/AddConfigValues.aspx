<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="AddConfigValues.aspx.cs" Inherits="AddConfigValues" Title="Config Values Master" %>

<%@ Register Src="User Controls/UCAdminConfig.ascx" TagName="UCAdminConfig" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   
            <uc1:UCAdminConfig ID="UCAdminConfig1" runat="server" />
     
</asp:Content>
