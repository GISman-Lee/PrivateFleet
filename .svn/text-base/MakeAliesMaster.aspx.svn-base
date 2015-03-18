<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="MakeAliesMaster.aspx.cs" Inherits="MakeAliesMaster" Title="Make Alies Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" align="center">
        <tr>
            <td style="height: 30px" valign="middle" align="left">
                <asp:Label ID="lblResult" runat="server" CssClass="dbresult"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="right" colspan="2">
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
            <td>
                <asp:GridView ID="gvMakeAlies" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                    Width="100%" AllowPaging="True" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None"
                    BorderWidth="1px" CellPadding="3" OnPageIndexChanging="gvMakeAlies_PageIndexChanging"
                    OnRowDataBound="gvMakeAlies_RowDataBound" OnSorting="gvMakeAlies_Sorting">
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
                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2_makeAlies" runat="server"
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
                        <asp:TemplateField HeaderText="Make Alies" SortExpression="Alies">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEditAlies" runat="server" Text='<%# Bind("Alies") %>'></asp:TextBox><br />
                                <asp:RequiredFieldValidator ID="ModelReq" runat="server" ControlToValidate="txtEditAlies"
                                    Display="None" ErrorMessage="Please Enter Alies" ValidationGroup="EditGroup"
                                    CssClass="gvValidationError" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
                                    HighlightCssClass="validatorCalloutHighlight" TargetControlID="ModelReq">
                                </ajaxToolkit:ValidatorCalloutExtender>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <table>
                                    <tr>
                                        <td valign="middle" align="left">
                                            <asp:TextBox ID="txtAlies" runat="server"></asp:TextBox><br />
                                        </td>
                                    </tr>
                                </table>
                                <asp:RequiredFieldValidator ID="ModelAddReq" runat="server" ControlToValidate="txtAlies"
                                    Display="None" ErrorMessage="Please Enter Alies" ValidationGroup="VGAdd" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server"
                                    HighlightCssClass="validatorCalloutHighlight" TargetControlID="ModelAddReq">
                                </ajaxToolkit:ValidatorCalloutExtender>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblAlies" runat="server" Text='<%# Bind("Alies") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <EditItemTemplate>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <table>
                                    <tr>
                                        <td align="left" valign="middle">
                                            <asp:ImageButton ID="imgbtnAdd" runat="server" ImageUrl="~/Images/Save1.png" ToolTip="Add Record"
                                                OnClick="imgbtnAdd_Click" ValidationGroup="VGAdd" Style="padding-left: 10px"
                                                Height="15px" ImageAlign="Middle" Width="48px" />
                                        </td>
                                    </tr>
                                </table>
                            </FooterTemplate>
                            <ItemStyle HorizontalAlign="Left" />
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
