<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCAccessories.ascx.cs"
    Inherits="User_Controls_UCAccessories" %>
<link href="../CSS/stylesheet.css" rel="stylesheet" type="text/css" />

<script type="text/javascript">
    //    function ConfirmUpdate() {
    //        var Choice = confirm('Do you want to update this entry?');
    //        return Choice;
    //    }
</script>

<asp:UpdatePanel ID="UP2" runat="server">
    <ContentTemplate>
        <asp:Panel ID="pnlAccMaster" runat="server">
            <table width="95%" align="center">
                <tr>
                    <td style="height: 30px" colspan="3" valign="middle">
                        <asp:Label ID="lblResult" runat="server" CssClass="dbresult"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" width="115px">
                        Search Accessory &nbsp;
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtSearchAcc" runat="server" CssClass="gvtextbox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left" width="115px">
                        Accessory Type
                    </td>
                    <td width="125px">
                        <asp:DropDownList ID="ddlAccType" runat="server" Width="154px" Height="23px">
                            <asp:ListItem Text="Master Accessory" Selected="True" Value="0"></asp:ListItem>
                            <asp:ListItem Text="All Accessory" Value="1"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="left">
                        <asp:ImageButton ID="imgbtnSearch" runat="server" ImageUrl="~/Images/search_accessory_hvr.gif"
                            onmouseout="this.src='Images/search_accessory_hvr.gif'" onmouseover="this.src='Images/search_accessory.gif'"
                            OnClick="imgbtnSearch_Click" />
                        &nbsp;
                        <asp:ImageButton ID="imgbtnSearchCancel" runat="server" ImageUrl="~/Images/Cancel.gif"
                            onmouseout="this.src='Images/Cancel.gif'" onmouseover="this.src='Images/Cancel_hvr.gif'"
                            CausesValidation="False" OnClick="imgbtnSearchCancel_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <br />
                        <asp:GridView ID="gvAccessoryDetails" runat="server" AutoGenerateColumns="False"
                            OnRowCommand="gvAccessoryDetails_RowCommand" OnRowDataBound="gvAccessoryDetails_RowDataBound"
                            OnRowEditing="gvAccessoryDetails_RowEditing" OnRowUpdating="gvAccessoryDetails_RowUpdating"
                            BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                            CellPadding="3" AllowPaging="True" Width="100%" ShowFooter="True" OnPageIndexChanging="gvAccessoryDetails_PageIndexChanging"
                            OnSorting="gv_Sorting" AllowSorting="True" PageSize="15">
                            <Columns>
                                <asp:TemplateField HeaderText="Accessory Name" SortExpression="Name">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtEditAccessories" runat="server" Text='<%# Bind("Name") %>' CssClass="gvtextbox"></asp:TextBox><br />
                                        <asp:HiddenField ID="hdfID" runat="server" Value='<%# Bind("ID") %>' />
                                        <asp:RequiredFieldValidator ID="EditAccessoriesReq" runat="server" ControlToValidate="txtEditAccessories"
                                            Display="None" ErrorMessage="Please Enter accessory Name" ValidationGroup="EditGroup"
                                            CssClass="gvValidationError"></asp:RequiredFieldValidator>
                                        <br />
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2_ucAcc" runat="server"
                                            HighlightCssClass="validatorCalloutHighlight" TargetControlID="EditAccessoriesReq">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <table>
                                            <tr>
                                                <td align="left" valign="middle">
                                                    <asp:Image ID="imgActive" runat="server" Height="25px" ImageUrl="~/Images/footerarrow.png"
                                                        Width="25px" />
                                                </td>
                                                <td align="left" valign="middle">
                                                    <asp:TextBox ID="txtAccessories" runat="server" CssClass="gvtextbox"></asp:TextBox><br />
                                            </tr>
                                        </table>
                                        <asp:RequiredFieldValidator ID="AccessoriesReq" runat="server" ControlToValidate="txtAccessories"
                                            Display="None" ErrorMessage="Please Enter accessory Name" ValidationGroup="VGAdd"
                                            CssClass="gvValidationError" Style="padding-left: 35px"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
                                            HighlightCssClass="validatorCalloutHighlight" TargetControlID="AccessoriesReq">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Image ID="imgActive" runat="server" Height="10px" ImageUrl="~/Images/active_bullate.jpg"
                                            Width="10px" />
                                        <asp:Label ID="lblAccessories" runat="server" Text='<%# Bind("Name") %>' Style="padding-left: 10px"></asp:Label>&nbsp;
                                        <asp:HiddenField ID="hdfID" runat="server" Value='<%# Bind("ID") %>' />
                                    </ItemTemplate>
                                    <ItemStyle Width="50%" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Active">
                                    <ItemTemplate>
                                        <asp:Image ID="imgbtnActivate" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="20%" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <FooterTemplate>
                                        <asp:CheckBox ID="chkIsMaster" runat="server" Text="<br/>Is Master?" />
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkBtnMarkMaster" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                            CommandName="MasterFlagUpdate" Style="padding-left: 1px" CssClass="activatelink">Mark Master</asp:LinkButton>&nbsp;
                                        <asp:HiddenField ID="hdfIsMaster" runat="server" Value='<%# Bind("IsMaster") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
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
                                        <asp:LinkButton ID="lnkbtnActiveness" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                            CommandName="Activeness" Style="padding-left: 10px" CssClass="activatelink">Activate</asp:LinkButton>&nbsp;
                                        <asp:HiddenField ID="hdfIsActive" runat="server" Value='<%# Bind("IsActive") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <EditItemTemplate>
                                        <asp:ImageButton ID="imgbtnUpdate" runat="server" CommandName="Update" ImageUrl="~/Images/update.png"
                                            ToolTip="Update This Record" ValidationGroup="EditGroup" />
                                        <asp:ImageButton ID="imgbtnCancel" runat="server" CausesValidation="False" CommandName="Update"
                                            ImageUrl="~/Images/cancel.png" OnClick="imgbtnCancel_Click" ToolTip="Cancel" />
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="20%" />
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
