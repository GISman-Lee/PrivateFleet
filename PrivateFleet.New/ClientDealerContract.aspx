<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ClientDealerContract.aspx.cs" Inherits="ClientDealerContract" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="~/CSS/stylesheet.css" rel="stylesheet" type="text/css" />
    <asp:Panel ID="CDContractPanel" runat="server">
        <table style="padding: 20px; border-width: 2px; border-color: gray; border-style: solid; Width:100%;">
            <tr>
                <td style="width: 150px">
                    <asp:Label ID="Label1" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Customer :</asp:Label>
                </td>
                <td style="width: 90px">
                    <asp:TextBox ID="txtCustomerName" runat="server" Width="160px" CssClass="gvtextbox"></asp:TextBox>
                </td>
                <td style="width: 150px; padding-left: 20px;">
                    <asp:Label ID="Label2" runat="server" CssClass="label"><span style="color:black"> </span>&nbsp;&nbsp;Email :</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="gvtextbox" Width="160px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 150px;">
                    <asp:Label ID="Label4" runat="server" CssClass="label"><span style="color:black"> </span>&nbsp;&nbsp;Company :</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCompany" runat="server" CssClass="gvtextbox" Width="160"></asp:TextBox>
                </td>
                <td style="width: 150px; padding-left: 20px;">
                    <asp:Label ID="Label11" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Fax :</asp:Label>
                </td>
                <td style="width: 160px">
                    <asp:TextBox ID="txtFax" runat="server" Width="160px" CssClass="gvtextbox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 150px;">
                    <asp:Label ID="Label9" runat="server" CssClass="label"><span style="color:black"> </span>&nbsp;&nbsp;City :</asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlCity" runat="server" Style="width: 100px" DataTextField="City"
                        DataValueField="City">
                    </asp:DropDownList>
                </td>
                <td style="width: 150px; padding-left: 20px;">
                    <asp:Label ID="Label3" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Address :</asp:Label>
                </td>
                <td style="width: 160px">
                    <asp:TextBox ID="txtAddress" runat="server" Width="160px" CssClass="gvtextbox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 150px">
                    <asp:Label ID="Label10" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;PostCode :</asp:Label>
                </td>
                <td style="width: 100px">
                    <asp:TextBox ID="txtPostCode" runat="server" Width="160px" CssClass="gvtextbox"></asp:TextBox>
                </td>
                <td style="width: 150px; padding-left: 20px;">
                    <asp:Label ID="Label12" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;State :</asp:Label>
                </td>
                <td style="width: 100px">
                    <asp:DropDownList ID="ddlState" runat="server" Style="width: 100px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 150px">
                    <asp:Label ID="Label5" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Mobile :</asp:Label>
                </td>
                <td style="width: 100px">
                    <asp:TextBox ID="txtMobile" runat="server" Width="160px" CssClass="gvtextbox"></asp:TextBox>
                </td>
                <td style="width: 150px; padding-left: 20px;">
                    <asp:Label ID="Label6" runat="server" CssClass="label"><span style="color:black"> </span>&nbsp;&nbsp;Phone :</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPhone" runat="server" CssClass="gvtextbox" Width="160"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 150px">
                    <asp:Label ID="Label21" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Consultant :</asp:Label>
                </td>
                <td style="width: 100px">
                    <asp:TextBox ID="txtConsultant" runat="server" Width="160px" CssClass="gvtextbox"></asp:TextBox>
                </td>
                <td style="width: 150px; padding-left: 20px;">
                    <asp:Label ID="Label24" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Registration :</asp:Label>
                </td>
                <td style="width: 160px">
                    <asp:DropDownList ID="ddlRegistration" runat="server" Style="width: 100px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 150px">
                    <asp:Label ID="Label7" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Member No :</asp:Label>
                </td>
                <td style="width: 160px">
                    <asp:TextBox ID="txtMemberNo" runat="server" Width="160px" CssClass="gvtextbox"></asp:TextBox>
                </td>
                <td style="width: 150px; padding-left: 20px;">
                    <asp:Label ID="Label22" runat="server" CssClass="label"><span style="color:black"> </span>&nbsp;&nbsp;Sur Name :</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSurName" runat="server" CssClass="gvtextbox" Width="160"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table style="padding: 20px; border-width: 2px; border-color: gray; border-style: solid; Width:100%;">
            <tr>
                <td style="width: 150px">
                    <asp:Label ID="Label13" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Vehicle Year :</asp:Label>
                </td>
                <td style="width: 100px">
                    <asp:TextBox ID="txtVehicleYear" runat="server" Width="160px" CssClass="gvtextbox"></asp:TextBox>
                </td>
                <td style="width: 150px; padding-left: 20px;">
                    <asp:Label ID="Label14" runat="server" CssClass="label"><span style="color:black"> </span>&nbsp;&nbsp;Car Make :</asp:Label>
                </td>
                <td style="width: 160px">
                    <asp:DropDownList ID="ddlCarMake" runat="server" Style="width: 100px" DataTextField="Make"
                        DataValueField="ID">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 150px">
                    <asp:Label ID="Label15" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Model :</asp:Label>
                </td>
                <td style="width: 100px">
                    <asp:TextBox ID="txtModel" runat="server" Width="160px" CssClass="gvtextbox"></asp:TextBox>
                </td>
                <td style="width: 150px; padding-left: 20px;">
                    <asp:Label ID="Label16" runat="server" CssClass="label"><span style="color:black"> </span>&nbsp;&nbsp;Series :</asp:Label>
                </td>
                <td style="width: 160px">
                    <asp:TextBox ID="txtSeries" runat="server" Width="160px" CssClass="gvtextbox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 150px">
                    <asp:Label ID="Label17" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Body Shape :</asp:Label>
                </td>
                <td style="width: 100px">
                    <asp:TextBox ID="txtBodyShape" runat="server" Width="160px" CssClass="gvtextbox"></asp:TextBox>
                </td>
                <td style="width: 150px; padding-left: 20px;">
                    <asp:Label ID="Label18" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Fuel Type :</asp:Label>
                </td>
                <td style="width: 160px">
                    <asp:DropDownList ID="ddlFuelType" runat="server" Style="width: 100px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 150px">
                    <asp:Label ID="Label19" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Transmission :</asp:Label>
                </td>
                <td style="width: 100px">
                    <asp:DropDownList ID="ddlTransmission" runat="server" Style="width: 100px">
                    </asp:DropDownList>
                </td>
                <td style="width: 150px; padding-left: 20px;">
                    <asp:Label ID="Label20" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Body Color :</asp:Label>
                </td>
                <td style="width: 160px">
                    <asp:TextBox ID="txtBodyColor" runat="server" Width="160px" CssClass="gvtextbox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 150px">
                    <asp:Label ID="Label25" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Delivery Date :</asp:Label>
                </td>
                <td style="width: 100px">
                    <asp:TextBox ID="txtEstimatedDeliveryDate" runat="server" Width="160px" CssClass="gvtextbox"></asp:TextBox>
                </td>
                <td style="width: 150px; padding-left: 20px;">
                    <asp:Label ID="Label8" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Supplier :</asp:Label>
                </td>
                <td style="width: 160px">
                    <asp:DropDownList ID="ddlSupplier" runat="server" Style="width: 100px" DataTextField="Name"
                        DataValueField="ID">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <table style="padding: 20px; border-width: 2px; border-color: gray; border-style: solid; Width:100%;">
            <tr>
                
                <td style="width: 150px; padding-left: 20px;">
                    <asp:Label ID="Label23" runat="server" CssClass="label"><span style="color:black"> </span>&nbsp;&nbsp;Deposit :</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDeposit" runat="server" CssClass="gvtextbox" Width="160px"></asp:TextBox>
                </td>  
            </tr>
            <tr>
                <td style="width: 150px">
                    <asp:Label ID="Label26" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Card Type :</asp:Label>
                </td>
                <td style="width: 100px">
                    <asp:DropDownList ID="ddlCardType" runat="server" Style="width: 100px">
                    </asp:DropDownList>
                </td>
                <td style="width: 150px; padding-left: 20px;">
                    <asp:Label ID="Label27" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Card Number :</asp:Label>
                </td>
                <td style="width: 160px">
                    <asp:TextBox ID="txtCardNumber" runat="server" Width="160px" CssClass="gvtextbox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 150px">
                    <asp:Label ID="Label28" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Expiry Date :</asp:Label>
                </td>
                <td style="width: 100px">
                    <asp:TextBox ID="txtMonth" runat="server" Width="20px" CssClass="gvtextbox"></asp:TextBox>
                    <asp:Label ID="Label30" runat="server" CssClass="label">&nbsp;/</asp:Label>
                    <asp:TextBox ID="txtYear" runat="server" Width="40px" CssClass="gvtextbox"></asp:TextBox>
                </td>
                <td style="width: 150px; padding-left: 20px;">
                    <asp:Label ID="Label29" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;CV Number :</asp:Label>
                </td>
                <td style="width: 100px">
                    <asp:TextBox ID="txtCVNumber" runat="server" Width="160px" CssClass="gvtextbox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 150px; padding-top: 20px">
                    <asp:Button ID="Button5" runat="server" Text="Add Credit Card" OnClick="Button5_Click" Visible="false" />
                </td>
                <td style="width: 150px; padding-left: 30px; padding-top: 20px">
                    <asp:Button ID="Button6" runat="server" Text="Edit Credit Card" OnClick="Button6_Click" Visible="false"/>
                </td>
            </tr>
        </table>
        <table style="padding: 20px; border-width: 2px; border-color: gray; border-style: solid; Width:100%;">
            <tr>
                <td style="width: 78px">
                    <asp:Label ID="Label41" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Vehicle Retail Price :</asp:Label>
                </td>
                <td style="width: 60px">
                    <asp:TextBox ID="txtVehicleRetailPrice" runat="server" Width="160" CssClass="gvtextbox"></asp:TextBox>
                </td>
                <td style="width: 78px; padding-left: 10px;">
                    <asp:Label ID="Label42" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Accessory Total Cost:</asp:Label>
                </td>
                <td style="width: 60px">
                    <asp:TextBox ID="txtTotalAccessories" runat="server" Width="160" CssClass="gvtextbox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 78px">
                    <asp:Label ID="Label44" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Fleet Discount :</asp:Label>
                </td>
                <td style="width: 60px">
                    <asp:TextBox ID="txtFleetDiscount" runat="server" Width="160px" CssClass="gvtextbox"></asp:TextBox>
                </td>
                <td style="width: 78px; padding-left: 10px;">
                    <asp:Label ID="Label43" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Total OnRoad Cost :</asp:Label>
                </td>
                <td style="width: 60px">
                    <asp:TextBox ID="txtTotalOnRoadCost" runat="server" Width="160px" CssClass="gvtextbox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 150px">
                    <asp:Label ID="Label31" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Pre-Delivery Price :</asp:Label>
                </td>
                <td style="width: 100px">
                    <asp:TextBox ID="txtPreDelivery" runat="server" Width="160px" CssClass="gvtextbox"></asp:TextBox>
                </td>
                <td style="width: 150px; padding-left: 10px;">
                    <asp:Label ID="Label32" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Stamp Duty :</asp:Label>
                </td>
                <td style="width: 100px">
                    <asp:TextBox ID="txtStampDuty" runat="server" Width="160px" CssClass="gvtextbox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 150px">
                    <asp:Label ID="Label33" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Registration Price :</asp:Label>
                </td>
                <td style="width: 100px">
                    <asp:TextBox ID="txtRegistrationPrice" runat="server" Width="160px" CssClass="gvtextbox"></asp:TextBox>
                </td>
                <td style="width: 150px; padding-left: 10px;">
                    <asp:Label ID="Label34" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;CTP :</asp:Label>
                </td>
                <td style="width: 100px">
                    <asp:TextBox ID="txtCTP" runat="server" Width="160" CssClass="gvtextbox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 150px">
                    <asp:Label ID="Label35" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;GST :</asp:Label>
                </td>
                <td style="width: 100px">
                    <asp:TextBox ID="txtGST" runat="server" Width="160px" CssClass="gvtextbox"></asp:TextBox>
                </td>
                <td style="width: 150px; padding-left: 10px;">
                    <asp:Label ID="Label36" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Plate Fee :</asp:Label>
                </td>
                <td style="width: 100px">
                    <asp:TextBox ID="txtPlateFee" runat="server" Width="160px" CssClass="gvtextbox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 150px; padding-left:10px">
                    <asp:Label ID="Label37" runat="server" CssClass="label" Text="Accessory 1"></asp:Label>
                </td>
                <td style="width: 100px">
                    <asp:TextBox ID="txtAccessory1" runat="server" Width="160px" CssClass="gvtextbox"
                        Text="0.00"></asp:TextBox>
                </td>
                <td style="width: 150px; padding-left: 10px;">
                    <asp:Label ID="Label38" runat="server" CssClass="label" Text="Accessory 2"></asp:Label>
                </td>
                <td style="width: 100px">
                    <asp:TextBox ID="txtAccessory2" runat="server" Width="160px" CssClass="gvtextbox"
                        Text="0.00"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 150px; padding-left:10px">
                    <asp:Label ID="Label39" runat="server" CssClass="label" Text=" Accessory 3"></asp:Label>
                </td>
                <td style="width: 100px">
                    <asp:TextBox ID="txtAccessory3" runat="server" Width="160px" CssClass="gvtextbox"
                        Text="0.00"></asp:TextBox>
                </td>
                <td style="width: 150px; padding-left: 10px;">
                    <asp:Label ID="Label40" runat="server" CssClass="label" Text=" Accessory 4"></asp:Label>
                </td>
                <td style="width: 100px">
                    <asp:TextBox ID="txtAccessory4" runat="server" Width="160px" CssClass="gvtextbox"
                        Text="0.00"></asp:TextBox>
                </td>
            </tr>
        </table>
        <asp:ImageButton ID="btnAddDeliveryTrack" runat="server" ImageUrl="~/Images/btnAddDeliveryTrack.png"  CssClass="btnAddTradeIn"  onClick="AddDeliveryTrack_Click" Style="margin-top: 30px;"/>
        <asp:ImageButton ID="btnCreateContract" runat="server" ImageUrl="~/Images/btnCreateContract.png"  CssClass="btnAddTradeIn"  onClick="Button4_Click" Style="margin-top: 30px; margin-left: 260px;"/> <!--margin-left: 260px-->

        <br>
        <br>
        <br></br>
        <table style="padding: 140px">
            <tr>
                <asp:HyperLink ID="LinkDownLoad" runat="server" 
                    NavigateUrl="~/Contract/ContractNoTrade2.pdf" Visible="false">Down Load Contract</asp:HyperLink>
            </tr>
        </table>
        <br></br>
        </br>
        </br>
    </asp:Panel>
</asp:Content>
