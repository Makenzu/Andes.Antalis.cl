<%@ Page Language="vb" AutoEventWireup="false" Codebehind="grilla.aspx.vb" Inherits="app.potencial_potencial" %>
<%@ Register TagPrefix="Wilson" Namespace="Wilson.MasterPages" Assembly="WilsonMasterPages" %>
<wilson:masterpage style="Z-INDEX: 0" id="MPContainer" runat="server">
	<WILSON:CONTENTREGION id="MPTitle" runat="server">Grilla potencial 
- capacidad</WILSON:CONTENTREGION>
	<WILSON:CONTENTREGION id="MPCaption" runat="server">Grilla potencial - capacidad</WILSON:CONTENTREGION>
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

function actualizaIndicadoresVentaCliente(codSociedad, seccion, codCliente, area, obj)
{
	$('#img_' + seccion + '_' + codCliente).html('<img src="/images/indicator.gif">');
	$('#' + obj.id).attr("disabled", "disabled");

	if ((!isNaN(parseFloat(obj.value))) && (isFinite(obj.value)))
	{
		valorIndicador = obj.value;
		$.post('/potencial/actIndicadoresVtaCliente.aspx', {codSociedad:codSociedad, codCliente:codCliente, area:area, valor:valorIndicador, seccion:seccion}, 
			function (data)
			{
				$('#img_' + seccion + '_' + codCliente).html('');
				$('#' + obj.id).attr("disabled", "");
				$('#' + obj.id).css('border', 'solid 1px #0000FF');
				$('#' + obj.id).css('background-color', '#FFF666');
				$('#' + obj.id).attr('title', 'Datos de la celda fueron grabados.');
				muestraMensaje('Cambios guardados correctamente.', 500);
			}
		);
	}
	else
	{
		$('#' + obj.id).css("border", "solid 1px #FF0000");
		$('#img_' + seccion + '_' + codCliente).html('<img src="/images/error-icon.jpg">');
		$('#' + obj.id).attr("disabled", "");
	}

}




	</SCRIPT>
	<DIV class="pot-contenedor-controles">
		<FIELDSET>
			<LEGEND class="pot-groupbox-potencial">Busqueda de clientes</LEGEND>
			<DIV class="pot-contenedor-controles">
				<UL class="tabs">
					<LI id="op1">
						<A href="#tab1">Ejecutiva comercial</A>
					<LI id="op2">
						<A href="#tab2">Vendedora virtual</A>
					<LI id="op3">
						<A href="#tab3">Cobrador</A>
					<LI id="op4">
						<A href="#tab4">Célula</A>
					<LI id="op5">
						<A href="#tab5">Clasificación Antalis</A>
					<LI id="op6">
						<A href="#tab6">Cliente</A>
					</LI>
				</UL>
				<DIV class="tab_container">
					<DIV id="tab1" class="tab_content">
						<TABLE border="0" cellSpacing="0" cellPadding="0">
							<TR>
								<TD class="nombre-campo">Ejec. comercial:&nbsp;</TD>
								<TD>
									<asp:DropDownList style="Z-INDEX: 0" id="ddlEjecutivaComercial" runat="server"></asp:DropDownList></TD>
								<TD>&nbsp;
									<asp:Button id="bBuscarPorEjecComercial" runat="server" Text="buscar"></asp:Button></TD>
							</TR>
							<TR>
								<TD class="nombre-campo" height="15" colSpan="3"></TD>
							</TR>
						</TABLE>
						<asp:Panel id="pResultado1" runat="server"></asp:Panel></DIV>
					<DIV id="tab2" class="tab_content">
						<TABLE border="0" cellSpacing="0" cellPadding="0">
							<TR>
								<TD class="nombre-campo">Vend. virtual:&nbsp;</TD>
								<TD>
									<asp:DropDownList id="ddlVendedoraVirtual" runat="server"></asp:DropDownList></TD>
								<TD>&nbsp;
									<asp:Button id="bBuscarPorVendVirtual" runat="server" Text="buscar"></asp:Button></TD>
							</TR>
							<TR>
								<TD class="nombre-campo" height="15" colSpan="3"></TD>
							</TR>
						</TABLE>
						<asp:Panel id="pResultado2" runat="server"></asp:Panel></DIV>
					<DIV id="tab3" class="tab_content">
						<TABLE border="0" cellSpacing="0" cellPadding="0">
							<TR>
								<TD class="nombre-campo">Cobrador:&nbsp;</TD>
								<TD>
									<asp:DropDownList id="ddlCobrador" runat="server"></asp:DropDownList></TD>
								<TD>&nbsp;
									<asp:Button id="bBuscarPorCobrador" runat="server" Text="buscar"></asp:Button></TD>
							</TR>
							<TR>
								<TD class="nombre-campo" height="15" colSpan="3"></TD>
							</TR>
						</TABLE>
						<asp:Panel id="pResultado3" runat="server"></asp:Panel></DIV>
					<DIV id="tab4" class="tab_content">
						<TABLE border="0" cellSpacing="0" cellPadding="0">
							<TR>
								<TD class="nombre-campo">Célula:&nbsp;</TD>
								<TD>
									<asp:DropDownList id="ddlCelula" runat="server"></asp:DropDownList>&nbsp;</TD>
								<TD>
									<asp:Button id="bBuscarPorCelula" runat="server" Text="buscar"></asp:Button></TD>
							</TR>
							<TR>
								<TD class="nombre-campo" height="15" colSpan="3"></TD>
							</TR>
						</TABLE>
						<asp:Panel id="pResultado4" runat="server"></asp:Panel></DIV>
					<DIV id="tab5" class="tab_content">
						<TABLE border="0" cellSpacing="0" cellPadding="0">
							<TR>
								<TD class="nombre-campo">Clasific. Antalis:&nbsp;</TD>
								<TD>
									<asp:DropDownList id="ddlClasificacionAntalis" runat="server"></asp:DropDownList></TD>
								<TD>&nbsp;
									<asp:Button id="bBuscarPorClasificacionAntalis" runat="server" Text="buscar"></asp:Button></TD>
							</TR>
							<TR>
								<TD class="nombre-campo" height="15" colSpan="3"></TD>
							</TR>
						</TABLE>
						<asp:Panel id="pResultado5" runat="server"></asp:Panel></DIV>
					<DIV id="tab6" class="tab_content">
						<TABLE id="Table1" border="0" cellSpacing="0" cellPadding="0">
							<TR>
								<TD class="pot-celda-titulo">Código / Razón social:</TD>
								<TD>
									<asp:TextBox id="tbPatronCliente" runat="server" MaxLength="80" Width="400px"></asp:TextBox></TD>
								<TD>&nbsp;
									<asp:Button id="bBuscarPorPatron" runat="server" Text="buscar"></asp:Button></TD>
							</TR>
							<TR>
								<TD class="pot-celda-titulo" height="15" colSpan="3"></TD>
							</TR>
							<TR>
								<TD colSpan="3">
									<asp:Label id="lMensajeAccionBusqueda" runat="server"></asp:Label></TD>
							</TR>
						</TABLE>
						<asp:Panel id="pResultado6" runat="server"></asp:Panel></DIV>
				</DIV>
			</DIV>
		</FIELDSET>
	</DIV>
</wilson:masterpage>
