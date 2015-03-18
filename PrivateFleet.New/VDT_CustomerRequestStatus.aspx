<%@ Page Title="Customer Request to Update Status Report" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="VDT_CustomerRequestStatus.aspx.cs" Inherits="VDT_CustomerRequestStatus" %>
<%@ Register Src="~/User Controls/UC_VDT_ClientUpdateRequest.ascx" TagName="ucClietRequstsendReprot"
    TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<uc1:ucClietRequstsendReprot runat ="server" ID="UCClietRequstsendReprot" />
</asp:Content>

