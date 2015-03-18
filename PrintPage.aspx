<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintPage.aspx.cs" Inherits="PrintPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="User Controls/Quotation/ucShortlistedQuotation.ascx" TagName="ucShortlistedQuotation"
    TagPrefix="uc1" %>
<%@ Register Src="User Controls/Request/ucRequestHeader.ascx" TagName="ucRequestHeader"
    TagPrefix="uc2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print of Shortlisted Quotation</title>
    <%--<link href="CSS/stylesheet.css" rel="stylesheet" type="text/css" />--%>

    <script type="text/javascript" language="javascript">
        function printWindow() {
            window.print();
        }
    </script>

    <script type="text/javascript" language="javascript">
        function CallPrint() {

            var prtContent = document.getElementById('print_div');
            //alert(prtContent.innerHTML);
            var printData = prtContent.innerHTML
            prtContent.style.display = 'block';

            var WinPrint = window.open('', '', 'left=0,top=0,width=500,height=500,toolbar=0,scrollbars=0,status=0');
            WinPrint.document.write("<html xmlns='http://www.w3.org/1999/xhtml'> <head><style>td,th{font-size: 11px; font-family: Arial;}</style></head><body ><div >");
            WinPrint.document.write(printData);
            WinPrint.document.write('</div></body></html>');

            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
            //   prtContent.innerHTML = strOldOne;
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
                <asp:ImageButton ID="btnSendMail" Visible="true" runat="server" ImageUrl="~/Images/send_mail_hvr.gif"
                    onmouseout="this.src='Images/send_mail_hvr.gif'" onmouseover="this.src='Images/send_mail.gif'"
                    OnClick="btnSendMail_Click" />
                <asp:ImageButton ID="btnPrint" runat="server" ImageUrl="~/Images/print_hvr.gif" onmouseout="this.src='Images/print_hvr.gif'"
                    onmouseover="this.src='Images/print.gif'" OnClick="btnPrint_Click" OnClientClick="javascript:CallPrint()" />
            </td>
        </tr>
    </table>
    <div id="print_div">
        <div style="font-size: 9px !important;">
            <table width="100%" style="border: solid 1px black; padding: 0px;">
                <tr>
                    <td align="left" valign="top">
                        <div class="logo">
                            <img src="Images/Private_fleet_logo1.png" alt="" width="298" height="45px" /></div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <uc2:ucRequestHeader ID="UcRequestHeader1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="border-style: solid; font-family: Arial; border-color: #acacac; border-width: 1px;
                        width: 100%">
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:Label ID="lblSub" runat="server" Width="85px"><strong>Suburb : </strong> </asp:Label>
                                </td>
                                <td>
                                    <%-- <a id="map" href="#" runat="server" style="text-decoration: none;">--%>
                                    <asp:Label ID="lblSub1" runat="server" Width="200px"></asp:Label><%--</a>--%>
                                </td>
                                <td style="width: 20%">
                                    <asp:Label ID="lblPCode" runat="server" Width="85px"><strong>Postal Code :  </strong> </asp:Label>
                                </td>
                                <td style="width: 30%">
                                    <asp:Label ID="lblPCode1" runat="server" Width="200px"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <uc1:ucShortlistedQuotation ID="UcShortlistedQuotation1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="border: solid 1px #acacac; font-size: 11px; padding: 0px;">
                        <asp:GridView ID="gvMakeDetails" runat="server" AutoGenerateColumns="False" BackColor="White"
                            BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="1" OnRowDataBound="gvMakeDetails_RowDataBound"
                            PageSize="20" Width="100%">
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <PagerStyle CssClass="" />
                            <HeaderStyle CssClass="" Font-Bold="True" />
                            <Columns>
                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("Description") %>'>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quote Value">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQuoteValue" runat="server" Text='<%# Bind("QuoteValue") %>'>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
