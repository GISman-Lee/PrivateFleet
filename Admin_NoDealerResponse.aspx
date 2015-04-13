<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Admin_NoDealerResponse.aspx.cs" Inherits="Admin_NoDealerResponse" %>

<%@ Register Src="~/VDT_Customer/User Controls/UC_VDTDealerResponseReport.ascx" TagName="UC_VDT_DealerRespone"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br>
    <uc1:UC_VDT_DealerRespone runat="server" ID="ucDealerResponse" Visible="true" />
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
