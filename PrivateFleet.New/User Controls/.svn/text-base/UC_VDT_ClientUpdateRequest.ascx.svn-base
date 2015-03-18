<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UC_VDT_ClientUpdateRequest.ascx.cs"
    Inherits="User_Controls_UC_VDT_ClientUpdateRequest" %>
<table width="100%">
    <tr>
        <td align="center">
            <table>
                <tr>
                    <td align="left">
                        Make 
                    </td>
                    <td align="left">
                                      <asp:DropDownList runat="server" ID="drpMake" OnSelectedIndexChanged="drpMake_SelectedIndexChanged" Width ="150"
                            AutoPostBack="true">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" ID="requiredMake" ControlToValidate="drpMake"
                            ErrorMessage="Select Make." InitialValue="0" Display="None" ValidationGroup="ClinetUpate"></asp:RequiredFieldValidator>
                        <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="callountMake" TargetControlID="requiredMake">
                        </ajaxToolkit:ValidatorCalloutExtender>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        For
                        </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlSearch" runat="server" Height="21px" Width ="150" Display="None"
                            InitialValue="-Select-" AppendDataBoundItems="True" >
                            <asp:ListItem Value="0">-Select Criteria-</asp:ListItem>
                            <asp:ListItem Value="1">Today</asp:ListItem>
                            <asp:ListItem Value="2">Yesterday</asp:ListItem>
                            <asp:ListItem Value="3">Last 7 days</asp:ListItem>
                            <asp:ListItem Value="4">This month</asp:ListItem>
                            <asp:ListItem Value="5">Last Month</asp:ListItem>
                            <asp:ListItem Value="6">All Time</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td align="left">
                     <%--   <asp:Button runat="server" ID="btnSubmit" Text="Submit" CausesValidation="true" OnClick="btnSubmit_Click"
                            ValidationGroup="ClinetUpate" />--%>
                            
                               <asp:ImageButton ID="imgbtnSubmit" runat="server" ImageUrl="~/Images/Submit.gif"
                                    OnClick="btnSubmit_Click" onmouseout="this.src='Images/Submit.gif'" onmouseover="this.src='Images/Submit_hvr.gif'"
                                    ValidationGroup="ClinetUpate" />
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
    <tr>
        <td>
            <asp:GridView runat="server" ID="grdVechileDelivaryReport" Width="100%" EmptyDataText="No Records Found."
                AutoGenerateColumns="false" AllowPaging="true" RowStyle-Height="30px" OnPageIndexChanging="grdVechileDelivaryReport_PageIndexChanging"
                PageSize="10" AllowSorting="true" OnSorting="gv_Sorting" OnRowDataBound ="grdVechileDelivaryReport_RowDataBound">
                <FooterStyle CssClass="gvFooterrow" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <PagerStyle CssClass="pgr" />
                <HeaderStyle BackColor="#0A73A2" Font-Bold="True" CssClass="gvHeader" Height="30px" />
                <Columns>
                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                        ItemStyle-CssClass="grid_padding" HeaderText="Customer Name" ItemStyle-Width="20%"
                        SortExpression="fullname" >
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblCustmerName" Text='<%# bind("fullname") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                        ItemStyle-CssClass="grid_padding" ItemStyle-Width="20%"  >
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
                        ItemStyle-Width="20%" HeaderText="Make" SortExpression="Make" ItemStyle-CssClass ="grid_padding" >
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblMake" Text='<%# bind("make") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                        ItemStyle-CssClass="grid_padding" ItemStyle-Width="20%" HeaderText="Requested Date" SortExpression="Make" >
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblLastUpdate" Text='<%# bind("Lastentery","{0:dd MMM yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                  
                    
                     <asp:TemplateField>
                    <ItemTemplate>
                    
                   
                   
                    <asp:HiddenField runat ="server" ID="hidenClientid" Value ='<%# bind("ID") %>'/>
                        <asp:HyperLink runat ="server" ID ="hypupdate" Text ="Update"  NavigateUrl=""> </asp:HyperLink>
                    </ItemTemplate>
                    
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </td>
    </tr>

</table>
