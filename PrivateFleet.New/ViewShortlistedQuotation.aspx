<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ViewShortlistedQuotation.aspx.cs" Inherits="ViewShortlistedQuotation"
    Title="View Shortlisted Quotation" %>

<%@ Register Src="User Controls/Quotation/ucShortlistedQuotation.ascx" TagName="ucShortlistedQuotation"
    TagPrefix="uc1" %>
<%@ Register Src="User Controls/Request/ucRequestHeader.ascx" TagName="ucRequestHeader"
    TagPrefix="uc1" %>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--  <script type="text/javascript" language="javascript">
    function showWindow(int intRequestId)
    {
      window.showModalDialog('PrintPage.aspx?ReqID='+ intRequestId,'name','dialogLeft:200px;dialogTop:100px;dialogWidth:600px;dialogHeight:400px;center:yes');
      
    }
    </script>--%>
    <table style="width: 95%" align="center">
        <tr>
            <td align="right" style="width: 50%; padding-bottom: 2px; height: 19px;">
            </td>
            <td align="right" style="padding-bottom: 2px; height: 19px;">
                <asp:ImageButton ID="btnPrint" runat="server" ImageUrl="~/Images/print_hvr.gif"
                    onmouseout="this.src='Images/print_hvr.gif'" onmouseover="this.src='Images/print.gif'"
                    OnClick="btnPrint_Click" />
                &nbsp;
                <asp:ImageButton ID="btnBack" runat="server" ImageUrl="~/Images/back.gif" onmouseout="this.src='Images/back.gif'"
                    onmouseover="this.src='Images/back_hvr.gif'" OnClick="btnBack_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <uc1:ucRequestHeader ID="UcRequestHeader1" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="border-style: solid; font-family: Arial; border-color: #acacac;
                border-width: 1px; width: 100%">
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
        <tr style="width: 200px;">
            <td colspan="2">
                <uc1:ucShortlistedQuotation ID="UcShortlistedQuotation1" runat="server" />
            </td>
        </tr>
        <tr>
            <td style="width: 110%" colspan="2">
                <asp:GridView ID="gvMakeDetails" runat="server" AutoGenerateColumns="False" BackColor="White"
                    BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" OnRowDataBound="gvMakeDetails_RowDataBound"
                    PageSize="30" Width="100%">
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <PagerStyle CssClass="pgr" />
                    <HeaderStyle BackColor="#0A73A2" CssClass="gvHeader" Font-Bold="True" Height="30px" />
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
        <tr>
            <td colspan="2" align="left" style="padding-bottom: 2px; height: 19px;">
                <asp:ImageButton ID="btnBack_Copy" runat="server" ImageUrl="~/Images/back.gif" onmouseout="this.src='Images/back.gif'"
                    onmouseover="this.src='Images/back_hvr.gif'" OnClick="btnBack_Click" />
            </td>

            <td>
                <asp:Button ID="Button1" runat="server" Text="Create Contract" OnClick = "CreateContract" style="height: 19px"/>
            </td>
        </tr>
    </table>
</asp:Content>
