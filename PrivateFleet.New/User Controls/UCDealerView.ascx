<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCDealerView.ascx.cs"
    Inherits="User_Controls_UCDealerView" %>
<link href="../CSS/stylesheet.css" rel="stylesheet" type="text/css" />
<%@ Register Src="~/User Controls/UCDealerCRUD.ascx" TagName="UCDealerCRUD" TagPrefix="uc1" %>
<table width="95%" align="center">
    <tr>
        <td>
            <uc1:UCDealerCRUD ID="ctrlAddDealer" runat="server" />
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
            <asp:GridView ID="gvDealerDetails" runat="server" AutoGenerateColumns="False" OnRowCommand="gvDealerDetails_RowCommand"
                OnRowDataBound="gvDealerDetails_RowDataBound" OnRowEditing="gvDealerDetails_RowEditing"
                OnRowUpdating="gvDealerDetails_RowUpdating" Width="100%" DataKeyNames="ID" AllowPaging="True"
                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                CellPadding="3" OnPageIndexChanging="gvDealerDetails_PageIndexChanging" AllowSorting="True"
                OnSorting="gv_Sorting" PageSize="10" EmptyDataText="No dealer found">
                <Columns>
                    <asp:TemplateField HeaderText="Dealer" SortExpression="Name">
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

                    <asp:TemplateField HeaderText="Contact Info">
                        <ItemTemplate>
                            Company&nbsp;&nbsp:&nbsp;
                            <asp:Label ID="Company" runat="server" Font-Size="14px" Text='<%#Bind("Company") %>' EnableViewState="True"></asp:Label>
                            <br />
                            
                            Mobile&nbsp;&nbsp;&nbsp;:&nbsp;
                            <asp:Label ID="lblMobile" runat="server" Font-Size="14px" Text='<%#Bind("Mobile") %>' EnableViewState="True"></asp:Label>
                            <br />
                      
                            Phone&nbsp;&nbsp;&nbsp;:&nbsp;
                            <asp:Label ID="Phone" runat="server" Font-Size="14px" Text='<%#Bind("Phone") %>' EnableViewState="True"></asp:Label>
                            <%-- <asp:HiddenField ID="hdfIsHotDealer" runat="server" Value='<%# Eval("IsHotDealer") %>' />  --%>

                            <br />
                            
                            Address&nbsp;&nbsp;&nbsp;:&nbsp;
                            <asp:Label ID="lblAddress" runat="server" Font-Size="14px" Text='<%#Bind("Address") %>' EnableViewState="True"></asp:Label>
                            <br />

                            Post Code&nbsp;&nbsp;&nbsp;:&nbsp;
                            <asp:Label ID="lblPostCode" runat="server" Font-Size="14px" Text='<%#Bind("PCode") %>' EnableViewState="True"></asp:Label>
                            <br />

                            <a href='<%# "mailto:"+ Eval("Email") %>' style="color: Blue;">
                                <asp:Label ID="Email" runat="server" Text='<%#Bind("Email") %>'></asp:Label></a>
                            <br />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="Make" HeaderText="Make" />

                    <%--
                    <asp:BoundField DataField="Company" HeaderText="Company" SortExpression="Company" />
                    <asp:BoundField DataField="Email" HeaderText="Email" />
                    <asp:BoundField DataField="Fax" HeaderText="Fax" Visible="False" />
                    <asp:BoundField DataField="Phone" HeaderText="Phone" />
                    <asp:BoundField DataField="PCode" HeaderText="PCode" Visible="False" />
                    <asp:TemplateField HeaderText="State" Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblState" runat="server" Text='<%# Bind("State") %>'></asp:Label>
                            <asp:HiddenField ID="hdfStateID" runat="server" Value='<%# Bind("StateID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    --%>
                    <%--<asp:TemplateField HeaderText="City" SortExpression="City">
                                <ItemTemplate>
                                    <asp:Label ID="lblCity" runat="server" Text='<%# Bind("City") %>'></asp:Label><asp:HiddenField
                                        ID="hdfCityID" runat="server" Value='<%# Bind("CityID") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                    <%--<asp:TemplateField HeaderText="Location" SortExpression="Location">
                                <ItemTemplate>
                                    <asp:Label ID="lblLocation" runat="server" Text='<%# Bind("Location") %>'></asp:Label>
                                    <asp:HiddenField ID="hdfLocationID" runat="server" Value='<%# Bind("LocationID") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                    
                    <asp:TemplateField HeaderText="Is Hot ?" SortExpression="IsHotDealer" Visible="false">
                        <ItemTemplate>
                            <asp:Image ID="imgbtnIsHotDealer" runat="server" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

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
                        <FooterTemplate>
                            <table>
                                <tr>
                                    <td align="left" valign="middle">
                                        <asp:ImageButton ID="imgbtnAdd" runat="server" ImageUrl="~/Images/Save1.png" OnClick="imgbtnAdd_Click"
                                            ToolTip="Add Record" ValidationGroup="VGAdd" Style="padding-left: 10px" Height="15px"
                                            ImageAlign="Middle" Width="48px" />
                                    </td>
                                </tr>
                            </table>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:HiddenField ID="hdfID" runat="server" Value='<%# Bind("ID") %>' />
                            <asp:LinkButton ID="lnkbtnActiveness" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                CommandName="Activeness" Style="padding-left: 10px" CssClass="activatelink">Activate</asp:LinkButton>&nbsp;
                            <asp:HiddenField ID="hdfIsActive" runat="server" Value='<%# Bind("IsActive") %>' />
                            <asp:HiddenField ID="hdfIsHotDealer" runat="server" Value='<%# Bind("IsHotDealer") %>' />
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
                                ImageUrl="~/Images/edit.png" ToolTip="Edit This Record" CommandArgument='<%# Container.DataItemIndex %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle CssClass="gvFooterrow" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <PagerStyle CssClass="pgr" />
                <HeaderStyle BackColor="#0A73A2" Font-Bold="True" CssClass="gvHeader" Height="30px" />
            </asp:GridView>
            <asp:HiddenField ID="hdfBindData" runat="server" OnValueChanged="hdfBindData_ValueChanged"
                Value="0" />
        </td>
    </tr>
</table>
