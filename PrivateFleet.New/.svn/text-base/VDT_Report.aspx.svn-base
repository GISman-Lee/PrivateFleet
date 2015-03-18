<%@ Page Title="Vehicle Delivery Tracker Reports" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="VDT_Report.aspx.cs" Inherits="VDT_Report" %>
<%@ Register Src ="~/User Controls/UC_VDT_DealerReports.ascx" TagName ="ucReport" TagPrefix ="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<uc1:ucReport runat ="server" ID="Report" />
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

