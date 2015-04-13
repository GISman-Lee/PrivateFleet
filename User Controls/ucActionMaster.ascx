<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucActionMaster.ascx.cs"
    Inherits="User_Controls_ucActionMaster" %>

<script type="text/javascript">
//        function ConfirmUpdate()
//        {
//           var Choice =confirm('Do you want to update this entry ?');
//           return Choice;
//        }
</script>

 
 

<table width="95%" align="center">
    <tr>
        <td style="height: 30px" valign="middle">
            <asp:Label ID="lblMsg" runat="server" Text="" CssClass="dbresult"></asp:Label>
        </td>
    </tr>
    <tr>
        <td valign="top">
            <asp:GridView ID="gvActions" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                DataKeyNames="ID" OnRowEditing="gvActions_RowEditing" OnRowUpdating="gvActions_RowUpdating"
                OnRowCommand="gvActions_RowCommand" OnRowDataBound="gvActions_RowDataBound" 
                AllowPaging="true" OnPageIndexChanging="gvActions_PageIndexChanging"
                BackColor="White"
                BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" Width="100%" CellPadding="3">
                <Columns>
                    <asp:TemplateField HeaderText="Action">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditAction" runat="server" Text='<%#Bind("Action") %>'></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvEditAction" runat="server" ErrorMessage="Please Enter the action" ControlToValidate="txtEditAction" Display="None" ValidationGroup="EditGroup"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                HighlightCssClass="validatorCalloutHighlight" TargetControlID="rfvEditAction">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Image ID="imgActive" runat="server" Height="10px" ImageUrl="~/Images/active_bullate.jpg"
                                Width="10px" />
                            <asp:Label ID="lblAction" runat="server" Text='<%#Bind("Action") %>' Style="padding-left: 10px"></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <table>
                                <tr>
                                    <td align="left" valign="middle">
                                        <asp:Image ID="imgActive" runat="server" Height="25px" ImageUrl="~/Images/footerarrow.png"
                                            Width="25px" /></td>
                                    <td align="left" valign="middle">
                                        <asp:TextBox ID="txtAddAction" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvAddAction" runat="server" ErrorMessage="Please Enter the action" ControlToValidate="txtAddAction"
                                            ValidationGroup="VGAdd" Display="None"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                            </table>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2_ucActionMaster" runat="server"
                                HighlightCssClass="validatorCalloutHighlight" TargetControlID="rfvAddAction">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </FooterTemplate>
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
                            <asp:TextBox ID="txtAddDesc" runat="server" TextMode="MultiLine" Rows="2"></asp:TextBox>
                        </FooterTemplate>
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
