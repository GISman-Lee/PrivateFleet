<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ViewQuotationReport.aspx.cs" Inherits="ViewQuotationReport" Title="Untitled Page" %>
<%@ Register Src="User Controls/Quotation/ucShortlistedQuotation.ascx" TagName="ucShortlistedQuotation"
    TagPrefix="uc1" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="95%" align="center">
        <tr>
            <td align="right">
                <asp:ImageButton ID="btnBack" runat="server" ImageUrl="~/Images/back.gif" onmouseout="this.src='Images/back.gif'"
                    onmouseover="this.src='Images/back_hvr.gif'" OnClick="btnBack_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <uc1:ucshortlistedquotation id="UcShortlistedQuotation1" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvMakeDetails" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    BackColor="White" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"
                    CellPadding="3" OnRowDataBound="gvMakeDetails_RowDataBound" PageSize="20" Width="100%">
                    <selectedrowstyle backcolor="#669999" font-bold="True" forecolor="White" />
                    <pagerstyle cssclass="pgr" />
                    <headerstyle backcolor="#0A73A2" cssclass="gvHeader" font-bold="True" height="30px" />
                    <columns>
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
                    </columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
