<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Quotation.aspx.cs" Inherits="Quotation" Title="Create Quotation" %>

<%@ Register Src="User Controls/Request/ucRequestHeader.ascx" TagName="ucRequestHeader"
    TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function checkDate(sender, args) {
            if (sender._selectedDate < new Date()) {
                alert("Date should be greater than todays date");
                //sender._selectedDate = new Date();
                // set the date back to the current date
                sender._textbox.set_Value("");
                //document.getElementById('<%=txtEstimatedTimeOfDelivery.ClientID%>').value = '';
                //document.getElementById('<%=txtEstimatedTimeOfDelivery.ClientID%>').focus();
            }
        }

        //22 may 2012
        function chkchange(chkid) {
            // alert(chkid)
            var tr = document.getElementById("ctl00_ContentPlaceHolder1_tdExpDate");
            if (chkid.checked)
                tr.style.display = "block";
            else {
                tr.style.display = "none";
                var txtDate = document.getElementById('<%=txtBonusExpire.ClientID %>');
                txtDate.value = "";
            }
        }

    </script>

    <script type="text/javascript">

        function ExemptLCT() {
            //  alert("ExemptLCT in");
            var GST = 0; var sumFinal = 0; var sum1 = 0;
            var txtLast = ''; var temp;

            var grid = document.getElementById("<%= gvMakeDetails.ClientID %>");
            var totalrowcount = grid.rows.length;
            //alert(grid.rows.length);

            var hfTemp_1 = document.getElementById("<%= hfTemp_1.ClientID %>");
            hfTemp_1.value = "ExemptLCT";
            var hfTemp = document.getElementById("<%= hfTemp.ClientID %>");
            hfTemp.value = "ExemptLCT";

            for (var j = 1; j <= 2; j++) {

                if (totalrowcount <= 9) {
                    txtLast = document.getElementById('ctl00_ContentPlaceHolder1_gvMakeDetails_ctl0' + totalrowcount + '_txtValue' + j);
                }
                else {
                    txtLast = document.getElementById('ctl00_ContentPlaceHolder1_gvMakeDetails_ctl' + totalrowcount + '_txtValue' + j);
                }

                temp = (txtLast.value).toString();
                while (temp.indexOf(',') != -1) {
                    temp = temp.replace(',', '')
                }
                sumFinal = parseFloat(temp)
                //alert("Sum Final-" + sumFinal)

                if (!isNaN(sumFinal)) {
                    for (var i = 2; i < totalrowcount; i++) {
                        var Discount = '';
                        try {
                            if (i <= 9) {
                                var txt = document.getElementById('ctl00_ContentPlaceHolder1_gvMakeDetails_ctl0' + i + '_txtValue' + j);
                                Discount = document.getElementById('ctl00_ContentPlaceHolder1_gvMakeDetails_ctl0' + i + '_lblMake').innerHTML;
                            }
                            else {
                                var txt = document.getElementById('ctl00_ContentPlaceHolder1_gvMakeDetails_ctl' + i + '_txtValue' + j);
                                Discount = document.getElementById('ctl00_ContentPlaceHolder1_gvMakeDetails_ctl' + i + '_lblMake').innerHTML;
                            }

                            if (Discount == 'Sub Total -') {
                                temp = (txt.value).toString();
                                while (temp.indexOf(',') != -1) {
                                    temp = temp.replace(',', '')
                                }
                                sum1 = parseFloat(temp);
                            }

                            if (Discount == 'GST ( LCT if applicable)') {
                                //   alert("Discount - " + Discount);
                                //  alert(txt.value)

                                temp = (txt.value).toString();
                                while (temp.indexOf(',') != -1) {
                                    temp = temp.replace(',', '')
                                }
                                sumFinal = sumFinal - parseFloat(temp);

                                GST = sum1 * 0.10;
                                //  alert("gst - " + GST)

                                var strQutval1;
                                strQutval1 = String.format("{0:c}", GST);
                                txt.value = strQutval1.substring(1, strQutval1.length);
                                val = parseFloat(GST);

                                sumFinal = sumFinal + val;
                                strQutval1 = String.format("{0:c}", sumFinal);
                                txtLast.value = strQutval1.substring(1, strQutval1.length);
                                //  alert(txtLast.value)
                            }
                        }
                        catch (err) {
                            // alert('err= ' + err);
                            continue;
                        }
                    }
                }
            }
        }

        function onCalendarShown(sender, e) {
            var cal = $find("calendar1");
            //Setting the default mode to month
            cal._switchMode("months", true);
            //Iterate every month Item and attach click event to it
            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", call);
                    }
                }
            }
        }

        function onCalendarHidden(sender, e) {
            var cal = sender;
            //Iterate every month Item and remove click event from it
            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", call);
                    }
                }
            }
        }

        function call(eventElement) {
            var target = eventElement.target;
            // var behaviourId = target.id.substring(0, target.id.indexOf('_'));
            switch (target.mode) {
                case "month":
                    var cal = $find("calendar1");
                    cal._visibleDate = target.date;
                    cal.set_selectedDate(target.date);
                    cal._switchMonth(target.date);
                    cal._blur.post(true);
                    cal.raiseDateSelectionChanged();
                    break;
            }
        }
    </script>

    <script type="text/javascript">

        function maxLength(evt, txt) {
            var charCode = (evt.which) ? evt.which : event.keyCode

            if (charCode == 8 || charCode == 27)
                return true

            var txt = document.getElementById(txt.id);
            if (txt.value.length > 499) {
                alert("Maximum Limit reached");
                return false;
            }
        }

        function isNumberKey(evt, chk) {
            //alert(chk.value);
            //            var val = chk.value;
            //            if (val.indexOf('.') != -1) {
            //                val = val.substring(val.indexOf('.') + 1);
            //                if (val.length >= 3)
            //                    return false;
            //            }

            var charCode = (evt.which) ? evt.which : evt.keyCode
            if (charCode == 13) {
                return false;
            }

            var str = (chk.id).toString();
            str = str.substring(0, str.length - 10)
            str = str + "_lblMake";
            var lbl = document.getElementById(str).innerHTML;
            if (lbl == 'Sub Total' || lbl == 'Sub Total -' || lbl == 'Total-On Road Cost (Inclusive of GST)') {
                if (charCode == 9)
                    return true;
                else
                    return false;
            }

            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                if (charCode == 78 || charCode == 47 || charCode == 65 || charCode == 97 || charCode == 110 || charCode == 73 || charCode == 105 || charCode == 99 || charCode == 67 || charCode == 46) {
                    return true;
                }
                return false;
            }
            else {
                //                var val = chk.value;
                //                if (val.indexOf('.') != -1) {
                //                    var Tempval = val.substring(val.indexOf('.') + 1);
                //                    if (Tempval.length >= 3) {
                //                        chk.value = parseFloat(val.substring(0, val.indexOf(".")) + '.' + Tempval.substring(0, 2))
                //                        return false;
                //                    }
                //                }
                return true;
            }
        }
    </script>

    <script type="text/javascript">
        function callError(err, Discount) {
            try {
                //  alert(Discount);
                xhr = new XMLHttpRequest();
                xhr.open("POST", "Default.aspx", true);
                // Set appropriate headers
                xhr.setRequestHeader("Content-Type", "multipart/form-data");
                xhr.setRequestHeader("X-File-Err", err);
                xhr.setRequestHeader("X-File-Name", Discount);
                xhr.setRequestHeader("X-BrowserName", navigator.appName);
                xhr.setRequestHeader("X-BrowserVersion", navigator.appVersion);
                xhr.setRequestHeader("X-BrowserCookieEnable", navigator.cookieEnabled);
                xhr.setRequestHeader("X-Platform", navigator.platform);
                xhr.setRequestHeader("X-UserAgent", navigator.userAgent);
                //  xhr.setRequestHeader("X-File-List-cnt", img);
                // Send the file (doh)

                xhr.send();
                // alert('22')
            } catch (e) {
                //  alert(e.message)
            }
        }
    </script>

    <script type="text/javascript">

        function Calculate1(chk) {
            //alert("1")
            var sum = 0; var LCT = 0; var GST = 0; var GSTTemp = 0;
            var sum1 = 0; var sumFinal = 0;
            var txtSub = ''; var txtSub1 = ''; var txtLast = '';
            var txtDiscount = '';

            var Discount = '';
            try {

                //for GSt & LCT calcu.
                var str = (chk.id).toString();
                //alert("STR - " + str);

                str = str.substring(0, str.length - 10)
                str = str + "_lblMake";
                //alert("STR 1- " + str);
                var lbl = document.getElementById(str).innerHTML;
                //alert("LBL  - " + lbl);

                var hfTemp = document.getElementById("<%= hfTemp.ClientID %>");
                //alert("hfTemp - " + hfTemp);

                if (lbl == 'GST ( LCT if applicable)')
                    hfTemp.value = "man";
                //end

                var grid = document.getElementById("<%= gvMakeDetails.ClientID %>");
                var totalrowcount = grid.rows.length;
                //alert(grid.rows.length);

                if (totalrowcount <= 9) {
                    txtLast = document.getElementById('ctl00_ContentPlaceHolder1_gvMakeDetails_ctl0' + totalrowcount + '_txtValue1');
                }
                else {
                    txtLast = document.getElementById('ctl00_ContentPlaceHolder1_gvMakeDetails_ctl' + totalrowcount + '_txtValue1');
                }
                txtLast.value = 0;
                //alert("txtLast -" + txtLast)
                //alert("txtLast val -" + txtLast.value)
                for (var i = 2; i < totalrowcount; i++) {
                    reg1 = null;
                    Discount = '';

                    if (i <= 9) {
                        var txt = document.getElementById('ctl00_ContentPlaceHolder1_gvMakeDetails_ctl0' + i + '_txtValue1');
                        Discount = document.getElementById('ctl00_ContentPlaceHolder1_gvMakeDetails_ctl0' + i + '_lblMake').innerHTML;
                    }
                    else {
                        var txt = document.getElementById('ctl00_ContentPlaceHolder1_gvMakeDetails_ctl' + i + '_txtValue1');
                        Discount = document.getElementById('ctl00_ContentPlaceHolder1_gvMakeDetails_ctl' + i + '_lblMake').innerHTML;
                    }

                    if (Discount == 'Additional Accessories' || Discount == 'Fixed Charges')
                        continue;
                    //alert("Discount - " + Discount)
                    //alert("Discount txt - " + txt)

                    if (Discount == 'Sub Total')
                        txtSub = txt;
                    else if (Discount == 'Sub Total -')
                        txtSub1 = txt;
                    else if (Discount == 'Fleet Discount')
                        txtDiscount = txt;

                    //alert("txtSub - " + txtSub)
                    //alert("txtSub1 - " + txtSub1)
                    //alert("txtDiscount - " + txtDiscount)

                    //on 6 apr 11 by manoj for currency formation
                    var temp = (txt.value).toString();
                    while (temp.indexOf(',') != -1) {
                        temp = temp.replace(',', '')
                    }
                    var val = parseFloat(temp);
                    if (lbl != 'GST ( LCT if applicable)') {
                        if (Discount == 'GST ( LCT if applicable)') {
                            GSTTemp = val;
                            val = 0;
                        }
                    }
                    //end

                    if (isNaN(val)) {
                        // txt.value = "";
                    }
                    else {
                        //fromat input to currency
                        // alert(Discount + "_" + val)
                        var strQutval = String.format("{0:c}", val);
                        txt.value = strQutval.substring(1, strQutval.length);

                        if (Discount == 'Fleet Discount') {
                            if (sum < val) {
                                alert("Discount is not greater than total Price");
                                txtDiscount.value = "";
                                txtDiscount.focus();
                            }
                            else {
                                sum1 = sum1 - val;
                                sumFinal = sumFinal - val;
                            }
                        }
                        else if (Discount == 'GST ( LCT if applicable)' || Discount == 'Stamp Duty' || Discount == 'Registration' || Discount == 'Premium Plate Fee' || Discount == 'CTP' || Discount == 'Delivered in immaculate condition with a full tank of fuel') {
                            sumFinal = sumFinal + val;
                        }
                        else if (Discount == 'Sub Total' || Discount == 'Sub Total -') {
                        }
                        else {
                            sum = sum + val;
                            sum1 = sum1 + val;
                            sumFinal = sumFinal + val;
                        }
                    }

                    //Dealer Accessories validation
                    if (Discount == 'AddA1') {
                        var textbox
                        if (i <= 9)
                            textbox = document.getElementById('ctl00_ContentPlaceHolder1_gvMakeDetails_ctl0' + i + '_txtAdd1');
                        else
                            textbox = document.getElementById('ctl00_ContentPlaceHolder1_gvMakeDetails_ctl' + i + '_txtAdd1');

                        if (textbox.value == "" && txt.value != "") {
                            alert("Please Enter Accessory name First and then Price.");
                            txt.value = "";
                            val = 0;
                            textbox.focus();
                        }
                    } else if (Discount == 'AddA2') {
                        var textbox
                        if (i <= 9)
                            textbox = document.getElementById('ctl00_ContentPlaceHolder1_gvMakeDetails_ctl0' + i + '_txtAdd2');
                        else
                            textbox = document.getElementById('ctl00_ContentPlaceHolder1_gvMakeDetails_ctl' + i + '_txtAdd2');

                        if (textbox.value == "" && txt.value != "") {
                            alert("Please Enter Accessory name First and then Price.");
                            txt.value = "";
                            val = 0;
                            textbox.focus();
                        }
                    }
                    //ends dealer Accessories validation
                }
                //fromat input to currency
                // alert("S - " + sum + " S1-" + sum1 + " SF - " + sumFinal);
                var strQutval1 = String.format("{0:c}", sum);
                txtSub.value = strQutval1.substring(1, strQutval1.length);

                strQutval1 = String.format("{0:c}", sum1);
                txtSub1.value = strQutval1.substring(1, strQutval1.length);

                strQutval1 = String.format("{0:c}", sumFinal);
                txtLast.value = strQutval1.substring(1, strQutval1.length);

                //by manoj on 6 apr 11 For GST & LCT Calculation
                if (lbl != 'GST ( LCT if applicable)') {
                    for (var i = 2; i < totalrowcount; i++) {
                        var Discount = '';
                        if (i <= 9) {
                            var txt = document.getElementById('ctl00_ContentPlaceHolder1_gvMakeDetails_ctl0' + i + '_txtValue1');
                            Discount = document.getElementById('ctl00_ContentPlaceHolder1_gvMakeDetails_ctl0' + i + '_lblMake').innerHTML;
                        }
                        else {
                            var txt = document.getElementById('ctl00_ContentPlaceHolder1_gvMakeDetails_ctl' + i + '_txtValue1');
                            Discount = document.getElementById('ctl00_ContentPlaceHolder1_gvMakeDetails_ctl' + i + '_lblMake').innerHTML;
                        }

                        if (Discount == 'GST ( LCT if applicable)') {
                            //alert(Discount);
                            //if (sum1 > 57466.00) {
                            //if (sum1 > 52242.00) {
                            // 2 july 2013 : if (sum1 > 59133.00) {
                            if (sum1 > 60316.00) {
                                // alert(sum)
                                LCT = (sum1 * 0.10) + sum1;
                                LCT = (LCT - 60316.00) * 10 / 11 * 0.33;
                                // LCT = (sum1 - 59133.00) * 0.30
                                GST = (sum1 * 0.10) + LCT;
                                // alert(LCT+"_"+GST)
                            }
                            else {
                                GST = (sum1 * 0.10) + LCT;
                            }
                            //  alert(txt.value)
                            var strQutval1;
                            if (hfTemp.value != "man") {
                                strQutval1 = String.format("{0:c}", GST);
                            }
                            else {
                                GST = GSTTemp;
                                strQutval1 = String.format("{0:c}", GST);
                            }
                            txt.value = strQutval1.substring(1, strQutval1.length);
                            val = parseFloat(GST);
                            sumFinal = sumFinal + val;
                            strQutval1 = String.format("{0:c}", sumFinal);
                            txtLast.value = strQutval1.substring(1, strQutval1.length);
                            // alert("LCT -" + LCT + " GST - " + GST + " Val - " + val);

                            // alert(hfTemp.value)
                            if (hfTemp.value == 'ExemptLCT') {
                                ExemptLCT();
                            }
                        }
                    }
                } //GST & LCT Calculation ends here
            }
            catch (err3) {
                //calling web service to write error in log file
                //alert('err= ' + err3.message);
                //alert(Discount);
                callError(err3, Discount);
                return;
            }
        }
            
    </script>

    <script type="text/javascript" language="javascript">

        function Calculate2(chk) {
            var sum = 0; var LCT = 0; var GST = 0; var GSTTemp = 0;
            var sum1 = 0; var sumFinal = 0;
            var txtSub = ''; var txtSub1 = ''; var txtLast = '';
            var txtDiscount = '';
            var Discount = '';
            try {
                //for GSt & LCT calcu.
                var str = (chk.id).toString();
                str = str.substring(0, str.length - 10)
                str = str + "_lblMake";
                var lbl = document.getElementById(str).innerHTML;

                var hfTemp_1 = document.getElementById("<%= hfTemp_1.ClientID %>");
                if (lbl == 'GST ( LCT if applicable)')
                    hfTemp_1.value = "man";
                //end

                var grid = document.getElementById("<%= gvMakeDetails.ClientID %>");
                var totalrowcount = grid.rows.length;

                if (totalrowcount <= 9) {
                    txtLast = document.getElementById('ctl00_ContentPlaceHolder1_gvMakeDetails_ctl0' + totalrowcount + '_txtValue2');
                }
                else {
                    txtLast = document.getElementById('ctl00_ContentPlaceHolder1_gvMakeDetails_ctl' + totalrowcount + '_txtValue2');
                }
                txtLast.value = 0;
                for (var i = 2; i < totalrowcount; i++) {
                    Discount = '';
                    if (i <= 9) {
                        var txt = document.getElementById('ctl00_ContentPlaceHolder1_gvMakeDetails_ctl0' + i + '_txtValue2');
                        Discount = document.getElementById('ctl00_ContentPlaceHolder1_gvMakeDetails_ctl0' + i + '_lblMake').innerHTML;
                    }
                    else {
                        var txt = document.getElementById('ctl00_ContentPlaceHolder1_gvMakeDetails_ctl' + i + '_txtValue2');
                        Discount = document.getElementById('ctl00_ContentPlaceHolder1_gvMakeDetails_ctl' + i + '_lblMake').innerHTML;
                    }

                    if (Discount == 'Additional Accessories' || Discount == 'Fixed Charges')
                        continue;

                    if (Discount == 'Sub Total')
                        txtSub = txt;
                    else if (Discount == 'Sub Total -')
                        txtSub1 = txt;
                    else if (Discount == 'Fleet Discount')
                        txtDiscount = txt;

                    //on 6 apr 11 by manoj for currency formation
                    var temp = (txt.value).toString();
                    while (temp.indexOf(',') != -1) {
                        temp = temp.replace(',', '')
                    }
                    var val = parseFloat(temp);
                    if (lbl != 'GST ( LCT if applicable)') {
                        if (Discount == 'GST ( LCT if applicable)') {
                            GSTTemp = val;
                            val = 0;
                        }
                    }
                    //end

                    if (isNaN(val)) {
                        // txt.value = "";
                    }
                    else {
                        //fromat input to currency
                        // alert(Discount + "_" + val)
                        var strQutval = String.format("{0:c}", val);
                        txt.value = strQutval.substring(1, strQutval.length);

                        if (Discount == 'Fleet Discount') {
                            if (sum < val) {
                                alert("Discount is not greater than total Price");
                                txtDiscount.value = 0;
                                txtDiscount.focus();
                                alert("1")
                            }
                            else {
                                sum1 = sum1 - val;
                                sumFinal = sumFinal - val;
                            }
                        }
                        else if (Discount == 'GST ( LCT if applicable)' || Discount == 'Stamp Duty' || Discount == 'Registration' || Discount == 'Premium Plate Fee' || Discount == 'CTP' || Discount == 'Delivered in immaculate condition with a full tank of fuel') {
                            sumFinal = sumFinal + val;
                        }
                        else if (Discount == 'Sub Total' || Discount == 'Sub Total -') {
                        }
                        else {
                            sum = sum + val;
                            sum1 = sum1 + val;
                            sumFinal = sumFinal + val;
                        }
                    }

                    //Dealer Accessories validation
                    if (Discount == 'AddA1') {
                        var textbox
                        if (i <= 9)
                            textbox = document.getElementById('ctl00_ContentPlaceHolder1_gvMakeDetails_ctl0' + i + '_txtAdd1');
                        else
                            textbox = document.getElementById('ctl00_ContentPlaceHolder1_gvMakeDetails_ctl' + i + '_txtAdd1');

                        if (textbox.value == "" && txt.value != "") {
                            alert("Please Enter Accessory name First and then Price.");
                            txt.value = "";
                            val = 0;
                            textbox.focus();
                        }
                    } else if (Discount == 'AddA2') {
                        var textbox
                        if (i <= 9)
                            textbox = document.getElementById('ctl00_ContentPlaceHolder1_gvMakeDetails_ctl0' + i + '_txtAdd2');
                        else
                            textbox = document.getElementById('ctl00_ContentPlaceHolder1_gvMakeDetails_ctl' + i + '_txtAdd2');

                        if (textbox.value == "" && txt.value != "") {
                            alert("Please Enter Accessory name First and then Price.");
                            txt.value = "";
                            val = 0;
                            textbox.focus();
                        }
                    }
                    //ends dealer Accessories validation
                }
                //fromat input to currency
                // alert("S - " + sum + " S1-" + sum1 + " SF - " + sumFinal);
                var strQutval1 = String.format("{0:c}", sum);
                txtSub.value = strQutval1.substring(1, strQutval1.length);

                strQutval1 = String.format("{0:c}", sum1);
                txtSub1.value = strQutval1.substring(1, strQutval1.length);

                strQutval1 = String.format("{0:c}", sumFinal);
                txtLast.value = strQutval1.substring(1, strQutval1.length);

                //by manoj on 6 apr 11 For GST & LCT Calculation
                if (lbl != 'GST ( LCT if applicable)') {
                    for (var i = 2; i < totalrowcount; i++) {
                        var Discount = '';
                        if (i <= 9) {
                            var txt = document.getElementById('ctl00_ContentPlaceHolder1_gvMakeDetails_ctl0' + i + '_txtValue2');
                            Discount = document.getElementById('ctl00_ContentPlaceHolder1_gvMakeDetails_ctl0' + i + '_lblMake').innerHTML;
                        }
                        else {
                            var txt = document.getElementById('ctl00_ContentPlaceHolder1_gvMakeDetails_ctl' + i + '_txtValue2');
                            Discount = document.getElementById('ctl00_ContentPlaceHolder1_gvMakeDetails_ctl' + i + '_lblMake').innerHTML;
                        }

                        if (Discount == 'GST ( LCT if applicable)') {
                            //alert(Discount);
                            if (sum1 > 60316.00) {
                                // alert(sum)
                                LCT = (sum1 * 0.10) + sum1;
                                LCT = (LCT - 60316.00) * 10 / 11 * 0.33;
                                // LCT = (sum1 - 59133.00) * 0.30
                                GST = (sum1 * 0.10) + LCT;
                                // alert(LCT+"_"+GST)
                            }
                            else {
                                GST = (sum1 * 0.10) + LCT;
                            }
                            //  alert(txt.value)
                            var strQutval1;
                            if (hfTemp_1.value != "man") {
                                strQutval1 = String.format("{0:c}", GST);
                            }
                            else {
                                GST = GSTTemp;
                                strQutval1 = String.format("{0:c}", GST);
                            }
                            txt.value = strQutval1.substring(1, strQutval1.length);
                            val = parseFloat(GST);
                            sumFinal = sumFinal + val;
                            strQutval1 = String.format("{0:c}", sumFinal);
                            txtLast.value = strQutval1.substring(1, strQutval1.length);
                            // alert("LCT -" + LCT + " GST - " + GST + " Val - " + val);

                            if (hfTemp_1.value == 'ExemptLCT') {
                                ExemptLCT();
                            }
                        }
                    }
                } //GST & LCT Calculation ends here
            }
            catch (err3) {
                //calling web service to write error in log file
                //alert('err= ' + err3.message);
                callError(err3, Discount);
                return;
            }
        }
            
    </script>

    <script type="text/javascript" language="javascript">
        function Calculate3() {


            var sum = 0;
            var Discount;
            var grid = document.getElementById("<%= gvMakeDetails.ClientID %>");
            var totalrowcount = (grid.rows.length);
            if (totalrowcount <= 9) {
                var txtLast = document.getElementById('ctl00_ContentPlaceHolder1_gvMakeDetails_ctl0' + totalrowcount + '_txtValue3');
            }
            else {
                var txtLast = document.getElementById('ctl00_ContentPlaceHolder1_gvMakeDetails_ctl' + totalrowcount + '_txtValue3');
            }

            txtLast.value = 0;

            for (var i = 2; i < totalrowcount; i++) {
                try {
                    if (i <= 9) {
                        var txt = document.getElementById('ctl00_ContentPlaceHolder1_gvMakeDetails_ctl0' + i + '_txtValue3');
                        Discount = document.getElementById('ctl00_ContentPlaceHolder1_gvMakeDetails_ctl0' + i + '_lblMake').innerHTML;
                    }
                    else {
                        var txt = document.getElementById('ctl00_ContentPlaceHolder1_gvMakeDetails_ctl' + i + '_txtValue3');
                        Discount = document.getElementById('ctl00_ContentPlaceHolder1_gvMakeDetails_ctl' + i + '_lblMake').innerHTML;
                    }

                    var val = parseFloat(txt.value);
                    //                    if (!isNaN(val)) {
                    //                        if (val.toString().indexOf(".") == -1)
                    //                        { }
                    //                        else {
                    //                            txt.value = val.toFixed(2);
                    //                        }
                    //                    }    


                    if (isNaN(val)) {
                        txt.value = "";
                    }
                    else {

                        if (Discount == 'Fleet Discount') {
                            sum = sum - val;
                        }
                        else {
                            sum = sum + val;
                        }

                    }

                }
                catch (err) {
                    continue;
                }
            }


            if (totalrowcount <= 9) {
                var txtLast = document.getElementById('ctl00_ContentPlaceHolder1_gvMakeDetails_ctl0' + totalrowcount + '_txtValue3');
            }
            else {
                var txtLast = document.getElementById('ctl00_ContentPlaceHolder1_gvMakeDetails_ctl' + totalrowcount + '_txtValue3');
            }
            var s1 = sum.toFixed(2);
            txtLast.value = s1;
            // txtLast.value = sum;

        }
            
    </script>

    <script type="text/javascript" language="javascript">
        function Calculate4() {


            var sum = 0;
            var Discount;
            var grid = document.getElementById("<%= gvMakeDetails.ClientID %>");
            var totalrowcount = (grid.rows.length);
            if (totalrowcount <= 9) {
                var txtLast = document.getElementById('ctl00_ContentPlaceHolder1_gvMakeDetails_ctl0' + totalrowcount + '_txtValue4');
            }
            else {
                var txtLast = document.getElementById('ctl00_ContentPlaceHolder1_gvMakeDetails_ctl' + totalrowcount + '_txtValue4');
            }

            txtLast.value = 0;

            for (var i = 2; i < totalrowcount; i++) {
                try {
                    if (i <= 9) {
                        var txt = document.getElementById('ctl00_ContentPlaceHolder1_gvMakeDetails_ctl0' + i + '_txtValue4');
                        Discount = document.getElementById('ctl00_ContentPlaceHolder1_gvMakeDetails_ctl0' + i + '_lblMake').innerHTML;
                    }
                    else {
                        var txt = document.getElementById('ctl00_ContentPlaceHolder1_gvMakeDetails_ctl' + i + '_txtValue4');
                        Discount = document.getElementById('ctl00_ContentPlaceHolder1_gvMakeDetails_ctl' + i + '_lblMake').innerHTML;
                    }

                    var val = parseFloat(txt.value);
                    //                    if (!isNaN(val)) {
                    //                        if (val.toString().indexOf(".") == -1)
                    //                        { }
                    //                        else {
                    //                            txt.value = val.toFixed(2);
                    //                        }
                    //                    }    


                    if (isNaN(val)) {
                        txt.value = "";
                    }
                    else {

                        if (Discount == 'Fleet Discount') {
                            sum = sum - val;
                        }
                        else {
                            sum = sum + val;
                        }

                    }

                }
                catch (err) {
                    continue;
                }
            }

            if (totalrowcount <= 9) {
                var txtLast = document.getElementById('ctl00_ContentPlaceHolder1_gvMakeDetails_ctl0' + totalrowcount + '_txtValue4');
            }
            else {
                var txtLast = document.getElementById('ctl00_ContentPlaceHolder1_gvMakeDetails_ctl' + totalrowcount + '_txtValue4');
            }
            var s1 = sum.toFixed(2);
            txtLast.value = s1;
            //            txtLast.value = sum;

        }
    </script>

    <script type="text/javascript" language="javascript">
        function Calculate5() {


            var sum = 0;
            var Discount;
            var grid = document.getElementById("<%= gvMakeDetails.ClientID %>");
            var totalrowcount = (grid.rows.length);
            if (totalrowcount <= 9) {
                var txtLast = document.getElementById('ctl00_ContentPlaceHolder1_gvMakeDetails_ctl0' + totalrowcount + '_txtValue5');
            }
            else {
                var txtLast = document.getElementById('ctl00_ContentPlaceHolder1_gvMakeDetails_ctl' + totalrowcount + '_txtValue5');
            }

            txtLast.value = 0;

            for (var i = 2; i < totalrowcount; i++) {
                try {
                    if (i <= 9) {
                        var txt = document.getElementById('ctl00_ContentPlaceHolder1_gvMakeDetails_ctl0' + i + '_txtValue5');
                        Discount = document.getElementById('ctl00_ContentPlaceHolder1_gvMakeDetails_ctl0' + i + '_lblMake').innerHTML;

                    }
                    else {
                        var txt = document.getElementById('ctl00_ContentPlaceHolder1_gvMakeDetails_ctl' + i + '_txtValue5');
                        Discount = document.getElementById('ctl00_ContentPlaceHolder1_gvMakeDetails_ctl' + i + '_lblMake').innerHTML;
                    }

                    var val = parseFloat(txt.value);
                    //                    if (!isNaN(val)) {
                    //                        if (val.toString().indexOf(".") == -1)
                    //                        { }
                    //                        else {
                    //                            txt.value = val.toFixed(2);
                    //                        }
                    //                    }    


                    if (isNaN(val)) {
                        txt.value = "";
                    }
                    else {

                        if (Discount == 'Fleet Discount') {
                            sum = sum - val;
                        }
                        else {
                            sum = sum + val;
                        }

                    }

                }
                catch (err) {
                    continue;
                }
            }


            if (totalrowcount <= 9) {
                var txtLast = document.getElementById('ctl00_ContentPlaceHolder1_gvMakeDetails_ctl0' + totalrowcount + '_txtValue5');
            }
            else {
                var txtLast = document.getElementById('ctl00_ContentPlaceHolder1_gvMakeDetails_ctl' + totalrowcount + '_txtValue5');
            }
            var s1 = sum.toFixed(2);
            txtLast.value = s1;
            // txtLast.value = sum;

        }
            
             
           
    </script>

    <asp:Panel ID="pan1" runat="server">
        <table style="width: 96%;" align="center">
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <div align="center" style="text-decoration: underline; font-weight: bold; font-size: medium;">
                        Quotation
                    </div>
                </td>
            </tr>
            <tr>
                <td valign="middle">
                    <asp:Label ID="lblMsg" runat="server" CssClass="dbresult"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <uc3:ucRequestHeader ID="UcRequestHeader1" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="border-style: solid; border-color: #acacac; border-width: 1px; width: 100%">
                    <table width="100%">
                        <tr>
                            <td valign="top" style="width: 25%">
                                <asp:Label ID="lblConsultantNotes" runat="server" Width="130px"><strong>Consultant Notes : </strong> </asp:Label>
                            </td>
                            <td style="width: 75%;" valign="top">
                                <asp:Label ID="lblConsultantNotes1" Width="673px" runat="server"></asp:Label>
                                <%--<asp:TextBox ID="txtConsultantNotes1" Font-Names="Arial" runat="server"
                                    ReadOnly="true" TextMode="MultiLine" Width="673px" BorderStyle="None" BackColor="Transparent"></asp:TextBox>--%>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="border-style: solid; font-family: Arial; border-color: #acacac; border-width: 1px;
                    width: 100%">
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:Label ID="lblSub" runat="server" Width="85px"><strong>Suburb : </strong> </asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblSub1" runat="server" Width="200px"> </asp:Label>
                            </td>
                            <td style="width: 20%">
                                <asp:Label ID="lblPCode" runat="server" Width="85px"><strong>Postal Code :  </strong> </asp:Label>
                            </td>
                            <td style="width: 30%" valign="top">
                                <asp:Label ID="lblPCode1" runat="server" Width="200px"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr runat="server" id="trConsultantInfo" visible="true">
                <td>
                    <table width="100%">
                        <tr class="ucHeader" style="border: solid 1px #acacac width: 100%;">
                            <td align="left" bgcolor="">
                                <asp:Label ID="Label3" runat="server" CssClass="gvLabel" Text="">Consultant</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="100%" align="right">
                                <asp:DataList ID="dlConsultantInfo" runat="server" RepeatDirection="Horizontal" Width="85%"
                                    BorderStyle="solid" BorderColor="#acacac" BorderWidth="1px">
                                    <ItemTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 50%; font-weight: bold" align="left" bgcolor="#eaeaea">
                                                    <asp:Label ID="lblHeader" runat="server" CssClass="gvLabel" Text='<%# Bind("Header") %>'></asp:Label>
                                                </td>
                                                <td style="width: 50%; padding-left: 10px;" align="left">
                                                    <asp:Label ID="lblDetails" runat="server" CssClass="gvLabel" Text='<%# Bind("Details") %>'></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:DataList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="padding-top: 5px">
                    <asp:GridView ID="gvMakeDetails" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                        CellPadding="3" OnRowDataBound="gvMakeDetails_RowDataBound" Width="100%" OnPageIndexChanging="gvMakeDetails_PageIndexChanging"
                        PageSize="100">
                        <FooterStyle CssClass="gvFooterrow" />
                        <Columns>
                            <asp:TemplateField HeaderText="Description">
                                <EditItemTemplate>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Image ID="imgActive" runat="server" ImageUrl="~/Images/active_bullate.jpg" Width="10px"
                                        Height="10px" Visible="False" __designer:wfdid="w1"></asp:Image>
                                    <asp:Label Style="padding-left: 25px" ID="lblMake" runat="server" Text='<%# Bind("Key") %>'
                                        CssClass="gvLabel" __designer:wfdid="w2"></asp:Label>
                                    <asp:Label Style="padding-left: 25px" ID="lblMake_1" Visible="false" runat="server"
                                        Text='<%# Bind("Key") %>' CssClass="gvLabel" __designer:wfdid="w2"></asp:Label>
                                    <asp:ImageButton ID="imgbtnLCTExempt" runat="server" OnClientClick="javascript:ExemptLCT()"
                                        AlternateText="LCT Exempt" vertical-align="bottom" Height="16" Visible="false"
                                        ImageUrl="~/Images/LCTExempt.gif" onmouseout="this.src='Images/LCTExempt.gif'"
                                        onmouseover="this.src='Images/LCTExempt_hvr.gif'" CausesValidation="false" />
                                    <div style="margin-left: 25px">
                                        <asp:Label ID="lblSpecification" runat="server" Text='<%# Bind("Specification") %>'
                                            CssClass="gvLabel" __designer:wfdid="w3"></asp:Label>
                                    </div>
                                    <asp:HiddenField ID="hdfID" runat="server" Value='<%# Bind("ID") %>' __designer:wfdid="w4">
                                    </asp:HiddenField>
                                    <asp:HiddenField ID="hdfIsAccessory" runat="server" Value='<%# Bind("IsAccessory") %>'
                                        __designer:wfdid="w5"></asp:HiddenField>
                                    <asp:HiddenField ID="hdfIsChargeType" runat="server" Value='<%# Bind("IsChargeType") %>'
                                        __designer:wfdid="w6"></asp:HiddenField>
                                    <asp:HiddenField ID="hdfIsAdditionAccessory" runat="server" Value='<%# Bind("IsAdditionalAccessory") %>'
                                        __designer:wfdid="w7"></asp:HiddenField>
                                    <asp:HiddenField ID="hfTemp" runat="server" Value="manoj"></asp:HiddenField>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <PagerStyle CssClass="pgr" />
                        <HeaderStyle BackColor="#0A73A2" CssClass="gvHeader" Font-Bold="True" Height="30px" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%" cellpadding="2" cellspacing="2">
                        <tr>
                            <td style="width: 30%;">
                                <asp:Label ID="Label5" runat="server" CssClass="gvLabel"><strong style="color:Red;">*</strong> Estimated Time For Delivery</asp:Label>
                            </td>
                            <td style="width: 15%;">
                                <asp:TextBox ReadOnly="true" ID="txtEstimatedTimeOfDelivery" runat="server"></asp:TextBox>
                                <ajaxtoolkit:CalendarExtender ID="CalExtEstimatedTimeOfdelivery" runat="server" Animated="true"
                                    Format="MM/dd/yyyy" OnClientDateSelectionChanged="checkDate" PopupButtonID="txtEstimatedTimeOfDelivery"
                                    TargetControlID="txtEstimatedTimeOfDelivery">
                                </ajaxtoolkit:CalendarExtender>
                                <%-- <asp:RangeValidator ID="RangeValEstimatedTimeOfDelivery" runat="server" ControlToValidate="txtEstimatedTimeOfDelivery"
                                            CssClass="gvValidationError" Display="None" ErrorMessage="Date should be greater than todays date" 
                                            SetFocusOnError="True" Type="Date" Width="222px" ValidationGroup="VGSubmit">
                                        </asp:RangeValidator>
                                        <ajaxtoolkit:ValidatorCalloutExtender runat="Server" ID="RangeValEstimatedTimeOfDeliveryExt"
                                            TargetControlID="RangeValEstimatedTimeOfDelivery" HighlightCssClass="validatorCalloutHighlight" />--%>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEstimatedTimeOfDelivery"
                                    CssClass="gvValidationError" Display="None" ErrorMessage="Please Enter the Estimated Delivery Date"
                                    SetFocusOnError="True" ValidationGroup="VGSubmit">
                                </asp:RequiredFieldValidator>
                                <ajaxtoolkit:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender2_ucQuotation"
                                    TargetControlID="RequiredFieldValidator1" HighlightCssClass="validatorCalloutHighlight" />
                            </td>
                            <td style="width: 10%;">
                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True">Ex Stock</asp:ListItem>
                                    <asp:ListItem>Order</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td style="width: 45%;">
                                <asp:TextBox ID="txtExStock" runat="server" onkeypress="return isNumberKey(event);"
                                    Visible="false">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label8" runat="server" CssClass="gvLabel">Approx Compliance Date</asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlComplianceMonth" Width="65px" runat="server">
                                    <asp:ListItem Value="0">-Month-</asp:ListItem>
                                    <asp:ListItem Value="1">Jan</asp:ListItem>
                                    <asp:ListItem Value="2">Feb</asp:ListItem>
                                    <asp:ListItem Value="3">Mar</asp:ListItem>
                                    <asp:ListItem Value="4">Apr</asp:ListItem>
                                    <asp:ListItem Value="5">May</asp:ListItem>
                                    <asp:ListItem Value="6">Jun</asp:ListItem>
                                    <asp:ListItem Value="7">July</asp:ListItem>
                                    <asp:ListItem Value="8">Aug</asp:ListItem>
                                    <asp:ListItem Value="9">Sept</asp:ListItem>
                                    <asp:ListItem Value="10">Oct</asp:ListItem>
                                    <asp:ListItem Value="11">Nov</asp:ListItem>
                                    <asp:ListItem Value="12">Dec</asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlComplianceYear" Width="75px" runat="server">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlComplianceMonth"
                                    CssClass="gvValidationError" InitialValue="0" Display="None" ErrorMessage="Please Select the Month"
                                    SetFocusOnError="True" ValidationGroup="VGSubmit">
                                </asp:RequiredFieldValidator><ajaxtoolkit:ValidatorCalloutExtender runat="server"
                                    ID="ValidatorCalloutExtender1" TargetControlID="RequiredFieldValidator2" HighlightCssClass="validatorCalloutHighlight" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlComplianceYear"
                                    CssClass="gvValidationError" Display="None" ErrorMessage="Please Select the Year"
                                    SetFocusOnError="True" InitialValue="-Year-" ValidationGroup="VGSubmit">
                                </asp:RequiredFieldValidator><ajaxtoolkit:ValidatorCalloutExtender runat="server"
                                    ID="ValidatorCalloutExtender4" TargetControlID="RequiredFieldValidator4" HighlightCssClass="validatorCalloutHighlight" />
                            </td>
                            <td style="padding-left: 14px;">
                                <asp:Label ID="Label2" runat="server" Width="150px" CssClass="gvLabel">Approx Build Date</asp:Label>
                            </td>
                            <td>
                                <%--
                                       <asp:TextBox ID="txtComplianceDate" runat="server" ReadOnly="true"></asp:TextBox>
                                        <ajaxtoolkit:CalendarExtender ID="CalendarExtender1" runat="server" Animated="true"
                                            OnClientHidden="onCalendarHidden" OnClientShown="onCalendarShown" Format="MMMM yyyy"
                                            BehaviorID="calendar2"  PopupButtonID="txtComplianceDate" TargetControlID="txtComplianceDate">
                                        </ajaxtoolkit:CalendarExtender>    
                             --%>
                                <asp:DropDownList ID="ddlMonthBuild" Width="65px" runat="server">
                                    <asp:ListItem Value="0">-Month-</asp:ListItem>
                                    <asp:ListItem Value="1">Jan</asp:ListItem>
                                    <asp:ListItem Value="2">Feb</asp:ListItem>
                                    <asp:ListItem Value="3">Mar</asp:ListItem>
                                    <asp:ListItem Value="4">Apr</asp:ListItem>
                                    <asp:ListItem Value="5">May</asp:ListItem>
                                    <asp:ListItem Value="6">Jun</asp:ListItem>
                                    <asp:ListItem Value="7">July</asp:ListItem>
                                    <asp:ListItem Value="8">Aug</asp:ListItem>
                                    <asp:ListItem Value="9">Sept</asp:ListItem>
                                    <asp:ListItem Value="10">Oct</asp:ListItem>
                                    <asp:ListItem Value="11">Nov</asp:ListItem>
                                    <asp:ListItem Value="12">Dec</asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlYearBuild" Width="75px" runat="server">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlMonthBuild"
                                    CssClass="gvValidationError" InitialValue="0" Display="None" ErrorMessage="Please Select the Month"
                                    SetFocusOnError="True" ValidationGroup="VGSubmit">
                                </asp:RequiredFieldValidator><ajaxtoolkit:ValidatorCalloutExtender runat="server"
                                    ID="ValidatorCalloutExtender3" TargetControlID="RequiredFieldValidator3" HighlightCssClass="validatorCalloutHighlight" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlYearBuild"
                                    CssClass="gvValidationError" Display="None" ErrorMessage="Please Select the Year"
                                    SetFocusOnError="True" InitialValue="-Year-" ValidationGroup="VGSubmit">
                                </asp:RequiredFieldValidator>
                                <ajaxtoolkit:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender5"
                                    TargetControlID="RequiredFieldValidator5" HighlightCssClass="validatorCalloutHighlight" />
                            </td>
                        </tr>
                        <tr style="height: 30px;">
                            <td>
                                <asp:Label ID="Label10" runat="server" CssClass="gvLabel">PF Reference</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPfReference" runat="server"></asp:TextBox>
                            </td>
                            <td colspan="2">
                                <asp:UpdatePanel ID="upBonus" runat="server">
                                    <ContentTemplate>
                                        <table cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="chkBonus" AutoPostBack="false" runat="server" onclick="chkchange(this)" />
                                                    <%--<asp:CheckBox ID="chkBonus" AutoPostBack="true" runat="server" OnCheckedChanged="chkBonus_CheckedChanged" />--%>
                                                    <asp:Label ID="lblBonus" runat="server" Text="Manufacturer Bonus"></asp:Label>
                                                </td>
                                                <td id="tdExpDate" runat="server" style="display: none;">
                                                    <asp:Label ID="lblExpDate" runat="server" Text="Expiration Date-"></asp:Label>
                                                    <asp:TextBox ID="txtBonusExpire" runat="server" Width="140"></asp:TextBox>
                                                    <asp:Image ID="imgCalender" runat="server" ImageUrl="~/Images/Calendar.gif" />
                                                    <ajaxtoolkit:CalendarExtender ID="calFrom" runat="server" TargetControlID="txtBonusExpire"
                                                        Format="MM/dd/yyyy" OnClientDateSelectionChanged="checkDate" PopupButtonID="imgCalender">
                                                    </ajaxtoolkit:CalendarExtender>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <asp:Label ID="lblDealerNotes" runat="server" CssClass="gvLabel">Dealer Notes</asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtDealerNotes" runat="server" Height="82px" MaxLength="2000" TextMode="MultiLine"
                                    Width="100%"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="None"
                                    ControlToValidate="txtDealerNotes" ValidationGroup="VGSubmit" SetFocusOnError="True"
                                    ssClass="gvValidationError" ValidationExpression="^([\S\s]{0,1000})$" ErrorMessage="Maximum Limit reached">
                                </asp:RegularExpressionValidator>
                                <ajaxtoolkit:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender2"
                                    TargetControlID="RegularExpressionValidator1" HighlightCssClass="validatorCalloutHighlight" />
                            </td>
                        </tr>
                        <tr id="trUpdate" visible="false" runat="server">
                            <td valign="top">
                                <asp:Label ID="lblUpNotes" runat="server" CssClass="gvLabel">Update Dealer Notes</asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtUpNotes" runat="server" Height="82px" MaxLength="500" TextMode="MultiLine"
                                    Width="100%"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Display="None"
                                    ControlToValidate="txtUpNotes" ValidationGroup="VGSubmit" SetFocusOnError="True"
                                    ssClass="gvValidationError" ValidationExpression="^([\S\s]{0,400})$" ErrorMessage="Maximum Limit reached">
                                </asp:RegularExpressionValidator>
                                <ajaxtoolkit:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender6"
                                    TargetControlID="RegularExpressionValidator2" HighlightCssClass="validatorCalloutHighlight" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td align="right" colspan="3">
                                <asp:ImageButton ID="imgbtnAdd" runat="server" ImageUrl="~/Images/Submit.gif" OnClick="imgbtnAdd_Click"
                                    onmouseout="this.src='Images/Submit.gif'" onmouseover="this.src='Images/Submit_hvr.gif'"
                                    ValidationGroup="VGSubmit" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <div id="divOrderCancelConfirm" runat="server" style="display: none; width: 300px;">
        <div id="progressBackgroundFilterOrderConfrm">
        </div>
        <div id="processMessageOrderConfirm" style="width: 300px; height: auto; padding: 5px !important;
            left: 35%;">
            <asp:Panel runat="server" ID="Panel1" BackColor="White">
                <table width="300px" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="background-color: #0A73A2; color: White; font-weight: bold; padding-left: 5px;
                            height: 30px; font-size: large">
                            Private Fleet
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px">
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 5px" align="center">
                            <p>
                                <asp:Label runat="server" ID="lblOrderCancelConfirmation" Text="This is not a proper way to access this page"></asp:Label>
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px">
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Button runat="server" ID="btnOk" Text="OK" OnClick="btnYes_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px">
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </div>
    <!-- Required to determin is LCT is calculated manually or automatically -->
    <asp:HiddenField ID="hfTemp" runat="server" />
    <asp:HiddenField ID="hfTemp_1" runat="server" />
</asp:Content>
