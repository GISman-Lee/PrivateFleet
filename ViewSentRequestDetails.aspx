<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ViewSentRequestDetails.aspx.cs" Inherits="ViewSentRequestDetails" %>

<%@ Register Src="User Controls/Request/ucCustomerDetails.ascx" TagName="ucCustomerDetails"
    TagPrefix="uc4" %>
<%@ Register Src="User Controls/Request/ucRequestHeader.ascx" TagName="ucRequestHeader"
    TagPrefix="uc3" %>
<%@ Register Src="User Controls/Request/ucFixedCharges.ascx" TagName="ucFixedCharges"
    TagPrefix="uc2" %>
<%@ Register Src="~/User Controls/Request/ucSeriesAccessories.ascx" TagName="ucSeriesAccessories"
    TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" align="center">
        <tr>
            <td align="right" style="padding-bottom: 0px">
                <asp:ImageButton ID="btnBack" runat="server" ImageUrl="~/Images/back.gif" onmouseout="this.src='Images/back.gif'"
                    onmouseover="this.src='Images/back_hvr.gif'" OnClick="btnBack_Click" />
            </td>
        </tr>
        <tr runat="server" visible="false" id="trViewShortListedQuotation">
            <td align="right" style="padding-bottom: 0px">
                <asp:LinkButton ID="lbtnViewSLQuotation" runat="server" OnClick="lbtnViewSLQuotation_Click"
                    CssClass="activeLink">View Short Listed Quotation For this Request</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td style="width: ">
                <uc3:ucRequestHeader ID="UcRequestHeader1" runat="server" />
            </td>
        </tr>
        <tr>
            <td style="border-style: solid; font-family: Arial; border-color: #acacac; border-width: 1px;
                width: 100%">
                <table width="100%">
                    <tr>
                        <td>
                            <asp:Label ID="lblSub" runat="server" Width="85px"><strong>Suburb : </strong> </asp:Label>
                        </td>
                        <td>
                            <a id="map" href="#" runat="server" style="text-decoration: none;">
                                <asp:Label ID="lblSub1" runat="server" Width="200px"></asp:Label></a>
                        </td>
                        <td style="width: 20%">
                            <asp:Label ID="lblPCode" runat="server" Width="85px"><strong>Postal Code :  </strong> </asp:Label>
                        </td>
                        <td style="width: 30%">
                            <asp:Label ID="lblPCode1" runat="server" Width="200px"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr align="center">
            <td align="center">
                <asp:Panel ID="dragMapPanel" runat="server">
                    <div id="info" style="display: none; width: 500px; z-index: 2; opacity: 0; font-size: 12px;
                        border: solid 1px #CCCCCC; background-color: #FFFFFF; padding: 5px;">
                        <%--<asp:Panel ID="drag" CssClass="dragable" runat="server">
                            Drag</asp:Panel>--%>
                        <div id="btnCloseParent" style="z-index: 2; padding-top: 10px; float: right; opacity: 1;">
                            <asp:LinkButton ID="btnClose" runat="server" Text="X" ToolTip="Close" Style="background-color: #666666;
                                color: #FFFFFF; text-align: center; font-weight: bold; text-decoration: none;
                                border: outset thin #FFFFFF; padding: 5px;" OnClientClick="javascript:divClose();  return false;" />
                        </div>
                        <div id="map1" style="padding-top: 10px; text-align: center; width: 450px; height: 450px;
                            background-color: #AAAAAA;">
                        </div>
                    </div>
                </asp:Panel>
                <%--  <ajax:DragPanelExtender ID="drgMap" runat="server" TargetControlID="dragMapPanel"
                    DragHandleID="drag">
                </ajax:DragPanelExtender>--%>
            </td>
        </tr>
        <tr>
            <td>
                <uc4:ucCustomerDetails ID="UcCustomerDetails1" runat="server" />
            </td>
        </tr>
        <tr runat="server" id="trConsultantInfo" visible="false">
            <td>
                <table width="100%">
                    <tr class="ucHeader" style="width: 100%; border: solid 1px #ACACAC">
                        <td align="left" bgcolor="">
                            <asp:Label ID="lblHeader" runat="server" CssClass="gvLabel" Text="">Consultant</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" width="100%" align="right">
                            <asp:DataList ID="dlConsultantInfo" runat="server" RepeatDirection="Horizontal" Width="85%"
                                BorderStyle="solid" BorderColor="#acacac" BorderWidth="1px">
                                <ItemTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 50%; font-weight: bold" align="left" bgcolor="#eaeaea">
                                                <asp:Label ID="lblHeader" runat="server" CssClass="gvLabel" Text='<%# Bind("Header") %>'></asp:Label>
                                            </td>
                                            <td style="width: 50%; padding-left: 10px;" align="left">
                                                <asp:Label ID="lblDetails" runat="server" CssClass="gvLabel" Text='<%# Bind("Details") %>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:DataList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr runat="server" id="trDealerInfo" visible="true">
            <td>
                <table width="100%">
                    <tr class="ucHeader" style="width: 100%; border: solid 1px #acacac">
                        <td align="left">
                            <asp:Label ID="lblDealer" runat="server" CssClass="gvLabel" Text="">Dealer</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%" align="right">
                            <asp:GridView ID="gvDealerInfo" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                BackColor="White" BorderColor="#acacac" BorderStyle="Solid" BorderWidth="1px"
                                CellPadding="3" PageSize="5" Width="85%" DataKeyNames="Reminder" OnRowDataBound="gvDealerInfo_RowDataBound"
                                OnRowCommand="gvDealerInfo_RowCommand">
                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                <PagerStyle CssClass="pgr" />
                                <HeaderStyle BackColor="#eaeaea" ForeColor="Black" CssClass="gvHeader" Font-Bold="True"
                                    Height="20px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Dealer Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDealerName" runat="server" Text='<%# Bind("[Dealer Name]") %>'>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Email">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="Email"  runat="server" Text='<%# Bind("Email") %>'>'></asp:Label>--%>
                                            <a href='<%# "mailto:"+ Eval("Email") %>' style="color: Blue; text-decoration: underline;">
                                                <asp:Label ID="Email" runat="server" Text='<%#Bind("Email") %>'></asp:Label></a>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Phone/Mobile">
                                        <ItemTemplate>
                                            <asp:Label ID="Phone" runat="server" Text='<%# Eval("Phone")%>'></asp:Label>
                                            /
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("Mobile")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Reminder">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkRemind" runat="server" CommandArgument='<%# Bind("DealerID") %>'
                                                CommandName="RemindDealer" CssClass="activeLink">
                                                <asp:Literal ID="litlnk" Text="Quotation Pending <br /> Remind Dealer" runat="server"></asp:Literal>
                                            </asp:LinkButton>
                                            <asp:HiddenField runat="server" ID="LastRemindDateTime" Value='<% #Bind("LastRemindDateTime")%>' />
                                            <asp:HiddenField runat="server" ID="QoutationID" Value='<% #Bind("QoutaionExist")%>' />
                                            <asp:HiddenField runat="server" ID="LastDate" Value='<% #Bind("LastRemindDateTime")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                        <!-- <td colspan="2" width="100%" align="right" >
                            <asp:DataList ID="dlDealerInfo" runat="server" RepeatDirection="Horizontal" Width="85%"
                                BorderStyle="solid" BorderColor="#acacac" BorderWidth="1px">
                                <ItemTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 35%; font-weight :bold " align="left" bgcolor="#eaeaea">
                                                <asp:Label ID="lblHeader" runat="server" CssClass="gvLabel" >Dealer Name</asp:Label>
                                            </td>
                                            <td style="width: 35%; font-weight :bold " align="left" bgcolor="#eaeaea">
                                                <asp:Label ID="Label2" runat="server" CssClass="gvLabel" >Email </asp:Label>
                                            </td>
                                            <td style="width: 30%; font-weight :bold " align="left" bgcolor="#eaeaea">
                                                <asp:Label ID="Label3" runat="server" CssClass="gvLabel" >Phone</asp:Label>
                                            </td>
                                            
                                        </tr>
                                        <tr>
                                            <td style="width: 100px; height: 21px;" align="left">
                                                <asp:Label ID="lblDetails" runat="server" CssClass="gvLabel" Text='<%# Bind("[Dealer Name]") %>'></asp:Label>
                                            </td>
                                            <td style="width: 100px; height: 21px;" align="left">
                                                <asp:Label ID="Label4" runat="server" CssClass="gvLabel" Text='<%# Bind("Email") %>'></asp:Label>
                                            </td>
                                            <td style="width: 100px; height: 21px;" align="left">
                                                <asp:Label ID="Label5" runat="server" CssClass="gvLabel" Text='<%# Bind("Phone") %>'></asp:Label>
                                            </td>
                                        </tr>
                                        
                                    </table>
                                </ItemTemplate>
                            </asp:DataList>
                        </td>  -->
                    </tr>
                </table>
            </td>
        </tr>
        <%--<tr>
            <td>
                <table width="100%" align="center" cellpadding="1" cellspacing="3" border="0">
                    <tr>
                        <td>
                            <strong>Make, Model & Series : </strong>
                            <asp:Label ID="lblMake" runat="server" Text=''></asp:Label>
                            <asp:Label ID="lblModel" runat="server" Text=''></asp:Label>
                            <asp:Label ID="lblSeries" runat="server" Text=''></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>--%>
        <tr>
            <td style="padding-bottom: 0px; width: 100%">
                <uc1:ucSeriesAccessories ID="UcSeriesAccessories1" runat="server" Visible="false" />
            </td>
        </tr>
        <tr>
            <td style="padding-left: 110px; padding-top: 0px; padding-bottom: 0px;">
                <asp:Label ID="lblAccessory1" Width="100%" BorderColor="#ACACAC" BorderStyle="Solid"
                    BorderWidth="1px" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="padding-top: 0px">
                <asp:GridView ID="gvAccessories" runat="server" AutoGenerateColumns="False" CellPadding="3"
                    CellSpacing="1" Width="100%" BorderColor="#acacac">
                    <Columns>
                        <asp:TemplateField HeaderText="Accessory">
                            <ItemTemplate>
                                <asp:Label ID="lblAccessory" runat="server" Text='<%#Bind("AccessoryName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="specification">
                            <ItemTemplate>
                                <asp:Label ID="lblSpec" runat="server" Text='<%#Bind("Specification") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle ForeColor="Navy" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
                <uc2:ucFixedCharges ID="UcFixedCharges1" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <table width="90%" align="left" cellpadding="1" cellspacing="3" border="0">
                    <%--<tr>
                        <td class="subheading">
                            <strong>Parameters</strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvParameters" runat="server" AutoGenerateColumns="False" CellPadding="3"
                                CellSpacing="1" Width="100%" BorderColor="#acacac" EmptyDataText="No Records Found">
                                <Columns>
                                    <asp:TemplateField HeaderText="Parameter">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAccessory" runat="server" Text='<%#Bind("Parameter") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Value">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSpec" runat="server" Text='<%#Bind("ParamValue") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle ForeColor="Navy" />
                            </asp:GridView>
                        </td>
                    </tr>--%>
                    <%-- <tr>
                        <td class="subheading">
                            <strong>Additional Accessories</strong>
                        </td>
                    </tr>--%>
                    <%-- <tr>
                        <td>
                            <asp:GridView ID="gvAccessories" runat="server" AutoGenerateColumns="False" CellPadding="3"
                                CellSpacing="1" Width="100%" BorderColor="#acacac" EmptyDataText="No Records Found">
                                <Columns>
                                    <asp:TemplateField HeaderText="Accessory">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAccessory" runat="server" Text='<%#Bind("AccessoryName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="specification">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSpec" runat="server" Text='<%#Bind("Specification") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle ForeColor="Navy" />
                            </asp:GridView>
                        </td>
                    </tr>--%>
                    <tr>
                        <td class="subheading" runat="Server" id="DealerInfoSecion" visible="False">
                            <strong>Dealers</strong>
                        </td>
                    </tr>
                    <tr style="width: 100%">
                        <td>
                            <asp:GridView ID="gvDealers" runat="server" AutoGenerateColumns="False" CellPadding="3"
                                CellSpacing="1" Width="100%" BorderColor="#acacac" EmptyDataText="No Records Found">
                                <Columns>
                                    <asp:BoundField HeaderText="Dealer Name" DataField="DealerName" />
                                    <asp:BoundField HeaderText="Email" DataField="Email" />
                                    <asp:BoundField HeaderText="Phone Number" DataField="Phone" />
                                </Columns>
                                <HeaderStyle ForeColor="Navy" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="subheading" style="font-size: 14px; width: 100%">
                <strong>Consultant Notes:</strong>
            </td>
        </tr>
        <tr>
            <td style="border: 1px solid #acacac; width: 100%">
                <asp:Label ID="lblNotes" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="Center" style="padding-bottom: 0px">
                <asp:ImageButton ID="btnBack_Copy" runat="server" ImageUrl="~/Images/back.gif" onmouseout="this.src='Images/back.gif'"
                    onmouseover="this.src='Images/back_hvr.gif'" OnClick="btnBack_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
