<%@ Page Language="C#" MasterPageFile="~/MasterPage.master"  AutoEventWireup="true" CodeFile="ClientDealerContract.aspx.cs" Inherits="ClientDealerContract" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<link href="~/CSS/stylesheet.css" rel="stylesheet" type="text/css" />
<asp:Panel ID="CDContractPanel" runat="server">
<table style="padding:20px">
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
            <td style="width: 108px">
                <asp:Label ID="Label3" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Address :</asp:Label>
            </td>
            <td style="width: 100px">
                <asp:TextBox ID="TextBox1" runat="server" Width="217px" CssClass="gvtextbox"></asp:TextBox>
            </td>
            <td style="width: 108px; padding-left: 20px;">
                <asp:Label ID="Label4" runat="server" CssClass="label"><span style="color:black"> </span>&nbsp;&nbsp;Company :</asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBox2" runat="server" CssClass="gvtextbox" Width="217px"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td style="width: 108px">
                <asp:Label ID="Label5" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Mobile :</asp:Label>
            </td>
            <td style="width: 100px">
                <asp:TextBox ID="TextBox3" runat="server" Width="217px" CssClass="gvtextbox"></asp:TextBox>
            </td>
            <td style="width: 108px; padding-left: 20px;">
                <asp:Label ID="Label6" runat="server" CssClass="label"><span style="color:black"> </span>&nbsp;&nbsp;Phone :</asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBox4" runat="server" CssClass="gvtextbox" Width="217px"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td style="width: 108px">
                <asp:Label ID="Label7" runat="server" CssClass="label"><span style="color:Red"> </span>&nbsp;&nbsp;Price :</asp:Label>
            </td>
            <td style="width: 100px">
                <asp:TextBox ID="TextBox5" runat="server" Width="217px" CssClass="gvtextbox"></asp:TextBox>
            </td>
            <td style="width: 108px; padding-left: 20px;">
                <asp:Label ID="Label8" runat="server" CssClass="label"><span style="color:black"> </span>&nbsp;&nbsp;Commission :</asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBox6" runat="server" CssClass="gvtextbox" Width="217px"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td style="width:auto">
                          
            </td>
            <td style="width:50">
                
            </td>
            <td style="width:auto">
                
            </td>
            <td style="width:auto">
                  
            </td>
        </tr>
    
        
    </table>
    <asp:Button ID="Button1" runat="server" Text="Search Client" style="padding-right:10px; padding-left: 30px; margin-left: 150px"/>
    <asp:Button ID="Button2" runat="server" Text="Add Client" style="padding-right:10px"/>
    <asp:Button ID="Button3" runat="server" Text="Edit Client" style="padding-right:10px"/>
    <asp:Button ID="Button4" runat="server" Text="Create Contract" style="padding-right:10px"/>
</asp:Panel>
</asp:Content>