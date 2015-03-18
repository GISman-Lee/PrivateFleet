<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCLoaction.ascx.cs" Inherits="User_Controls_UCLoaction" %>
<link href="../CSS/stylesheet.css" rel="stylesheet" type="text/css" />

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
            <asp:Label ID="lblResult" runat="server" CssClass="dbresult"></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 100px">
            <asp:GridView ID="gvLocationDetails" runat="server" AutoGenerateColumns="False" OnRowCommand="gvLocationDetails_RowCommand"
                OnRowDataBound="gvLocationDetails_RowDataBound" OnRowEditing="gvLocationDetails_RowEditing"
                OnRowUpdating="gvLocationDetails_RowUpdating" ShowFooter="True" Width="100%"
                AllowPaging="True" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None"
                BorderWidth="1px" CellPadding="3" OnPageIndexChanging="gvLocationDetails_PageIndexChanging"
                AllowSorting="True" OnSorting="gv_Sorting">
                <Columns>
                    <asp:TemplateField HeaderText="City" SortExpression="City">
                        <EditItemTemplate>
                            <asp:HiddenField ID="hdfCityID" runat="server" Value='<%# Bind("CityID") %>' />
                            <asp:HiddenField ID="hdfID" runat="server" Value='<%# Bind("ID") %>' />
                            <asp:DropDownList ID="ddlEditCity" runat="server" DataTextField="City" DataValueField="ID"
                                Width="174px">
                            </asp:DropDownList><br />
                            <asp:RequiredFieldValidator ID="EditCityReq" runat="server" ControlToValidate="ddlEditCity"
                                Display="None" ErrorMessage="Please Select City" InitialValue="-Select-" ValidationGroup="EditGroup"
                                CssClass="gvValidationError" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                HighlightCssClass="validatorCalloutHighlight" TargetControlID="EditCityReq">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <table>
                                <tr>
                                    <td align="left" valign="middle">
                                        <asp:Image ID="imgActive" runat="server" Height="25px" ImageUrl="~/Images/footerarrow.png"
                                            Width="25px" /></td>
                                    <td align="left" valign="middle">
                                        <asp:DropDownList ID="ddlCity" runat="server" AppendDataBoundItems="True" DataTextField="City"
                                            DataValueField="ID" Width="174px">
                                        </asp:DropDownList>
                                </tr>
                            </table>
                            <asp:RequiredFieldValidator ID="StateReq" runat="server" ControlToValidate="ddlCity"
                                Display="None" ErrorMessage="Please Select City" InitialValue="-Select-" ValidationGroup="VGAdd"
                                CssClass="gvValidationError" Style="padding-left: 35px" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2ucLocation" runat="server"
                                HighlightCssClass="validatorCalloutHighlight" TargetControlID="StateReq">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Image ID="imgActive" runat="server" Height="10px" ImageUrl="~/Images/active_bullate.jpg"
                                Width="10px" />
                            <asp:HiddenField ID="hdfID" runat="server" Value='<%# Bind("ID") %>' />
                            <asp:Label ID="lblCity" runat="server" Text='<%# Bind("City") %>' Style="padding-left: 10px"></asp:Label>&nbsp;
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Location" SortExpression="Location">
                        <EditItemTemplate>
                            <asp:HiddenField ID="hdfSuburbID" runat="server" Value='<%# Bind("SuburbID") %>' />
                            <asp:DropDownList ID="ddlEditSuburb" runat="server" AppendDataBoundItems="True" DataTextField="Suburb"
                                DataValueField="ID" Width="174px">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="LoactionReq" runat="server" ControlToValidate="ddlEditSuburb"
                                Display="None" ErrorMessage="Please Enter Loaction" ValidationGroup="EditGroup"
                                CssClass="gvValidationError" SetFocusOnError="True" InitialValue="-Select-"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
                                HighlightCssClass="validatorCalloutHighlight" TargetControlID="LoactionReq">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <ajaxToolkit:ListSearchExtender ID="ListSearchExtender2" runat="server" TargetControlID="ddlEditSuburb"
                                PromptCssClass="ListSearchExtenderPrompt" PromptText="Type Here For Location"></ajaxToolkit:ListSearchExtender>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <br />
                            <asp:DropDownList ID="ddlAddSuburb" runat="server" AppendDataBoundItems="True" DataTextField="Suburb"
                                DataValueField="ID" Width="174px">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="LocationAddReq" runat="server" ControlToValidate="ddlAddSuburb"
                                Display="None" ErrorMessage="Please Enter Loaction" ValidationGroup="VGAdd" CssClass="gvValidationError"
                                SetFocusOnError="True" InitialValue="-Select-"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server"
                                HighlightCssClass="validatorCalloutHighlight" TargetControlID="LocationAddReq">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <ajaxToolkit:ListSearchExtender ID="ListSearchExtender1" runat="server" TargetControlID="ddlAddSuburb"
                                PromptCssClass="ListSearchExtenderPrompt" PromptText="Type Here For Location">
                            </ajaxToolkit:ListSearchExtender>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblLocation" runat="server" Text='<%# Bind("Location") %>'></asp:Label>
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
