<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucPageMaster.ascx.cs"
    Inherits="User_Controls_ucPageMaster" %>
<link href="../CSS/stylesheet.css" rel="stylesheet" type="text/css" />

<script type="text/javascript">
//        function ConfirmUpdate()
//        {
//           var Choice =confirm('Do you want to update this entry ?');
//           return Choice;
//        }
</script>

<asp:UpdatePanel ID="UP2" runat="server">
    <ContentTemplate>
        <table width="95%" align="center">
            <tr>
                <td style="height: 30px" valign="middle">
                    <asp:Label ID="lblMsg" runat="server" Text="" CssClass="dbresult"></asp:Label>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <asp:GridView ID="gvPages" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                        DataKeyNames="ID" OnRowEditing="gvPages_RowEditing" OnRowUpdating="gvPages_RowUpdating"
                        OnRowCommand="gvPages_RowCommand" OnRowDataBound="gvPages_RowDataBound" AllowPaging="True"
                        OnPageIndexChanging="gvPages_PageIndexChanging" BackColor="White" BorderColor="#CCCCCC"
                        BorderStyle="None" BorderWidth="1px" Width="100%" CellPadding="3" AllowSorting="True"
                        OnSorting="gv_Sorting">
                        <Columns>
                            <asp:TemplateField HeaderText="Page" SortExpression="PageName">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEditPage" runat="server" Text='<%#Bind("PageName") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvEditPage" runat="server" ErrorMessage="*" ControlToValidate="txtEditPage"></asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <table border="0">
                                        <tr>
                                            <td valign="top" style="padding-top: 5px; width: 15px;">
                                                <asp:Image ID="imgActive" runat="server" Height="10px" ImageUrl="~/Images/active_bullate.jpg"
                                                    Width="10px" /></td>
                                            <td valign="top">
                                                <asp:Label ID="lblPage" runat="server" Text='<%#Bind("PageName") %>'></asp:Label></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <table>
                                        <tr>
                                            <td align="left" valign="middle">
                                                <asp:Image ID="imgActive" runat="server" Height="25px" ImageUrl="~/Images/footerarrow.png"
                                                    Width="25px" /></td>
                                            <td align="left" valign="middle">
                                                <asp:TextBox ID="txtAddPage" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvAddPage" runat="server" ErrorMessage="*" ControlToValidate="txtAddPage"
                                                    ValidationGroup="VGAdd"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Page URL" SortExpression="PageUrl" Visible="false">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEditUrl" runat="server" Text='<%#Bind("PageUrl") %>' Style="width: 150px;"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblDesc" runat="server" Text='<%#Bind("PageUrl") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtAddUrl" runat="server" Style="width: 150px;"></asp:TextBox>
                                </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Parent Menu" SortExpression="ParentPageName">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlEditParent" runat="server">
                                    </asp:DropDownList>
                                    <asp:HiddenField ID="hdfParent" runat="server" Value='<%#Bind("ParentID") %>'></asp:HiddenField>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblParent" runat="server" Text='<%#Bind("ParentPageName") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddlAddParent" runat="server">
                                    </asp:DropDownList>
                                </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Is Internal Link">
                                <EditItemTemplate>
                                    <asp:CheckBox ID="chkEditInternal" runat="server" Checked='<%#Bind("IsInternalLink") %>' />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblInternal" runat="server" Text='<%#Bind("IsInternalLink") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:CheckBox ID="chkAddInternal" runat="server" Checked="false" />
                                </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Active">
                                <ItemTemplate>
                                    <asp:Image ID="imgbtnActivate" runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <FooterTemplate>
                                    <asp:ImageButton ID="imgbtnAdd" runat="server" ImageUrl="~/Images/Save1.png" OnClick="imgbtnAdd_Click"
                                        ToolTip="Add Record" ValidationGroup="VGAdd" Style="padding-left: 10px" Height="15px"
                                        ImageAlign="Middle" Width="48px" />
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkbtnActiveness" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                        CommandName="Activeness" Style="padding-left: 10px" CssClass="activatelink">Activate</asp:LinkButton>&nbsp;
                                    <asp:HiddenField ID="hdfIsActive" runat="server" Value='<%# Bind("IsActive") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <EditItemTemplate>
                                    <asp:ImageButton ID="imgbtnUpdate" runat="server" CommandName="Update" ImageUrl="~/Images/update.png"
                                        OnClientClick="" ToolTip="Update This Record" ValidationGroup="EditGroup" />
                                    <asp:ImageButton ID="imgbtnCancel" runat="server" CausesValidation="False" CommandName="Update"
                                        ImageUrl="~/Images/cancel.png" OnClick="imgbtnCancel_Click" ToolTip="Cancel" />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnEdit" runat="server" CausesValidation="False" CommandName="Edit"
                                        ImageUrl="~/Images/edit.png" ToolTip="Edit This Record" />
                                </ItemTemplate>
                                <ItemStyle Width="50px" HorizontalAlign="Center" />
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
