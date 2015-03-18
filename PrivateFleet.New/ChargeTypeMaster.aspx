<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ChargeTypeMaster.aspx.cs" Inherits="ChargeTypeMaster" Title="Fixed Charges" %>

<%@ Register Src="User Controls/UCChargeType.ascx" TagName="UCChargeType" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
     <uc1:UCChargeType ID="UCChargeType1" runat="server" />
</asp:Content>

