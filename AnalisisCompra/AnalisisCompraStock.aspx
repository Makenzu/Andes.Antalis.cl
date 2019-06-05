<%@ Register TagPrefix="Wilson" Namespace="Wilson.MasterPages" Assembly="WilsonMasterPages" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="AnalisisCompraStock.aspx.vb" Inherits="app.AnalisisCompraStock"%>
<wilson:masterpage id="MPContainer" DESIGNTIMEDRAGDROP="176" runat="server">
	<WILSON:CONTENTREGION id="MPTitle" runat="server">Bienvenido a&nbsp;ANDES</WILSON:CONTENTREGION>
	<WILSON:CONTENTREGION id="MPCaption" runat="server" aling="left">Reporte analisis 
de stock y compras</WILSON:CONTENTREGION>
	<SCRIPT type="text/javascript">
$(document).ready(function()   
{   
	$('#pDetalleOC').jqm();
	$('#pDetallePROF').jqm();
	$('#pDetalleFACT').jqm();
	
	$('.acs-popup1').click(function(event){
		var arg = $(this).attr('href');
		var arrStr = arg.split('|');

		if (arrStr[0] == '#OC')
		{
			$.get("DetalleOC.aspx", { codigoMaterial: arrStr[1]}, function(data){
				$('#contenidoDetalleOC').html(data);
				$('#pDetalleOC').jqmShow();
			}, 'html');
		}
		else if(arrStr[0] == '#PROF')
		{
			$.get("DetalleProf.aspx", { codigoMaterial: arrStr[1]}, function(data){
					$('#contenidoDetallePROF').html(data);
					$('#pDetallePROF').jqmShow();
			}, 'html');
		}
		else if(arrStr[0] == '#FACT') 
		{
			$.get("DetalleFACT.aspx", { codigoMaterial: arrStr[1]}, function(data){
				$('#contenidoDetalleFACT').html(data);
				$('#pDetalleFACT').jqmShow();
			}, 'html');
		}  
	});
   
		$("#tbMaterial").autocomplete("/acMateriales.aspx", {   
			extraParams: {
					cs: function() { 
						var sucursal = $('#ddlSucursal').val();
						if  (sucursal == '001')
							return 'GMSC';
						if  ((sucursal == '002') || (sucursal == '022'))
							return 'APER';
						if  ((sucursal == '003') || (sucursal == '004'))
							return 'ABOL';
						if  (sucursal == '005')
							return 'DGS0';												
						if  (sucursal == '007')
							return 'DGS0';	
							
						if (sucursal == '-100')
						{
							if ($('#ddlFilial').val() == 'CHI')
								return 'GMSC';
							if ($('#ddlFilial').val() == 'BOL')
								return 'ABOL';
							if ($('#ddlFilial').val() == 'PER')
								return 'APER';
								
								
						}
					}	  
				} 
		});		
	});	
	</SCRIPT>
	<DIV>
		<TABLE border="0" cellSpacing="0" cellPadding="3" align="left">
			<TR>
				<TD height="15" colSpan="5"></TD>
			</TR>
			<TR>
				<TD align="left">Product Manager:</TD>
				<TD align="left">
					<asp:DropDownList style="Z-INDEX: 0" id="ddlEncargadoSubfamilia" runat="server" AutoPostBack="True"
						CssClass="listBox"></asp:DropDownList></TD>
				<TD></TD>
				<TD width="150"></TD>
				<TD></TD>
			</TR>
			<TR>
				<TD align="left">Jerarquía:</TD>
				<TD align="left">
					<asp:DropDownList style="Z-INDEX: 0" id="ddlArea" runat="server" AutoPostBack="True" CssClass="listBox"></asp:DropDownList>-
					<asp:DropDownList style="Z-INDEX: 0" id="ddlFamilia" runat="server" AutoPostBack="True" CssClass="listBox"></asp:DropDownList>-
					<asp:DropDownList style="Z-INDEX: 0" id="ddlSubfamilia" runat="server" AutoPostBack="True" CssClass="listBox"></asp:DropDownList></TD>
				<TD></TD>
				<TD width="150"></TD>
				<TD></TD>
			</TR>
			<TR>
				<TD align="left">Material:</TD>
				<TD>
					<asp:TextBox id="tbMaterial" runat="server" Width="400"></asp:TextBox></TD>
				<TD>
					<asp:Button style="Z-INDEX: 0" id="bConsultar" runat="server" Text="Consultar"></asp:Button></TD>
				<TD width="150"></TD>
				<TD>
					<asp:ImageButton style="Z-INDEX: 0" id="Imagebutton1" title="Exportar a Excel" runat="server" Visible="False"
						ToolTip="Exportar a Excel" ImageUrl="/images/exportar.gif" CausesValidation="False" EnableViewState="False"></asp:ImageButton></TD>
			</TR>
			<TR>
				<TD align="left"></TD>
				<TD></TD>
				<TD></TD>
				<TD width="150"></TD>
				<TD></TD>
			</TR>
		</TABLE>
	</DIV>
	<asp:Panel id="Panel1" runat="server"></asp:Panel>
	<DIV style="DISPLAY: none" id="pDetalleOC" class="jqmWindowOC">
		<TABLE border="0" cellSpacing="0" cellPadding="0">
			<TR>
				<TD bgColor="#2e2e2e" width="600" align="right">
					<TABLE border="0" cellSpacing="0" cellPadding="0">
						<TR>
							<TD><A class="cerrar-popUp" onclick="$('#pDetalleOC').jqmHide(); $('#contenidoDetalleOC').html('');return false;"
									href="#">CERRAR&nbsp;</A></TD>
							<TD><A class="cerrar-popUp" onclick="$('#pDetalleOC').jqmHide(); $('#contenidoDetalleOC').html('');return false;"
									href="#"><IMG border="0" src="/images/cerrar.gif" valign="middle"></A>
							</TD>
						</TR>
					</TABLE>
				</TD>
			</TR>
			<TR>
				<TD id="contenidoDetalleOC" width="600">[AQUI VA UN TEXTO OC]</TD>
			</TR>
		</TABLE>
	</DIV>
	<DIV style="DISPLAY: none" id="pDetallePROF" class="jqmWindowPROF">
		<TABLE border="0" cellSpacing="0" cellPadding="0">
			<TR>
				<TD bgColor="#2e2e2e" width="auto" align="right">
					<TABLE border="0" cellSpacing="0" cellPadding="0">
						<TR>
							<TD><A class="cerrar-popUp" onclick="$('#pDetallePROF').jqmHide(); $('#contenidoDetallePROF').html(''); return false;"
									href="#">CERRAR&nbsp;</A></TD>
							<TD><A class="cerrar-popUp" onclick="$('#pDetallePROF').jqmHide(); $('#contenidoDetallePROF').html('');return false;"
									href="#"><IMG border="0" src="/images/cerrar.gif" valign="middle"></A>
							</TD>
						</TR>
					</TABLE>
				</TD>
			</TR>
			<TR>
				<TD id="contenidoDetallePROF" width="700">[AQUI VA UN TEXTO PROFORMA]</TD>
			</TR>
		</TABLE>
	</DIV>
	<DIV style="DISPLAY: none" id="pDetalleFACT" class="jqmWindowFACT">
		<TABLE border="0" cellSpacing="0" cellPadding="0">
			<TR>
				<TD bgColor="#2e2e2e" width="600" align="right">
					<TABLE border="0" cellSpacing="0" cellPadding="0">
						<TR>
							<TD><A class="cerrar-popUp" onclick="$('#pDetalleFACT').jqmHide(); $('#contenidoDetalleFACT').html(''); return false;"
									href="#">CERRAR&nbsp;</A></TD>
							<TD><A class="cerrar-popUp" onclick="$('#pDetalleFACT').jqmHide(); $('#contenidoDetalleFACT').html('');return false;"
									href="#"><IMG border="0" src="/images/cerrar.gif" valign="middle"></A>
							</TD>
						</TR>
					</TABLE>
				</TD>
			</TR>
			<TR>
				<TD id="contenidoDetalleFACT" width="600">[AQUI VA UN TEXTO FACTURA]</TD>
			</TR>
		</TABLE>
	</DIV>
</wilson:masterpage>
