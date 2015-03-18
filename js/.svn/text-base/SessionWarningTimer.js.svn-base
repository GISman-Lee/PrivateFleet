var reqXML;

function LoadXMLDoc(url) {

    if (window.ActiveXObject) { //IE
        reqXML = new ActiveXObject("Microsoft.XMLHTTP");
        if (reqXML) {
            reqXML.onreadystatechange = BuildXMLResults;
            reqXML.open("GET", url, true);
            reqXML.send();
        }
    }
    else if (window.XMLHttpRequest) { //Mozilla, Firefox, Opera 8.01, Safari
        reqXML = new XMLHttpRequest();
        reqXML.onreadystatechange = BuildXMLResults;
        reqXML.open("GET", url, true);
        reqXML.send(null);
    }
    else { //Older Browsers
        alert("Your Browser does not support Ajax!");
    }
}

function BuildXMLResults() {

    if (reqXML.readyState == 4) { //completed state
        if (reqXML.status == 200) { //We got a sucess page back

            //Check to verify the message from the server 
            if (reqXML.responseText.indexOf("Session Updated - Server Time:") == 0) {
                //window.status = reqXML.responseText; //display the message in the status bar
                SetTimer(); //restart timer
            }
            else {
                //display that that session expired
                //alert("Your session appears to have expired. You may loose your current data.");
            }
        }
        else {
            //display server code not be accessed
            // alert("There was a problem retrieving the XML data:\n" + reqXML.statusText);
        }
    }
}

function ConfirmUpdate() {
    //Ask them to extend
    // if(confirm("Your session is about to expire. Press 'OK' to renew your session.")){
    //load server side page if ok
    LoadXMLDoc('EndSession.aspx');
    // }
}

var timerObj;
function SetTimer() {
    //How long before timeout (should be a few minutes before your server's timeout
    var dblMinutes = 10;
    //  alert("test");
    //set timer to call function to confirm update 
    //timerObj = setTimeout("ConfirmUpdate()",1000*60*dblMinutes);
    timerObj = setTimeout("ConfirmUpdate()", dblMinutes * 60000);
}

//start the timer
SetTimer();
