<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Src="WebUserControl.ascx" TagName="WebUserControl" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
<script language="javascript" type="text/javascript">
// <!CDATA[



// ]]>
</script>
</head>
<body style="position: static" vlink="#999900">
    <form id="form1" runat="server">
    <div>
        &nbsp;<uc1:WebUserControl ID="WebUserControl1" runat="server"  />
    </div>
        <asp:Menu ID="Menu1" runat="server">
        </asp:Menu>
    </form>
</body>
</html>
