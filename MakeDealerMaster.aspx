<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MakeDealerMaster.aspx.cs" Inherits="MakeDealerMaster" Title="Make Dealer Asscociation" %>

<%@ Register Src="User Controls/UCMakeDealer.ascx" TagName="UCMakeDealer" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:UCMakeDealer id="UCMakeDealer1" runat="server">
    </uc1:UCMakeDealer>
</asp:Content>

