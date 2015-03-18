<%@ Page Title="Vehicle Delivery Tracking Report" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="VDT_CustomerHelp.aspx.cs" Inherits="VDT_CustomerHelp" %>
<%@ Register Src ="~/User Controls/Uc_VDT_CustomerHelp.ascx" TagName ="UC_CustomerHelp" TagPrefix ="uc1" %>
<%@ Register Src ="~/VDT_Customer/User Controls/UC_VDT_AutomaticMailSendReport.ascx" TagName ="UC_VDT_AutomaticMailReport" TagPrefix ="uc1" %>
<%@ Register Src ="~/VDT_Customer/User Controls/UC_VDTDealerResponseReport.ascx" TagName ="UC_VDT_DealerRespone" TagPrefix ="uc1" %>
<%@ Register Src ="~/VDT_Customer/User Controls/UC_VDTCustomerReport.ascx"  TagName ="UC_VDTCustomer" TagPrefix ="uc1" %>
<%@ Register Src ="~/VDT_Customer/User Controls/UC_VDTDealerOrederCount.ascx" TagName ="UC_VDTDealerCustomercount" TagPrefix ="uc1" %>
<%@ Register Src ="~/VDT_Customer/User Controls/UC_VDT_DrasticChangeInETA.ascx" TagName ="UC_DrasticChangeETA" TagPrefix ="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<table width ="100%">
    <tr >
        <td align="right">
            Select Report Type    
        </td>
        <td align="left">
            <asp:DropDownList runat ="server" ID="drpReportType" AutoPostBack ="true" OnSelectedIndexChanged ="drpReportType_SelectedIndexChanged">
                <asp:ListItem Text="--Select--" Value ="0" Selected ="True"></asp:ListItem>
                <asp:ListItem Text="Customer Help Report" Value="1"></asp:ListItem>
                <asp:ListItem Text="Automatic EMail Send Report" Value="2"></asp:ListItem>
                <asp:ListItem Text="Dealer No Response Report" Value="3"></asp:ListItem>
                <asp:ListItem Text="Customer Order Status Report" Value="4"></asp:ListItem>
                <asp:ListItem Text="Dealer Summary Report" Value="5"></asp:ListItem>
                 <asp:ListItem Text="Drastic Change In ETA" Value="6"></asp:ListItem>
            </asp:DropDownList>
            
        </td>
    </tr>

</table>
    <asp:Panel runat ="server" ID="pnlCustomerHelp" Visible ="false">
     <uc1:UC_CustomerHelp runat ="server" ID="ucCustomerHelp" />
    </asp:Panel>
    
    <asp:Panel runat ="server" ID="pnlAutomaticMailReport" Visible ="false">
        <uc1:UC_VDT_AutomaticMailReport  runat="server" id="UC_VDTAutomaticMail" Visible ="true"></uc1:UC_VDT_AutomaticMailReport>
    </asp:Panel>
    
    <asp:Panel runat ="server" ID="pnlDealerResponseReport" Visible ="false">
            <uc1:UC_VDT_DealerRespone runat ="server" ID ="ucDealerResponse" Visible ="true" />
    </asp:Panel>
    
    <asp:Panel runat ="server" ID="PnlCustomerReport" Visible ="false">
        <uc1:UC_VDTCustomer runat ="server" ID ="UC_VDT_Customer" Visible ="true" />
    </asp:Panel>
    
    <asp:Panel runat ="server" ID="pnlDealerCustomerCount" Visible ="false">
        <uc1:UC_VDTDealerCustomercount runat ="server" ID ="UC_VDTDealerCustomer" Visible ="true" />
    </asp:Panel>
    
    
     <asp:Panel runat ="server" ID="pnDrasticChangeETA" Visible ="false">
       <uc1:UC_DrasticChangeETA runat ="server" ID="uc_DrasticChangeETA" />
    </asp:Panel>
    
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

