function chkdt(fromdate, todate) {

    fromdate_array = fromdate.split("/");

    if (fromdate_array.length != 3) {


        return false;

    }
    fromdate_date = (fromdate_array[0]);
    fromdate_month = (fromdate_array[1]);
    fromdate_year = (fromdate_array[2]);

    //tempdate = new Date(fromdate_month + '/' + fromdate_date + '/' + fromdate_year);
    tempdat = new Date(fromdate_month, fromdate_date, fromdate_year);
    //            tempdat.setDate(fromdate_date);
    //            tempdat.setMonth(fromdate_month);  
    //          tempdat.setFullYear (fromdate_year);





    todate_array = todate.split("/");

    if (todate_array.length != 3) {

        return false;
    }
    if (fromdate_year.length != 4) {
        return false;
    }
    todate_date = (todate_array[0]);
    todate_month = (todate_array[1]);
    todate_year = (todate_array[2])




    tempdat1 = new Date(todate_month, todate_date, todate_year);
    // alert(tempdat1.getTime());

    //            _Diff = Math.ceil((tempdat1.getTime() - tempdat.getTime()) / (1000 * 60 * 60 * 24));
    //            alert(_Diff);
    //            if (parseFloat(_Diff) < parseFloat(0)) {
    //                return false;
    //            }
    //            else {
    //                return true; 
    //            }

    if (todate_year.length != 4) {
        return false;
    }





    if (parseFloat(fromdate_year) > parseFloat(todate_year)) {
        // from year is grather than to year


        return false;


    }
    else {

        if (parseFloat(fromdate_year) < parseFloat(todate_year)) {
            return true;
        }

        if (parseFloat(fromdate_year) <= parseFloat(todate_year)) {


            if (parseFloat(fromdate_month) < parseFloat(todate_month)) {

                return true;
            }
            else {
                // alert("SS");

                if (parseFloat(fromdate_month) > parseFloat(todate_month)) {

                    if (parseFloat(todate_year) < parseFloat(todate_year)) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                else {
                    if (parseFloat(fromdate_year) < parseFloat(todate_year)) {
                        return true;
                    }
                    else {
                        if (parseFloat(fromdate_date) > parseFloat(todate_date)) {
                            if (parseFloat(fromdate_year) < parseFloat(todate_year)) {
                                return true;
                            }
                            else {
                                return false;
                            }
                        }
                        else {

                            return true;
                        }
                    }
                }
            }

        }
        else {

            if (parseFloat(fromdate_month) > parseFloat(todate_month)) {

                return false;
            }
            else if (parseFloat(fromdate_month) < parseFloat(todate_month)) {

                return true;
            }


            else {

                if (parseFloat(fromdate_date) > parseFloat(todate_date)) {

                    return false;
                }
            }

        }
    }


    return true;
}




function compaire_dates_ViewRecivedQuoteRequests(source, args) {
    // debugger;
    args.IsValid = true;
    fromdate = (document.getElementById('ctl00_ContentPlaceHolder1_txtCalenderFrom').value).trim();

    if (fromdate.trim() == "") {
        args.IsValid = false;
        return;
    }
    todate = (document.getElementById('ctl00_ContentPlaceHolder1_TxtToDate').value).trim();

    if (todate.trim() == "") {
        args.IsValid = false;
        return;
    }
    // alert(chkdt(fromdate, todate));
    args.IsValid = chkdt(fromdate, todate);
    return;
}



function compaire_dates_ViewDealersQuotation(source, args) {

    // debugger;
    args.IsValid = true;
    fromdate = (document.getElementById('ctl00_ContentPlaceHolder1_txtCalenderFrom').value).trim();

    if (fromdate.trim() == "") {
        args.IsValid = false;
        return;
    }
    todate = (document.getElementById('ctl00_ContentPlaceHolder1_TxtToDate').value).trim();

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


//----validation for ViewSentRequests.aspx added 19/12/2011

function compaire_dates_ViewSentRequests(source, args) {

    // debugger;
    args.IsValid = true;
    fromdate = (document.getElementById('ctl00_ContentPlaceHolder1_txtCalenderFrom').value).trim();

    if (fromdate.trim() == "") {
        args.IsValid = false;
        return;
    }
    todate = (document.getElementById('ctl00_ContentPlaceHolder1_TxtToDate').value).trim();

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


//----validation for ViewSentRequests.aspx END




//----validation for Consultant_Summary_Report.aspx added 19/12/2011

function compaire_dates_Consultant_Summary_Report(source, args) {

    // debugger;
    args.IsValid = true;
    fromdate = (document.getElementById('ctl00_ContentPlaceHolder1_txtCalenderFrom').value).trim();

    if (fromdate.trim() == "") {
        args.IsValid = false;
        return;
    }
    todate = (document.getElementById('ctl00_ContentPlaceHolder1_txtCalenderToDate').value).trim();

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


//----validation for Consultant_Summary_Report.aspx END





//----validation for ConsultantTradeInReport.aspx added 19/12/2011

function compaire_yearsin(source, args) {

    // debugger;
    args.IsValid = true;

    fromyear = document.getElementById("ctl00_ContentPlaceHolder1_txtT1FromYear").value;
    Toyear = document.getElementById("ctl00_ContentPlaceHolder1_txtT1ToYear").value;
    if (fromyear.trim() != "" && Toyear.trim() != "") {


        if (parseFloat(fromyear) > parseFloat(Toyear)) {
            return args.IsValid = false;

        }

    }

    return args.IsValid;
}


function compaire_minMaxvalues(source, args) {

    // debugger;
    args.IsValid = true;


    minvalue = document.getElementById("ctl00_ContentPlaceHolder1_txtValueFrom").value;
    maxvalue = document.getElementById("ctl00_ContentPlaceHolder1_txtValueTo").value;
    if (minvalue.trim() != "" && maxvalue.trim() != "") {


        if (parseFloat(minvalue) > parseFloat(maxvalue)) {
            return args.IsValid = false;

        }

    }

    return args.IsValid;
}








//----validation for ConsultantTradeInReport.aspx END




//----validation for CompletedQuoatationReport.aspx added 19/12/2011

function compaire_dates_CompletedQuoatationReport(source, args) {

    // debugger;
    args.IsValid = true;
    fromdate = (document.getElementById('ctl00_ContentPlaceHolder1_txtCalenderFrom').value).trim();

    if (fromdate.trim() == "") {
        args.IsValid = false;
        return;
    }
    todate = (document.getElementById('ctl00_ContentPlaceHolder1_TxtToDate').value).trim();

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


function ShowAlert() {
    alert("Date Should be greater than current date.");
}
function ShowDateAlert(dtLast7Days) {
    alert("Date Should be greater than " + dtLast7Days + ".");
}
function go_toDashboard() {
    try {

        document.getElementById("ctl00_btnModalInvoke").click();
        //  _doPostBack('ctl00_btnModalInvoke', '');


        //alert("test", "There are outstanding delivery updates required. Please complete these before accessing new quote requests.");
        // window.location.href = 'ClinetIfo_ForDealer.aspx';

    } catch (e) {
        alert(e);
    }
}
function ShowAlert_CustomerSection() {
    if (document.getElementById("lblSendMailMessage") != null) {
        alert("Please contact the dealer directly or Private Fleet if you still have queries outstanding.");
        return false;
    }
    else {
        return true;
    }

}
//
function show_info() {
    alert('Your password is your member number and can be found on your original paperwork.  Please call us on 1300 303 181 if you have any trouble');
    myWindow = window.open("", "tinyWindow", 'toolbar,width=150,height=100')


    return false;
}

