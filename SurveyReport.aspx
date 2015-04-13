<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="SurveyReport.aspx.cs" Inherits="SurveyReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        /* The following styles are for the Rating */.ratingStar
        {
            font-size: 0pt;
            width: 13px;
            height: 12px;
            margin: 0px;
            padding: 0px;
            cursor: pointer;
            display: block;
            background-repeat: no-repeat;
        }
        .filledRatingStar
        {
            background-image: url(Images/FilledStar.png);
        }
        .emptyRatingStar
        {
            background-image: url(Images/EmptyStar.png);
        }
        .savedRatingStar
        {
            background-image: url(Images/SavedStar.png);
        }
        .posi
        {
            text-align: center;
            padding-left: 60px;
        }
        /******************************************/</style>
    <asp:Panel runat="server" ID="pnlSearch" DefaultButton="btnGenerateReport">
        <table width="100%">
            <tr>
                <td>
                    <br />
                    <table cellpadding="2" cellspacin="2">
                        <tr>
                            <td style="width: 25%">
                                Search criteria
                            </td>
                            <td colspan="3">
                                <asp:DropDownList runat="server" ID="drpSearchCriteria" AutoPostBack="true" OnSelectedIndexChanged="drpSearchCriteria_SelectedIndexChanged">
                                    <asp:ListItem Selected="True" Text="-- Select --" Value=""></asp:ListItem>
                                    <asp:ListItem Text="All completed survey" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="All completed survey Primary Contact" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Search by Keyword" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Survey Report" Value="4"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidatorSearch" ControlToValidate="drpSearchCriteria"
                                    InitialValue="" ErrorMessage="Select Search Criteria." Display="None" ValidationGroup="search"></asp:RequiredFieldValidator>
                                <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="calloutSearch" TargetControlID="RequiredFieldValidatorSearch">
                                </ajaxToolkit:ValidatorCalloutExtender>
                            </td>
                        </tr>
                        <tr runat="server" id="reportype2" visible="false">
                            <td style="width: 25%">
                                Name
                            </td>
                            <td colspan="3">
                                <asp:DropDownList runat="server" ID="drpServeyDealer">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidatorServayDealer"
                                    ControlToValidate="drpServeyDealer" ErrorMessage="Select Dealer." InitialValue="0"
                                    Display="None" ValidationGroup="search"></asp:RequiredFieldValidator>
                                <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="calloutServeyDealer" TargetControlID="RequiredFieldValidatorServayDealer">
                                </ajaxToolkit:ValidatorCalloutExtender>
                            </td>
                        </tr>
                        <tr runat="server" id="reportype1_2" visible="false">
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
                            </td>
                            <%--   </tr>
                            </table>
                        </td>--%>
                        </tr>
                        <tr runat="server" id="reportype3" visible="false">
                            <td>
                                Keyword
                            </td>
                            <td colspan="3">
                                <asp:TextBox runat="server" ID="txtSearchKey" MaxLength="50"> </asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidatorSearchkey" ControlToValidate="txtSearchKey"
                                    ErrorMessage="Enter Search Key." Display="None" ValidationGroup="search"></asp:RequiredFieldValidator>
                                <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="calloutSearchkey" TargetControlID="RequiredFieldValidatorSearchkey">
                                </ajaxToolkit:ValidatorCalloutExtender>
                            </td>
                        </tr>
                        <tr runat="server" id="reportype4" visible="false">
                            <%-- <td colspan="4">
                            <table  cellpadding="0" cellspacing="0">
                                <tr>--%>
                            <td style="width: 25%">
                                <asp:Label ID="lblRole" CssClass="label" runat="server" Text="Role :"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlRole" runat="server" Width="180" AutoPostBack="true" OnSelectedIndexChanged="ddlRole_SelectedIndexChanged">
                                    <asp:ListItem Value="0" Text="- Select -"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Dealer"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="Consultant"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidatorRole" ControlToValidate="ddlRole"
                                    ErrorMessage="Select Role." InitialValue="0" Display="None" ValidationGroup="search"></asp:RequiredFieldValidator>
                                <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="calloutRole" TargetControlID="RequiredFieldValidatorRole">
                                </ajaxToolkit:ValidatorCalloutExtender>
                            </td>
                            <td colspan="2">
                            </td>
                        </tr>
                        <tr runat="server" id="reportype41" visible="false">
                            <td style="width: 25%">
                                <asp:Label ID="lblName" runat="server" CssClass="label" Text="Name :"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlName" AutoPostBack="true" OnSelectedIndexChanged="ddlName_SelectedIndexChanged" runat="server" Width="180">
                                </asp:DropDownList>
                            </td>
                            <td colspan="2">
                            </td>
                        </tr>
                        <tr runat="server" id="reportype42" visible="false">
                            <td style="width: 25%">
                                <asp:Label ID="lblRatingFilter" runat="server" CssClass="label" Text="Rating :"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlRatingFilter" runat="server" Width="180">
                                    <asp:ListItem Value="0" Text="All" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="3 and above"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="5 and above"></asp:ListItem>
                                    <asp:ListItem Value="10" Text="10 and above"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td colspan="2">
                            </td>
                        </tr>
                        <%--  </table>
                        </td>--%>
                        <%--</tr>--%>
                        <tr>
                            <%-- </td>--%>
                            <td>
                            </td>
                            <td align="left">
                                <asp:ImageButton ID="btnGenerateReport" runat="server" ImageUrl="~/Images/Generate_report_hvr.gif"
                                    onmouseout="this.src='Images/Generate_report_hvr.gif'" onmouseover="this.src='Images/Generate_report.gif'"
                                    ValidationGroup="search" OnClick="btnGenerateReport_Click" />
                            </td>
                            <td align="right">
                                <asp:ImageButton ID="btnCancel" runat="server" ImageUrl="~/Images/Cancel.gif" onmouseout="this.src='Images/Cancel.gif'"
                                    onmouseover="this.src='Images/Cancel_hvr.gif'" ValidationGroup="VGMakeModelSeries"
                                    OnClick="btnCancel_Click" Visible="false" />
                            </td>
                            <td colspan="2">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlReport1_2_3">
        <table width="100%">
            <tr id="trRowToDisp" runat="server" visible="false">
                <td align="left" style="padding-left: 5px;">
                    <asp:UpdatePanel ID="UpPnl30days" runat="server">
                        <ContentTemplate>
                            <b>For Downloading All Search Survey Details
                                <asp:LinkButton ID="lnk30Days" runat="server" Text=" click here" OnClick="lnk30Days_Click"></asp:LinkButton></b>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="lnk30Days" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="right">
                    <asp:Label ID="lblRepoty123" runat="server">Rows To Display</asp:Label>
                    <asp:DropDownList ID="drpreportypePaging" runat="server" AutoPostBack="true" Width="50px"
                        OnSelectedIndexChanged="drpreportypePaging_SelectedIndexChanged">
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
                    <asp:UpdatePanel ID="uppanReport" runat="server">
                        <ContentTemplate>
                            <asp:Panel runat="server" ID="pnlreport1_2">
                                <%--All Completed Survey Report / All Completed Survey Report by Primary Contact / Search by Keyword--%>
                                <asp:GridView runat="server" ID="grdSearchReport1_2" AutoGenerateColumns="false"
                                    OnRowCommand="grdSearchReport1_2_RowCommand" OnPageIndexChanging="grdSearchReport1_2_PageIndexChanging"
                                    OnSorting="grdSearchReport1_2_Sorting" Width="100%" EmptyDataText="No Records Found."
                                    AllowPaging="true" AllowSorting="true" AlternatingRowStyle-BackColor="#d5ecfd"
                                    RowStyle-Height="30px" DataKeyNames="id" data OnRowDataBound="grdSearchReport1_2_RowDataBound">
                                    <FooterStyle CssClass="gvFooterrow" />
                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle CssClass="pgr" />
                                    <HeaderStyle BackColor="#0A73A2" Font-Bold="True" HorizontalAlign="Center" CssClass="gvHeader"
                                        Height="30px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Customer Name" SortExpression="name">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblCustomername" Text='<%# bind("Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="State" SortExpression="state">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblstate" Text='<%# bind("state") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Make" SortExpression="make">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblmake" Text='<%# bind("make") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dealer Name" SortExpression="dealername">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lbldealername" Text='<%# bind("dealername") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Consultant Name" SortExpression="ConsultantName">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblConsultantName" Text='<%# bind("ConsultantName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkViewServayAnswer" Text="View" CommandName="ServayDetail"
                                                    CommandArgument='<%# bind("id") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkDownloadcsv" Text="Download csv" CommandName="DownloadCSV"
                                                    CommandArgument='<%# bind("id") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="grdSearchReport1_2" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <%-- <asp:UpdateProgress ID="upProcess_csv" DisplayAfter="10" runat="server" AssociatedUpdatePanelID="uppanReport">
                        <ProgressTemplate>
                            <div id="progressBackgroundFilter">
                            </div>
                            <div id="processMessage">
                                <span style="text-align: center;">
                                    <img src="images/loading.gif" /><br />
                                    Downloading...Please wait...</span></div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>--%>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlSR" runat="server" DefaultButton="btnGenerateReport">
        <table width="100%">
            <tr>
                <td colspan="2" style="height: 15px;">
                </td>
            </tr>
            <tr>
                <td align="right" colspan="2">
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="left" style="width: 70%">
                                <asp:Label ID="lblOverallR" Visible="false" Font-Bold="true" runat="server"></asp:Label>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblRowsToDisplay" runat="server" Visible="false">Rows To Display</asp:Label>
                                <asp:DropDownList ID="ddl_NoRecords" runat="server" AutoPostBack="true" Visible="false"
                                    Width="50px" OnSelectedIndexChanged="ddl_NoRecords_SelectedIndexChanged">
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
                <td colspan="2">
                    <asp:Panel ID="pnlAll" runat="server">
                        <asp:GridView ID="gvAll" AllowPaging="true" AutoGenerateColumns="false" runat="server"
                            DataKeyNames="ID" Width="100%" PageSize="10" BackColor="White" BorderColor="#CCCCCC"
                            BorderStyle="None" BorderWidth="1px" CellPadding="3" AllowSorting="True" OnRowDataBound="gvAll_RowDataBound"
                            OnPageIndexChanging="gvAll_PageIndexChanging" OnSorting="gvAll_Sorting" EmptyDataText="No Records Found"
                            OnRowCommand="gvAll_RowCommand" AlternatingRowStyle-BackColor="#d5ecfd">
                            <Columns>
                                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                                <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                                <asp:TemplateField HeaderText="Overall Rating" SortExpression="Rating">
                                    <ItemTemplate>
                                        <%--<ajaxToolkit:Rating ID="userRating" runat="server" MaxRating="5" ReadOnly="true"
                                        FilledStarCssClass="filledRatingStar" WaitingStarCssClass="waitingRatingStar"
                                        EmptyStarCssClass="emptyRatingStar" />--%>
                                        <ajaxToolkit:Rating ID="userRating" runat="server" StarCssClass="ratingStar" WaitingStarCssClass="savedRatingStar"
                                            FilledStarCssClass="filledRatingStar" MaxRating="10" EmptyStarCssClass="emptyRatingStar"
                                            ReadOnly="true" Visible="false">
                                        </ajaxToolkit:Rating>
                                        <asp:Label ID="lblRating" runat="server" Text='<% #Eval("Rating") %>' />
                                        <asp:HiddenField ID="hdfTotal" runat="server" Value='<% # Bind("Total") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="65px" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkbtnViewCustomer" Text="View Customer" CommandArgument='<%# bind("ID") %>'
                                            CommandName="Search"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="gvFooterrow" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <PagerStyle CssClass="pgr" />
                            <HeaderStyle BackColor="#0A73A2" Font-Bold="True" HorizontalAlign="Center" CssClass="gvHeader"
                                Height="30px" />
                        </asp:GridView>
                    </asp:Panel>
                    <asp:Panel ID="pnlIndi" runat="server" Visible="false">
                        <%--Dealer Individual Survery Details--%>
                        <asp:GridView ID="gvIndi" AllowPaging="true" AutoGenerateColumns="false" runat="server"
                            DataKeyNames="ID" Width="100%" PageSize="10" BackColor="White" BorderColor="#CCCCCC"
                            BorderStyle="None" BorderWidth="1px" CellPadding="3" AllowSorting="True" OnRowDataBound="gvIndi_RowDataBound"
                            OnPageIndexChanging="gvIndi_PageIndexChanging" OnSorting="gvIndi_Sorting" EmptyDataText="No Records Found">
                            <Columns>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdfDealerID" runat="server" Value='<% #Eval("ID") %>' />
                                        <asp:HiddenField ID="hdfClientID" runat="server" Value='<% #Eval("ClientID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ClientName" HeaderText="Client Name" SortExpression="ClientName" />
                                <asp:BoundField DataField="Name" Visible="false" HeaderText="Dealer Name" SortExpression="Name" />
                                <asp:TemplateField HeaderText="Dealer Rating" SortExpression="Rating">
                                    <ItemTemplate>
                                        <ajaxToolkit:Rating ID="dealerRating" runat="server" StarCssClass="ratingStar" WaitingStarCssClass="savedRatingStar"
                                            FilledStarCssClass="filledRatingStar" MaxRating="10" EmptyStarCssClass="emptyRatingStar"
                                            ReadOnly="true" Visible="false">
                                        </ajaxToolkit:Rating>
                                        <asp:Label ID="lblDRating" runat="server" Text='<% #Eval("Rating") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="65px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="Make" HeaderText="Make" SortExpression="Make" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="CSatis" HeaderText="Client Satisfaction" SortExpression="CSatis"
                                    ItemStyle-Width="80" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="DService" HeaderText="Service Note" SortExpression="DService"
                                    ItemStyle-Width="200" />
                            </Columns>
                            <FooterStyle CssClass="gvFooterrow" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <PagerStyle CssClass="pgr" />
                            <HeaderStyle BackColor="#0A73A2" Font-Bold="True" HorizontalAlign="Center" CssClass="gvHeader"
                                Height="30px" />
                        </asp:GridView>
                        <%--Consultant Individual Survery Details--%>
                        <asp:GridView ID="gvIndiConsultant" AllowPaging="true" AutoGenerateColumns="false"
                            runat="server" DataKeyNames="ID" Width="100%" PageSize="10" BackColor="White"
                            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" AllowSorting="True"
                            OnRowDataBound="gvIndiConsultant_RowDataBound" Visible="false" OnPageIndexChanging="gvIndiConsultant_PageIndexChanging"
                            OnSorting="gvIndiConsultant_Sorting" EmptyDataText="No Records Found">
                            <Columns>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdfConsultantID" runat="server" Value='<% #Eval("ID") %>' />
                                        <asp:HiddenField ID="hdfClientID" runat="server" Value='<% #Eval("ClientID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ClientName" HeaderText="Client Name" SortExpression="ClientName" />
                                <asp:BoundField DataField="Name" Visible="false" HeaderText="Consultant Name" SortExpression="Name" />
                                <asp:TemplateField HeaderText="Consultant Rating" SortExpression="Rating">
                                    <ItemTemplate>
                                        <ajaxToolkit:Rating ID="consultantRating" runat="server" StarCssClass="ratingStar"
                                            WaitingStarCssClass="savedRatingStar" FilledStarCssClass="filledRatingStar" MaxRating="10"
                                            EmptyStarCssClass="emptyRatingStar" ReadOnly="true" Visible="false">
                                        </ajaxToolkit:Rating>
                                        <asp:Label ID="lblCRating" runat="server" Text='<% #Eval("Rating") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="65px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="CService" HeaderText="Service Note" SortExpression="CService"
                                    ItemStyle-Width="200" />
                                <asp:BoundField DataField="Sugg" HeaderText="Suggestion" SortExpression="Sugg" ItemStyle-Width="200"
                                    ItemStyle-HorizontalAlign="Left" />
                            </Columns>
                            <FooterStyle CssClass="gvFooterrow" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <PagerStyle CssClass="pgr" />
                            <HeaderStyle BackColor="#0A73A2" Font-Bold="True" HorizontalAlign="Center" CssClass="gvHeader"
                                Height="30px" />
                        </asp:GridView>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <br />
    <asp:HiddenField ID="status" runat="server" Value="1" />
    <%--Panel for Customer list of Dealer--%>
    <asp:Panel runat="server" ID="pnlServayCustomerlist">
        <table width="98%">
            <tr>
                <td style="padding-right: 10px;" align="right">
                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/back.gif" onmouseout="this.src='Images/back.gif'"
                        onmouseover="this.src='Images/back_hvr.gif'" OnClick="ImageButton2_Click" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblCustoerRowShow" runat="server" Visible="false">Rows To Display</asp:Label>
                    <asp:DropDownList ID="ddlCustomer" runat="server" AutoPostBack="true" Visible="false"
                        Width="50px" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged">
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
                    <asp:GridView runat="server" ID="grdServayCustomerList" AutoGenerateColumns="false"
                        RowStyle-Height="30px" Width="100%" OnRowCommand="grdServayCustomerList_RowCommand"
                        OnPageIndexChanging="grdServayCustomerList_PageIndexChanging" OnSorting="grdServayCustomerList_Sorting"
                        EmptyDataText="No Record Found." AllowSorting="true">
                        <FooterStyle CssClass="gvFooterrow" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <PagerStyle CssClass="pgr" />
                        <HeaderStyle BackColor="#0A73A2" Font-Bold="True" HorizontalAlign="Center" CssClass="gvHeader"
                            Height="30px" />
                        <Columns>
                            <asp:TemplateField HeaderText="Customer Name" SortExpression="name">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblCustomerName" Text='<%# bind("name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Email">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblCustomerEmail" Text='<%# bind("email") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="State" SortExpression="state">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblCustomerstate" Text='<%# bind("state") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Make" SortExpression="make">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblCustomerMake" Text='<%# bind("make") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkViewServayAnswer" Text="View" CommandName="ServayDetail"
                                        CommandArgument='<%# bind("id") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <%--Panel for Display Survey Details--%>
    <asp:Panel ID="pnlSurvey" runat="server" Visible="false">
        <table width="100%" cellspacing="2" cellpadding="2">
            <tr>
                <td style="padding-right: 10px" align="right">
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/back.gif" onmouseout="this.src='Images/back.gif'"
                        onmouseover="this.src='Images/back_hvr.gif'" OnClick="ImageButton1_Click" />
                </td>
            </tr>
            <tr>
                <td style="background-color: #0A73A2">
                    <table width="100%">
                        <tr>
                            <td style="color: White; font-weight: bold" align="left">
                                Customer Satisfaction Survey
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <div class="headderContent">
                        <div style="float: left">
                            <div class="headerSubTitle">
                                About Private Fleet - (Question 1 of 5)
                            </div>
                            <div class="content">
                                <table width="100%" class="commonText" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td style="padding: 10px 0 10px 10px;">
                                            a.
                                        </td>
                                        <td width="428">
                                            <asp:Label ID="lbl_APFQ1" runat="server" Text="Where did you hear about Private Fleet?"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lbl_APFQ1_Answer"></asp:Label>
                                            <br />
                                            <asp:TextBox ID="txtAPFHear" runat="server" CssClass="input" Visible="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 10px 0 10px 10px;">
                                            b.
                                        </td>
                                        <td width="428">
                                            <asp:Label ID="lbl_APFQ2" runat="server" Text="What was it about Private Fleet’s service offering that originally appealed to you?"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lbl_APFQ2_Answer"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="headerSubTitle">
                                About Consultant - (Question 2 of 5)
                            </div>
                            <div class="content">
                                <table width="100%" class="commonText" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td style="padding: 10px 0 10px 10px;">
                                            a.
                                        </td>
                                        <td width="428">
                                            <asp:Label ID="lbl_ConsultantQ1" runat="server" Text="Please indicated which consultant was responsible for sourcing your new vehicle?"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lbl_ConsultantQ1_Answer"></asp:Label>
                                            <asp:DropDownList ID="ddlConsultant" runat="server" CssClass="select" Visible="false">
                                            </asp:DropDownList>
                                            <asp:TextBox ID="txtConsultant" runat="server" CssClass="input" Visible="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 10px 0 10px 10px;">
                                            b.
                                        </td>
                                        <td width="428">
                                            <asp:Label ID="lbl_ConsultantQ2" runat="server" Text="How would you rate the service you received from your consultant?"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lbl_ConsultantQ2_Answer"></asp:Label>
                                            <asp:RadioButtonList ID="radio_Consultant" runat="server" CssClass="paddingRemove"
                                                RepeatDirection="Horizontal" Visible="false">
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 10px 0 10px 10px;">
                                            c.
                                        </td>
                                        <td width="428">
                                            <asp:Label ID="lbl_ConsultantQ3" runat="server" Text="Could you write a few words regarding the service you received specifically from your consultant?"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lbl_ConsultantQ3_Answer"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 10px 0 10px 10px;">
                                            d.
                                        </td>
                                        <td width="428">
                                            <asp:Label ID="lbl_ConsultantQ4" runat="server" Text="What one suggestion would you make to improve the service you received from your consultant?"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lbl_ConsultantQ4_Answer"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="content contentBorder">
                            </div>
                            <div class="headerSubTitle">
                                Administration and Updates - (Question 3 of 5)
                            </div>
                            <div class="content">
                                <table width="100%" class="commonText" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td style="padding: 10px 0 10px 10px;">
                                            a.
                                        </td>
                                        <td width="428">
                                            <asp:Label ID="lbl_AdminQ1" runat="server" Text="Who was your primary contact from Private Fleet between order and delivery?"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lbl_AdminQ1_Answer"></asp:Label>
                                            <asp:DropDownList ID="ddlAdminContact" CssClass="select" runat="server" AutoPostBack="true"
                                                Visible="false">
                                                <asp:ListItem Value="" Text="--Select--"></asp:ListItem>
                                                <asp:ListItem Value="Laura Martin" Text="Laura Martin"></asp:ListItem>
                                                <asp:ListItem Value="Catherine Heyes" Text="Catherine Heyes"></asp:ListItem>
                                                <asp:ListItem Value="Anna Mears" Text="Anna Mears"></asp:ListItem>
                                                <asp:ListItem Value="Other" Text="Other"></asp:ListItem>
                                            </asp:DropDownList>
                                            <br />
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 10px 0 10px 10px;">
                                            b.
                                        </td>
                                        <td width="428">
                                            <asp:Label ID="lbl_AdminQ2" runat="server" Text="How would you rate the service you received from this person?"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lbl_AdminQ2_Answer"></asp:Label>
                                            <asp:RadioButtonList ID="radio_AdminService" runat="server" CssClass="paddingRemove"
                                                RepeatDirection="Horizontal" Visible="false">
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 10px 0 10px 10px;">
                                            c.
                                        </td>
                                        <td width="428">
                                            <asp:Label ID="lbl_AdminQ3" runat="server" Text="Specifically were you kept up to date as to the estimated delivery of your vehicle to your satisfaction?"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lbl_AdminQ3_Answer"></asp:Label>
                                            <asp:RadioButtonList ID="radio_AdminEstumateDelivery" runat="server" CssClass="paddingRemove"
                                                RepeatDirection="Horizontal" Visible="false">
                                                <asp:ListItem Value="Y" Text="Yes"></asp:ListItem>
                                                <asp:ListItem Value="N" Text="No"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 10px 0 10px 10px;">
                                            d.
                                        </td>
                                        <td width="428">
                                            <asp:Label ID="lbl_AdminQ4" runat="server" Text="Could you write a few words regarding the service you received specifically from your administration contact?"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lbl_AdminQ4_Answer"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 10px 0 10px 10px;">
                                            e.
                                        </td>
                                        <td width="428">
                                            <asp:Label ID="lbl_AdminQ5" runat="server" Text="What one suggestion would you make to improve the service you received from your administration contact?"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lbl_AdminQ5_Answer"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="headerSubTitle">
                                Dealership - (Question 4 of 5)
                            </div>
                            <div class="content">
                                <table width="100%" class="commonText" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td style="padding: 10px 0 10px 10px;">
                                            a.
                                        </td>
                                        <td width="428">
                                            <asp:Label ID="lbl_DealerQ1" runat="server" Text="What was the make of vehicle that you purchased?"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lbl_DealerQ1_Answer"></asp:Label>
                                            <asp:DropDownList ID="ddlMake" runat="server" CssClass="select" AutoPostBack="true"
                                                Visible="false">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 10px 0 10px 10px;">
                                            b.
                                        </td>
                                        <td width="428">
                                            <asp:Label ID="lbl_DealerQ2" runat="server" Text="Which was the supplying dealer for your new car?"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lbl_DealerQ2_Answer"></asp:Label>
                                            <asp:DropDownList ID="ddlDealer" runat="server" CssClass="select" Visible="false">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 10px 0 10px 10px;">
                                            c.
                                        </td>
                                        <td width="428">
                                            <asp:Label ID="lbl_DealerQ3" runat="server" Text="How would you rate the service you received from the dealer?"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lbl_DealerQ3_Answer"></asp:Label>
                                            <asp:RadioButtonList Visible="false" ID="radio_DealerService" runat="server" CssClass="paddingRemove"
                                                RepeatDirection="Horizontal">
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 10px 0 10px 10px;">
                                            d.
                                        </td>
                                        <td width="428">
                                            <asp:Label ID="lbl_DealerQ4" runat="server" Text="Was the vehicle delivered to your satisfaction (eg on time, perfectly presented, all options present, full tank of fuel)"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lbl_DealerQ4_Answer"></asp:Label>
                                            <asp:RadioButtonList ID="radio_DealerSatisfiction" runat="server" CssClass="paddingRemove"
                                                Visible="false" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="Y" Text="Yes"></asp:ListItem>
                                                <asp:ListItem Value="N" Text="No"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 10px 0 10px 10px;">
                                            e.
                                        </td>
                                        <td width="428">
                                            <asp:Label ID="lbl_DealerQ5" runat="server" Text="Could you write a few words regarding the service you received specifically from the dealer?"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lbl_DealerQ5_Answer"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="headerSubTitle">
                                Overall - (Question 5 of 5)
                            </div>
                            <div class="content">
                                <table width="100%" class="commonText" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td style="padding: 10px 0 10px 10px;">
                                            a.
                                        </td>
                                        <td width="428">
                                            <asp:Label ID="lbl_OverallQ1" runat="server" Text="Overall how was your car buying experience?"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lbl_OverallQ1_Answer"></asp:Label>
                                            <asp:RadioButtonList ID="radio_Overall" Visible="false" runat="server" CssClass="paddingRemove"
                                                RepeatDirection="Horizontal">
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 10px 0 10px 10px;">
                                            b.
                                        </td>
                                        <td width="428">
                                            <asp:Label ID="lbl_OverallQ2" runat="server" Text="Will you recommend Private Fleet to any friends or family looking to buy a new car?"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lbl_OverallQ2_Answer"></asp:Label>
                                            <asp:RadioButtonList ID="radio_OverallRecommended" runat="server" CssClass="paddingRemove"
                                                RepeatDirection="Horizontal" Visible="false">
                                                <asp:ListItem Value="Y" Text="Yes"></asp:ListItem>
                                                <asp:ListItem Value="N" Text="No"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 10px 0 10px 10px;">
                                            c.
                                        </td>
                                        <td width="428">
                                            <asp:Label ID="lbl_OverallQ3" runat="server" Text="Could you write a few words regarding the service you received overall?"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lbl_OverallQ3_Answer"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 10px 0 10px 10px;">
                                            d.
                                        </td>
                                        <td width="428">
                                            <asp:Label ID="lbl_OverallQ4" runat="server" Text="What one suggestion would you make to improve the service you received?"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lbl_OverallQ4_Answer"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="padding-right: 10px" align="center">
                    <asp:ImageButton ID="ImageButton1_Copy" runat="server" ImageUrl="~/Images/back.gif"
                        onmouseout="this.src='Images/back.gif'" onmouseover="this.src='Images/back_hvr.gif'"
                        OnClick="ImageButton1_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
