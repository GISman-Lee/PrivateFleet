<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCSeries.ascx.cs" Inherits="User_Controls_UCSeries" %>
<link href="../CSS/stylesheet.css" rel="stylesheet" type="text/css" />

<script type="text/javascript">
//        function ConfirmUpdate()
//        {
//           var Choice =confirm('Do you want to update this entry ?');
//           return Choice;
//        }
</script>

<asp:UpdatePanel ID="UP2" runat="server">
    <ContentTemplate>
        <table width="95%" align="center">
            <tr>
                <td style="height: 30px" valign="middle">
                    <asp:Label ID="lblResult" runat="server" CssClass="dbresult"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Search Series &nbsp;
                    <asp:DropDownList ID="ddlSearchMakes" runat="server" Width="174px" AppendDataBoundItems="True" AutoPostBack="true"
                        DataTextField="Make" DataValueField="ID" OnSelectedIndexChanged="ddlSearchMakes_SelectedIndexChanged">
                    </asp:DropDownList>
                    &nbsp;
                    <asp:DropDownList ID="ddlSearchModel" runat="server" Width="174px" AppendDataBoundItems="True"
                        DataTextField="Model" DataValueField="ID">
                    </asp:DropDownList>
                    &nbsp;
                    <asp:TextBox ID="txtSearchSeries" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:ImageButton ID="imgbtnSearch" runat="server" ImageUrl="~/Images/Search_series_hvr.gif"
                        onmouseout="this.src='Images/Search_series_hvr.gif'" onmouseover="this.src='Images/Search_series.gif'"
                         OnClick="imgbtnSearch_Click"/>
                    &nbsp;
                    <asp:ImageButton ID="imgbtnSearchCancel" runat="server" ImageUrl="~/Images/Cancel.gif"
                        onmouseout="this.src='Images/Cancel.gif'" onmouseover="this.src='Images/Cancel_hvr.gif'"
                        CausesValidation="False" />
                </td>
            </tr>
            <tr>
                <td style="width: 100px">
                    <asp:GridView ID="gvSeriesDetails" runat="server" AutoGenerateColumns="False" OnRowCommand="gvSeriesDetails_RowCommand"
                        OnRowDataBound="gvSeriesDetails_RowDataBound" OnRowEditing="gvSeriesDetails_RowEditing"
                        OnRowUpdating="gvSeriesDetails_RowUpdating" ShowFooter="True" Width="100%" AllowPaging="True"
                        BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                        CellPadding="3" OnPageIndexChanging="gvSeriesDetails_PageIndexChanging" AllowSorting="true"
                        OnSorting="gv_Sorting" DataKeyNames="ID">
                        <Columns>
                            <asp:TemplateField HeaderText="Make" SortExpression="Make">
                                <EditItemTemplate>
                                    <asp:HiddenField ID="hdfMakeID" runat="server" Value='<%# Bind("MakeID") %>' />
                                    <asp:DropDownList ID="ddlEditMakes" runat="server" Width="174px" DataTextField="Make"
                                        DataValueField="ID" AutoPostBack="True" OnSelectedIndexChanged="ddlEditMakes_SelectedIndexChanged"
                                        ValidationGroup="EditMakeSelect">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlEditMakes"
                                        Display="None" ErrorMessage="Please Select Make" InitialValue="-Select-" ValidationGroup="EditGroup"
                                        CssClass="gvValidationError" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender111" runat="server"
                                        HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator1">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <asp:RequiredFieldValidator ID="reqEditMakeRequired" runat="server" ControlToValidate="ddlEditMakes"
                                        CssClass="gvValidationError" Display="None" ErrorMessage="Please Select Make"
                                        InitialValue="-Select-" SetFocusOnError="True" ValidationGroup="EditMakeSelect"></asp:RequiredFieldValidator><ajaxToolkit:ValidatorCalloutExtender
                                            ID="ValidatorCalloutExtender1" runat="server" HighlightCssClass="validatorCalloutHighlight"
                                            TargetControlID="reqEditMakeRequired">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <table>
                                        <tr>
                                            <td align="left" valign="middle">
                                                <asp:Image ID="imgActive" runat="server" Height="25px" ImageUrl="~/Images/footerarrow.png"
                                                    Width="25px" />
                                            </td>
                                            <td align="left" valign="middle">
                                                <asp:DropDownList ID="ddlMakes" runat="server" Width="174px" AppendDataBoundItems="True"
                                                    DataTextField="Make" DataValueField="ID" ValidationGroup="VGAdd" OnSelectedIndexChanged="ddlMakes_SelectedIndexChanged"
                                                    AutoPostBack="True">
                                                </asp:DropDownList>
                                                <br />
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:RequiredFieldValidator ID="MakeReq" runat="server" ControlToValidate="ddlMakes"
                                        Display="None" ErrorMessage="Please Select Make" InitialValue="-Select-" ValidationGroup="VGAdd"
                                        Style="padding-left: 35px" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender112" runat="server"
                                        HighlightCssClass="validatorCalloutHighlight" TargetControlID="MakeReq">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Image ID="imgActive" runat="server" Height="10px" ImageUrl="~/Images/active_bullate.jpg"
                                        Width="10px" />
                                    <asp:Label ID="lblMake" runat="server" Text='<%# Bind("Make") %>' Style="padding-left: 10px"></asp:Label>&nbsp;
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Model" SortExpression="Model">
                                <EditItemTemplate>
                                    <asp:HiddenField ID="hdfModelID" runat="server" Value='<%# Bind("ModelID") %>' />
                                    <asp:DropDownList ID="ddlEditModels" runat="server" DataTextField="Model" DataValueField="ID"
                                        Width="174px">
                                    </asp:DropDownList>
                                    <br />
                                    <asp:RequiredFieldValidator ID="EditModelReq" runat="server" ControlToValidate="ddlEditModels"
                                        Display="None" ErrorMessage="Please Select Model" InitialValue="-Select-" ValidationGroup="EditGroup"
                                        CssClass="gvValidationError" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender113" runat="server"
                                        HighlightCssClass="validatorCalloutHighlight" TargetControlID="EditModelReq">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddlModels" runat="server" AppendDataBoundItems="True" DataTextField="Model"
                                        DataValueField="ID" Width="174px">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="ModelReq" runat="server" ControlToValidate="ddlModels"
                                        Display="None" ErrorMessage="Please Select Model" InitialValue="-Select-" ValidationGroup="VGAdd"
                                        CssClass="gvValidationError" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender113" runat="server"
                                        HighlightCssClass="validatorCalloutHighlight" TargetControlID="ModelReq">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblModel" runat="server" Text='<%# Bind("Model") %>' Style="padding-left: 10px"></asp:Label>&nbsp;
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Series" SortExpression="Series">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEditSeries" runat="server" Text='<%# Bind("Series") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="SeriesEditReq" runat="server" ControlToValidate="txtEditSeries"
                                        Display="None" ErrorMessage="Please Enter Series" ValidationGroup="EditGroup"
                                        CssClass="gvValidationError" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender114" runat="server"
                                        HighlightCssClass="validatorCalloutHighlight" TargetControlID="SeriesEditReq">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtSeries" runat="server"></asp:TextBox><br />
                                    <asp:RequiredFieldValidator ID="SeriesAddReq" runat="server" ControlToValidate="txtSeries"
                                        Display="None" ErrorMessage="Please Enter Series" ValidationGroup="VGAdd" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender115" runat="server"
                                        HighlightCssClass="validatorCalloutHighlight" TargetControlID="SeriesAddReq">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblSeries" runat="server" Text='<%# Bind("Series") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Active">
                                <ItemTemplate>
                                    <asp:Image ID="imgbtnActivate" runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <EditItemTemplate>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <table>
                                        <tr>
                                            <td align="left" valign="middle">
                                                <asp:ImageButton ID="imgbtnAdd" runat="server" ImageUrl="~/Images/Save1.png" OnClick="imgbtnAdd_Click"
                                                    ToolTip="Add Record" ValidationGroup="VGAdd" Style="padding-left: 10px" Height="15px"
                                                    ImageAlign="Middle" Width="48px" />
                                            </td>
                                        </tr>
                                    </table>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkbtnActiveness" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                        CommandName="Activeness" Style="padding-left: 10px" CssClass="activatelink">Activate</asp:LinkButton>&nbsp;
                                    <asp:HiddenField ID="hdfIsActive" runat="server" Value='<%# Bind("IsActive") %>' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <EditItemTemplate>
                                    <asp:ImageButton ID="imgbtnUpdate" runat="server" CommandName="Update" ImageUrl="~/Images/update.png"
                                        OnClientClick="" ToolTip="Update This Record" ValidationGroup="EditGroup" />
                                    <asp:ImageButton ID="imgbtnCancel" runat="server" CausesValidation="False" CommandName="Update"
                                        ImageUrl="~/Images/cancel.png" OnClick="imgbtnCancel_Click" ToolTip="Cancel" />
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnEdit" runat="server" CausesValidation="False" CommandName="Edit"
                                        ImageUrl="~/Images/edit.png" ToolTip="Edit This Record" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle CssClass="gvFooterrow" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <PagerStyle CssClass="pgr" />
                        <HeaderStyle BackColor="#0A73A2" Font-Bold="True" CssClass="gvHeader" Height="30px" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress ID="upProcess" runat="server" DisplayAfter="500">
    <ProgressTemplate>
        <div id="progressBackgroundFilter">
        </div>
        <div id="processMessage">
            <span style="text-align: center;">
                <img src="../images/loading.gif" /><br />
                Loading...Please wait...</span></div>
    </ProgressTemplate>
</asp:UpdateProgress>
