<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="CompletedQuoatationReport.aspx.cs" Inherits="CompletedQuoatationReport"
    Title="Completed Quoatation Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        /*AutoComplete flyout */.autocomplete_completionListElement
        {
            font-weight: normal;
            font-family: Arial;
            font-size: 13px;
            border: solid 1px #acacac;
            line-height: 20px;
            padding: 0px;
            background-color: White;
            margin-left: 10px;
        }
        /* AutoComplete highlighted item */.autocomplete_highlightedListItem
        {
            color: White;
            background-color: #316AC5;
            cursor: pointer;
            border-bottom: dotted 1px #acacac;
        }
        /* AutoComplete item */.autocomplete_listItem
        {
            cursor: pointer;
            color: WindowText;
            background-color: Window;
            border-bottom: dotted 1px #acacac;
        }
    </style>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlCQR" runat="server" DefaultButton="btnGenerateReport">
                <table width="100%" align="center" cellpadding="1" cellspacing="3" border="0">
                    <tr>
                        <td align="left" colspan="6">
                            <strong>Search Criteria:</strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%--<span style="color: Red">*</span>--%>
                            Make
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlMake" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlMake_SelectedIndexChanged"
                                Height="21px" Width="170px" AppendDataBoundItems="True">
                            </asp:DropDownList>
                            <%--   <asp:RequiredFieldValidator ID="rfvMake" runat="server" ControlToValidate="ddlMake"
                                ErrorMessage="Make Required" ValidationGroup="VGMakeModelSeries" Display="None"
                                InitialValue="0" SetFocusOnError="True">
                            </asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidateMake" runat="server" TargetControlID="rfvMake"
                                HighlightCssClass="validatorCalloutHighlight">
                            </ajaxToolkit:ValidatorCalloutExtender>--%>
                        </td>
                        <td>
                            <%--<span style="color: Red">*</span>--%>
                            Model
                        </td>
                        <td>
                            <asp:TextBox ID="txtModel" AutoComplete="off" Width="170px" runat="server"></asp:TextBox>
                            <ajaxToolkit:AutoCompleteExtender ID="ACE_Model" BehaviorID="ACE_ModelEx" runat="server"
                                MinimumPrefixLength="1" ServiceMethod="GetModels" UseContextKey="true" ContextKey="Model"
                                CompletionInterval="10" CompletionSetCount="20" ServicePath="AutoComplete.asmx"
                                CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" TargetControlID="txtModel">
                                <Animations>
                                      <OnShow>
                                              <Sequence>
                                              <%-- Make the completion list transparent and then show it --%>
                                              <OpacityAction Opacity="0" />
                                              <HideAction Visible="true" />

                                              <%--Cache the original size of the completion list the first time
                                                the animation is played and then set it to zero --%>
                                              <ScriptAction Script="// Cache the size and setup the initial size
                                                                            var behavior = $find('ACE_ModelEx');
                                                                            if (!behavior._height) {
                                                                                var target = behavior.get_completionList();
                                                                                behavior._height = target.offsetHeight - 2;
                                                                                target.style.height = '0px';
                                                                            }" />
                                              <%-- Expand from 0px to the appropriate size while fading in --%>
                                              <Parallel Duration=".4">
                                                  <FadeIn />
                                                  <Length PropertyKey="height" StartValue="0" EndValueScript="$find('ACE_ModelEx')._height" />
                                              </Parallel>
                                              </Sequence>
                                      </OnShow>
                                      <OnHide>
                                              <%-- Collapse down to 0px and fade out --%>
                                              <Parallel Duration=".4">
                                                  <FadeOut />
                                                  <Length PropertyKey="height" StartValueScript="$find('ACE_ModelEx')._height" EndValue="0" />
                                              </Parallel>
                                      </OnHide>
                                </Animations>
                            </ajaxToolkit:AutoCompleteExtender>
                            <asp:DropDownList ID="ddlModel" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlModel_SelectedIndexChanged"
                                Height="21px" Width="170px" Visible="false">
                                <asp:ListItem Value="0">-Select Model-</asp:ListItem>
                            </asp:DropDownList>
                            <%--   <asp:RequiredFieldValidator ID="rfvModel" runat="server" ControlToValidate="ddlModel"
                                ErrorMessage="Model Required" ValidationGroup="VGMakeModelSeries" Display="None"
                                InitialValue="0" SetFocusOnError="True">
                            </asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorModel" runat="server" TargetControlID="rfvModel"
                                HighlightCssClass="validatorCalloutHighlight">
                            </ajaxToolkit:ValidatorCalloutExtender>--%>
                        </td>
                        <td>
                            <%--  Series--%>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSeries" runat="server" Height="21px" Width="119px" Display="None"
                                InitialValue="-Select-" AutoPostBack="true" Visible="false" OnSelectedIndexChanged="ddlSeries_SelectedIndexChanged">
                                <asp:ListItem Value="0">-Select Series-</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Transmission
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlTransmission" runat="server" Height="21px" Width="170px"
                                InitialValue="0">
                                <asp:ListItem Value="0">-Select Transmision-</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            State
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlState" runat="server" Height="21px" Width="170px" Display="None">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <%--  Winning Quote--%>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlWin" runat="server" Height="21px" Width="119px" Display="None"
                                InitialValue="-Select-" AppendDataBoundItems="True" Visible="false">
                                <asp:ListItem Value="0">-Select Winning-</asp:ListItem>
                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                <asp:ListItem Value="2">No</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span style="color: Red">*</span> From Date
                        </td>
                        <td>
                            <asp:TextBox ID="txtCalenderFrom" runat="server" Width="170px" MaxLength="10"></asp:TextBox>
                            <asp:Image ID="imgCal" runat="server" ImageUrl="~/Images/Calendar.gif" />
                            <asp:RequiredFieldValidator runat="server" ID="requiredFromdate" ControlToValidate="txtCalenderFrom"
                                ErrorMessage="Enter From Date." Display="None" ValidationGroup="VGMakeModelSeries"></asp:RequiredFieldValidator>
                            <ajaxToolkit:CalendarExtender ID="calFrom" runat="server" TargetControlID="txtCalenderFrom"
                                PopupButtonID="imgCal" Format="dd/MM/yyyy">
                            </ajaxToolkit:CalendarExtender>
                            <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender2"
                                TargetControlID="requiredFromdate" HighlightCssClass="validatorCalloutHighlight" />
                            <%--    validation for date format check  dd/mm/yy--%>
                            <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidatorDatecheck"
                                Display="None" ControlToValidate="txtCalenderFrom" ErrorMessage="Enter proper date in dd/mm/yyyy format."
                                ValidationGroup="VGMakeModelSeries" SetFocusOnError="False" ValidationExpression='^(((0?[1-9]|[12]\d|3[01])\/(0?[13578]|1[02])\/((1[6-9]|[2-9]\d)\d{2}))|((0?[1-9]|[12]\d|30)\/(0?[13456789]|1[012])\/((1[6-9]|[2-9]\d)\d{2}))|((0?[1-9]|1\d|2[0-8])\/0?2\/((1[6-9]|[2-9]\d)\d{2}))|(29\/0?2\/((1[6-9]|[2-9]\d)(0?[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$'></asp:RegularExpressionValidator>
                            <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender3"
                                TargetControlID="RegularExpressionValidatorDatecheck" HighlightCssClass="validatorCalloutHighlight" />
                        </td>
                        <td>
                            <span style="color: Red">*</span> To Date
                        </td>
                        <td>
                            <asp:TextBox ID="TxtToDate" runat="server" Width="170px" MaxLength="10"></asp:TextBox>
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Calendar.gif" />
                            <ajaxToolkit:CalendarExtender ID="calTO" runat="server" TargetControlID="TxtToDate"
                                PopupButtonID="Image1" Format="dd/MM/yyyy">
                            </ajaxToolkit:CalendarExtender>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="TxtToDate"
                                ErrorMessage="Enter To Date." Display="None" ValidationGroup="VGMakeModelSeries"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender1"
                                TargetControlID="RequiredFieldValidator1" HighlightCssClass="validatorCalloutHighlight" />
                            <%-- validation for date format check in dd/mm/yyyy--%>
                            <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1" Display="None"
                                ControlToValidate="TxtToDate" ErrorMessage="Enter proper date in dd/mm/yyyy format."
                                ValidationGroup="VGMakeModelSeries" SetFocusOnError="False" ValidationExpression='^(((0?[1-9]|[12]\d|3[01])\/(0?[13578]|1[02])\/((1[6-9]|[2-9]\d)\d{2}))|((0?[1-9]|[12]\d|30)\/(0?[13456789]|1[012])\/((1[6-9]|[2-9]\d)\d{2}))|((0?[1-9]|1\d|2[0-8])\/0?2\/((1[6-9]|[2-9]\d)\d{2}))|(29\/0?2\/((1[6-9]|[2-9]\d)(0?[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$'></asp:RegularExpressionValidator>
                            <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender4"
                                TargetControlID="RegularExpressionValidator1" HighlightCssClass="validatorCalloutHighlight" />
                            <%--validation for date comparision --%>
                            <asp:CustomValidator runat="server" ID="cust1" ClientValidationFunction="compaire_dates_CompletedQuoatationReport"
                                ValidationGroup="VGMakeModelSeries" ErrorMessage="From date can not be greater than to date."
                                Display="None" ControlToValidate="txtCalenderFrom"> </asp:CustomValidator>
                            <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="validatorcallourdatecomparision"
                                TargetControlID="cust1" HighlightCssClass="validatorCalloutHighlight">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="font-size: 11px;">
                            <b><i>Example ( dd/mm/yyyy )</i></b>
                        </td>
                        <td>
                        </td>
                        <td style="font-size: 11px;">
                            <b><i>Example ( dd/mm/yyyy )</i></b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Fuel Type
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlFuelType" runat="server" Height="21px" Width="170px" InitialValue="0">
                                <asp:ListItem Value="0">-Select Fuel Type-</asp:ListItem>
                                <asp:ListItem Value="Diesel">Diesel</asp:ListItem>
                                <asp:ListItem Value="Dual Fuel">Dual Fuel</asp:ListItem>
                                <asp:ListItem Value="Hybrid">Hybrid</asp:ListItem>
                                <asp:ListItem Value="LPG">LPG</asp:ListItem>
                                <asp:ListItem Value="Petrol">Petrol</asp:ListItem>
                                <asp:ListItem Value="Turbo Diesel">Turbo Diesel</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="right">
                            <asp:ImageButton ID="btnGenerateReport" runat="server" ImageUrl="~/Images/Generate_report_hvr.gif"
                                onmouseout="this.src='Images/Generate_report_hvr.gif'" onmouseover="this.src='Images/Generate_report.gif'"
                                ValidationGroup="VGMakeModelSeries" OnClick="btnGenerateReport_Click" />
                        </td>
                        <td colspan="2" align="left">
                            <asp:ImageButton ID="btnCancel" runat="server" ImageUrl="~/Images/Cancel.gif" onmouseout="this.src='Images/Cancel.gif'"
                                onmouseover="this.src='Images/Cancel_hvr.gif'" ValidationGroup="VGMakeModelSeries"
                                OnClick="btnCancel_Click" CausesValidation="false" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" align="center">
                            <asp:Label ID="lblMsg" runat="server" Visible="false"><strong>No record to display.</strong></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" colspan="6">
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
                        <td colspan="6" align="center">
                            <asp:GridView ID="gvReport" runat="server" AllowPaging="true" AllowSorting="true"
                                PageSize="15" AutoGenerateColumns="false" Visible="true" Width="100%" BackColor="White"
                                BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" OnRowDeleting="gvReport_RowDeleting"
                                EmptyDataText="No record to display." OnPageIndexChanging="gvReport_PageIndexChanging"
                                OnSorting="gvReport_Sorting" OnRowDataBound="gvReport_RowDataBound" DataKeyNames="QuotationID,RequestID,UserID,DealerID">
                                <Columns>
                                    <asp:TemplateField HeaderText="Make" SortExpression="Make">
                                        <ItemTemplate>
                                            <a id="lnkViewQuote" runat="server" href='<%# "ViewQuotation.aspx?QuoteID=" + Eval("QuotationID") + "&ReqID="+ Eval("RequestID") + "&UserID="+ Eval("UserID") + "&DID="+ Eval("DealerID")+"&chk=fromAdminCQR"%>'>
                                                <asp:Label ID="lblMake" Visible="false" runat="server" Text="<%# Bind('Make') %>"></asp:Label>
                                            </a>
                                            <asp:LinkButton ID="lnkMake" runat="server" Text='<%#Bind("Make") %>' OnClick="lnkMake_Click"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Make" DataField="Make" SortExpression="Make" Visible="false" />
                                    <asp:BoundField DataField="Model" HeaderText="Model" SortExpression="Model" />
                                    <%--<asp:BoundField DataField="Series" HeaderText="Series" />--%>
                                    <asp:BoundField DataField="BodyShape" HeaderText="Body" SortExpression="BodyShape" />
                                    <asp:BoundField DataField="Transmission" HeaderText="Trans" SortExpression="Transmission" />
                                    <asp:BoundField DataField="FuelType" HeaderText="Fuel" SortExpression="FuelType" />
                                    <asp:BoundField DataField="State" HeaderText="State" SortExpression="State" />
                                    <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="DateSorting" />
                                    <asp:BoundField DataField="Winning" HeaderText="Winning" SortExpression="Winning"
                                        Visible="false" />
                                    <asp:BoundField DataField="Dealer" HeaderText="Dealer" SortExpression="Dealer" />
                                    <asp:BoundField DataField="WithoutAAPrice" HeaderText="Price" SortExpression="WithoutAAPrice" />
                                    <%--  <asp:BoundField DataField="AddAccPrice" HeaderText="Additional Accessories Price"
                                        SortExpression="AddAccPrice" Visible="false" />--%>
                                </Columns>
                                <FooterStyle CssClass="gvFooterrow" />
                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                <PagerStyle CssClass="pgr" />
                                <HeaderStyle BackColor="#0A73A2" Font-Bold="True" CssClass="gvHeader" Height="30px" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlConsultantQR" runat="server" Visible="false">
                <br />
                <br />
                <table style="padding: 5px; line-height: 22px; border: solid 1px #acacac;" align="center"
                    width="90%" cellpadding="4" cellspacing="4">
                    <tr style="background-color: #0A73A2; font-weight: bold; color: White; padding-left: 10px;">
                        <td>
                            To View this Report -
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td style="width: 4%; vertical-align: top;">
                                        1.
                                    </td>
                                    <td style="width: 96%;">
                                        <asp:Label ID="lblC1" runat="server" Text="You need to have selected at least xx% of winning quotes from quotes received in the past 14 days but we can only see XX%</b>"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp; <span style="padding-left: 15px; font-size: 11px; font-weight: bold;">
                                            please <a href="ViewSentRequests.aspx" target="_parent">click here</a> to Shortlist
                                            more Quotation</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="padding-left: 30px; font-size: 14px;">
                                        <b>AND</b>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top;">
                                        2.
                                    </td>
                                    <td>
                                        <asp:Label ID="lblC2" runat="server" Text="You have made X finance referrals in the last 5 full working days but we can only see y"></asp:Label>
                                        <br />
                                        <span style="font-size: 11px; font-weight: bold;">please <a href="Finance_Referral.aspx"
                                            target="_parent">click here</a> to give more Finance Referral</span>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
