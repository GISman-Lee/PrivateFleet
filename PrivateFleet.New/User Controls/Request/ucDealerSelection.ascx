<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucDealerSelection.ascx.cs"
    Inherits="User_Controls_ucDealerSelection" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:UpdatePanel ID="UpPanMap" runat="server">
    <ContentTemplate>
        <br />
        <table width="100%" id="tblSelectedDealers_pre" runat="server" visible="false" align="center"
            style="border: 1px solid #4A4A4A" cellpadding="1" cellspacing="2">
            <tr>
                <td valign="top" class="subheading">
                    <strong>Previously Selected Dealers</strong>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <asp:GridView ID="gvSelectedDealers_pre" runat="server" DataKeyNames="ID" Width="100%"
                        AllowPaging="true" PageSize="10" AutoGenerateColumns="false" BorderColor="#acacac"
                        CellPadding="1" CellSpacing="3" EmptyDataText="No records found">
                        <Columns>
                            <asp:BoundField DataField="Name" HeaderText="Dealer Name" />
                            <asp:BoundField DataField="Email" HeaderText="Email" />
                            <asp:BoundField DataField="Phone" HeaderText="Phone Number" />
                            <asp:BoundField DataField="Fax" HeaderText="Fax" Visible="False" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkRemove" runat="server" ForeColor="Gray" Enabled="false" Text="Remove Dealer"
                                        CommandName="RemoveDealer" CssClass="activeLink">
                                    </asp:LinkButton>
                                    <asp:HiddenField ID="hdfID" runat="server" Value='<%# Bind("ID") %>' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="gvDealerSelectHeader" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <br />
        <table width="100%" id="tblSelectedDealers" runat="server" visible="false" align="center"
            style="border: 1px solid #4A4A4A" cellpadding="1" cellspacing="2">
            <tr>
                <td valign="top" class="subheading">
                    <strong>Selected Dealers</strong>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <asp:GridView ID="gvSelectedDealers" runat="server" DataKeyNames="ID" Width="100%"
                        AllowPaging="true" PageSize="10" AutoGenerateColumns="false" OnRowCommand="gvSelectedDealers_RowCommand"
                        BorderColor="#acacac" CellPadding="1" CellSpacing="3" EmptyDataText="No records found"
                        OnRowDataBound="gvSelectedDealers_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="Name" HeaderText="Dealer Name" />
                            <asp:BoundField DataField="Email" HeaderText="Email" />
                            <asp:BoundField DataField="Phone" HeaderText="Phone Number" />
                            <asp:BoundField DataField="Fax" HeaderText="Fax" Visible="False" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkRemove" runat="server" Text="Remove Dealer" CommandName="RemoveDealer"
                                        CssClass="activeLink">
                                    </asp:LinkButton>
                                    <asp:HiddenField ID="hdfID" runat="server" Value='<%# Bind("ID") %>' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="gvDealerSelectHeader" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <br />
        <table width="100%" align="center" style="border: 1px solid #4A4A4A" cellpadding="1"
            cellspacing="2">
            <tr>
                <td valign="top" align="left" colspan="2" style="height: 626px">
                    <table width="100%" align="center" cellpadding="0" cellspacing="1" id="tblNormalDealers"
                        runat="server">
                        <tr>
                            <td class="subheading" style="height: 19px">
                                <strong>Dealers</strong>
                            </td>
                        </tr>
                        <tr>
                            <td id="animation" align="center">
                                <!-- "Wire frame" div used to transition from the button to the info panel -->
                                <%--<div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF;
                                    border: solid 1px #D0D0D0;">
                                </div>--%>
                                <!-- Info panel to be displayed as a flyout when the button is clicked -->
                                <asp:Panel ID="dragMapPanel" runat="server">
                                    <div id="info" style="display: none; width: 500px; z-index: 2; opacity: 0; font-size: 12px;
                                        border: solid 1px #CCCCCC; background-color: #FFFFFF; padding: 5px;">
                                        <%-- <asp:Panel ID="drag" CssClass="dragable" runat="server">
                                            Drag</asp:Panel>--%>
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
                                <%--<ajax:DragPanelExtender ID="drgMap" runat="server" TargetControlID="dragMapPanel"
                                    DragHandleID="drag">
                                </ajax:DragPanelExtender>--%>
                            </td>
                        </tr>
                        <tr>
                            <td valign="middle">
                                <br />
                                <table width="100%">
                                    <tr align="center">
                                        <td align="center" width="10%">
                                        </td>
                                        <td class="datalistNormalDealer" width="30%" align="center" style="border-right: #000000 double;
                                            border-top: #000000 double; border-left: #000000 double; border-bottom: #000000 double">
                                            Statewide Dealers
                                        </td>
                                        <td align="center" width="10%">
                                        </td>
                                        <%-- <td width="25%" class="datalistHotDealer" align="center" style="border-right: #000000 double;
                                            border-top: #000000 double; border-left: #000000 double; border-bottom: #000000 double;
                                            color: White; background: #799C60 url(../images/hot_deale_btn1.png) no-repeat left bottom;">
                                            Hot Dealers
                                        </td>--%>
                                        <td align="center" width="10%">
                                        </td>
                                        <td width="30%" style="border-right: #000000 double; border-top: #000000 double;
                                            border-left: #000000 double; border-bottom: #000000 double" class="datalistOutSideDealer">
                                            Interstate Dealers
                                        </td>
                                        <td align="center" width="10%">
                                        </td>
                                    </tr>
                                </table>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 50%">
                                            <asp:Label ID="lblNoOfNormal" runat="server"></asp:Label>
                                        </td>
                                        <td style="width: 50%">
                                            <asp:Label ID="lblNoOfHot" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
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
                            <td class="dealerHeading">
                                <asp:GridView ID="gvDealerDetails" runat="server" AutoGenerateColumns="False" Width="100%"
                                    DataKeyNames="ID" AllowPaging="True" PageSize="10" BackColor="White" BorderColor="#CCCCCC"
                                    BorderStyle="None" BorderWidth="1px" CellPadding="3" AllowSorting="True" OnRowCommand="gvDealerDetails_RowCommand"
                                    OnRowDataBound="gvDealerDetails_RowDataBound" EmptyDataText="No dealer found."
                                    OnPageIndexChanging="gvDealerDetails_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemStyle Width="100px" />
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hfDealerID" runat="server" Value='<%# Eval("ID") %>' />
                                                <asp:Label ID="lblName" runat="server" Text='<%#Bind("Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%-- <asp:BoundField DataField="Name" HeaderText="Dealer" SortExpression="Name" />--%>
                                        <asp:TemplateField HeaderText="Contact Info">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCompany" runat="server" Text='<%#Bind("Company") %>'></asp:Label>
                                                <br />
                                                <asp:Label ID="lblEmail" Visible="false" runat="server" Text='<%#Bind("Email") %>'></asp:Label>
                                                <a id="aemail" runat="server" href='<%# "mailto:"+ Eval("Email") %>' style="color: blue;
                                                    text-decoration: underline;">
                                                    <asp:Label ID="Label1" runat="server" Text='<%#Bind("Email") %>'></asp:Label></a>
                                                <br />
                                                Mobile&nbsp;:&nbsp;<asp:Label ID="lblFax" Font-Size="14px" runat="server" Text='<%#Bind("Mobile") %>'></asp:Label>
                                                <br />
                                                Phone&nbsp;&nbsp;&nbsp;:&nbsp;
                                                <asp:Label ID="lblPhone" runat="server" Font-Size="14px" Text='<%#Bind("Phone") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="TotalPoints" HeaderText="Points" />
                                        <%--<asp:BoundField DataField="kms" HeaderText="Distance(In Kms)" />--%>
                                        <asp:TemplateField HeaderText="Distance">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lnkMap" NavigateUrl="javascript://" ToolTip="Click to see Google Map"
                                                    runat="server" Text='<%# Eval("kmsState") %>'></asp:HyperLink>
                                                <br />
                                                <asp:Label ID="lblCity" runat="server" Text='<%#Bind("CityID") %>'></asp:Label>
                                                <asp:Label ID="lblLati" runat="server" Visible="false" Text='<%#Bind("Lati") %>'></asp:Label>
                                                <asp:Label ID="lblLongi" runat="server" Visible="false" Text='<%#Bind("Longi") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rating" SortExpression="Rating">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdfTot" runat="server" Value='<% #Bind("Total") %>' />
                                                <asp:Label ID="lblRating" runat="server" Text='<%#Bind("Rating") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnSelect" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/Images/Select_dealer.gif"
                                                    CommandName="SelectDealer" onmouseout="this.src='Images/Select_dealer.gif'" onmouseover="this.src='Images/Select_dealer_hvr.gif'" />
                                                <asp:HiddenField ID="hdfIsHotDealer" runat="server" Value='<%# Eval("IsHotDealer") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle Height="80" />
                                    <FooterStyle CssClass="gvFooterrow" />
                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle CssClass="pgr" />
                                    <HeaderStyle BackColor="#0A73A2" Font-Bold="True" CssClass="gvHeader" Height="30px" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <%--<tr id="tr1" runat="server" visible="false">
                            <td>
                                <strong>Page Size:</strong>
                                <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                                    <asp:ListItem>5</asp:ListItem>
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                    <asp:ListItem>40</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                    <asp:ListItem>100</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>--%>
                        <%-- <tr>
                    <td>
                        <table width="100%">
                            <tr>
                                <td style="width: 50%">
                                    <asp:Label ID="lblNoOfNormal" runat="server"></asp:Label>
                                </td>
                                <td style="width: 50%">
                                    <asp:Label ID="lblNoOfHot" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>--%>
                        <%--  <tr>
                    <td>
                        <asp:DataList ID="dlNormalDealers" runat="server" DataKeyField="ID" OnItemCommand="datalist_ItemCommand"
                            CellPadding="0" ForeColor="Black" GridLines="None" Width="100%" OnItemCreated="dlNormalDealers_ItemCreated"
                            OnItemDataBound="dlNormalDealers_ItemDataBound">
                            <ItemTemplate>
                                <asp:HiddenField ID="hfDealerID" runat="server" Value='<%# Eval("ID") %>' />
                                <table width="100%" align="left" cellpadding="0" cellspacing="2" style="border: 1px solid #CCC">
                                    <tr>
                                        <td width="20%" class="dealerHeading">
                                            Dealer Name :
                                        </td>
                                        <td width="30%">
                                            <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                            <strong>(<asp:Label ID="Label1" runat="server" Text='<%# Eval("TotalPoints") %>'></asp:Label>)</strong>
                                        </td>
                                        <td width="15%" class="dealerHeading">
                                            Contact Info :&nbsp;
                                        </td>
                                        <td width="40%" class="dealerHeading" rowspan="3" valign="top">
                                            <asp:Label ID="CompanyLabel" runat="server" Text='<%# Eval("Company") %>'></asp:Label><br />
                                            <asp:Label ID="EmailLabel" runat="server" Text='<%# Eval("Email") %>'></asp:Label>
                                            <br />
                                            <asp:Label ID="FaxLabel" runat="server" Text='<%# Eval("Fax") %>' Visible="false"></asp:Label>
                                            <br />
                                            <asp:Label ID="PhoneLabel" runat="server" Text='<%# Eval("Phone") %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="dealerHeading">
                                            Distance From Customer:
                                        </td>
                                        <td class="dealerHeading">
                                            <asp:Label ID="lblKms" runat="server" Text='<%# Eval("Kms") %>'></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnSelect" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/Images/Select_dealer.gif"
                                                onmouseout="this.src='Images/Select_dealer.gif'" onmouseover="this.src='Images/Select_dealer_hvr.gif'" />
                                        </td>
                                        <td>
                                            <asp:HiddenField ID="hdfIsHotDealer" runat="server" Value='<%# Eval("IsHotDealer") %>' />
                                        </td>
                                    </tr>
                                </table>
                                <table>
                                    <tr>
                                        <td style="height: 3px;">
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                            <SelectedItemStyle BackColor="#CC3333" Font-Bold="True" />
                            <HeaderStyle BackColor="#333333" Font-Bold="True" />
                        </asp:DataList>
                    </td>
                </tr>--%>
                        <%-- <tr>
                    <td>
                        <table id="tblEmptyNormal" runat="server" width="100%" align="left" cellpadding="0"
                            cellspacing="2" style="border: 1px solid #CCC">
                            <tr>
                                <td class="dealerHeading">
                                    No dealers found
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>--%>
                        <tr id="trPaging" runat="server" visible="false">
                            <td visible="false">
                                <asp:LinkButton ID="lnkbtnPrevious" runat="server" Text="<< Prev" OnClick="lnkbtnPrevious_Click"></asp:LinkButton>
                                <asp:DataList ID="dlPaging" runat="server" OnItemCommand="dlPaging_ItemCommand" OnItemDataBound="dlPaging_ItemDataBound"
                                    RepeatDirection="Horizontal" RepeatLayout="Flow">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkbtnPaging" runat="server" CommandArgument='<%# Eval("PageIndex") %>'
                                            CommandName="lnkbtnPaging" Text='<%# Eval("PageText") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:DataList>
                                <asp:LinkButton ID="lnkbtnNext" runat="server" Text="Next >>" OnClick="lnkbtnNext_Click"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="left" valign="top" width="50%">
                </td>
                <td align="left" valign="top" width="50%">
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
