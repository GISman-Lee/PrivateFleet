<%@ Page Title="Servey Question Report" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeFile="ServeyQuestion3Report.aspx.cs" Inherits="ServeyQuestion3Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .borderGridBottom
        {
            border-left: 1px solid #666;
            border-right: 1px solid #666;
            border-bottom: 1px solid #666;
            padding: 4px 4px 4px 4px;
        }
        .borderGridTop
        {
            border-left: 1px solid #666;
            border-right: 1px solid #666;
            border-top: 1px solid #666;
            padding: 4px 4px 4px 4px;
        }
    </style>

    <script type="text/javascript">
        function compaire_dates_ViewSentRequests1(source, args) {

            // debugger;
            args.IsValid = true;
            fromdate = (document.getElementById('ctl00_ContentPlaceHolder1_txtFromdate').value).trim();

            if (fromdate.trim() == "") {
                args.IsValid = false;
                return;
            }
            todate = (document.getElementById('ctl00_ContentPlaceHolder1_txtToDate').value).trim();

            if (todate.trim() == "") {
                args.IsValid = false;
                return;
            }
            // alert(chkdt(fromdate, todate));
            dt = new Date();
            dt.setDate(fromdate);


            args.IsValid = chkdt(fromdate, todate);
            return;
        }
    </script>

    <asp:UpdatePanel ID="upPnlMain" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlSearch" DefaultButton="btnGenerateReport">
                <table width="100%">
                    <tr>
                        <td>
                            <br />
                            <table cellpadding="2" cellspacing="2">
                                <tr>
                                    <td style="width: 25%">
                                        Search criteria :
                                    </td>
                                    <td colspan="3">
                                        All completed survey Primary Contact
                                    </td>
                                </tr>
                                <tr runat="server" id="reportype2">
                                    <td style="width: 25%">
                                        Name
                                    </td>
                                    <td colspan="3">
                                        <asp:DropDownList runat="server" ID="ddlContactPerson" DataTextField="Name" DataValueField="Id">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <%--  <td colspan="4">
                            <table>
                                <tr>--%>
                                    <td style="width: 25%">
                                        From Date
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtFromdate" MaxLength="10" Width="90px"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender runat="server" ID="CalenderFrom" TargetControlID="txtFromdate"
                                            Format="dd/MM/yyyy">
                                        </ajaxToolkit:CalendarExtender>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidatorFromdate" ControlToValidate="txtFromdate"
                                            ErrorMessage="Enter From Date." Display="None" ValidationGroup="search"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="calloutFromdate" TargetControlID="RequiredFieldValidatorFromdate">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                        <asp:RegularExpressionValidator runat="server" ID="regExprFromDate" Display="None"
                                            ControlToValidate="txtFromdate" ErrorMessage="Enter proper date in dd/mm/yyyy format."
                                            ValidationGroup="search" SetFocusOnError="False" ValidationExpression='^(((0?[1-9]|[12]\d|3[01])\/(0?[13578]|1[02])\/((1[6-9]|[2-9]\d)\d{2}))|((0?[1-9]|[12]\d|30)\/(0?[13456789]|1[012])\/((1[6-9]|[2-9]\d)\d{2}))|((0?[1-9]|1\d|2[0-8])\/0?2\/((1[6-9]|[2-9]\d)\d{2}))|(29\/0?2\/((1[6-9]|[2-9]\d)(0?[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$'></asp:RegularExpressionValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="valExtFromDate" TargetControlID="regExprFromDate"
                                            HighlightCssClass="validatorCalloutHighlight" />
                                    </td>
                                    <td>
                                        To Date
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtToDate" MaxLength="10" Width="90px"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtenderTodate" TargetControlID="txtToDate"
                                            Format="dd/MM/yyyy">
                                        </ajaxToolkit:CalendarExtender>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidatorTodate" ControlToValidate="txtToDate"
                                            ErrorMessage="Enter To Date." Display="None" ValidationGroup="search"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="calloutTodate" TargetControlID="RequiredFieldValidatorTodate">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                        <asp:RegularExpressionValidator runat="server" ID="regExpreToDate" Display="None"
                                            ControlToValidate="txtToDate" ErrorMessage="Enter proper date in dd/mm/yyyy format."
                                            ValidationGroup="search" SetFocusOnError="False" ValidationExpression='^(((0?[1-9]|[12]\d|3[01])\/(0?[13578]|1[02])\/((1[6-9]|[2-9]\d)\d{2}))|((0?[1-9]|[12]\d|30)\/(0?[13456789]|1[012])\/((1[6-9]|[2-9]\d)\d{2}))|((0?[1-9]|1\d|2[0-8])\/0?2\/((1[6-9]|[2-9]\d)\d{2}))|(29\/0?2\/((1[6-9]|[2-9]\d)(0?[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$'></asp:RegularExpressionValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="valExtToDate" TargetControlID="regExpreToDate"
                                            HighlightCssClass="validatorCalloutHighlight" />
                                        <asp:CustomValidator runat="server" ID="cust1" ClientValidationFunction="compaire_dates_ViewSentRequests1"
                                            ValidationGroup="search" ErrorMessage="From date can not be greater than to date."
                                            Display="None" ControlToValidate="txtFromdate"> </asp:CustomValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="valCmpToDate" TargetControlID="cust1"
                                            HighlightCssClass="validatorCalloutHighlight">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                    </td>
                                    <%--   </tr>
                            </table>
                        </td>--%>
                                </tr>
                                <tr>
                                    <td colspan="1">
                                    </td>
                                    <td align="right" colspan="3" style="padding-right: 156px;">
                                        <asp:ImageButton ID="btnGenerateReport" runat="server" ImageUrl="~/Images/Generate_report_hvr.gif"
                                            onmouseout="this.src='Images/Generate_report_hvr.gif'" onmouseover="this.src='Images/Generate_report.gif'"
                                            ValidationGroup="search" OnClick="btnGenerateReport_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlReport" Visible="false">
                <table width="100%">
                    <tr>
                        <td>
                            <asp:Panel ID="pnlQ7" runat="server">
                                <table cellpadding="3" cellspacing="3" width="750px">
                                    <tr>
                                        <td>
                                            <%-----------------OverAll Result Grid---------------%>
                                            <table width="100%" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="borderGridTop">
                                                        <asp:Label ID="lblHdrQ7" runat="server" Text="Q : Who was your primary contact from Private Fleet between order and delivery?"
                                                            Visible="false" Font-Bold="true"></asp:Label><br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:GridView ID="grdQ7_1" runat="server" AutoGenerateColumns="false" AlternatingRowStyle-BackColor="#d5ecfd"
                                                            AllowPaging="true" OnPageIndexChanging="grdQ7_1_OnPaging" Width="100%" RowStyle-Height="30px"
                                                            PageSize="10" OnRowDataBound="grdQ7_1_RowDataBound">
                                                            <HeaderStyle BackColor="#0A73A2" Font-Bold="True" HorizontalAlign="Center" CssClass="gvHeader"
                                                                Height="30px" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Name" ItemStyle-Width="40%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Count" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCount" runat="server" Text='<%# Bind("Count") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Percentage" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40%">
                                                                    <ItemTemplate>
                                                                        <asp:Panel ID="pnlPer" runat="server" Style="width: 75%; border: solid 1px green;
                                                                            float: left;">
                                                                            <asp:Panel ID="pnlInner" runat="server" Style="width: 10%; background-color: Green;
                                                                                float: left; height: 10px;">
                                                                            </asp:Panel>
                                                                        </asp:Panel>
                                                                        <asp:Label ID="lblPercentage" runat="server" Text=""></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <EmptyDataTemplate>
                                                                <asp:Label ID="lbloRecordFound" runat="server" Text="No Record found." ForeColor="Red"></asp:Label>
                                                            </EmptyDataTemplate>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="borderGridBottom">
                                                        <asp:Label ID="lblTotalAns_Q7_1" runat="server" Text="Total Answers : " Font-Bold="true"
                                                            Visible="false"></asp:Label>
                                                        <asp:Label ID="lblTotal_Q7_1" runat="server" Text="" Visible="false"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="pnlQ8" runat="server">
                                <table cellpadding="3" cellspacing="3" width="750px">
                                    <tr>
                                        <td>
                                            <table width="100%" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="borderGridTop">
                                                        <asp:Label ID="lblHdrQ8" runat="server" Text="Q : How would you rate the service you received from this person?"
                                                            Visible="false" Font-Bold="true"></asp:Label><br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <%-----------------OverAll Result Grid---------------%>
                                                        <asp:GridView ID="grdQ8_1" runat="server" AutoGenerateColumns="false" OnRowDataBound="grdQ8_1_RowDataBound"
                                                            RowStyle-Height="30px" AlternatingRowStyle-BackColor="#d5ecfd" Width="100%">
                                                            <HeaderStyle BackColor="#0A73A2" Font-Bold="True" HorizontalAlign="Center" CssClass="gvHeader"
                                                                Height="30px" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Rating" ItemStyle-Width="40%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblName" runat="server" Text='<%# Bind("Text") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Count" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCount" runat="server" Text='<%# Bind("Count") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Percentage" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40%">
                                                                    <ItemTemplate>
                                                                        <asp:Panel ID="pnlPer" runat="server" Style="width: 75%; border: solid 1px green;
                                                                            float: left;">
                                                                            <asp:Panel ID="pnlInner" runat="server" Style="width: 10%; background-color: Green;
                                                                                float: left; height: 10px;">
                                                                            </asp:Panel>
                                                                        </asp:Panel>
                                                                        <asp:Label ID="lblPercentage" runat="server" Text=""></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Total" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTotalQ8_1" runat="server" Text='<%# Bind("total") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <EmptyDataTemplate>
                                                                <asp:Label ID="lbloRecordFound" runat="server" Text="No Record found." ForeColor="Red"></asp:Label>
                                                            </EmptyDataTemplate>
                                                        </asp:GridView>
                                                        <asp:GridView ID="grdQ8All" runat="server" AutoGenerateColumns="false" RowStyle-HorizontalAlign="Center"
                                                            OnRowDataBound="grdQ8_all_RowDataBound" RowStyle-Height="30px" AlternatingRowStyle-BackColor="#d5ecfd"
                                                            Width="100%">
                                                            <HeaderStyle BackColor="#0A73A2" Font-Bold="True" HorizontalAlign="Center" CssClass="gvHeader"
                                                                Height="30px" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Name" ItemStyle-Width="25%" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Excellent" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblExcellent" runat="server" Text='<%# Bind("Excellent") %>'></asp:Label><br />
                                                                        <asp:Panel ID="pnlPer_E" runat="server" Style="width: 75%; border: solid 1px green;
                                                                            float: left; margin-left: 10%;">
                                                                            <asp:Panel ID="pnlInner_E" runat="server" Style="width: 10%; background-color: Green;
                                                                                float: left; height: 10px;">
                                                                            </asp:Panel>
                                                                        </asp:Panel>
                                                                        <asp:Label ID="lblExellentPer" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Very Good" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblVeryGood" runat="server" Text='<%# Bind("VeryGood") %>'></asp:Label>
                                                                        <br />
                                                                        <asp:Panel ID="pnlPer_VG" runat="server" Style="width: 75%; border: solid 1px green;
                                                                            float: left; margin-left: 10%;">
                                                                            <asp:Panel ID="pnlInner_VG" runat="server" Style="width: 10%; background-color: Green;
                                                                                float: left; height: 10px;">
                                                                            </asp:Panel>
                                                                        </asp:Panel>
                                                                        <asp:Label ID="lblVGoodPer" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Average" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAverage" runat="server" Text='<%# Bind("Average") %>'></asp:Label>
                                                                        <br />
                                                                        <asp:Panel ID="pnlPer_AVG" runat="server" Style="width: 75%; border: solid 1px green;
                                                                            float: left; margin-left: 10%;">
                                                                            <asp:Panel ID="pnlInner_AVG" runat="server" Style="width: 10%; background-color: Green;
                                                                                float: left; height: 10px;">
                                                                            </asp:Panel>
                                                                        </asp:Panel>
                                                                        <asp:Label ID="lblAvgPer" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Poor" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPoor" runat="server" Text='<%# Bind("Poor") %>'></asp:Label>
                                                                        <br />
                                                                        <asp:Panel ID="pnlPer_P" runat="server" Style="width: 75%; border: solid 1px green;
                                                                            float: left; margin-left: 10%;">
                                                                            <asp:Panel ID="pnlInner_P" runat="server" Style="width: 10%; background-color: Green;
                                                                                float: left; height: 10px;">
                                                                            </asp:Panel>
                                                                        </asp:Panel>
                                                                        <asp:Label ID="lblPoorPer" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Very Poor" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblVeryPoor" runat="server" Text='<%# Bind("VeryPoor") %>'></asp:Label>
                                                                        <br />
                                                                        <asp:Panel ID="pnlPer_VP" runat="server" Style="width: 75%; border: solid 1px green;
                                                                            float: left; margin-left: 10%;">
                                                                            <asp:Panel ID="pnlInner_VP" runat="server" Style="width: 10%; background-color: Green;
                                                                                float: left; height: 10px;">
                                                                            </asp:Panel>
                                                                        </asp:Panel>
                                                                        <asp:Label ID="lblVPoorPer" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Total" ItemStyle-Width="15%" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTotalQ8ALL" runat="server" Text='<%# Bind("total") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <EmptyDataTemplate>
                                                                <asp:Label ID="lbloRecordFound" runat="server" Text="No Record found." ForeColor="Red"></asp:Label>
                                                            </EmptyDataTemplate>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <%-- <tr>
                                                    <td class="borderGridBottom">
                                                        <asp:Label ID="lblTotalAns_Q8_1" runat="server" Text="Total Answers : " Font-Bold="true"
                                                            Visible="false"></asp:Label>
                                                        <asp:Label ID="lblTotal_Q8_1" runat="server" Text="" Visible="false"></asp:Label>
                                                    </td>
                                                </tr>--%>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="pnlQ9" runat="server">
                                <table cellpadding="3" cellspacing="3" width="750px">
                                    <tr>
                                        <td>
                                            <table width="100%" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="borderGridTop">
                                                        <asp:Label ID="lblHdrQ9" runat="server" Text="Q : Specifically were you kept up to date as to the estimated delivery of your vehicle to your satisfaction?"
                                                            Visible="false" Font-Bold="true"></asp:Label><br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <%-----------------OverAll Result Grid---------------%>
                                                        <asp:GridView ID="grdQ9_1" BorderColor="#666666" runat="server" AutoGenerateColumns="false"
                                                            OnRowDataBound="grdQ9_1_RowDataBound" RowStyle-Height="30px" AlternatingRowStyle-BackColor="#d5ecfd"
                                                            Width="100%">
                                                            <HeaderStyle BackColor="#0A73A2" Font-Bold="True" HorizontalAlign="Center" CssClass="gvHeader"
                                                                Height="30px" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Answer" ItemStyle-Width="40%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAnswer" runat="server" Text='<%# Bind("Answer") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Count" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCount" runat="server" Text='<%# Bind("Count") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Percentage" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40%">
                                                                    <ItemTemplate>
                                                                        <asp:Panel ID="pnlPer" runat="server" Style="width: 75%; border: solid 1px green;
                                                                            float: left;">
                                                                            <asp:Panel ID="pnlInner" runat="server" Style="width: 10%; background-color: Green;
                                                                                float: left; height: 10px;">
                                                                            </asp:Panel>
                                                                        </asp:Panel>
                                                                        <asp:Label ID="lblPercentage" runat="server" Text="" Style="vertical-align: top;"> </asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Total" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTotalQ9_1" runat="server" Text='<%# Bind("total") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <EmptyDataTemplate>
                                                                <asp:Label ID="lbloRecordFound" runat="server" Text="No Record found." ForeColor="Red"></asp:Label>
                                                            </EmptyDataTemplate>
                                                        </asp:GridView>
                                                        <asp:GridView ID="grdQ9All" runat="server" AutoGenerateColumns="false" RowStyle-HorizontalAlign="Center"
                                                            OnRowDataBound="grdQ9All_RowDataBound" RowStyle-Height="30px" AlternatingRowStyle-BackColor="#d5ecfd"
                                                            Width="100%">
                                                            <HeaderStyle BackColor="#0A73A2" Font-Bold="True" HorizontalAlign="Center" CssClass="gvHeader"
                                                                Height="30px" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Name" ItemStyle-Width="50%" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Yes(Count)" ItemStyle-Width="25%" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblYesCount" runat="server" Text='<%# Bind("Yes") %>'></asp:Label>
                                                                        <br />
                                                                        <asp:Panel ID="pnlPer_Yes" runat="server" Style="width: 75%; border: solid 1px green;
                                                                            float: left; margin-left: 10%;">
                                                                            <asp:Panel ID="pnlInner_Yes" runat="server" Style="width: 10%; background-color: Green;
                                                                                float: left; height: 10px;">
                                                                            </asp:Panel>
                                                                        </asp:Panel>
                                                                        <br />
                                                                        <asp:Label ID="lblYesPer" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="No(Count)" ItemStyle-Width="25%" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblNoCount" runat="server" Text='<%# Bind("No") %>'></asp:Label>
                                                                        <br />
                                                                        <asp:Panel ID="pnlPer_No" runat="server" Style="width: 75%; border: solid 1px green;
                                                                            float: left; margin-left: 10%;">
                                                                            <asp:Panel ID="pnlInner_No" runat="server" Style="width: 10%; background-color: Green;
                                                                                float: left; height: 10px;">
                                                                            </asp:Panel>
                                                                        </asp:Panel>
                                                                        <br />
                                                                        <asp:Label ID="lblNoPer" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Total" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTotalQ9All" runat="server" Text='<%# Bind("Total") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <EmptyDataTemplate>
                                                                <asp:Label ID="lbloRecordFound" runat="server" Text="No Record found." ForeColor="Red"></asp:Label>
                                                            </EmptyDataTemplate>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <%--  <tr>
                                                    <td class="borderGridBottom">
                                                        <asp:Label ID="lblTotalAns_Q9_1" runat="server" Text="Total Answers : " Font-Bold="true"
                                                            Visible="false"></asp:Label>
                                                        <asp:Label ID="lblTotal_Q9_1" runat="server" Text="" Visible="false"></asp:Label>
                                                    </td>
                                                </tr>--%>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
