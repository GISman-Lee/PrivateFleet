<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="AdminReport.aspx.cs" Inherits="AdminReport" Title="Quotation Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="98%">
        <tr>
            <td style="height: 10px;">
                &nbsp;<asp:Label ID="lblHeader" runat="server" CssClass="dbresult"></asp:Label>
            </td>
        </tr>
    </table>
    <table width="98%" style="border: 1px solid #CCC;" align="center">
        <tr>
            <td style="height: 3px;">
            </td>
        </tr>
        <tr>
            <td align="left" colspan="2">
                <asp:Label ID="Label4" runat="server" CssClass="label" Width="176px">&nbsp;Select Search Criteria :</asp:Label>&nbsp;
                <asp:DropDownList ID="ddlSearchCriteria" runat="server" Width="238px" OnSelectedIndexChanged="ddlSearchCriteria_SelectedIndexChanged"
                    AutoPostBack="True">
                    <asp:ListItem>-- Select --</asp:ListItem>
                    <asp:ListItem Value="RBC">Request's  By Consultant</asp:ListItem>
                    <asp:ListItem Value="ASQ">All Shortlisted Quotations</asp:ListItem>
                    <asp:ListItem Value="SQOC">Short Listed Quotations Of Consultant</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2" width="100%">
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">
                        <asp:Panel ID="PView1" runat="server">
                            <table align="center" width="100%">
                                <tr>
                                    <td align="left" colspan="2">
                                        <asp:Label ID="Label1" runat="server" CssClass="label" Width="178px">Select Consultant :</asp:Label>
                                        <asp:DropDownList ID="ddlConsultants" runat="server" Width="238px" AppendDataBoundItems="True"
                                            DataTextField="Name" DataValueField="ID" OnSelectedIndexChanged="ddlConsultants_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlConsultants"
                                            CssClass="gvValidationError" Display="None" ErrorMessage="Please Select Consultant"
                                            SetFocusOnError="True" InitialValue="0" ValidationGroup="VGSubmit"> </asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender4"
                                            TargetControlID="RequiredFieldValidator4" HighlightCssClass="validatorCalloutHighlight" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblFromDate" runat="server" CssClass="label" Width="178px"><span style="color: Red">*</span>From Date :</asp:Label>
                                        <asp:TextBox ID="txtCalenderFrom" runat="server" Width="119px" MaxLength ="10"></asp:TextBox>
                                        <asp:Image ID="imgCal" runat="server" ImageUrl="~/Images/Calendar.gif" />
                                        <ajaxToolkit:CalendarExtender ID="calFrom" runat="server" TargetControlID="txtCalenderFrom"
                                            Format="dd/MM/yyyy" PopupButtonID="imgCal">
                                        </ajaxToolkit:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCalenderFrom"
                                            CssClass="gvValidationError" Display="None" ErrorMessage="Please Enter the from Date"
                                            SetFocusOnError="True" ValidationGroup="VGSubmit"> </asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender2_adminReport"
                                            TargetControlID="RequiredFieldValidator1" HighlightCssClass="validatorCalloutHighlight" />
                                            
                                            
                                            
                                                 <%--    validation for date format check  dd/mm/yy--%>
                           <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidatorDatecheck" Display="None" ControlToValidate="txtCalenderFrom"
                        ErrorMessage="Enter proper date in dd/mm/yyyy format." ValidationGroup="VGSubmit"
                        SetFocusOnError="False" ValidationExpression='^(((0?[1-9]|[12]\d|3[01])\/(0?[13578]|1[02])\/((1[6-9]|[2-9]\d)\d{2}))|((0?[1-9]|[12]\d|30)\/(0?[13456789]|1[012])\/((1[6-9]|[2-9]\d)\d{2}))|((0?[1-9]|1\d|2[0-8])\/0?2\/((1[6-9]|[2-9]\d)\d{2}))|(29\/0?2\/((1[6-9]|[2-9]\d)(0?[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$'></asp:RegularExpressionValidator>
                    <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender5"
                        TargetControlID="RegularExpressionValidatorDatecheck" HighlightCssClass="validatorCalloutHighlight" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblToDate" runat="server" CssClass="label" Width="178px"><span style="color: Red">*</span>To Date :</asp:Label>
                                        <asp:TextBox ID="TxtToDate" runat="server" Width="119px" MaxLength ="10"></asp:TextBox>
                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Calendar.gif" />
                                        <ajaxToolkit:CalendarExtender ID="calTO" runat="server" TargetControlID="TxtToDate"
                                            Format="dd/MM/yyyy" PopupButtonID="Image1">
                                        </ajaxToolkit:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtToDate"
                                            CssClass="gvValidationError" Display="None" ErrorMessage="Please Enter the to Date"
                                            SetFocusOnError="True" ValidationGroup="VGSubmit"> </asp:RequiredFieldValidator><ajaxToolkit:ValidatorCalloutExtender
                                                runat="server" ID="ValidatorCalloutExtender1" TargetControlID="RequiredFieldValidator2"
                                                HighlightCssClass="validatorCalloutHighlight" />
                                                
                                                
                                                  <%-- validation for date format check in dd/mm/yyyy--%>
                        <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1" Display="None"
                        ControlToValidate="TxtToDate" ErrorMessage="Enter proper date in dd/mm/yyyy format."
                        ValidationGroup="VGSubmit" SetFocusOnError="False" ValidationExpression='^(((0?[1-9]|[12]\d|3[01])\/(0?[13578]|1[02])\/((1[6-9]|[2-9]\d)\d{2}))|((0?[1-9]|[12]\d|30)\/(0?[13456789]|1[012])\/((1[6-9]|[2-9]\d)\d{2}))|((0?[1-9]|1\d|2[0-8])\/0?2\/((1[6-9]|[2-9]\d)\d{2}))|(29\/0?2\/((1[6-9]|[2-9]\d)(0?[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$'></asp:RegularExpressionValidator>
                    <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender2"
                        TargetControlID="RegularExpressionValidator1" HighlightCssClass="validatorCalloutHighlight" />  
                        
                        <%--validation for date comparision --%>
                        <asp:CustomValidator runat="server" ID="cust1" ClientValidationFunction="compaire_dates_ViewSentRequests"
                        ValidationGroup="VGSubmit" ErrorMessage="From date can not be greater than to date."
                        Display="None" ControlToValidate="txtCalenderFrom"> </asp:CustomValidator>
                    <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="validatorcallourdatecomparision"
                        TargetControlID="cust1" HighlightCssClass="validatorCalloutHighlight">
                    </ajaxToolkit:ValidatorCalloutExtender>          
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 20px; padding-left: 178px;" align="left" colspan="2">
                                        &nbsp;<asp:ImageButton ID="btnSearchByConsultant" runat="server" ImageAlign="AbsMiddle"
                                            ImageUrl="~/Images/Submit.gif" OnClick="btnSearchByConsultant_Click" CausesValidation="true"
                                            ValidationGroup="VGSubmit" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <asp:Label ID="lblTotal" runat="server"></asp:Label>
                                    </td>
                                    <td align="right">
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
                                    <td colspan="2">
                                        <asp:GridView ID="gvRequests" runat="server" AutoGenerateColumns="False" BorderColor="#ACACAC"
                                            CellPadding="3" CellSpacing="1" DataKeyNames="ID" EmptyDataText="No Requests Found"
                                            OnRowCommand="gvRequests_RowCommand" OnRowDataBound="gvRequests_RowDataBound"
                                            Width="100%" AllowPaging="true" OnPageIndexChanging="gvRequests_PageIndexChanging"
                                            OnSorting="gvRequests_Sorting" AllowSorting="true">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Consultant" SortExpression="Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblName" runat="server" CssClass="label" Text='<%# Bind("Name") %>'></asp:Label></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Make" HeaderText="Make" SortExpression="Make" />
                                                <asp:BoundField DataField="Model" HeaderText="Model" SortExpression="Model" />
                                                <%-- <asp:BoundField DataField="Series" HeaderText="Series" />
                                                <asp:BoundField DataField="RequestStatus" HeaderText="Status" />--%>
                                                <asp:BoundField DataField="RequestDate" HeaderText="Request Date" SortExpression="RequestDate1" />
                                                <asp:TemplateField Visible="False">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkCompare" runat="server" CommandArgument='<%# Bind("QuotationID") %>'
                                                            CommandName="CompareQuotations" CssClass="activeLink" Text="Compare Quotations"></asp:LinkButton></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDetails" runat="server" CommandName="ViewDetails" CssClass="activeLink"
                                                            Text="View Details"></asp:LinkButton></ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle CssClass="gvFooterrow" />
                                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle CssClass="pgr" />
                                            <HeaderStyle BackColor="#0A73A2" CssClass="gvHeader" Font-Bold="True" Height="25px" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </asp:View>
                    <asp:View ID="View2" runat="server">
                        <asp:Panel ID="PView2" runat="server">
                            <table align="center" width="100%">
                                <tr>
                                    <td align="left">
                                        <asp:Label ID="Label2" runat="server" CssClass="label" Width="176px">Select Consultant :</asp:Label>
                                        <asp:DropDownList ID="ddlSLConsultant" runat="server" AppendDataBoundItems="True"
                                            DataTextField="Name" DataValueField="ID" OnSelectedIndexChanged="ddlSLConsultant_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="height: 20px" align="left">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 20px; padding-left: 178px;" align="left" colspan="2">
                                        &nbsp;<asp:ImageButton ID="btnSLGetRequest" runat="server" ImageAlign="AbsMiddle"
                                            ImageUrl="~/Images/Submit.gif" OnClick="btnSLGetRequest_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <asp:Label ID="lblTotalSLQC" runat="server"></asp:Label>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="lblRowsToDisplay1" runat="server">Rows To Display:</asp:Label>
                                        <asp:DropDownList ID="ddl_NoRecords1" runat="server" AutoPostBack="true" Width="50px"
                                            OnSelectedIndexChanged="ddl_NoRecords1_SelectedIndexChanged">
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
                                        <asp:GridView ID="gvSLRequests" runat="server" AutoGenerateColumns="False" BorderColor="#ACACAC"
                                            CellPadding="3" CellSpacing="1" DataKeyNames="ID,QuotationID,OptionID,DealerID"
                                            EmptyDataText="No Requests Found" OnRowCommand="gvSLRequests_RowCommand" OnRowDataBound="gvSLRequests_RowDataBound"
                                            Width="100%" OnDataBound="gvSLRequests_DataBound" AllowSorting="true" OnSorting="gvSLRequests_Sorting"
                                            AllowPaging="true" OnPageIndexChanging="gvSLRequests_PageIndexChanging">
                                            <FooterStyle CssClass="gvFooterrow" />
                                            <Columns>
                                                <asp:BoundField DataField="Name" HeaderText="Consultant" SortExpression="Name"></asp:BoundField>
                                                <asp:BoundField DataField="Make" HeaderText="Make" SortExpression="Make"></asp:BoundField>
                                                <asp:BoundField DataField="Model" HeaderText="Model" SortExpression="Model"></asp:BoundField>
                                                <%--<asp:BoundField DataField="Series" HeaderText="Series"></asp:BoundField>
                                                <asp:BoundField DataField="RequestStatus" HeaderText="Status"></asp:BoundField>--%>
                                                <asp:BoundField DataField="RequestDate" HeaderText="Request Date" SortExpression="RequestDate1">
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Deal Done">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkMarkAsDealDone" runat="server" CommandArgument='<%# Container.DataItemIndex %>'
                                                            CommandName="MarkAsDealDone" CssClass="activeLink" Text="Deal Done"></asp:LinkButton><asp:Label
                                                                ID="lblMarkAsDealDone" runat="server" Text="Deal Done" Visible="False"></asp:Label></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkCompare" runat="server" CommandArgument='<%# Container.DataItemIndex %>'
                                                            CommandName="ViewQuotations" CssClass="activeLink" Text="View Quotations"></asp:LinkButton></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDetails" runat="server" Text="View Details" CssClass="activeLink"
                                                            CommandName="ViewDetails"></asp:LinkButton></ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle CssClass="pgr" />
                                            <HeaderStyle BackColor="#0A73A2" CssClass="gvHeader" Font-Bold="True" Height="25px" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </asp:View>
                    <asp:View ID="vwAllSLQuotation" runat="server">
                        <table align="center" width="100%">
                            <tr>
                                <td align="left">
                                    <asp:Label ID="lblTotalShortlested" runat="server"></asp:Label>
                                </td>
                                <td align="right" colspan="2">
                                    <asp:Label ID="lblRowsToDisplay" runat="server">Rows To Display:</asp:Label>
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
                                <td colspan="3">
                                    <asp:GridView ID="gvAllSLQuotations" runat="server" AutoGenerateColumns="False" BorderColor="#acacac"
                                        CellPadding="3" CellSpacing="1" DataKeyNames="ID,QuotationID,OptionID,ConsultantID"
                                        EmptyDataText="No Requests Found" OnRowCommand="gvAllSLQuotations_RowCommand"
                                        OnRowDataBound="gvAllSLQuotations_RowDataBound" Width="100%" AllowPaging="true"
                                        OnPageIndexChanging="gvAllSLQuotations_PageIndexChanging" OnSorting="gvAllSLQuotations_Sorting"
                                        AllowSorting="true">
                                        <FooterStyle CssClass="gvFooterrow" />
                                        <Columns>
                                            <asp:BoundField DataField="Name" HeaderText="Consultant" SortExpression="Name" />
                                            <asp:BoundField DataField="Make" HeaderText="Make" SortExpression="Make" />
                                            <asp:BoundField DataField="Model" HeaderText="Model" SortExpression="Model" />
                                            <%--<asp:BoundField DataField="Series" HeaderText="Series" />
                                            <asp:BoundField DataField="RequestStatus" HeaderText="Status" />--%>
                                            <asp:BoundField DataField="RequestDate" HeaderText="Request Date" SortExpression="RequestDate1" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkCompare" runat="server" CommandArgument='<%# Container.DataItemIndex %>'
                                                        CommandName="ViewQuotations" CssClass="activeLink" Text="View Quotations"></asp:LinkButton></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDetails" runat="server" CommandName="ViewDetails" CssClass="activeLink"
                                                        Text="View Details"></asp:LinkButton></ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle CssClass="pgr" />
                                        <HeaderStyle BackColor="#0A73A2" CssClass="gvHeader" Font-Bold="True" Height="25px" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                </asp:MultiView>
            </td>
        </tr>
        <tr>
            <td colspan="2" width="100%">
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="Label3" Visible="false" runat="server" CssClass="label" Width="178px"><span style="color: Red">*</span>From Date :</asp:Label>
                <asp:TextBox ID="TextBox1" Visible="false" runat="server" Width="119px"></asp:TextBox>
                <asp:Image ID="Image2" Visible="false" runat="server" ImageUrl="~/Images/Calendar.gif" />
                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TextBox1"
                    Format="dd/MM/yyyy" PopupButtonID="Image2">
                </ajaxToolkit:CalendarExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBox1"
                    CssClass="gvValidationError" Display="None" ErrorMessage="Please Enter the from Date"
                    SetFocusOnError="True" ValidationGroup="VGSubmit"> </asp:RequiredFieldValidator>
                <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender3"
                    TargetControlID="RequiredFieldValidator3" HighlightCssClass="validatorCalloutHighlight" />
            </td>
        </tr>
    </table>
</asp:Content>
