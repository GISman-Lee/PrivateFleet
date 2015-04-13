<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCSeriesAccessories.ascx.cs"
    Inherits="User_Controls_UCSeriesAccessories" %>
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
                <td colspan="2" style="height: 30px" valign="middle" align="center">
                    <asp:Label ID="lblResult" runat="server" CssClass="dbresult"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="padding-left: 150px; width: 92px">
                    <asp:Label ID="Label6" runat="server" CssClass="label">Make :</asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlMake" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                        DataTextField="Make" DataValueField="ID" OnSelectedIndexChanged="ddlMake_SelectedIndexChanged"
                        Width="261px" ValidationGroup="VGSubmit">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="padding-left: 150px; width: 92px;">
                    <asp:Label ID="Label5" runat="server" CssClass="label">Model :</asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlModel" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                        DataTextField="Model" DataValueField="ID" OnSelectedIndexChanged="ddlModel_SelectedIndexChanged"
                        Width="261px" ValidationGroup="VGSubmit">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td style="padding-left: 150px; width: 92px">
                    <asp:Label ID="Label2" runat="server" CssClass="label">Series :</asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlSeries" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                        DataTextField="Series" DataValueField="ID" OnSelectedIndexChanged="ddlSeries_SelectedIndexChanged"
                        Width="261px" ValidationGroup="VGSubmit">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td style="padding-left: 150px; width: 92px">
                    <asp:Label ID="Label3" runat="server" CssClass="label">Accessory :</asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlAccessory" runat="server" AppendDataBoundItems="True" DataTextField="Accessory"
                        DataValueField="ID" Width="261px">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td style="padding-left: 150px; width: 92px; vertical-align: top;">
                    <asp:Label ID="Label4" runat="server" CssClass="label">Specification :</asp:Label></td>
                <td>
                    <asp:TextBox ID="txtSpecification" runat="server" TextMode="MultiLine" Rows="3" Width="300px"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 92px;">
                </td>
                <td>
                    &nbsp;
                    <asp:ImageButton ID="imgbtnAdd" runat="server" ImageUrl="~/Images/Submit.gif" OnClick="imgbtnAdd_Click"
                        onmouseout="this.src='Images/Submit.gif'" onmouseover="this.src='Images/Submit_hvr.gif'" ValidationGroup="VGSubmit" />
                    <asp:ImageButton ID="imgbtnCancel" runat="server" ImageUrl="~/Images/Cancel.gif"
                        OnClick="imgbtnCancel_Click" onmouseout="this.src='Images/Cancel.gif'" onmouseover="this.src='Images/Cancel_hvr.gif'" /></td>
            </tr>
            <tr>
                <td style="width: 92px">
                    <ajaxToolkit:ValidatorCalloutExtender ID="vcextrfvModel" runat="server"
                        HighlightCssClass="validatorCalloutHighlight" TargetControlID="rfvModel">
                    </ajaxToolkit:ValidatorCalloutExtender>
                    <ajaxToolkit:ValidatorCalloutExtender ID="vcextrfvmake" runat="server"
                        HighlightCssClass="validatorCalloutHighlight" TargetControlID="rfvMake">
                    </ajaxToolkit:ValidatorCalloutExtender>
                    <ajaxToolkit:ValidatorCalloutExtender ID="vcextrfvSeries" runat="server"
                        HighlightCssClass="validatorCalloutHighlight" TargetControlID="SeriesReq">
                    </ajaxToolkit:ValidatorCalloutExtender>
                    <ajaxToolkit:ValidatorCalloutExtender ID="vcextrfvAccessory" runat="server"
                        HighlightCssClass="validatorCalloutHighlight" TargetControlID="AccessoryReq">
                    </ajaxToolkit:ValidatorCalloutExtender>
                </td>
                <td>
                <asp:RequiredFieldValidator ID="rfvModel" runat="server" ControlToValidate="ddlModel"
                        Display="None" ErrorMessage="Please select the Model" InitialValue="-Select-"
                        SetFocusOnError="True" ValidationGroup="VGSubmit"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="AccessoryReq" runat="server" ControlToValidate="ddlAccessory"
                        Display="None" ErrorMessage="Please select the Accessory." InitialValue="-Select-"
                        ValidationGroup="VGSubmit" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="SeriesReq" runat="server" ControlToValidate="ddlSeries"
                        Display="None" ErrorMessage="Please select the Series." InitialValue="-Select-"
                        ValidationGroup="VGSubmit" SetFocusOnError="True"></asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="rfvMake" runat="server" ControlToValidate="ddlMake"
                        Display="None" ErrorMessage="Please select the Make." InitialValue="-Select-"
                        SetFocusOnError="True" ValidationGroup="VGSubmit"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="gvSeriesAccessoriesDetails" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        OnPageIndexChanging="gvSeriesAccessoriesDetails_PageIndexChanging" OnRowCommand="gvSeriesAccessoriesDetails_RowCommand"
                        OnRowDataBound="gvSeriesAccessoriesDetails_RowDataBound" OnRowEditing="gvSeriesAccessoriesDetails_RowEditing"
                        OnSelectedIndexChanging="gvSeriesAccessoriesDetails_SelectedIndexChanging" Width="100%"
                        DataKeyNames="ID" OnRowUpdating="gvSeriesAccessoriesDetails_RowUpdating" BackColor="White"
                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                        <Columns>
                            <asp:TemplateField HeaderText="Accessory">
                                <EditItemTemplate>
                                    &nbsp;
                                    <asp:Label ID="lblEditDealer" runat="server" Text='<%# Bind("Accessory") %>'></asp:Label>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    &nbsp;<br />
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Image ID="imgActive" runat="server" Height="10px" ImageUrl="~/Images/active_bullate.jpg"
                                        Width="10px" />
                                    <asp:Label ID="lblDealer" runat="server" Text='<%# Bind("Accessory") %>' Style="padding-left: 10px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="20%" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Specification">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEditSpecification" runat="server" Text='<%# Bind("Specification") %>'
                                        Height="41px" TextMode="MultiLine" Width="227px"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Specification") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="40%" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Active">
                                <ItemTemplate>
                                    <asp:Image ID="imgbtnActivate" runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <EditItemTemplate>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkbtnActiveness" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                        CommandName="Activeness" Style="padding-left: 10px" CssClass="activatelink">Activate</asp:LinkButton>&nbsp;
                                    <asp:HiddenField ID="hdfIsActive" runat="server" Value='<%# Bind("IsActive") %>' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="20%" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <EditItemTemplate>
                                    <asp:ImageButton ID="imgbtnUpdate" runat="server" CommandName="Update" ImageUrl="~/Images/update.png"
                                        OnClientClick="" ToolTip="Update This Record" ValidationGroup="EditGroup" />
                                    <asp:ImageButton ID="imgbtnCancel" runat="server" CausesValidation="False" CommandName="Update"
                                        ImageUrl="~/Images/cancel.png" OnClick="imgbtnCancel_Click" ToolTip="Cancel" />
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="20%" />
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
                    <asp:HiddenField ID="hdfID" runat="server" Value='<%# Bind("ID") %>' />
                    &nbsp;&nbsp;
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
