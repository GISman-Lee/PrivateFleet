<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCCustomerView.ascx.cs"
    Inherits="User_Controls_UCCustomerView" %>
<%@ Register Src="UCCustomerCRUD.ascx" TagName="UCCustomerCRUD" TagPrefix="uc2" %>
<link href="../CSS/stylesheet.css" rel="stylesheet" type="text/css" />
<%@ Register Src="~/User Controls/UCDealerCRUD.ascx" TagName="UCDealerCRUD" TagPrefix="uc1" %>
<asp:UpdatePanel ID="UP2" runat="server">
    <ContentTemplate>
        <table width="95%" align="center">
            <tr>
                <td>
                    &nbsp;<uc2:UCCustomerCRUD ID="UCCustomerCRUD1" runat="server"></uc2:UCCustomerCRUD>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblResult" runat="server" CssClass="dbresult"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvCustomerDetails" runat="server" AutoGenerateColumns="False" OnRowCommand="gvCustomerDetails_RowCommand"
                        OnRowDataBound="gvCustomerDetails_RowDataBound" OnRowEditing="gvCustomerDetails_RowEditing"
                        OnRowUpdating="gvCustomerDetails_RowUpdating" Width="100%" DataKeyNames="ID"
                        AllowPaging="True" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None"
                        BorderWidth="1px" CellPadding="3" OnPageIndexChanging="gvCustomerDetails_PageIndexChanging"
                        AllowSorting="True" OnSorting="gv_Sorting">
                        <Columns>
                            <asp:TemplateField HeaderText="Name" SortExpression="Name">
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
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="Email" HeaderText="Email">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Location" HeaderText="Location" SortExpression="Location">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Address" HeaderText="Address" Visible="False">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PhoneNo" HeaderText="Phone">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PostalCode" HeaderText="Postal Code">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
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
                                <ItemStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdfID" runat="server" Value='<%# Bind("ID") %>' />
                                    <asp:LinkButton ID="lnkbtnActiveness" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                        CommandName="Activeness" Style="padding-left: 10px" CssClass="activatelink">Activate</asp:LinkButton>&nbsp;
                                    <asp:HiddenField ID="hdfIsActive" runat="server" Value='<%# Bind("IsActive") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
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
                    <%-- <asp:HiddenField ID="hdfBindData" runat="server" OnValueChanged="hdfBindData_ValueChanged"
                Value="0" />--%>
                </td>
            </tr>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress ID="upProcess" runat="server" DisplayAfter="500">
    <ProgressTemplate>
        <div id="progressBackgroundFilter">
        </div>
        <div id="processMessage">
            <span style="text-align: center;">
                <img src="../images/loading.gif" /><br />
                Loading...Please wait...</span></div>
    </ProgressTemplate>
</asp:UpdateProgress>
