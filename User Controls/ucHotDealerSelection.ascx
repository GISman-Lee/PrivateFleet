<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucHotDealerSelection.ascx.cs"
    Inherits="User_Controls_ucHotDealerSelection" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>--%>
<asp:UpdatePanel ID="UP2" runat="server">
    <ContentTemplate>
        <%--<table width="100%" align="center" style="border: 1px solid #4A4A4A" cellpadding="1"       cellspacing="2">--%>
        <table width="100%" align="center" cellpadding="1" cellspacing="2">
            <tr>
                <td valign="top" width="50%" align="left">
                    <table width="100%" align="center" cellpadding="0" cellspacing="1" id="tblHotDealers"
                        runat="server">
                        <tr visible="false">
                            <td class="subheading">
                                <strong>Hot Dealers</strong>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 5px;">
                            </td>
                        </tr>
                        <tr visible="false">
                            <td>
                                <asp:DataList ID="dlHotDealers" runat="server" DataKeyField="ID" OnItemCommand="datalist2_ItemCommand"
                                    CellPadding="0" ForeColor="Black" Width="100%">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hfDealerID" runat="server" Value='<%# Eval("ID") %>' />
                                        <table width="100%" align="left" cellpadding="0" cellspacing="2" style="border: 1px solid #CCC">
                                            <tr>
                                                <td width="30%" class="dealerHeading">
                                                    Dealer Name :
                                                </td>
                                                <td width="70%">
                                                    <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                    &nbsp;<strong>(<asp:Label ID="Label1" runat="server" Text='<%# Eval("TotalPoints") %>'></asp:Label>
                                                        Points)</strong>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="dealerHeading">
                                                    Contact Info :
                                                    <asp:HiddenField ID="hdfIsHotDealer" runat="server" Value='<%# Eval("IsHotDealer") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="CompanyLabel" runat="server" Text='<%# Eval("Company") %>'></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Label ID="EmailLabel" runat="server" Text='<%# Eval("Email") %>'></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Label ID="FaxLabel" runat="server" Text='<%# Eval("Fax") %>' Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PhoneLabel" runat="server" Text='<%# Eval("Phone") %>'></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="btnSelect" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/Images/Remove.gif"
                                                        onmouseout="this.src='Images/Remove.gif'" onmouseover="this.src='Images/Remove_hvr.gif'" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 3px;">
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
                                    <SelectedItemStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                                </asp:DataList>
                            </td>
                        </tr>
                        <tr visible="false">
                            <td>
                                <table id="tblEmptyHot" runat="server" width="100%" align="left" cellpadding="0"
                                    cellspacing="2" style="border: 1px solid #CCC">
                                    <tr>
                                        <td class="dealerHeading">
                                            No dealers found
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
                <td valign="top" width="50%" align="left">
                    <table width="100%" align="center" cellpadding="0" cellspacing="1" id="tblNormalDealers"
                        runat="server" visible="false">
                        <tr>
                            <td class="subheading">
                                <strong>Normal Dealers</strong>
                            </td>
                        </tr>
                        <tr visible="false">
                            <td style="height: 5px;">
                            </td>
                        </tr>
                        <tr id="tr1" runat="server" visible="false">
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
                        </tr>
                        <tr visible="false">
                            <td>
                                <asp:DataList ID="dlNormalDealers" runat="server" DataKeyField="ID" OnItemCommand="datalist1_ItemCommand"
                                    CellPadding="0" ForeColor="Black" GridLines="None" Width="100%">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hfDealerID" runat="server" Value='<%# Eval("ID") %>' />
                                        <table width="100%" align="left" cellpadding="0" cellspacing="2" style="border: 1px solid #CCC">
                                            <tr>
                                                <td width="30%" class="dealerHeading">
                                                    Dealer Name :
                                                </td>
                                                <td width="70%">
                                                    <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                    &nbsp;<strong>(<asp:Label ID="Label1" runat="server" Text='<%# Eval("TotalPoints") %>'></asp:Label>
                                                        Points)</strong>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="dealerHeading">
                                                    Contact Info :
                                                    <asp:HiddenField ID="hdfIsHotDealer" runat="server" Value='<%# Eval("IsHotDealer") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="CompanyLabel" runat="server" Text='<%# Eval("Company") %>'></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Label ID="EmailLabel" runat="server" Text='<%# Eval("Email") %>'></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Label ID="FaxLabel" runat="server" Text='<%# Eval("Fax") %>' Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PhoneLabel" runat="server" Text='<%# Eval("Phone") %>'></asp:Label>
                                                </td>
                                            </tr>
                                            <%-- <tr>
                                                <td class="dealerHeading">
                                                    Distance :</td>
                                                <td class="dealerHeading">
                                                    <asp:Label ID="lblKms" runat="server" Text='<%# Eval("Kms") %>'></asp:Label></td>
                                                <td>
                                                </td>
                                            </tr>--%>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="btnSelect" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/Images/HotDealer.gif"
                                                        onmouseout="this.src='Images/HotDealer.gif'" onmouseover="this.src='Images/HotDealer_hvr.gif'"
                                                        OnClick="btnSelect_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 3px;">
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
                        </tr>
                        <tr>
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
                        </tr>
                        <tr id="trPaging" runat="server">
                            <td>
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
        </table>
        <table width="100%">
            <tr style="width: 100%">
                <td>
                    <ajaxToolkit:TabContainer ID="DealersTabContainer" runat="server" Width="100%" ActiveTabIndex="1">
                        <ajaxToolkit:TabPanel ID="HotDealers" runat="server" HeaderText="Hot Dealers">
                            <ContentTemplate>
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr id="trShowHD" align="right" runat="server" visible="false">
                                        <td>
                                            <asp:Label ID="lblRowsToDisplay1" runat="server">Rows To Display:</asp:Label>
                                            <asp:DropDownList ID="ddl_NoRecords1" runat="server" AutoPostBack="true" Width="50px"
                                                OnSelectedIndexChanged="ddl_NoRecords1_SelectedIndexChanged">
                                                <asp:ListItem Value="10">10</asp:ListItem>
                                                <asp:ListItem Value="20" Selected="True">20</asp:ListItem>
                                                <asp:ListItem Value="30">30</asp:ListItem>
                                                <asp:ListItem Value="50">50</asp:ListItem>
                                                <asp:ListItem Value="All">All</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="gvHotDealerDetails" runat="server" AllowPaging="True" AllowSorting="True"
                                                AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None"
                                                BorderWidth="1px" CellPadding="3" OnSelectedIndexChanged="gvHotDealerDetails_SelectedIndexChanged"
                                                OnRowCommand="gvHotDealers_RowCommand" Width="100%" EmptyDataText="Hot Dealers Not Found!"
                                                DataKeyNames="ID" OnPageIndexChanging="gvHotDealerDetails_PageIndexChanging"
                                                PageSize="15" OnSorting="gvHotDealerDetails_Sorting">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Dealer Name" SortExpression="Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblName" runat="server" CssClass="gvLabel" Text='<%# Bind("Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="20%" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Company" SortExpression="Company">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCompany" runat="server" Style="padding-left: 5px" Text='<%# Bind("Company") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Make" SortExpression="Make">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMake" runat="server" Style="padding-left: 10px" Text='<%# Bind("Make") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Postal Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPostalCode" runat="server" Text='<%# Bind("Pcode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkbtnActiveness" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                                CommandName="MakeDealerNormal" CssClass="activatelink" Style="padding-left: 10px">Make Dealer Normal</asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="15%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkbtnActiveness1" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                                CommandName="MakeDealerCold" CssClass="activatelink" Style="padding-left: 10px">Make Dealer Cold</asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="15%" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle CssClass="gvFooterrow" />
                                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle CssClass="pgr" />
                                                <HeaderStyle BackColor="#0A73A2" CssClass="gvHeader" Font-Bold="True" Height="30px" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </ajaxToolkit:TabPanel>
                        <ajaxToolkit:TabPanel ID="NormalDealers" runat="server" HeaderText="Normal Dealers">
                            <ContentTemplate>
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr id="trShowND" align="right" runat="server" visible="false">
                                        <td>
                                            <asp:Label ID="lblRowsToDisplay2" runat="server">Rows To Display:</asp:Label>
                                            <asp:DropDownList ID="ddl_NoRecords2" runat="server" AutoPostBack="true" Width="50px"
                                                OnSelectedIndexChanged="ddl_NoRecords2_SelectedIndexChanged">
                                                <asp:ListItem Value="10">10</asp:ListItem>
                                                <asp:ListItem Value="20" Selected="True">20</asp:ListItem>
                                                <asp:ListItem Value="30">30</asp:ListItem>
                                                <asp:ListItem Value="50">50</asp:ListItem>
                                                <asp:ListItem Value="All">All</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="gvNormalDealerDetails" runat="server" AllowPaging="True" AllowSorting="True"
                                                AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None"
                                                BorderWidth="1px" CellPadding="3" EmptyDataText="Normal Dealers Not Found!" Width="100%"
                                                OnRowCommand="gvNormalDealerDetails_RowCommand" DataKeyNames="ID" OnPageIndexChanging="gvNormalDealerDetails_PageIndexChanging"
                                                PageSize="15" OnSorting="gvNormalDealerDetails_Sorting">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Dealer Name" SortExpression="Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblName0" runat="server" CssClass="gvLabel" Style="padding-left: 5px"
                                                                Text='<%# Bind("Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="20%" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Company" SortExpression="Company">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCompany" runat="server" Style="padding-left: 5px" Text='<%# Bind("Company") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Make" SortExpression="Make">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMake0" runat="server" Style="padding-left: 5px" Text='<%# Bind("Make") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Postal Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPostalCode0" runat="server" Text='<%# Bind("Pcode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkbtnMakeHotDealer" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                                CommandName="MakeHotDealer" CssClass="activatelink" Style="padding-left: 10px">Make Dealer Hot</asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="15%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkbtnActiveness1" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                                CommandName="MakeDealerCold" CssClass="activatelink" Style="padding-left: 10px">Make Dealer Cold</asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="15%" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle CssClass="gvFooterrow" />
                                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle CssClass="pgr" />
                                                <HeaderStyle BackColor="#0A73A2" CssClass="gvHeader" Font-Bold="True" Height="30px" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </ajaxToolkit:TabPanel>
                        <ajaxToolkit:TabPanel ID="tabPanelColdDealer" runat="server" HeaderText="Cold Dealers">
                            <ContentTemplate>
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr id="tr2" align="right" runat="server" visible="false">
                                        <td>
                                            <asp:Label ID="lblColdRows" runat="server">Rows To Display:</asp:Label>
                                            <asp:DropDownList ID="ddlColdCount" runat="server" AutoPostBack="true" Width="50px"
                                                OnSelectedIndexChanged="ddlColdCount_SelectedIndexChanged">
                                                <asp:ListItem Value="10">10</asp:ListItem>
                                                <asp:ListItem Value="20" Selected="True">20</asp:ListItem>
                                                <asp:ListItem Value="30">30</asp:ListItem>
                                                <asp:ListItem Value="50">50</asp:ListItem>
                                                <asp:ListItem Value="All">All</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="gvColdDealer" runat="server" AllowPaging="True" AllowSorting="True"
                                                AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None"
                                                BorderWidth="1px" CellPadding="3" EmptyDataText="Cold Dealers Not Found!" Width="100%"
                                                DataKeyNames="ID" PageSize="15" OnRowCommand="gvColdDealer_RowCommand" OnPageIndexChanging="gvColdDealer_PageIndexChanging"
                                                OnSorting="gvColdDealer_Sorting">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Dealer Name" SortExpression="Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNameCold" runat="server" CssClass="gvLabel" Style="padding-left: 5px"
                                                                Text='<%# Bind("Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="20%" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Company" SortExpression="Company">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCompanyCold" runat="server" Style="padding-left: 5px" Text='<%# Bind("Company") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Make" SortExpression="Make">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMakeCold" runat="server" Style="padding-left: 5px" Text='<%# Bind("Make") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Postal Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPostalCodeCold" runat="server" Text='<%# Bind("Pcode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkMakeDealerNormal" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                                CommandName="MakeDealerNormal" CssClass="activatelink" Style="padding-left: 10px">Make Dealer Normal</asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="15%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkMakeDealerHot" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                                CommandName="MakeDealerHot" CssClass="activatelink" Style="padding-left: 10px">Make Dealer Hot</asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="15%" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle CssClass="gvFooterrow" />
                                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle CssClass="pgr" />
                                                <HeaderStyle BackColor="#0A73A2" CssClass="gvHeader" Font-Bold="True" Height="30px" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </ajaxToolkit:TabPanel>
                    </ajaxToolkit:TabContainer>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
