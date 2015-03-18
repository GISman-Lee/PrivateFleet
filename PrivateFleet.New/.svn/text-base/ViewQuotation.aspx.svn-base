<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ViewQuotation.aspx.cs" Inherits="ViewQuotation" Title="View Quotation Details" %>

<%@ Register Src="User Controls/Quotation/ucQuotationHeader.ascx" TagName="ucQuotationHeader"
    TagPrefix="uc1" %>
<%@ Register Src="User Controls/Request/ucRequestHeader.ascx" TagName="ucRequestHeader"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table style="width: 100%" align="center">
        <tr>
            <td align="right" style="width: 100%">
                <asp:ImageButton ID="btnPrint" runat="server" ImageUrl="~/Images/print_hvr.gif" onmouseout="this.src='Images/print_hvr.gif'"
                    onmouseover="this.src='Images/print.gif'" OnClick="btnPrint_Click" />
                <asp:ImageButton ID="btnBack" runat="server" ImageUrl="~/Images/back.gif" OnClick="btnBack_Click"
                    onmouseout="this.src='Images/back.gif'" onmouseover="this.src='Images/back_hvr.gif'" />
            </td>
        </tr>
        <tr>
            <%--<td style="width:100%; padding-right:0px">
                 <uc2:ucrequestheader ID="UcRequestHeader1" runat="server" />
             </td>--%>
            <td>
                <table width="100%">
                    <tr>
                        <td width="100%" style="padding-top: 20px">
                            <asp:DataList ID="DataList1" runat="server" RepeatDirection="Horizontal" GridLines="Both"
                                Width="100%" BorderStyle="solid" BorderColor="#acacac" BorderWidth="1px">
                                <ItemTemplate>
                                    <table width="100%">
                                        <tr class="ucHeader">
                                            <td align="center" bgcolor="">
                                                <asp:Label ID="lblHeader" runat="server" CssClass="gvLabel" Text='<%# Bind("Header") %>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr style="">
                                            <td style="" align="center">
                                                <asp:Label ID="lblDetails" runat="server" CssClass="gvLabel" Text='<%# Bind("Details") %>'></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:DataList>
                        </td>
                    </tr>
                    <tr style="width: 100%; border: solid 1px #acacac" class="ucHeader">
                        <td align="left" bgcolor="">
                            <asp:Label ID="lblHeader" runat="server" CssClass="gvLabel" Text="">Request Parameter</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width="100%" style="" align="right">
                            <asp:DataList ID="DataList2" runat="server" RepeatDirection="Horizontal" GridLines="Both"
                                Width="85%" BorderStyle="solid" BorderColor="#acacac" BorderWidth="1px">
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
            <td style="width: 100%">
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
        <%-- <tr>
            <td align="right">
                <asp:ImageButton ID="btnUpdateQuote" runat="server" 
                    onclick="btnUpdateQuote_Click" ></asp:ImageButton>
            </td>
        </tr>--%>
        <tr>
            <td align="center" style="width: 100%">
                <asp:ImageButton ID="btnBack_Copy" runat="server" ImageUrl="~/Images/back.gif" OnClick="btnBack_Click"
                    onmouseout="this.src='Images/back.gif'" onmouseover="this.src='Images/back_hvr.gif'" />
            </td>
        </tr>
    </table>
</asp:Content>
