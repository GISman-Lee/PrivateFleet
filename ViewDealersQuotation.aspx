<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ViewDealersQuotation.aspx.cs" Inherits="ViewDealersQuotation" Title="View Quotations" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script>
        function test(id) {
            alert("");

            dt = new Date()
            dt.setDate = (document.getElementById("ctl00_ContentPlaceHolder1_txtCalenderFrom").value);
            //alert(dt.getDate());
            // alert(dt.getMonth());
            // alert(dt.getFullYear());
            // alert(dt);
            dt1 = dt.getDate() + '/' + dt.getMonth() + '/' + dt.getFullYear();
            if (dt1 != (document.getElementById("ctl00_ContentPlaceHolder1_txtCalenderFrom").value)) {
                alert("In vailid date");
            }


            if (Object.prototype.toString.call(dt) === "[object Date]") {
                // it is a date
                if (isNaN(dt.getTime())) {  // d.valueOf() could also work
                    // date is not valid
                    alert("date is invalid1");
                }
                else {     // date is valid
                    alert("date is valid");
                }

            } else {
                // not a date
                alert("date is invalid 3");

            }

        }

     

   
    </script>

    <asp:Panel ID="panDeler" runat="server">
        <table align="center" width="100%">
            <tr>
                <td colspan="2" align="center">
                    <asp:Label ID="lblResult" runat="server" CssClass="dbresult" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <span style="color: Red">*</span> From Date
                    <asp:TextBox ID="txtCalenderFrom" runat="server" Width="119px" MaxLength="10"></asp:TextBox>
                    <asp:Image ID="imgCal" runat="server" ImageUrl="~/Images/Calendar.gif" />
                    <ajaxToolkit:CalendarExtender ID="calFrom" runat="server" TargetControlID="txtCalenderFrom"
                        Format="dd/MM/yyyy" PopupButtonID="imgCal">
                    </ajaxToolkit:CalendarExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCalenderFrom"
                        CssClass="gvValidationError" Display="None" ErrorMessage="Please Enter the from Date"
                        SetFocusOnError="True" ValidationGroup="VGSubmit"> </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator runat="server" ID="ss" Display="None" ControlToValidate="txtCalenderFrom"
                        ErrorMessage="Enter proper date in dd/mm/yyyy format." ValidationGroup="VGSubmit"
                        SetFocusOnError="False" ValidationExpression='^(((0?[1-9]|[12]\d|3[01])\/(0?[13578]|1[02])\/((1[6-9]|[2-9]\d)\d{2}))|((0?[1-9]|[12]\d|30)\/(0?[13456789]|1[012])\/((1[6-9]|[2-9]\d)\d{2}))|((0?[1-9]|1\d|2[0-8])\/0?2\/((1[6-9]|[2-9]\d)\d{2}))|(29\/0?2\/((1[6-9]|[2-9]\d)(0?[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$'></asp:RegularExpressionValidator>
                    <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender2"
                        TargetControlID="ss" HighlightCssClass="validatorCalloutHighlight" />
                    <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender2_ViewDealersQuotation"
                        TargetControlID="RequiredFieldValidator1" HighlightCssClass="validatorCalloutHighlight" />
                </td>
                <td align="left">
                    <span style="color: Red">*</span> To Date
                    <asp:TextBox ID="TxtToDate" runat="server" Width="119px" MaxLength="10"></asp:TextBox>
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Calendar.gif" />
                    <ajaxToolkit:CalendarExtender ID="calTO" runat="server" TargetControlID="TxtToDate"
                        Format="dd/MM/yyyy" PopupButtonID="Image1">
                    </ajaxToolkit:CalendarExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtToDate"
                        CssClass="gvValidationError" Display="None" ErrorMessage="Please Enter the to Date"
                        SetFocusOnError="True" ValidationGroup="VGSubmit">
                        
                    
                    </asp:RequiredFieldValidator><ajaxToolkit:ValidatorCalloutExtender runat="server"
                        ID="ValidatorCalloutExtender1" TargetControlID="RequiredFieldValidator2" HighlightCssClass="validatorCalloutHighlight" />
                    <%-- <asp:CompareValidator runat ="server" ID ="CompareValidator1" Display ="None"   ControlToValidate ="TxtToDate" Type ="Date" Operator ="DataTypeCheck"  SetFocusOnError ="true"  ValidationGroup="VGSubmit"  ErrorMessage ="Enter proper date"></asp:CompareValidator>  
                      
                        <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender3"
                        TargetControlID="CompareValidator1" HighlightCssClass="validatorCalloutHighlight" />--%>
                    <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1" Display="None"
                        ControlToValidate="TxtToDate" ErrorMessage="Enter proper date in dd/mm/yyyy format."
                        ValidationGroup="VGSubmit" SetFocusOnError="False" ValidationExpression='^(((0?[1-9]|[12]\d|3[01])\/(0?[13578]|1[02])\/((1[6-9]|[2-9]\d)\d{2}))|((0?[1-9]|[12]\d|30)\/(0?[13456789]|1[012])\/((1[6-9]|[2-9]\d)\d{2}))|((0?[1-9]|1\d|2[0-8])\/0?2\/((1[6-9]|[2-9]\d)\d{2}))|(29\/0?2\/((1[6-9]|[2-9]\d)(0?[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$'></asp:RegularExpressionValidator>
                    <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender3"
                        TargetControlID="RegularExpressionValidator1" HighlightCssClass="validatorCalloutHighlight" />
                    <asp:CustomValidator runat="server" ID="cust1" ClientValidationFunction="compaire_dates_ViewDealersQuotation"
                        ValidationGroup="VGSubmit" ErrorMessage="From date can not be greater than to date."
                        Display="None" ControlToValidate="txtCalenderFrom"> </asp:CustomValidator>
                    <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="validatorcallourdatecomparision"
                        TargetControlID="cust1" HighlightCssClass="validatorCalloutHighlight">
                    </ajaxToolkit:ValidatorCalloutExtender>
                    &nbsp;&nbsp;
                    <asp:Label ID="lblMake" runat="server" Text="Make"></asp:Label>
                    <asp:DropDownList ID="ddlMake" Width="142" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right" colspan="1">
                </td>
                <td align="left" style="padding-top: 10px;" colspan="1">
                    <asp:ImageButton ID="btnGenerateReport" runat="server" ImageUrl="~/Images/Generate_report_hvr.gif"
                        onmouseout="this.src='Images/Generate_report_hvr.gif'" onmouseover="this.src='Images/Generate_report.gif'"
                        ValidationGroup="VGSubmit" OnClick="btnGenerateReport_Click" />
                    <asp:ImageButton ID="btnCancel" runat="server" ImageUrl="~/Images/Cancel.gif" onmouseout="this.src='Images/Cancel.gif'"
                        onmouseover="this.src='Images/Cancel_hvr.gif'" ValidationGroup="VGMakeModelSeries"
                        OnClick="btnCancel_Click" />
                </td>
            </tr>
            <tr>
                <td align="right" colspan="2">
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
                <td colspan="2">
                    <asp:GridView ID="gvMakeDetails" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                        CellPadding="3" DataKeyNames="ID" OnPageIndexChanging="gvMakeDetails_PageIndexChanging"
                        OnRowCommand="gvMakeDetails_RowCommand" OnRowDataBound="gvMakeDetails_RowDataBound"
                        OnRowEditing="gvMakeDetails_RowEditing" OnRowUpdating="gvMakeDetails_RowUpdating"
                        Width="100%" PageSize="15" AllowSorting="true" OnSorting="gv_Sorting" EmptyDataText="No Record to display"
                        OnRowDeleting="gvMakeDetails_RowDeleting">
                        <FooterStyle CssClass="gvFooterrow" />
                        <Columns>
                            <asp:TemplateField HeaderText="Created Date" SortExpression="CreatedDate">
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <table border="0">
                                        <tr>
                                            <td valign="top" style="padding-top: 5px; width: 15px;">
                                                <asp:Image ID="Image1" runat="server" Height="10px" ImageUrl="~/Images/active_bullate.jpg"
                                                    Width="10px" />
                                            </td>
                                            <td valign="top">
                                                <asp:Label ID="lblCreatedDate" runat="server" Style="padding-left: 10px" Text='<%# Bind("CreatedDate") %>'></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="hdfID" runat="server" Value='<%# Bind("ID") %>' />
                                    <asp:HiddenField ID="hdfRequestID" runat="server" Value='<%# Bind("RequestID") %>' />
                                    <asp:HiddenField ID="hdfDID" runat="server" Value='<%# Bind("DealerID") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- <asp:TemplateField HeaderText="Dealer Notes">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblDealerNotes" runat="server" Text='<%# Bind("DealerNotes") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Make" SortExpression="Make">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblMake" runat="server" Text='<%# Bind("Make") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Model" SortExpression="Model">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblModele" runat="server" Text='<%# Bind("Model") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Series" SortExpression="Series">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblSeries" runat="server" Text='<%# Bind("Series") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Estimated Delivery Date" SortExpression="EstimatedDeleveryDates">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblEstimatedDeliveryDate" runat="server" Text='<%# Bind("EstimatedDeleveryDates") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Completed Date" SortExpression="CreatedDate1">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblmxDate" runat="server" Text='<%# Bind("CompleteDate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- by manoj on 23 mar 2011 --%>
                            <asp:TemplateField HeaderText="On Road Price<br/>Option I/II" SortExpression="OnRoadPrice">
                                <HeaderStyle HorizontalAlign="Center" Width="200px" />
                                <ItemStyle />
                                <ItemTemplate>
                                    <asp:Label ID="lblOnRoadPrice" runat="server" Text='<%# Bind("OnRoadPrice") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Consultant" SortExpression="Consultant">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblConsultant" runat="server" Text='<%# Bind("Consultant") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- <asp:TemplateField HeaderText="ExStock/<br/>Order">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblExStock" runat="server" Text='<%# Bind("ExStock") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <%--<asp:TemplateField HeaderText="Order">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblOrder" runat="server" Text='<%# Bind("Order") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDetails" runat="server" CommandName="ViewDetails" CssClass="activeLink"
                                        Text="View Details"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Width="90px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status" SortExpression="Result" Visible="false">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" Width="120px" CssClass="activeLink" runat="server" Text='<%# Bind("Result") %>'>
                                    </asp:Label>
                                    <asp:HiddenField ID="hdfQRStatus" runat="server" Value='<%#Eval("IsQRCancel") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--  <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" CssClass="activeLink"
                                        Text="Delete Quotation"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Width="90px" />
                            </asp:TemplateField>--%>
                        </Columns>
                        <RowStyle CssClass="gridactiverow" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <PagerStyle CssClass="pgr" />
                        <HeaderStyle BackColor="#0A73A2" CssClass="gvHeader" Font-Bold="True" Height="30px" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
