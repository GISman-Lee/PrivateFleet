<%@ Page Language="C#" AutoEventWireup="true" CodeFile="vdtLogin.aspx.cs" Inherits="VDT_Customer_Index" %>

<%@ Register Src="~/VDT_Customer/User Controls/UC_VDT_Loginl.ascx" TagName="ucVDT_Login"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <%--25 Mar 2013 : For IE 10 Javascript issue--%>
    <meta http-equiv="X-UA-Compatible" content="IE=9" />
    <title>Vehicle Delivery Tracking</title>
    <link href="../CSS/stylesheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="1000px" border="0" align="center" cellpadding="0" cellspacing="0" class="mainbdr">
            <tr>
                <td align="left" valign="top">
                    <div class="logo">
                        <img src="../images/Private_fleet_logo.jpg" alt="" width="298" height="113" /></div>
                    <div class="banner">
                        <img src="../images/Banner.jpg" alt="" width="700" height="113" /></div>
                </td>
            </tr>
            <tr>
                <td align="left" valign="top">
                    <div class="topnavd">
                        <div class="logout">
                            &nbsp;</div>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <table width="960" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF"
                        class="mainbdr">
                        <tr>
                            <td align="left" valign="top">
                                <div class="maincontaint">
                                    <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#fafbfb"
                                        class="mainbdr">
                                        <tr>
                                            <td align="center" colspan="2" valign="middle">
                                                <div align="center" style="vertical-align: middle; width: 100%; height: 400px;">
                                                    <br />
                                                    <table width="100%">
                                                        <tr>
                                                            <td style="font-weight: bold; font-size: larger; color: #606060; font-size: 25px"
                                                                align="center">
                                                                Delivery Update Tracker
                                                                <br />
                                                                <br />
                                                                <br />
                                                                <br />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 10px">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center">
                                                                <uc1:ucVDT_Login runat="server" ID="ucLogin" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
