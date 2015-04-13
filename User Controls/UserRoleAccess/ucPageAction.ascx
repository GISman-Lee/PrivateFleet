<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucPageAction.ascx.cs"
    Inherits="User_Controls_ucPageAction" %>
<table width="95%" align="center">
    <tr>
        <td style="height: 25px;">
            <asp:Label ID="lblMsg" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 83px">
            Select Page :
        </td>
        <td>
            <asp:DropDownList ID="ddlPages" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPages_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
    </tr>
</table>
<table width="95%" align="center" id="tblActions" runat="server" visible="false">
    <tr>
        <td style="height: 10px;">
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="gvActions" runat="server" AutoGenerateColumns="False" BorderColor="#CCCCCC"
                BorderWidth="1px" Width="50%" CellPadding="3" DataKeyNames="ID" OnRowDataBound="gvActions_RowDataBound">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server" />
                        </ItemTemplate>
                        <ItemStyle Width="5%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:Label ID="lblAction" runat="server" Text='<%#Bind("Action") %>' Style="padding-left: 10px"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle ForeColor="navy" />
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td style="height: 10px;">
        </td>
    </tr>
    <tr>
        <td>
            <asp:ImageButton ID="ibtnSubmit" runat="server" ImageUrl="~/Images/Submit.gif" ImageAlign="AbsMiddle"
                OnClick="ibtnSubmit_Click" />
        </td>
    </tr>
</table>
