<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucTradeInData.ascx.cs"
    Inherits="User_Controls_ucTradeInData" %>
<%@ Register Src="~/User Controls/ucTradeInPhoto.ascx" TagName="ucTradeInPhoto" TagPrefix="uc1" %>
<style type="text/css">
    .dlHeader
    {
        background-color: #C2DBE7 !important;
        color: Black;
        height: 20px;
        font-size: 12px;
        font-weight: bold;
    }
    .dlHeaderAlternet
    {
        background-color: #F4F8FA !important;
        color: Black;
        height: 20px;
        font-size: 12px;
        font-weight: bold;
    }
    .tdspc tr td
    {
        padding-left: 8px;
    }
    .brdrImg td img
    {
        border: 1px solid #999 !important;
    }
</style>
<asp:UpdatePanel ID="upnlTradeInDate" runat="server">
    <ContentTemplate>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td id="animation" align="center">
                    <!-- "Wire frame" div used to transition from the button to the info panel -->
                    <!-- Info panel to be displayed as a flyout when the button is clicked -->
                    <asp:Panel ID="dragMapPanel" runat="server">
                        <div id="info" style="display: none; width: 500px; z-index: 2; opacity: 0; font-size: 12px;
                            border: solid 1px #CCCCCC; background-color: #FFFFFF; padding: 5px;">
                            <div id="btnCloseParent" style="z-index: 2; padding-top: 10px; float: right; opacity: 1;">
                                <asp:LinkButton ID="btnClose" runat="server" Text="X" ToolTip="Close" Style="background-color: #666666;
                                    color: #FFFFFF; text-align: center; font-weight: bold; text-decoration: none;
                                    border: outset thin #FFFFFF; padding: 5px;" OnClientClick="javascript:divClose(); return false;" />
                            </div>
                            <div id="map1" style="padding-top: 10px; text-align: center; width: 450px; height: 450px;
                                background-color: #AAAAAA;">
                            </div>
                        </div>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td align="center" id="tdHeader1" runat="server" style="color: Red; font-size: 13px;
                    font-weight: bold" visible="true">
                    <table align="center" width="100%" cellpadding="2" cellspacing="2">
                        <tr>
                            <td style="width: 30%;">
                                <asp:Label ID="lblState_t" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="width: 20%; padding-left: 5px; vertical-align: middle">
                                <asp:Label ID="lblCar_t" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="width: 30%;">
                                <asp:Label ID="lblSurname_t" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%;">
                                <asp:Label ID="lblClientEmail_t" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="width: 30%; padding-left: 5px; vertical-align: middle">
                                <asp:Label ID="lblClientNo_t" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="width: 20%;">
                                <asp:Label ID="lblCunsultant_t" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DataList ID="DataList1" runat="server" GridLines="Both" Width="100%" RepeatDirection="Horizontal"
                        ShowFooter="False" ShowHeader="False" OnItemDataBound="DataList1_ItemDataBound">
                        <ItemTemplate>
                            <asp:Panel ID="pnl_1" runat="server">
                                <table width="100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td colspan="4" class="ucHeader" style="height: 25px;">
                                            &nbsp;&nbsp;Matched Alerts
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" align="center" style="height: 20px; font-size: 11px; font-weight: bold;
                                            background-color: #E7DEB4;">
                                            Alert Details
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table width="100%" cellpadding="2" cellspacing="2" class="tdspc">
                                                <tr class="dlHeader">
                                                    <td style="width: 18%; font-weight: normal;">
                                                        Name -
                                                    </td>
                                                    <td style="width: 32%;">
                                                        <asp:Label ID="lblCName" runat="server" Text='<%#Bind("CustName") %>'></asp:Label>
                                                    </td>
                                                    <td style="width: 18%; font-weight: normal;">
                                                        Contact -
                                                    </td>
                                                    <td style="width: 32%;">
                                                        <asp:Label ID="lblCContact" runat="server" Text='<%#Bind("Contact") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr class="dlHeaderAlternet">
                                                    <td style="width: 18%; font-weight: normal;">
                                                        Make -
                                                    </td>
                                                    <td style="width: 32%;">
                                                        <asp:Label ID="lblMake" runat="server" Text='<%#Bind("Make") %>'></asp:Label>
                                                    </td>
                                                    <td style="width: 18%; font-weight: normal;">
                                                        Model -
                                                    </td>
                                                    <td style="width: 32%;">
                                                        <asp:Label ID="lblModel" runat="server" Text='<%#Bind("Model") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table width="100%" cellpadding="2" cellspacing="2" class="tdspc">
                                                <tr class="dlHeader">
                                                    <td style="width: 18%; font-weight: normal;">
                                                        Alert Period -
                                                    </td>
                                                    <td style="width: 32%;">
                                                        <asp:Label ID="lblAPeriod" runat="server" Text='<%#Bind("AlertPeriod") %>'></asp:Label>
                                                    </td>
                                                    <td style="width: 18%; font-weight: normal;">
                                                        Alert Notes -
                                                    </td>
                                                    <td style="width: 32%;">
                                                        <asp:Label ID="lblANotes" runat="server" Text='<%#Bind("Notes") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" align="center" style="height: 20px; font-size: 11px; font-weight: bold;
                                            background-color: #E7DEB4;">
                                            Trade in Data
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <table width="100%" cellpadding="0" cellspacing="0" style="border-bottom: solid 1px black;">
                                <tr>
                                    <td>
                                        <table width="100%" cellpadding="2" cellspacing="2" class="tdspc">
                                            <tr class="dlHeader" style="font-size: 13px !important;">
                                                <td style="width: 18%; font-weight: normal;">
                                                    Input Date -
                                                </td>
                                                <td style="width: 32%;">
                                                    <asp:Label ID="Label20" runat="server" Text='<%#Bind("InputDate") %>'></asp:Label>
                                                </td>
                                                <td style="width: 18%; font-weight: normal;">
                                                    Rego Number -
                                                </td>
                                                <td style="width: 32%;">
                                                    <asp:Label ID="Label19" runat="server" Text='<%#Bind("T1RegoNumber") %>'></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <table width="100%" cellpadding="0" cellspacing="0" ">
                                <tr>
                                    <td>
                                        <table width="100%" cellpadding="2" cellspacing="2" class="tdspc">
                                            <tr class="dlHeaderAlternet" style="font-size: 13px !important">
                                                <td style="width: 18%; font-weight: normal;">
                                                    State -
                                                </td>
                                                <td style="width: 32%;">
                                                    <asp:Label ID="lblTState" runat="server" Text='<%#Bind("HomeState") %>'></asp:Label>
                                                </td>
                                                <td style="width: 18%; font-weight: normal;">
                                                    City -
                                                </td>
                                                <td style="width: 32%;">
                                                    <asp:HyperLink ID="lnkCityMap" NavigateUrl="javascript://" ToolTip="Click to see Google Map"
                                                        runat="server" Text='<%#Bind("HomeCity") %>'></asp:HyperLink>
                                                    <%--  <asp:Label ID="lblTCity" runat="server" Text='<%#Bind("HomeCity") %>'></asp:Label>--%>
                                                    <asp:HiddenField ID="hdfLati" runat="server" Value='<%#Bind("Longi") %>'></asp:HiddenField>
                                                    <asp:HiddenField ID="hdfLongi" runat="server" Value='<%#Bind("Lati") %>'></asp:HiddenField>
                                                </td>
                                            </tr>
                                            <tr class="dlHeader" style="font-size: 13px !important">
                                                <td style="width: 18%; font-weight: normal;">
                                                    Make -
                                                </td>
                                                <td style="width: 32%;">
                                                    <asp:Label ID="lblTMake" runat="server" Text='<%#Bind("T1Make") %>'></asp:Label>
                                                </td>
                                                <td style="width: 18%; font-weight: normal;">
                                                    Model -
                                                </td>
                                                <td style="width: 32%;">
                                                    <asp:Label ID="lblTModel" runat="server" Text='<%#Bind("T1Model") %>'></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table width="100%" cellpadding="2" cellspacing="2" class="tdspc">
                                            <tr class="dlHeaderAlternet" style="font-size: 13px !important">
                                                <td style="width: 18%; font-weight: normal;">
                                                    Series -
                                                </td>
                                                <td style="width: 32%;">
                                                    <asp:Label ID="lblTSeries" runat="server" Text='<%#Bind("T1Series") %>'></asp:Label>
                                                </td>
                                                <td style="width: 18%; font-weight: normal;">
                                                    Year -
                                                </td>
                                                <td style="width: 32%;">
                                                    <asp:Label ID="lblTYear" runat="server" Text='<%#Bind("T1Year") %>'></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="dlHeader" style="font-size: 13px !important">
                                                <td style="width: 18%; font-weight: normal;">
                                                    Body Shape -
                                                </td>
                                                <td style="width: 32%;">
                                                    <asp:Label ID="lblBShape" runat="server" Text='<%#Bind("T1BodyShap") %>'></asp:Label>
                                                </td>
                                                <td style="width: 18%; font-weight: normal;">
                                                    Body Color -
                                                </td>
                                                <td style="width: 32%;">
                                                    <asp:Label ID="lblBColor" runat="server" Text='<%#Bind("T1BodyColor") %>'></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table width="100%" cellpadding="2" cellspacing="2" class="tdspc">
                                            <tr class="dlHeaderAlternet" style="font-size: 13px !important">
                                                <td style="width: 18%; font-weight: normal;">
                                                    Fuel Type -
                                                </td>
                                                <td style="width: 32%;">
                                                    <asp:Label ID="lblFT" runat="server" Text='<%#Bind("T1FuelType") %>'></asp:Label>
                                                </td>
                                                <td style="width: 18%; font-weight: normal;">
                                                    Transmission -
                                                </td>
                                                <td style="width: 32%;">
                                                    <asp:Label ID="lblTeans" runat="server" Text='<%#Bind("T1Transmission") %>'></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="dlHeader" style="font-size: 13px !important">
                                                <td style="width: 18%; font-weight: normal;">
                                                    Trim Color -
                                                </td>
                                                <td style="width: 32%;">
                                                    <asp:Label ID="lblTrimColor" runat="server" Text='<%#Bind("T1TrimColor") %>'></asp:Label>
                                                </td>
                                                <td style="width: 18%; font-weight: normal;">
                                                    Odometer -
                                                </td>
                                                <td style="width: 32%;">
                                                    <asp:Label ID="lblOdometer" runat="server" Text='<%#Bind("T1Odometer") %>'></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table width="100%" cellpadding="2" cellspacing="2" class="tdspc">
                                            <tr class="dlHeaderAlternet" style="font-size: 13px !important">
                                                <td style="width: 18%; font-weight: normal;">
                                                    Rego -
                                                </td>
                                                <td style="width: 32%;">
                                                    <asp:Label ID="lblRego" runat="server" Text='<%#Bind("Rego") %>'></asp:Label>
                                                </td>
                                                <td style="width: 18%; font-weight: normal;">
                                                    Orig Trade in Value -
                                                </td>
                                                <td style="width: 32%;">
                                                    <asp:Label ID="lblValue" runat="server" Text='<%#Bind("T1OrigValue") %>'></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="dlHeader" style="font-size: 13px !important">
                                                <td style="width: 18%; font-weight: normal;">
                                                    Delivery Date -
                                                </td>
                                                <td style="width: 32%;">
                                                    <asp:Label ID="lblDDate" runat="server" Text='<%#Bind("DeliveryDate") %>'></asp:Label>
                                                </td>
                                                <td style="width: 18%; font-weight: normal;">
                                                    Trade Status -
                                                </td>
                                                <td style="width: 32%;">
                                                    <asp:Label ID="lblTraeStatus" runat="server" Text='<%#Bind("TradeStatus") %>'></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table width="100%" cellpadding="2" cellspacing="2" class="tdspc">
                                            <tr class="dlHeaderAlternet" style="font-size: 13px !important">
                                                <td style="width: 18%; font-weight: normal;">
                                                    Log Books -
                                                </td>
                                                <td style="width: 32%;">
                                                    <asp:Label ID="lblLogBooks" runat="server" Text='<%#Bind("LogBooks") %>'></asp:Label>
                                                </td>
                                                <td style="width: 18%; font-weight: normal;">
                                                    <asp:Label ID="lblComission_1" runat="server" Text="Commission -"></asp:Label>
                                                </td>
                                                <td style="width: 32%;">
                                                    <asp:Label ID="lblComission" runat="server" Text='<%#Bind("Comission") %>'></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table width="100%" cellpadding="2" cellspacing="2" class="tdspc">
                                            <tr class="dlHeader" style="font-size: 13px !important">
                                                <td style="width: 18%; font-weight: normal;">
                                                    Trade In Description-
                                                </td>
                                                <td colspan="3" style="font-weight: normal;">
                                                    <asp:Label ID="lblDesc" runat="server" Text='<%#Bind("TradeInDesc") %>'></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:DataList>
                </td>
            </tr>
            <tr style="height: 5px;">
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <ajaxToolkit:TabContainer ID="tcTradeIn" runat="server" Width="100%" ActiveTabIndex="1"
                        Visible="false">
                        <ajaxToolkit:TabPanel ID="tbHistory" runat="server" HeaderText="History">
                            <ContentTemplate>
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr align="center" style="background-color: #0A73A2; color: White; font-weight: bold;
                                        height: 30px">
                                        <td style="width: 14%;">
                                            Date
                                        </td>
                                        <td style="width: 9%;">
                                            Time
                                        </td>
                                        <td style="width: 15%;">
                                            Result
                                        </td>
                                        <td style="width: 38%;">
                                            Regarding & Details
                                        </td>
                                        <td style="width: 14%;">
                                            Record Manager
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                            <asp:Panel ID="pnlTradeInHistory" runat="server" Height="220px" ScrollBars="Vertical">
                                                <asp:GridView ID="gvTradeInHistory" runat="server" AllowPaging="false" AllowSorting="false"
                                                    DataKeyNames="ID" AutoGenerateColumns="false" Width="100%" BackColor="White"
                                                    ShowFooter="false" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="0px"
                                                    CellPadding="3" EmptyDataText="No Records Found" ShowHeader="false" OnRowDataBound="gvTradeInHistory_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField DataField="CreateDate1" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center"
                                                            HeaderStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top" ItemStyle-Height="20px" />
                                                        <asp:BoundField DataField="HTime" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                                                            HeaderStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top" ItemStyle-Height="20px" />
                                                        <asp:BoundField DataField="Result" ItemStyle-Width="18%" ItemStyle-HorizontalAlign="Left"
                                                            HeaderStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top" ItemStyle-Height="20px" />
                                                        <%--  <asp:BoundField DataField="REGARDING" ItemStyle-Width="43%" ItemStyle-VerticalAlign="Top"
                                                            ItemStyle-Height="20px" />--%>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRD" runat="server" Text='<%# Bind("REGARDING") %>'></asp:Label>
                                                                <asp:HiddenField ID="hdfRegardings" runat="server" Value='<%# Bind("REGARDING") %>' />
                                                                <asp:HiddenField ID="hdfDetails" runat="server" Value='<%# Bind("DETAILS") %>' />
                                                                <asp:HiddenField ID="hdfResult" runat="server" Value='<%# Bind("Result") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle Width="43%" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="MANAGER" ItemStyle-Width="14%" ItemStyle-HorizontalAlign="Left"
                                                            HeaderStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top" ItemStyle-Height="20px" />
                                                    </Columns>
                                                </asp:GridView>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </ajaxToolkit:TabPanel>
                        <ajaxToolkit:TabPanel ID="tbViewPhoto" runat="server" HeaderText="View Photo">
                            <ContentTemplate>
                                <div style="overflow-x: scroll; width: 710px;" class="brdrImg">
                                    <asp:DataList ID="dlTradeInImage" CellPadding="5" CellSpacing="5" BorderWidth="0"
                                        runat="server" GridLines="None" ItemStyle-Width="251px" RepeatDirection="Horizontal"
                                        ShowFooter="False" ShowHeader="False" DataKeyField="ID">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdfPhotoID" runat="server" Value='<%# Bind("ID") %>' />
                                            <div class="headIMG" style="background-color: #F4F8FA; border-right: 1px solid #C2DBE7;
                                                border-left: 1px solid #C2DBE7; border-top: 1px solid #C2DBE7; float: left; padding: 3px 5px;
                                                width: 95.5%;">
                                                <asp:Label ID="lnkImage" Text='<%# Bind("PhotoName") %>' runat="server" Width="95%"
                                                    Style="float: left;" />
                                                <asp:ImageButton ID="imgDelete" OnClick="imgDelete_Click" ImageUrl="~/Images/Inactive.ico"
                                                    runat="server" ToolTip="Delete Image" Width="5%" Style="float: right; margin-top: 2px;" />
                                            </div>
                                            <asp:Image ID="imgPhoto" runat="server" Width="250px" Height="200px" ImageUrl='<% #Eval("PhotoPath") %>' />
                                        </ItemTemplate>
                                    </asp:DataList>
                                </div>
                            </ContentTemplate>
                        </ajaxToolkit:TabPanel>
                    </ajaxToolkit:TabContainer>
                </td>
            </tr>
            <tr id="trRemoveTradeIn" runat="server" visible="false" align="center">
                <td>
                    <br />
                    <asp:HiddenField ID="hdfTradeInID" runat="server" />
                    <asp:HiddenField ID="hdfISWithPhoto" runat="server" />
                    <asp:HiddenField ID="hdfTradeIn" runat="server" />
                    <asp:ImageButton ID="imgbtnClientPDF" runat="server" ImageUrl="~/Images/client_pdf_u.gif"
                        onmouseout="this.src='Images/client_pdf_u.gif'" onmouseover="this.src='Images/client_pdf_d.gif'"
                        OnClick="imgbtnClientPDF_Click" CommandName="Cwithout" />
                    <asp:ImageButton ID="imgbtnClientPDFWithPhoto" runat="server" ImageUrl="~/Images/client_pdfWP_u.gif"
                        onmouseout="this.src='Images/client_pdfWP_u.gif'" onmouseover="this.src='Images/client_pdfWP_d.gif'"
                        OnClick="imgbtnClientPDF_Click" AlternateText="With Photo" CommandName="Cwith" />
                    <asp:ImageButton ID="imgbtnStandardPDF" runat="server" ImageUrl="~/Images/standard_pdf_u.gif"
                        onmouseout="this.src='Images/standard_pdf_u.gif'" onmouseover="this.src='Images/standard_pdf_d.gif'"
                        OnClick="imgbtnStandardPDF_Click" CommandName="Swithout" />
                    <asp:ImageButton ID="imgbtnStandardPDFWithPhoto" runat="server" ImageUrl="~/Images/standard_pdfWP_u.gif"
                        onmouseout="this.src='Images/standard_pdfWP_u.gif'" onmouseover="this.src='Images/standard_pdfWP_d.gif'"
                        OnClick="imgbtnStandardPDF_Click" AlternateText="With Photo" CommandName="Swith" />
                    <asp:ImageButton ID="btnRemove" runat="server" Visible="false" ImageUrl="~/Images/Remove.gif" onmouseout="this.src='Images/Remove.gif'"
                        onmouseover="this.src='Images/Remove_hvr.gif'" OnClick="btnRemove_Click" />
                    <asp:ImageButton ID="imgbtnUploadPhoto" runat="server" ImageUrl="~/Images/uploadPhoto.png"
                        onmouseout="this.src='Images/uploadPhoto.png'" onmouseover="this.src='Images/uploadPhoto_hvr.png'"
                        OnClick="imgbtnUploadPhoto_Click" />
                    <asp:ImageButton ID="imgbtnViewPhoto" runat="server" ImageUrl="~/Images/viewPhoto.png"
                        onmouseout="this.src='Images/viewPhoto.png'" onmouseover="this.src='Images/viewPhoto_hvr.png'"
                        OnClick="imgbtnViewPhoto_Click" Visible="false" />
                </td>
            </tr>
        </table>
        <div id="divpopID" runat="server" visible="false" align="center" style="width: 386px;
            height: 135px;">
            <div id="progressBackgroundFilter">
            </div>
            <div id="processMessage1" align="center" style="width: 386px; height: 135px; left: 250
                !important; border: solid 2px #000000;">
                <table>
                    <tr>
                        <td style="background-color: #17608C; padding: 5px;">
                            <span style="text-align: left;"><b>
                                <asp:Label ID="lblTitle" runat="server" ForeColor="White">Upload Trade In Photos</asp:Label></b></span>
                            <span style="float: right; margin-top: -1px; margin-right: 8px; z-index: 2;">
                                <asp:ImageButton ID="btnPopClose" runat="server" ImageUrl="~/Images/cancel.png" OnClick="btnPopClose_Click" />
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <uc1:ucTradeInPhoto ID="ucTradeInPhoto1" runat="server" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <%--<div id="pnlMsgPDF" runat="server" align="center" style="display: none; width: 330px;
            height: 110px;">
            <div id="progressBackgroundFilter1">
            </div>
            <div id="processMessage" align="center" style="width: 330px; height: 110px; border: solid 2px #000000;">
                <table>
                    <tr>
                        <td style="background-color: #17608C; width: 330px; padding: 5px;">
                            <span style="text-align: left;"><b>
                                <asp:Label ID="lblPF" runat="server" ForeColor="White">Private Fleet</asp:Label></b></span>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <br />
                            Are you want Trade In Photo in PDF?
                        </td>
                    </tr>
                    <tr>
                        <td align="center" id="tdButtons" runat="server">
                            <br />
                            <asp:ImageButton ID="imgbtnYes" Style="float: left; margin-left: 25%;" runat="server"
                                ImageUrl="~/Images/yes_d.png" onmouseout="this.src='Images/yes_d.png'" onmouseover="this.src='Images/yes_u.png'"
                                AlternateText="Yes" OnClick="imgbtnYes_Click" />
                            <asp:ImageButton ID="imgbtnNo" Style="float: left; margin-left: 5px;" runat="server"
                                ImageUrl="~/Images/no_d.png" ImageAlign="AbsMiddle" onmouseout="this.src='Images/no_d.png'"
                                onmouseover="this.src='Images/no_u.png'" AlternateText="No" OnClick="imgbtnNo_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>--%>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="imgbtnClientPDF" />
        <asp:PostBackTrigger ControlID="imgbtnClientPDFWithPhoto" />
        <asp:PostBackTrigger ControlID="imgbtnStandardPDF" />
        <asp:PostBackTrigger ControlID="imgbtnStandardPDFWithPhoto" />
    </Triggers>
</asp:UpdatePanel>
