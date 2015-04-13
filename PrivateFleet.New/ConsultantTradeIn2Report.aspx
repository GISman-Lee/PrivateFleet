<%@ Page Title="Trade In Report" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeFile="ConsultantTradeIn2Report.aspx.cs" Inherits="ConsultantTradeIn2Report" %>

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
        }   
    </script>

    <asp:Panel ID="pnlTradeIn" runat="server" DefaultButton="btnGenerateReport">
        <table width="100%" align="center" cellpadding="1" cellspacing="3" border="0">
            <tr>
                <td align="left" colspan="4">
                    <strong>Search Criteria:</strong>
                </td>
            </tr>
            <tr>
                <td>
                    Used Car
                </td>
                <td>
                    <asp:DropDownList ID="ddlMake" runat="server" Height="21px" Width="170px" AppendDataBoundItems="True"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlMake_SelectedIndexChanged">
                    </asp:DropDownList>
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
                    State
                </td>
                <td>
                    <asp:DropDownList ID="ddlState" runat="server" Height="21px" Width="170px" AppendDataBoundItems="True">
                    </asp:DropDownList>
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
                <td id="td_trade" runat="server" visible="false">
                    Trade Status
                </td>
                <td>
                    <asp:DropDownList ID="ddlstatus" runat="server" Height="21px" Width="170px" Visible="false">
                    <asp:ListItem Value="0" Text="-Select Status-"></asp:ListItem>
                        <asp:ListItem Value="Bespoke" Text="Bespoke"></asp:ListItem>
                        <asp:ListItem Value="Chatswood" Text="Chatswood"></asp:ListItem>
                        <asp:ListItem Value="Jeff Bell and Co" Text="Jeff Bell and Co"></asp:ListItem>
                        <asp:ListItem Value="John Hughes NSW" Text="John Hughes NSW"></asp:ListItem>
                        <asp:ListItem Value="John Hughes WA" Text="John Hughes WA"></asp:ListItem>
                        <asp:ListItem Value="Minto" Text="Minto"></asp:ListItem>
                        <asp:ListItem Value="Nationwide" Text="Nationwide"></asp:ListItem>
                        <asp:ListItem Value="Pickles" Text="Pickles"></asp:ListItem>
                        <asp:ListItem Value="Stuart - Home" Text="Stuart - Home"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="4" align="center" valign="bottom" style="padding-top: 10px;">
                    <asp:ImageButton ID="btnGenerateReport" runat="server" ImageUrl="~/Images/search_now_hover.gif"
                        onmouseout="this.src='Images/search_now_hover.gif'" onmouseover="this.src='Images/search_now.gif'"
                        ValidationGroup="TradeIn" OnClick="btnGenerateReport_Click" CausesValidation="false" />
                    <asp:ImageButton ID="btnCancel" runat="server" ImageUrl="~/Images/Cancel.gif" onmouseout="this.src='Images/Cancel.gif'"
                        onmouseover="this.src='Images/Cancel_hvr.gif'" ValidationGroup="TradeIn" CausesValidation="false"
                        OnClick="btnCancel_Click" />
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
                    <asp:GridView ID="gvTradeIn2Report" runat="server" AllowPaging="true" AllowSorting="true"
                        PageSize="15" DataKeyNames="ID" AutoGenerateColumns="false" Width="100%" BackColor="White"
                        ShowFooter="false" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                        CellPadding="3" EmptyDataText="No Records Found" OnSorting="gvTradeIn2Report_Sorting"
                        OnPageIndexChanging="gvTradeIn2Report_PageIndexChanging" OnRowCommand="gvTradeIn2Report_RowCommand"
                        OnRowDataBound="gvTradeIn2Report_RowDataBound">
                        <Columns>
                            <asp:BoundField HeaderText="State" DataField="HomeState" SortExpression="HomeState"
                                ItemStyle-Width="30px" />
                            <asp:BoundField HeaderText="Consultant" DataField="Consultant" SortExpression="Consultant"
                                ItemStyle-Width="30px" />
                            <asp:BoundField DataField="Make" HeaderText="Make" SortExpression="Make" />
                            <asp:BoundField DataField="T1Model" HeaderText="Model" SortExpression="T1Model" />
                            <asp:BoundField DataField="T1Transmission" HeaderText="Trans" SortExpression="T1Transmission" />
                            <asp:BoundField DataField="T1Year" HeaderText="Year" SortExpression="T1Year" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="T1OrigValue" HeaderText="Orig Trade in Value" SortExpression="T1OrigValue" />
                            <asp:BoundField DataField="LastName" HeaderText="Surname" SortExpression="LastName" />
                            <asp:BoundField DataField="DeliveryDate" HeaderText="Delivery Date" SortExpression="DeliveryDateSort"
                                ItemStyle-Width="80" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="TradeStatus" HeaderText="Trade Status" SortExpression="TradeStatus" />
                            <%--<asp:BoundField DataField="TradeInType" HeaderText="Trade In 1/2" SortExpression="TradeInType" />--%>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDetails" runat="server" CommandName="ViewDetails" Text="View Details"
                                        CssClass="activeLink"></asp:LinkButton>
                                    <asp:HiddenField ID="hdfKey" runat="server" Value='<%# Bind("Key") %>' />
                                    <asp:HiddenField ID="hdfIsPhoto" runat="server" Value='<%# Bind("IsPhoto") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkReport" runat="server" CommandName="ReportProblem" Text="Report Problem"
                                        CssClass="activeLink"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
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
    <div id="divpopID" runat="server" visible="false">
        <div id="progressBackgroundFilter">
        </div>
        <div id="processMessage1">
            <table width="100%" cellpadding="2" cellspacing="2">
                <tr id="trTitle" runat="server">
                    <td style="background-color: #17608C; padding: 5px;" colspan="2">
                        <span style="text-align: center;"><b>
                            <asp:Label ID="lblTitle" runat="server" ForeColor="White" Text="Report Problem"></asp:Label></b></span>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:TextBox ID="txtDesc" runat="server" TextMode="MultiLine" MaxLength="5000" Width="490px"
                            Height="170px"></asp:TextBox>
                        <asp:HiddenField ID="hdfKey1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                    </td>
                    <td align="left">
                        <asp:ImageButton ID="btnSend" runat="server" Text="Send" OnClick="btnSend_Click"
                            ImageUrl="~/Images/send_mail_hvr.gif" onmouseout="this.src='Images/send_mail_hvr.gif'"
                            onmouseover="this.src='Images/send_mail.gif'" />
                        <asp:ImageButton ID="btnCancelPopup" runat="server" Text="Cancel" OnClick="btnCancelPopup_Click"
                            ImageUrl="~/Images/Cancel.gif" onmouseout="this.src='Images/Cancel.gif'" onmouseover="this.src='Images/Cancel_hvr.gif'" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
