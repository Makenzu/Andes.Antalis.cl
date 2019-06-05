<%@ Register TagPrefix="Wilson" Namespace="Wilson.MasterPages" Assembly="WilsonMasterPages" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="default.aspx.vb" Inherits="app.catastromaquinas_default" %>
<wilson:masterpage style="Z-INDEX: 0" id="MPContainer" runat="server">
	<WILSON:CONTENTREGION id="MPTitle" runat="server">Catastro 
Clientes</WILSON:CONTENTREGION>
	<WILSON:CONTENTREGION id="MPCaption" runat="server">Equipos</WILSON:CONTENTREGION> <!-- #dialog is the id of a DIV defined in the code below -->
	<DIV id="modalPopup">
		<DIV class="modalContainer">
			<DIV class="modal">
				<DIV class="modalTop"><SPAN id="tituloVentanaDialogo"></SPAN>&nbsp;&nbsp;&nbsp;<A id="vinculo_salida" href="Javascript:Popup.hide('modalPopup');">[X]</A></DIV>
				<DIV class="modalBody"><IFRAME id="framePopup" height="400" src="artefacto.aspx" width="780"></IFRAME></DIV>
			</DIV>
		</DIV>
	</DIV>
	<SCRIPT language="javascript">
$(document).ready(function()   
{   
    $(".tab_content").hide(); 
    $('#panelOpcionesDespliegue').hide();
    $('#chkbMostrarDetalles').click(function(){
 
		if ($('#chkbMostrarDetalles').is(':checked'))
			$('[id^=DETALLE_]').fadeIn();
		else
			$('[id^=DETALLE_]').fadeOut();
    });
      
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
      
    $('#opcionesDespliegue').click(function(){
		$('#panelOpcionesDespliegue').slideToggle();    
    });
    
    $('#chkOpcionesMuestraDetalleEquipos').click(function(){
		if ($('#chkOpcionesMuestraDetalleEquipos').is(':checked'))
		{
			$('[id^=DETALLE_]').fadeIn();	
			$('[id^=cma-artefacto-]').removeClass('cma-nombre-artefacto');
			$('[id^=cma-artefacto-]').addClass('cma-nombre-artefacto-sel');
		}
		else
		{		
			$('[id^=DETALLE_]').fadeOut();
			$('[id^=cma-artefacto-]').removeClass('cma-nombre-artefacto-sel');
			$('[id^=cma-artefacto-]').addClass('cma-nombre-artefacto');
		}			
	});
	    
    $("#tbPatronCliente").autocomplete("/acClientes.aspx");   
});

function despliegaDialogo(codigoSociedad, codigoCliente, razonSocial, accion, idRegistroCliente)
{
	$('#tituloVentanaDialogo').text(razonSocial);
	document.getElementById('framePopup').src= 'artefacto.aspx?accion=' + accion + '&cs=' + codigoSociedad + '&cc=' + codigoCliente + '&id=' + idRegistroCliente;
	Popup.showModal('modalPopup');	
	return false;
} 

function cerrarDialogo()
{
	Popup.hide('modalPopup');
}

function eliminaRegistroCliente(codSociedad, idRegistroCliente, usrResponsable)
{
	if (confirm('¿Está seguro que desea eliminar este elemento?'))
	{
		$.post('eliminaRegistroCliente.aspx', {codSociedad:codSociedad, idRegistroCliente:idRegistroCliente, usrResponsable:usrResponsable}, 
					function (data)
					{
						$('#equipo_' + idRegistroCliente).hide();
					}
				);	
		muestraMensaje('exito', 'Registro eliminado correctamente.', 2000);
	}
}

function agregaFilaDatos(codSociedad, codCliente, razonSocial, dmcArtefacto, tipoArtefacto, idRegistroCliente)
{
	if (!$('#equipos_' + codCliente).length)
	{
		//No existe la tabla, la creamos
		var str = '<table id="equipos_' + codCliente + '" cellspacing="1" cellpadding="0" border="0" style="border-width:0px;">\n';
		str += '<tr class="cma-cabecera-equipos">\n';
		str += '<td class="cma-cabecera-equipos-c0">Nombre</td>\n';
		str += '<td class="cma-cabecera-equipos-c1">Tipo</td>\n';
		str += '<td class="cma-cabecera-equipos-c2"></td><td></td>\n';				
		str += '</tr>\n';
		str += '</table>\n';
		
		$('#cont_equipos_' + codCliente).prepend(str);
	}

	str = '<tr id="equipo_' + idRegistroCliente + '" class="cma-dato-equipos">';
	str += '<td class="cma-dato-equipos-c0">' + dmcArtefacto + '</td>';
	str += '<td class="cma-dato-equipos-c1">' + tipoArtefacto + '</td>';
	str += '<td class="cma-dato-equipos-c2"><a onclick="despliegaDialogo(\'' + codSociedad + '\', \'' + codCliente + '\', \'' + razonSocial + '\', \'editar\', ' + idRegistroCliente + '); return false;" href="#">editar</a></td>';
	str += '<td class="cma-dato-equipos-c3"><a onclick="eliminaRegistroCliente(\'' + codSociedad + '\', ' + idRegistroCliente + ', \'\'); return false;" href="#">eliminar</a></td>';
	str += '</tr>';
	
	$('#equipos_' + codCliente).append(str);
}

	</SCRIPT>
	<DIV class="pot-contenedor-controles">
		<FIELDSET>
			<LEGEND class="pot-groupbox-potencial">Consulta 
