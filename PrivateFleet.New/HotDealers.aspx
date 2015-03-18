<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="HotDealers.aspx.cs" Inherits="HotDealers" Title="Hot Dealers Selection" %>

<%@ Register Src="User Controls/ucHotDealerSelection.ascx" TagName="ucHotDealerSelection"
    TagPrefix="uc2" %>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UP1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlHotDealerSelection" runat="server" DefaultButton="btnSearchDealers">
                <table width="100%" align="center" style="border: 1px solid #4A4A4A" cellpadding="1"
                    cellspacing="2">
                    <tr>
                        <td valign="middle">
                            <asp:Label ID="lblMsg" runat="server" CssClass="dbresult"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="subheading">
                            <strong>Search Dealer</strong>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px">
                            Make:
                        </td>
                        <td colspan="2" style="height: 5px">
                            <asp:DropDownList ID="ddlMake" runat="server" Height="21px" Width="190px">
                            </asp:DropDownList>
                        </td>
                        <td style="height: 5px" valign="bottom">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px;">
                            Postal Code:
                        </td>
                        <td style="height: 5px">
                            <asp:TextBox ID="txtPCode" runat="server" Width="190px"></asp:TextBox>
                        </td>
                        <td align="left" style="padding-left: 15px;">
                            Suburb:
                        </td>
                        <td style="height: 5px" valign="bottom">
                            <asp:DropDownList ID="ddlLocation" runat="server" DataTextField="Suburb" DataValueField="ID"
                                Width="190px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px">
                        </td>
                        <td style="height: 5px">
                            <asp:LinkButton ID="lbtGetLocations" runat="server" OnClick="lbtGetLocations_Click"
                                ValidationGroup="VGGetLocation">Get Location's</asp:LinkButton>
                        </td>
                        <td style="height: 5px; width: 61px;" valign="bottom">
                        </td>
                        <td style="height: 5px" valign="bottom">
                        </td>
                    </tr>
                    <tr runat="server" visible="false" id="tr1">
                        <td valign="top">
                            State:
                            <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                            </asp:DropDownList>
                            &nbsp;
                        </td>
                        <td valign="top">
                            <asp:Label ID="lblCity" runat="server" Style="vertical-align: top;" Text="City :"></asp:Label>
                            <asp:ListBox ID="lstCity" runat="server" Style="vertical-align: top;" SelectionMode="Multiple">
                            </asp:ListBox>
                        </td>
                        <td valign="top" style="width: 61px">
                            <asp:LinkButton ID="lnkGetLocations" Text="Get Locations" runat="server" OnClick="lnkGetLocations_Click"
                                CssClass="activeLink" Visible="False">
                            </asp:LinkButton>
                        </td>
                        <td valign="top">
                            <asp:Label ID="lblLocation" runat="server" Style="vertical-align: top;" Text="Location :"
                                Visible="False"></asp:Label>
                            <asp:ListBox ID="lstLocation" runat="server" Style="vertical-align: top;" SelectionMode="Multiple"
                                Visible="False"></asp:ListBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%;">
                            Company:
                        </td>
                        <td style="width: 20%;">
                            <asp:TextBox ID="txtCompany" runat="server" Width="190px"></asp:TextBox>
                        </td>
                        <td align="left" style="width: 15%; padding-left: 15px;">
                            Contact:
                        </td>
                        <td valign="bottom">
                            <asp:TextBox ID="txtContact" runat="server" Width="190px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px;">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <%--  <asp:RequiredFieldValidator id="rfvMake" runat="server" ControlToValidate="ddlMake"
                    Display="None" ErrorMessage="Make Required" InitialValue="-Select-" SetFocusOnError="True"
                    ValidationGroup="VGSubmit"></asp:RequiredFieldValidator>
                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server"
                    HighlightCssClass="validatorCalloutHighlight" TargetControlID="rfvMake">
                </ajaxToolkit:ValidatorCalloutExtender>--%>
                            <%--  <asp:RequiredFieldValidator id="rfvLocationRequired" runat="server" ControlToValidate="ddlLocation"
                    CssClass="gvValidationError" Display="None" ErrorMessage="Suburb Required." InitialValue="-Select-"
                    SetFocusOnError="True" ValidationGroup="VGSubmit">
                </asp:RequiredFieldValidator><ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3"
                    runat="server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="rfvLocationRequired">
                </ajaxToolkit:ValidatorCalloutExtender>--%>
                            <asp:RegularExpressionValidator ID="revPostalCode" runat="server" ControlToValidate="txtPcode"
                                Display="None" ErrorMessage="Enter Correct Postal Code" ValidationExpression="^(0[289][0-9]{2})|([1345689][0-9]{3})|(2[0-8][0-9]{2})|(290[0-9])|(291[0-4])|(7[0-4][0-9]{2})|(7[8-9][0-9]{2})$"
                                ValidationGroup="VGGetLocation">
                            </asp:RegularExpressionValidator><ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4"
                                runat="server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="revPostalCode">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <%-- <asp:RequiredFieldValidator id="rfvPostalCodeOnSearch" runat="server" ControlToValidate="txtPCode"
                    CssClass="gvValidationError" Display="None" ErrorMessage="Postal Code  Required"
                    SetFocusOnError="True" ValidationGroup="VGSubmit">
                </asp:RequiredFieldValidator><ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2"
                    runat="server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="rfvPostalCodeOnSearch">
                </ajaxToolkit:ValidatorCalloutExtender>
                <asp:RequiredFieldValidator id="rfvPostalCodeForLocations" runat="server" ControlToValidate="txtPCode"
                    CssClass="gvValidationError" Display="None" ErrorMessage="Postal Code  Required"
                    SetFocusOnError="True" ValidationGroup="VGGetLocation">
                        </asp:RequiredFieldValidator><ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender6"
                    runat="server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="rfvPostalCodeForLocations">
                </ajaxToolkit:ValidatorCalloutExtender>--%>
                        </td>
                        <td valign="top" align="center">
                            <asp:ImageButton ID="btnSearchDealers" runat="server" OnClick="btnSearchDealers_Click"
                                ImageUrl="~/Images/Search_dealer.gif" onmouseout="this.src='Images/Search_dealer.gif'"
                                onmouseover="this.src='Images/Search_dealer_hvr.gif'" ValidationGroup="VGSubmit" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" valign="top">
                            <uc2:ucHotDealerSelection ID="UcHotDealerSelection1" runat="server" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
