<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Admin_DeliveryInNxt10Days.aspx.cs" Inherits="Admin_DeliveryInNxt10Days" %>

<%@ Register Src="~/User Controls/UC_DeliveryInNxt10days.ascx" TagName="UC_DeliveryInNxt10day"
    TagPrefix="ucRpt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <ucRpt:UC_DeliveryInNxt10day ID="ucDeliveryRpt" runat="server" Visible="true" />
    <asp:UpdateProgress ID="upProcess" runat="server" DisplayAfter="2">
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
