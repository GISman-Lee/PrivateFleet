<%@ Page Title="Vehicle Delivery Tracker Dashboard" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="VDT_Dashboard.aspx.cs" Inherits="VDT_Dashboard" %>
<%@ Register Src="~/User Controls/UC_VDT_DashBoard.ascx" TagName ="VDTDashboard" TagPrefix ="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<uc1:VDTDashboard runat ="server" ID ="ucVDTDashboard" />
 <asp:UpdateProgress ID="upProcess" runat="server" DisplayAfter="2" >
    <ProgressTemplate>
        <div id="progressBackgroundFilter">
        </div>
        <div id="processMessage">
            <span style="text-align: center;">
                <img alt="" src="Images/loading.gif" /><br />
                Loading...Please wait...</span></div>
    </ProgressTemplate>
</asp:UpdateProgress>

</asp:Content>

