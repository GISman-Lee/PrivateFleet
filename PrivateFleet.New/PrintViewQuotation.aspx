<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintViewQuotation.aspx.cs"
    Inherits="PrintViewQuotation" %>

<%@ Register Src="User Controls/Quotation/ucQuotationHeader.ascx" TagName="ucQuotationHeader"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print of Quotation</title>
    <link href="CSS/stylesheet.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript">
        function CallPrint() {

            var prtContent = document.getElementById('print_div');
            prtContent.style.display = 'block';

            var WinPrint = window.open('', '', 'left=0,top=0,width=800,height=800,toolbar=0,scrollbars=1,status=0');
            WinPrint.document.write('<html> <head> </head><body><div style="font-size:13px;">');
            WinPrint.document.write(prtContent.innerHTML);
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

    <style type="text/css" media="print">
        body
        {
            margin: 0;
            font-family: Arial;
            font-size: 13px;
        }
        #PRINT, #CLOSE
        {
            visibility: hidden;
        }
    </style>
</head>
<body style="background-image: url('')">
    <form id="form1" style="background-image: url(''); font-size: 13px; font-family: Arial;"
    runat="server">
    <div>
        <table width="100%">
            <tr align="right">
                <td align="right" style="width: 100%">
                    <asp:ImageButton ID="btnPrint" runat="server" ImageUrl="~/Images/print_hvr.gif" onmouseout="this.src='Images/print_hvr.gif'"
                        onmouseover="this.src='Images/print.gif'" OnClick="btnPrint_Click" OnClientClick="javascript:CallPrint()" />
                </td>
            </tr>
        </table>
        <div id="print_div" style="background-image: url(''); background-image: none; background-color: none;">
            <table style="width: 100%; padding-top: 0px; border: solid 1px #000000;">
                <tr>
                    <td align="left" valign="top" style="background-color: #F7F7F7;">
                        <div class="logo">
                            <img src="images/Private_fleet_logo.jpg" alt="" width="298" height="113" /></div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table width="100%">
                            <tr>
                                <td colspan="2" width="100%">
                                    <asp:DataList ID="DataList1" runat="server" RepeatDirection="Horizontal" GridLines="Both"
                                        Width="100%">
                                        <ItemTemplate>
                                            <table width="100%">
                                                <tr style="color: White; background-color: #0A73A2; font-weight: bold; font-size: 14px;">
                                                    <td align="center">
                                                        <asp:Label ID="lblHeader" runat="server" CssClass="gvLabel" Text='<%# Bind("Header") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr style="border-top: solid 1px #aacac">
                                                    <td style="" align="center">
                                                        <asp:Label ID="lblDetails" runat="server" CssClass="gvLabel" Text='<%# Bind("Details") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </td>
                            </tr>
                            <tr style="color: White; background-color: #0A73A2; font-weight: bold; width: 100%;
                                font-size: 14px;">
                                <td colspan="2" align="left">
                                    <asp:Label ID="lblHeader" runat="server" CssClass="gvLabel" Text="">Request Parameter</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" width="100%" style="" align="left">
                                    <asp:DataList ID="DataList2" runat="server" RepeatDirection="Horizontal" GridLines="Both"
                                        Width="100%">
                                        <ItemTemplate>
                                            <table width="100%">
                                                <tr>
                                                    <td align="left" style="width: 50%; background-color: #eaeaea; font-weight: bold;">
                                                        <asp:Label ID="lblHeader1" runat="server" CssClass="gvLabel" Text='<%# Bind("Header") %>'></asp:Label>
                                                    </td>
                                                    <td style="padding-left: 10px" align="left">
                                                        <asp:Label ID="Label1" runat="server" CssClass="gvLabel" Text='<%# Bind("Details") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:DataList>
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
                                                <asp:Label ID="lblSub1" runat="server" Width="200px"> </asp:Label>
                                            </td>
                                            <td style="width: 20%">
                                                <asp:Label ID="lblPCode" runat="server" Width="85px"><strong>Postal Code :  </strong> </asp:Label>
                                            </td>
                                            <td style="width: 30%" valign="top">
                                                <asp:Label ID="lblPCode1" runat="server" Width="200px"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%">
                        <uc1:ucQuotationHeader ID="UcQuotationHeader1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%; border: solid 1px #000000">
                        <asp:GridView ID="gvMakeDetails" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                            CellPadding="3" OnPageIndexChanging="gvMakeDetails_PageIndexChanging" OnRowDataBound="gvMakeDetails_RowDataBound"
                            PageSize="100" Width="100%" OnRowCreated="gvMakeDetails_RowCreated">
                            <FooterStyle CssClass="gvFooterrow" />
                            <Columns>
                                <asp:TemplateField HeaderText="Description">
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Image ID="imgActive" runat="server" Height="10px" ImageUrl="~/Images/active_bullate.jpg"
                                            Width="10px" Visible="False" />
                                        <asp:Label ID="lblMake" runat="server" CssClass="gvLabel" Text='<%# Bind("Key") %>'
                                            Style="padding-left: 25px"></asp:Label><br />
                                        <div style="margin-left: 25px;">
                                            <asp:Label ID="lblSpecification" runat="server" CssClass="gvLabel" Text='<%# Bind("Specification") %>'></asp:Label>
                                        </div>
                                        <asp:HiddenField ID="hdfID" runat="server" Value='<%# Bind("ID") %>' />
                                        <asp:HiddenField ID="hdfIsAccessory" runat="server" Value='<%# Bind("IsAccessory") %>' />
                                        <asp:HiddenField ID="hdfIsChargeType" runat="server" Value='<%# Bind("IsChargeType") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <PagerStyle CssClass="pgr" />
                            <HeaderStyle BackColor="#0A73A2" CssClass="gvHeader" Font-Bold="True" Height="30px" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
