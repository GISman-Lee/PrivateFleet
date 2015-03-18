<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UC_VDT_DealerResponseReport.ascx.cs"
    Inherits="User_Controls_UC_VDT_DealerResponseReport" %>
    <asp:Panel runat="server" ID="pnlDealerResponseReport_1" Visible="true" DefaultButton="imgbtnSubmit">
<table width="100%">
    <tr>
        <td align="center">
            <table>
                <tr>
                    <td align="left">
                        Make
                    </td>
                    <td align="left">
                        <asp:DropDownList runat="server" ID="drpMake" OnSelectedIndexChanged="drpMake_SelectedIndexChanged"
                            AutoPostBack="true">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" ID="requiredMake" ControlToValidate="drpMake"
                            ErrorMessage="Select Make." InitialValue="0" Display="None" ValidationGroup="DealerResponse"></asp:RequiredFieldValidator>
                        <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="callountMake" TargetControlID="requiredMake">
                        </ajaxToolkit:ValidatorCalloutExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td align="left">
                        <%-- <asp:Button runat="server" ID="btnSubmit" Text="Submit" CausesValidation="true" OnClick="btnSubmit_Click"
                            ValidationGroup="DealerResponse" />--%>
                        <asp:ImageButton ID="imgbtnSubmit" runat="server" ImageUrl="~/Images/Submit.gif"
                            OnClick="btnSubmit_Click" onmouseout="this.src='Images/Submit.gif'" onmouseover="this.src='Images/Submit_hvr.gif'"
                            ValidationGroup="DealerResponse" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td style="height: 4px">
        </td>
    </tr>
    
    <tr>
        
    </tr>
    <tr>
        <td>
            <table width="100%"          >
               
                <tr>
                    <td runat ="server" id="DealerNonResponseLowervalue">
                    <div  style="width:20px; height :6px; background-color:#ffc3c3;">
                    
                    </div>
                    </td>
                    <td colspan ="2">
                        <asp:Label runat ="server" ID ="lblDealerNonResponseLowervalue"></asp:Label>
                    </td>
                    
                     <td align="right" style="padding-right: 10px">
            <asp:Label ID="lblRowsToDisplay2" runat="server">Rows To Display:</asp:Label>
            <asp:DropDownList ID="ddl_NoRecords2" runat="server" AutoPostBack="true" Width="50px"
                OnSelectedIndexChanged="ddl_NoRecords2_SelectedIndexChanged">
                <asp:ListItem Value="10">10</asp:ListItem>
                <asp:ListItem Value="20">20</asp:ListItem>
                <asp:ListItem Value="30">30</asp:ListItem>
                <asp:ListItem Value="50">50</asp:ListItem>
                <asp:ListItem Value="All">All</asp:ListItem>
            </asp:DropDownList>
        </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView runat="server" ID="grdVechileDelivaryReport" Width="100%" EmptyDataText="No Records Found."
                AutoGenerateColumns="false" AllowPaging="true" RowStyle-Height="30px" OnPageIndexChanging="grdVechileDelivaryReport_PageIndexChanging"
                PageSize="10" AllowSorting="true" OnSorting="gv_Sorting" OnRowDataBound="grdVechileDelivaryReport_RowDataBound">
                <FooterStyle CssClass="gvFooterrow" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <PagerStyle CssClass="pgr" />
                <HeaderStyle BackColor="#0A73A2" Font-Bold="True" CssClass="gvHeader" Height="30px" />
                <Columns>
                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                        ItemStyle-CssClass="grid_padding" HeaderText="Customer Name" ItemStyle-Width="20%"
                        SortExpression="fullname"  >
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblCustmerName" Text='<%# bind("fullname") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                        ItemStyle-CssClass="grid_padding" ItemStyle-Width="20%" >
                        <HeaderTemplate>
                            Email
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblEmail" Text='<%# bind("email") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                        ItemStyle-CssClass="grid_padding" ItemStyle-Width="20%"  >
                        <HeaderTemplate>
                            Phone
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblPhone" Text='<%# bind("phone") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                        ItemStyle-CssClass="grid_padding" ItemStyle-Width="20%" HeaderText="Make" SortExpression="Make"  >
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblMake" Text='<%# bind("make") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                        ItemStyle-CssClass="grid_padding" ItemStyle-Width="20%" HeaderText="Last Update"  
                        SortExpression="lastupdate">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblETA" Text='<%# bind("lastupdate","{0:dd MMM yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:HiddenField runat="server" ID="hiddenhighvalue" Value='<%# bind("highvalue") %>' />
                            <asp:HiddenField runat="server" ID="HiddenLowerActucalval" Value='<%# bind("lowervalue") %>' />
                            <asp:HiddenField runat="server" ID="hiddendiff" Value='<%# bind("diff") %>' />
                            <asp:HiddenField runat="server" ID="hidenClientid" Value='<%# bind("ID") %>' />
                            <asp:HyperLink runat="server" ID="hypupdate" Text="Update" NavigateUrl=""> </asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </td>
    </tr>
</table>
</asp:Panel>