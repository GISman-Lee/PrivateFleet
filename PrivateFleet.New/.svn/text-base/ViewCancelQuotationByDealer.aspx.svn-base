<%@ Page Title="View Received Quote Requests" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ViewCancelQuotationByDealer.aspx.cs" Inherits="ViewCancelQuotationByDealer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="ViewReceivedQR" runat="server">
        <table width="98%" align="center">
            <tr>
                <td align="right" style="padding-top: 10px;">
                    <span style="color: Red">*</span> From Date
                    <asp:TextBox ID="txtCalenderFrom" runat="server" Width="119px" MaxLength="10"></asp:TextBox>
                    <asp:Image ID="imgCal" runat="server" ImageUrl="~/Images/Calendar.gif" />
                    <ajaxToolkit:CalendarExtender ID="calFrom" runat="server" TargetControlID="txtCalenderFrom"
                        Format="dd/MM/yyyy" PopupButtonID="imgCal">
                    </ajaxToolkit:CalendarExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCalenderFrom"
                        CssClass="gvValidationError" Display="None" ErrorMessage="Please Enter the from Date"
                        SetFocusOnError="True" ValidationGroup="VGSubmit">
                    </asp:RequiredFieldValidator>
                    <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender2_ViewReceivedQuoteReq"
                        TargetControlID="RequiredFieldValidator1" HighlightCssClass="validatorCalloutHighlight" />
                    <asp:RegularExpressionValidator runat="server" ID="regexpfromdate" Display="None"
                        ControlToValidate="txtCalenderFrom" ErrorMessage="Enter proper date in dd/mm/yyyy format."
                        ValidationGroup="VGSubmit" SetFocusOnError="False" ValidationExpression='^(((0?[1-9]|[12]\d|3[01])\/(0?[13578]|1[02])\/((1[6-9]|[2-9]\d)\d{2}))|((0?[1-9]|[12]\d|30)\/(0?[13456789]|1[012])\/((1[6-9]|[2-9]\d)\d{2}))|((0?[1-9]|1\d|2[0-8])\/0?2\/((1[6-9]|[2-9]\d)\d{2}))|(29\/0?2\/((1[6-9]|[2-9]\d)(0?[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$'></asp:RegularExpressionValidator>
                    <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender2"
                        TargetControlID="regexpfromdate" HighlightCssClass="validatorCalloutHighlight" />
                </td>
                <td align="left">
                    &nbsp; <span style="color: Red">*</span> To Date
                    <asp:TextBox ID="TxtToDate" runat="server" Width="119px" MaxLength="10"></asp:TextBox>
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Calendar.gif" />
                    <ajaxToolkit:CalendarExtender ID="calTO" runat="server" TargetControlID="TxtToDate"
                        Format="dd/MM/yyyy" PopupButtonID="Image1">
                    </ajaxToolkit:CalendarExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtToDate"
                        CssClass="gvValidationError" Display="None" ErrorMessage="Please Enter the to Date"
                        SetFocusOnError="True" ValidationGroup="VGSubmit">
                    
                    </asp:RequiredFieldValidator><ajaxToolkit:ValidatorCalloutExtender runat="server"
                        ID="ValidatorCalloutExtender1" TargetControlID="RequiredFieldValidator2" HighlightCssClass="validatorCalloutHighlight" />
                    <asp:CustomValidator runat="server" ID="cust1" ClientValidationFunction="compaire_dates_ViewRecivedQuoteRequests"
                        ValidationGroup="VGSubmit" ErrorMessage="From date can not be greater than to date."
                        Display="None" ControlToValidate="txtCalenderFrom"> </asp:CustomValidator>
                    <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="validatorcallourdatecomparision"
                        TargetControlID="cust1" HighlightCssClass="validatorCalloutHighlight">
                    </ajaxToolkit:ValidatorCalloutExtender>
                    <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1" Display="None"
                        ControlToValidate="TxtToDate" ErrorMessage="Enter proper date in dd/mm/yyyy format."
                        ValidationGroup="VGSubmit" SetFocusOnError="False" ValidationExpression='^(((0?[1-9]|[12]\d|3[01])\/(0?[13578]|1[02])\/((1[6-9]|[2-9]\d)\d{2}))|((0?[1-9]|[12]\d|30)\/(0?[13456789]|1[012])\/((1[6-9]|[2-9]\d)\d{2}))|((0?[1-9]|1\d|2[0-8])\/0?2\/((1[6-9]|[2-9]\d)\d{2}))|(29\/0?2\/((1[6-9]|[2-9]\d)(0?[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$'></asp:RegularExpressionValidator>
                    <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender3"
                        TargetControlID="RegularExpressionValidator1" HighlightCssClass="validatorCalloutHighlight" />
                    &nbsp;&nbsp;
                    <asp:Label ID="lblMake" runat="server" Text="Make"></asp:Label>
                    <asp:DropDownList ID="ddlMake" Width="142" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right" colspan="1">
                </td>
                <td align="left" style="padding-top: 10px;" colspan="1">
                    <asp:ImageButton ID="btnGenerateReport" runat="server" ImageUrl="~/Images/Generate_report_hvr.gif"
                        onmouseout="this.src='Images/Generate_report_hvr.gif'" onmouseover="this.src='Images/Generate_report.gif'"
                        ValidationGroup="VGSubmit" OnClick="btnGenerateReport_Click" />
                    <asp:ImageButton ID="btnCancel" runat="server" ImageUrl="~/Images/Cancel.gif" onmouseout="this.src='Images/Cancel.gif'"
                        onmouseover="this.src='Images/Cancel_hvr.gif'" ValidationGroup="VGMakeModelSeries"
                        Height="21px" OnClick="btnCancel_Click" />
                </td>
            </tr>
            <tr>
                <td style="height: 10px;" colspan="2">
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
                <td colspan="2">
                    <asp:GridView ID="gvMakeDetails" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                        CellPadding="3" OnPageIndexChanging="gvMakeDetails_PageIndexChanging" OnRowCommand="gvMakeDetails_RowCommand"
                        OnRowEditing="gvMakeDetails_RowEditing" OnRowUpdating="gvMakeDetails_RowUpdating"
                        Width="100%" DataKeyNames="ID" AllowSorting="True" OnSorting="gv_Sorting" PageSize="15"
                        EmptyDataText="No record to display">
                        <FooterStyle CssClass="gvFooterrow" />
                        <Columns>
                            <asp:TemplateField HeaderText="Make" SortExpression="Make" HeaderStyle-Width="60px">
                                <ItemStyle HorizontalAlign="Left" Width="120"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Image ID="imgActive" runat="server" Height="10px" ImageUrl="~/Images/active_bullate.jpg"
                                        Width="10px" />
                                    <asp:Label ID="lblMake" runat="server" Style="padding-left: 1px; display: inline;"
                                        Text='<%# Bind("Make") %>'></asp:Label>&nbsp;
                                    <asp:HiddenField ID="hdfID" runat="server" Value='<%# Bind("ID") %>' />
                                    <asp:HiddenField ID="hdfDealerID" runat="server" Value='<%# Bind("DealerID") %>' />
                                    <asp:HiddenField runat="server" ID="hdfConsultantID" Value='<% #Bind("ConsultantID")%>' />
                                    <asp:HiddenField ID="hdfQRStatus" runat="server" Value='<%#Eval("IsQRCancel") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Model" SortExpression="Model">
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Model") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Series" SortExpression="Series">
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("Series") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status" SortExpression="Status">
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("RequestStatus") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="RequestDate1" HeaderText="Request Date">
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("RequestDate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Name" SortExpression="Name" HeaderText="Consultant"></asp:BoundField>
                        </Columns>
                        <RowStyle CssClass="gridactiverow" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <PagerStyle CssClass="pgr" />
                        <HeaderStyle BackColor="#0A73A2" CssClass="gvHeader" Font-Bold="True" Height="30px" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
