<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Admin_VDT_Dashboard.aspx.cs" Inherits="Admin_VDT_Dashboard" %>

<%@ Register Src ="~/VDT_Customer/User Controls/UC_VDT_AdminDashboardl.ascx" TagName ="ucVDTDashboard" TagPrefix ="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


<table width="100%">
    <tr>
        <td>
            <uc1:ucVDTDashboard runat ="server" ID="UCVDTDashboard" />
        </td>
    </tr>
</table>
</asp:Content>

