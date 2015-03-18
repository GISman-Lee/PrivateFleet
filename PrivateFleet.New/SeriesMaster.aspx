<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SeriesMaster.aspx.cs" Inherits="SeriesMaster" Title="Series Master" %>

<%@ Register Src="User Controls/UCSeries.ascx" TagName="UCSeries" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:UCSeries ID="UCSeries1" runat="server" />
</asp:Content>

