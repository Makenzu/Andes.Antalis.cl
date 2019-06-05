<%@ Page Language="vb" AutoEventWireup="false" Codebehind="artefacto.aspx.vb" Inherits="app.artefacto"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>artefacto</title>
		<LINK rel="stylesheet" type="text/css" href="/css/andes.css">
			<script type="text/javascript" src="/js/jquery-1.5.1.js"></script>
			<script type="text/javascript" src="/js/jquery.autocomplete.js"></script>
			<LINK rel="stylesheet" type="text/css" href="/css/jquery.autocomplete.css">
				<script type="text/javascript">
		$(document).ready(function(){
			$("#tbMaquina").autocomplete("/acMaquinas.aspx", {   
				extraParams: {
					ta: function() { return $("#ddlTipoArtefacto").val(); }
				}
			});
			<asp:literal id="Literal1" runat="server"></asp:literal>
			
		});
		
		function agregaValorMultiple(codPropiedad)
		{
			//en codPropiedad tenemos el codigo de la propiedad
			var nuevaMedida = $.trim($('#tb_' + codPropiedad).val());

			if (nuevaMedida != '')
			{	
				var valor = '';
				//Si es mantilla, entonces obtengo datos de barra y tipo
				if (codPropiedad == 'MEDMANT')
				{
					tipoMantilla = $('#ddl_tm_' + codPropiedad).val();
					barra = '';
					if ($('#rb_cb_' + codPropiedad).attr('checked'))
						barra = 'CB';
					else if (($('#rb_sb_' + codPropiedad).attr('checked')))
						barra = 'SB';

					valor = nuevaMedida;
					
					if ((tipoMantilla != '') && (barra !=''))
						valor += ' (' + tipoMantilla + ', ' + barra + ')';
					else if (tipoMantilla != '')
						valor += ' (' + tipoMantilla + ')';
					else if (barra !='')
						valor += ' (' + barra + ')';

				}
				else
					valor = nuevaMedida;

				$('<option>').val(valor).text(valor).appendTo('select[name=lb_' + codPropiedad + ']');
				$('#tb_' + codPropiedad).val('');
				
				if (codPropiedad == 'MEDMANT')
				{
					$('#rb_cb_' + codPropiedad).attr('checked', false);
					$('#rb_sb_' + codPropiedad).attr('checked', false);
					$('#ddl_tm_' + codPropiedad).val('');
				}
			}
			
			//Reconstruimos
			var str = '';
			$('#lb_' + codPropiedad + ' option').each(function(index, option){
				str += option.value + '||';
			});
			$('#hf_' + codPropiedad).val(str);
		}
		
		function remueveValorMultiple(codPropiedad)
		{
			$('#lb_' + codPropiedad + ' option:selected').remove();
			
			//Reconstruimos
			var str = '';
			$('#lb_' + codPropiedad + ' option').each(function(index, option){
				str += option.value + '||';
			});
			$('#hf_' + codPropiedad).val(str);			
		}
				</script>
	</HEAD>
	<body>
		<form id="frmMain" method="post" runat="server">
			<TABLE id="Table3" border="0" cellSpacing="0" cellPadding="5">
				<TR>
					<TD vAlign="top">
						<TABLE id="Table1" border="0" cellSpacing="3" cellPadding="0">
							<TR>
								<TD class="cma-nombre-campo-chico" noWrap>Tipo de equipo:</TD>
								<TD><asp:dropdownlist id="ddlTipoArtefacto" runat="server" AutoPostBack="True" CssClass="cma-texto"></asp:dropdownlist></TD>
							</TR>
						</TABLE>
						<asp:panel style="Z-INDEX: 0" id="pNombreEquipo" runat="server">
							<TABLE style="Z-INDEX: 0" id="Table2" border="0" cellSpacing="3" cellPadding="0">
								<TR>
									<TD class="cma-nombre-campo-chico" noWrap>Nombre equipo:</TD>
									<TD>
										<asp:textbox style="Z-INDEX: 0" id="tbMaquina" runat="server" CssClass="cma-texto" Width="224px"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="cma-nombre-campo-chico" colSpan="2"><BR>
										(*) Si el nombre del equipo no existe, digítelo y<BR>
										será creado una vez grabado los datos.</TD>
								</TR>
							</TABLE>
						</asp:panel>
						<P>&nbsp;</P>
						<P>&nbsp;</P>
						<asp:panel style="Z-INDEX: 0" id="pBotonera" runat="server" HorizontalAlign="Center">
<asp:Button id="bGuardar" runat="server" CssClass="cma-boton-accion-principal" Text="guardar"></asp:Button>&nbsp; 
<INPUT class="cma-boton-accion-principal" onclick="window.parent.cerrarDialogo();" value="cerrar"
								type="button" name="cerrar">
						</asp:panel></TD>
					<TD vAlign="top"><asp:panel style="Z-INDEX: 0" id="pValores" runat="server">
							<FIELDSET>
								<LEGEND class="cma-titulo1">Valores</LEGEND>
								<asp:placeholder id="phPropiedades" runat="server"></asp:placeholder>
							</FIELDSET>
						</asp:panel></TD>
				</TR>
				<TR>
					<TD colSpan="2" align="center"><INPUT style="Z-INDEX: 0" id="hfIdRegistroCliente" type="hidden" name="hfIdRegistroCliente"
							runat="server"><INPUT style="Z-INDEX: 0" id="hfIdValor" type="hidden" name="hfIdValor" runat="server"><INPUT style="Z-INDEX: 0" id="hfRazonSocial" type="hidden" name="hfIdValor" runat="server"></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
