<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UC_VDT_DashBoard.ascx.cs"
    Inherits="User_Controls_UC_VDT_DashBoard" %>
<%@ Register Src="~/User Controls/UC_VDT_VechileDealivaryReport.ascx" TagName="UC_VehicleDelivaryReport"
    TagPrefix="uc1" %>
<%@ Register Src="~/User Controls/UC_VDT_DealerResponseReport.ascx" TagName="UCDealerResponse"
    TagPrefix="uc1" %>
<%@ Register Src="~/User Controls/UC_VDT_ClientUpdateRequest.ascx" TagName="ucClietRequstsendReprot"
    TagPrefix="uc1" %>
<%@ Register Src="~/User Controls/UC_VDT_ETACommingReport.ascx" TagName="UcETA" TagPrefix="uc1" %>
<table width="100%">
    <tr>
        <td>
            <table width="100%" cellpadding="10px">
                <tr>
                    <td style="width: 50%; height: 400px" align="left" valign="top">
                        <table width="100%" cellspacing="0" border="1" style="border-collapse: collapse;
                            border-color: #c8c8c8; height: 100%; border-width: thin">
                            <tr>
                                <td style="background-color: #d5ecfd; color: Black; font-weight: bold; height: 30px;
                                    padding-left: 5px;">
                                  Vehicle Delivered
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" style="height: 15px;">
                                    <marquee onmouseover="this.stop();" onmouseout="this.start();" style="color:#b90505">
                                             <asp:Label runat ="server" ID="lblVechileMarqueeMessage" Text ="" Font-Bold="true"  ></asp:Label>
        
                                      </marquee>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <uc1:UC_VehicleDelivaryReport runat="server" ID="ucVechileDelivary"></uc1:UC_VehicleDelivaryReport>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-right: 5px; height: 20px;" align="right">
                                    <asp:HyperLink runat="server" ID="hypVehicle" Text="View Full Report>>" NavigateUrl="#" Style="text-decoration: none;"> </asp:HyperLink>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 50%; height: 400px" align="left" valign="top">
                        <table width="100%" border="1" style="border-collapse: collapse; border-color: #c8c8c8;
                            height: 100%; border-width: thin">
                            <tr>
                                <td style="background-color: #d5ecfd; color: Black; font-weight: bold; height: 30px;
                                    padding-left: 5px;">
                                    Customer Request for Status Update
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" style="height: 15px;">
                                    <marquee onmouseover="this.stop();" onmouseout="this.start();" style="color:#b90505">
                                            <asp:Label runat ="server" ID="lblClientResponseMarquee" Text ="" Font-Bold="true"  ></asp:Label>
        
                                      </marquee>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <uc1:ucClietRequstsendReprot runat="server" ID="UCClientRequstSend" />
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-right: 5px; height: 20px" align="right">
                                    <asp:HyperLink runat="server" ID="hypClientResponse" Text="View Full Report>>" NavigateUrl="#"
                                        Style="text-decoration: none;"> </asp:HyperLink>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="height: 20px;">
                    </td>
                </tr>
                <tr>
                    <td valign="top" style="height: 400px" valign="top">
                        <table width="100%" border="1" style="border-collapse: collapse; border-color: #c8c8c8;
                            height: 100%; border-width: thin">
                            <tr>
                                <td style="background-color: #d5ecfd; color: Black; font-weight: bold; height: 30px;border-color:#c8c8c8;
                                    padding-left: 5px;">
                                    Dealer Respones
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" style="height: 15px;">
                                    <marquee onmouseover="this.stop();" onmouseout="this.start();" style="color:#b90505">
                                            <asp:Label runat ="server" ID="lblDealerResponseMarquee" Text ="" Font-Bold="true"  ></asp:Label>
        
                                      </marquee>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <uc1:UCDealerResponse runat="server" ID="ucDealerResponseReport" />
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-right: 5px; height: 20px" align="right" valign="top">
                                    <asp:HyperLink runat="server" ID="hypDealerResponse" Text="View Full Report>>" NavigateUrl="#"
                                        Style="text-decoration: none;"> </asp:HyperLink>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td valign="top" style="height: 400px" valign="top">
                        <table width="100%" border="1" style="border-collapse: collapse; border-color: #c8c8c8;
                            height: 100%; border-width: thin">
                            <tr>
                                <td style="background-color: #d5ecfd; color: Black; font-weight: bold; height: 30px; 
                                    padding-left: 5px;">
                                    ETA Coming Closer
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" style="height: 15px;">
                                    <marquee onmouseover="this.stop();" onmouseout="this.start();" style="color:#b90505">
                                             <asp:Label runat ="server" ID="lblETAMarquee" Text ="" Font-Bold="true"  ></asp:Label>
        
                                      </marquee>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <uc1:UcETA runat="server" ID="UCETAReport"></uc1:UcETA>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-right: 5px; height: 20px" align="right">
                                    <asp:HyperLink runat="server" ID="hypETA" Text="View Full Report>>" NavigateUrl="#" Style="text-decoration: none;"> </asp:HyperLink>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
