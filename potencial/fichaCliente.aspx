<%@ Page Language="vb" AutoEventWireup="false" Codebehind="fichaCliente.aspx.vb" Inherits="app.potencial_fichacliente" %>
<%@ Register TagPrefix="Wilson" Namespace="Wilson.MasterPages" Assembly="WilsonMasterPages" %>
<wilson:masterpage style="Z-INDEX: 0" id="MPContainer" runat="server">
	<WILSON:CONTENTREGION id="MPTitle" runat="server">Ficha 
cliente</WILSON:CONTENTREGION>
	<WILSON:CONTENTREGION id="MPCaption" runat="server">Ficha cliente</WILSON:CONTENTREGION>
	<SCRIPT language="javascript">
	$(document).ready(function(){

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
				muestraMensaje('Cambios aplicados correctamente.', 2000);
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

function actualizaIndicadoresMiscCliente(obj)
{
	//$('#img_' + seccion + '_' + codCliente).html('<img src="/images/indicator.gif">');
	$('#' + obj.id).attr("disabled", "disabled");
	codSociedad = 'GMSC';
	codCliente = $("#hfCodCliente").attr("value");

	dotacioPersonal = $("#tbDotacionPersonal").attr("value");
	if (!((!isNaN(parseFloat(dotacioPersonal))) && (isFinite(dotacioPersonal))))
		dotacioPersonal = -1;

	codClaAntalis = $("#ddlClasificacionAntalis option:selected").attr("value");
	
	$.post('/potencial/actIndicadoresMiscCliente.aspx', {codSociedad:codSociedad, codCliente:codCliente, dotacioPersonal:dotacioPersonal, codClaAntalis:codClaAntalis}, 
		function (data)
		{
			//$('#img_' + seccion + '_' + codCliente).html('');
			$('#' + obj.id).attr("disabled", "");
			$('#' + obj.id).css('border', 'solid 1px #0000FF');
			$('#' + obj.id).css('background-color', '#FFF666');
			muestraMensaje('Cambios aplicados correctamente.', 2000);
		}
	);
}

function muestraMensaje(message,timeout,add)
{            
	if (typeof _statusbar == "undefined")    
	{       
		// ** Create a new statusbar instance as a global object        
		_statusbar = $("<div id='_statusbar' class='statusbar'></div>")                  
		.appendTo(document.body)                                       
		.show();
	}     
	if (add)                    
		// *** add before the first item            
		_statusbar.prepend( "<div style='margin-bottom: 2px;' >" + message + "</div>")[0].focus();    
	 else            
		_statusbar.text(message)     
	 _statusbar.show();             
	 
	 if (timeout)    
	 {        
		_statusbar.addClass("statusbarhighlight");        
		setTimeout( function() { 
			_statusbar.removeClass("statusbarhighlight"); 
			_statusbar.fadeOut(2000);
		},timeout);    }                
	}	
	
	</SCRIPT>
	<asp:Panel style="Z-INDEX: 0" id="pResultadoCliente" runat="server" CssClass="pot-resultado-cliente">
		<FIELDSET style="Z-INDEX: 0">
			<LEGEND class="pot-groupbox-potencial">Cliente</LEGEND>
			<DIV class="pot-contenedor-controles">
				<TABLE id="Table2" border="0" cellSpacing="0" cellPadding="0" align="left">
					<TR class="pot-ft1">
						<TD class="pot-celda-titulo" align="left">Cliente</TD>
						<TD class="pot-celda-valor" align="left">
							<asp:Label id="lCodigoCliente" runat="server"></asp:Label>&nbsp;::
							<asp:Label style="Z-INDEX: 0" id="lRazonSocialCliente" runat="server"></asp:Label>&nbsp;
						</TD>
						<TD class="pot-celda-valor">&nbsp;&nbsp;&nbsp;&nbsp;
							<asp:HyperLink style="Z-INDEX: 0" id="hlBuscarotroCliente" runat="server" NavigateUrl="default.aspx"
								cssClass="pot-vinculo">buscar otro cliente</asp:HyperLink></TD>
					</TR>
					<TR class="pot-ft2">
						<TD class="pot-celda-titulo" align="left"></TD>
						<TD class="pot-celda-valor" align="left">
							<asp:Label id="lRutCliente" runat="server"></asp:Label><INPUT id="hfCodCliente" type="hidden" name="hfCodCliente" runat="server"></TD>
						<TD class="pot-celda-valor"></TD>
					</TR>
					<TR class="pot-ft1">
						<TD class="pot-celda-titulo" align="left">Dirección:</TD>
						<TD class="pot-celda-valor" align="left">
							<asp:Label id="lDireccionComercial" runat="server"></asp:Label></TD>
						<TD class="pot-celda-valor"></TD>
					</TR>
					<TR class="pot-ft2">
						<TD class="pot-celda-titulo" align="left">Célula:</TD>
						<TD class="pot-celda-valor" align="left">
							<asp:Label id="lCelula" runat="server"></asp:Label></TD>
						<TD class="pot-celda-valor"></TD>
					</TR>
					<TR class="pot-ft1">
						<TD class="pot-celda-titulo" align="left">Ejec. Comercial:</TD>
						<TD class="pot-celda-valor" align="left">
							<asp:Label id="lEjecutivaComercial" runat="server"></asp:Label></TD>
						<TD class="pot-celda-valor"></TD>
					</TR>
					<TR class="pot-ft2">
						<TD class="pot-celda-titulo" align="left">Vend. Virtual:</TD>
						<TD class="pot-celda-valor" align="left">
							<asp:Label id="lVendedoraVirtual" runat="server"></asp:Label></TD>
						<TD class="pot-celda-valor"></TD>
					</TR>
					<TR class="pot-ft1">
						<TD class="pot-celda-titulo" align="left">Cobrador:</TD>
						<TD class="pot-celda-valor" align="left">
							<asp:Label id="lCobrador" runat="server"></asp:Label></TD>
						<TD class="pot-celda-valor"></TD>
					</TR>
				</TABLE>
			</DIV>
		</FIELDSET>
		<BR>
		<FIELDSET>
			<LEGEND class="pot-groupbox-potencial">Estadísticas de ventas</LEGEND>
			<DIV class="pot-contenedor-controles">
				<TABLE border="0" cellSpacing="0" cellPadding="0" align="left">
					<TR>
						<TD>
							<asp:DataGrid id="dgSerie" runat="server" CellPadding="3" BackColor="White" BorderWidth="1px"
								BorderStyle="Solid" BorderColor="#336666" AutoGenerateColumns="False" Font-Size="XX-Small"
								Font-Names="Verdana" GridLines="None" CellSpacing="1">
								<FooterStyle ForeColor="#333333" BackColor="White"></FooterStyle>
								<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#339966"></SelectedItemStyle>
								<ItemStyle ForeColor="#333333" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#336666"></HeaderStyle>
								<Columns>
									<asp:BoundColumn DataField="val_tipo" HeaderText="Concepto"></asp:BoundColumn>
									<asp:BoundColumn DataField="mes_doce" DataFormatString="{0:#,##0}">
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="mes_once" DataFormatString="{0:#,##0}">
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="mes_diez" DataFormatString="{0:#,##0}">
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="mes_nueve" DataFormatString="{0:#,##0}">
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="mes_ocho" DataFormatString="{0:#,##0}">
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="mes_siete" DataFormatString="{0:#,##0}">
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="mes_seis" DataFormatString="{0:#,##0}">
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="mes_cinco" DataFormatString="{0:#,##0}">
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="mes_cuatro" DataFormatString="{0:#,##0}">
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="mes_tres" DataFormatString="{0:#,##0}">
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="mes_dos" DataFormatString="{0:#,##0}">
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="mes_uno" DataFormatString="{0:#,##0}">
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="mes_actual" HeaderText="Mes Actual" DataFormatString="{0:#,##0}">
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Center" ForeColor="White" BackColor="#336666" Mode="NumericPages"></PagerStyle>
							</asp:DataGrid></TD>
					</TR>
					<TR>
						<TD height="20">&nbsp;</TD>
					</TR>
					<TR>
						<TD>
							<TABLE id="Table4" border="0" cellSpacing="1" cellPadding="0" align="left">
								<TR>
									<TD></TD>
									<TD class="pot-promedio-ventas" colSpan="3" align="center">PROMEDIOS MENSUALES</TD>
								</TR>
								<TR>
									<TD></TD>
									<TD class="pot-promedio-ventas-n2">Últimos&nbsp;12 meses</TD>
									<TD class="pot-promedio-ventas-n2">Últimos 06 meses</TD>
									<TD class="pot-promedio-ventas-n2">Últimos&nbsp;03 meses</TD>
								</TR>
								<TR>
									<TD class="pot-promedio-ventas-n2">Venta CLP:</TD>
									<TD class="pot-celda-dato-t1" align="right">
										<asp:Label style="Z-INDEX: 0" id="lVentaClp12meses" runat="server"></asp:Label></TD>
									<TD class="pot-celda-dato-t1" align="right">
										<asp:Label id="lVentaClp6meses" runat="server"></asp:Label></TD>
									<TD class="pot-celda-dato-t1" align="right">
										<asp:Label style="Z-INDEX: 0" id="lVentaClp3meses" runat="server"></asp:Label></TD>
								</TR>
								<TR>
									<TD class="pot-promedio-ventas-n2">Venta GUSD:</TD>
									<TD class="pot-celda-dato-t2" align="right">
										<asp:Label style="Z-INDEX: 0" id="lVentaGusd12meses" runat="server"></asp:Label></TD>
									<TD class="pot-celda-dato-t2" align="right">
										<asp:Label id="lVentaGusd6meses" runat="server"></asp:Label></TD>
									<TD class="pot-celda-dato-t2" align="right">
										<asp:Label style="Z-INDEX: 0" id="lVentaGusd3meses" runat="server"></asp:Label></TD>
								</TR>
								<TR>
									<TD class="pot-promedio-ventas-n2">Mg GUSD:</TD>
									<TD class="pot-celda-dato-t1" align="right">
										<asp:Label style="Z-INDEX: 0" id="lVentaMgd12meses" runat="server"></asp:Label></TD>
									<TD class="pot-celda-dato-t1" align="right">
										<asp:Label id="lVentaMgd6meses" runat="server"></asp:Label></TD>
									<TD class="pot-celda-dato-t1" align="right">
										<asp:Label style="Z-INDEX: 0" id="lVentaMgd3meses" runat="server"></asp:Label></TD>
								</TR>
								<TR>
									<TD class="pot-promedio-ventas-n2">Pedidos VTA:</TD>
									<TD class="pot-celda-dato-t2" align="right">
										<asp:Label style="Z-INDEX: 0" id="lPedidoVta12meses" runat="server"></asp:Label></TD>
									<TD class="pot-celda-dato-t2" align="right">
										<asp:Label id="lPedidoVta6meses" runat="server"></asp:Label></TD>
									<TD class="pot-celda-dato-t2" align="right">
										<asp:Label style="Z-INDEX: 0" id="lPedidoVta3meses" runat="server"></asp:Label></TD>
								</TR>
								<TR>
									<TD class="pot-promedio-ventas-n2">Materiales distintos:</TD>
									<TD class="pot-celda-dato-t1" align="right">
										<asp:Label style="Z-INDEX: 0" id="lMaterialesDistintos12meses" runat="server"></asp:Label></TD>
									<TD class="pot-celda-dato-t1" align="right">
										<asp:Label id="lMaterialesDistintos6meses" runat="server"></asp:Label></TD>
									<TD class="pot-celda-dato-t1" align="right">
										<asp:Label style="Z-INDEX: 0" id="lMaterialesDistintos3meses" runat="server"></asp:Label></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
				</TABLE>
			</DIV>
		</FIELDSET>
		<BR>
		<FIELDSET DESIGNTIMEDRAGDROP="199">
			<LEGEND class="pot-groupbox-potencial">Potencial</LEGEND>
			<DIV class="pot-contenedor-controles">
				<TABLE id="Table3" border="0" cellSpacing="0" cellPadding="0" align="left">
					<TR>
						<TD class="pot-celda-titulo" align="left">Dotación personal:</TD>
						<TD class="pot-celda-valor" colSpan="7" align="left">
							<asp:TextBox style="Z-INDEX: 0" id="tbDotacionPersonal" runat="server" CssClass="pot-textbox-cifra"></asp:TextBox></TD>
					</TR>
					<TR class="pot-ft1">
						<TD class="pot-celda-titulo" align="left">Clasificación Antalis:</TD>
						<TD class="pot-celda-valor" colSpan="7" align="left">
							<asp:DropDownList id="ddlClasificacionAntalis" runat="server"></asp:DropDownList></TD>
					</TR>
					<TR>
						<TD class="pot-celda-titulo" align="left"></TD>
						<TD class="pot-celda-valor" align="right">General</TD>
						<TD class="pot-celda-valor" align="right">PAP</TD>
						<TD class="pot-celda-valor" align="right">CVI</TD>
						<TD class="pot-celda-valor" align="right">PAK</TD>
						<TD class="pot-celda-valor" align="right">EII</TD>
						<TD class="pot-celda-valor" align="right">ST</TD>
						<TD width="30" align="right"></TD>
					</TR>
					<TR class="pot-ft2">
						<TD class="pot-celda-titulo" align="left">Potencial:</TD>
						<TD class="pot-celda-valor" align="right">
							<asp:TextBox id="tbPotCliGNR" runat="server" CssClass="pot-cifra"></asp:TextBox></TD>
						<TD class="pot-celda-valor" align="right">
							<asp:TextBox style="Z-INDEX: 0" id="tbPotCliPAP" runat="server" CssClass="pot-cifra"></asp:TextBox></TD>
						<TD class="pot-celda-valor" align="right">
							<asp:TextBox style="Z-INDEX: 0" id="tbPotCliCVI" runat="server" CssClass="pot-cifra"></asp:TextBox></TD>
						<TD class="pot-celda-valor" align="right">
							<asp:TextBox style="Z-INDEX: 0" id="tbPotCliPAK" runat="server" CssClass="pot-cifra"></asp:TextBox></TD>
						<TD class="pot-celda-valor" align="right">
							<asp:TextBox style="Z-INDEX: 0" id="tbPotCliEII" runat="server" CssClass="pot-cifra"></asp:TextBox></TD>
						<TD class="pot-celda-valor" align="right">
							<asp:TextBox style="Z-INDEX: 0" id="tbPotCliST" runat="server" CssClass="pot-cifra"></asp:TextBox></TD>
						<TD width="30" align="right">
							<asp:Panel id="pImgPotCli" runat="server"></asp:Panel></TD>
					</TR>
					<TR class="pot-ft1">
						<TD class="pot-celda-titulo" align="left">Capacidad instalada:</TD>
						<TD class="pot-celda-valor" align="right">
							<asp:TextBox style="Z-INDEX: 0" id="tbCapInstGNR" runat="server" CssClass="pot-cifra"></asp:TextBox></TD>
						<TD class="pot-celda-valor" align="right">
							<asp:TextBox style="Z-INDEX: 0" id="tbCapInstPAP" runat="server" CssClass="pot-cifra"></asp:TextBox></TD>
						<TD class="pot-celda-valor" align="right">
							<asp:TextBox style="Z-INDEX: 0" id="tbCapInstCVI" runat="server" CssClass="pot-cifra"></asp:TextBox></TD>
						<TD class="pot-celda-valor" align="right">
							<asp:TextBox style="Z-INDEX: 0" id="tbCapInstPAK" runat="server" CssClass="pot-cifra"></asp:TextBox></TD>
						<TD class="pot-celda-valor" align="right">
							<asp:TextBox style="Z-INDEX: 0" id="tbCapInstEII" runat="server" CssClass="pot-cifra"></asp:TextBox></TD>
						<TD class="pot-celda-valor" align="right">
							<asp:TextBox style="Z-INDEX: 0" id="tbCapInstST" runat="server" CssClass="pot-cifra"></asp:TextBox></TD>
						<TD width="30" align="right">
							<asp:Panel id="pImgCapInst" runat="server"></asp:Panel></TD>
					</TR>
					<TR>
						<TD class="pot-celda-titulo" colSpan="8" align="left"></TD>
					</TR>
					<TR>
						<TD class="pot-celda-titulo" colSpan="8" align="left"></TD>
					</TR>
					<TR>
						<TD colSpan="8" align="left">
							<asp:Panel style="Z-INDEX: 0" id="pMensajeAccion2" runat="server">
								<asp:Label id="lMensajeAccion2" runat="server"></asp:Label>
							</asp:Panel></TD>
					</TR>
				</TABLE>
				<P>&nbsp;</P>
			</DIV>
		</FIELDSET>
	</asp:Panel>
</wilson:masterpage>
