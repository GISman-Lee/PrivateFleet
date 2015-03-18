<%@ Page Title="Delivery List Report" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Admin_DeliveryListReport.aspx.cs" Inherits="Admin_DeliveryListReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pnlDeliveredList" runat="server" DefaultButton="btnGenerateReport">
        <table width="100%" cellpadding="2" cellspacing="2" align="center">
            <tr>
                <td height="5px">
                </td>
            </tr>
            <tr>
                <td align="right">
                    Primary Contact&nbsp;
                </td>
                <td width="170">
                    <asp:DropDownList runat="server" ID="ddlPrimaryContact" Width="150px">
                    </asp:DropDownList>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <span style="color: Red">*</span>From Date&nbsp;
                </td>
                <td>
                    <asp:TextBox ID="txtCalenderFrom" runat="server" Width="119px" MaxLength="10"></asp:TextBox>
                    <asp:Image ID="imgCal" runat="server" ImageUrl="~/Images/Calendar.gif" />
                    <ajaxToolkit:CalendarExtender ID="calFrom" runat="server" TargetControlID="txtCalenderFrom"
                        Format="dd/MM/yyyy" PopupButtonID="imgCal">
                    </ajaxToolkit:CalendarExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCalenderFrom"
                        CssClass="gvValidationError" Display="None" ErrorMessage="Please Enter the from Date"
                        SetFocusOnError="True" ValidationGroup="VGMakeModelSeries">
                    </asp:RequiredFieldValidator>
                    <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender2_ViewSentRequest"
                        TargetControlID="RequiredFieldValidator1" HighlightCssClass="validatorCalloutHighlight" />
                    <%--    validation for date format check  dd/mm/yy--%>
                    <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidatorDatecheck"
                        Display="None" ControlToValidate="txtCalenderFrom" ErrorMessage="Enter proper date in dd/mm/yyyy format."
                        ValidationGroup="VGMakeModelSeries" SetFocusOnError="False" ValidationExpression='^(((0?[1-9]|[12]\d|3[01])\/(0?[13578]|1[02])\/((1[6-9]|[2-9]\d)\d{2}))|((0?[1-9]|[12]\d|30)\/(0?[13456789]|1[012])\/((1[6-9]|[2-9]\d)\d{2}))|((0?[1-9]|1\d|2[0-8])\/0?2\/((1[6-9]|[2-9]\d)\d{2}))|(29\/0?2\/((1[6-9]|[2-9]\d)(0?[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$'></asp:RegularExpressionValidator>
                    <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender2"
                        TargetControlID="RegularExpressionValidatorDatecheck" HighlightCssClass="validatorCalloutHighlight" />
                </td>
                <td align="left">
                    <span style="color: Red">*</span> To Date
                </td>
                <td>
                    <asp:TextBox ID="TxtToDate" runat="server" Width="119px" MaxLength="10"></asp:TextBox>
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Calendar.gif" />
                    <ajaxToolkit:CalendarExtender ID="calTO" runat="server" TargetControlID="TxtToDate"
                        Format="dd/MM/yyyy" PopupButtonID="Image1">
                    </ajaxToolkit:CalendarExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtToDate"
                        CssClass="gvValidationError" Display="None" ErrorMessage="Please Enter the to Date"
                        SetFocusOnError="True" ValidationGroup="VGMakeModelSeries">
                    
                    </asp:RequiredFieldValidator><ajaxToolkit:ValidatorCalloutExtender runat="server"
                        ID="ValidatorCalloutExtender1" TargetControlID="RequiredFieldValidator2" HighlightCssClass="validatorCalloutHighlight" />
                    <%-- validation for date format check in dd/mm/yyyy--%>
                    <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1" Display="None"
                        ControlToValidate="TxtToDate" ErrorMessage="Enter proper date in dd/mm/yyyy format."
                        ValidationGroup="VGMakeModelSeries" SetFocusOnError="False" ValidationExpression='^(((0?[1-9]|[12]\d|3[01])\/(0?[13578]|1[02])\/((1[6-9]|[2-9]\d)\d{2}))|((0?[1-9]|[12]\d|30)\/(0?[13456789]|1[012])\/((1[6-9]|[2-9]\d)\d{2}))|((0?[1-9]|1\d|2[0-8])\/0?2\/((1[6-9]|[2-9]\d)\d{2}))|(29\/0?2\/((1[6-9]|[2-9]\d)(0?[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$'></asp:RegularExpressionValidator>
                    <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender3"
                        TargetControlID="RegularExpressionValidator1" HighlightCssClass="validatorCalloutHighlight" />
                    <%--validation for date comparision --%>
                    <asp:CustomValidator runat="server" ID="cust1" ClientValidationFunction="compaire_dates_ViewSentRequests"
                        ValidationGroup="VGMakeModelSeries" ErrorMessage="From date can not be greater than to date."
                        Display="None" ControlToValidate="txtCalenderFrom"> </asp:CustomValidator>
                    <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="validatorcallourdatecomparision"
                        TargetControlID="cust1" HighlightCssClass="validatorCalloutHighlight">
                    </ajaxToolkit:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr>
                <td align="right" colspan="2">
                    <asp:ImageButton ID="btnGenerateReport" runat="server" ImageUrl="~/Images/Submit.gif"
                        onmouseout="this.src='Images/Submit.gif'" onmouseover="this.src='Images/Submit_hvr.gif'"
                        OnClick="btnGenerateReport_Click" />
                </td>
            </tr>
            <tr id="trPaging" runat="server" visible="false">
                <td align="right" style="padding-right: 10px" colspan="4">
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
                <td colspan="4">
                    <asp:GridView runat="server" ID="grdDeliveryReport" Width="100%" AutoGenerateColumns="false"
                        AllowPaging="true" AllowSorting="true" OnPageIndexChanging="grdDeliveryReport_PageIndexChanging"
                        OnSorting="grdDeliveryReport_Sorting" EmptyDataText="No Recrods Found." RowStyle-Height="30px">
                        <FooterStyle CssClass="gvFooterrow" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <PagerStyle CssClass="pgr" />
                        <HeaderStyle BackColor="#0A73A2" Font-Bold="True" CssClass="gvHeader" Height="30px" />
                        <Columns>
                            <asp:TemplateField HeaderText="Primary Contact" ItemStyle-HorizontalAlign="Left"
                                SortExpression="PrimaryContact" ItemStyle-CssClass="grid_padding">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblPrimaryContact" Text='<%# bind("PrimaryContact") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Customer Name" SortExpression="customerName" ItemStyle-HorizontalAlign="Left"
                                ItemStyle-CssClass="grid_padding">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblCustomerName" Text='<%# bind("customerName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="New Car" SortExpression="Make" ItemStyle-HorizontalAlign="Left"
                                ItemStyle-CssClass="grid_padding">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblmake" Text='<%# bind("Make") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- <asp:TemplateField HeaderText="Customer Email" SortExpression="CustomerEmail" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="grid_padding">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblCustomerEmail" Text='<%# bind("CustomerEmail") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="phone" SortExpression="phone"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="grid_padding">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblphone" Text='<%# bind("phone") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Supplying DealerShip" SortExpression="Company" ItemStyle-HorizontalAlign="Left"
                                ItemStyle-CssClass="grid_padding">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblDealerShip" Text='<%# bind("Company") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Dealer Name" SortExpression="DealerName" ItemStyle-HorizontalAlign="Left"
                                ItemStyle-CssClass="grid_padding">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblDealerName" Text='<%# bind("DealerName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ETA" SortExpression="ETASort" ItemStyle-CssClass="grid_padding">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblETA" Text='<%# bind("ETA") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Trade In Status" SortExpression="Tradestatus" ItemStyle-CssClass="grid_padding">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblTradeStatus" Text='<%# bind("Tradestatus") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status" SortExpression="Status1" ItemStyle-HorizontalAlign="Left"
                                ItemStyle-CssClass="grid_padding">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblStatus" Text='<%# bind("Status1") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Order Status" SortExpression="OrderStatus" ItemStyle-HorizontalAlign="Left"
                                ItemStyle-CssClass="grid_padding">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblOrderStatus" Text='<%# bind("OrderStatus") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
