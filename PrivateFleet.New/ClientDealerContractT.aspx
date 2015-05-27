<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ClientDealerContractT.aspx.cs" Inherits="ClientDealerContractT" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="~/CSS/stylesheet.css" rel="stylesheet" type="text/css" />
    <link href="~/CSS/Miles.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .Initial
        {
            display: block;
            padding: 2px 2px 1px 1px;
            float: left;
            background: url("~/Images/InitialImage.png") no-repeat right top;
            color: Black;
            font-weight: bold;
        }
        .Initial:hover
        {
            color: White;
            background: url("~/Images/SelectedButton.png") no-repeat right top;
        }
        .Clicked
        {
            float: left;
            display: block;
            background: url("~/Images/SelectedButton.png") no-repeat right top;
            padding: 2px 1px 1px 1px;
            color: Black;
            font-weight: bold;
            color: White;
        }
        .btnAddTradeIn
        {}
    </style>
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
                    <asp:TextBox ID="txtCompany" runat="server" CssClass="gvtextbox" Width="160px"></asp:TextBox>
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
                    <asp:DropDownList ID="ddlCity" runat="server" Style="width: 160px" DataTextField="City"
                        DataValueField="City">
                    </asp:DropDownList>
                </td>
                <td style="width: 150px; padding-left: 20px;">
                    <asp:Label ID="Label3" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Address :</asp:Label>
                </td>
                <td style="width: 160px">
                    <asp:TextBox ID="txtAddress" runat="server" Width="200px" CssClass="gvtextbox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 150px">
                    <asp:Label ID="Label10" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;PostCode :</asp:Label>
                </td>
                <td style="width: 160px">
                    <asp:TextBox ID="txtPostCode" runat="server" Width="160px" CssClass="gvtextbox"></asp:TextBox>
                </td>
                <td style="width: 150px; padding-left: 20px;">
                    <asp:Label ID="Label12" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;State :</asp:Label>
                </td>
                <td style="width: 160px">
                    <asp:DropDownList ID="ddlState" runat="server" Style="width: 160px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 150px">
                    <asp:Label ID="Label5" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Mobile :</asp:Label>
                </td>
                <td style="width: 160px">
                    <asp:TextBox ID="txtMobile" runat="server" Width="160px" CssClass="gvtextbox"></asp:TextBox>
                </td>
                <td style="width: 150px; padding-left: 20px;">
                    <asp:Label ID="Label6" runat="server" CssClass="label"><span style="color:black"> </span>&nbsp;&nbsp;Phone :</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPhone" runat="server" CssClass="gvtextbox" Width="160px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 150px">
                    <asp:Label ID="Label21" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Consultant :</asp:Label>
                </td>
                <td style="width: 160px">
                    <asp:TextBox ID="txtConsultant" runat="server" Width="160px" CssClass="gvtextbox"></asp:TextBox>
                </td>
                <td style="width: 150px; padding-left: 20px;">
                    <asp:Label ID="Label24" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Registration :</asp:Label>
                </td>
                <td style="width: 160px">
                    <asp:DropDownList ID="ddlRegistration" runat="server" Style="width: 160px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 150px">
                    <asp:Label ID="Label71" runat="server" CssClass="label" Visible=false><span style="color:Red"> </span>&nbsp;&nbsp;Consultant Phone :</asp:Label>
                </td>
                <td style="width: 160px">
                    <asp:TextBox ID="txtConsultantPhone" runat="server" Width="160px" CssClass="gvtextbox" Visible=false></asp:TextBox>
                </td>
                <td style="width: 150px; padding-left: 20px;">
                    <asp:Label ID="Label72" runat="server" CssClass="label" Visible=false><span style="color:black"> </span>&nbsp;&nbsp;Consultant Mail :</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtConsultantMail" runat="server" CssClass="gvtextbox" Width="160px" Visible=false></asp:TextBox>
                </td>
            </tr>
        </table>
        <table style="padding: 20px; border-width: 2px; border-color: gray; border-style: solid;Width:100%;">
            <tr>
                <td style="width: 150px">
                    <asp:Label ID="Label13" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Vehicle Year :</asp:Label>
                </td>
                <td style="width: 160px">
                    <asp:TextBox ID="txtVehicleYear" runat="server" Width="160px" CssClass="gvtextbox"></asp:TextBox>
                </td>
                <td style="width: 150px; padding-left: 20px;">
                    <asp:Label ID="Label14" runat="server" CssClass="label"><span style="color:black"> </span>&nbsp;&nbsp;Car Make :</asp:Label>
                </td>
                <td style="width: 160px">
                    <asp:DropDownList ID="ddlCarMake" runat="server" Style="width: 160px" DataTextField="Make"
                        DataValueField="ID">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 150px">
                    <asp:Label ID="Label15" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Model :</asp:Label>
                </td>
                <td style="width: 160px">
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
                <td style="width: 160px">
                    <asp:TextBox ID="txtBodyShape" runat="server" Width="160px" CssClass="gvtextbox"></asp:TextBox>
                </td>
                <td style="width: 150px; padding-left: 20px;" >
                    <asp:Label ID="Label18" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Fuel Type :</asp:Label>
                </td>
                <td style="width: 160px">
                    <asp:DropDownList ID="ddlFuelType" runat="server" Style="width: 160px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 150px">
                    <asp:Label ID="Label19" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Transmission :</asp:Label>
                </td>
                <td style="width: 160px">
                    <asp:DropDownList ID="ddlTransmission" runat="server" Style="width: 160px">
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
                <td style="width: 160px">
                    <asp:TextBox ID="txtEstimatedDeliveryDate" runat="server" Width="160px" CssClass="gvtextbox"></asp:TextBox>
                </td>
                <td style="width: 150px; padding-left: 20px;">
                    <asp:Label ID="Label8" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Supplier :</asp:Label>
                </td>
                <td style="width: 160px">
                    <asp:DropDownList ID="ddlSupplier" runat="server" Style="width: 160px" DataTextField="Company"
                        DataValueField="ID">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <table style="padding: 20px; border-width: 2px; border-color: gray; border-style: solid;Width:100%;">
            <tr>
                <td style="width: 150px">
                    <asp:Label ID="Label26" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Card Type :</asp:Label>
                </td>
                <td style="width: 160px">
                    <asp:DropDownList ID="ddlCardType" runat="server" Style="width: 160px">
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
                <td style="width: 160px">
                    <asp:TextBox ID="txtMonth" runat="server" Width="20px" CssClass="gvtextbox"></asp:TextBox>
                    <asp:Label ID="Label30" runat="server" CssClass="label">&nbsp;/</asp:Label>
                    <asp:TextBox ID="txtYear" runat="server" Width="40px" CssClass="gvtextbox"></asp:TextBox>
                </td>
                <td style="width: 150px; padding-left: 20px;">
                    <asp:Label ID="Label29" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;CV Number :</asp:Label>
                </td>
                <td style="width: 160px">
                    <asp:TextBox ID="txtCVNumber" runat="server" Width="160px" CssClass="gvtextbox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 150px; padding-top: 20px">
                    <asp:Button ID="Button5" runat="server" Text="Add Credit Card" OnClick="Button5_Click" Visible="false" />
                </td>
                <td style="width: 150px; padding-left: 30px; padding-top:20px">
                    <asp:Button ID="Button6" runat="server" Text="Edit Credit Card" OnClick="Button6_Click" Visible="false"/>
                </td>
            </tr>
        </table>
        <table style="padding: 20px; border-width: 2px; border-color: gray; border-style: solid;Width:100%;">
            <tr>
                <td style="width: 150px">
                    <asp:Label ID="Label41" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Retail Price :</asp:Label>
                </td>
                <td style="width: 60px">
                    <asp:TextBox ID="txtVehicleRetailPrice" runat="server" Width="160px" CssClass="gvtextbox"></asp:TextBox>
                </td>
                <td style="width: 150px">
                    <asp:Label ID="Label42" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Accessory Total Cost:</asp:Label>
                </td>
                <td style="width: 60px">
                    <asp:TextBox ID="txtTotalAccessories" runat="server" Width="160px" CssClass="gvtextbox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 150px">
                    <asp:Label ID="Label44" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Fleet Discount :</asp:Label>
                </td>
                <td style="width: 60px">
                    <asp:TextBox ID="txtFleetDiscount" runat="server" Width="160px" CssClass="gvtextbox"></asp:TextBox>
                </td>
                <td style="width: 150px">
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
                <td style="width: 160px">
                    <asp:TextBox ID="txtPreDelivery" runat="server" Width="160px" CssClass="gvtextbox"></asp:TextBox>
                </td>
                <td style="width: 150px">
                    <asp:Label ID="Label32" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Stamp Duty :</asp:Label>
                </td>
                <td style="width: 160px">
                    <asp:TextBox ID="txtStampDuty" runat="server" Width="160px" CssClass="gvtextbox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 150px">
                    <asp:Label ID="Label33" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Registration Price :</asp:Label>
                </td>
                <td style="width: 160px">
                    <asp:TextBox ID="txtRegistrationPrice" runat="server" Width="160px" CssClass="gvtextbox"></asp:TextBox>
                </td>
                <td style="width: 150px">
                    <asp:Label ID="Label34" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;CTP :</asp:Label>
                </td>
                <td style="width: 160px">
                    <asp:TextBox ID="txtCTP" runat="server" Width="160px" CssClass="gvtextbox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 150px">
                    <asp:Label ID="Label35" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;GST :</asp:Label>
                </td>
                <td style="width: 160px">
                    <asp:TextBox ID="txtGST" runat="server" Width="160px" CssClass="gvtextbox"></asp:TextBox>
                </td>
                <td style="width: 150px">
                    <asp:Label ID="Label36" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Plate Fee :</asp:Label>
                </td>
                <td style="width: 160px">
                    <asp:TextBox ID="txtPlateFee" runat="server" Width="160px" CssClass="gvtextbox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 150px; padding-left:10px">
                    <asp:Label ID="Label37" runat="server" CssClass="label" Text="Accessory 1"></asp:Label>
                </td>
                <td style="width: 160px">
                    <asp:TextBox ID="txtAccessory1" runat="server" Width="160px" CssClass="gvtextbox"
                        Text="0.00"></asp:TextBox>
                </td>
                <td style="width: 150px; padding-left:10px">
                    <asp:Label ID="Label38" runat="server" CssClass="label" Text="Accessory 2"></asp:Label>
                </td>
                <td style="width: 160px">
                    <asp:TextBox ID="txtAccessory2" runat="server" Width="160px" CssClass="gvtextbox"
                        Text="0.00"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 150px; padding-left:10px">
                    <asp:Label ID="Label39" runat="server" CssClass="label" Text=" Accessory 3"></asp:Label>
                </td>
                <td style="width: 160px">
                    <asp:TextBox ID="txtAccessory3" runat="server" Width="160px" CssClass="gvtextbox"
                        Text="0.00"></asp:TextBox>
                </td>
                <td style="width: 150px; padding-left:10px">
                    <asp:Label ID="Label40" runat="server" CssClass="label" Text=" Accessory 4"></asp:Label>
                </td>
                <td style="width: 160px">
                    <asp:TextBox ID="txtAccessory4" runat="server" Width="160px" CssClass="gvtextbox"
                        Text="0.00"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table width="100%" align="center">
            <tr>
                <td>
                    <!--http://dabuttonfactory.com/#t=TAB++2&f=Calibri-Bold&ts=20&tc=fff&tshs=1&tshc=000&hp=20&vp=8&c=5&bgt=gradient&bgc=d4d8dc&ebgc=073763-->
                    <asp:ImageButton ID="Tab_1" runat="server" ImageUrl="~/Images/tabTrade1.png"  OnClick="Tab1_Click"  ImageAlign="Left"  CssClass=".Clicked" />
                    <asp:ImageButton ID="Tab_2" runat="server" ImageUrl="~/Images/tabPFMembership.png"  OnClick="Tab2_Click"  ImageAlign="Left"  CssClass=".Initial" />
                    <asp:ImageButton ID="Tab_3" runat="server" ImageUrl="~/Images/tabDealerInfo.png"  OnClick="Tab3_Click"  ImageAlign="Left"  CssClass=".Initial" />
                    <asp:MultiView ID="MainView" runat="server">
                        <asp:View ID="View1" runat="server">
                            <table style="width: 100%; border-width: 5px; border-color: Navy; border-style: solid;">
                                <caption>
                                    <tr>
                                        <td style="width: 170px">
                                            <asp:Label ID="Label46" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;T Year :</asp:Label>
                                        </td>
                                        <td style="width: 160px; padding-left: 10px;">
                                            <asp:TextBox ID="txtTradeYear" runat="server" CssClass="gvtextbox" 
                                                Width="160px"></asp:TextBox>
                                        </td>
                                        <td style="width: 150px; padding-left: 10px;">
                                            <asp:Label ID="Label45" runat="server" CssClass="label"><span style="color:black"> </span>&nbsp;&nbsp;T Make :</asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlTCarMake" runat="server" DataTextField="Make" 
                                                DataValueField="ID" Style="width: 160px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 170px">
                                            <asp:Label ID="Label47" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;T Model :</asp:Label>
                                        </td>
                                        <td style="width: 160px; padding-left: 10px;">
                                            <asp:TextBox ID="txtTModel" runat="server" CssClass="gvtextbox" Width="160px"></asp:TextBox>
                                        </td>
                                        <td style="width: 150px; padding-left: 10px;">
                                            <asp:Label ID="Label48" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;T Series :</asp:Label>
                                        </td>
                                        <td style="width: 160px">
                                            <asp:TextBox ID="txtTSeries" runat="server" CssClass="gvtextbox" Width="160px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 170px">
                                            <asp:Label ID="Label49" runat="server" CssClass="label" ><span style="color:Red"> </span>&nbsp;&nbsp;T BodyShape:</asp:Label>
                                        </td>
                                        <td style="width: 160px; padding-left: 10px;">
                                            <asp:TextBox ID="txtTBodyShape" runat="server" CssClass="gvtextbox" 
                                                Width="160px"></asp:TextBox>
                                        </td>
                                        <td style="width: 150px; padding-left: 10px;">
                                            <asp:Label ID="Label50" runat="server" CssClass="label"><span style="color:black"> </span>&nbsp;&nbsp;T Fuel Type :</asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlTFuelType" runat="server" Style="width: 160px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 170px">
                                            <asp:Label ID="Label51" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;T Odometer :</asp:Label>
                                        </td>
                                        <td style="width: 160px; padding-left: 10px;">
                                            <asp:TextBox ID="txtTOdometer" runat="server" CssClass="gvtextbox" 
                                                Width="160px"></asp:TextBox>
                                        </td>
                                        <td style="width: 150px; padding-left: 10px;">
                                            <asp:Label ID="Label52" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;T Transmission :</asp:Label>
                                        </td>
                                        <td style="width: 160px">
                                            <asp:DropDownList ID="ddlTTransmission" runat="server" Style="width: 160px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 170px">
                                            <asp:Label ID="Label53" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;T Body Color:</asp:Label>
                                        </td>
                                        <td style="width: 160px; padding-left: 10px">
                                            <asp:TextBox ID="txtTBodyColour" runat="server" CssClass="gvtextbox" 
                                                Width="160px"></asp:TextBox>
                                        </td>
                                        <td style="width: 150px; padding-left: 10px;">
                                            <asp:Label ID="Label54" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;T Trim Color :</asp:Label>
                                        </td>
                                        <td style="width: 160px">
                                            <asp:TextBox ID="txtTTrimColour" runat="server" CssClass="gvtextbox" 
                                                Width="160px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 170px">
                                            <asp:Label ID="Label55" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;T RegExpiry :</asp:Label>
                                        </td>
                                        <td style="width: 160px; padding-left:10px">
                                            <asp:TextBox ID="txtTExpiryMonth" runat="server" CssClass="gvtextbox" 
                                                Width="20px"></asp:TextBox>
                                            <asp:Label ID="Label56" runat="server" CssClass="label">&nbsp;/</asp:Label>
                                            <asp:TextBox ID="txtTExpiryYear" runat="server" CssClass="gvtextbox" 
                                                Width="20px"></asp:TextBox>
                                        </td>
                                        <td style="width: 150px; padding-left: 10px;">
                                            <asp:Label ID="Label57" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;T Log Books :</asp:Label>
                                        </td>
                                        <td style="width: 160px">
                                            <asp:DropDownList ID="ddlTLogBooks" runat="server" Style="width: 160px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 170px">
                                            <asp:Label ID="Label58" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;T OrigValue :</asp:Label>
                                        </td>
                                        <td style="width: 160px; padding-left: 10px">
                                            <asp:TextBox ID="txtTOrigValue" runat="server" CssClass="gvtextbox" Width="160px"
                                                ></asp:TextBox>
                                        </td>
                                        <td style="width: 170px; padding-left: 10px"">
                                            <asp:Label ID="Label59" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;T Status :</asp:Label>
                                        </td>
                                        <td style="width: 160px;">
                                            <asp:DropDownList ID="ddlTTradeStatus" runat="server" Style="width: 160px">
                                            </asp:DropDownList>
                                        </td>  
                                    </tr>
                                    <tr>
                                        <td style="width: 150px">
                                            <asp:Label ID="Label22" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;T Description:</asp:Label>
                                        </td>
                                        <td style="width: 200px; padding-left: 10px">
                                            <asp:TextBox ID="txtTDescription" runat="server" CssClass="gvtextbox" Height="100px"
                                                TextMode="MultiLine" Width="158px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td style="width: 90px">
                                            <asp:ImageButton ID="btnAddTradeIn" runat="server" ImageUrl="~/Images/btnAddTradeIn.png"
                                                CssClass="btnAddTradeIn" OnClick="Button7_Click" ImageAlign="left" Width="113px" />
                                        </td>
                                    </tr>
                                </caption>
                            </table>
                        </asp:View>
                        <asp:View ID="View2" runat="server">
                            <table style="width: 100%; border-width: 5px; border-color: Navy; border-style: solid">
                                <caption>
                                    <tr>
                                        <td style="width: 170px">
                                            <asp:Label ID="Label7" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Member No :</asp:Label>
                                        </td>
                                        <td style="width: 160px; padding-left: 10px;">
                                            <asp:TextBox ID="txtMemberNo" runat="server" Width="160px" CssClass="gvtextbox"></asp:TextBox>
                                        </td>
                                        <td style="width: 150px; padding-left: 10px;">
                                            <asp:Label ID="Label60" runat="server" CssClass="label"><span style="color:black"> </span>&nbsp;&nbsp;Sur Name :</asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtSurName" runat="server" CssClass="gvtextbox" Width="160px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 170px">
                                            <asp:Label ID="Label61" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Deposit Amount :</asp:Label>
                                        </td>
                                        <td style="width: 160px; padding-left: 10px;">
                                            <asp:TextBox ID="txtDepositAmount" runat="server" CssClass="gvtextbox" 
                                                Width="160px"></asp:TextBox>
                                        </td>
                                        <td style="width: 150px; padding-left: 10px;">
                                            <asp:Label ID="Label62" runat="server" CssClass="label"><span style="color:black"> </span>&nbsp;&nbsp;Deposit Taken :</asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlDepositTaken" runat="server" Style="width: 160px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 170px">
                                            <asp:Label ID="Label63" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Membership Fee :</asp:Label>
                                        </td>
                                        <td style="width: 160px; padding-left: 10px;">
                                            <asp:TextBox ID="txtMembershipFee" runat="server" CssClass="gvtextbox" 
                                                Width="160px"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnUpdatePFMembership" runat="server" ImageUrl="~/Images/btnUpdatePFMembership.png"  CssClass="btnAddTradeIn"  onClick="UpdatePFMembership_Click" Style=""/>
                                        </td>
                                    </tr>
                                </caption>
                            </table>
                        </asp:View>
                        <asp:View ID="View3" runat="server">
                            <table style="width: 100%; border-width: 5px; border-color: Navy; border-style: solid">
                                <caption>
                                    <tr>
                                        <td style="width: 170px">
                                            <asp:Label ID="Label23" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Compnay :</asp:Label>
                                        </td>
                                        <td style="width: 160px; padding-left: 10px;">
                                            <asp:TextBox ID="txtDealerCompany" runat="server" Width="160px" CssClass="gvtextbox"></asp:TextBox>
                                        </td>
                                        <td style="width: 150px; padding-left: 10px;">
                                            <asp:Label ID="Label64" runat="server" CssClass="label"><span style="color:black"> </span>&nbsp;&nbsp;Name :</asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDealerName" runat="server" CssClass="gvtextbox" Width="160px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 170px">
                                            <asp:Label ID="Label65" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Phone :</asp:Label>
                                        </td>
                                        <td style="width: 160px; padding-left: 10px;">
                                            <asp:TextBox ID="txtDealerPhone" runat="server" CssClass="gvtextbox" 
                                                Width="160px"></asp:TextBox>
                                        </td>
                                        <td style="width: 150px; padding-left: 10px;">
                                            <asp:Label ID="Label66" runat="server" CssClass="label"><span style="color:black"> </span>&nbsp;&nbsp;Email :</asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDealerEmail" runat="server" CssClass="gvtextbox" 
                                                Width="160px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 170px">
                                            <asp:Label ID="Label67" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Address :</asp:Label>
                                        </td>
                                        <td style="width: 160px; padding-left: 10px;">
                                            <asp:TextBox ID="txtDealerAddress" runat="server" CssClass="gvtextbox" 
                                                Width="160px"></asp:TextBox>
                                        </td>
                                        <td style="width: 150px; padding-left: 10px;">
                                            <asp:Label ID="Label68" runat="server" CssClass="label"><span style="color:black"> </span>&nbsp;&nbsp;City :</asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDealerCity" runat="server" CssClass="gvtextbox" 
                                                Width="160px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 170px">
                                            <asp:Label ID="Label69" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;State :</asp:Label>
                                        </td>
                                        <td style="width: 160px; padding-left: 10px;">
                                            <asp:TextBox ID="txtDealerState" runat="server" CssClass="gvtextbox" 
                                                Width="160px"></asp:TextBox>
                                        </td>
                                        <td style="width: 150px; padding-left: 10px;">
                                            <asp:Label ID="Label70" runat="server" CssClass="label"><span style="color:black"> </span>&nbsp;&nbsp;PCode :</asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDealerPCode" runat="server" CssClass="gvtextbox" 
                                                Width="160px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </caption>
                            </table>
                        </asp:View>
                    </asp:MultiView>
                </td>
            </tr>
        </table>
        <table style="padding: 30px">
        <asp:ImageButton ID="btnAddDeliveryTrack" runat="server" ImageUrl="~/Images/btnAddCustomerPool.png"  CssClass="btnAddTradeIn"  onClick="AddDeliveryTrack_Click" Style="margin-top: 20px; margin-left: 30px"/>
        <asp:ImageButton ID="btnCreateContract" runat="server" ImageUrl="~/Images/btnCreateContract.png"  CssClass="btnAddTradeIn"  onClick="Button4_Click"  ImageAlign=Right Style="margin-top: 20px; margin-right: 30px"/>
        <asp:ImageButton ID="btnSearchCustomer" runat="server" ImageUrl="~/Images/btnSearchCustomer.png"  CssClass="btnAddTradeIn"  onClick="SearchCustomer_Click"  ImageAlign=Right Style="margin-top: 20px; margin-right: 30px"/>
        </table>
        <table style="padding: 10px; padding-left: 30px">
            <tr>
                <asp:HyperLink ID="LinkDownLoad" runat="server" NavigateUrl="~/Contract/ContractTrade2.pdf"
                    Visible="false" >Down Load Contract</asp:HyperLink>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
