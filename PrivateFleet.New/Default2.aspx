<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Default2.aspx.cs" Inherits="Default2" Title="Report" Trace="false"
    ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function Print()
        {
            var HTML=document.getElementById("gridContainer").innerHTML;
          var myWindow = window.open("", "Report", 'menubar=yes,width=800,height=800,scrollbars=yes,resizable=yes') 
          
          myWindow.document.open();
          myWindow.document.write('<style> .subheading{color: #FFF;background-color: #0A73A2;font-size: 12px;font-weight: bold;height: 20px;text-align: left;padding-left: 5px;} .gridactiverow{background-color: #EFF9FF;height: 25px;color: #000000;font-family: Arial, Helvetica, sans-serif;font-size: 12px;} .griddeactiverow{background-image: url(../images/readmssage_bg.gif);background-repeat: repeat-x;height: 25px;color: #9C9C9C;font-family: Arial, Helvetica, sans-serif;font-size: 12px;} .dbresult{color: #A53B1D;font-family: Arial, Helvetica, sans-serif;font-size: 13px;font-weight: bold;} .label{color: #000000;font-family: Verdana, Helvetica, sans-serif;font-size: 12px;} .gvHeader{color: #ffffff;font-family: Verdana,Arial,sans-serif;font-weight: bold;}</style>');
////          
          myWindow.document.write(HTML);
          myWindow.document.close();
           myWindow.print();
         
            
        }
        
        function SetHiddenField()
        {
              
                var hdn= document.getElementById('ctl00_ContentPlaceHolder1_hdnEmail');
                var style= "<style> .subheading{color: #FFF;background-color: #0A73A2;font-size: 12px;font-weight: bold;height: 20px;text-align: left;padding-left: 5px;} .gridactiverow{background-color: #EFF9FF;height: 25px;color: #000000;font-family: Arial, Helvetica, sans-serif;font-size: 12px;} .griddeactiverow{background-image: url(../images/readmssage_bg.gif);background-repeat: repeat-x;height: 25px;color: #9C9C9C;font-family: Arial, Helvetica, sans-serif;font-size: 12px;} .dbresult{color: #A53B1D;font-family: Arial, Helvetica, sans-serif;font-size: 13px;font-weight: bold;} .label{color: #000000;font-family: Verdana, Helvetica, sans-serif;font-size: 12px;} .gvHeader{color: #ffffff;font-family: Verdana,Arial,sans-serif;font-weight: bold;}</style> ";
                hdn.value= "<div><img alt='Report' hspace=0 src='cid:companylogo' align=baseline border=0 ></div>" + document.getElementById("gridContainer").innerHTML + style ;
              
                
            
        }
    </script>

    <div style="width: 100%" align="center">
        <div style="padding-bottom: 5px; padding-top: 5px;">
            <table width="95%" style="border-right: black thin solid; border-top: black thin solid;
                border-left: black thin solid; border-bottom: black thin solid">
                <tr>
                    <td class="subheading" colspan="4">
                        Search&nbsp; Panel&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        Make</td>
                    <td>
                        <asp:DropDownList ID="ddlMake" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                            DataTextField="Make" DataValueField="ID" OnSelectedIndexChanged="ddlMake_SelectedIndexChanged"
                            ValidationGroup="VGSubmit" Width="261px">
                        </asp:DropDownList></td>
                    <td>
                        Model</td>
                    <td>
                        <asp:DropDownList ID="ddlModel" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                            DataTextField="Model" DataValueField="ID" OnSelectedIndexChanged="ddlModel_SelectedIndexChanged"
                            ValidationGroup="VGSubmit" Width="261px">
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td>
                        Series</td>
                    <td>
                        <asp:DropDownList ID="ddlSeries" runat="server" AppendDataBoundItems="True" DataTextField="Series"
                            DataValueField="ID" OnSelectedIndexChanged="ddlSeries_SelectedIndexChanged" ValidationGroup="VGSubmit"
                            Width="261px">
                        </asp:DropDownList></td>
                    <td>
                        State</td>
                    <td>
                        <asp:DropDownList ID="ddlState" runat="server" AppendDataBoundItems="True" DataTextField="Make"
                            DataValueField="ID" OnSelectedIndexChanged="ddlMake_SelectedIndexChanged" ValidationGroup="VGSubmit"
                            Width="261px">
                            <asp:ListItem Value="- Select -">- Select State -</asp:ListItem>
                            <asp:ListItem Value="ACT">Australian Capital Territory (ACT)</asp:ListItem>
                            <asp:ListItem Value="NSW">New South Wales (NSW)</asp:ListItem>
                            <asp:ListItem Value="NT">Northern Territory (NT)</asp:ListItem>
                            <asp:ListItem Value="QLD">Queensland (QLD)</asp:ListItem>
                            <asp:ListItem Value="SA">South Australia (SA)</asp:ListItem>
                            <asp:ListItem Value="TAS">Tasmania (TAS)</asp:ListItem>
                            <asp:ListItem Value="VIC">Victoria (VIC)</asp:ListItem>
                            <asp:ListItem Value="WA">Western Australia (WA)</asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td>
                        Start Date</td>
                    <td align="left">
                        <asp:TextBox ID="txtStartDate" runat="server">
                        </asp:TextBox>
                    </td>
                    <td>
                        End Date</td>
                    <td align="left">
                        <asp:TextBox ID="txtEndDate" runat="server">
                        </asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Group&nbsp; By</td>
                    <td align="left">
                        <asp:DropDownList ID="ddlGroupBy" runat="server" Width="147px">
                            <asp:ListItem>Dealer</asp:ListItem>
                            <asp:ListItem>Consultant</asp:ListItem>
                            <asp:ListItem>State</asp:ListItem>
                        </asp:DropDownList></td>
                    <td>
                        Quote Filter</td>
                    <td align="left">
                        <asp:DropDownList ID="ddlQuoteFilters" runat="server" Width="147px">
                            <asp:ListItem Value="-Select-">- Select -</asp:ListItem>
                            <asp:ListItem Value="AQR">All Quotes Returned</asp:ListItem>
                            <asp:ListItem Value="WQ">Winning Quotes</asp:ListItem>
                        </asp:DropDownList>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                        <asp:ImageButton ID="igmbtnSubmit" runat="server" ImageUrl="~/Images/Submit.gif"
                            OnClick="imgbtnAdd_Click" onmouseout="this.src='Images/Submit.gif'" onmouseover="this.src='Images/Submit_hvr.gif'"
                            ValidationGroup="VGSubmit" />
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Animated="true"
                            Format="MM/dd/yyyy" PopupButtonID="txtEndDate" TargetControlID="txtEndDate">
                        </ajaxToolkit:CalendarExtender>
                        <ajaxToolkit:CalendarExtender ID="CalExtEstimatedTimeOfdelivery" runat="server" Animated="true"
                            Format="MM/dd/yyyy" PopupButtonID="txtStartDate" TargetControlID="txtStartDate">
                        </ajaxToolkit:CalendarExtender>
                        <asp:RequiredFieldValidator ID="rfvQuoteFilter" runat="server" ControlToValidate="ddlQuoteFilters"
                            Display="None" ErrorMessage="Please select the quote filter criteria" InitialValue="-Select-"
                            SetFocusOnError="True" ValidationGroup="VGSubmit">
                        </asp:RequiredFieldValidator><ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1"
                            runat="server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="rfvQuoteFilter">
                        </ajaxToolkit:ValidatorCalloutExtender>
                    </td>
                </tr>
            </table>
        </div>
        <div id="gridContainer">
            <asp:GridView runat="Server" ID="gvCustom" AutoGenerateColumns="False" GridLines="Horizontal"
                ShowHeader="False" Width="95%" OnRowCommand="gvCustom_RowCommand" OnRowDataBound="gvCustom_RowDataBound">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <div class="demoarea">
                                <asp:Panel ID="Panel2" runat="server" CssClass="subheading">
                                    <div>
                                        <div style="float: left; padding-top: 1px">
                                            <asp:HiddenField ID="hdfFieldToQuery" runat="Server" Value='<%# Bind("FieldToQuery") %>'>
                                            </asp:HiddenField>
                                        </div>
                                        <div style="float: left; margin-left: 10px">
                                            <asp:Label ID="lblHeader" runat="server" Text='<%# Eval("HeaderField") %>'>
                                            </asp:Label>
                                            <asp:Label ID="Label1" runat="server">(Show Details...)</asp:Label>
                                        </div>
                                        <br />
                                        <%--<div style="float: right; vertical-align: middle;">
                                            <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/images/expand_blue.jpg" AlternateText="(Show Details...)" />
                                        </div>--%>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="Panel1" runat="server" CssClass="collapsePanel" ScrollBars="Auto"
                                    Height="0px" HorizontalAlign="Center">
                                    <br />
                                    <asp:GridView ID="gvDetails" runat="server" Width="95%" OnRowDataBound="gvDetails_RowDataBound"
                                        OnRowCommand="gvDetails_RowCommand" AutoGenerateColumns="False" HorizontalAlign="Center"
                                        EmptyDataText="No Quotations Found." DataKeyNames="ID,RequestID,OptionID" AllowPaging="false">
                                        <Columns>
                                            <asp:BoundField DataField="Make" HeaderText="Make"></asp:BoundField>
                                            <asp:BoundField DataField="Model" HeaderText="Model"></asp:BoundField>
                                            <asp:BoundField DataField="Series" HeaderText="Series"></asp:BoundField>
                                            <asp:BoundField DataField="Date" HeaderText="Date"></asp:BoundField>
                                            <asp:BoundField DataField="Dealer Name" HeaderText="Dealer Name"></asp:BoundField>
                                            <asp:BoundField DataField="State" HeaderText="State"></asp:BoundField>
                                            <asp:TemplateField HeaderText="View Details">
                                                <ItemTemplate>
                                                    <%--  <asp:LinkButton ID="lnkbtnViewDetails" runat="Server" __designer:wfdid="w8" CommandName="ViewDetails"
                                                        CommandArgument="<%# Container.DataItemIndex %>">View Details</asp:LinkButton>--%>
                                                    <asp:DataList ID="dtListQuoteValues" runat="server" ShowHeader="False" HorizontalAlign="Center"
                                                        ShowFooter="False" Width="100%" OnItemDataBound="dtListQuoteValues_ItemDataBound">
                                                        <ItemTemplate>
                                                            <div style="width: 100%">
                                                                <asp:HyperLink ID="lnkDetails" runat="server" Text='<%# Bind("QuoteValue") %>' href="javascript://" ></asp:HyperLink>
                                                                <asp:HiddenField ID="hdfRequestID" runat="server" Value='<%# Bind("RequestID") %>'></asp:HiddenField>
                                                                <asp:HiddenField ID="hdfOptionID" runat="server" Value='<%# Bind("OptionID") %>'></asp:HiddenField>
                                                                <asp:HiddenField ID="hdfQuotationID" runat="server" Value='<%# Bind("QuotationID") %>'>
                                                                </asp:HiddenField>
                                                            </div>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="gridactiverow" />
                                                    </asp:DataList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <RowStyle CssClass="gridactiverow"></RowStyle>
                                        <HeaderStyle CssClass="griddeactiverow"></HeaderStyle>
                                    </asp:GridView>
                                    <br />
                                </asp:Panel>
                            </div>
                            <ajaxToolkit:CollapsiblePanelExtender ID="cpeDemo" runat="Server" TargetControlID="Panel1"
                                SuppressPostBack="true" CollapsedText="(Show Details...)" ExpandedText="(Hide Details...)"
                                ImageControlID="Image1" TextLabelID="Label1" CollapseControlID="Panel2" ExpandControlID="Panel2">
                            </ajaxToolkit:CollapsiblePanelExtender>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <RowStyle CssClass="gridactiverow" />
            </asp:GridView>
        </div>
    </div>
    <asp:Button ID="btnExport" runat="server" OnClick="btnExport_Click" Text="btnExport" />
    <input id="Button1" type="button" value="Print" onclick="Print();" />
    <asp:Button ID="Email" runat="server" OnClick="Email_Click" Text="EMail" />
    <asp:HiddenField ID="hdnEmail" runat="server"></asp:HiddenField>
</asp:Content>
