<%@ Page Title="Customer Status Update" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Admin_CustomerOrderStatus.aspx.cs" Inherits="Admin_CustomerOrderStatus" %>
<%@ Register Src ="~/VDT_Customer/User Controls/UC_VDTCustomerReport.ascx"  TagName ="UC_VDTCustomer" TagPrefix ="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<br>

  <uc1:UC_VDTCustomer runat ="server" ID ="UC_VDT_Customer" Visible ="true" />
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

