<%@ Page Title="Customer Help Report" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Admin_CustomerHelpReport.aspx.cs" Inherits="Admin_CustomerHelpReport" %>
<%@ Register Src ="~/User Controls/Uc_VDT_CustomerHelp.ascx" TagName ="UC_CustomerHelp" TagPrefix ="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<br>

    <script type="text/javascript">
        function compaire_dates_ViewSentRequests1(source, args) {
 
            // debugger;
            args.IsValid = true;
            fromdate = (document.getElementById('ctl00_ContentPlaceHolder1_ucCustomerHelp_txtFromDate').value).trim();

            if (fromdate.trim() == "") {
                args.IsValid = false;
                return;
            }
            todate = (document.getElementById('ctl00_ContentPlaceHolder1_ucCustomerHelp_txtToDate').value).trim();

            if (todate.trim() == "") {
                args.IsValid = false;
                return;
            }
            // alert(chkdt(fromdate, todate));
            dt = new Date();
            dt.setDate(fromdate);


            args.IsValid = chkdt(fromdate, todate);
            return;
        }
    </script>

 <uc1:UC_CustomerHelp runat ="server" ID="ucCustomerHelp" />
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

