<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Admin_DrasticChangeETA.aspx.cs" Inherits="Admin_DrasticChangeETA" %>
<%@ Register Src ="~/VDT_Customer/User Controls/UC_VDT_DrasticChangeInETA.ascx" TagName ="UC_DrasticChangeETA" TagPrefix ="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <uc1:UC_DrasticChangeETA runat ="server" ID="uc_DrasticChangeETA" />
 <br>

     <asp:UpdateProgress ID="upProcess" runat="server" DisplayAfter="2" >
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

