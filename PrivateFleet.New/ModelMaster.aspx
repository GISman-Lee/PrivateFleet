<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ModelMaster.aspx.cs" Inherits="ModelMaster" Title="Model Master" %>

<%@ Register Src="User Controls/UCModel.ascx" TagName="UCModel" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:UCModel ID="UCModel1" runat="server" />
</asp:Content>

