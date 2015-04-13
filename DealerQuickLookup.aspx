<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="DealerQuickLookup.aspx.cs" Inherits="DealerQuickLookup" Title="Dealer Quick Lookup" %>

<%@ Register Src="~/User Controls/UCSendEnquiry.ascx" TagName="UCSendEnquiry" TagPrefix="ucEnq" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">


        function EnableBulkEnq(chkBox) {



            if (document.getElementById('ctl00_ContentPlaceHolder1_btnSendBulkEnquiry').disabled == true) {
                var chkSelect = document.getElementById(chkBox);
                if (chkSelect.checked) {
                    document.getElementById('ctl00_ContentPlaceHolder1_btnSendBulkEnquiry').disabled = false;
                }
                else {
                    if (count == 0) {
                        document.getElementById('ctl00_ContentPlaceHolder1_btnSendBulkEnquiry').disabled = true;
                    }
                }
            }
        }




        var checked = false;

        function SelectAll() {

            e = document.forms[0].elements;

            //alert(e[i].type);

            for (i = 0; i < e.length; i++) {

                if (e[i].type == "checkbox") {

                    if (checked) {

                        e[i].checked = false;

                    }

                    else {

                        e[i].checked = true;
                    }
                }
            }

            if (checked == true) {

                checked = false
                document.getElementById('ctl00_ContentPlaceHolder1_btnSendBulkEnquiry').disabled = true;

            }

            else {

                checked = true;
                document.getElementById('ctl00_ContentPlaceHolder1_btnSendBulkEnquiry').disabled = false;
            }

        }
  
    </script>

    <asp:Panel ID="pnlQuick" runat="server">
        <table width="100%" align="center" cellpadding="1" cellspacing="3" border="0">
            <tr align="right">
                <td>
                    Make:
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlQuickMake" runat="server" Height="20px" Width="120px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvQuickMake" runat="server" ControlToValidate="ddlQuickMake"
                        ErrorMessage="Make Required" ValidationGroup="VGQuickReport" Display="None" InitialValue="-Select-">
                    </asp:RequiredFieldValidator>
                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
                        HighlightCssClass="validatorCalloutHighlight" TargetControlID="rfvQuickMake">
                    </ajaxToolkit:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr align="right">
                <td>
                    Postal Code:
                </td>
                <td align="left">
                    <asp:TextBox ID="txtQuickPCode" runat="server" Width="120px">
                    </asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvPostalCodeOnSearch" runat="server" ControlToValidate="txtQuickPCode"
                        Display="None" ErrorMessage="Postal Code  Required" ValidationGroup="VGQuickReport">
                    </asp:RequiredFieldValidator>
                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1111" runat="server"
                        HighlightCssClass="validatorCalloutHighlight" TargetControlID="rfvPostalCodeOnSearch">
                    </ajaxToolkit:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr>
                <td valign="top" colspan="2" style="text-align: center; padding-right: 20px;">
                    <asp:ImageButton ID="btnSearchDealers" runat="server" ImageUrl="~/Images/Search_dealer.gif"
                        onmouseout="this.src='Images/Search_dealer.gif'" onmouseover="this.src='Images/Search_dealer_hvr.gif'"
                        ValidationGroup="VGQuickReport" OnClick="btnSearchDealers_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Panel ID="pnlDealerDetails" runat="server" Visible="false">
                        <table visible="false" width="100%" style="border: 1px solid #4A4A4A" cellpadding="1"
                            cellspacing="2">
                            <tr>
                                <td valign="middle" colspan="2">
                                    <table width="100%" style="padding: 0px;">
                                        <tr align="center">
                                            <td align="center" width="10%">
                                            </td>
                                            <td class="datalistNormalDealer" width="30%" align="center" style="border-right: #000000 double;
                                                border-top: #000000 double; border-left: #000000 double; border-bottom: #000000 double">
                                                Statewide Dealers
                                            </td>
                                            <td align="center" width="10%">
                                            </td>
                                            <%--  <td width="25%" class="datalistHotDealer" align="center" style="border-right: #000000 double;
                                                border-top: #000000 double; border-left: #000000 double; border-bottom: #000000 double;
                                                background: #799C60 url(../images/hot_deale_btn1.png) no-repeat left bottom;">
                                                Hot Dealers
                                            </td>--%>
                                            <td align="center" width="10%">
                                            </td>
                                            <td width="30%" style="border-right: #000000 double; border-top: #000000 double;
                                                border-left: #000000 double; border-bottom: #000000 double" class="datalistOutSideDealer">
                                                Interstate Dealers
                                            </td>
                                            <td align="center" width="10%">
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    <asp:Label ID="lblRowsToDisplay" runat="server">Rows To Display</asp:Label>
                                    <asp:DropDownList ID="ddl_NoRecords" runat="server" AutoPostBack="true" Width="50px"
                                        OnSelectedIndexChanged="ddl_NoRecords_SelectedIndexChanged">
                                        <asp:ListItem Value="10">10</asp:ListItem>
                                        <asp:ListItem Value="20">20</asp:ListItem>
                                        <asp:ListItem Value="30">30</asp:ListItem>
                                        <asp:ListItem Value="50">50</asp:ListItem>
                                        <asp:ListItem Value="All">All</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="dealerHeading" colspan="2">
                                    <asp:GridView ID="gvDealerDetails" runat="server" AutoGenerateColumns="False" Width="100%"
                                        DataKeyNames="ID" AllowPaging="True" PageSize="10" BackColor="White" BorderColor="#CCCCCC"
                                        BorderStyle="None" BorderWidth="1px" CellPadding="3" AllowSorting="True" OnRowDataBound="gvDealerDetails_RowDataBound"
                                        EmptyDataText="No dealer found." OnPageIndexChanging="gvDealerDetails_PageIndexChanging"
                                        OnSorting="gvDealerDetails_Sorting">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Name" SortExpression="Name">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hfDealerID" runat="server" Value='<%# Eval("ID") %>' />
                                                    <asp:Label ID="lblName" runat="server" Text='<%#Bind("Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- <asp:BoundField DataField="Name" HeaderText="Dealer" SortExpression="Name" />--%>
                                            <asp:TemplateField HeaderText="Contact Info">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCompany" runat="server" Text='<%#Bind("Company") %>'></asp:Label>
                                                    <br />
                                                    <asp:Label ID="lblEmail" Visible="false" runat="server" Text='<%#Bind("Email") %>'></asp:Label>
                                                    <a href='<%# "mailto:"+ Eval("Email") %>' style="color: Blue; text-decoration: underline;">
                                                        <asp:Label ID="Label1" runat="server" Text='<%#Bind("Email") %>'></asp:Label></a>
                                                    <br />
                                                    Mobile&nbsp;&nbsp;&nbsp;:&nbsp;
                                                    <asp:Label ID="lblFax" runat="server" Font-Size="14px" Text='<%#Bind("Mobile") %>'></asp:Label>
                                                    <br />
                                                    Phone&nbsp;&nbsp;&nbsp;:&nbsp;
                                                    <asp:Label ID="lblPhone" runat="server" Font-Size="14px" Text='<%#Bind("Phone") %>'></asp:Label>
                                                    <asp:HiddenField ID="hdfIsHotDealer" runat="server" Value='<%# Eval("IsHotDealer") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="TotalPoints" HeaderText="Points" SortExpression="TotalPoints1" />
                                            <asp:BoundField DataField="kmsState" HeaderText="Distance(In Kms)" SortExpression="kms" />
                                            <asp:TemplateField HeaderText="Rating" SortExpression="Rating">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdfTot" runat="server" Value='<% #Bind("Total") %>' />
                                                    <asp:Label ID="lblRating" runat="server" Text='<%#Bind("Rating")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Select">
                                                <HeaderTemplate>
                                                    <asp:CheckBox runat="server" ID="chkSelectAll" onclick="javascript:SelectAll();" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox runat="server" ID="chkSelect" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnSelect" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/Images/Select_dealer.gif"
                                                        CommandName="SelectDealer" onmouseout="this.src='Images/Select_dealer.gif'" onmouseover="this.src='Images/Select_dealer_hvr.gif'" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- <asp:TemplateField HeaderText="FinalPoints" SortExpression="FinalPoints">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFinalPoints" runat="server" Text='<%#Bind("FinalPoints") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                        </Columns>
                                        <RowStyle Height="80"/>
                                        <FooterStyle CssClass="gvFooterrow" />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle CssClass="pgr" />
                                        <HeaderStyle BackColor="#0A73A2" Font-Bold="True" CssClass="gvHeader" Height="30px" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:ImageButton ID="btnSendBulkEnquiry" runat="server" Text="Send Bulk Enquiry"
                                        OnClick="btnSendBulkEnquiry_Click" ImageUrl="~/Images/Send_Bulk_Order_hover.gif"
                                        onmouseout="this.src='Images/Send_Bulk_Order_hover.gif'" onmouseover="this.src='Images/Send_Bulk_Order.gif'" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <div id="divpopID" runat="server" visible="false">
        <div id="progressBackgroundFilter">
        </div>
        <div id="processMessage1">
            <ucEnq:UCSendEnquiry ID="UCEnquiry" runat="server" />
        </div>
    </div>
</asp:Content>
