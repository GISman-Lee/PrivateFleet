<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UC_VDT_AdminDashboardl.ascx.cs"
    Inherits="VDT_Customer_User_Controls_UC_VDT_AdminDashboardl" %>
<%@ Register Src="~/VDT_Customer/User Controls/UC_VDT_DrasticChangeInETA.ascx" TagName="uc_DrasticETAChange"
    TagPrefix="uc1" %>
<%@ Register Src="~/User Controls/Uc_VDT_CustomerHelp.ascx" TagName="uc_CustomerRequestHelp"
    TagPrefix="uc1" %>
<%@ Register Src="~/VDT_Customer/User Controls/UC_VDTDealerResponseReport.ascx" TagName="uc_DealerResponse"
    TagPrefix="uc1" %>
<%@ Register Src="~/VDT_Customer/User Controls/UC_VDTDealerOrederCount.ascx" TagName="uc_DealerOrderSummary"
    TagPrefix="uc1" %>
<style type="text/css">
    .style2
    {
    }
</style>
<table width="100%"">
    <tr>
        <td>
            <table width="750px" cellpadding="10px">
                <tr>
                    <td align="left" valign="top" style="width: 375px; height: 330px; overflow: auto;">
                        <table width="100%" cellspacing="0" border="1" style="border-collapse: collapse;
                            border-color: #c8c8c8; height: 100%; border-width: thin">
                            <tr>
                                <td style="background-color: #d5ecfd; color: Black; font-weight: bold; height: 30px;
                                    padding-left: 5px;">
                                    Drastic Change in ETA
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" style="height: 15px;">
                                    <marquee onmouseover="this.stop();" onmouseout="this.start();" style="color: #b90505">
                                             <asp:Label runat ="server" ID="lblDrasticETAChangeMarqueeMessage" Text ="" Font-Bold="true"  ></asp:Label></marquee>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <uc1:uc_DrasticETAChange runat="server" ID="ucDrasticChangeETA" />
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-right: 5px; height: 20px;" align="right">
                                    <asp:HyperLink runat="server" ID="hypDrasticETAChange" Text="View Full Report>>"
                                        NavigateUrl="~/Admin_DrasticChangeETA.aspx?type=6" Style="text-decoration: none;"> </asp:HyperLink>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="left" valign="top" style="width: 375px; height: 330px; overflow: auto;">
                        <table width="100%" cellspacing="0" border="1" style="border-collapse: collapse;
                            border-color: #c8c8c8; height: 100%; border-width: thin">
                            <tr>
                                <td style="background-color: #d5ecfd; color: Black; font-weight: bold; height: 30px;
                                    padding-left: 5px;">
                                    Customer Request for Help
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" style="height: 15px;">
                                    <marquee onmouseover="this.stop();" onmouseout="this.start();" style="color: #b90505">
                                             <asp:Label runat ="server" ID="lblCustomerHelpMarquee" Text ="" Font-Bold="true"  ></asp:Label></marquee>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <uc1:uc_CustomerRequestHelp runat="server" ID="ucCustomerHelp" />
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-right: 5px; height: 20px;" align="right">
                                    <asp:HyperLink runat="server" ID="hypCustomerNeedHelp" Text="View Full Report>>"
                                        NavigateUrl="" Style="text-decoration: none;"> </asp:HyperLink>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 375px; height: 330px; overflow: auto;" align="left" valign="top">
                        <table width="100%" cellspacing="0" border="1" style="border-collapse: collapse;
                            border-color: #c8c8c8; height: 100%; border-width: thin">
                            <tr>
                                <td style="background-color: #d5ecfd; color: Black; font-weight: bold; height: 30px;
                                    padding-left: 5px;">
                                    Dealer Order Summary
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" style="height: 15px;">
                                    <marquee onmouseover="this.stop();" onmouseout="this.start();" style="color: #b90505">
                                             <asp:Label runat ="server" ID="lblDrasticETAChangeMarquee" Text ="" Font-Bold="true"  ></asp:Label></marquee>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <uc1:uc_DealerOrderSummary runat="server" ID="UCDealerOrer" />
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-right: 5px; height: 20px;" align="right">
                                    <asp:HyperLink runat="server" ID="hypDealercount" Text="View Full Report>>" NavigateUrl=""
                                        Style="text-decoration: none;"> </asp:HyperLink>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 375px; height: 330px; overflow: auto;" align="left" valign="top">
                        <table width="100%" cellspacing="0" border="1" style="border-collapse: collapse;
                            border-color: #c8c8c8; height: 100%; border-width: thin">
                            <tr>
                                <td style="background-color: #d5ecfd; color: Black; font-weight: bold; height: 30px;
                                    padding-left: 5px;">
                                    Dealer No Response
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" style="height: 15px;">
                                    <marquee onmouseover="this.stop();" onmouseout="this.start();" style="color: #b90505">
                                             <asp:Label runat ="server" ID="lblDealerNoResponseMarquee" Text ="" Font-Bold="true"  ></asp:Label></marquee>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <uc1:uc_DealerResponse runat="server" ID="ucDealerResponse" />
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-right: 5px; height: 20px;" align="right">
                                    <asp:HyperLink runat="server" ID="hypDealerNoResponse" Text="View Full Report>>"
                                        NavigateUrl="~/Admin_NoDealerResponse.aspx?type=3" Style="text-decoration: none;"> </asp:HyperLink>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
