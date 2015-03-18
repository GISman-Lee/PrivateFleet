<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCUserMasterView.ascx.cs"
    Inherits="User_Controls_UCUserMasterView" %>
<link href="../CSS/stylesheet.css" rel="stylesheet" type="text/css" />
<%@ Register Src="~/User Controls/UCUserMasterCRUD.ascx" TagName="UCUserMasterCRUD" TagPrefix="uc1" %>
<table width="95%" align="center">
    <tr>
        <td>
            <uc1:UCUserMasterCRUD ID="UCUserMasterCRUD1" runat="server" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblResult" runat="server" CssClass="dbresult"></asp:Label>
        </td>
    </tr>
    <tr>
        <td align="right">
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
        <td>
            <asp:GridView ID="gvUserDetails" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                DataKeyNames="ID" OnRowCommand="gvUserDetails_RowCommand" OnRowDataBound="gvUserDetails_RowDataBound"
                OnRowEditing="gvUserDetails_RowEditing" OnRowUpdating="gvUserDetails_RowUpdating"
               OnPageIndexChanging="gvUserDetails_PageIndexChanging" BackColor="White"
                BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" AllowSorting="True"
                OnSorting="gv_Sorting" Width="100%">
                <Columns>
                    <asp:TemplateField HeaderText="Name"  SortExpression="Name">
                        <EditItemTemplate>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <table border="0">
                                <tr>
                                    <td valign="top" style="padding-top: 5px; width: 15px;">
                                        <asp:Image ID="imgActive" runat="server" Height="10px" ImageUrl="~/Images/active_bullate.jpg"
                                            Width="10px" />
                                    </td>
                                    <td valign="top">
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Username" HeaderText="User Name" />
                    <asp:TemplateField HeaderText="Password" >
                        <EditItemTemplate>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <table border="0">
                                <tr>
                                    <td valign="top" style="width:85%">
                                        <asp:Label ID="lblPassword1"  runat="server" Text="********"></asp:Label>
                                        <asp:Label ID="lblPassword"  Visible="false" runat="server" Text='<%# Bind("Password") %>'></asp:Label>
                                    </td>
                                    <td valign="top" style=" width: 15px;" align="right">
                                       <asp:ImageButton ID="imgbtnSeePass"  runat="server" CausesValidation="False" CommandName="SeePassword"
                                        ImageUrl="~/Images/edit.png" ToolTip="Click To See Password" CommandArgument="<%# Container.DataItemIndex %>" />
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Email" HeaderText="Email" />
                    <asp:BoundField DataField="Address" HeaderText="Address" />
                    <asp:BoundField DataField="Phone" HeaderText="Phone" />
                    <%--<asp:BoundField DataField="ExpriryDate" HeaderText="ExpriryDate" />--%>
                    <asp:BoundField DataField="Role" HeaderText="Role" />
                    <asp:TemplateField HeaderText="Active">
                        <ItemTemplate>
                            <asp:Image ID="imgbtnActivate" runat="server" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <EditItemTemplate>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkbtnActiveness" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                CommandName="Activeness" Style="padding-left: 10px" CssClass="activatelink">Activate</asp:LinkButton>&nbsp;
                            <asp:HiddenField ID="hdfIsActive" runat="server" Value='<%# Bind("IsActive") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <EditItemTemplate>
                            <asp:ImageButton ID="imgbtnUpdate" runat="server" CommandName="Update" ImageUrl="~/Images/update.png"
                                OnClientClick="" ToolTip="Update This Record" ValidationGroup="EditGroup" />
                            <asp:ImageButton ID="imgbtnCancel" runat="server" CausesValidation="False" CommandName="Update"
                                ImageUrl="~/Images/cancel.png" OnClick="imgbtnCancel_Click" ToolTip="Cancel" />
                        </EditItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:ImageButton ID="imgbtnEdit" runat="server" CausesValidation="False" CommandName="Edit"
                                ImageUrl="~/Images/edit.png" ToolTip="Edit This Record" CommandArgument="<%# Container.DataItemIndex %>" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle CssClass="gvFooterrow" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <PagerStyle CssClass="pgr" />
                <HeaderStyle BackColor="#0A73A2" Font-Bold="True" CssClass="gvHeader" Height="30px" />
            </asp:GridView>
        </td>
    </tr>
</table>

