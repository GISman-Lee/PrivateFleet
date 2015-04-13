<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucAdditionalAccessories.ascx.cs"
    Inherits="User_Controls_Request_ucAdditionalAccessories" %>
<table width="90%" align="left">
    <tr>
        <td class="subheading">
            <strong>Additional Accessories</strong>
        </td>
    </tr>
    <tr>
        <td>
            Select Additional Accessories :
            <asp:DropDownList ID="ddlAccessories" runat="server" Width="213px">
            </asp:DropDownList>&nbsp;
            <asp:ImageButton ID="ibtnAdd" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/Images/AddAccessory.gif"
                OnClick="ibtnAdd_Click" ValidationGroup="VGNewAccessory" />&nbsp;
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="gvAccessories" runat="server" AutoGenerateColumns="False" CellPadding="3"
                CellSpacing="1" Width="100%" OnRowCommand="gvAccessories_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="Accessory">
                        <ItemTemplate>
                            <asp:HiddenField ID="hdfID" runat="Server" Value='<%# Bind("ID") %>' />
                            <asp:Label ID="lblAccessory" runat="server" Text='<%# Bind("accessoryname") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddAccessory" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvAccesory" runat="server" ControlToValidate="txtAddAccessory"
                                Display="None" ErrorMessage="Please enter The accessory name here <br> or Click Cancel "
                                SetFocusOnError="True" ValidationGroup="VGNewAccessory"></asp:RequiredFieldValidator><ajaxToolkit:ValidatorCalloutExtender
                                    ID="ValidatorCalloutExtender9" runat="server" HighlightCssClass="validatorCalloutHighlight"
                                    TargetControlID="rfvAccesory">
                                </ajaxToolkit:ValidatorCalloutExtender>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Specification">
                        <ItemTemplate>
                            <asp:TextBox ID="txtSpec" runat="server" TextMode="MultiLine" Rows="3" Width="400px"
                                Text='<%#Bind("Specification") %>'></asp:TextBox>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddSpec" runat="server" Rows="3" Text='<%#Bind("Specification") %>'
                                TextMode="MultiLine" Width="400px"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkSave" runat="server" Text="Save" CommandName="SaveAccessory"
                                CssClass="activeLink">
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkRemove" runat="server" Text="Remove" CommandName="RemoveAccessory"
                                CssClass="activeLink">
                            </asp:LinkButton>
                            <asp:HiddenField ID="hdfIsDBDriven" runat="server" Value='<%# Bind("IsDBDriven") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <FooterTemplate>
                            <asp:ImageButton ID="ibtnSaveAddtional" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/Images/Submit.gif"
                                OnClick="ibtnSaveAddtional_Click" ValidationGroup="VGNewAccessory" onmouseout="this.src='Images/Submit.gif'"
                                onmouseover="this.src='Images/Submit_hvr.gif'" />&nbsp;
                            <asp:ImageButton ID="imgbtnCancel" runat="server" CausesValidation="False" ImageUrl="~/Images/Cancel.gif"
                                OnClick="imgbtnCancel_Click" onmouseout="this.src='Images/Cancel.gif'" onmouseover="this.src='Images/Cancel_hvr.gif'" />
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle ForeColor="Navy" />
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:ImageButton ID="ibtnSave" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/Images/Submit.gif"
                OnClick="ibtnSave_Click" /></td>
    </tr>
</table>
