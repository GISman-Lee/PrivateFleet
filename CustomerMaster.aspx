<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="CustomerMaster.aspx.cs" Inherits="CustomerMaster" Title="Untitled Page" %>

<%@ Register Src="User Controls/UCCustomerView.ascx" TagName="UCCustomerView" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:UCCustomerView id="UCCustomerView1" runat="server">
    </uc1:UCCustomerView>
</asp:Content>
