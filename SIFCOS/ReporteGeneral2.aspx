<%@ Page Language="C#" AutoEventWireup="true" 
CodeBehind="ReporteGeneral2.aspx.cs" Inherits="SIFCOS.ReporteGeneral2" 
uiCulture="es" culture="es-MX"  %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head  runat="server">
    <title></title>
</head>
<body>

    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager14" runat="server" EnableScriptGlobalization="True" EnableScriptLocalization="True" />
    
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%"  >
        <LocalReport EnableExternalImages="true">
            </LocalReport>
    </rsweb:ReportViewer>
    
    </form>
    <script type="text/javascript">
       
    </script> 
</body>
    
</html>
