<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCMake.ascx.cs" Inherits="User_Controls_UCMake" %>
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
        <div style="">
            <table width="95%" align="center">
                <tr>
                    <td style="height: 30px" valign="middle" align="left">
                        <asp:Label ID="lblResult" runat="server" CssClass="dbresult"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvMakeDetails" runat="server" AutoGenerateColumns="False" Width="100%"
                            OnRowEditing="gvMakeDetails_RowEditing" OnRowUpdating="gvMakeDetails_RowUpdating"
                            OnRowDataBound="gvMakeDetails_RowDataBound" ShowFooter="True" OnRowCommand="gvMakeDetails_RowCommand"
                            AllowPaging="True" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None"
                            BorderWidth="1px" CellPadding="3" OnPageIndexChanging="gvMakeDetails_PageIndexChanging"
                            AllowSorting="True" OnSorting="gv_Sorting" PageSize="10">
                            <Columns>
                                <asp:TemplateField HeaderText="Make" SortExpression="Make">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtEditMake" runat="server" Text='<%# Bind("Make") %>'></asp:TextBox><br />
                                        <asp:HiddenField ID="hdfID" runat="server" Value='<%# Bind("ID") %>' />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEditMake"
                                            Display="None" ErrorMessage="Please Enter Make" ValidationGroup="EditGroup" CssClass="gvValidationError"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                            HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator1">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <table>
                                            <tr>
                                                <td align="left" valign="middle">
                                                    <asp:Image ID="imgActive" runat="server" Height="25px" ImageUrl="~/Images/footerarrow.png"
                                                        Width="25px" /></td>
                                                <td align="left" valign="middle">
                                                    <asp:TextBox ID="txtMake" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMake"
                                            Display="None" ErrorMessage="Please Enter Make" ValidationGroup="VGAdd" Style="padding-left: 35px"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2_ucMake" runat="server"
                                            HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator1">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Image ID="imgActive" runat="server" Height="10px" ImageUrl="~/Images/active_bullate.jpg"
                                            Width="10px" />
                                        <asp:Label ID="lblMake" runat="server" Text='<%# Bind("Make") %>' Style="padding-left: 10px"></asp:Label>&nbsp;
                                        <asp:HiddenField ID="hdfID" runat="server" Value='<%# Bind("ID") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
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
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <table>
                                            <tr>
                                                <td align="left" valign="middle">
                                                    <asp:ImageButton ID="imgbtnAdd" runat="server" ImageUrl="~/Images/Save1.png" OnClick="imgbtnAdd_Click"
                                                        ToolTip="Add Record" ValidationGroup="VGAdd" Style="padding-left: 10px" Height="15px"
                                                        ImageAlign="Middle" Width="48px" /></td>
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
        </div>
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
