<%@ Page Title="Trade In Alerts" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="TradeInAlerts.aspx.cs" Inherits="TradeInAlerts" %>

<%@ Register Src="~/User Controls/ucTradeInData.ascx" TagName="ucTradeInData" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function manageHeight() {
            var behavior = $find('ACE_ModelEx');
            if (!behavior._height) {
                var target = behavior.get_completionList();
                alert(target);
                behavior._height = target.offsetHeight - 2;
                target.style.height = '0px';
            }
        }
    </script>

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
    <asp:Panel ID="pnlAlert" runat="server">
        <br />
        <br />
        <table align="center" width="90%" cellpadding="2" cellspacing="3">
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
                    <asp:Label ID="lblmake" runat="server" Text="Make - "></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlMake" runat="server" Width="150px" AutoPostBack="true" OnSelectedIndexChanged="ddlMake_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RFV_make" runat="server" ControlToValidate="ddlMake"
                        ErrorMessage="Make Required" ValidationGroup="VGAlert" Display="None" InitialValue="0"
                        SetFocusOnError="True"> </asp:RequiredFieldValidator>
                    <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="VCE_make" TargetControlID="RFV_make"
                        HighlightCssClass="validatorCalloutHighlight" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblModel" runat="server" Text="Model - "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtModel" AutoComplete="off" Width="150px" runat="server"></asp:TextBox>
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
                <td style="width: 20px;">
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:ImageButton ID="btnSaveasAlert" runat="server" ImageUrl="~/Images/save_as_alert.png"
                        ImageAlign="AbsMiddle" onmouseout="this.src='Images/save_as_alert.png'" onmouseover="this.src='Images/save_as_alert_hvr.png'"
                        ValidationGroup="VGAlert" OnClick="btnSaveasAlert_Click" />
                    &nbsp;&nbsp;
                    <asp:ImageButton ID="btnRun" runat="server" ImageUrl="~/Images/runSaveSearch.png"
                        ImageAlign="AbsMiddle" onmouseout="this.src='Images/runSaveSearch.png'" onmouseover="this.src='Images/runSaveSearch_hvr.png'"
                        CausesValidation="false" OnClick="btnRun_Click" />
                    &nbsp;&nbsp;
                    <asp:ImageButton ID="btnCancel" runat="server" ImageUrl="~/Images/Cancel.gif" ImageAlign="AbsMiddle"
                        onmouseout="this.src='Images/Cancel.gif'" onmouseover="this.src='Images/Cancel_hvr.gif'"
                        ValidationGroup="VGAlert" OnClick="btnCancel_Click"  CausesValidation ="false" />
                </td>
            </tr>
        </table>
        <table align="center" width="100%" cellpadding="2" cellspacing="3">
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
                <td>
                    <asp:GridView ID="gvTradeInAlerts" runat="server" AutoGenerateColumns="false" ShowFooter="false"
                        EmptyDataText="No Records Found" DataKeyNames="ID" AllowPaging="true" AllowSorting="true"
                        PageSize="10" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" Width="100%"
                        BorderWidth="1px" OnSorting="gvTradeInAlerts_Sorting" OnPageIndexChanging="gvTradeInAlerts_PageIndexChanging"
                        OnRowCommand="gvTradeInAlerts_RowCommand" OnRowDataBound="gvTradeInAlerts_RowDataBound">
                        <Columns>
                            <asp:BoundField HeaderText="Name" DataField="CustName" SortExpression="CustName">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Contact" DataField="Contact" SortExpression="Contact">
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Make" DataField="Make" SortExpression="Make">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Model" DataField="Model" SortExpression="Model">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Alert Period" DataField="AlertP" SortExpression="AlertP">
                                <ItemStyle HorizontalAlign="Center" Width="65px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Notes" DataField="Notes" SortExpression="Notes">
                                <ItemStyle Width="180px" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Image ID="imgActivate" ImageUrl="~/Images/Active.png" runat="server" />
                                    <asp:HiddenField ID="hdfActive" runat="server" Value='<% #Bind("IsActive") %>' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="20px" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkActive" runat="server" CommandName="Activate" Text="Active"
                                        CssClass="activeLink"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle CssClass="gvFooterrow" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <PagerStyle CssClass="pgr" />
                        <HeaderStyle BackColor="#0A73A2" Font-Bold="True" CssClass="gvHeader" HorizontalAlign="Center"
                            Height="30px" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlAlert_1" runat="server" Visible="false">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td align="right" style="padding: 10px">
                    <asp:ImageButton ID="btnBack" runat="server" ImageUrl="~/Images/back.gif" onmouseout="this.src='Images/back.gif'"
                        onmouseover="this.src='Images/back_hvr.gif'" OnClick="btnBack_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <uc1:ucTradeInData ID="ucTradeInData1" runat="server" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
