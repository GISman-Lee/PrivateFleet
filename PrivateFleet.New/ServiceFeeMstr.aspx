<%@ Page Title="Handling Fee Master" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ServiceFeeMstr.aspx.cs" Inherits="ServiceFeeMstr" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<asp:GridView ID="gv" runat="server" AutoGenerateColumns ="false" ShowFooter="false"  DataKeyNames ="ID"  BorderColor ="#CCCCCC" BorderStyle ="None" 
      AllowSorting ="true" Width="100%" BorderWidth ="1px" CellPadding ="3" PageSize ="10" >
       <Columns>
                <asp:TemplateField HeaderText="Role" SortExpression="Role">
                   
         
                    <ItemTemplate>
                        <asp:Label ID="lblRole" runat="server" Text='<%#Bind("State") %>' Style="padding-left: 10px"></asp:Label>
                    </ItemTemplate>
                    
                    <HeaderStyle HorizontalAlign="Center" ForeColor="Black" />
                </asp:TemplateField>
                </Columns> 
    </asp:GridView>--%>
    <br />
    <br />
    <br />
    <table align="center" width="95%">
        <tr>
            <td>
                <asp:Label ID="lblMsg" runat="server" Text="Label" CssClass="dbresult"></asp:Label>
                
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvServiceFee" runat="server" AutoGenerateColumns="False" ShowFooter="false"
                    DataKeyNames="ID" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None"
                    BorderWidth="1px" Width="100%" CellPadding="3" AllowSorting="True" PageSize="10"
                    OnRowEditing="gvServiceFee_RowEditing" OnRowUpdating="gvServiceFee_RowUpdating"  OnRowDataBound ="gvServiceFee_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="State">
                            <EditItemTemplate>
                                <asp:Label ID="lblState1" runat="server" Text='<%#Bind("State") %>' Style="padding-left: 10px"></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate >
                                <asp:Label ID="lblState" runat="server" Text='<%#Bind("State") %>' Style="padding-left: 10px" ></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="120" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Handling Fee">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEditFee" runat="server" Style="padding-left: 10px" Text='<%#Bind("Fee") %>'></asp:TextBox>
                                <AjaxToolkit:FilteredTextBoxExtender ID="ftetxtexewarr" runat="server" FilterType="Custom,Numbers"  ValidChars="." TargetControlID="txtEditFee" ></AjaxToolkit:FilteredTextBoxExtender>
                                                  
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblValue" runat="server" Style="padding-left: 10px" Text='<%#Bind("Fee") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Height="30" />
                            <ItemStyle Height="30" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Rego CTP Charges">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEditRegoCTP" runat="server" Style="padding-left: 10px" Text='<%#Bind("RegoCTP") %>'></asp:TextBox>
                                <AjaxToolkit:FilteredTextBoxExtender ID="ftetxtexewarr1" runat="server" FilterType="Numbers"  TargetControlID="txtEditRegoCTP" ></AjaxToolkit:FilteredTextBoxExtender>
                                                  
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblEditRegoCTP" runat="server" Style="padding-left: 10px" Text='<%#Bind("RegoCTP") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Height="30" />
                            <ItemStyle Height="30" />
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
