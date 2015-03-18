<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCDealers_ClientList.ascx.cs"
    Inherits="User_Controls_UCDealers_ClientList" %>
<style type="text/css">
    .showPadding
    {
        padding-left: 2px;
    }
    .popUp
    {
        border: 1px solid #000000;
        height: 50px;
        left: 403.5px;
        padding-top: 33px;
        position: fixed;
        text-align: center;
        width: 200px;
    }
</style>

<script type="text/javascript">
    function chkRequired() {
        //  alert("12");
        //        var gridv = document.getElementById("ctl00_ContentPlaceHolder1_UCDealersClientList_grdClientList_ctl02_grdDetails");
        //        alert(gridv);
        //        var gridlength = gridv.rows.length;
        //        alert(gridlength)
        //        var grow = gridv.rows[gridvlenth - 1];
        //        alert(grow)
        //        var txtETA = grow.cells(4).firstchild;
        //        alert(txtETA)
        //        return false;
    } 
</script>

<table width="100%" border="0">
    <tr>
        <td>
            <table width="100%">
                <tr>
                    <td style="width: 100%">
                        <asp:Panel runat="server" ID="pnlCustomerList">
                            <table width="100%">
                                <tr>
                                    <td align="right" style="padding-left: 10px">
                                        <asp:ImageButton runat="server" ID="imgBtnBacktoMainRpt" ImageUrl="~/Images/back.gif"
                                            OnClick="imgBtnBacktoMainRpt_Click" onmouseout="this.src='Images/back.gif'" onmouseover="this.src='Images/back_hvr.gif'"
                                            Visible="false" ToolTip="Back" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="lblMessage" runat="server" Font-Bold="true" ForeColor="Green"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="padding-right: 10px; width: 100%">
                                        <table width="100%">
                                            <tr>
                                                <td valign="top" align="left">
                                                    <div class="ClientRequest_Dealer" style="height: 10px; width: 10px">
                                                    </div>
                                                </td>
                                                <td valign="top" align="left">
                                                    Client Specifically Requested Additional Update
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="left">
                                                    <div class="nearDanger" style="height: 10px; width: 10px">
                                                    </div>
                                                </td>
                                                <td valign="top" align="left">
                                                    Update Required
                                                </td>
                                                <td valign="top" align="left">
                                                    <div class="Danger" style="height: 10px; width: 10px">
                                                    </div>
                                                </td>
                                                <td valign="top" align="left">
                                                    Update Overdue
                                                </td>
                                                <td valign="top" align="right">
                                                    <asp:Label ID="lblRowsToDisplay2" runat="server">Rows To Display:</asp:Label>
                                                    <asp:DropDownList ID="ddl_NoRecords2" runat="server" AutoPostBack="true" Width="50px"
                                                        OnSelectedIndexChanged="ddl_NoRecords2_SelectedIndexChanged">
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
                                    <td>
                                        <asp:GridView runat="server" ID="grdCustomerListOnly" AutoGenerateColumns="false"
                                            AllowSorting="true" AllowPaging="true" Width="100%" RowStyle-Height="30px" OnRowDataBound="grdCustomerListOnly_RowDataBound"
                                            OnSorting="grdCustomerListOnly_Sorting" OnPageIndexChanging="grdCustomerListOnly_PageIndexChanging"
                                            EmptyDataText="No records Found.">
                                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle CssClass="pgr" HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle BackColor="#0A73A2" Font-Bold="True" CssClass="gvHeader" Height="30px" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Last Updated" SortExpression="date">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblupdatedon" Text='<%# bind("date","{0: dd-MMM-yyyy}") %>'></asp:Label>
                                                        <asp:HiddenField runat="server" ID="hiddenDealerStatus" Value='<%# bind("DealerStatus") %>' />
                                                        <asp:HiddenField runat="server" ID="HiddenCustomerRequest" Value='<%# bind("customerRequest") %>' />
                                                        <asp:HiddenField runat="server" ID="hdfDateDiff" Value='<%# bind("diff") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delivery Date" SortExpression="eta">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lbleta" Text='<%# bind("eta","{0: dd-MMM-yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Customer Name" SortExpression="fullname">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblCustomerName" Text='<%#bind("fullname") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Email" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblcustomerEmail" Text='<%#bind("email") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Make" SortExpression="Make">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblMake" Text='<%#bind("Make") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Model" SortExpression="model">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblmodel" Text='<%#bind("model") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:HyperLink runat="server" ID="hypcustomerlinkview" Text="Update New Date" CssClass="Active"
                                                            NavigateUrl='<%# bind("id") %>'></asp:HyperLink>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%">
                        <asp:Panel runat="server" ID="pnlCustomerDetail" Visible="false">
                            <table width="100%">
                                <tr>
                                    <td align="right" style="padding-left: 10px">
                                        <asp:ImageButton runat="server" ID="imgbtnBack" ImageUrl="~/Images/back.gif" OnClick="imgbtnBack_Click"
                                            onmouseout="this.src='Images/back.gif'" onmouseover="this.src='Images/back_hvr.gif'" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="font-size: small; font-weight: bold">
                                        <i>Please update our mutual customer below on at least a weekly basis adding as much
                                            information into the notes as possible.. If there is no change, acknowledge this
                                            by simply pressing the ‘Update No Change’ button below.</i>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div id="divClientListOnly" runat="server">
                                            <asp:GridView runat="server" ID="grdClientList" AutoGenerateColumns="false" Width="100%"
                                                OnRowDataBound="grdClientList_RowDataBound" EmptyDataText="No Records Found"
                                                ShowHeader="false" AllowPaging="true" PageSize="2" OnPageIndexChanging="grdClientList_PageIndexChanging"
                                                RowStyle-Height="100px">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <div>
                                                                            <table width="100%" cellpadding="3px" style="background-color: #d5ecfd; color: Black;">
                                                                                <tr>
                                                                                    <td align="left" style="width: 15%">
                                                                                        <asp:Label runat="server" ID="lblCustomerName" Text="Customer Name" Font-Bold="true"></asp:Label>
                                                                                        <asp:Label runat="server" ID="lblCustomerID" Visible="false" Text='<%# bind("ID") %>'></asp:Label>
                                                                                    </td>
                                                                                    <td align="left" style="padding-left: 5px" colspan="3">
                                                                                        <asp:Label runat="server" ID="lblCustomerNameValue" Text='<%# bind("fullname")%>'></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="left">
                                                                                        <asp:Label runat="server" ID="lblEmail" Text="Email-ID" Font-Bold="true"></asp:Label>
                                                                                    </td>
                                                                                    <td align="left" style="padding-left: 5px" colspan="3">
                                                                                        <asp:Label runat="server" ID="lblEmailValue" Text='<%# bind("Email") %>'></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="left">
                                                                                        <asp:Label runat="server" ID="lblphone" Text="Phone" Font-Bold="true"></asp:Label>
                                                                                    </td>
                                                                                    <td align="left" style="padding-left: 5px; width: 25%">
                                                                                        <asp:Label runat="server" ID="lblphoneValue" Text='<%# bind("Phone") %>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="width: 10%" align="left">
                                                                                        <asp:Label runat="server" ID="Label2" Text="Mobile" Font-Bold="true"></asp:Label>
                                                                                    </td>
                                                                                    <td align="left" style="padding-left: 5px">
                                                                                        <asp:Label runat="server" ID="lblmobile" Text='<%# bind("mobile") %>'></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="left">
                                                                                        <asp:Label runat="server" ID="lblmake" Text="Make" Font-Bold="true"></asp:Label>
                                                                                    </td>
                                                                                    <td align="left" style="padding-left: 5px">
                                                                                        <asp:Label runat="server" ID="lblmakevalue" Text='<%# bind("Make") %>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="width: 10%">
                                                                                        <asp:Label runat="server" ID="lblModel" Text="Model" Font-Bold="true"></asp:Label>
                                                                                    </td>
                                                                                    <td align="left" style="padding-left: 5px">
                                                                                        <asp:Label runat="server" ID="Label1" Text='<%# bind("model") %>'></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <div>
                                                                            <table width="100%" cellpadding="3px" style="background-color: #d5ecfd; color: Black;">
                                                                                <tr>
                                                                                    <td align="left" style="width: 15%">
                                                                                        <asp:Label runat="server" ID="lblBuildYear" Text="Build Year" Font-Bold="true"></asp:Label>
                                                                                    </td>
                                                                                    <td align="left" style="padding-left: 5px; width: 25%">
                                                                                        <asp:Label Visible="false" runat="server" ID="lblBuildYear1" Text='<%# bind("BuildYear") %>'></asp:Label>
                                                                                        <asp:TextBox ID="txtBuildYear" Font-Bold="true" Enabled="false" runat="server" Width="120"
                                                                                            Text='<%# bind("BuildYear") %>'></asp:TextBox>
                                                                                    </td>
                                                                                    <td style="width: 20%" align="left">
                                                                                        <asp:Label runat="server" ID="lblComplianceYear" Text="Compliance Year" Font-Bold="true"></asp:Label>
                                                                                    </td>
                                                                                    <td align="left" style="padding-left: 5px">
                                                                                        <asp:Label runat="server" Visible="false" ID="lblComplianceYear1" Text='<%# bind("ComplianceYear") %>'></asp:Label>
                                                                                        <asp:TextBox ID="txtComplianceYear" Font-Bold="true" Enabled="false" runat="server"
                                                                                            Width="120" Text='<%# bind("ComplianceYear") %>'></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="left">
                                                                                        <asp:Label runat="server" ID="lblStockNo" Text="Stock Number" Font-Bold="true"></asp:Label>
                                                                                    </td>
                                                                                    <td align="left" style="padding-left: 5px">
                                                                                        <asp:Label runat="server" Visible="false" ID="lblStockNo1" Text='<%# bind("StockNo") %>'></asp:Label>
                                                                                        <asp:TextBox ID="txtStockNo" Enabled="false" Font-Bold="true" runat="server" Width="120"
                                                                                            Text='<%# bind("StockNo") %>'></asp:TextBox>
                                                                                    </td>
                                                                                    <td colspan="2" style="width: 10%" align="left">
                                                                                        <asp:ImageButton ID="btnEditDealerIp" runat="server" ImageUrl="~/Images/edit.gif"
                                                                                            onmouseout="this.src='../Images/edit.gif'" onmouseover="this.src='Images/edit_hover.gif'"
                                                                                            CausesValidation="false" CommandName="editDealerIp" OnClick="btnEditDealerIp_Click" />
                                                                                        <asp:ImageButton ID="btnSaveDealerIp" runat="server" ImageUrl="~/Images/Submit.gif"
                                                                                            onmouseout="this.src='../Images/Submit.gif'" onmouseover="this.src='Images/Submit_hvr.gif'"
                                                                                            CausesValidation="false" Visible="false" CommandName="saveDealerIp" OnClick="btnSaveDealerIp_Click" />
                                                                                        <asp:ImageButton ID="btnCancelDealerIp" runat="server" ImageUrl="~/Images/Cancel.gif"
                                                                                            onmouseout="this.src='../Images/Cancel.gif'" onmouseover="this.src='Images/Cancel_hvr.gif'"
                                                                                            CausesValidation="false" Visible="false" CommandName="cancelDealerIp" OnClick="btnCancelDealerIp_Click" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 100%" valign="top">
                                                                        <asp:GridView runat="server" ID="grdDetails" ShowFooter="true" AutoGenerateColumns="false"
                                                                            Width="730px" OnRowDataBound="grdDetails_RowDataBound " RowStyle-Height="30px">
                                                                            <Columns>
                                                                                <asp:TemplateField FooterStyle-VerticalAlign="top" HeaderStyle-VerticalAlign="Middle"
                                                                                    HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                                                                                    <HeaderTemplate>
                                                                                        Date of Update
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label runat="server" ID="lbldatofUpdate" Text='<%# bind("Date","{0:dd MMM yyyy}") %>'
                                                                                            htmlencode="false"></asp:Label>
                                                                                        <asp:Label runat="server" ID="lblCustomerID" Text='<%# bind("customerid") %>' Visible="false"></asp:Label>
                                                                                        <asp:HiddenField runat="server" ID="hiddenDealerid" Value='<%# bind("Dealerid") %>' />
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <asp:Label ID="txtDateOfUpdate" runat="server"></asp:Label>
                                                                                        <asp:Label runat="server" ID="lblCustomerID" Text='<%# bind("customerid") %>' Visible="false"></asp:Label>
                                                                                    </FooterTemplate>
                                                                                    <ItemStyle CssClass="showPadding" Width="120" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField FooterStyle-VerticalAlign="Top" HeaderStyle-VerticalAlign="Middle"
                                                                                    HeaderStyle-HorizontalAlign="Center">
                                                                                    <HeaderTemplate>
                                                                                        Status
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label runat="server" ID="lblStatus" Text='<%# bind("Status") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <asp:DropDownList runat="server" Width="150px" ID="drpStatus">
                                                                                        </asp:DropDownList>
                                                                                        <asp:RequiredFieldValidator runat="server" ID="requiredStatus" ControlToValidate="drpStatus"
                                                                                            InitialValue="0" ErrorMessage="Select Status." ValidationGroup="Dealer" Display="None"></asp:RequiredFieldValidator>
                                                                                        <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="statuscallout" TargetControlID="requiredStatus">
                                                                                        </ajaxToolkit:ValidatorCalloutExtender>
                                                                                    </FooterTemplate>
                                                                                    <ItemStyle CssClass="showPadding" Width="150" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField FooterStyle-VerticalAlign="Top" HeaderStyle-VerticalAlign="Middle"
                                                                                    HeaderStyle-HorizontalAlign="Center">
                                                                                    <HeaderTemplate>
                                                                                        <span style="color: Red">*</span> ETA
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label runat="server" ID="lblETA" Text='<%# bind("ETA","{0:dd MMM yyyy}") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <asp:TextBox runat="server" ID="txtETA" MaxLength="10" Text="" OnTextChanged="txtETA_TextChanged"
                                                                                            AutoPostBack="true" Width="100"></asp:TextBox>
                                                                                        <asp:HiddenField runat="server" ID="hidETAOrg" />
                                                                                        <ajaxToolkit:CalendarExtender runat="server" ID="EtaCalender" TargetControlID="txtETA"
                                                                                            Format="dd/MM/yyyy">
                                                                                        </ajaxToolkit:CalendarExtender>
                                                                                        <asp:RegularExpressionValidator runat="server" ID="ss" Display="None" ControlToValidate="txtETA"
                                                                                            ErrorMessage="Enter proper date in dd/mm/yyyy format." ValidationGroup="Dealer"
                                                                                            SetFocusOnError="False" ValidationExpression='^(((0?[1-9]|[12]\d|3[01])\/(0?[13578]|1[02])\/((1[6-9]|[2-9]\d)\d{2}))|((0?[1-9]|[12]\d|30)\/(0?[13456789]|1[012])\/((1[6-9]|[2-9]\d)\d{2}))|((0?[1-9]|1\d|2[0-8])\/0?2\/((1[6-9]|[2-9]\d)\d{2}))|(29\/0?2\/((1[6-9]|[2-9]\d)(0?[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$'></asp:RegularExpressionValidator>
                                                                                        <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="ETACallout" TargetControlID="ss">
                                                                                        </ajaxToolkit:ValidatorCalloutExtender>
                                                                                        <asp:RequiredFieldValidator ID="RFV_ETADateNew" runat="server" ValidationGroup="Dealer"
                                                                                            Display="None" ErrorMessage="Please Enter ETA" ControlToValidate="txtETA"></asp:RequiredFieldValidator>
                                                                                        <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="VCE_ETADateNew" TargetControlID="RFV_ETADateNew"
                                                                                            PopupPosition="Right">
                                                                                        </ajaxToolkit:ValidatorCalloutExtender>
                                                                                    </FooterTemplate>
                                                                                    <ItemStyle CssClass="showPadding" Width="100" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField FooterStyle-VerticalAlign="Top" HeaderStyle-VerticalAlign="Middle"
                                                                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" ItemStyle-CssClass="showPadding"
                                                                                    ItemStyle-Width="360px">
                                                                                    <HeaderTemplate>
                                                                                        Notes to be sent to client
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label runat="server" ID="lblNotes" Text='<%# bind("DealerNotes") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <asp:TextBox runat="server" ID="txtNotes" Width="300px" Height="80px" TextMode="MultiLine"></asp:TextBox>
                                                                                        <asp:RequiredFieldValidator runat="server" ID="requiredNotes" ControlToValidate="txtNotes"
                                                                                            ErrorMessage="Enter Notes." Enabled="false" Display="None" SetFocusOnError="true"
                                                                                            ValidationGroup="Dealer"></asp:RequiredFieldValidator>
                                                                                        <ajaxToolkit:ValidatorCalloutExtender runat="server" PopupPosition="TopRight" ID="Notescallout"
                                                                                            TargetControlID="requiredNotes">
                                                                                        </ajaxToolkit:ValidatorCalloutExtender>
                                                                                    </FooterTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField FooterStyle-VerticalAlign="Top" FooterStyle-HorizontalAlign="Right"
                                                                                    ItemStyle-Width="100px">
                                                                                    <FooterTemplate>
                                                                                        <table>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Button runat="server" ID="btnNoChangeETA" Text="Same ETA" OnClick="btnNoChangeETA_Click"
                                                                                                        CommandArgument='<%# bind("customerid") %>' Visible="false" />
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:ImageButton ID="btnSubmit" runat="server" ImageUrl="~/Images/sendToClient_d.png"
                                                                                                        ValidationGroup="Dealer" OnClick="btnSubmit_Click" onmouseout="this.src='../Images/sendToClient_d.png'"
                                                                                                        onmouseover="this.src='Images/sendToClient_u.png'" />
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </FooterTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <FooterStyle CssClass="gvFooterrow" />
                                                                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                                            <PagerStyle CssClass="pgr" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                            <HeaderStyle BackColor="#0A73A2" Font-Size="15px" Font-Bold="true" CssClass="gvHeader"
                                                                                Height="30px" />
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" style="width: 10%; padding-left: 10px; font-weight: bold; font-size: x-small;
                                                                        color: Red" colspan="4">
                                                                        ETA= *ETA represents the estimated day of delivery to the client. Please be conservative
                                                                        and allow for additional freight, registration requirements, fitting of accessories
                                                                        etc as required
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" style="width: 10%; padding-left: 10px; font-weight: bold; font-size: x-small;
                                                                        color: Red" colspan="4">
                                                                        *“Please be aware that upon pressing Submit, all information including notes will
                                                                        be sent directly to the client.”
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle CssClass="gvFooterrow" />
                                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle CssClass="pgr" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle BackColor="#0A73A2" Font-Bold="True" CssClass="gvHeader" Height="30px" />
                                            </asp:GridView>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:HiddenField ID="hdnClientUpdated" runat="server" />
                                        <%-- <ajaxToolkit:ModalPopupExtender ID="mdlPopClientUpdated" runat="server" PopupControlID="pnlpopUp"
                                            Enabled="true" BackgroundCssClass="ModalCSS" TargetControlID="hdnClientUpdated">
                                        </ajaxToolkit:ModalPopupExtender>
                                        <asp:Panel ID="pnlpopUp" runat="server" BackColor="White" CssClass="popUp">
                                            Client Updated.</asp:Panel>--%>
                                        <asp:HiddenField ID="hdnIsClientUpdated" runat="server" Value="" />
                                    </td>
                                </tr>
                            </table>
                            <table width="100%">
                                <tr>
                                    <td align="right">
                                        <asp:ImageButton ID="imgbtnSameETA" runat="server" ImageUrl="~/Images/sameetc_hover.gif"
                                            OnClick="btnNoChangeETA_Click" onmouseout="this.src='Images/sameetc_hover.gif'"
                                            onmouseover="this.src='Images/sameetc.gif'" CausesValidation="false" />
                                    </td>
                                    <td align="left">
                                        <asp:ImageButton ID="imgBtnCancelOrder" runat="server" ImageUrl="~/Images/CancelOrder_hover.gif"
                                            ToolTip="Cancel Order" OnClick="imgBtnCancelOrder_Click" onmouseout="this.src='Images/CancelOrder_hover.gif'"
                                            onmouseover="this.src='Images/CancelOrder.gif'" CausesValidation="false" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <p>
                            This system is currently being used on a trial basis. If you need any help or have
                            any feedback, queries or bug reports please let David Lye know on bug reports please
                            let David Lye know on <a href="mailto:davidlye@privatefleet.com.au">davidlye@privatefleet.com.au</a>
                            or (02) 9411 6777 ext 236. Thanks for your patience!</p>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <asp:HiddenField ID="hdnDealerEmail" runat="server" />
            <asp:HiddenField ID="hdnDealerId" runat="server" />
        </td>
    </tr>
