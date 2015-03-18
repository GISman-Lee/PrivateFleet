<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ProspectReport.aspx.cs" Inherits="ProspectReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellpadding="3" cellspacing="2" style="border: solid 1px White;" width="100%">
        <tr align="left">
            <td colspan="2" align="left">
                <strong>Search Criteria:</strong>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="lblFromDate" runat="server" Text="Created Date From"></asp:Label>
            </td>
            <td align="left">
                <asp:TextBox ID="txtCalenderFrom" runat="server" Width="119px" ReadOnly="true"></asp:TextBox>
                <asp:Image ID="imgCal" runat="server" ImageUrl="~/Images/Calendar.gif" Visible="false" />
                <ajaxToolkit:CalendarExtender ID="calFrom" runat="server" TargetControlID="txtCalenderFrom"
                    PopupButtonID="txtCalenderFrom" Format="dd/MM/yyyy" Animated="true">
                </ajaxToolkit:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="lblToDate" runat="server" Text="Created Date To"></asp:Label>
            </td>
            <td align="left">
                <asp:TextBox ID="txtCalenderToDate" runat="server" Width="119px" ReadOnly="true"></asp:TextBox>
                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Calendar.gif" Visible="false" />
                <ajaxToolkit:CalendarExtender ID="calTO" runat="server" TargetControlID="txtCalenderToDate"
                    PopupButtonID="txtCalenderToDate" Format="dd/MM/yyyy">
                </ajaxToolkit:CalendarExtender>
            </td>
        </tr>
        <tr>
        <td>
        </td>
        <td>
            <table>
            <tr>
                
                
            <td align="left">
                <asp:ImageButton ID="btnGenerateReport" runat="server" ImageUrl="~/Images/Generate_report_hvr.gif"
                    onmouseout="this.src='Images/Generate_report_hvr.gif'" onmouseover="this.src='Images/Generate_report.gif'"
                    ValidationGroup="VGMakeModelSeries" OnClick="btnGenerateReport_Click" />
            </td>
            <td align="left">
                <asp:ImageButton ID="btnCancel" runat="server" ImageUrl="~/Images/Cancel.gif" onmouseout="this.src='Images/Cancel.gif'"
                    onmouseover="this.src='Images/Cancel_hvr.gif'" ValidationGroup="VGMakeModelSeries" />
            </td>
            </tr>
            
            </table>
            </td>
        </tr>
        <tr align="right">
            <%--<td align="left">
                <asp:Label ID="lblTotal" runat="server"></asp:Label>
            </td>--%>
            <td align="right" colspan="2">
                <asp:Label ID="lblRowsToDisplay2" runat="server" Text="Rows To Display : " Visible="false"></asp:Label>
                <asp:DropDownList ID="ddl_NoRecords" runat="server" AutoPostBack="true" Width="50px"
                    Visible="false" OnSelectedIndexChanged="ddl_NoRecords_SelectedIndexChanged">
                    <asp:ListItem Value="10">10</asp:ListItem>
                    <asp:ListItem Value="20">20</asp:ListItem>
                    <asp:ListItem Value="30">30</asp:ListItem>
                    <asp:ListItem Value="50">50</asp:ListItem>
                    <asp:ListItem Value="All">All</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="gvProspect" runat="server" AllowPaging="true" AutoGenerateColumns="false"
                    AllowSorting="true" PageSize="50" Visible="true" Width="100%" BackColor="White"
                    BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" OnSorting="gvProspect_Sorting"
                    OnPageIndexChanging="gvProspect_PageIndexChanging" OnRowDataBound="gvProspect_RowDataBound" EmptyDataText="No Records Found">
                    <Columns>
                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" ItemStyle-HorizontalAlign="Left">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" ItemStyle-HorizontalAlign="Left">
                            <ItemStyle HorizontalAlign="Left" Width="150" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Mobile" HeaderText="Mobile" SortExpression="Mobile" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle HorizontalAlign="Center" Width="80" />
                        </asp:BoundField>
                        <asp:BoundField DataField="State" HeaderText="State" SortExpression="State" ItemStyle-HorizontalAlign="Center"
                            Visible="true">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CDate" HeaderText="Created Date" SortExpression="CDate"
                            ItemStyle-HorizontalAlign="Center" Visible="true">
                            <ItemStyle HorizontalAlign="Center" Width="85px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Owner" HeaderText="Owner" SortExpression="Owner" ItemStyle-HorizontalAlign="Center"
                            Visible="true">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" Visible="true">
                            <ItemStyle HorizontalAlign="Center" Width="90" />
                        </asp:BoundField>
                    </Columns>
                    <FooterStyle CssClass="gvFooterrow" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <PagerStyle CssClass="pgr" />
                    <HeaderStyle HorizontalAlign="Center" BackColor="#0A73A2" Font-Bold="True" CssClass="gvHeader"
                        Height="30px" />
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
