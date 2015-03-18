<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="QuoteRequest_1.aspx.cs" Inherits="QuoteRequest_1" %>

<%@ Register Src="User Controls/Request/ucRequestParameters.ascx" TagName="ucRequestParameters"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        function maxLength(evt, txt) {
            var charCode = (evt.which) ? evt.which : event.keyCode

            if (charCode == 8 || charCode == 27)
                return true

            var txt = document.getElementById(txt.id);
            if (txt.value.length > 999) {
                alert("Maximum Limit reached");
                return false;
            }
        }
    </script>

    <asp:UpdatePanel ID="UPQuoteRequest" runat="server">
        <ContentTemplate>
            <asp:Panel ID="QuoteRequest1" runat="server">
                <table width="98%" align="center" border="0" cellpadding="0" cellspacing="1">
                    <tr>
                        <td valign="middle">
                            <asp:Label ID="lblMsgOp" runat="server" CssClass="dbresult"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%;">
                            <table width="100%" align="center" cellpadding="1" cellspacing="3" border="0">
                                <tr>
                                    <td>
                                        Make:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlMake" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlMake_SelectedIndexChanged"
                                            Height="21px" Width="120px">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvMake" runat="server" ControlToValidate="ddlMake"
                                            ErrorMessage="Make Required" ValidationGroup="VGMakeModelSeries" Display="None"
                                            InitialValue="0" SetFocusOnError="True">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        Model:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlModel" runat="server" Height="21px" Width="200px">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        Series:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSeries" runat="server" Width="200px" Height="19px">
                                        </asp:TextBox>
                                        <asp:DropDownList ID="ddlSeries" Visible="false" runat="server" AutoPostBack="false"
                                            Width="190px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%;">
                            <uc1:ucRequestParameters ID="UcRequestParameters1" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width="100%" cellpadding="1" cellspacing="1" border="0">
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkOrderTaken" runat="server" Text="<b style='color:red; text-decoration:blink'> Order Taken</b>">
                                        </asp:CheckBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkUrgent" runat="server" Text=" Some flexibility depending on delivery.Urgently required">
                                        </asp:CheckBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkBuid" runat="server" Text=" Must be 2011 Build & Complied">
                                        </asp:CheckBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width="100%" cellpadding="1" cellspacing="1" border="0">
                                <tr>
                                    <td>
                                        Consultant Notes:
                                        <asp:TextBox ID="TextBox1" runat="server" Rows="3" TextMode="MultiLine" Width="605px">
                                        </asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Display="None"
                                            ControlToValidate="TextBox1" ValidationGroup="VGMakeModelSeries" SetFocusOnError="True"
                                            ssClass="gvValidationError" ValidationExpression="^([\S\s]{0,1000})$" ErrorMessage="Maximum Limit reached">
                                        </asp:RegularExpressionValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender6"
                                            TargetControlID="RegularExpressionValidator2" HighlightCssClass="validatorCalloutHighlight" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px">
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server"
                                HighlightCssClass="validatorCalloutHighlight" TargetControlID="rfvMake">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            &nbsp;&nbsp;
                            <asp:ImageButton ID="btnNextStep" runat="server" Text="NEXT" ImageUrl="~/Images/Next.gif"
                                ImageAlign="AbsMiddle" onmouseout="this.src='Images/Next.gif'" onmouseover="this.src='Images/Next_hvr.gif'"
                                OnClick="btnNextStep_Click" ValidationGroup="VGMakeModelSeries" />
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
