<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCMakeDealer.ascx.cs"
    Inherits="User_Controls_UCMakeDealer" %>
<link href="../CSS/stylesheet.css" rel="stylesheet" type="text/css" />
<asp:UpdatePanel ID="UP2" runat="server">
    <ContentTemplate>
        <table width="95%" align="center">
            <tr>
                <td colspan="2" style="height: 30px" valign="middle" align="center">
                    <asp:Label ID="lblResult" runat="server" CssClass="dbresult"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right" style="padding-left: 100px">
                    <asp:Label ID="Label1" runat="server" CssClass="label">Make:</asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlMake" runat="server" AutoPostBack="True" Width="261px" OnSelectedIndexChanged="ddlMake_SelectedIndexChanged"
                        DataTextField="Make" DataValueField="ID" AppendDataBoundItems="True">
                    </asp:DropDownList><br />
                    <asp:RequiredFieldValidator ID="MakeReq" runat="server" ErrorMessage="Please select the Make."
                        Display="dynamic" ValidationGroup="VGSubmit" ControlToValidate="ddlMake" InitialValue="-Select-"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right" style="padding-left: 100px">
                    <asp:Label ID="Label2" runat="server" CssClass="label">Dealer:</asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlDealer" runat="server" Width="261px" DataTextField="Dealer"
                        DataValueField="ID" AppendDataBoundItems="True">
                    </asp:DropDownList><br />
                    <asp:RequiredFieldValidator ID="DealerReq" runat="server" ErrorMessage="Please select the Dealer."
                        Display="dynamic" ValidationGroup="VGSubmit" ControlToValidate="ddlDealer" InitialValue="-Select-"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    &nbsp;
                    <asp:ImageButton ID="imgbtnAdd" runat="server" ImageUrl="~/Images/Submit.gif" onmouseover="this.src='Images/Submit_hvr.gif'"
                        onmouseout="this.src='Images/Submit.gif'" OnClick="imgbtnAdd_Click" />&nbsp;
                    <asp:ImageButton ID="imgbtnCancel" runat="server" ImageUrl="~/Images/Cancel.gif"
                        onmouseover="this.src='Images/Cancel_hvr.gif'" onmouseout="this.src='Images/Cancel.gif'"
                        OnClick="imgbtnCancel_Click" /></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="gvMakeDealerDetails" runat="server" AutoGenerateColumns="False"
                        OnRowCommand="gvMakeDealerDetails_RowCommand" OnRowDataBound="gvMakeDealerDetails_RowDataBound"
                        Width="100%" DataKeyNames="ID,MakeID,DealerID" AllowPaging="True" OnPageIndexChanging="gvMakeDealerDetails_PageIndexChanging"
                        OnRowEditing="gvMakeDealerDetails_RowEditing" OnSelectedIndexChanging="gvMakeDealerDetails_SelectedIndexChanging"
                        BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                        CellPadding="3" AllowSorting="True" OnSorting="gv_Sorting">
                        <Columns>
                            <asp:TemplateField HeaderText="Dealer" SortExpression="Dealer">
                                <EditItemTemplate>
                                    <br />
                                    &nbsp;
                                </EditItemTemplate>
                                <FooterTemplate>
                                    &nbsp;<br />
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Image ID="imgActive" runat="server" Height="10px" ImageUrl="~/Images/active_bullate.jpg"
                                        Width="10px" />
                                    <asp:Label ID="lblDealer" runat="server" Text='<%# Bind("Dealer") %>' Style="padding-left: 10px"></asp:Label>&nbsp;&nbsp;&nbsp;
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
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkbtnActiveness" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                        CommandName="Activeness" Style="padding-left: 10px" CssClass="activatelink">Activate</asp:LinkButton>&nbsp;
                                    <asp:HiddenField ID="hdfIsActive" runat="server" Value='<%# Bind("IsActive") %>' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle CssClass="gvFooterrow" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <PagerStyle CssClass="pgr" />
                        <HeaderStyle BackColor="#0A73A2" Font-Bold="True" CssClass="gvHeader" Height="30px" />
                    </asp:GridView>
                    <asp:HiddenField ID="hdfID" runat="server" Value='<%# Bind("ID") %>' />
                    &nbsp;&nbsp;
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
