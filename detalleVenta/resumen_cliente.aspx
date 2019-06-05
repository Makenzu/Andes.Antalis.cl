<%@ Page Language="vb" AutoEventWireup="false" Codebehind="resumen_cliente.aspx.vb" Inherits="app.vta_x_cliente"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Consulta Vendedoras</title>
		<LINK href="/css/andes.css" type="text/css" rel="stylesheet">
		<LINK href="/css/menu.css" type="text/css" rel="stylesheet">
		<LINK href="/css/calendar.css" type="text/css" rel="stylesheet">
		<style type="text/css">.tbl-DataGridItem_1 { BORDER-BOTTOM-COLOR: black; BACKGROUND-COLOR: aliceblue; BORDER-TOP-COLOR: black; FONT-FAMILY: Arial, Helvetica, sans-serif; COLOR: #000000; BORDER-RIGHT-COLOR: black; FONT-SIZE: 10px; BORDER-LEFT-COLOR: black; TEXT-DECORATION: none }
	.tbl-DataGridItemAlternating_1 { BORDER-BOTTOM-COLOR: black; BACKGROUND-COLOR: #e8f0ff; BORDER-TOP-COLOR: black; FONT-FAMILY: Arial, Helvetica, sans-serif; COLOR: #000000; BORDER-RIGHT-COLOR: black; FONT-SIZE: 10px; BORDER-LEFT-COLOR: black; TEXT-DECORATION: none }
		</style>
		<style type="text/css">.tbl-DataGridItem_2 { BORDER-BOTTOM-COLOR: black; BACKGROUND-COLOR: aliceblue; BORDER-TOP-COLOR: black; FONT-FAMILY: Arial, Helvetica, sans-serif; COLOR: #000000; BORDER-RIGHT-COLOR: black; FONT-SIZE: 12px; BORDER-LEFT-COLOR: black; TEXT-DECORATION: none }
	.tbl-DataGridItemAlternating_2 { BORDER-BOTTOM-COLOR: black; BACKGROUND-COLOR: #e8f0ff; BORDER-TOP-COLOR: black; FONT-FAMILY: Arial, Helvetica, sans-serif; COLOR: #000000; BORDER-RIGHT-COLOR: black; FONT-SIZE: 12px; BORDER-LEFT-COLOR: black; TEXT-DECORATION: none }
		</style>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="/js/common.js"></script>
		<SCRIPT language="javascript">
		function Validar(){
			var f = document.forms[0]
			var msgObj = MM_findObj('rfvCliente')

			if (MM_trim(f.txCliente.value) == '') {
				msgObj.style.display =  "inline";
				return false;
			}
			else{
				msgObj.style.display =  "none";
				noDblClick('disbtn','btSend');
				f.submit();
				return true;
			}
		}
		</SCRIPT>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td colSpan="3">
						<TABLE id="Table7" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD>
									<TABLE id="tbParams" borderColor="black" cellSpacing="0" cellPadding="0" border="0">
										<TR height="30">
											<TD class="tbl-HorizHeader" width="80">Cliente:</TD>
											<TD><asp:textbox id="txCliente" runat="server" MaxLength="10" CssClass="textBox"></asp:textbox></TD>
											<TD><INPUT title="Buscar código de cliente" onclick="javascript: findCliente('txCliente');"
													type="image" src="/images/buscar.gif"></TD>
											<td><asp:imagebutton id="btSend" runat="server" CausesValidation="False" ImageUrl="/images/procesar.gif"></asp:imagebutton></td>
										</TR>
									</TABLE>
								</TD>
								<TD>
									<table borderColor="black" cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td><asp:requiredfieldvalidator id="rfvCliente" runat="server" CssClass="txt-AlertMessage" ErrorMessage="* Debe ingresar el código del cliente."
													ControlToValidate="txCliente" Display="Dynamic"></asp:requiredfieldvalidator>
												<DIV id="disbtn" style="DISPLAY: none" name="disbtn"><IMG alt="" src="/images/procesando.gif"></DIV>
											</td>
										</tr>
									</table>
								</TD>
							</TR>
							<TR>
								<td><A style="BACKGROUND-COLOR: white; FONT-FAMILY: Arial,helvetica; COLOR: #666666; FONT-SIZE: 9px; FONT-WEIGHT: bold"
										href="../default.aspx">|&nbsp;SALIR&nbsp;|</A></td>
								<td><asp:label id="lbErrors" runat="server" CssClass="txt-FatalMessage" EnableViewState="False"></asp:label></td>
							</TR>
						</TABLE>
					</td>
				</tr>
				<TR>
					<TD align="left" colSpan="3"></TD>
				</TR>
				<tr>
					<td vAlign="top" width="49%">
						<TABLE id="TableIzq" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr width="100%">
								<td class="tbl-FechaHeader" align="center" width="100%">Datos Generales</td>
							</tr>
							<TR width="100%">
								<TD width="100%"><asp:datagrid id="grdDatosGrales" runat="server" ForeColor="Black" AutoGenerateColumns="False"
										Width="100%">
										<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
										<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
										<AlternatingItemStyle CssClass="tbl-DataGridItemAlternating_1"></AlternatingItemStyle>
										<ItemStyle CssClass="tbl-DataGridItem_1"></ItemStyle>
										<HeaderStyle CssClass="tbl-DataGridHeader" Font-Bold="True" ForeColor="White" BackColor="#006699"></HeaderStyle>
										<Columns>
											<asp:BoundColumn DataField="nom_cliente" HeaderText="Raz&#243;n Social"></asp:BoundColumn>
											<asp:BoundColumn DataField="cod_clasificacion" HeaderText="Clasif"></asp:BoundColumn>
											<asp:BoundColumn DataField="nom_promotora" HeaderText="Promotora"></asp:BoundColumn>
											<asp:BoundColumn DataField="nom_cobrador" HeaderText="Cobrador"></asp:BoundColumn>
										</Columns>
										<PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White" Mode="NumericPages"></PagerStyle>
									</asp:datagrid></TD>
							</TR>
							<tr bgColor="black" height="10">
								<td></td>
							</tr>
							<tr>
								<td style="FONT-FAMILY: Verdana, Arial, Helvetica, sans-serif; HEIGHT: 20px; COLOR: #000000; FONT-SIZE: 10px; FONT-WEIGHT: bold; TEXT-DECORATION: none"
									align="center" bgColor="#ff9966">
									Productos NO Comprados</td>
							</tr>
							<TR>
								<TD><asp:datagrid id="grdNoComprados" runat="server" AutoGenerateColumns="False" Width="100%">
										<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
										<SelectedItemStyle Font-Bold="True" ForeColor="White"></SelectedItemStyle>
										<AlternatingItemStyle CssClass="tbl-DataGridItemAlternating_1" BackColor="Wheat"></AlternatingItemStyle>
										<ItemStyle CssClass="tbl-DataGridItem_1" BackColor="PapayaWhip"></ItemStyle>
										<HeaderStyle Font-Bold="True" ForeColor="Black" CssClass="tbl-DataGridHeader" BackColor="#CC9966"></HeaderStyle>
										<Columns>
											<asp:BoundColumn DataField="cod_producto" HeaderText="CodProd"></asp:BoundColumn>
											<asp:TemplateColumn Visible="False">
												<ItemTemplate>
													<asp:Image id="imgEstrella" runat="server" ImageUrl="file:///C:\Inetpub\wwwroot\Andes\images\estrella.GIF"
														Visible="False"></asp:Image>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn DataField="nom_producto" HeaderText="Producto"></asp:BoundColumn>
											<asp:BoundColumn DataField="fec_ultima_compra" HeaderText="Ult Compra"></asp:BoundColumn>
											<asp:BoundColumn DataField="val_cantidad" HeaderText="Cant" DataFormatString="{0:#,##0}">
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="val_precio_unit" HeaderText="Prec Unit" DataFormatString="{0:#,##0}">
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="val_total" HeaderText="Total" DataFormatString="{0:#,##0}">
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn Visible="False" DataField="cod_area" HeaderText="cod_area"></asp:BoundColumn>
											<asp:BoundColumn Visible="False" DataField="cod_familia" HeaderText="cod_familia"></asp:BoundColumn>
											<asp:BoundColumn Visible="False" DataField="des_familia" HeaderText="des_familia"></asp:BoundColumn>
										</Columns>
										<PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White" Mode="NumericPages"></PagerStyle>
									</asp:datagrid></TD>
							</TR>
							<tr bgColor="black" height="10">
								<td></td>
							</tr>
							<tr>
								<td class="tbl-FechaHeader" align="center">Mensajes</td>
							</tr>
							<TR>
								<TD><asp:datagrid id="grdMensajes" runat="server" AutoGenerateColumns="False" Width="100%">
										<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
										<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
										<AlternatingItemStyle CssClass="tbl-DataGridItemAlternating_2"></AlternatingItemStyle>
										<ItemStyle CssClass="tbl-DataGridItem_2"></ItemStyle>
										<HeaderStyle Font-Bold="True" ForeColor="#E0E0E0" CssClass="tbl-DataGridHeader" BackColor="#006699"></HeaderStyle>
										<Columns>
											<asp:BoundColumn DataField="txt_mensaje" HeaderText="Mensaje"></asp:BoundColumn>
											<asp:BoundColumn Visible="False" DataField="cod_tipo_mensaje" HeaderText="cod_tipo_mensaje"></asp:BoundColumn>
										</Columns>
										<PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White" Mode="NumericPages"></PagerStyle>
									</asp:datagrid></TD>
							</TR>
							<tr bgColor="black" height="10">
								<td></td>
							</tr>
							<!-- 03-01-2011 - AGS: Se elimina de acuerdo a lo solicitado por Pilar Jory
							<tr>
								<td class="tbl-FechaHeader" align="center">Productos Comprados mes actual y 2 meses 
									anteriores</td>
							</tr>
							<TR>
								<TD><asp:datagrid id="grdComprados" runat="server" AutoGenerateColumns="False" Width="100%">
										<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
										<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
										<AlternatingItemStyle CssClass="tbl-DataGridItemAlternating_1"></AlternatingItemStyle>
										<ItemStyle CssClass="tbl-DataGridItem_1"></ItemStyle>
										<HeaderStyle Font-Bold="True" ForeColor="#E0E0E0" CssClass="tbl-DataGridHeader" BackColor="#006699"></HeaderStyle>
										<Columns>
											<asp:BoundColumn DataField="cod_producto" HeaderText="CodProd"></asp:BoundColumn>
											<asp:BoundColumn DataField="nom_producto" HeaderText="Producto"></asp:BoundColumn>
											<asp:BoundColumn DataField="fec_ultima_compra" HeaderText="Ult Compra"></asp:BoundColumn>
											<asp:BoundColumn DataField="val_cantidad" HeaderText="Cant" DataFormatString="{0:#,##0}">
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="val_precio_unit" HeaderText="Prec Unit" DataFormatString="{0:#,##0}">
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="val_total" HeaderText="Total" DataFormatString="{0:#,##0}">
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn Visible="False" DataField="cod_area" HeaderText="cod_area"></asp:BoundColumn>
											<asp:BoundColumn Visible="False" DataField="cod_familia" HeaderText="cod_familia"></asp:BoundColumn>
											<asp:BoundColumn Visible="False" DataField="des_familia" HeaderText="des_familia"></asp:BoundColumn>
										</Columns>
										<PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White" Mode="NumericPages"></PagerStyle>
									</asp:datagrid></TD>
							</TR>
							-->
							<!-- 13-09-2010 - AGS: Se elimina de acuerdo a lo solicitado por área ventas
							<tr>
								<td style="FONT-WEIGHT: bold; FONT-SIZE: 10px; COLOR: #000000; FONT-FAMILY: Verdana, Arial, Helvetica, sans-serif; HEIGHT: 20px; TEXT-DECORATION: none"
									align="center" bgColor="#66cc66">Ultimos 3 Otellos</td>
							</tr>
							<TR>
								<TD><asp:datagrid id="grdOtello" AutoGenerateColumns="False" Width="100%" Runat="server">
										<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
										<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
										<AlternatingItemStyle CssClass="tbl-DataGridItemAlternating_1" BackColor="LemonChiffon"></AlternatingItemStyle>
										<ItemStyle CssClass="tbl-DataGridItem_1" BackColor="PaleGoldenrod"></ItemStyle>
										<HeaderStyle CssClass="tbl-DataGridHeader" Font-Bold="True" ForeColor="Black" BackColor="#99cc66"></HeaderStyle>
										<Columns>
											<asp:BoundColumn DataField="num_correcaminos" HeaderText="N&#186; Corrcam"></asp:BoundColumn>
											<asp:BoundColumn DataField="fec_ingreso" HeaderText="Fec Ingreso"></asp:BoundColumn>
											<asp:BoundColumn DataField="fec_visita" HeaderText="Fec Visita"></asp:BoundColumn>
											<asp:BoundColumn DataField="nom_agente" HeaderText="Agente"></asp:BoundColumn>
											<asp:BoundColumn DataField="txt_reportes" HeaderText="Reportes"></asp:BoundColumn>
										</Columns>
										<PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White" Mode="NumericPages"></PagerStyle>
									</asp:datagrid></TD>
							</TR>
							<tr bgColor="black" height="10">
								<td></td>
							</tr>
							--></TABLE>
					</td>
					<td bgColor="black"></td>
					<td vAlign="top" width="49%">
						<table id="TableDer" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="tbl-FechaHeader" align="center">Campañas</td>
							</tr>
							<TR>
								<TD><asp:datagrid id="grdCampañas" runat="server" AutoGenerateColumns="False" Width="100%">
										<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
										<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
										<AlternatingItemStyle CssClass="tbl-DataGridItemAlternating_1" BackColor="PaleGreen"></AlternatingItemStyle>
										<ItemStyle CssClass="tbl-DataGridItem_1" BackColor="LightCyan"></ItemStyle>
										<HeaderStyle Font-Bold="True" ForeColor="Black" CssClass="tbl-DataGridHeader" BackColor="#33CC99"></HeaderStyle>
										<Columns>
											<asp:BoundColumn DataField="cod_tipo_campa&#241;a" HeaderText="Tipo Campa&#241;a"></asp:BoundColumn>
											<asp:BoundColumn DataField="nom_campa&#241;a" HeaderText="Nombre"></asp:BoundColumn>
											<asp:BoundColumn DataField="fec_ini_campa&#241;a" HeaderText="Inicio"></asp:BoundColumn>
											<asp:BoundColumn DataField="fec_fin_campa&#241;a" HeaderText="Termino"></asp:BoundColumn>
											<asp:BoundColumn DataField="cod_obj_campa&#241;a" HeaderText="Cod Obj"></asp:BoundColumn>
											<asp:BoundColumn DataField="nom_obj_campa&#241;a" HeaderText="Objetivo"></asp:BoundColumn>
										</Columns>
										<PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White" Mode="NumericPages"></PagerStyle>
									</asp:datagrid></TD>
							</TR>
							<tr bgColor="black" height="10">
								<td></td>
							</tr>
							<tr>
								<td class="tbl-FechaHeader" align="center">Objetivos</td>
							</tr>
							<TR>
								<TD><asp:datagrid id="grdObjetivos" runat="server" AutoGenerateColumns="False" Width="100%">
										<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
										<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
										<AlternatingItemStyle CssClass="tbl-DataGridItemAlternating_1"></AlternatingItemStyle>
										<ItemStyle CssClass="tbl-DataGridItem_1"></ItemStyle>
										<HeaderStyle Font-Bold="True" ForeColor="#E0E0E0" CssClass="tbl-DataGridHeader" BackColor="#006699"></HeaderStyle>
										<Columns>
											<asp:BoundColumn DataField="nom_nivel_obj" HeaderText="Nivel Objetivo"></asp:BoundColumn>
											<asp:BoundColumn DataField="cod_nivel_obj" HeaderText="Cod Objetivo"></asp:BoundColumn>
											<asp:BoundColumn DataField="des_nivel_obj" HeaderText="Nom Objetivo"></asp:BoundColumn>
											<asp:BoundColumn DataField="dsc_objetivo" HeaderText="Desc Objetivo"></asp:BoundColumn>
										</Columns>
										<PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White" Mode="NumericPages"></PagerStyle>
									</asp:datagrid></TD>
							</TR>
							<tr bgColor="black" height="10">
								<td></td>
							</tr>
							<!-- 03-01-2011 - AGS: Se elimina de acuerdo a lo solicitado por Pilar Jory
							<tr>
								<td class="tbl-FechaHeader" align="center">Plan de Ventas del&nbsp;Cliente</td>
							</tr>
							<TR>
								<TD><asp:datagrid id="grdObjetivosOld" runat="server" AutoGenerateColumns="False" Width="100%">
										<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
										<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
										<AlternatingItemStyle CssClass="tbl-DataGridItemAlternating_1"></AlternatingItemStyle>
										<ItemStyle CssClass="tbl-DataGridItem_1"></ItemStyle>
										<HeaderStyle Font-Bold="True" ForeColor="#E0E0E0" CssClass="tbl-DataGridHeader" BackColor="#006699"></HeaderStyle>
										<Columns>
											<asp:BoundColumn DataField="cod_area" HeaderText="Area"></asp:BoundColumn>
											<asp:BoundColumn DataField="val_meta_vtusd" HeaderText="Meta Vta USD" DataFormatString="{0:#,##0}"></asp:BoundColumn>
											<asp:BoundColumn DataField="val_meta_mgusd" HeaderText="Meta Mg USD" DataFormatString="{0:#,##0}"></asp:BoundColumn>
											<asp:BoundColumn DataField="gls_comentario" HeaderText="Acciones"></asp:BoundColumn>
										</Columns>
										<PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White" Mode="NumericPages"></PagerStyle>
									</asp:datagrid></TD>
							</TR>
							<tr bgColor="black" height="10">
								<td></td>
							</tr>
							-->
							<!-- 03-01-2011 - AGS: Se elimina de acuerdo a lo solicitado por Pilar Jory
							<tr>
								<td class="tbl-FechaHeader" align="center">Ventas Fallidas por Stock Ultimos 2 
									Meses</td>
							</tr>
							<TR>
								<TD><asp:datagrid id="grdVtaFallida" runat="server" AutoGenerateColumns="False" Width="100%">
										<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
										<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
										<AlternatingItemStyle CssClass="tbl-DataGridItemAlternating_1"></AlternatingItemStyle>
										<ItemStyle CssClass="tbl-DataGridItem_1"></ItemStyle>
										<HeaderStyle Font-Bold="True" ForeColor="#E0E0E0" CssClass="tbl-DataGridHeader" BackColor="#006699"></HeaderStyle>
										<Columns>
											<asp:BoundColumn DataField="cod_producto" HeaderText="CodProd"></asp:BoundColumn>
											<asp:BoundColumn DataField="des_producto" HeaderText="Producto"></asp:BoundColumn>
											<asp:BoundColumn DataField="des_unidad" HeaderText="Unid"></asp:BoundColumn>
											<asp:BoundColumn DataField="fec_compra" HeaderText="Fec Compra"></asp:BoundColumn>
											<asp:BoundColumn DataField="num_cantsol" HeaderText="Solic" DataFormatString="{0:#,##0}"></asp:BoundColumn>
											<asp:BoundColumn DataField="num_cantfal" HeaderText="Fallo" DataFormatString="{0:#,##0}"></asp:BoundColumn>
											<asp:BoundColumn DataField="num_cantatp" HeaderText="ATP" DataFormatString="{0:#,##0}"></asp:BoundColumn>
											<asp:BoundColumn DataField="nom_vendedora" HeaderText="Vendedora"></asp:BoundColumn>
											<asp:BoundColumn Visible="False" DataField="cod_area" HeaderText="cod_area"></asp:BoundColumn>
											<asp:BoundColumn Visible="False" DataField="cod_familia" HeaderText="cod_familia"></asp:BoundColumn>
											<asp:BoundColumn Visible="False" DataField="des_familia" HeaderText="des_familia"></asp:BoundColumn>
										</Columns>
										<PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White" Mode="NumericPages"></PagerStyle>
									</asp:datagrid></TD>
							</TR>
							<tr bgColor="black" height="10">
								<td></td>
							</tr>
							-->
							<!-- tr>
								<td class="tbl-FechaHeader" align="center">Ventas Bajo Margen Mínimo Ultimos 2 
									Meses</td>
							</tr>
							<TR>
								<TD><asp:datagrid id="grdVtaMgMin" runat="server" Width="100%" AutoGenerateColumns="False">
										<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
										<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
										<AlternatingItemStyle CssClass="tbl-DataGridItemAlternating_1"></AlternatingItemStyle>
										<ItemStyle CssClass="tbl-DataGridItem_1"></ItemStyle>
										<HeaderStyle Font-Bold="True" ForeColor="#E0E0E0" CssClass="tbl-DataGridHeader" BackColor="#006699"></HeaderStyle>
										<Columns>
											<asp:BoundColumn DataField="cod_producto" HeaderText="CodProd"></asp:BoundColumn>
											<asp:BoundColumn DataField="des_producto" HeaderText="Producto"></asp:BoundColumn>
											<asp:BoundColumn DataField="fec_compra" HeaderText="Fec Compra"></asp:BoundColumn>
											<asp:BoundColumn DataField="val_estimado" HeaderText="Mg Subfam"></asp:BoundColumn>
											<asp:BoundColumn DataField="val_calculado" HeaderText="Mg Venta"></asp:BoundColumn>
											<asp:BoundColumn DataField="val_precio_dolar" HeaderText="$USD Vta"></asp:BoundColumn>
											<asp:BoundColumn DataField="es_precio_manual" HeaderText="Ind Precio Manual">
												<HeaderStyle Width="15px"></HeaderStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="cod_severidad_alarma" HeaderText="Severidad"></asp:BoundColumn>
											<asp:BoundColumn Visible="False" DataField="cod_area" HeaderText="cod_area"></asp:BoundColumn>
											<asp:BoundColumn Visible="False" DataField="cod_familia" HeaderText="cod_familia"></asp:BoundColumn>
											<asp:BoundColumn Visible="False" DataField="des_familia" HeaderText="des_familia"></asp:BoundColumn>
										</Columns>
										<PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White" Mode="NumericPages"></PagerStyle>
									</asp:datagrid></TD>
							</TR>
							<tr bgColor="black" height="10">
								<td></td>
							</tr>
							<tr>
								<td class="tbl-FechaHeader" align="center">Ventas Bajo Margen Seguridad Ultimos 2 
									Meses</td>
							</tr>
							<TR>
								<TD><asp:datagrid id="grdVtaMgSeg" runat="server" Width="100%" AutoGenerateColumns="False">
										<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
										<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
										<AlternatingItemStyle CssClass="tbl-DataGridItemAlternating_1"></AlternatingItemStyle>
										<ItemStyle CssClass="tbl-DataGridItem_1"></ItemStyle>
										<HeaderStyle Font-Bold="True" ForeColor="#E0E0E0" CssClass="tbl-DataGridHeader" BackColor="#006699"></HeaderStyle>
										<Columns>
											<asp:BoundColumn DataField="cod_producto" HeaderText="CodProd"></asp:BoundColumn>
											<asp:BoundColumn DataField="des_producto" HeaderText="Producto"></asp:BoundColumn>
											<asp:BoundColumn DataField="fec_compra" HeaderText="Fec Compra"></asp:BoundColumn>
											<asp:BoundColumn DataField="val_estimado" HeaderText="Mg Subfam"></asp:BoundColumn>
											<asp:BoundColumn DataField="val_calculado" HeaderText="Mg Venta"></asp:BoundColumn>
											<asp:BoundColumn DataField="val_precio_dolar" HeaderText="$USD Vta"></asp:BoundColumn>
											<asp:BoundColumn DataField="es_precio_manual" HeaderText="Ind Precio Manual">
												<HeaderStyle Width="15px"></HeaderStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="cod_severidad_alarma" HeaderText="Severidad"></asp:BoundColumn>
											<asp:BoundColumn Visible="False" DataField="cod_area" HeaderText="cod_area"></asp:BoundColumn>
											<asp:BoundColumn Visible="False" DataField="cod_familia" HeaderText="cod_familia"></asp:BoundColumn>
											<asp:BoundColumn Visible="False" DataField="des_familia" HeaderText="des_familia"></asp:BoundColumn>
										</Columns>
										<PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White" Mode="NumericPages"></PagerStyle>
									</asp:datagrid></TD>
							</TR>
							<tr bgColor="black" height="10">
								<td></td>
							</tr -->
							<!-- 03-01-2011 - AGS: Se elimina de acuerdo a lo solicitado por Pilar Jory
							<tr>
								<td class="tbl-FechaHeader" align="center">Ventas/Clasificación año actual por área</td>
							</tr>
							<TR>
								<TD><asp:datagrid id="grdVentasArea" runat="server" AutoGenerateColumns="False" Width="100%">
										<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
										<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
										<AlternatingItemStyle CssClass="tbl-DataGridItemAlternating_1"></AlternatingItemStyle>
										<ItemStyle CssClass="tbl-DataGridItem_1"></ItemStyle>
										<HeaderStyle Font-Bold="True" ForeColor="White" CssClass="tbl-DataGridHeader" BackColor="#006699"></HeaderStyle>
										<Columns>
											<asp:BoundColumn DataField="nom_titulo"></asp:BoundColumn>
											<asp:BoundColumn DataField="nom_cvi" HeaderText="CVI">
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="nom_eii" HeaderText="EII">
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="nom_pap" HeaderText="PAP">
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="nom_pak" HeaderText="PAK">
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="nom_tot" HeaderText="Total">
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
											</asp:BoundColumn>
										</Columns>
										<PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White" Mode="NumericPages"></PagerStyle>
									</asp:datagrid></TD>
							</TR>
							<tr bgColor="black" height="10">
								<td></td>
							</tr>
							-->
							<tr>
								<td class="tbl-FechaHeader" align="center">Contactos</td>
							</tr>
							<TR>
								<TD><asp:datagrid id="grdContactos" runat="server" ForeColor="Black" AutoGenerateColumns="False" Width="100%">
										<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
										<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
										<AlternatingItemStyle CssClass="tbl-DataGridItemAlternating_1"></AlternatingItemStyle>
										<ItemStyle CssClass="tbl-DataGridItem_1"></ItemStyle>
										<HeaderStyle CssClass="tbl-DataGridHeader" Font-Bold="True" ForeColor="White" BackColor="#006699"></HeaderStyle>
										<Columns>
											<asp:BoundColumn DataField="nom_persona" HeaderText="Nombre Contacto"></asp:BoundColumn>
											<asp:BoundColumn DataField="des_tipo_cargo" HeaderText="Cargo"></asp:BoundColumn>
										</Columns>
										<PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White" Mode="NumericPages"></PagerStyle>
									</asp:datagrid></TD>
							</TR>
							<tr bgColor="black" height="10">
								<td></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
