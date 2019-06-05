<%@ Register TagPrefix="Wilson" Namespace="Wilson.MasterPages" Assembly="WilsonMasterPages" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="template.aspx.vb" Inherits="app.catastroMaquinas_template" %>
<META content="text/html; charset=iso-8859-1" http-equiv="Content-Type">
<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
<wilson:masterpage style="Z-INDEX: 0" id="MPContainer" runat="server">
	<WILSON:CONTENTREGION id="MPTitle" runat="server">Catastro Clientes</WILSON:CONTENTREGION>
	<WILSON:CONTENTREGION id="MPCaption" runat="server">Máquinas</WILSON:CONTENTREGION>
	<SCRIPT language="javascript">
$(document).ready(function()   
{   
    $(".tab_content").hide();  
    
  
<asp:Literal id="Literal1" runat="server"></asp:Literal>

  
    $("ul.tabs li").click(function()   
       {   
        $("ul.tabs li").removeClass("active");   
        $(this).addClass("active");   
        $(".tab_content").hide();   
  
        var activeTab = $(this).find("a").attr("href");   
        $(activeTab).fadeIn();   
        return false;   
    });   
    
    $("#tbPatronCliente").autocomplete("/acClientes.aspx");
    		
}); 
	</SCRIPT>
	<DIV class="pot-contenedor-controles">
		<FIELDSET><LEGEND class="pot-groupbox-potencial">Titulo template</LEGEND></FIELDSET>
	</DIV>
</wilson:masterpage>
