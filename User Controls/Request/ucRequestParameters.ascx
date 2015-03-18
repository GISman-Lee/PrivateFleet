<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucRequestParameters.ascx.cs"
    Inherits="User_Controls_Request_ucRequestParameters" Debug="true" %>
<table width="100%" align="left" border="0">
    <tr>
        <td class="subheading">
            <strong>Request Parameters</strong>
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="gvParameters" runat="server" AutoGenerateColumns="False" CellPadding="3"
                CellSpacing="1" Width="100%" DataKeyNames="ID" BorderColor="#ACACAC" OnDataBound="gvParameters_DataBound"
                OnRowDataBound="gvParameters_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="Parameter">
                        <ItemTemplate>
                            <asp:Label ID="lblAccessory" runat="server" Text='<%#Bind("accessoryname") %>'></asp:Label>
                            <asp:HiddenField ID="hdfID" runat="server" Value='<%#Bind("ID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle ForeColor="Navy" />
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td>
            <table width="100%" align="left">
                <tr>
                    <td class="subheading">
                        <strong>Additional Accessories</strong>
                    </td>
                </tr>
                <tr>
                    <td>
                        Select Additional Accessories :
                        <asp:DropDownList ID="ddlAccessories" runat="server" Width="213px" Height="21px"
                            OnSelectedIndexChanged="ddlAccessories_SelectedIndexChanged" AutoPostBack="True">
                        </asp:DropDownList>
                        &nbsp;
                        <asp:ImageButton ID="ibtnAdd" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/Images/AddAccessory.gif"
                            OnClick="ibtnAdd_Click" ValidationGroup="VGNewAccessory" Visible="False" />&nbsp;
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
                                    <ItemStyle Width="30%" />
                                    <%--    <FooterTemplate>
                                       <table style="height: 100%" cellpadding="0" cellspacing="0">
                                            <tr id="trFirst" runat="server" style="height: 70px">
                                                <td>
                                                    <asp:TextBox ID="txtAddAccessory" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvAccesory" runat="server" ControlToValidate="txtAddAccessory"
                                                        Display="None" ErrorMessage="Please enter The accessory name here <br> or Click Cancel "
                                                        SetFocusOnError="True" ValidationGroup="VGNewAccessory"></asp:RequiredFieldValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server"
                                                        HighlightCssClass="validatorCalloutHighlight" TargetControlID="rfvAccesory">
                                                    </ajaxToolkit:ValidatorCalloutExtender>
                                                </td>
                                            </tr>
                                            <tr id="trSecond" runat="server" style="height: 70px">
                                                <td>
                                                    <asp:TextBox ID="txtAddAccessory1" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAddAccessory1"
                                                        Display="None" ErrorMessage="Please enter The accessory name here <br> or Click Cancel "
                                                        SetFocusOnError="True" ValidationGroup="VGNewAccessory1"></asp:RequiredFieldValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                                        HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator1">
                                                    </ajaxToolkit:ValidatorCalloutExtender>
                                                </td>
                                            </tr>
                                            <tr id="trThird" runat="server" style="height: 70px">
                                                <td>
                                                    <asp:TextBox ID="txtAddAccessory2" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAddAccessory2"
                                                        Display="None" ErrorMessage="Please enter The accessory name here <br> or Click Cancel "
                                                        SetFocusOnError="True" ValidationGroup="VGNewAccessory2"></asp:RequiredFieldValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                                        HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator2">
                                                    </ajaxToolkit:ValidatorCalloutExtender>
                                                </td>
                                            </tr>
                                        </table>
                                    </FooterTemplate>--%>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Specification">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtSpec" runat="server" TextMode="MultiLine" Rows="1" Width="400px"
                                            Text='<%#Bind("Specification") %>'></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="60%" />
                                    <%--     <FooterTemplate>
                                     <table cellpadding="0" cellspacing="0">
                                            <tr id="trFirst1" runat="server">
                                                <td>
                                                    <asp:TextBox ID="txtAddSpec" runat="server" Rows="3" Text='<%#Bind("Specification") %>'
                                                        TextMode="MultiLine" Width="400px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr id="trSecond1" runat="server">
                                                <td>
                                                    <asp:TextBox ID="txtAddSpec1" runat="server" Rows="3" Text='<%#Bind("Specification") %>'
                                                        TextMode="MultiLine" Width="400px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr id="trThird1" runat="server">
                                                <td>
                                                    <asp:TextBox ID="txtAddSpec2" runat="server" Rows="3" Text='<%#Bind("Specification") %>'
                                                        TextMode="MultiLine" Width="400px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </FooterTemplate>--%>
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
                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                    <%--    <FooterTemplate>
                                      <table cellpadding="0" cellspacing="0">
                                            <tr id="trFirst2" runat="server" style="height: 70px">
                                                <td>
                                                    <asp:ImageButton ID="ibtnSaveAddtional" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/Images/Submit.gif"
                                                        OnClick="ibtnSaveAddtional_Click" ValidationGroup="VGNewAccessory" onmouseout="this.src='Images/Submit.gif'"
                                                        onmouseover="this.src='Images/Submit_hvr.gif'" />&nbsp;
                                                    <asp:ImageButton ID="imgbtnCancel" runat="server" CausesValidation="False" ImageUrl="~/Images/Cancel.gif"
                                                        OnClick="imgbtnCancel_Click" onmouseout="this.src='Images/Cancel.gif'" onmouseover="this.src='Images/Cancel_hvr.gif'" />
                                                </td>
                                            </tr>
                                            <tr id="trSecond2" runat="server" style="height: 70px">
                                                <td>
                                                    <asp:ImageButton ID="ibtnSaveAddtional1" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/Images/Submit.gif"
                                                        OnClick="ibtnSaveAddtional1_Click" ValidationGroup="VGNewAccessory1" onmouseout="this.src='Images/Submit.gif'"
                                                        onmouseover="this.src='Images/Submit_hvr.gif'" />&nbsp;
                                                    <asp:ImageButton ID="imgbtnCancel1" runat="server" CausesValidation="False" ImageUrl="~/Images/Cancel.gif"
                                                        OnClick="imgbtnCancel1_Click" onmouseout="this.src='Images/Cancel.gif'" onmouseover="this.src='Images/Cancel_hvr.gif'" />
                                                </td>
                                            </tr>
                                            <tr id="trThird2" runat="server" style="height: 70px">
                                                <td>
                                                    <asp:ImageButton ID="ibtnSaveAddtional2" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/Images/Submit.gif"
                                                        OnClick="ibtnSaveAddtional2_Click" ValidationGroup="VGNewAccessory2" onmouseout="this.src='Images/Submit.gif'"
                                                        onmouseover="this.src='Images/Submit_hvr.gif'" />&nbsp;
                                                    <asp:ImageButton ID="imgbtnCancel2" runat="server" CausesValidation="False" ImageUrl="~/Images/Cancel.gif"
                                                        OnClick="imgbtnCancel2_Click" onmouseout="this.src='Images/Cancel.gif'" onmouseover="this.src='Images/Cancel_hvr.gif'" />
                                                </td>
                                            </tr>
                                        </table>--%>
                                    <%--  <asp:ImageButton ID="ibtnSaveAddtional" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/Images/Submit.gif"
                                            OnClick="ibtnSaveAddtional_Click" ValidationGroup="VGNewAccessory" onmouseout="this.src='Images/Submit.gif'"
                                            onmouseover="this.src='Images/Submit_hvr.gif'" />&nbsp;
                                        <asp:ImageButton ID="imgbtnCancel" runat="server" CausesValidation="False" ImageUrl="~/Images/Cancel.gif"
                                            OnClick="imgbtnCancel_Click" onmouseout="this.src='Images/Cancel.gif'" onmouseover="this.src='Images/Cancel_hvr.gif'" />
                                    </FooterTemplate>--%>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle ForeColor="Navy" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblMsgAcc" Font-Bold="true" ForeColor="Red" Text="* Please Enter Accessory Name"
                            Visible="false" runat="server"></asp:Label>
                        <asp:Label ID="lblMsgAcc_1" Font-Bold="true" ForeColor="Red" Text="* Please save Additional Accessory before you proceed."
                            Visible="false" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvAddAcc" runat="server" AutoGenerateColumns="False" CellPadding="3"
                            CellSpacing="1" Width="100%" OnRowCommand="gvAddAcc_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="Accessory">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdfID" runat="Server" Value='<%# Bind("ID") %>' />
                                        <%-- <asp:Label ID="lblAccessory" runat="server" Width="275" Text='<%# Bind("accessoryname") %>'></asp:Label>--%>
                                        <asp:TextBox ID="txtAddAccessory" runat="server"></asp:TextBox>
                                        <%--  <asp:RequiredFieldValidator ID="rfvAccesory" runat="server" ControlToValidate="txtAddAccessory"
                                            Display="None" ErrorMessage="Please enter The accessory name here <br> or Click Cancel "
                                            SetFocusOnError="True" ValidationGroup="VGNewAccessory"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server"
                                            HighlightCssClass="validatorCalloutHighlight" TargetControlID="rfvAccesory">
                                        </ajaxToolkit:ValidatorCalloutExtender>--%>
                                    </ItemTemplate>
                                    <ItemStyle Width="30%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Specification">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtAddSpec" Width="400px" runat="server" Rows="3" Text='<%#Bind("Specification") %>'
                                            TextMode="MultiLine">
                                        </asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="60%" />
                                </asp:TemplateField>
                                <asp:TemplateField Visible="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkSave" runat="server" Text="Save" CommandName="SaveAccessory">
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <table>
                                            <tr>
                                                <td align="left">
                                                    <asp:HiddenField ID="hdfIsDBDriven" runat="server" Value='<%# Bind("IsDBDriven") %>' />
                                                    <asp:ImageButton ID="ibtnSaveAddtional" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/Images/Submit.gif"
                                                        CommandName="AddSubmit" onmouseout="this.src='Images/Submit.gif'" onmouseover="this.src='Images/Submit_hvr.gif'" />
                                                 <%--   <asp:TextBox ID="txtTemp" runat="server" Width="0.001pt" Height="0.001pt"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RFV" runat="server" ControlToValidate="txtTemp" Display="None"
                                                        ErrorMessage="Please enter The accessory name here <br> or Click Cancel " SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="VCE" runat="server" HighlightCssClass="validatorCalloutHighlight"
                                                        TargetControlID="RFV" PopupPosition="Left">
                                                    </ajaxToolkit:ValidatorCalloutExtender>--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:ImageButton ID="imgbtnCancel" runat="server" ImageUrl="~/Images/Cancel.gif"
                                                        onmouseout="this.src='Images/Cancel.gif'" CommandName="AddCancel" onmouseover="this.src='Images/Cancel_hvr.gif'" />
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle ForeColor="Navy" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:ImageButton ID="ibtnSave" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/Images/Submit.gif"
                            OnClick="ibtnSave1_Click" Visible="false" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
