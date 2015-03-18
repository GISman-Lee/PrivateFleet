<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Admin_CustomerOrderStatus_PC.aspx.cs" Inherits="Admin_CustomerOrderStatus_PC" %>

<%@ Register Src="~/VDT_Customer/User Controls/UC_VDTCustomerReport_PC.ascx" TagName="UC_VDTCustomer_PC"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:UC_VDTCustomer_PC runat ="server" ID ="UC_VDT_Customer_PC" Visible ="true" />
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
