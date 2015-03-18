<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucRoleMaster.ascx.cs"
    Inherits="User_Controls_ucRoleMaster" %>

<script type="text/javascript">
//    function ConfirmUpdate() {
//        var Choice = confirm('Do you want to update this entry ?');
//        return Choice;
//    }
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
                    <asp:GridView ID="gvRoles" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                        DataKeyNames="ID" OnRowEditing="gvRoles_RowEditing" OnRowUpdating="gvRoles_RowUpdating"
                        OnRowCommand="gvRoles_RowCommand" OnRowDataBound="gvRoles_RowDataBound" AllowPaging="True"
                        OnPageIndexChanging="gvRoles_PageIndexChanging" BackColor="White" BorderColor="#CCCCCC"
                        BorderStyle="None" BorderWidth="1px" Width="100%" CellPadding="3" AllowSorting="True"
                        OnSorting="gv_Sorting" PageSize="10">
                        <Columns>
                            <asp:TemplateField HeaderText="Role" SortExpression="Role">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEditRole" runat="server" Text='<%#Bind("role") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvEditRole" runat="server" ErrorMessage="*" ControlToValidate="txtEditRole"></asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Image ID="imgActive" runat="server" Height="10px" ImageUrl="~/Images/active_bullate.jpg"
                                        Width="10px" />
                                    <asp:Label ID="lblRole" runat="server" Text='<%#Bind("role") %>' Style="padding-left: 10px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <table>
                                        <tr>
                                            <td align="left" valign="middle">
                                                <asp:Image ID="imgActive" runat="server" Height="25px" ImageUrl="~/Images/footerarrow.png"
                                                    Width="25px" />
                                            </td>
                                            <td align="left" valign="middle">
                                                <asp:TextBox ID="txtAddRole" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvAddRole" runat="server" ErrorMessage="*" ControlToValidate="txtAddRole"
                                                    ValidationGroup="VGAdd"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEditDesc" runat="server" Text='<%#Bind("description") %>' TextMode="MultiLine"
                                        Rows="2"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblDesc" runat="server" Text='<%#Bind("description") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtAddDesc" runat="server" TextMode="MultiLine"  Rows="2"></asp:TextBox>
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
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnEdit" runat="server" CausesValidation="False" CommandName="Edit"
                                        ImageUrl="~/Images/edit.png" ToolTip="Edit This Record" />
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
