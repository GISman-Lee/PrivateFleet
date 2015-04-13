<%@ Page Title="Dealer Summary Report" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Admin_DealerSummaryReport.aspx.cs" Inherits="Admin_DealerSummaryReport" %>

<%@ Register Src="~/VDT_Customer/User Controls/UC_VDTDealerOrederCount.ascx" TagName="UC_VDTDealerCustomercount"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

  <%--  <script type="text/javascript">

        /* Added By archana To set Equal Width of Make & Company Dropdown: Date: 28 March 2012 */
        function autoWidth() {
            var maxlength = 0;
            var ddlCompany = document.getElementById('ctl00_ContentPlaceHolder1_UC_VDTDealerCustomer_ddlCompany');
            var ddlMake = document.getElementById('ctl00_ContentPlaceHolder1_UC_VDTDealerCustomer_drpMake');
         
            for (var i = 0; i < ddlCompany.options.length; i++) {
                if (ddlCompany[i].text.length > maxlength) {
                    maxlength = ddlCompany[i].text.length;
                }
            }

            ddlCompany.style.width = maxlength * 7 + 'px';
            ddlMake.style.width = maxlength * 7 + 'px';
            alert(ddlMake.style.width)
            alert('OUT')
        }
   
    </script>--%>

    <br>
    <uc1:UC_VDTDealerCustomercount runat="server" ID="UC_VDTDealerCustomer" Visible="true" />
    <asp:UpdateProgress ID="upProcess" runat="server" DisplayAfter="2">
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
