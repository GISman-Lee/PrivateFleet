<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintPageQuoteComparison.aspx.cs"
    Inherits="PrintPageQuoteComparison" %>

<%@ Register Src="User Controls/Request/ucRequestHeader.ascx" TagName="ucRequestHeader"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print Of Quote Comparison</title>

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
           
        }
    </script>

    <style type="text/css" media="print">
        body
        {
            margin: 0;
            font-family: Arial;
            font-size: 13px;
        }
    </style>
</head>
<body style="background-image: url(''); font-size: 13px; font-family: Arial;" onload="javascript:shrinkDiv();">
    <form id="form1" runat="server">
    <table width="100%">
        <tr align="right">
            <td align="right" style="width: 100%">
                <%--<asp:ImageButton ID="btnSendMail" runat="server" ImageUrl="~/Images/send_mail_hvr.gif"
                        onmouseout="this.src='Images/send_mail_hvr.gif'" onmouseover="this.src='Images/send_mail.gif'"
                        OnClick="btnSendMail_Click" />--%>
                <asp:ImageButton ID="btnPrint" runat="server" ImageUrl="~/Images/print_hvr.gif" onmouseout="this.src='Images/print_hvr.gif'"
                    onmouseover="this.src='Images/print.gif'" OnClick="btnPrint_Click" OnClientClick="javascript:CallPrint()" />
            </td>
        </tr>
    </table>
     <div id="leftSide" style="width:30%;">
        <a href="javascript:void(0);" onclick="toggleShrink(); return false;">X</a>
    </div>

    <div id="print_div" style="background-image: url(''); background-image: none;" >
        <table style="width: 100%; padding-top: 0px; border: solid 1px #000000;">
            <tr>
                <td align="left" valign="top" style="background-color: #F7F7F7;">
                    <div class="logo">
                        <img src="images/Private_fleet_logo.jpg" alt="" width="298" height="113" /></div>
                </td>
            </tr>
            <tr>
                <td style="width: 100%; padding: 0px;">
                    <uc1:ucRequestHeader ID="UcRequestHeader1" runat="server" />
                </td>
            </tr>
            <tr runat="server" id="trDealerInfo" visible="true">
                <td style="border: solid 1px #acacac;">
                    <table width="100%">
                        <tr style="color: White; background-color: #0A73A2; font-weight: bold; width: 100%;
                            border-style: solid; font-size: 14px; border: solid 1px #acacac;">
                            <td align="left">
                                <asp:Label ID="lblDealer" runat="server" Text="">Dealer</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%">
                                <asp:GridView GridLines="Both" ID="gvDealerInfo" runat="server" AllowPaging="false" AutoGenerateColumns="False"
                                    BackColor="White" BorderColor="#acacac" BorderStyle="Solid" BorderWidth="1px"
                                    CellPadding="3" PageSize="5" Width="100%" DataKeyNames="Reminder" OnRowDataBound="gvDealerInfo_RowDataBound">
                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle />
                                    <HeaderStyle BackColor="#eaeaea" ForeColor="Black" Font-Bold="True" Height="20px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Dealer Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDealerName" runat="server" Text='<%# Bind("[Dealer Name]") %>'>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Company">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCompany" runat="server" Text='<%# Bind("Company") %>'>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Contacts">
                                            <ItemTemplate>
                                                <%--<asp:Label ID="Email"  runat="server" Text='<%# Bind("Email") %>'>'></asp:Label>--%>
                                                Email : <a href='<%# "mailto:"+ Eval("Email") %>' style="color: Blue; text-decoration: underline;">
                                                    <asp:Label ID="Email" runat="server" Text='<%#Bind("Email") %>'></asp:Label></a>
                                                <br />
                                                Fax :
                                                <asp:Label ID="lblFax" runat="server" Text='<%#Bind("Fax") %>'></asp:Label>
                                                <br />
                                                Phone :
                                                <asp:Label ID="lblPhone" runat="server" Text='<%#Bind("Phone") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Reminder">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkRemind" runat="server" CommandArgument='<%# Bind("DealerID") %>'
                                                    CommandName="RemindDealer" CssClass="activeLink">
                                                    <asp:Literal ID="litlnk" Text="Quotation Pending <br /> Remind Dealer" runat="server"></asp:Literal>
                                                </asp:LinkButton>
                                                <asp:HiddenField runat="server" ID="LastRemindDateTime" Value='<% #Bind("LastRemindDateTime")%>' />
                                                <asp:HiddenField runat="server" ID="QoutationID" Value='<% #Bind("QoutaionExist")%>' />
                                                <asp:HiddenField runat="server" ID="LastDate" Value='<% #Bind("LastRemindDateTime")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="width: 100px; border:solid 1px black">
                    <asp:GridView ID="gvMakeDetails" runat="server" AutoGenerateColumns="False" BackColor="White"
                        BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" OnPageIndexChanging="gvMakeDetails_PageIndexChanging"
                        OnRowDataBound="gvMakeDetails_RowDataBound" PageSize="30" Width="100%" OnRowCreated="gvMakeDetails_RowCreated"
                        ShowFooter="false">
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <PagerStyle />
                        <HeaderStyle BackColor="#0A73A2" Font-Bold="True" Height="30px" />
                        <Columns>
                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                    <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("Description") %>'>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
