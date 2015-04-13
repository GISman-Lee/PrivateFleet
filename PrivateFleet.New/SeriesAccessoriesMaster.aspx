<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SeriesAccessoriesMaster.aspx.cs" Inherits="SeriesAccessoriesMaster" Title="Series Accessories Association" %>

<%@ Register Src="User Controls/UCSeriesAccessories.ascx" TagName="UCSeriesAccessories"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:UCSeriesAccessories ID="UCSeriesAccessories1" runat="server" />
</asp:Content>

