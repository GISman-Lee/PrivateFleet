<%@ Page Title="Model-Accessories Mapping" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ModelAccessory.aspx.cs" Inherits="ModelAccessory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="95%" align="center">
        <tr>
            <td style="height: 30px" valign="middle" align="left">
                <asp:Label ID="lblResult" runat="server" CssClass="dbresult"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td style="width: 25%;">
                            Make:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlMake" runat="server" Width="300px" AppendDataBoundItems="True"
                                DataTextField="Make" DataValueField="ID" OnSelectedIndexChanged="ddlMake_SelectedIndexChanged"
                                AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td style="width: 25%;">
                            Model:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlModel" runat="server" Width="300px" AppendDataBoundItems="True"
                                DataTextField="Model" DataValueField="ID" AutoPostBack="true" OnSelectedIndexChanged="ddlModel_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <br />
            </td>
        </tr>
        <tr>
            <td style="width: 100px">
                <asp:GridView ID="gvModelAccessories" runat="server" AutoGenerateColumns="False"
                    ShowFooter="True" Width="100%" AllowPaging="True" BackColor="White" BorderColor="#CCCCCC"
                    BorderStyle="None" BorderWidth="1px" CellPadding="3" AllowSorting="True" OnRowCommand="gvModelAccessories_RowCommand"
                    OnRowDataBound="gvModelAccessories_RowDataBound" OnRowEditing="gvModelAccessories_RowEditing"
                    OnPageIndexChanging="gvModelAccessories_PageIndexChanging" OnRowUpdating="gvModelAccessories_RowUpdating" OnSorting="gv_Sorting">
                    <Columns>
                        <asp:TemplateField HeaderText="Accessory" SortExpression="Accessory">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtAccessory_Edit" runat="server" Text='<%# Bind("Accessory") %>'></asp:TextBox>
                                <asp:HiddenField ID="hdfID" runat="server" Value='<%# Bind("ID") %>' />
                                <asp:HiddenField ID="hdfAccessoryId" runat="server" Value='<%# Bind("AccessoryId") %>' />
                                <br />
                                <asp:RequiredFieldValidator ID="AccessoryReq1" runat="server" ControlToValidate="txtAccessory_Edit"
                                    Display="None" ErrorMessage="Please Enter Accessory Name" ValidationGroup="VGEdit"
                                    Style="padding-left: 35px" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                    HighlightCssClass="validatorCalloutHighlight" TargetControlID="AccessoryReq1">
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
                                            <asp:TextBox ID="txtAccessory" runat="server" MaxLength="50"></asp:TextBox>
                                            <br />
                                        </td>
                                    </tr>
                                </table>
                                <asp:RequiredFieldValidator ID="AccessoryReq" runat="server" ControlToValidate="txtAccessory"
                                    Display="None" ErrorMessage="Please Enter Accessory" ValidationGroup="VGAdd"
                                    Style="padding-left: 35px" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2_ucModelAcc" runat="server"
                                    HighlightCssClass="validatorCalloutHighlight" TargetControlID="AccessoryReq">
                                </ajaxToolkit:ValidatorCalloutExtender>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Image ID="imgActive" runat="server" Height="10px" ImageUrl="~/Images/active_bullate.jpg"
                                    Width="10px" />
                                <asp:HiddenField ID="hdfID" runat="server" Value='<%# Bind("ID") %>' />
                                <asp:Label ID="lblAccessory" runat="server" Text='<%# Bind("Accessory") %>' Style="padding-left: 10px"></asp:Label>&nbsp;
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Specification" SortExpression="Specification">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEditDesc_Edit" TextMode="MultiLine" runat="server" Text='<%# Bind("Specification") %>'></asp:TextBox><br />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <table>
                                    <tr>
                                        <td valign="middle" align="left">
                                            <asp:TextBox ID="txtAddDesc" runat="server" TextMode="MultiLine" Text='<%# Bind("Specification") %>'></asp:TextBox><br />
                                        </td>
                                    </tr>
                                </table>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblDesc" runat="server" Text='<%# Bind("Specification") %>'></asp:Label>
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
</asp:Content>
