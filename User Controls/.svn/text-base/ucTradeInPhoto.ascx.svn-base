<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucTradeInPhoto.ascx.cs"
    Inherits="User_Controls_ucTradeInPhoto" %>
<asp:UpdatePanel ID="upnAttachFile" runat="server">
    <ContentTemplate>
        <asp:Panel ID="pnlUploadTradeInPhoto" runat="server">
            <table cellpadding="5" cellspacing="5" width="100%">
                <tr>
                    <td align="center">
                        <asp:Label ID="lblmsg" runat="server" Visible="false" ForeColor="Red" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <table cellpadding="5" cellspacing="5">
                            <tr>
                                <td>
                                    <span style="color: Red; font-weight: bold;">*</span>Select Photo :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPhotoName" runat="server" Visible="false"></asp:TextBox>
                                  <%--  <asp:RequiredFieldValidator ID="RFV_Name" runat="server" ControlToValidate="txtPhotoName"
                                        ErrorMessage="Please Select File" ValidationGroup="UploadPhoto" Display="None"> </asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="VCE_Name" TargetControlID="RFV_Name"
                                        HighlightCssClass="validatorCalloutHighlight" />--%>
                                </td>
                                <td>
                                    <asp:FileUpload ID="FUTradeIn" runat="server"></asp:FileUpload>
                                    <asp:RequiredFieldValidator ID="rfvFileUpload" runat="server" ControlToValidate="FUTradeIn"
                                        ErrorMessage="Please Select File" ValidationGroup="UploadPhoto" Display="None"> </asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="VCE_FileUpload" TargetControlID="rfvFileUpload"
                                        HighlightCssClass="validatorCalloutHighlight" PopupPosition="Left" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" align="right">
                                    <asp:ImageButton ID="imgbtnUploadPhoto" runat="server" OnClick="imgbtnUploadPhoto_Click"
                                        ImageUrl="~/Images/upload.png" onmouseout="this.src='Images/upload.png'" onmouseover="this.src='Images/upload_hvr.png'"
                                        ValidationGroup="UploadPhoto" CausesValidation="true" />
                                    <asp:ImageButton Visible="false" ID="imgbtnCancel" runat="server" OnClick="imgbtnCancel_Click"
                                        ImageUrl="~/Images/Cancel.gif" onmouseout="this.src='Images/Cancel.gif'" onmouseover="this.src='Images/Cancel_hvr.gif'" />
                                </td>
                            </tr>
                            <%--<tr>
                                <td colspan="3">
                                    <asp:GridView Visible="false" ID="gvTradeInPhoto" runat="server" AllowPaging="true"
                                        AllowSorting="false" PageSize="15" DataKeyNames="ID" AutoGenerateColumns="false"
                                        Width="100%" BackColor="White" ShowFooter="false" BorderColor="#CCCCCC" BorderStyle="None"
                                        BorderWidth="1px" CellPadding="3" EmptyDataText="No Photo Found" OnPageIndexChanging="gvTradeInPhoto_PageIndexChanging">
                                        <Columns>
                                            <asp:BoundField DataField="PhotoName" NullDisplayText="--" HeaderText="Name" SortExpression="LastName"
                                                ItemStyle-HorizontalAlign="Center" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkRemove" runat="server" CommandArgument='<% #Eval("ID") %>'
                                                        CommandName="removePhoto" Text="remove">
                                                    </asp:LinkButton>
                                                    <asp:HiddenField ID="hdfTradeInID" runat="server" Value='<% #Eval("TradeInID") %>' />
                                                    <asp:HiddenField ID="hdfPhotoPath" runat="server" Value='<% #Eval("PhotoPath") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle CssClass="pgr" />
                                        <HeaderStyle BackColor="#0A73A2" Font-Bold="True" CssClass="gvHeader" Height="30px" />
                                    </asp:GridView>
                                </td>
                            </tr>--%>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="imgbtnUploadPhoto" />
    </Triggers>
</asp:UpdatePanel>
