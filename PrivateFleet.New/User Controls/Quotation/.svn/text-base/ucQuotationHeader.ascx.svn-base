<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucQuotationHeader.ascx.cs"
    Inherits="User_Controls_Quotation_ucQuotationHeader" %>
<asp:DataList ID="DataList1" runat="server" GridLines="Both" Width="100%" RepeatDirection="Horizontal"
    ShowFooter="False" ShowHeader="False">
    <ItemTemplate>
        <table width="100%">
            <tr>
                <td>
                    <table width="100%">
                        <tr class="ucHeader">
                            <td style="width: 30%;" bgcolor="">
                                Dealer Name
                            </td>
                            <td style="width: 40%" bgcolor="">
                                Email
                            </td>
                            <td style="width: 30%" bgcolor="">
                                Phone no./ Mobile
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text='<%#Bind("DealerName") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label5" runat="server" Text='<%#Bind("Email") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label6" runat="server" Text='<%#Bind("Phone") %>'></asp:Label>
                                /
                                <asp:Label ID="lblMob" runat="server" Text='<%#Bind("Mobile") %>'></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%">
                        <tr class="ucHeader">
                            <td style="width: 30%" bgcolor="">
                                Consultant Name
                            </td>
                            <td bgcolor="" style="width: 40%">
                                Consultant Notes
                            </td>
                            <td style="width: 30%">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblConsultant" runat="server" Text='<%#Bind("Name") %>'></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:Label ID="Label2" runat="server" Text='<%#Bind("ConsultantNotes") %>'></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%">
                        <tr class="ucHeader">
                            <!-- <td style="width: 35%" bgcolor="#eaeaea">
                                Dealer Name
                            </td>  -->
                            <td style="width: 30%" bgcolor="">
                                Estimated Delivery Date
                            </td>
                            <td style="width: 40%" bgcolor="">
                                Quotation Submitted Date
                            </td>
                            <td style="width: 30%" bgcolor="">
                                Compliance / Build Date
                            </td>
                        </tr>
                        <tr>
                            <!-- <td>
                                <asp:Label ID="lblDealer" runat="server" Text='<%#Bind("DealerName") %>'></asp:Label>
                            </td> -->
                            <td>
                                <asp:Label ID="lblEstimated" runat="server" Text='<%#Bind("EstimatedDeliveryDate") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblSent" runat="server" Text='<%#Bind("Date") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label8" runat="server" Text='<%#Bind("ComplianceDate") %>'></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%">
                        <tr class="ucHeader">
                            <%-- <td style="width: 35%" bgcolor="">
                                Ex Stock
                            </td>
                            <td style="width: 35%" bgcolor="">
                                Order
                            </td>--%>
                            <!-- <td style="width: 30%" bgcolor="#eaeaea">
                                Compliance Date
                            </td>  -->
                            <td colspan="3" style="width: 30%" bgcolor="">
                                Dealer Notes
                            </td>
                        </tr>
                        <tr>
                            <%-- <td>
                                <asp:Label ID="lblStock" runat="server" Text='<%#Bind("ExStock") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblOrder" runat="server" Text='<%#Bind("Order") %>'></asp:Label>
                            </td>--%>
                            <!--  <td>
                                <asp:Label ID="lblCompliance" runat="server" Text='<%#Bind("ComplianceDate") %>'></asp:Label>
                            </td>-->
                            <td colspan="3">
                                <asp:Label ID="lblDNotes" runat="server" Text='<%#Bind("DealerNotes") %>'></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <!--  <tr>
                <td>
                    <table width="100%">
                       <tr>
                          <td style="width: 30%" bgcolor="#eaeaea">
                                Build Date
                            </td> 
                            <td bgcolor="#eaeaea" colspan="3">
                                Dealer Notes
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text='<%#Bind("BuilDate") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text='<%#Bind("DealerNotes") %>'></asp:Label>
                            </td> 
                        </tr> 
                    </table>
                </td>
            </tr>-->
        </table>
    </ItemTemplate>
</asp:DataList>