</table>
<div id="msgpop" runat="server" style="display: none; width: 300px;">
    <div id="progressBackgroundFilter1">
    </div>
    <div id="processMessage1" style="width: 300px; height: auto; padding: 5px !important;">
        <asp:Panel runat="server" ID="pnlmodal" BackColor="White">
            <table width="300px" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="background-color: #0A73A2; color: White; font-weight: bold; padding-left: 5px;
                        height: 30px; font-size: large">
                        Private Fleet
                    </td>
                </tr>
                <tr>
                    <td style="height: 5px">
                    </td>
                </tr>
                <tr>
                    <td style="padding-left: 5px" align="center">
                        <p>
                            <asp:Label runat="server" ID="lblMessageForModal" Text="Client Updated."></asp:Label>
                        </p>
                    </td>
                </tr>
                <tr>
                    <td style="height: 5px">
                    </td>
                </tr>
                <%--  <tr>
                        <td align="center">
                            <asp:Button runat="server" ID="btnClose" Text="Ok" OnClick="btnClose_Click" />
                        </td>
                    </tr>--%>
                <tr>
                    <td style="height: 5px">
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</div>
<div id="divOrderCancelConfirm" runat="server" style="display: none; width: 300px;">
    <div id="progressBackgroundFilterOrderConfrm">
    </div>
    <div id="processMessageOrderConfirm" style="width: 300px; height: auto; padding: 5px !important;
        left: 35%;">
        <asp:Panel runat="server" ID="Panel1" BackColor="White">
            <table width="300px" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="background-color: #0A73A2; color: White; font-weight: bold; padding-left: 5px;
                        height: 30px; font-size: large">
                        Private Fleet
                    </td>
                </tr>
                <tr>
                    <td style="height: 5px">
                    </td>
                </tr>
                <tr>
                    <td style="padding-left: 5px" align="center">
                        <p>
                            <asp:Label runat="server" ID="lblOrderCancelConfirmation" Text="Are you sure you want to cancel this order – ie the deal is a fallover?  If you just want to clear it from the system, please instead choose 'Delivered' from the dropdown menu."></asp:Label>
                        </p>
                    </td>
                </tr>
                <tr>
                    <td style="height: 5px">
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button runat="server" ID="btnOk" Text="Yes" OnClick="btnYes_Click" />
                        <asp:Button runat="server" ID="btnNo" Text="No" OnClick="btnNo_Click" />
                        <asp:Button runat="server" ID="btnDrasticMsg" Text="Ok" OnClick="btnDrasticMsg_Click"
                            Visible="false" />
                    </td>
                </tr>
                <tr>
                    <td style="height: 5px">
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</div>
