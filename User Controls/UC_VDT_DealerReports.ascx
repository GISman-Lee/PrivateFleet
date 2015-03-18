<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UC_VDT_DealerReports.ascx.cs" Inherits="User_Controls_UC_VDT_DealerReports" %>
<%@ Register Src ="~/User Controls/UC_VDT_VechileDealivaryReport.ascx" TagName ="ucDelevaryReport" TagPrefix ="uc1" %>
<%@ Register Src ="~/User Controls/UC_VDT_DealerResponseReport.ascx" TagName ="UcDealerResponse" TagPrefix ="uc1" %>
<%@ Register Src ="~/User Controls/UC_VDT_ClientUpdateRequest.ascx" TagName ="UcClientRequest" TagPrefix ="uc1"%>
<%@ Register Src ="~/User Controls/UC_VDT_ETACommingReport.ascx" TagName ="UcETAComming" TagPrefix ="uc1" %>

<table width ="100%">
    <tr>
        <td align ="center">
            <table>
                <tr>
                    <td align="left">
                        Select Report Type
                    </td>
                    <td>
                        <asp:DropDownList runat ="server" ID="drpReportType" AutoPostBack ="true" OnSelectedIndexChanged ="drpReportType_SelectedIndexChanged">
                            <asp:ListItem Text ="-- Select --" Value ="0"></asp:ListItem>
                            <asp:ListItem Text= "Vechicle Delivered" Value ="1"></asp:ListItem>
                            <asp:ListItem Text ="No Response Report" Value ="2"></asp:ListItem>
                            <asp:ListItem Text ="Request comes for Dealer Update" Value ="3"></asp:ListItem>
                            <asp:ListItem Text ="ETA Comming Closer Report" Value ="4"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                
            </table>
        
        </td>
    </tr>
    <tr>
        <td style="width:100%">
            <asp:Panel runat ="server" ID ="pnlDelivaryReport" Visible ="false">
                <uc1:ucDelevaryReport runat ="server" ID="ucVechileDelivaryReport" />
            </asp:Panel>
            <asp:Panel runat ="server" ID="pnlDealerResponseReport" Visible ="false">
            <uc1:UcDealerResponse runat ="server" ID ="ucDealerResponseReport" />
            </asp:Panel>
            <asp:Panel runat ="server" ID="pnlClientRequest" Visible ="false" >
                <uc1:UcClientRequest  runat ="server" ID ="Uc_Client_Request" />
            </asp:Panel>
            <asp:Panel runat ="server" ID="pnlETA" Visible ="false">
                <uc1:UcETAComming runat="server" id="ucETA"></uc1:UcETAComming>
            </asp:Panel>
            
        
         </td>
    
    </tr>
</table>
