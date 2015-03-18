<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintCalculator.aspx.cs"
    Inherits="PrintCalculator" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print of Calculator</title>
    <%--<link href="CSS/stylesheet.css" rel="stylesheet" type="text/css" />--%>

    <script type="text/javascript" language="javascript">
        function printWindow() {
            window.print();
        }
    </script>

    <script type="text/javascript" language="javascript">
        function CallPrint() {

            var prtContent = document.getElementById('print_div');
            prtContent.style.display = 'block';

            var WinPrint = window.open('', '', 'left=0,top=0,width=500,height=500,toolbar=0,scrollbars=0,status=0');
            WinPrint.document.write('<html> <head></head><body><div>');
            WinPrint.document.write(prtContent.innerHTML);
            //            alert(prtContent.innerHTML);
            WinPrint.document.write('</div></body></html>');
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
            prtContent.innerHTML = strOldOne;
            prtContent.style.display = 'none';

            //return;
        }
    </script>

</head>
<body style="background-image: url(''); font-size: 13px; font-family: Arial;">
    <form id="form1" runat="server">
    <table width="100%" align="center">
        <tr>
            <td align="right">
                <asp:ImageButton ID="btnPrint" runat="server" ImageUrl="~/Images/print_hvr.gif" onmouseout="this.src='Images/print_hvr.gif'"
                    onmouseover="this.src='Images/print.gif'" OnClick="btnPrint_Click" OnClientClick="javascript:CallPrint()" />
            </td>
        </tr>
    </table>
    <div id="print_div">
        <div style="font-size: 12px; border: solid 1px #acacac; padding: 10px;">
            <table align="center" width="95%" cellpadding="2" cellspacing="2">
                <tr>
                    <td align="left" valign="top" style="padding-bottom: 20px;">
                        <div class="logo">
                            <img src="Images/Private_fleet_logo1.png" alt="" /></div>
                    </td>
                </tr>
            </table>
            <table align="center" width="95%" cellpadding="2" cellspacing="2" style="border: solid 1px #acacac;
                padding: 10px 0;">
                <tr>
                    <td colspan="4">
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">
                        <asp:Label ID="lblDeparture" runat="server" Text="Departure :"></asp:Label>
                    </td>
                    <td style="width: 25%; padding-left: 13px;">
                        <asp:Label ID="lblDeparture_1" runat="server" Text=""></asp:Label>
                    </td>
                    <td style="width: 15%">
                        <asp:Label ID="lblArrival" runat="server" Text="Arrival :"></asp:Label>
                    </td>
                    <td style="width: 40%">
                        <asp:Label ID="lblArrival_1" runat="server" Text=""></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblCylinder" runat="server" Visible="false" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblPP" runat="server" Text="Approximate Vehicle Purchase price :"></asp:Label>
                    </td>
                    <td valign="top">
                        $<asp:Label ID="lblPP_1" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
            <table align="center" width="95%" cellpadding="2" cellspacing="2" style="border: solid 1px #acacac;">
                <tr style="background-color: #0A73A2; border: solid 1px #acacac; height: 30px; color: White;
                    font-weight: bold;">
                    <td align="center" style="width: 50%;">
                        <asp:Label ID="Label1" runat="server" Text="Description"></asp:Label>
                    </td>
                    <td align="center" style="width: 50%;">
                        <asp:Label ID="Label2" runat="server" Text="Cost"></asp:Label>
                    </td>
                </tr>
                <tr style="height: 25px;">
                    <td style="padding-left: 15px;">
                        <asp:Label ID="lblfCharges" runat="server" Text="Freight Charges -"></asp:Label>
                    </td>
                    <td style="padding-left: 15px;">
                        <asp:Label ID="lblfCharges_ans" runat="server" Text="Freight Charges"></asp:Label>
                    </td>
                </tr>
                <tr style="height: 25px; background-color: #C2DBE7;">
                    <td style="padding-left: 15px;">
                        <asp:Label ID="lblHandlingFees" runat="server" Text="Handling Fees -"></asp:Label>
                    </td>
                    <td style="padding-left: 15px;">
                        <asp:Label ID="lblHandlingFees_ans" runat="server" Text="Handling Fees"></asp:Label>
                    </td>
                </tr>
                <tr style="height: 25px;">
                <td style="padding-left: 15px;">
                    <asp:Label ID="lblRegoCTP" runat="server" Text="Rego/CTP -"></asp:Label>
                </td>
                <td style="padding-left: 15px;">
                    <asp:Label ID="lblRegoCTP_ans" runat="server" Text="Rego/CTP"></asp:Label>
                </td>
            </tr>
            <tr style="height: 25px; background-color: #C2DBE7;">
                <td style="padding-left: 15px;">
                    <asp:Label ID="lblStampDuty" runat="server" Text="Stamp Duty -"></asp:Label>
                </td>
                <td style="padding-left: 15px;">
                    <asp:Label ID="lblStampDuty_ans" runat="server" Text="Stamp Duty"></asp:Label>
                </td>
            </tr>
            <tr style="height: 25px; font-weight: bold;">
                <td style="padding-left: 15px;">
                    <asp:Label ID="lblTotal" runat="server" Text="Total -"></asp:Label>
                </td>
                <td style="padding-left: 15px;">
                    <asp:Label ID="lblTotal_ans" runat="server" Text="Total"></asp:Label>
                </td>
            </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
