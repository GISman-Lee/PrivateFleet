<%@ Page Title="Customer Status Update" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ClinetIfo_ForDealer.aspx.cs" Inherits="ClinetIfo_ForDealer" %>

<%@ Register Src="~/User Controls/UCDealers_ClientList.ascx" TagName="ucDealers_ClientList"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">

        function hidePanel() {
            var mdlPopClientUpdated = document.getElementById('ctl00_ContentPlaceHolder1_UCDealersClientList_mdlPopClientUpdated_backgroundElement');
            var pnlUpdated = document.getElementById('ctl00_ContentPlaceHolder1_UCDealersClientList_pnlpopUp');
            mdlPopClientUpdated.style.display = 'none';
            pnlUpdated.style.display = 'none';
        }

        //        function pageLoad() {
        //            var hdnIsClientUpdated = document.getElementById('ctl00_ContentPlaceHolder1_UCDealersClientList_hdnIsClientUpdated');
        //            if (hdnIsClientUpdated != null && hdnIsClientUpdated.value == "yes") {
        //                setTimeout("hidePanel()", 2000);
        //                hdnIsClientUpdated.value = "";
        //            }
        //        }

        function hideDiv() {
            var msgpop = document.getElementById('ctl00_ContentPlaceHolder1_UCDealersClientList_msgpop');
            msgpop.style.display = 'none';
        }
        function SetTimerToHideDiv() {
            //            var _hdnIsClientUpdated = document.getElementById('ctl00_ContentPlaceHolder1_UCDealersClientList_hdnIsClientUpdated');
            //            alert(_hdnIsClientUpdated)                           
            //            if (hdnIsClientUpdated != null && hdnIsClientUpdated.value == "yes") {
            //                alert(hdnIsClientUpdated.value)
            setTimeout("hideDiv()", 2000);
            //                hdnIsClientUpdated.value = "";
            // }
        }
        function Read_Data() {
            alert('Hi...')
            var str = '';
            var Grid_Table = document.getElementById('ctl00_ContentPlaceHolder1_UCDealersClientList_grdClientList');
            alert(Grid_Table.innerHTML);
            alert(Grid_Table.rows.length)
            for (var row = 1; row < Grid_Table.rows.length; row++) {
                for (var col = 0; col < Grid_Table.rows[row].cells.length; col++) {
                    if (col == 0)
                        if (document.all)
                        str = str + Grid_Table.rows[row].cells[col].innerText;
                    else
                        str = str + Grid_Table.rows[row].cells[col].textContent;
                    else
                        if (document.all)
                        str = str + '--' + Grid_Table.rows[row].cells[col].innerText;
                    else
                        str = str + '--' + Grid_Table.rows[row].cells[col].textContent;
                }
                str = str + '\n';
            }
            alert(str);
            return false;
        }    


    </script>

    <table width="100%">
        <tr>
            <td>
                <uc1:ucDealers_ClientList runat="server" ID="UCDealersClientList" />
            </td>
        </tr>
    </table>
    <asp:UpdateProgress ID="upProcess" runat="server" DisplayAfter="2">
        <ProgressTemplate>
            <div id="progressBackgroundFilter">
            </div>
            <div id="processMessage">
                <span style="text-align: center;">
                    <img alt="" src="Images/loading.gif" /><br />
                    <asp:Label ID="lblProgress" runat="server" Text="Loading...Please wait..."></asp:Label>
                </span>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
