<%@ Page Title="Stamp Duty, Luxury Car Tax and Invoice Amount Calculator" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="SDandLCTCalculator.aspx.cs" Inherits="SDandLCTCalculator" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <table align="center" width="98%" cellpadding="2" cellspacing="2" style="border: solid 1px #acacac;">
        <tr>
            <td>
                <asp:GridView ID="gvSDandLctCalc" runat="server" AutoGenerateColumns="False" CellPadding="3"
                    CellSpacing="1" Width="100%" DataKeyNames="ID" BorderColor="#ACACAC" OnRowDataBound="gvSDandLctCalc_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:Label ID="lblState" runat="server" Text='<%#Bind("State") %>'></asp:Label>
                                <asp:HiddenField ID="hdfStateID" runat="server" Value='<%#Bind("ID") %>' />
                            </ItemTemplate>
                            <ItemStyle Width="8%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Purchase Price">
                            <ItemTemplate>
                                $&nbsp;<asp:TextBox ID="txtPurchasePrice" Width="100" AutoPostBack="true" runat="server"
                                    OnTextChanged="txtPurchasePrice_TextChanged"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FTBE_txtPurchasePrice" runat="server" TargetControlID="txtPurchasePrice"
                                    FilterType="Numbers">
                                </ajaxToolkit:FilteredTextBoxExtender>
                                <asp:DropDownList ID="ddlCylinders" Width="62" AutoPostBack="true" runat="server"
                                    OnSelectedIndexChanged="ddlCylinders_SelectedIndexChanged" Visible="false">
                                    <asp:ListItem Value="0" Text="-Select Cylinder-"></asp:ListItem>
                                    <asp:ListItem Value="1.5" Text="4cl"></asp:ListItem>
                                    <asp:ListItem Value="1.75" Text="6cl"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="8cl"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Hybrid"></asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                            <ItemStyle Width="27%" VerticalAlign="Middle" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Stamp Duty Payable">
                            <ItemTemplate>
                                <asp:Label ID="lblStampDutyPayable" runat="server" Text="$"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="17%" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Luxury Car Tax">
                            <ItemTemplate>
                                <asp:Label ID="lblLuxuryCarTax" runat="server" Text="$"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="15%" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Difference between dealer price and our price">
                            <ItemTemplate>
                                $&nbsp;
                                <asp:TextBox ID="txtDifference" Width="100" runat="server" AutoPostBack="true" OnTextChanged="txtDifference_TextChanged"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FTBE_txtDifference" runat="server" TargetControlID="txtPurchasePrice"
                                    FilterType="Numbers">
                                </ajaxToolkit:FilteredTextBoxExtender>
                            </ItemTemplate>
                            <ItemStyle Width="20%" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amount to Invoice">
                            <ItemTemplate>
                                <asp:Label ID="lblAmtInvoice" runat="server" Text="$"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="18%" HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle CssClass="gvFooterrow" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <PagerStyle CssClass="pgr" />
                    <HeaderStyle BackColor="#0A73A2" Font-Bold="True" HorizontalAlign="Center" CssClass="gvHeader"
                        Height="30px" />
                </asp:GridView>
            </td>
        </tr>
        <tr style="height: 10px;">
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <b><span style='color: Red; font-size: 18px;'>*</span><b />&nbsp;
                    <asp:Label ID="lblStar1" runat="server" Font-Bold="false" Font-Size="12px" Text="Here stamp duty is charged on RRP of vehicle. Discounts, accessories, referral fees etc make no difference to stamp duty payable."></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <b><span style='color: Red; font-size: 18px;'>*</span><b />&nbsp;
                    <asp:Label ID="lblStar2" runat="server" Font-Bold="false" Font-Size="12px" Text="Note Calculator now reflects 33% LCT, the threshold is $61884"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <b><span style='color: Red; font-size: 18px;'>*</span><b />&nbsp;
                    <asp:Label ID="lblStar3" runat="server" Font-Bold="false" Font-Size="12px" Text="Fuel - efficient car limit for 2011-12 FY is $75375"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
