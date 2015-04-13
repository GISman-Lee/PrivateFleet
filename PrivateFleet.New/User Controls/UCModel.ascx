<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCModel.ascx.cs" Inherits="User_Controls_UCModel" %>
<link href="../CSS/stylesheet.css" rel="stylesheet" type="text/css" />

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
                <td style="height: 30px" valign="middle" align="left">
                    <asp:Label ID="lblResult" runat="server" CssClass="dbresult"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Search Model &nbsp;
                    <asp:DropDownList ID="ddlSearchMakes" runat="server" Width="174px" AppendDataBoundItems="True"
                        DataTextField="Make" DataValueField="ID">
                    </asp:DropDownList>
                    &nbsp;
                    <asp:TextBox ID="txtSearchModel" runat="server"></asp:TextBox>&nbsp;
                    <asp:ImageButton ID="imgbtnSearch" runat="server" ImageUrl="~/Images/Search_model_hvr.gif"
                        onmouseout="this.src='Images/Search_model_hvr.gif'" onmouseover="this.src='Images/Search_model.gif'"
                        OnClick="imgbtnSearch_Click" />
                    &nbsp;
                    <asp:ImageButton ID="imgbtnSearchCancel" runat="server" ImageUrl="~/Images/Cancel.gif"
                        onmouseout="this.src='Images/Cancel.gif'" onmouseover="this.src='Images/Cancel_hvr.gif'"
                        CausesValidation="False" OnClick="imgbtnSearchCancel_Click" />
                </td>
            </tr>
            <tr>
                <td style="width: 100px">
                    <asp:GridView ID="gvModelDetails" runat="server" AutoGenerateColumns="False" OnRowCommand="gvModelDetails_RowCommand"
                        OnRowDataBound="gvModelDetails_RowDataBound" OnRowEditing="gvModelDetails_RowEditing"
                        OnRowUpdating="gvModelDetails_RowUpdating" ShowFooter="True" Width="100%" AllowPaging="True"
                        BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                        CellPadding="3" OnPageIndexChanging="gvModelDetails_PageIndexChanging" AllowSorting="True"
                        OnSorting="gv_Sorting">
                        <Columns>
                            <asp:TemplateField HeaderText="Make" SortExpression="Make">
                                <EditItemTemplate>
                                    <asp:HiddenField ID="hdfID" runat="server" Value='<%# Bind("ID") %>' />
                                    <asp:HiddenField ID="hdfMakeID" runat="server" Value='<%# Bind("MakeID") %>' />
                                    <asp:DropDownList ID="ddlEditMakes" runat="server" Width="174px" DataTextField="Make"
                                        DataValueField="ID">
                                    </asp:DropDownList>
                                    <br />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlEditMakes"
                                        Display="None" ErrorMessage="Please Select Make" InitialValue="-Select-" ValidationGroup="EditGroup"
                                        CssClass="gvValidationError" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                        HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator1">
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
                                                <asp:DropDownList ID="ddlMakes" runat="server" Width="174px" AppendDataBoundItems="True"
                                                    DataTextField="Make" DataValueField="ID">
                                                </asp:DropDownList>
                                                <br />
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:RequiredFieldValidator ID="MakeReq" runat="server" ControlToValidate="ddlMakes"
                                        Display="None" ErrorMessage="Please Select Make" InitialValue="-Select-" ValidationGroup="VGAdd"
                                        Style="padding-left: 35px" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2_ucModel" runat="server"
                                        HighlightCssClass="validatorCalloutHighlight" TargetControlID="MakeReq">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Image ID="imgActive" runat="server" Height="10px" ImageUrl="~/Images/active_bullate.jpg"
                                        Width="10px" />
                                    <asp:HiddenField ID="hdfID" runat="server" Value='<%# Bind("ID") %>' />
                                    <asp:Label ID="lblMake" runat="server" Text='<%# Bind("Make") %>' Style="padding-left: 10px"></asp:Label>&nbsp;
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Model" SortExpression="Model">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEditModel" runat="server" Text='<%# Bind("Model") %>'></asp:TextBox><br />
                                    <asp:RequiredFieldValidator ID="ModelReq" runat="server" ControlToValidate="txtEditModel"
                                        Display="None" ErrorMessage="Please Enter Model" ValidationGroup="EditGroup"
                                        CssClass="gvValidationError" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
                                        HighlightCssClass="validatorCalloutHighlight" TargetControlID="ModelReq">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <table>
                                        <tr>
                                            <td valign="middle" align="left">
                                                <asp:TextBox ID="txtModel" runat="server"></asp:TextBox><br />
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:RequiredFieldValidator ID="ModelAddReq" runat="server" ControlToValidate="txtModel"
                                        Display="None" ErrorMessage="Please Enter Model" ValidationGroup="VGAdd" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server"
                                        HighlightCssClass="validatorCalloutHighlight" TargetControlID="ModelAddReq">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblModel" runat="server" Text='<%# Bind("Model") %>'></asp:Label>
                                </ItemTemplate>
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
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkManageAccessories" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                        Style="padding-left: 10px" CssClass="activatelink">Manage Accessories</asp:LinkButton>&nbsp;
                                </ItemTemplate>
                                <FooterTemplate>
                                    Add Accessories?<br />
                                    <asp:RadioButtonList ID="rbAddAccessories" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="No" Selected="True"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <FooterStyle HorizontalAlign ="Center" />
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
