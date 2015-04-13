<%@ Page Title="Survey Send Report" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="SurveySendReport.aspx.cs" Inherits="SurveySendReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="96%" cellpadding="2" cellspacing="2">
        <tr>
            <td align="right">
                <asp:Label ID="lblRowsToDisplay" runat="server" Visible="false">Rows To Display</asp:Label>
                <asp:DropDownList ID="ddl_NoRecords" runat="server" AutoPostBack="true" Visible="false"
                    Width="50px" OnSelectedIndexChanged="ddl_NoRecords_SelectedIndexChanged">
                    <asp:ListItem Value="10">10</asp:ListItem>
                    <asp:ListItem Value="20">20</asp:ListItem>
                    <asp:ListItem Value="30">30</asp:ListItem>
                    <asp:ListItem Value="50">50</asp:ListItem>
                    <asp:ListItem Value="All">All</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvSurveySendR" AllowPaging="true" AutoGenerateColumns="false" runat="server"
                    DataKeyNames="ID" Width="100%" PageSize="10" BackColor="White" BorderColor="#CCCCCC"
                    BorderStyle="None" BorderWidth="1px" CellPadding="3" AllowSorting="True" OnPageIndexChanging="gvSurveySendR_PageIndexChanging"
                    OnSorting="gvSurveySendR_Sorting" EmptyDataText="No Records Found">
                    <Columns>
                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                        <asp:BoundField DataField="Make" HeaderText="Make" SortExpression="Make" />
                        <asp:BoundField DataField="Model" HeaderText="Model" SortExpression="Model" />
                        <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                        <asp:BoundField DataField="State" HeaderText="State" SortExpression="State" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="Mobile" HeaderText="Mobile" SortExpression="Mobile" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle Width="80px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DeliveryDate1" HeaderText="Delivery Date" SortExpression="DeliveryDate"
                            ItemStyle-HorizontalAlign="Center">
                        <itemstyle width="60px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="EmailStatus" HeaderText="Email Status" SortExpression="EmailStatus"
                            ItemStyle-HorizontalAlign="Center" />
                    </Columns>
                    <FooterStyle CssClass="gvFooterrow" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <PagerStyle CssClass="pgr" />
                    <HeaderStyle BackColor="#0A73A2" Font-Bold="True" HorizontalAlign="Center" CssClass="gvHeader"
                        Height="30px" />
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
