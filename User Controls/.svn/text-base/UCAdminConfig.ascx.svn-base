<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCAdminConfig.ascx.cs"
    Inherits="User_Controls_UCAdminConfig" %>
<link href="../CSS/stylesheet.css" rel="stylesheet" type="text/css" />

<script type="text/javascript">
    //    function ConfirmUpdate() {
    //        var Choice = confirm('Do you want to update this entry ?');
    //        return Choice;
    //    }
</script>

<asp:UpdatePanel ID="UP2" runat="server">
    <ContentTemplate>
        <asp:Panel ID="pnlConfigValue" runat="server" DefaultButton="lnkTemp">
            <table width="95%" align="center">
                <tr>
                    <td style="height: 30px" valign="middle">
                        <asp:Label ID="lblResult" runat="server" CssClass="dbresult"></asp:Label>
                        <asp:LinkButton ID="lnkTemp" runat="server" Text=""></asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvConfigValuesDetails" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            OnRowCommand="gvConfigValuesDetails_RowCommand" OnRowDataBound="gvConfigValuesDetails_RowDataBound"
                            OnRowEditing="gvConfigValuesDetails_RowEditing" OnRowUpdating="gvConfigValuesDetails_RowUpdating"
                            Width="100%" OnPageIndexChanging="gvConfigValuesDetails_PageIndexChanging" BackColor="White"
                            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" AllowSorting="True"
                            OnSorting="gv_Sorting" PageSize="15">
                            <Columns>
                                <asp:TemplateField HeaderText="Key" SortExpression="Description">
                                    <EditItemTemplate>
                                        <asp:Label ID="lblEditKey" runat="server" Style="padding-left: 10px" Text='<%# Bind("Description") %>'
                                            CssClass="gvLabel"></asp:Label><br />
                                        <asp:HiddenField ID="hdfID" runat="server" Value='<%# Bind("ID") %>' />
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <table>
                                            <tr>
                                                <td align="left" valign="middle">
                                                    <asp:Image ID="imgActive" runat="server" Height="25px" ImageUrl="~/Images/footerarrow.png"
                                                        Width="25px" />
                                                </td>
                                                <td align="left" valign="middle">
                                                    <asp:TextBox ID="txtName" runat="server" CssClass="gvtextbox"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName"
                                            Display="Dynamic" ErrorMessage="Please Enter Name" ValidationGroup="VGAdd" CssClass="gvValidationError"
                                            Style="padding-left: 35px"></asp:RequiredFieldValidator>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Image ID="imgActive" runat="server" Height="10px" ImageUrl="~/Images/active_bullate.jpg"
                                                        Width="10px" />
                                                </td>
                                                <td style="padding-left: 10px;">
                                                    <asp:Label ID="lblMake" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:HiddenField ID="hdfID" runat="server" Value='<%# Bind("ID") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Value">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtEditValue" runat="server" Text='<%# Bind("Value") %>' CssClass="gvlable"
                                            Width="120px"></asp:TextBox>
                                        <br />
                                        <asp:RequiredFieldValidator ID="editValueReq" runat="server" ControlToValidate="txtEditValue"
                                            Display="Dynamic" ValidationGroup="EditGroup" ErrorMessage="Please Enter the value"
                                            CssClass="gvValidationError"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Value") %>' Style="padding-left: 10px"
                                            CssClass="gvlable"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <table>
                                            <tr>
                                                <td valign="middle" align="left">
                                                    <asp:TextBox ID="txtValue" runat="server" CssClass="gvtextbox"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:RequiredFieldValidator ID="ValueReq" runat="server" ControlToValidate="txtValue"
                                            Display="Dynamic" ValidationGroup="VGAdd" ErrorMessage="Please Enter the Value"
                                            CssClass="gvValidationError"></asp:RequiredFieldValidator>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Active" Visible="False">
                                    <ItemTemplate>
                                        <asp:Image ID="imgbtnActivate" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField Visible="False">
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
        </asp:Panel>
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
