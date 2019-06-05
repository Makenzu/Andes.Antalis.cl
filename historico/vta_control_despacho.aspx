<%@ Page Language="vb" AutoEventWireup="false" Codebehind="vta_control_despacho.aspx.vb" Inherits="app.vta_control_despacho"%>
<%@ Register TagPrefix="Wilson" Namespace="Wilson.MasterPages" Assembly="WilsonMasterPages" %>
<META content="text/html; charset=iso-8859-1" http-equiv="Content-Type">
<LINK href="css/calendar.css" type="text/css" rel="stylesheet">
<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
<wilson:masterpage id="MPContainer" runat="server">
	<WILSON:CONTENTREGION id="MPTitle" runat="server">ANDES</WILSON:CONTENTREGION>
	<WILSON:CONTENTREGION id="MPCaption" runat="server">Control de Despacho</WILSON:CONTENTREGION>
	<asp:Label id="lbErrors" runat="server" CssClass="txt-FatalMessage"></asp:Label>
	<SCRIPT language="javascript" src="/js/CalendarPopup.js"></SCRIPT>
	<SCRIPT language="javascript">
			var cal1 = new CalendarPopup(('caldiv1'));		
			var cal2 = new CalendarPopup(('caldiv2'));		
	</SCRIPT>
	<TABLE id="Table1" border="0" cellSpacing="0" cellPadding="3" width="100%">
		<TR>
			<TD>
				<asp:Panel id="plRango" runat="server">
					<BR>
					<TABLE id="Table3" border="0" cellSpacing="0" cellPadding="0" width="300" align="center">
						<TR>
							<TD vAlign="bottom">
								<TABLE id="Table4" border="0" cellSpacing="0" cellPadding="0">
									<TR>
										<TD bgColor="whitesmoke"><IMG alt="" align="middle" src="/images/lupa_small.gif"></TD>
										<TD><IMG alt="" src="/images/tab_cliente_fecha.gif"></TD>
										<TD>
											<asp:ImageButton id="ibProdOff" runat="server" ImageUrl="/images/tab_cliente_item_off.gif" CausesValidation="False"></asp:ImageButton></TD>
										<TD>
											<asp:ImageButton id="ibFacturaOff" runat="server" ImageUrl="/images/tab_factura_off.gif" CausesValidation="False"></asp:ImageButton></TD>
									</TR>
								</TABLE>
							</TD>
							<TD vAlign="baseline" align="center"></TD>
						</TR>
						<TR>
							<TD bgColor="whitesmoke" colSpan="2">
								<TABLE id="tbParams" border="0" cellSpacing="0" cellPadding="2" width="350" align="center">
									<TR>
										<TD class="tbl-HorizHeader" height="15" width="5" colSpan="5"></TD>
									</TR>
									<TR>
										<TD class="tbl-HorizHeader" width="5"></TD>
										<TD class="tbl-HorizHeader" width="120">Cliente</TD>
										<TD class="tbl-HorizHeader" width="2">:</TD>
										<TD colSpan="2">
											<TABLE id="Table11" border="0" cellSpacing="0" cellPadding="0">
												<TR>
													<TD>
														<asp:TextBox id="txCliente" runat="server" CssClass="textBox" MaxLength="12" Width="90px"></asp:TextBox></TD>
													<TD><INPUT id="btBuscar1" title="Buscar código de cliente" onclick="javascript: findCliente('txCliente');"
															src="/images/buscar.gif" type="image"></TD>
												</TR>
											</TABLE>
										</TD>
									</TR>
									<TR>
										<TD class="tbl-HorizHeader" width="5"></TD>
										<TD class="tbl-HorizHeader" width="120">Fecha Inicio</TD>
										<TD class="tbl-HorizHeader">:</TD>
										<TD width="150">
											<TABLE id="Table2" border="0" cellSpacing="0" cellPadding="0">
												<TR>
													<TD>
														<asp:TextBox id="txDesde" runat="server" CssClass="textBox" Width="100px" ReadOnly="True"></asp:TextBox></TD>
													<TD align="center">&nbsp;<IMG id="ibtDesde" onclick="cal1.select(document.forms[0].txDesde,'ibtDesde','dd / MM / yyyy'); return false;"
															name="ibtDesde" alt="Elegir fecha" src="/images/calendar.gif">
													</TD>
												</TR>
											</TABLE>
										</TD>
										<TD width="70"></TD>
									</TR>
									<TR>
										<TD class="tbl-HorizHeader" width="5"></TD>
										<TD class="tbl-HorizHeader" width="120">Fecha Termino</TD>
										<TD class="tbl-HorizHeader">:</TD>
										<TD>
											<TABLE id="Table2" border="0" cellSpacing="0" cellPadding="0">
												<TR>
													<TD>
														<asp:TextBox id="txHasta" runat="server" CssClass="textBox" Width="100px" ReadOnly="True"></asp:TextBox></TD>
													<TD align="center">&nbsp;<IMG id="ibtHasta" onclick="cal2.select(document.forms[0].txHasta,'ibtHasta','dd / MM / yyyy'); return false;"
															name="ibtHasta" alt="Elegir fecha" src="/images/calendar.gif"></TD>
												</TR>
											</TABLE>
										</TD>
										<TD>
											<asp:ImageButton id="btSend" runat="server" ImageUrl="/images/procesar.gif"></asp:ImageButton></TD>
									</TR>
									<TR>
										<TD height="20"></TD>
										<TD height="20" colSpan="4">
											<asp:RequiredFieldValidator id="rfvCliente" runat="server" CssClass="txt-AlertMessage" Display="Dynamic" ErrorMessage="* Debe ingresar un codigo de cliente.<br>"
												ControlToValidate="txCliente"></asp:RequiredFieldValidator>
											<asp:RequiredFieldValidator id="rfvFechaIni" runat="server" CssClass="txt-AlertMessage" Display="Dynamic" ErrorMessage="* Debe ingresar una fecha de inicio.<BR>"
												ControlToValidate="txDesde"></asp:RequiredFieldValidator>
											<asp:RequiredFieldValidator id="rfvFechaHasta" runat="server" CssClass="txt-AlertMessage" Display="Dynamic"
												ErrorMessage="* Debe ingresar una fecha de término.<BR>" ControlToValidate="txHasta"></asp:RequiredFieldValidator>
											<asp:CompareValidator id="cvFechas" runat="server" CssClass="txt-AlertMessage" Display="Dynamic" ErrorMessage="* Fecha Inicio debe ser mayor a Fecha Termino<BR>"
												ControlToValidate="txDesde" Visible="False" ControlToCompare="txHasta" Operator="LessThan" Type="Date"
												EnableClientScript="False"></asp:CompareValidator></TD>
									</TR>
								</TABLE>
							</TD>
						</TR>
					</TABLE>
				</asp:Panel>
				<asp:Panel id="plFactura" runat="server" Visible="False">
					<BR>
					<TABLE id="Table5" border="0" cellSpacing="0" cellPadding="0" width="300" align="center">
						<TR>
							<TD vAlign="bottom">
								<TABLE id="Table6" border="0" cellSpacing="0" cellPadding="0">
									<TR>
										<TD>
											<asp:ImageButton id="ibCliRanOff" runat="server" ImageUrl="/images/tab_cliente_fecha_off.gif" CausesValidation="False"></asp:ImageButton></TD>
										<TD>
											<asp:ImageButton id="ibProdOff1" runat="server" ImageUrl="/images/tab_cliente_item_off.gif" CausesValidation="False"></asp:ImageButton></TD>
										<TD bgColor="#f5f5f5"><IMG alt="" align="middle" src="/images/lupa_small.gif"></TD>
										<TD bgColor="whitesmoke"><IMG alt="" src="/images/tab_factura.gif" height="23"></TD>
									</TR>
								</TABLE>
							</TD>
						</TR>
						<TR>
							<TD bgColor="#f5f5f5">
								<TABLE id="Table7" border="0" cellSpacing="0" cellPadding="2" width="350">
									<TR>
										<TD height="15" width="5" colSpan="5"></TD>
									</TR>
									<TR>
										<TD width="5"></TD>
										<TD style="WIDTH: 111px" class="tbl-HorizHeader" width="111">N°. Documento</TD>
										<TD style="WIDTH: 5px" class="tbl-HorizHeader" width="5">:</TD>
										<TD width="150">
											<asp:TextBox id="txFactura" runat="server" CssClass="textBox" Width="90px"></asp:TextBox></TD>
										<TD align="left">
											<asp:ImageButton id="btAceptarF" runat="server" ImageUrl="/images/procesar.gif"></asp:ImageButton></TD>
									</TR>
									<TR>
										<TD width="5"></TD>
										<TD class="tbl-HorizHeader" colSpan="4">
											<asp:RequiredFieldValidator id="rfvFactura" runat="server" Width="100%" Display="Dynamic" ErrorMessage="* Debe ingresar un número de factura."
												ControlToValidate="txFactura"></asp:RequiredFieldValidator></TD>
									</TR>
								</TABLE>
							</TD>
						</TR>
					</TABLE>
				</asp:Panel>
				<asp:Panel id="plItem" runat="server" Visible="False">
					<BR>
					<TABLE id="Table8" border="0" cellSpacing="0" cellPadding="0" width="300" align="center">
						<TR>
							<TD vAlign="bottom">
								<TABLE id="Table9" border="0" cellSpacing="0" cellPadding="0">
									<TR>
										<TD>
											<asp:ImageButton id="ibCliFecOff" runat="server" ImageUrl="/images/tab_cliente_fecha_off.gif" CausesValidation="False"></asp:ImageButton></TD>
										<TD bgColor="#f5f5f5"><IMG alt="" align="middle" src="/images/lupa_small.gif"></TD>
										<TD bgColor="#f5f5f5"><IMG alt="" src="/images/tab_cliente_item.gif"></TD>
										<TD>
											<asp:ImageButton id="ibIFacturaOff" runat="server" ImageUrl="/images/tab_factura_off.gif" CausesValidation="False"></asp:ImageButton></TD>
									</TR>
								</TABLE>
							</TD>
						</TR>
						<TR>
							<TD bgColor="#f5f5f5">
								<TABLE id="Table10" border="0" cellSpacing="0" cellPadding="2" width="350">
									<TR>
										<TD height="15" width="5" colSpan="5"></TD>
									</TR>
									<TR>
										<TD width="5"></TD>
										<TD class="tbl-HorizHeader" width="90">Cliente</TD>
										<TD class="tbl-HorizHeader" width="2">:</TD>
										<TD colSpan="2">
											<TABLE id="Table12" border="0" cellSpacing="0" cellPadding="0">
												<TR>
													<TD>
														<asp:TextBox id="txICliente" runat="server" CssClass="textBox" MaxLength="12" Width="90px"></asp:TextBox></TD>
													<TD><INPUT id="Button1" title="Buscar código de cliente" onclick="javascript: findCliente('txICliente');"
															src="/images/buscar.gif" type="image" name="Button1"></TD>
												</TR>
											</TABLE>
										</TD>
									</TR>
									<TR>
										<TD width="5"></TD>
										<TD class="tbl-HorizHeader" width="90">Producto</TD>
										<TD class="tbl-HorizHeader" width="2">:</TD>
										<TD width="150">
											<asp:TextBox id="txICodProd" runat="server" CssClass="textBox" MaxLength="12" Width="100px"></asp:TextBox></TD>
										<TD align="left">
											<asp:ImageButton id="btAceptarI" runat="server" ImageUrl="/images/procesar.gif"></asp:ImageButton></TD>
									</TR>
									<TR>
										<TD width="5"></TD>
										<TD class="tbl-HorizHeader" colSpan="4">
											<asp:RequiredFieldValidator id="rfvICliente" runat="server" CssClass="txt-AlertMessage" Width="100%" Display="Dynamic"
												ErrorMessage="* Debe ingresar el código de cliente.<BR>" ControlToValidate="txICliente"></asp:RequiredFieldValidator>
											<asp:RequiredFieldValidator id="rfvIProd" runat="server" CssClass="txt-AlertMessage" Display="Dynamic" ErrorMessage="* Debe ingresar el código de producto."
												ControlToValidate="txICodProd"></asp:RequiredFieldValidator></TD>
									</TR>
								</TABLE>
							</TD>
						</TR>
					</TABLE>
				</asp:Panel></TD>
		</TR>
		<TR>
			<TD colSpan="2" align="center">
				<asp:DataGrid id="dgResultado" runat="server" AutoGenerateColumns="False" CellPadding="3">
					<AlternatingItemStyle CssClass="tbl-DataGridItemAlternating"></AlternatingItemStyle>
					<ItemStyle CssClass="tbl-DataGridItem"></ItemStyle>
					<HeaderStyle CssClass="tbl-DataGridHeader"></HeaderStyle>
					<Columns>
						<asp:BoundColumn DataField="Fec_Documento" SortExpression="Fec_Documento DESC" ReadOnly="True" HeaderText="Fec. Docto."
							DataFormatString="{0:dd/MMM/yyyy}"></asp:BoundColumn>
						<asp:BoundColumn DataField="Cod_Documento" ReadOnly="True" HeaderText="T.Doc"></asp:BoundColumn>
						<asp:BoundColumn DataField="Num_Documento" SortExpression="Num_Documento DESC" ReadOnly="True" HeaderText="No. Docto">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="num_pedido" SortExpression="num_pedido DESC" ReadOnly="True" HeaderText="No. Pedido">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="Monto_Peso" ReadOnly="True" HeaderText="Neto ($)">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn Visible="False" DataField="ano_periodo" HeaderText="ano_periodo"></asp:BoundColumn>
						<asp:BoundColumn Visible="False" DataField="mes_periodo" HeaderText="mes_periodo"></asp:BoundColumn>
						<asp:BoundColumn Visible="False" DataField="cod_filial" HeaderText="cod_filial"></asp:BoundColumn>
						<asp:BoundColumn Visible="False" DataField="cod_sucursal" HeaderText="cod_sucursal"></asp:BoundColumn>
					</Columns>
				</asp:DataGrid></TD>
		</TR>
		<TR>
			<TD colSpan="2"></TD>
		</TR>
	</TABLE>
	<DIV style="POSITION: absolute; BACKGROUND-COLOR: white; VISIBILITY: hidden; layer-background-color: white"
		id="caldiv1"></DIV>
	<DIV style="POSITION: absolute; BACKGROUND-COLOR: white; VISIBILITY: hidden; layer-background-color: white"
		id="caldiv2"></DIV>
</wilson:masterpage>
