<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCCity.ascx.cs" Inherits="User_Controls_UCCity" %>
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
                    <asp:Label ID="lblResult" runat="server" CssClass="dbresult"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 100px">
                    <asp:GridView ID="gvCityDetails" runat="server" AutoGenerateColumns="False" OnRowCommand="gvCityDetails_RowCommand"
                        OnRowDataBound="gvCityDetails_RowDataBound" OnRowEditing="gvCityDetails_RowEditing"
                        OnRowUpdating="gvCityDetails_RowUpdating" ShowFooter="True" Width="100%" AllowPaging="True"
                        BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                        CellPadding="3" OnPageIndexChanging="gvCityDetails_PageIndexChanging" AllowSorting="true"
                        OnSorting="gv_Sorting">
                        <Columns>
                            <asp:TemplateField HeaderText="State" SortExpression="State">
                                <EditItemTemplate>
                                    <asp:HiddenField ID="hdfStateID" runat="server" Value='<%# Bind("StateID") %>' />
                                    <asp:HiddenField ID="hdfID" runat="server" Value='<%# Bind("ID") %>' />
                                    <asp:DropDownList ID="ddlEditStates" runat="server" DataTextField="State" DataValueField="ID"
                                        Width="174px">
                                    </asp:DropDownList><br />
                                    <asp:RequiredFieldValidator ID="EditStateReq" runat="server" ControlToValidate="ddlEditStates"
                                        Display="None" ErrorMessage="Please Select State" InitialValue="-Select-" ValidationGroup="EditGroup"
                                        CssClass="gvValidationError"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2_ucCity" runat="server"
                                        HighlightCssClass="validatorCalloutHighlight" TargetControlID="EditStateReq">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <table>
                                        <tr>
                                            <td align="left" valign="middle">
                                                <asp:Image ID="imgActive" runat="server" Height="25px" ImageUrl="~/Images/footerarrow.png"
                                                    Width="25px" /></td>
                                            <td align="left" valign="middle">
                                                <asp:DropDownList ID="ddlStates" runat="server" AppendDataBoundItems="True" DataTextField="State"
                                                    DataValueField="ID" Width="174px" CssClass="gvtextbox">
                                                </asp:DropDownList><br />
                                        </tr>
                                    </table>
                                    <asp:RequiredFieldValidator ID="StateReq" runat="server" ControlToValidate="ddlStates"
                                        Display="None" ErrorMessage="Please Select State" InitialValue="-Select-" ValidationGroup="VGAdd"
                                        Style="padding-left: 35px"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="valExtAddState" runat="server" HighlightCssClass="validatorCalloutHighlight"
                                        TargetControlID="StateReq">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Image ID="imgActive" runat="server" Height="10px" ImageUrl="~/Images/active_bullate.jpg"
                                        Width="10px" />
                                    <asp:HiddenField ID="hdfID" runat="server" Value='<%# Bind("ID") %>' />
                                    <asp:Label ID="lblState" runat="server" Text='<%# Bind("State") %>' Style="padding-left: 10px"></asp:Label>&nbsp;
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="City" SortExpression="City">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEditCity" runat="server" Text='<%# Bind("City") %>'></asp:TextBox><br />
                                    <asp:RequiredFieldValidator ID="ModelReq" runat="server" ControlToValidate="txtEditCity"
                                        Display="None" ErrorMessage="Please Enter City" ValidationGroup="EditGroup" CssClass="gvVakidationError"
                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="valExtEditCity" runat="server" HighlightCssClass="validatorCalloutHighlight"
                                        TargetControlID="ModelReq">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtCity" runat="server" CssClass="gvtextbox"></asp:TextBox><br />
                                    <asp:RequiredFieldValidator ID="CityAddReq" runat="server" ControlToValidate="txtCity"
                                        Display="None" ErrorMessage="Please Enter City" ValidationGroup="VGAdd" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="valExtCity" runat="server" HighlightCssClass="validatorCalloutHighlight"
                                        TargetControlID="CityAddReq">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblCity" runat="server" Text='<%# Bind("City") %>'></asp:Label>
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
                                        ToolTip="Update This Record" ValidationGroup="EditGroup" />
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
