<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="DealerMaster.aspx.cs" Inherits="DealerMaster" Title="Dealer Master" %>

<%@ Register Src="User Controls/UCDealerView.ascx" TagName="UCDealerView" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UP_DealerView" runat="server">
        <ContentTemplate>
            <uc1:UCDealerView ID="UCDealerView1" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="upProcess" runat="server" DisplayAfter="1500" AssociatedUpdatePanelID="UP_DealerView">
        <ProgressTemplate>
            <div id="progressBackgroundFilter">
            </div>
            <div id="processMessage">
                <span style="text-align: center;">
                    <img alt="" src="Images/loading.gif" /><br />
                    Loading...Please wait...</span></div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
