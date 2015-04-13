<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Make.aspx.cs" Inherits="Make" %>

<%@ Register Src="User Controls/UCMake.ascx" TagName="UCMake" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:UCMake ID="UCMake1" runat="server" />
    
    </div>
    </form>
</body>
</html>
