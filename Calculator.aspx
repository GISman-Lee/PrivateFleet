<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Calculator.aspx.cs" Inherits="Calculator" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">
        function isNumberKey(evt, chk) {
            var charCode = (evt.which) ? evt.which : evt.keyCode
            //  alert(charCode);
            // 9- tab  46 del
            if (charCode == 9 || charCode == 8 || charCode == 27 || charCode == 46 || charCode == 37 || charCode == 39)   // 8 - backspace 27 - Escape
                return true;

            var txt = document.getElementById(chk.id);
            var val = txt.value.toString();
            //  alert(val)
            //  alert(val.indexOf("."));
            //            if (val.indexOf(".") >= 0 && charCode == 46) {
            //             //   alert("hi")
            //                return false;
            //            }

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

    <br />
    <table align="center" width="95%" cellpadding="2" cellspacing="2">
        <tr>
            <td style="width: 20%">
                <asp:Label ID="lblDeparture" runat="server" Text="Departure :"></asp:Label>
            </td>
            <td style="width: 25%; padding-left: 13px;">
                <asp:DropDownList Width="120" ID="ddlDeparture" runat="server" DataTextField="State"
                    DataValueField="ID">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RFVDeparture" runat="server" ControlToValidate="ddlDeparture"
                    CssClass="gvValidationError" Display="None" ErrorMessage="Required" ValidationGroup="VGSubmit"
                    Width="145px" SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                <ajaxToolkit:ValidatorCalloutExtender ID="VCEDeparture" runat="server" HighlightCssClass="validatorCalloutHighlight"
                    TargetControlID="RFVDeparture">
                </ajaxToolkit:ValidatorCalloutExtender>
            </td>
            <td style="width: 15%">
                <asp:Label ID="lblArrival" runat="server" Text="Arrival :"></asp:Label>
            </td>
            <td style="width: 40%">
                <asp:DropDownList ID="ddlArrival" AutoPostBack="true" Width="120" runat="server"
                    DataTextField="State" DataValueField="ID" OnSelectedIndexChanged="ddlArrival_SelectedIndexChanged">
                </asp:DropDownList>
                &nbsp;&nbsp;
                <asp:DropDownList ID="ddlCylinders" Width="120" runat="server" Visible="false">
                    <asp:ListItem Value="-1" Text="-Select Cylinder-"></asp:ListItem>
                    <asp:ListItem Value="0" Text="Hybrid"></asp:ListItem>
                    <asp:ListItem Value="4" Text="4cl"></asp:ListItem>
                    <asp:ListItem Value="6" Text="6cl"></asp:ListItem>
                    <asp:ListItem Value="8" Text="8cl"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RFVArrival" runat="server" ControlToValidate="ddlArrival"
                    CssClass="gvValidationError" Display="None" ErrorMessage="Required" ValidationGroup="VGSubmit"
                    Width="145px" SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                <ajaxToolkit:ValidatorCalloutExtender ID="VCEArrival" runat="server" HighlightCssClass="validatorCalloutHighlight"
                    TargetControlID="RFVArrival">
                </ajaxToolkit:ValidatorCalloutExtender>
                <asp:RequiredFieldValidator ID="RFVCylinder" runat="server" ControlToValidate="ddlCylinders"
                    CssClass="gvValidationError" Display="None" ErrorMessage="Required" ValidationGroup="VGSubmit"
                    Width="145px" SetFocusOnError="True" InitialValue="-1"></asp:RequiredFieldValidator>
                <ajaxToolkit:ValidatorCalloutExtender ID="VCECylinder" runat="server" HighlightCssClass="validatorCalloutHighlight"
                    TargetControlID="RFVCylinder" PopupPosition="Left">
                </ajaxToolkit:ValidatorCalloutExtender>
                <asp:DropDownList ID="ddlGreenStar" Width="130" runat="server" Visible="false">
                    <asp:ListItem Value="-1" Text="-Select Stars-"></asp:ListItem>
                    <asp:ListItem Value="0" Text="5 Star (A Rate)"></asp:ListItem>
                    <asp:ListItem Value="2" Text="4 to 4.5 Star (B Rate)"></asp:ListItem>
                    <asp:ListItem Value="3" Text="3 to 3.5 Star (C Rate)"></asp:ListItem>
                    <asp:ListItem Value="4" Text="1 to 1.5 Star (D Rate)"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfv_ddlGreenStar" runat="server" ControlToValidate="ddlGreenStar"
                    CssClass="gvValidationError" Display="None" ErrorMessage="Required" ValidationGroup="VGSubmit"
                    Width="145px" SetFocusOnError="True" InitialValue="-1"></asp:RequiredFieldValidator>
                <ajaxToolkit:ValidatorCalloutExtender ID="vce_ddlGreenStar" runat="server" HighlightCssClass="validatorCalloutHighlight"
                    TargetControlID="rfv_ddlGreenStar" PopupPosition="Left">
                </ajaxToolkit:ValidatorCalloutExtender>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblPP" runat="server" Text="Approximate Vehicle Purchase price :"></asp:Label>
            </td>
            <td>
                $&nbsp;<asp:TextBox ID="txtPP" Width="120" onkeypress="return isNumberKey(event,this);"
                    runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RFVPP" runat="server" ControlToValidate="txtPP" CssClass="gvValidationError"
                    Display="None" ErrorMessage="Required" ValidationGroup="VGSubmit" Width="145px"
                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                <ajaxToolkit:ValidatorCalloutExtender ID="VCEPP" runat="server" HighlightCssClass="validatorCalloutHighlight"
                    TargetControlID="RFVPP">
                </ajaxToolkit:ValidatorCalloutExtender>
            </td>
            <td>
                <asp:ImageButton ID="imgCalculate" runat="server" ValidationGroup="VGSubmit" ImageUrl="~/Images/Calculate.png"
                    onmouseout="this.src='Images/Calculate.png'" onmouseover="this.src='Images/Calculate_hover.png'"
                    OnClick="imgCalculate_Click" />
            </td>
            <td>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <asp:Panel ID="pnlCalculator" runat="server" Visible="false">
        <table align="center" width="95%" cellpadding="2" cellspacing="2" style="border: solid 1px #acacac;">
            <tr style="background-color: #0A73A2; border: solid 1px #acacac; height: 30px; color: White;
                font-weight: bold;">
                <td align="center" style="width: 50%;">
                    <asp:Label ID="Label1" runat="server" Text="Description"></asp:Label>
                </td>
                <td align="center" style="width: 50%;">
                    <asp:Label ID="Label2" runat="server" Text="Cost"></asp:Label>
                </td>
            </tr>
            <tr style="height: 25px;">
                <td style="padding-left: 15px;">
                    <asp:Label ID="lblfCharges" runat="server" Text="Freight Charges -"></asp:Label>
                </td>
                <td style="padding-left: 15px;">
                    <asp:Label ID="lblfCharges_ans" runat="server" Text="Freight Charges"></asp:Label>
                </td>
            </tr>
            <tr style="height: 25px; background-color: #C2DBE7;">
                <td style="padding-left: 15px;">
                    <asp:Label ID="lblHandlingFees" runat="server" Text="Handling Fees -"></asp:Label>
                </td>
                <td style="padding-left: 15px;">
                    <asp:Label ID="lblHandlingFees_ans" runat="server" Text="Handling Fees"></asp:Label>
                </td>
            </tr>
            <tr style="height: 25px;">
                <td style="padding-left: 15px;">
                    <asp:Label ID="lblRegoCTP" runat="server" Text="Rego/CTP -"></asp:Label>
                </td>
                <td style="padding-left: 15px;">
                    <asp:Label ID="lblRegoCTP_ans" runat="server" Text="Rego/CTP"></asp:Label>
                </td>
            </tr>
            <tr style="height: 25px; background-color: #C2DBE7;">
                <td style="padding-left: 15px;">
                    <asp:Label ID="lblStampDuty" runat="server" Text="Stamp Duty -"></asp:Label>
                </td>
                <td style="padding-left: 15px;">
                    <asp:Label ID="lblStampDuty_ans" runat="server" Text="Stamp Duty"></asp:Label>
                </td>
            </tr>
            <tr style="height: 25px; font-weight: bold;">
                <td style="padding-left: 15px;">
                    <asp:Label ID="lblTotal" runat="server" Text="Total -"></asp:Label>
                </td>
                <td style="padding-left: 15px;">
                    <asp:Label ID="lblTotal_ans" runat="server" Text="Total"></asp:Label>
                </td>
            </tr>
        </table>
        <table align="center" width="95%" border="0">
            <tr>
                <td align="center" colspan="2">
                    <asp:ImageButton ID="btnPrint" runat="server" ImageUrl="~/Images/print_hvr.gif" onmouseout="this.src='Images/print_hvr.gif'"
                        onmouseover="this.src='Images/print.gif'" OnClick="btnPrint_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
