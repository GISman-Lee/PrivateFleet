<%@ Page Title="User Master" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="UserMaster_1.aspx.cs" Inherits="UserMaster_1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UP_usermaster" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td colspan="4">
                        <asp:Label ID="lblResult" runat="server" CssClass="dbresult"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span style="color: Red">*</span> Role :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlRoles" runat="server" AutoPostBack="true" DataValueField="ID"
                            DataTextField="Role" Width="170px" OnSelectedIndexChanged="ddlRoles_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvRole" runat="server" ControlToValidate="ddlRoles"
                            Display="None" ErrorMessage="Please Select the Role" InitialValue="-Select-"
                            SetFocusOnError="True" ValidationGroup="VGSubmit" CssClass="gvValidationError"></asp:RequiredFieldValidator>
                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server"
                            HighlightCssClass="validatorCalloutHighlight" TargetControlID="rfvRole">
                        </ajaxToolkit:ValidatorCalloutExtender>
                    </td>
                    <td>
                        &nbsp;&nbsp;Dealer :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlDealer" runat="server" AutoPostBack="true" AppendDataBoundItems="True"
                            OnSelectedIndexChanged="ddlDealer_SelectedIndexChanged" Width="170px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span style="color: Red">*</span> Name :
                    </td>
                    <td>
                        <asp:TextBox ID="txtName" runat="server" Width="170px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvName" runat="server" ErrorMessage="Please enter name"
                            ControlToValidate="txtName" ValidationGroup="VGSubmit" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                            HighlightCssClass="validatorCalloutHighlight" TargetControlID="rfvName">
                        </ajaxToolkit:ValidatorCalloutExtender>
                    </td>
                    <td>
                        <span style="color: Red">*</span> Email :
                    </td>
                    <td>
                        <asp:TextBox ID="txtEmail" runat="server" Width="230px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="Please enter email"
                            ControlToValidate="txtEmail" ValidationGroup="VGSubmit" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="REVEMail" runat="server" ControlToValidate="txtEmail"
                            Display="None" ErrorMessage="Please enter correct email address" SetFocusOnError="True"
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="VGSubmit"></asp:RegularExpressionValidator>
                        <%-- <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                            HighlightCssClass="validatorCalloutHighlight" TargetControlID="rfvEmail">
                        </ajaxToolkit:ValidatorCalloutExtender>
                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server"
                            HighlightCssClass="validatorCalloutHighlight" TargetControlID="REVEMail">
                        </ajaxToolkit:ValidatorCalloutExtender>--%>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        &nbsp;&nbsp;Phone :
                    </td>
                    <td valign="top">
                        <asp:TextBox ID="txtPhone" runat="server" Width="100px"></asp:TextBox>
                        ext&nbsp;<asp:TextBox ID="txtExtension" runat="server" Width="42px" MaxLength="4"></asp:TextBox>
                        <%--     <ajaxToolkit:FilteredTextBoxExtender ID="FTE_Phone" runat="server" TargetControlID="txtPhone"
                            FilterType="Custom" ValidChars="0123456789+">
                        </ajaxToolkit:FilteredTextBoxExtender>--%>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FTE_Extension" runat="server" TargetControlID="txtExtension"
                            FilterType="Custom" ValidChars="0123456789+">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </td>
                    <td valign="top">
                        &nbsp;&nbsp;Mobile :
                    </td>
                    <td>
                        <asp:TextBox ID="txtMobile" runat="server" Width="170px" MaxLength="15"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FTE_Mobile" runat="server" TargetControlID="txtMobile"
                            FilterType="Custom" ValidChars="0123456789+">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        &nbsp;&nbsp;Address :
                    </td>
                    <td valign="top">
                        <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" Rows="2" Width="230px"></asp:TextBox>
                    </td>
                    <td valign="top">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr runat="server" id="trUserName">
                    <td>
                        <%-- <span style="color: Red">*</span> User Name :--%>
                    </td>
                    <td>
                        <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
                        <%--  <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ErrorMessage="Please enter username"
                                        ControlToValidate="txtUserName" ValidationGroup="VGSubmit" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
                                        HighlightCssClass="validatorCalloutHighlight" TargetControlID="rfvUsername">
                                    </ajaxToolkit:ValidatorCalloutExtender>--%>
                    </td>
                    <td>
                        <asp:Label ID="Label8" runat="server" CssClass="label" Visible="false">Username Expiry Date</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtExpiryDate" runat="server" Visible="false"></asp:TextBox>
                    </td>
                </tr>
                <tr id="trPassword" runat="server">
                    <td>
                        <%--   <span style="color: Red">*</span> Password :--%>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="rfvPassword" runat="server" ErrorMessage="Please enter password"
                                        ControlToValidate="txtPassword" ValidationGroup="VGSubmit" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server"
                                        HighlightCssClass="validatorCalloutHighlight" TargetControlID="rfvPassword">
                                    </ajaxToolkit:ValidatorCalloutExtender>--%>
                    </td>
                    <td>
                    </td>
                    <td>
                        <asp:HiddenField ID="hdfID" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="4">
                        <asp:ImageButton ID="imgbtnAdd" runat="server" ImageUrl="~/Images/AddUser.gif" OnClick="imgbtnAdd_Click"
                            onmouseout="this.src='Images/AddUser.gif'" onmouseover="this.src='Images/AddUser_hvr.gif'"
                            ValidationGroup="VGSubmit" />
                        <asp:ImageButton ID="imgbtnSearch" runat="server" ImageUrl="~/Images/searchUser.gif"
                            OnClick="imgbtnSearch_Click" onmouseout="this.src='Images/searchUser.gif'" onmouseover="this.src='Images/searchUser_hvr.gif'"
                            CausesValidation="False" />
                        <asp:ImageButton ID="imgbtnCancel" runat="server" ImageUrl="~/Images/Cancel.gif"
                            OnClick="imgbtnCancel_Click" onmouseout="this.src='Images/Cancel.gif'" onmouseover="this.src='Images/Cancel_hvr.gif'"
                            CausesValidation="False" />
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="4">
                        <asp:Label ID="lblRowsToDisplay" runat="server">Rows To Display</asp:Label>
                        <asp:DropDownList ID="ddl_NoRecords" runat="server" AutoPostBack="true" Width="50px"
                            OnSelectedIndexChanged="ddl_NoRecords_SelectedIndexChanged">
                            <asp:ListItem Value="10">10</asp:ListItem>
                            <asp:ListItem Value="20">20</asp:ListItem>
                            <asp:ListItem Value="30">30</asp:ListItem>
                            <asp:ListItem Value="50">50</asp:ListItem>
                            <asp:ListItem Value="All">All</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:GridView ID="gvUserDetails" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            DataKeyNames="ID" OnRowCommand="gvUserDetails_RowCommand" OnRowDataBound="gvUserDetails_RowDataBound"
                            OnPageIndexChanging="gvUserDetails_PageIndexChanging" BackColor="White" BorderColor="#CCCCCC"
                            BorderStyle="None" BorderWidth="1px" Width="100%" CellPadding="3" AllowSorting="True"
                            OnSorting="gv_Sorting" OnRowEditing="gvUserDetails_RowEditing" OnRowUpdating="gvUserDetails_RowUpdating">
                            <Columns>
                                <asp:TemplateField HeaderText="Name" SortExpression="Name">
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <table border="0">
                                            <tr>
                                                <td valign="top" style="padding-top: 5px;">
                                                    <asp:Image ID="imgActive" runat="server" Height="10px" ImageUrl="~/Images/active_bullate.jpg" />
                                                </td>
                                                <td valign="top">
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Username" HeaderText="User Name" />
                                <asp:TemplateField HeaderText="Password">
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <table border="0">
                                            <tr>
                                                <td valign="top">
                                                    <asp:Label ID="lblPassword1" runat="server" Text="********"></asp:Label>
                                                    <asp:Label ID="lblPassword" Visible="false" runat="server" Text='<%# Bind("Password") %>'></asp:Label>
                                                </td>
                                                <td valign="top" align="right">
                                                    <asp:ImageButton ID="imgbtnSeePass" runat="server" CausesValidation="False" CommandName="SeePassword"
                                                        ImageUrl="~/Images/edit.png" ToolTip="Click To See Password" CommandArgument="<%# Container.DataItemIndex %>" />
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Email" Visible="false" HeaderText="Email" />
                                <%--  <asp:BoundField DataField="Address" HeaderText="Address" />--%>
                                <asp:BoundField DataField="Phone1" HeaderText="Phone" />
                                <%--<asp:BoundField DataField="ExpriryDate" HeaderText="ExpriryDate" />--%>
                                <asp:BoundField DataField="Role" HeaderText="Role" />
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
                                        <asp:LinkButton ID="lnkbtnActiveness" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                            CommandName="Activeness" Style="padding-left: 10px" CssClass="activatelink">Activate</asp:LinkButton>
                                        <asp:HiddenField ID="hdfIsActive" runat="server" Value='<%# Bind("IsActive") %>' />
                                        <asp:HiddenField ID="hdfId" runat="server" Value='<%# Bind("Id") %>' />
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
                                            ImageUrl="~/Images/edit.png" ToolTip="Edit This Record" CommandArgument="<%# Container.DataItemIndex %>" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="gvFooterrow" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <PagerStyle CssClass="pgr" />
                            <HeaderStyle BackColor="#0A73A2" Font-Bold="True" CssClass="gvHeader" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--<asp:UpdateProgress ID="upProcess" runat="server" AssociatedUpdatePanelID="UP_usermaster">
        <ProgressTemplate>
            <div id="progressBackgroundFilter">
            </div>
            <div id="processMessage">
                <span style="text-align: center;">
                    <img src="Images/loading.gif" alt="" /><br />
                    Loading...Please wait...</span></div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
</asp:Content>
