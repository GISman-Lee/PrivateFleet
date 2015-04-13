<%@ Page Title="ETA Comming Closer Report" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="VDT_ETACommingCloser.aspx.cs" Inherits="VDT_ETACommingCloser" %>
<%@ Register Src="~/User Controls/UC_VDT_ETACommingReport.ascx" TagName="UcETA" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <uc1:UcETA runat="server" ID="UCETAReport"></uc1:UcETA>
</asp:Content>

