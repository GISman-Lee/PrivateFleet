<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ClientDealerContract.aspx.cs" Inherits="ClientDealerContract" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="~/CSS/stylesheet.css" rel="stylesheet" type="text/css" />
    <asp:Panel ID="CDContractPanel" runat="server">
        <table style="padding: 20px">
            <tr>
                <td style="width: 108px">
                    <asp:Label ID="Label1" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Customer :</asp:Label>
                </td>
                <td style="width: 100px">
                    <asp:TextBox ID="txtCustomerName" runat="server" Width="217px" CssClass="gvtextbox"></asp:TextBox>
                </td>
                <td style="width: 108px; padding-left: 20px;">
                    <asp:Label ID="Label2" runat="server" CssClass="label"><span style="color:black"> </span>&nbsp;&nbsp;Email :</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="gvtextbox" Width="217px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 108px; padding-left: 20px;">
                    <asp:Label ID="Label4" runat="server" CssClass="label"><span style="color:black"> </span>&nbsp;&nbsp;Company :</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCompany" runat="server" CssClass="gvtextbox" Width="217px"></asp:TextBox>
                </td>
                <td style="width: 108px">
                    <asp:Label ID="Label11" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Fax :</asp:Label>
                </td>
                <td style="width: 100px">
                    <asp:TextBox ID="txtFax" runat="server" Width="217px" CssClass="gvtextbox"></asp:TextBox>
                </td>
                
            </tr>
            <tr>
                <td style="width: 108px; padding-left: 20px;">
                    <asp:Label ID="Label9" runat="server" CssClass="label"><span style="color:black"> </span>&nbsp;&nbsp;City :</asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlCity" runat="server" style="width:100px" DataTextField="City" DataValueField="City">
                    </asp:DropDownList>
                </td>
                <td style="width: 108px">
                    <asp:Label ID="Label3" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Address :</asp:Label>
                </td>
                <td style="width: 100px">
                    <asp:TextBox ID="txtAddress" runat="server" Width="217px" CssClass="gvtextbox"></asp:TextBox>
                </td>     
            </tr>
            <tr>
                <td style="width: 108px">
                    <asp:Label ID="Label10" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;PostCode :</asp:Label>
                </td>
                <td style="width: 100px">
                    <asp:TextBox ID="txtPostCode" runat="server" Width="217px" CssClass="gvtextbox"></asp:TextBox>
                </td>
                <td style="width: 108px">
                    <asp:Label ID="Label12" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;State :</asp:Label>
                </td>
                <td style="width: 100px">
                    <asp:DropDownList ID="ddlState" runat="server" style="width:100px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 108px">
                    <asp:Label ID="Label5" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Mobile :</asp:Label>
                </td>
                <td style="width: 100px">
                    <asp:TextBox ID="txtMobile" runat="server" Width="217px" CssClass="gvtextbox"></asp:TextBox>
                </td>
                <td style="width: 108px; padding-left: 20px;">
                    <asp:Label ID="Label6" runat="server" CssClass="label"><span style="color:black"> </span>&nbsp;&nbsp;Phone :</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPhone" runat="server" CssClass="gvtextbox" Width="217px"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td style="width: 108px">
                    <asp:Label ID="Label21" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Consultant :</asp:Label>
                </td>
                <td style="width: 100px">
                    <asp:TextBox ID="txtConsultant" runat="server" Width="217px" CssClass="gvtextbox"></asp:TextBox>
                </td>
                <td style="width: 108px">
                    <asp:Label ID="Label24" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Registration :</asp:Label>
                </td>
                <td style="width: 100px">
                    <asp:DropDownList ID="ddlRegistration" runat="server" style="width:100px">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>

        <table style="padding: 20px">
            <tr>
                <td style="width: 108px">
                    <asp:Label ID="Label13" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Vehicle Year :</asp:Label>
                </td>
                <td style="width: 100px">
                    <asp:TextBox ID="txtVehicleYear" runat="server" Width="217px" CssClass="gvtextbox"></asp:TextBox>
                </td>
                <td style="width: 108px; padding-left: 20px;">
                    <asp:Label ID="Label14" runat="server" CssClass="label"><span style="color:black"> </span>&nbsp;&nbsp;Car Make :</asp:Label>
                </td>
                <td style="width:100px">
                    <asp:DropDownList ID="ddlCarMake" runat="server" style="width:100px" DataTextField="Make" DataValueField="ID">
                    </asp:DropDownList>           
                </td>
            </tr>

            <tr>
                <td style="width: 108px">
                    <asp:Label ID="Label15" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Model :</asp:Label>
                </td>
                <td style="width: 100px">
                    <asp:TextBox ID="txtModel" runat="server" Width="217px" CssClass="gvtextbox"></asp:TextBox>
                </td>
                <td style="width: 108px; padding-left: 20px;">
                    <asp:Label ID="Label16" runat="server" CssClass="label"><span style="color:black"> </span>&nbsp;&nbsp;Series :</asp:Label>
                </td>
                <td style="width:100px">
                    <asp:TextBox ID="txtSeries" runat="server" Width="217px" CssClass="gvtextbox"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td style="width: 108px">
                    <asp:Label ID="Label17" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Body Shape :</asp:Label>
                </td>
                <td style="width: 100px">
                    <asp:TextBox ID="txtBodyShape" runat="server" Width="217px" CssClass="gvtextbox"></asp:TextBox>
                </td>
                <td style="width: 108px">
                    <asp:Label ID="Label18" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Fuel Type :</asp:Label>
                </td>
                <td style="width: 100px">
                    <asp:DropDownList ID="ddlFuelType" runat="server" style="width:100px">
                    </asp:DropDownList>
                </td>
            </tr>

            <tr>
               <td style="width: 108px">
                   <asp:Label ID="Label19" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Transmission :</asp:Label>
               </td>
               <td style="width: 100px">
                   <asp:DropDownList ID="ddlTransmission" runat="server" style="width:100px">
                   </asp:DropDownList>
               </td>
               <td style="width: 108px">
                   <asp:Label ID="Label20" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Body Color :</asp:Label>
               </td>
               <td style="width: 100px">
                   <asp:TextBox ID="txtBodyColor" runat="server" Width="217px" CssClass="gvtextbox"></asp:TextBox>
               </td>
            </tr>

            <tr>
                <td style="width: 108px">
                    <asp:Label ID="Label25" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Estimated Delivery Date :</asp:Label>
                </td>
                <td style="width: 100px">
                    <asp:TextBox ID="txtEstimatedDeliveryDate" runat="server" Width="217px" CssClass="gvtextbox"></asp:TextBox>
                </td>
            </tr>

        </table>

        <table style="padding: 20px">
            <tr>
                <td style="width: 108px">
                    <asp:Label ID="Label22" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Price :</asp:Label>
                </td>
                <td style="width: 100px">
                    <asp:TextBox ID="txtPrice" runat="server" Width="217px" CssClass="gvtextbox"></asp:TextBox>
                </td>
                <td style="width: 108px; padding-left: 20px;">
                    <asp:Label ID="Label23" runat="server" CssClass="label"><span style="color:black"> </span>&nbsp;&nbsp;Commission :</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCommision" runat="server" CssClass="gvtextbox" Width="217px"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td style="width: 108px">
                    <asp:Label ID="Label7" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Member No :</asp:Label>
                </td>
                <td style="width: 100px">
                    <asp:TextBox ID="txtMemberNo" runat="server" Width="217px" CssClass="gvtextbox"></asp:TextBox>
                </td>
                <td style="width: 108px">
                   <asp:Label ID="Label8" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Supplier :</asp:Label>
               </td>
               <td style="width: 100px">
                   <asp:DropDownList ID="ddlSupplier" runat="server" style="width:100px" DataTextField="Name" DataValueField="ID">
                   </asp:DropDownList>
               </td>
            </tr>

            <tr>
               <td style="width: 108px">
                   <asp:Label ID="Label26" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Card Type :</asp:Label>
               </td>
               <td style="width: 100px">
                    <asp:TextBox ID="TextBox1" runat="server" Width="217px" CssClass="gvtextbox"></asp:TextBox>
                </td>

                <td style="width: 108px">
                   <asp:Label ID="Label27" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Card Number :</asp:Label>
               </td>
               <td style="width: 100px">
                    <asp:TextBox ID="txtCardNumber" runat="server" Width="217px" CssClass="gvtextbox"></asp:TextBox>
                </td>
            </tr>

            <tr>
               <td style="width: 108px">
                   <asp:Label ID="Label28" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Expiry Date :</asp:Label>
               </td>
               <td style="width: 100px">
                    <asp:TextBox ID="txtMonth" runat="server" Width="20px" CssClass="gvtextbox"></asp:TextBox>
                    <asp:Label ID="Label30" runat="server" CssClass="label">&nbsp;/</asp:Label>
                    <asp:TextBox ID="txtYear" runat="server" Width="20px" CssClass="gvtextbox"></asp:TextBox>
                </td>

                <td style="width: 108px">
                   <asp:Label ID="Label29" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;CV Number :</asp:Label>
               </td>
               <td style="width: 100px">
                    <asp:TextBox ID="txtCVNumber" runat="server" Width="217px" CssClass="gvtextbox"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td style="width: 108px">
                   
                </td>
            </tr>

        </table>

        <asp:Button ID="Button1" runat="server" Text="Search Client by Name/Email" Style="padding-right: 10px;
            padding-left: 30px; margin-left: 150px" />
        <asp:Button ID="Button2" runat="server" Text="Add Client" 
            Style="padding-right: 10px" onclick="Button2_Click" />
        <asp:Button ID="Button3" runat="server" Text="Edit Client" Style="padding-right: 10px" />
        <asp:Button ID="Button4" runat="server" Text="Create Contract" 
            Style="padding-right: 10px" onclick="Button4_Click" />
    </asp:Panel>
</asp:Content>
