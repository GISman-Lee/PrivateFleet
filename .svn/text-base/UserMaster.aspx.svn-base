<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="UserMaster.aspx.cs" Inherits="UserMaster" Title="User Master" %>

<%@ Register Src="User Controls/UCUserMasterView.ascx" TagName="UCUserMasterView"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UP_usermaster" runat="server">
        <ContentTemplate>
            <uc1:UCUserMasterView ID="UCUserMasterView1" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress  ID="upProcess" runat="server"  DisplayAfter="500" AssociatedUpdatePanelID="UP_usermaster">
        <ProgressTemplate>
            <div id="progressBackgroundFilter">
            </div>
            <div id="processMessage">
                <span style="text-align: center;">
                    <img src="Images/loading.gif" alt="" /><br />
                    Loading...Please wait...</span></div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