de&nbsp;clientes</LEGEND>
			<DIV class="pot-contenedor-controles">
				<UL class="tabs">
					<LI id="op1">
						<A href="#tab1">Ejec. comercial</A>
					<LI id="op2">
						<A href="#tab2">Vend. virtual</A>
					<LI id="op3">
						<A href="#tab3">Cobrador</A>
					<LI id="op4">
						<A href="#tab4">Célula</A>
					<LI id="op5">
						<A href="#tab5">Clasif. Antalis</A>
					<LI id="op6">
						<A href="#tab6">Cliente</A>
					<LI id="op7">
						<A href="#tab7">Equipo / Tipo</A>
					</LI>
				</UL>
				<DIV class="tab_container">
					<DIV id="tab1" class="tab_content">
						<TABLE border="0" cellSpacing="0" cellPadding="0">
							<TR>
								<TD class="nombre-campo">Ejec. comercial:&nbsp;</TD>
								<TD>
									<asp:DropDownList style="Z-INDEX: 0" id="ddlEjecutivaComercial" runat="server"></asp:DropDownList></TD>
								<TD>
									<asp:Button id="bBuscarPorEjecComercial" runat="server" Text="buscar"></asp:Button></TD>
							</TR>
							<TR>
								<TD class="nombre-campo" height="15" colSpan="3"></TD>
							</TR>
						</TABLE>
						<asp:Panel style="Z-INDEX: 0" id="Panel1" runat="server"></asp:Panel></DIV>
					<DIV style="DISPLAY: none" id="tab2" class="tab_content">
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
						<asp:Panel style="Z-INDEX: 0" id="Panel2" runat="server"></asp:Panel></DIV>
					<DIV style="DISPLAY: none" id="tab3" class="tab_content">
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
						<asp:Panel style="Z-INDEX: 0" id="Panel3" runat="server"></asp:Panel></DIV>
					<DIV style="DISPLAY: none" id="tab4" class="tab_content">
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
						<asp:Panel style="Z-INDEX: 0" id="Panel4" runat="server"></asp:Panel></DIV>
					<DIV style="DISPLAY: none" id="tab5" class="tab_content">
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
						<asp:Panel style="Z-INDEX: 0" id="Panel5" runat="server"></asp:Panel></DIV>
					<DIV style="DISPLAY: none" id="tab6" class="tab_content">
						<TABLE id="Table1" border="0" cellSpacing="0" cellPadding="0">
							<TR>
								<TD class="pot-celda-titulo">Código / Razón social:</TD>
								<TD>
									<asp:TextBox id="tbPatronCliente" runat="server" Width="400px" MaxLength="80"></asp:TextBox></TD>
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
						<asp:Panel style="Z-INDEX: 0" id="Panel6" runat="server"></asp:Panel></DIV>
					<DIV style="DISPLAY: none" id="tab7" class="tab_content">
						<TABLE id="Table7" border="0" cellSpacing="0" cellPadding="0">
							<TR>
								<TD class="pot-celda-titulo">Tipo:</TD>
								<TD align="left">
									<asp:DropDownList id="ddlTipo" runat="server" CssClass="cma-ddl-t1" AutoPostBack="True"></asp:DropDownList></TD>
								<TD>&nbsp;
									<asp:Button id="bBuscarPorTipoEquipo" runat="server" Text="buscar"></asp:Button></TD>
							</TR>
							<TR>
								<TD class="pot-celda-titulo">Equipo:</TD>
								<TD align="left">
									<asp:DropDownList style="Z-INDEX: 0" id="ddlEquipo" runat="server" CssClass="cma-ddl-t1"></asp:DropDownList></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD class="pot-celda-titulo" height="15" colSpan="3"></TD>
							</TR>
							<TR>
								<TD colSpan="3">
									<asp:Label id="Label1" runat="server"></asp:Label></TD>
							</TR>
						</TABLE>
						<asp:Panel style="Z-INDEX: 0" id="Panel7" runat="server"></asp:Panel></DIV>
				</DIV>
			</DIV>
		</FIELDSET>
	</DIV>
</wilson:masterpage>
