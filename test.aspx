<%@ Page Language="vb" AutoEventWireup="false" Codebehind="test.aspx.vb" Inherits="app.test"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<HTML>
	<HEAD>
		<title>test</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script type="text/javascript" src="/js/jquery-1.5.1.js"></script>
		<style>
    .statusbar { Z-INDEX: 200; BORDER-BOTTOM: lightgray 1px solid; POSITION: fixed; FILTER: alpha(opacity="70"); BORDER-LEFT: lightgray 1px solid; PADDING-BOTTOM: 5px; OVERFLOW-Y: auto; PADDING-LEFT: 5px; BOTTOM: 5px; PADDING-RIGHT: 5px; BACKGROUND: black; HEIGHT: 16px; COLOR: white; OVERFLOW: hidden; BORDER-TOP: lightgray 1px solid; RIGHT: 0px; BORDER-RIGHT: lightgray 1px solid; PADDING-TOP: 5px; LEFT: 0px; opacity: .70 }
    .statusbarhighlight { BORDER-BOTTOM: silver 1px solid; BORDER-LEFT: silver 1px solid; BACKGROUND-COLOR: khaki; COLOR: maroon; BORDER-TOP: silver 1px solid; FONT-WEIGHT: bold; BORDER-RIGHT: silver 1px solid }
		</style>
		<script>
 
 $(document).ready(function(){
 
	 
 });
 
function InfoByDate()
{     	
	$.ajax({
      
      type: "POST",
      
      url: "http://localhost/wsEjecutivoComercial.asmx/obtieneEjecutivosComerciales",
      
      data: "{'codigoSociedad':'GMSC'}",
      
      contentType: "application/json; charset=utf-8",
      
      dataType: "json",
      
      beforeSend: function(){
		alert('alla vamos');
	  },
	  
      success: function(response) {
		alert('llegué');
        var cars = response.d;
        alert(cars);        
      },

      failure: function(msg) {
        alert(msg);
      }

    });

} 	
    
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<input type="button" onclick="InfoByDate( );">
			<asp:DropDownList style="Z-INDEX: 101; POSITION: absolute; TOP: 128px; LEFT: 64px" id="DropDownList1"
				runat="server"></asp:DropDownList>
		</form>
	</body>
</HTML>
