<%@ Register TagPrefix="Wilson" Namespace="Wilson.MasterPages" Assembly="WilsonMasterPages" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="vta_detalle_documento.aspx.vb" Inherits="app.vta_detalle_documento"%>
<META content="text/html; charset=iso-8859-1" http-equiv="Content-Type">
<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
<wilson:masterpage id="MPContainer" runat="server">
	<WILSON:CONTENTREGION id="MPTitle" runat="server">Detalle 
Documento</WILSON:CONTENTREGION>
	<WILSON:CONTENTREGION id="MPCaption" runat="server">Detalle Documento</WILSON:CONTENTREGION>
	<TABLE id="Table1" border="0" cellSpacing="0" cellPadding="4" width="98%">
		<TR>
			<TD>
				<asp:label id="lbErrors" runat="server" Visible="False" Width="90%" CssClass="txt-FatalMessage"></asp:label></TD>
		</TR>
		<TR>
			<TD align="left">
				<TABLE id="Table6" border="0" cellSpacing="0" cellPadding="0" width="100%">
					<TR>
						<TD vAlign="top">
							<TABLE id="Table4" border="1" cellSpacing="0" cellPadding="2">
								<TR>
									<TD>
										<TABLE id="Table5" border="0" cellSpacing="0" cellPadding="0">
											<TR>
												<TD><IMG onclick="javascript:history.back();" alt="Volver" src="/images/arrow_blue.gif"></TD>
												<TD vAlign="middle">&nbsp;<A href="Javascript: history.back();">Volver</A>&nbsp;
												</TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
							</TABLE>
						</TD>
						<TD></TD>
						<TD vAlign="top" align="right"><IMG onclick="javascript:imprimir();" border="0" alt="Imprimir" src="/images/imprimir.gif"></TD>
					</TR>
				</TABLE>
			</TD>
		</TR>
		<TR>
			<TD align="center">
				<TABLE id="Table2" border="1" cellSpacing="0" cellPadding="0" width="100%">
					<TR>
						<TD>
							<TABLE id="Table7" border="0" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD style="WIDTH: 400px" vAlign="top" align="left">
										<TABLE id="Table3" border="1" cellSpacing="0" cellPadding="2" width="100%">
											<TR>
												<TD class="tbl-HorizHeader" bgColor="#b0c4de" height="19" colSpan="8"><STRONG>DATOS DE 
														ENCABEZADO</STRONG></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 76px; HEIGHT: 25px" class="tbl-HorizHeader">Docto</TD>
												<TD style="WIDTH: 5px; HEIGHT: 25px">:</TD>
												<TD style="HEIGHT: 25px" align="left">
													<asp:label id="lbNum_Doc" runat="server" CssClass="tbl-HorizItem"></asp:label></TD>
												<TD style="HEIGHT: 25px" colSpan="2" align="right"></TD>
												<TD style="HEIGHT: 25px" class="tbl-HorizHeader" align="right"></TD>
												<TD style="HEIGHT: 25px" align="right"></TD>
												<TD style="HEIGHT: 25px" align="left"></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 76px" class="tbl-HorizHeader">Fecha</TD>
												<TD style="WIDTH: 5px">:</TD>
												<TD align="left">
													<asp:label id="lbFec_Doc" runat="server" CssClass="tbl-HorizItem"></asp:label></TD>
												<TD colSpan="2" align="right"></TD>
												<TD class="tbl-HorizHeader" align="right">Vence</TD>
												<TD align="right">:</TD>
												<TD align="left">
													<asp:Label id="lbFecVenc" runat="server" CssClass="tbl-HorizItem"></asp:Label></TD>
											</TR>
											<TR>
												<TD bgColor="#b0c4de" height="2" colSpan="8"></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 76px" class="tbl-HorizHeader">Cliente</TD>
												<TD style="WIDTH: 5px">:</TD>
												<TD colSpan="6" noWrap>
													<asp:Label id="lbCodCliente" runat="server" CssClass="tbl-HorizItem"></asp:Label>&nbsp;-
													<asp:label id="lbNom_Cliente" runat="server" CssClass="tbl-HorizItem"></asp:label></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 76px; HEIGHT: 26px" class="tbl-HorizHeader">C.Pago</TD>
												<TD style="WIDTH: 5px; HEIGHT: 26px">:</TD>
												<TD style="HEIGHT: 25px" colSpan="6">
													<asp:Label id="lbCPago" runat="server" CssClass="tbl-HorizItem"></asp:Label>&nbsp;&nbsp;-&nbsp;
													<asp:Label id="lbdiasPago" runat="server" CssClass="tbl-HorizItem"></asp:Label></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 76px" class="tbl-HorizHeader">Vend.</TD>
												<TD style="WIDTH: 5px">:</TD>
												<TD colSpan="6">
													<asp:label id="lbNom_Vend" runat="server" CssClass="tbl-HorizItem"></asp:label></TD>
											</TR>
											<TR>
												<TD bgColor="#b0c4de" height="2" colSpan="8"></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 76px" class="tbl-HorizHeader">Moneda:</TD>
												<TD style="WIDTH: 5px"></TD>
												<TD>
													<asp:DropDownList id="ddlMoneda" runat="server" CssClass="listBox" AutoPostBack="True">
														<asp:ListItem Value="DOL" Selected="True">Dolares</asp:ListItem>
														<asp:ListItem Value="PES">Pesos</asp:ListItem>
													</asp:DropDownList></TD>
												<TD colSpan="6">
													<asp:Label id="lbNum_Docto_Sap" runat="server" CssClass="tbl-HorizItem" Font-Bold="True"></asp:Label></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 76px" class="tbl-HorizHeader">Total Neto $</TD>
												<TD style="WIDTH: 5px">:</TD>
												<TD colSpan="6">&nbsp;
													<asp:Label id="lbMonto_Peso" runat="server" CssClass="tbl-HorizItem"></asp:Label></TD>
											</TR>
										</TABLE>
										<TABLE id="Table8" border="1" cellSpacing="0" cellPadding="2" width="400">
											<TR>
												<TD class="tbl-HorizHeader" width="99">Val. Cambio</TD>
												<TD width="12">:</TD>
												<TD width="289">&nbsp;
													<asp:Label id="lbValCambio" runat="server" CssClass="tbl-HorizItem"></asp:Label></TD>
											</TR>
										</TABLE>
									</TD>
									<TD width="20" align="right"></TD>
									<TD vAlign="top" align="right">
										<TABLE id="Table3" border="1" cellSpacing="0" cellPadding="2" width="100%" DESIGNTIMEDRAGDROP="3323">
											<TR>
												<TD class="tbl-HorizHeader" bgColor="#b0c4de" colSpan="8"><STRONG>INFORMACION DE 
														DESPACHO</STRONG></TD>
											</TR>
											<TR>
												<TD class="tbl-HorizHeader">No. Guía Rep.</TD>
												<TD width="1">:</TD>
												<TD colSpan="2">
													<asp:Label id="lbNum_Reparto" runat="server" CssClass="tbl-HorizItem"></asp:Label></TD>
												<TD align="right"></TD>
												<TD class="tbl-HorizHeader" align="right">Fecha Guía Rep.</TD>
												<TD align="right">:</TD>
												<TD align="right">
													<asp:Label id="lbFec_Reparto" runat="server" CssClass="tbl-HorizItem"></asp:Label></TD>
											</TR>
											<TR>
												<TD class="tbl-HorizHeader">Fecha Salida</TD>
												<TD width="1">:</TD>
												<TD colSpan="2">
													<asp:Label id="lbFecSalida" runat="server" CssClass="tbl-HorizItem"></asp:Label></TD>
												<TD align="right"></TD>
												<TD class="tbl-HorizHeader" align="right">Hora Salida</TD>
												<TD align="right">:</TD>
												<TD align="right">
													<asp:Label id="lbHoraSalida" runat="server" CssClass="tbl-HorizItem"></asp:Label></TD>
											</TR>
											<TR>
												<TD class="tbl-HorizHeader">Fecha Llegada</TD>
												<TD width="1">:</TD>
												<TD colSpan="2">
													<asp:Label id="lbFecLlegada" runat="server" CssClass="tbl-HorizItem"></asp:Label></TD>
												<TD align="right"></TD>
												<TD class="tbl-HorizHeader" align="right">Hora Llegada</TD>
												<TD align="right">:</TD>
												<TD align="right">
													<asp:Label id="lbHoraLlegada" runat="server" CssClass="tbl-HorizItem"></asp:Label></TD>
											</TR>
											<TR>
												<TD class="tbl-HorizHeader">Dir. Desp.</TD>
												<TD width="1">:</TD>
												<TD colSpan="6">
													<asp:Label id="lbDirDes" runat="server" CssClass="tbl-HorizItem"></asp:Label></TD>
											</TR>
											<TR>
												<TD class="tbl-HorizHeader">Vehiculo</TD>
												<TD width="1">:</TD>
												<TD colSpan="6">
													<asp:Label id="lbVehiculo" runat="server" CssClass="tbl-HorizItem"></asp:Label></TD>
											</TR>
											<TR>
												<TD class="tbl-HorizHeader">Transporte</TD>
												<TD width="1">:</TD>
												<TD colSpan="2">
													<asp:Label id="lbTrans" runat="server" CssClass="tbl-HorizItem"></asp:Label></TD>
												<TD align="right"></TD>
												<TD class="tbl-HorizHeader" align="right">No. de Boleto</TD>
												<TD align="right">:</TD>
												<TD align="left">
													<asp:Label id="lbBoleto" runat="server" CssClass="tbl-HorizItem"></asp:Label></TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD height="2" background="images/line_dot_horiz.gif"><IMG src="/images/line_dot_horiz.gif"></TD>
					</TR>
					<TR>
						<TD colSpan="2" align="left"><BR>
							<asp:datagrid id="dgDetalle" runat="server" Width="100%" ShowFooter="True" AllowSorting="True"
								ToolTip="Listado de Items" CellPadding="3" AutoGenerateColumns="False">
								<AlternatingItemStyle CssClass="tbl-DataGridItemAlternating"></AlternatingItemStyle>
								<ItemStyle CssClass="tbl-DataGridItem"></ItemStyle>
								<HeaderStyle CssClass="tbl-DataGridHeader" BackColor="LightSteelBlue"></HeaderStyle>
								<FooterStyle CssClass="tbl-DataGridFooter"></FooterStyle>
								<Columns>
									<asp:BoundColumn DataField="Cod_Producto" ReadOnly="True" HeaderText="Codigo"></asp:BoundColumn>
									<asp:BoundColumn DataField="Des_Producto" ReadOnly="True" HeaderText="Descripci&#243;n">
										<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="val_cant_umv" HeaderText="Cantidad" DataFormatString="{0:#,##0}">
										<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="cod_umv" HeaderText="UMV">
										<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Val_Precio_Origen" ReadOnly="True" HeaderText="Neto" DataFormatString="{0:#,##0.0000}">
										<HeaderStyle HorizontalAlign="Right" Width="70px"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<FooterStyle HorizontalAlign="Right"></FooterStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="val_zmar_ori" HeaderText="Margen" DataFormatString="{0:#,##0.0000}">
										<HeaderStyle HorizontalAlign="Right" Width="70px"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<FooterStyle HorizontalAlign="Right"></FooterStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="val_volumen" HeaderText="Volumen" DataFormatString="{0:#,##0.0000}">
										<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<FooterStyle HorizontalAlign="Right"></FooterStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="val_zges_ori" HeaderText="C. Gest." DataFormatString="{0:#,##0.0000}">
										<HeaderStyle HorizontalAlign="Right" Width="70px"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<FooterStyle HorizontalAlign="Right"></FooterStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="val_neto_srcp_ori" HeaderText="Neto (S/RCP)" DataFormatString="{0:#,##0.0000}">
										<HeaderStyle HorizontalAlign="Right" Width="100px"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<FooterStyle HorizontalAlign="Right"></FooterStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="p_zmar_ori" HeaderText="% Mg." DataFormatString="{0:#,##0.0000}">
										<HeaderStyle HorizontalAlign="Right" Width="70px"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<FooterStyle HorizontalAlign="Right"></FooterStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="val_precio_dolar" DataFormatString="{0:#,##0.0000}">
										<FooterStyle HorizontalAlign="Right"></FooterStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="val_costo_dolar" DataFormatString="{0:#,##0.0000}">
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="val_precio_origen" DataFormatString="{0:#,##0.0000}">
										<FooterStyle HorizontalAlign="Right"></FooterStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="val_costo_origen" DataFormatString="{0:#,##0.0000}">
										<FooterStyle HorizontalAlign="Right"></FooterStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="val_precio_pesos" DataFormatString="{0:#,##0.0000}">
										<FooterStyle HorizontalAlign="Right"></FooterStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="val_costo_pesos" DataFormatString="{0:#,##0.0000}">
										<FooterStyle HorizontalAlign="Right"></FooterStyle>
									</asp:BoundColumn>
									<asp:BoundColumn HeaderText="Total Item" DataFormatString="{0:#,##0.0000}">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<FooterStyle HorizontalAlign="Right"></FooterStyle>
									</asp:BoundColumn>
								</Columns>
							</asp:datagrid>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						</TD>
					</TR>
				</TABLE>
			</TD>
		</TR>
	</TABLE>
</wilson:masterpage>
