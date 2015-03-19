<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DealerManager.aspx.cs" Inherits="DealerManager"  Title="Dealer Manager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <Table ID="DealerManagerTable"  width="95%" align="center">
    <tr>
        <td align="right">
            <asp:Label ID="lblRowsToDisplay" runat="server">Rows To Display</asp:Label>
            <asp:DropDownList ID="Records" runat="server" AutoPostBack="true" Width="50px" OnSelectedIndexChanged="Records_IndexChanged">
                <asp:ListItem Value="10">10</asp:ListItem>
                <asp:ListItem Value="20">20</asp:ListItem>
                <asp:ListItem Value="30">30</asp:ListItem>
                <asp:ListItem Value="50">50</asp:ListItem>
                <asp:ListItem Value="All">All</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
        <asp:GridView ID="GridDealer" runat="server" Width="100%" AllowPaging="True" 
        BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
        PageSize="10" DataKeyNames="ID" AllowSorting="True" OnPageIndexChanging="GridDealer_PageIndexChanged"
        AutoGenerateColumns="False" OnRowDataBound="GridDealer_RowDataBound">
        <Columns>
                   <asp:TemplateField HeaderText="Dealer">
                        <ItemTemplate>
                            <table border="0">
                                <tr>
                                    <td valign="top">
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Active">
                        <ItemTemplate>
                           <asp:Image ID="imgbtnActivate" runat="server" /> 
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:BoundField DataField="Company" HeaderText="Company" SortExpression="Company" />
                    <asp:BoundField DataField="Email" HeaderText="Email" Visible="true"/>
                    <asp:BoundField DataField="Fax" HeaderText="Fax" Visible="False" />
                    <asp:BoundField DataField="Phone" HeaderText="Phone" Visible=true/>
                    <asp:BoundField DataField="PCode" HeaderText="PCode" Visible="False" />
                    <asp:BoundField DataField="City" HeaderText="City" Visible="true" />


        </Columns>
                <FooterStyle CssClass="gvFooterrow" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <PagerStyle CssClass="pgr" />
                <HeaderStyle BackColor="#0A73A2" Font-Bold="True" CssClass="gvHeader" Height="30px" />
        </asp:GridView>
    </Table>
    

</asp:Content>