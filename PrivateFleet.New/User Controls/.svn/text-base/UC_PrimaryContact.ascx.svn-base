<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UC_PrimaryContact.ascx.cs"
    Inherits="User_Controls_UC_PrimaryContact" %>
    <link href="../CSS/stylesheet.css" rel="stylesheet" type="text/css" />
<asp:UpdatePanel ID="UP2" runat="server">
    <ContentTemplate>
        <table width="95%" align="center">
            <tr>
                <td height="30px" valign="middle">
                    <asp:Label ID="lblResult" runat="server" CssClass="dbresult"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="grdPrimaryContactDtls" runat="server" AutoGenerateColumns="False"
                        OnRowCommand="grdPrimaryContactDtls_RowCommand" OnRowDataBound="grdPrimaryContactDtls_RowDataBound"
                        OnRowEditing="grdPrimaryContactDtls_RowEditing" OnRowUpdating="grdPrimaryContactDtls_RowUpdating"
                        ShowFooter="true" Width="100%" AllowPaging="True" BackColor="White" BorderColor="#CCCCCC"
                        BorderStyle="None" BorderWidth="1px" CellPadding="3" OnPageIndexChanging="grdPrimaryContactDtls_PageIndexChanging"
                        AllowSorting="true" OnSorting="grdPrimaryContactDtls_Sorting">
                        <Columns>
                            <asp:TemplateField HeaderText="Name" SortExpression="Name">
                                <ItemTemplate>
                                    <asp:Image ID="imgActive" runat="server" Height="10px" ImageUrl="~/Images/active_bullate.jpg"
                                        Width="10px" />
                                    <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>' Style="padding-left: 10px"></asp:Label>&nbsp;
                                    <asp:HiddenField ID="hdfID" runat="server" Value='<%# Bind("ID") %>' />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Image ID="imgActive" runat="server" Height="10px" ImageUrl="~/Images/active_bullate.jpg"
                                        Width="10px" />
                                    <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>' Style="padding-left: 10px"></asp:Label>&nbsp;
                                    <asp:HiddenField ID="hdfID" runat="server" Value='<%# Bind("ID") %>' />
                                    <%-- <asp:RequiredFieldValidator ID="EditNameReq" runat="server" ControlToValidate="txtEditName"
                                        Display="None" ErrorMessage="Please Enter Name" ValidationGroup="EditGroup" CssClass="gvValidationError"
                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValName" runat="server" HighlightCssClass="validatorCalloutHighlight"
                                        TargetControlID="EditNameReq">
                                    </ajaxToolkit:ValidatorCalloutExtender>--%>
                                </EditItemTemplate>
                                <FooterTemplate>
                                        <table>
                                            <tr>
                                                <td align="left" valign="middle">
                                                    <asp:Image ID="imgActive1" runat="server" Height="25px" ImageUrl="~/Images/footerarrow.png"
                                                        Width="25px" />
                                                </td>
                                                <td align="left" valign="middle">
                                                    <asp:TextBox ID="txtName" runat="server" CssClass="gvtextbox"></asp:TextBox><br />
                                            </tr>
                                        </table>
                                        <asp:RequiredFieldValidator ID="NameReq" runat="server" ControlToValidate="txtName"
                                            Display="None" ErrorMessage="Please Enter Name" ValidationGroup="VGAdd"
                                            CssClass="gvValidationError" Style="padding-left: 35px"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
                                            HighlightCssClass="validatorCalloutHighlight" TargetControlID="NameReq">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                    </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Email" SortExpression="Email">
                                <ItemTemplate>
                                    <asp:Label ID="lblEmail" runat="server" Text='<%# Bind("Email") %>' Style="padding-left: 10px"></asp:Label>&nbsp;
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEditEmail" runat="server" Text='<%# Bind("Email") %>' MaxLength="100"></asp:TextBox><br />
                                    <asp:RequiredFieldValidator ID="reqEmail" runat="server" ControlToValidate="txtEditEmail"
                                        Display="None" ErrorMessage="Please Enter Email" ValidationGroup="EditGroup"
                                        CssClass="gvValidationError" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="valEmail" runat="server" HighlightCssClass="validatorCalloutHighlight"
                                        TargetControlID="reqEmail">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <asp:RegularExpressionValidator ID="regExprEmail" runat="server" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        ControlToValidate="txtEditEmail" SetFocusOnError="True" Display="None" ValidationGroup="EditGroup"
                                        CssClass="errormsg" ErrorMessage="Please enter valid email."></asp:RegularExpressionValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                        HighlightCssClass="validatorCalloutHighlight" TargetControlID="regExprEmail">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </EditItemTemplate>
                                <FooterTemplate>
                                        <table>
                                            <tr>
                                                <td align="left" valign="middle">
                                                    <asp:Image ID="imgActive2" runat="server" Height="25px" ImageUrl="~/Images/footerarrow.png"
                                                        Width="25px" />
                                                </td>
                                                <td align="left" valign="middle">
                                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="gvtextbox"></asp:TextBox><br />
                                            </tr>
                                        </table>
                                        <asp:RequiredFieldValidator ID="EmailReq" runat="server" ControlToValidate="txtEmail"
                                            Display="None" ErrorMessage="Please Enter Email" ValidationGroup="VGAdd"
                                            CssClass="gvValidationError" Style="padding-left: 35px"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server"
                                            HighlightCssClass="validatorCalloutHighlight" TargetControlID="EmailReq">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                    </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="Primary Contact For" SortExpression="PrimaryContactFor">
                                <ItemTemplate>
                                    <asp:Label ID="lblPrimaryContactFor" runat="server" Text='<%# Bind("PrimaryContactFor") %>'
                                        Style="padding-left: 10px"></asp:Label>&nbsp;
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEditPrimaryContactFor" runat="server" Text='<%# Bind("PrimaryContactFor") %>'
                                        MaxLength="100"></asp:TextBox><br />
                                    <asp:RequiredFieldValidator ID="reqPrimaryContactFor" runat="server" ControlToValidate="txtEditPrimaryContactFor"
                                        Display="None" ErrorMessage="Please Enter Primary Contact For" ValidationGroup="EditGroup"
                                        CssClass="gvValidationError" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="valPrimaryContactFor" runat="server" HighlightCssClass="validatorCalloutHighlight"
                                        TargetControlID="reqPrimaryContactFor">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </EditItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Active" >
                                <ItemTemplate>
                                    <asp:Image ID="imgbtnActivate" runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField >
                                <EditItemTemplate>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkbtnActiveness" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                        CommandName="Activeness" Style="padding-left: 10px" CssClass="activatelink">Activate</asp:LinkButton>&nbsp;
                                    <asp:HiddenField ID="hdfIsActive" runat="server" Value='<%# Bind("IsActive") %>' />
                                </ItemTemplate>
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
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnEdit" runat="server" CausesValidation="False" CommandName="Edit"
                                        ImageUrl="~/Images/edit.png" ToolTip="Edit This Record" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:ImageButton ID="imgbtnUpdate" runat="server" CommandName="Update" ImageUrl="~/Images/update.png"
                                        ToolTip="Update This Record" ValidationGroup="EditGroup" />
                                    <asp:ImageButton ID="imgbtnCancel" runat="server" CausesValidation="False" CommandName="Update"
                                        ImageUrl="~/Images/cancel.png" OnClick="imgbtnCancel_Click" ToolTip="Cancel" />
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
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
