<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ConsultantTradeInReport.aspx.cs" Inherits="ConsultantTradeInReport"
    ValidateRequest="false" %>

<%@ Register Src="~/User Controls/ucTradeInData.ascx" TagName="ucTradeInData" TagPrefix="uc1" %>
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

    <script type="text/javascript" language="javascript">

        function isNumberKey(evt, chk) {
            var charCode = (evt.which) ? evt.which : evt.keyCode
            // alert(charCode);

            // 9- tab  46 del
            if (charCode == 9 || charCode == 8 || charCode == 27 || charCode == 37 || charCode == 39)   // 8 - backspace 27 - Escape
                return true;


            if (charCode == 13) {
                return false;
            }

            //for numbers only
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            else
                return true;
        }    </script>

    <%--  <asp:UpdatePanel UpdateMode="Conditional" ID="upnlSearchC" runat="server">
        <ContentTemplate> --%>
    <asp:Panel ID="pnlTradeIn" runat="server" DefaultButton="btnGenerateReport">
        <table width="100%" align="center" cellpadding="1" cellspacing="3" border="0">
            <tr>
                <td align="left" colspan="4">
                    <strong>Search Criteria:</strong>
                </td>
            </tr>
            <tr>
                <td>
                    <%--<span style="color: Red">*</span>--%>
                    Make
                </td>
                <td>
                    <asp:DropDownList ID="ddlMake" runat="server" Height="21px" Width="170px" AppendDataBoundItems="True"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlMake_SelectedIndexChanged">
                    </asp:DropDownList>
                    <%--    <asp:RequiredFieldValidator ID="RFV_Make" runat="server" ControlToValidate="ddlMake"
                        ErrorMessage="Make Required to Save Search." ValidationGroup="TradeIn" Display="None"
                        InitialValue="0" SetFocusOnError="True" Width="100%"> </asp:RequiredFieldValidator>
                    <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="VCE_Make" TargetControlID="RFV_Make"
                        HighlightCssClass="validatorCalloutHighlight" />--%>
                </td>
                <td>
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
                </td>
            </tr>
            <tr>
                <td>
                    Transmission
                </td>
                <td>
                    <asp:DropDownList ID="ddlTransmission" runat="server" Height="21px" Width="170px">
                    </asp:DropDownList>
                </td>
                <td>
                    <%--<span style="color: Red">*</span>--%>
                    State
                </td>
                <td>
                    <asp:DropDownList ID="ddlState" runat="server" Height="21px" Width="170px" AppendDataBoundItems="True">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <%-- <span style="color: Red">*</span> --%>From Year
                </td>
                <td>
                    <asp:TextBox ID="txtT1FromYear" runat="server" Width="170px" onkeypress="return isNumberKey(event,this);"
                        MaxLength="4"></asp:TextBox>
                </td>
                <td>
                    <%-- <span style="color: Red">*</span>--%>
                    To Year
                </td>
                <td>
                    <asp:TextBox ID="txtT1ToYear" runat="server" Width="170px" onkeypress="return isNumberKey(event,this);"
                        MaxLength="4"></asp:TextBox>
                    <asp:CustomValidator runat="server" ControlToValidate="txtT1FromYear" ID="customval1_checkyears"
                        ClientValidationFunction="compaire_yearsin" ErrorMessage="From year must be less than or equal to To year."
                        Display="None" ValidationGroup="TradeIn"></asp:CustomValidator>
                    <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender3"
                        TargetControlID="customval1_checkyears" HighlightCssClass="validatorCalloutHighlight" />
                </td>
            </tr>
            <tr>
                <td>
                    <%--<span style="color: Red">*</span>--%>
                    Min Value
                </td>
                <td>
                    <asp:TextBox ID="txtValueFrom" runat="server" Width="170px" onkeypress="return isNumberKey(event,this);"></asp:TextBox>
                    <asp:CustomValidator runat="server" ControlToValidate="txtValueFrom" ID="CustomValidator1"
                        ClientValidationFunction="compaire_minMaxvalues" ErrorMessage="Min value must be smaller or equal to  Max. value."
                        Display="None" ValidationGroup="TradeIn"></asp:CustomValidator>
                    <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender1"
                        TargetControlID="CustomValidator1" HighlightCssClass="validatorCalloutHighlight" />
                </td>
                <td>
                    <%-- <span style="color: Red">*</span>--%>
                    Max Value
                </td>
                <td>
                    <asp:TextBox ID="txtValueTo" runat="server" Width="170px" onkeypress="return isNumberKey(event,this);"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Rego No.
                </td>
                <td>
                    <asp:TextBox ID="txtRegoNo" runat="server" Width="170px"></asp:TextBox>
                </td>
                <td>
                    Surname
                </td>
                <td>
                    <asp:TextBox ID="txtSurname" runat="server" Width="170px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Consultant
                </td>
                <td>
                    <asp:TextBox ID="txtConsultant" runat="server" Width="170px"></asp:TextBox>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td colspan="4" align="center" valign="bottom" style="padding-top: 10px;">
                    <asp:ImageButton ID="btnViewSearch" runat="server" ImageUrl="~/Images/view_saved_searches_hover.gif"
                        onmouseout="this.src='Images/view_saved_searches_hover.gif'" onmouseover="this.src='Images/view_saved_searches.gif'"
                        ValidationGroup="TradeIn" OnClick="btnViewSearch_Click" CausesValidation="false" />
                    <asp:ImageButton ID="btnSaveasAlert" runat="server" ImageUrl="~/Images/save-serach_hover.gif"
                        onmouseout="this.src='Images/save-serach_hover.gif'" onmouseover="this.src='Images/save-serach.gif'"
                        OnClick="btnSaveasAlert_Click" ValidationGroup="TradeIn" />
                    <asp:ImageButton ID="btnGenerateReport" runat="server" ImageUrl="~/Images/search_now_hover.gif"
                        onmouseout="this.src='Images/search_now_hover.gif'" onmouseover="this.src='Images/search_now.gif'"
                        ValidationGroup="TradeIn" OnClick="btnGenerateReport_Click" CausesValidation="false" />
                    <asp:ImageButton ID="btnCancel" runat="server" ImageUrl="~/Images/Cancel.gif" onmouseout="this.src='Images/Cancel.gif'"
                        onmouseover="this.src='Images/Cancel_hvr.gif'" ValidationGroup="TradeIn" CausesValidation="false"
                        OnClick="btnCancel_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="4" align="center">
                    <asp:Label ID="lblMsg" runat="server" Visible="false"><strong>No record to display.</strong></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right" colspan="4">
                    <asp:Label ID="lblRowsToDisplay_1" runat="server">Rows To Display</asp:Label>
                    <asp:DropDownList ID="ddl_NoRecords_1" runat="server" AutoPostBack="true" Width="50px"
                        OnSelectedIndexChanged="ddl_NoRecords_1_SelectedIndexChanged">
                        <asp:ListItem Value="10">10</asp:ListItem>
                        <asp:ListItem Value="20">20</asp:ListItem>
                        <asp:ListItem Value="30">30</asp:ListItem>
                        <asp:ListItem Value="50">50</asp:ListItem>
                        <asp:ListItem Value="All">All</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="4" align="center">
                    <asp:Panel ID="pnlSearch" runat="server" Visible="false">
                        <asp:GridView ID="gvSearchCriteria" runat="server" AllowPaging="true" AllowSorting="true"
                            PageSize="15" DataKeyNames="ID" AutoGenerateColumns="false" Width="100%" BackColor="White"
                            ShowFooter="false" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                            CellPadding="3" EmptyDataText="No Records Found" OnPageIndexChanging="gvSearchCriteria_PageIndexChanging"
                            OnRowCommand="gvSearchCriteria_RowCommand" OnSorting="gvSearchCriteria_Sorting">
                            <Columns>
                                <asp:BoundField DataField="LastName" NullDisplayText="--" HeaderText="Surname" SortExpression="LastName"
                                    ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="Make" NullDisplayText="--" HeaderText="Make" SortExpression="Make"
                                    ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="Model" NullDisplayText="--" HeaderText="Model" SortExpression="Model"
                                    ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="Transmission" NullDisplayText="--" HeaderText="Trans"
                                    SortExpression="Transmission" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="State" NullDisplayText="--" DataField="State" SortExpression="State"
                                    ItemStyle-HorizontalAlign="Center" Visible="false" />
                                <asp:BoundField HeaderText="From Year" NullDisplayText="--" DataField="FromYear"
                                    SortExpression="FromYear" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="ToYear" NullDisplayText="--" HeaderText="To Year" SortExpression="ToYear"
                                    ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="MinValue" NullDisplayText="--" HeaderText="Min Value"
                                    SortExpression="MinValue" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="MaxValue" NullDisplayText="--" HeaderText="Max Value"
                                    SortExpression="MaxValue" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                                <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" SortExpression="CreatedDate1"
                                    ItemStyle-Width="80" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="CustName" HeaderText="Customer Name" SortExpression="CustName"
                                    ItemStyle-Width="80" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="Contact" HeaderText="Contact" SortExpression="Contact"
                                    ItemStyle-Width="80" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="AlertPeriod" HeaderText="Alert Days" SortExpression="AlertPeriod"
                                    ItemStyle-Width="80" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="Notes" HeaderText="Notes" ItemStyle-Width="80" ItemStyle-HorizontalAlign="Left" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument="Run" CommandName="Run"
                                            Text="Run" CssClass="activeLink"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument="me_Delete" CommandName="me_Delete"
                                            Text="Delete" CssClass="activeLink"></asp:LinkButton>
                                        <asp:HiddenField ID="hdfMakeID" runat="server" Value='<%# Bind("MakeID") %>' />
                                        <asp:HiddenField ID="hdfTrans" runat="server" Value='<%# Bind("TransmissionID") %>' />
                                        <asp:HiddenField ID="hdfState" runat="server" Value='<%# Bind("StateID") %>' />
                                        <asp:HiddenField ID="hdfRegoNo" runat="server" Value='<%# Bind("RegoNumber") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="gvFooterrow" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <PagerStyle CssClass="pgr" />
                            <HeaderStyle BackColor="#0A73A2" Font-Bold="True" CssClass="gvHeader" Height="30px" />
                        </asp:GridView>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td colspan="2" valign="middle" align="left" style="padding-left: 5px;">
                    <div style="height: 12px; width: 20px; float: left; background-color: #87C8E3; border: 1px solid #acacac">
                    </div>
                    &nbsp;&nbsp; <b>Trade In with Photo</b>
                </td>
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
                <td colspan="4" align="center">
                    <asp:Panel ID="pnlTrade12" runat="server" Visible="false">
                        <asp:GridView ID="gvTInReport" runat="server" AllowPaging="true" AllowSorting="true"
                            PageSize="15" DataKeyNames="ID" AutoGenerateColumns="false" Width="100%" BackColor="White"
                            ShowFooter="false" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                            CellPadding="3" EmptyDataText="No Records Found" OnSorting="gvTInReport_Sorting"
                            OnPageIndexChanging="gvTInReport_PageIndexChanging" OnRowCommand="gvTInReport_RowCommand"
                            OnRowDataBound="gvTInReport_RowDataBound">
                            <Columns>
                                <asp:BoundField HeaderText="State" DataField="HomeState" SortExpression="HomeState"
                                    ItemStyle-Width="30px" />
                                <%-- <asp:BoundField HeaderText="City" DataField="HomeCity" SortExpression="HomeCity"
                                    ItemStyle-Width="30px" />--%>
                                <asp:BoundField HeaderText="Consultant" DataField="Consultant" SortExpression="Consultant"
                                    ItemStyle-Width="30px" />
                                <asp:BoundField DataField="Make" HeaderText="Make" SortExpression="Make" />
                                <asp:BoundField DataField="T1Model" HeaderText="Model" SortExpression="T1Model" />
                                <asp:BoundField DataField="T1Transmission" HeaderText="Trans" SortExpression="T1Transmission" />
                                <asp:BoundField DataField="T1Year" HeaderText="Year" SortExpression="T1Year" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="T1OrigValue" HeaderText="Orig Trade in Value" SortExpression="T1OrigValue" />
                                <asp:BoundField DataField="LastName" HeaderText="Surname" SortExpression="LastName" />
                                <%--<asp:BoundField DataField="T1RegoNumber" HeaderText="RegoNo" SortExpression="T1RegoNumber" />--%>
                                <asp:BoundField DataField="DeliveryDate" HeaderText="Delivery Date" SortExpression="DeliveryDateSort"
                                    ItemStyle-Width="80" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="TradeStatus" HeaderText="Trade Status" SortExpression="TradeStatus" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDetails" runat="server" CommandName="ViewDetails" Text="View Details"
                                            CssClass="activeLink"></asp:LinkButton>
                                        <asp:HiddenField ID="hdfKey" runat="server" Value='<%# Bind("Key") %>' />
                                        <asp:HiddenField ID="hdfIsPhoto" runat="server" Value='<%# Bind("IsPhoto") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="gvFooterrow" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <PagerStyle CssClass="pgr" />
                            <HeaderStyle BackColor="#0A73A2" Font-Bold="True" CssClass="gvHeader" Height="30px" />
                        </asp:GridView>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <br />
    <br />
    <asp:Panel ID="pnlTradeIn_1" runat="server" Visible="false">
        <table align="center" width="95%" cellpadding="0" cellspacing="0">
            <tr>
                <td align="right" style="padding-bottom: 10px">
                    <asp:ImageButton ID="btnBack" runat="server" ImageUrl="~/Images/back.gif" onmouseout="this.src='Images/back.gif'"
                        onmouseover="this.src='Images/back_hvr.gif'" OnClick="btnBack_Click" />
                </td>
            </tr>
        </table>
        <uc1:ucTradeInData ID="ucTradeInData1" runat="server" />
    </asp:Panel>
    <div id="divpopID" runat="server" visible="false" align="center" style="width: 570px;
        height: 250px;">
        <div id="progressBackgroundFilter">
        </div>
        <div id="processMessage1" align="center" style="width: 570px; height: 250px; border: solid 2px #000000;">
            <table align="center" width="90%" cellpadding="2" cellspacing="3">
                <tr style="height: 20px;">
                    <td colspan="2">
                        <%--<asp:Label ID="lblMkErr" runat="server" Text="Make Required to save Search. Please click on Cancle to select Make."
                            Visible="false" Font-Bold="true" ForeColor="Red"></asp:Label>--%>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%">
                        <asp:Label ID="lblCName" runat="server" Text="Customer Name - "></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCName" Width="250px" runat="server"> </asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblContact" runat="server" Text="Customer Contact No, - "></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtContact" runat="server" Width="150px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblAlertPeriod" runat="server" Text="Alert Period - "></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlAType" runat="server" Width="150" AutoPostBack="false">
                            <asp:ListItem Value="0" Text="-Select Perion-"></asp:ListItem>
                            <asp:ListItem Value="30" Text="1 Month"></asp:ListItem>
                            <asp:ListItem Value="90" Text="3 Month" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="180" Text="6 Month"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RFV_p" runat="server" ControlToValidate="ddlAType"
                            ErrorMessage="Period Required" ValidationGroup="VGAlert" Display="None" InitialValue="0"
                            SetFocusOnError="True"> </asp:RequiredFieldValidator>
                        <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="VCE_p" TargetControlID="RFV_p"
                            HighlightCssClass="validatorCalloutHighlight" />
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <asp:Label ID="lblNotes" runat="server" Text="Notes - "></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Width="350" Rows="5"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20px;" colspan="2">
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:ImageButton ID="imgbtnSave" runat="server" ImageUrl="~/Images/save-serach_hover.gif"
                            onmouseout="this.src='Images/save-serach_hover.gif'" onmouseover="this.src='Images/save-serach.gif'"
                            ValidationGroup="VGAlert" OnClick="imgbtnSave_Click" />
                        &nbsp;&nbsp;
                        <asp:ImageButton ID="imgbtnCancle_1" runat="server" ImageUrl="~/Images/Cancel.gif"
                            onmouseout="this.src='Images/Cancel.gif'" onmouseover="this.src='Images/Cancel_hvr.gif'"
                            ValidationGroup="VGAlert" CausesValidation="false" OnClick="imgbtnCancle_1_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <%--  </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
