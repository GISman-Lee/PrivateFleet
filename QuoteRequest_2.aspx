<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="QuoteRequest_2.aspx.cs" Inherits="QuoteRequest_2" %>

<%@ Register Src="~/User Controls/Request/ucDealerSelection.ascx" TagName="ucDealerSelection"
    TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        function isNumberKey(evt, chk) {
            //alert("Hi");
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                alert("Please enter number only");
                return false;
            }
            return true;
        }
    </script>

    <asp:UpdatePanel ID="UPQuoteRequest" runat="server">
        <ContentTemplate>
            <asp:Panel ID="QuoteRequest2" runat="server">
                <table width="95%" align="center" style="border: 1px solid #4A4A4A" cellpadding="1"
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
                            Postal Code:
                        </td>
                        <td style="height: 5px">
                            <asp:TextBox ID="txtPCode" runat="server" onblur="javascript: __doPostBack();" OnTextChanged="txtPCode_TextChanged">
                            </asp:TextBox>
                            <%--    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Display="None"
                                        ControlToValidate="txtPCode" ValidationGroup="VGSubmit" SetFocusOnError="True"
                                        ssClass="gvValidationError" ValidationExpression="^[0-9]*$" ErrorMessage="<p>Enter Numbers Only</p>">
                                    </asp:RegularExpressionValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender6"
                                        TargetControlID="RegularExpressionValidator2" HighlightCssClass="validatorCalloutHighlight" />--%>
                        </td>
                        <td style="height: 5px">
                            Suburb
                        </td>
                        <td style="height: 5px" valign="bottom">
                            <asp:DropDownList ID="ddlLocation" runat="server" DataTextField="Suburb" DataValueField="ID"
                                Width="249px">
                                <asp:ListItem Value="0">-Select Suburb-</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px">
                        </td>
                        <td style="height: 5px">
                            <%--  <asp:LinkButton ID="lbtGetLocations" runat="server" OnClick="lbtGetLocations_Click"
                                        ValidationGroup="VGRadius">Get Location's</asp:LinkButton>--%>
                        </td>
                        <td style="height: 5px" valign="bottom">
                        </td>
                        <td style="height: 5px" valign="bottom">
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" colspan="4" style="text-align: right; padding-right: 20px;">
                            <asp:ImageButton ID="btnSearchDealers" runat="server" OnClick="btnSearchDealers_Click"
                                ImageUrl="~/Images/Search_dealer.gif" onmouseout="this.src='Images/Search_dealer.gif'"
                                onmouseover="this.src='Images/Search_dealer_hvr.gif'" ValidationGroup="VGRadius" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            &nbsp;
                            <asp:RequiredFieldValidator ID="rfvPostalCodeOnSearch" runat="server" ControlToValidate="txtPCode"
                                CssClass="gvValidationError" Display="None" ErrorMessage="Postal Code  Required"
                                SetFocusOnError="True" ValidationGroup="VGRadius">
                            </asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
                                HighlightCssClass="validatorCalloutHighlight" TargetControlID="rfvPostalCodeOnSearch">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlLocation"
                                CssClass="gvValidationError" Display="None" ErrorMessage="Suburb Required" SetFocusOnError="True"
                                InitialValue="0" ValidationGroup="VGRadius">
                            </asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator1">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                        <td valign="top" align="center">
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <table width="95%" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td valign="top" colspan="4">
                            <uc1:ucDealerSelection ID="UcDealerSelection1" runat="server" Visible="True" />
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top" width="40%" style="height: 20px">
                            <asp:ImageButton ID="btnPrevious" runat="server" OnClick="btnPrevious_Click" ImageUrl="~/Images/back.gif"
                                onmouseout="this.src='Images/back.gif'" onmouseover="this.src='Images/back_hvr.gif'" />
                        </td>
                        <td align="left" valign="top" width="60%" style="height: 20px">
                            <asp:ImageButton ID="btnCreateRequest" runat="server" OnClick="btnCreateRequest_Click"
                                ImageUrl="~/Images/Create_request.gif" onmouseout="this.src='Images/Create_request.gif'"
                                onmouseover="this.src='Images/Create_request_hvr.gif'" ValidationGroup="Create" />
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px">
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
