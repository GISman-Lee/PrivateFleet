<%@ Page Title="Vehicle Delivery Report" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="VDT_VehicleDelivaryReportDealer.aspx.cs" Inherits="VDT_Customer_VDT_VehicleDelivaryReportDealer" %>
<%@ Register Src="~/User Controls/UC_VDT_VechileDealivaryReport.ascx" TagName ="ucVehileDelivary" TagPrefix ="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<uc1:ucVehileDelivary runat="server" ID="UCVehicleDelivary" />
</asp:Content>

