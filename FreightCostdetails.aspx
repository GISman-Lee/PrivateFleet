<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="FreightCostdetails.aspx.cs" Inherits="FreightCostdetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <br />
    <table align="center" width="98%">
        <tr>
            <td>
                <asp:Label ID="lblMsg" runat="server" Text="Label" CssClass="dbresult"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvFreightCost" runat="server" AutoGenerateColumns="False" ShowFooter="false"
                    BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" DataKeyNames="fromSID"
                    BorderWidth="1px" Width="100%" CellPadding="3" AllowSorting="True" PageSize="10"
                    OnRowEditing="gvFreightCost_RowEditing" OnRowUpdating="gvFreightCost_RowUpdating"
                    OnRowDataBound="gvFreightCost_RowDataBound" >
                    <Columns>
                        <asp:TemplateField HeaderText="" SortExpression="Role">
                            <EditItemTemplate>
                                <asp:Label ID="lblFromS" runat="server" Text='<%#Bind("fromS") %>' Style="padding-left: 10px"></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblFromS1" runat="server" Text='<%#Bind("fromS") %>' Style="padding-left: 10px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center " Width="100" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ACT">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEditACT" runat="server" Text='<%#Bind("ACT") %>' Width="70"></asp:TextBox>
                                 <%--<AjaxToolkit:FilteredTextBoxExtender ID="FltrACT" runat="server" FilterType="Custom,Numbers"  ValidChars="." TargetControlID="txtEditACT" ></AjaxToolkit:FilteredTextBoxExtender>--%>
                                 <asp:CompareValidator Display="Dynamic" runat="server" ID="cvType" ErrorMessage="Please Enter Decimal Value." Type="Double" ControlToValidate="txtEditACT" Operator="DataTypeCheck"></asp:CompareValidator>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblACT" runat="server" Text='<%#Bind("ACT") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Height="30" />
                            <ItemStyle Height="30" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="NSW">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEditNSW" runat="server" Text='<%#Bind("NSW")%>' Width="70"></asp:TextBox>
                                 <AjaxToolkit:FilteredTextBoxExtender ID="FltrNSW" runat="server" FilterType="Custom,Numbers"  ValidChars="." TargetControlID="txtEditNSW" ></AjaxToolkit:FilteredTextBoxExtender>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblNSW" runat="server" Text='<%#Bind("NSW") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="QLD">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEditQLD" runat="server" Text='<%#Bind("QLD") %>' Width="70"></asp:TextBox>
                                 <AjaxToolkit:FilteredTextBoxExtender ID="FltrQLD" runat="server" FilterType="Custom,Numbers"  ValidChars="." TargetControlID="txtEditQLD" ></AjaxToolkit:FilteredTextBoxExtender>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblQLD" runat="server" Text='<%#Bind("QLD") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="VIC">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEditVIC" runat="server" Text='<%#Bind("VIC") %>' Width="70"></asp:TextBox>
                                 <AjaxToolkit:FilteredTextBoxExtender ID="FltrVIC" runat="server" FilterType="Custom,Numbers"  ValidChars="." TargetControlID="txtEditVIC" ></AjaxToolkit:FilteredTextBoxExtender>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblVIC" runat="server" Text='<%#Bind("VIC") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SA">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEditSA" runat="server" Text='<%#Bind("SA") %>' Width="70"></asp:TextBox>
                                <AjaxToolkit:FilteredTextBoxExtender ID="FltrSA" runat="server" FilterType="Custom,Numbers"  ValidChars="." TargetControlID="txtEditSA" ></AjaxToolkit:FilteredTextBoxExtender>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblSA" runat="server" Text='<%#Bind("SA") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                          <asp:TemplateField HeaderText="WA">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEditWA" runat="server" Text='<%#Bind("WA") %>' Width="70"></asp:TextBox>
                                <AjaxToolkit:FilteredTextBoxExtender ID="FltrWA" runat="server" FilterType="Custom,Numbers"  ValidChars="." TargetControlID="txtEditWA" ></AjaxToolkit:FilteredTextBoxExtender>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblWA" runat="server" Text='<%#Bind("WA") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="TAS">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEditTAS" runat="server" Text='<%#Bind("TAS") %>' Width="70"></asp:TextBox>
                                   <AjaxToolkit:FilteredTextBoxExtender ID="FltrTAS" runat="server" FilterType="Custom,Numbers"  ValidChars="." TargetControlID="txtEditTAS" ></AjaxToolkit:FilteredTextBoxExtender>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblTAS" runat="server" Text='<%#Bind("TAS") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
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
                            <ItemStyle Width="60" />
                        </asp:TemplateField>
                    </Columns>
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <PagerStyle CssClass="pgr" />
                    <HeaderStyle BackColor="#0A73A2" Font-Bold="True" CssClass="gvHeader" Height="30px" />
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
