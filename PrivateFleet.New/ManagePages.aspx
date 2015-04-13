<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ManagePages.aspx.cs" Inherits="ManagePages" Title="Page Master" %>

<%@ Register Src="~/User Controls/ucPageMaster.ascx" TagName="ucPageMaster" TagPrefix="uc" %>

<asp:content id="Content1" contentplaceholderid="ContentPlaceHolder1" runat="Server">
        <uc:ucPageMaster id="UcPageMaster1" runat="server">
        </uc:ucPageMaster>
</asp:content>
