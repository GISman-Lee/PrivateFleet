<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Index"
    Title="Private Fleet Login" %>

<%@ Register Src="User Controls/UcLogin.ascx" TagName="UcLogin" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head runat="server">
    <%--25 Mar 2013 : For IE 10 Javascript issue--%>
    <%--    <meta http-equiv="X-UA-Compatible" content="IE=9" />--%>
    <meta http-equiv="X-UA-Compatible" content="IE=9">
    <link href="CSS/stylesheet.css" rel="stylesheet" type="text/css" />
    <title>Private Fleet Login</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="1000px" border="0" align="center" cellpadding="0" cellspacing="0" class="mainbdr">
            <tr>
                <td align="left" valign="top">
                    <div class="logo">
                        <img src="images/Private_fleet_logo.jpg" alt="" width="298" height="113" />
                    </div>
                    <div class="banner">
                        <img src="images/Banner.jpg" alt="" width="700" height="113" />
                    </div>
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
                <td align="left" valign="top">
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
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <uc1:UcLogin ID="UcLogin1" runat="server" />
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
            <tr>
                <td align="left" valign="top">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="left" valign="top">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="left" valign="top">
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
