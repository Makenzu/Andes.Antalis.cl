<%@ Register TagPrefix="Wilson" Namespace="Wilson.MasterPages" Assembly="WilsonMasterPages" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="encuesta.aspx.vb" Inherits="app.encuesta"%>
<META content="text/html; charset=iso-8859-1" http-equiv="Content-Type">
<LINK href="http://localhost/css/andes.css" type="text/css" rel="stylesheet">
<meta content="False" name="vs_snapToGrid">
<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
<wilson:masterpage id="MPContainer" runat="server">
	<P>&nbsp;</P>
	<P>
		<WILSON:CONTENTREGION id="MPTitle" runat="server">Ventas por Cliente - 
Item</WILSON:CONTENTREGION>
		<WILSON:CONTENTREGION id="MPCaption" runat="server">Ventas por Cliente - Item<BR>
<asp:Label id="lbFecha" runat="server"></asp:Label></WILSON:CONTENTREGION>
		<SCRIPT language="javascript">
function showPop(cp,cc,ms,an){
var mywin
	var param 
	param = "width=450,height=460,Top=50,Left=50,toolbar=0,scrollbars=1,location=0,statusbar=0,menubar=0,resizable=1";
	mywin = window.open("vta_x_cliente_item_detalle.aspx?cc="+cc+"&cp="+cp+"&ms="+ms+"&an="+an,"Detalle_Anual",param );
	mywin.focus();
}

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
	</P>
	<TABLE id="Table1" border="0" cellSpacing="0" cellPadding="0" width="95%" align="center">
		<TR>
			<TD style="WIDTH: 714px">
				<P>
					<asp:Label id="lbErrors" runat="server" EnableViewState="False" CssClass="txt-FatalMessage"></asp:Label></P>
			</TD>
			<TD width="30%" align="right"><INPUT title="Imprimir" onclick="javascript:imprimir();return false;" alt="Imprimir" src="/images/imprimir.gif"
					type="image">&nbsp;&nbsp;&nbsp;
				<asp:ImageButton id="ibExportar" title="Exportar a Excel" runat="server" EnableViewState="False"
					ImageUrl="/images/exportar.gif" Visible="False" ToolTip="Exportar a Excel"></asp:ImageButton></TD>
		</TR>
	</TABLE>
	<TABLE id="Table2" border="0" cellSpacing="0" cellPadding="0" width="98%" align="center">
	</TABLE>
	<TABLE id="Table3" border="1" cellSpacing="0" cellPadding="0" width="100%" align="center">
		<TR>
			<TD></TD>
		</TR>
		<TR>
			<TD>
				<P>
					<TABLE id="tblEncabezado" border="0" cellSpacing="0" cellPadding="0" width="70%" runat="server">
						<TR>
							<TD style="HEIGHT: 12px" class="tbl-HorizHeader">Cliente:</TD>
							<TD style="HEIGHT: 12px">
								<asp:TextBox id="txCliente" runat="server" CssClass="textBox" Width="80px" MaxLength="10"></asp:TextBox>&nbsp;&nbsp;
								<INPUT title="Buscar código de cliente" onclick="javascript: findCliente('txCliente');"
									src="/images/buscar.gif" type="image"></TD>
							<TD style="HEIGHT: 12px">
								<asp:Label id="lbRazonSocial" runat="server" CssClass="tbl-HorizItem" Font-Bold="True"></asp:Label></TD>
							<TD style="HEIGHT: 12px">
								<asp:RequiredFieldValidator id="rfvCliente" runat="server" CssClass="txt-AlertMessage" ErrorMessage="* Debe ingresar el código del cliente."
									ControlToValidate="txCliente" Display="Dynamic"></asp:RequiredFieldValidator></TD>
						</TR>
						<TR>
							<TD class="tbl-HorizHeader">direccion:</TD>
							<TD>
								<asp:Label id="lbDireccion" runat="server" CssClass="tbl-HorizItem"></asp:Label></TD>
							<TD class="tbl-HorizHeader">Telefono Movil:</TD>
							<TD>
								<asp:Label id="lbMovil" runat="server" CssClass="tbl-HorizItem"></asp:Label></TD>
						</TR>
						<TR>
							<TD class="tbl-HorizHeader">Comuna:</TD>
							<TD>
								<asp:Label id="lbComuna" runat="server" CssClass="tbl-HorizItem"></asp:Label></TD>
							<TD class="tbl-HorizHeader">Telefono:</TD>
							<TD>
								<asp:Label id="lbFono" runat="server" CssClass="tbl-HorizItem"></asp:Label></TD>
						</TR>
						<TR>
							<TD class="tbl-HorizHeader">Cuidad:</TD>
							<TD>
								<asp:Label id="lbCiudad" runat="server" CssClass="tbl-HorizItem"></asp:Label></TD>
							<TD class="tbl-HorizHeader">Fax:</TD>
							<TD>
								<asp:Label id="lbFax" runat="server" CssClass="tbl-HorizItem"></asp:Label></TD>
						</TR>
						<TR>
							<TD></TD>
							<TD></TD>
							<TD class="tbl-HorizHeader">Ejec. comercial:</TD>
							<TD>
								<asp:Label id="lbPromotora" runat="server" CssClass="tbl-HorizItem"></asp:Label></TD>
						</TR>
						<TR>
							<TD style="HEIGHT: 18px"></TD>
							<TD style="HEIGHT: 18px"></TD>
							<TD style="HEIGHT: 18px" class="tbl-HorizHeader">Contacto 1:</TD>
							<TD style="HEIGHT: 18px">
								<asp:Label id="lbContacto1" runat="server"></asp:Label></TD>
						</TR>
						<TR>
							<TD></TD>
							<TD></TD>
							<TD class="tbl-HorizHeader">Contacto 2:
							</TD>
							<TD>
								<asp:Label id="lbContacto2" runat="server"></asp:Label></TD>
						</TR>
						<TR>
							<TD style="HEIGHT: 18px"></TD>
							<TD style="HEIGHT: 18px"></TD>
							<TD style="HEIGHT: 18px" class="tbl-HorizHeader">Contacto 3:</TD>
							<TD style="HEIGHT: 18px">
								<asp:Label id="lbContacto3" runat="server"></asp:Label></TD>
						</TR>
						<TR>
							<TD></TD>
							<TD class="tbl-HorizHeader">
								<asp:ImageButton id="btSend" runat="server" ImageUrl="/images/procesar.gif" CausesValidation="False"></asp:ImageButton>
								<DIV style="DISPLAY: none" id="disbtn" class="tbl-HorizItem" name="disbtn">
									<asp:Label id="Label1" runat="server" CssClass="tbl-HorizHeader">Consultar Cliente</asp:Label></DIV>
							</TD>
							<TD>
								<DIV style="DISPLAY: none" id="disbtn" class="tbl-HorizItem" name="disbtn">&nbsp;</DIV>
							</TD>
							<TD></TD>
						</TR>
					</TABLE>
				</P>
				<P>
					<TABLE id="tblForm1" border="0" cellSpacing="0" cellPadding="0" width="100%" runat="server">
						<TR>
							<TD style="HEIGHT: 16px" class="tbl-HorizHeader">Promedio Actual</TD>
							<TD style="HEIGHT: 16px">
								<asp:TextBox id="tbVtaMensual" runat="server" CssClass="textBox" Width="80px"></asp:TextBox></TD>
							<TD style="HEIGHT: 16px" class="tbl-HorizHeader">
								<asp:TextBox id="TextBox1" runat="server" CssClass="textBox" Width="80px"></asp:TextBox>&nbsp;Iva 
								Empresa</TD>
							<TD style="HEIGHT: 16px" class="tbl-HorizHeader">
								<P>
									<asp:TextBox id="tbvtaAnual" runat="server" CssClass="textBox" Width="80px"></asp:TextBox>&nbsp;Total 
									Anual</P>
							</TD>
							<TD style="HEIGHT: 16px"></TD>
						</TR>
						<TR>
							<TD style="HEIGHT: 40px" class="tbl-HorizHeader">Tiene preprensa?</TD>
							<TD style="HEIGHT: 40px">
								<asp:RadioButtonList id="rbPrePrensa" runat="server" CssClass="tbl-HorizHeader">
									<asp:ListItem Value="0" Selected="True">Si</asp:ListItem>
									<asp:ListItem Value="1">No</asp:ListItem>
								</asp:RadioButtonList></TD>
							<TD style="HEIGHT: 40px"></TD>
							<TD style="HEIGHT: 40px" class="tbl-HorizHeader">
								<P>Tipo Maquina de Preprensa&nbsp;</P>
							</TD>
							<TD style="HEIGHT: 40px">
								<asp:TextBox id="tbDesPre" runat="server" CssClass="textBox" TextMode="MultiLine"></asp:TextBox></TD>
						</TR>
						<TR>
							<TD style="HEIGHT: 23px" colSpan="6"></TD>
						</TR>
						<TR>
							<TD style="HEIGHT: 482px" class="tbl-HorizHeader" colSpan="6" align="center">
								<TABLE id="tblFicha" border="1" cellSpacing="1" cellPadding="1" width="100%" runat="server">
									<TR>
										<TD>
											<TABLE id="Table11" border="0" cellSpacing="0" cellPadding="0" width="100%">
												<TR>
													<TD class="tbl-HorizHeader" colSpan="2"></TD>
												</TR>
												<TR>
													<TD colSpan="2"></TD>
												</TR>
											</TABLE>
											<TABLE id="Table12" border="0" cellSpacing="0" cellPadding="0" width="100%">
												<TR>
													<TD class="tbl-HorizHeader" colSpan="2">
														<TABLE id="Table8" border="0" cellSpacing="0" cellPadding="0" width="100%">
															<TR>
																<TD class="tbl-HorizHeader" colSpan="2">Rubros</TD>
															</TR>
															<TR>
																<TD>
																	<asp:Panel id="plRubro" runat="server">
																		<asp:DataGrid id="dgRubros" runat="server" AutoGenerateColumns="False">
																			<FooterStyle CssClass="tbl-DataGridFooter"></FooterStyle>
																			<AlternatingItemStyle CssClass="tbl-DataGridItemAlternating"></AlternatingItemStyle>
																			<ItemStyle CssClass="tbl-DataGridItem"></ItemStyle>
																			<HeaderStyle CssClass="tbl-DataGridHeader"></HeaderStyle>
																			<Columns>
																				<asp:BoundColumn Visible="False" DataField="cod_cliente" HeaderText="codCliente"></asp:BoundColumn>
																				<asp:BoundColumn Visible="False" DataField="cod_segmento" HeaderText="codSegmento"></asp:BoundColumn>
																				<asp:BoundColumn DataField="des_segmento" HeaderText="Segmento"></asp:BoundColumn>
																				<asp:BoundColumn DataField="porcentaje" HeaderText="% Venta"></asp:BoundColumn>
																				<asp:EditCommandColumn Visible="False" ButtonType="LinkButton" UpdateText="Update" CancelText="" EditText="Edit"></asp:EditCommandColumn>
																				<asp:ButtonColumn Text="Borrar" CommandName="Delete"></asp:ButtonColumn>
																			</Columns>
																		</asp:DataGrid>
																	</asp:Panel></TD>
																<TD>
																	<asp:Panel id="pldgRubro" runat="server" Visible="False">
																		<TABLE id="tblRubro" border="0" cellSpacing="0" cellPadding="0" runat="server">
																			<TR>
																				<TD class="tbl-HorizHeader">Rubro:</TD>
																				<TD>
																					<asp:DropDownList id="ddlRubros" runat="server" CssClass="textBox"></asp:DropDownList></TD>
																			</TR>
																			<TR>
																				<TD class="tbl-HorizHeader">%&nbsp;Vta:</TD>
																				<TD>
																					<asp:TextBox id="tbPjeVenta" runat="server" CssClass="textBox"></asp:TextBox></TD>
																			</TR>
																			<TR>
																				<TD style="HEIGHT: 19px" colSpan="2">
																					<asp:LinkButton id="lkGrabarRubro" runat="server" CausesValidation="False">Grabar</asp:LinkButton>&nbsp;
																					<asp:LinkButton id="lkbcancelar" runat="server" CausesValidation="False">Cancelar</asp:LinkButton></TD>
																			</TR>
																		</TABLE>
																	</asp:Panel></TD>
															</TR>
															<TR>
																<TD>
																	<asp:LinkButton id="lkNuevoRubro" runat="server" ForeColor="Blue">Nuevo Rubro</asp:LinkButton></TD>
																<TD></TD>
															</TR>
														</TABLE>
													</TD>
												</TR>
												<TR>
													<TD class="tbl-HorizHeader" colSpan="2"></TD>
												</TR>
												<TR>
													<TD class="tbl-HorizHeader" colSpan="2"></TD>
												</TR>
												<TR>
													<TD class="tbl-HorizHeader" colSpan="2">Tipos de Maquinas Impresoras</TD>
												</TR>
												<TR>
													<TD>
														<asp:Panel id="plMaquinas" runat="server">
															<asp:DataGrid id="dgMaquinas" runat="server" AutoGenerateColumns="False">
																<FooterStyle CssClass="tbl-DataGridFooter"></FooterStyle>
																<AlternatingItemStyle CssClass="tbl-DataGridItemAlternating"></AlternatingItemStyle>
																<ItemStyle CssClass="tbl-DataGridItem"></ItemStyle>
																<HeaderStyle CssClass="tbl-DataGridHeader"></HeaderStyle>
																<Columns>
																	<asp:BoundColumn Visible="False" DataField="cod_cliente" HeaderText="cod_cliente">
																		<ItemStyle HorizontalAlign="Center"></ItemStyle>
																	</asp:BoundColumn>
																	<asp:BoundColumn Visible="False" DataField="cod_maquina" HeaderText="cod_maquina">
																		<ItemStyle HorizontalAlign="Center"></ItemStyle>
																	</asp:BoundColumn>
																	<asp:BoundColumn DataField="des_maquina" HeaderText="Maquina">
																		<ItemStyle HorizontalAlign="Center"></ItemStyle>
																	</asp:BoundColumn>
																	<asp:BoundColumn DataField="modelo_maquina" HeaderText="Modelo Maquina">
																		<ItemStyle HorizontalAlign="Center"></ItemStyle>
																	</asp:BoundColumn>
																	<asp:BoundColumn DataField="tam_mantilla" HeaderText="Tipo Mantilla">
																		<ItemStyle HorizontalAlign="Right"></ItemStyle>
																	</asp:BoundColumn>
																	<asp:BoundColumn DataField="n_cuerpos" HeaderText="N&#186; de cuerpos">
																		<ItemStyle HorizontalAlign="Right"></ItemStyle>
																	</asp:BoundColumn>
																	<asp:BoundColumn DataField="t_barniz" HeaderText="Torre de Barniz">
																		<ItemStyle HorizontalAlign="Center"></ItemStyle>
																	</asp:BoundColumn>
																	<asp:BoundColumn DataField="tipo_plancha" HeaderText="Tipo de Planchas">
																		<ItemStyle HorizontalAlign="Center"></ItemStyle>
																	</asp:BoundColumn>
																	<asp:BoundColumn DataField="tam_plancha" HeaderText="Tama&#241;o Plancha">
																		<ItemStyle HorizontalAlign="Right"></ItemStyle>
																	</asp:BoundColumn>
																	<asp:BoundColumn DataField="consumo" HeaderText="Consumo Mensual">
																		<ItemStyle HorizontalAlign="Right"></ItemStyle>
																	</asp:BoundColumn>
																	<asp:BoundColumn DataField="barra" HeaderText="Barras">
																		<ItemStyle HorizontalAlign="Center"></ItemStyle>
																	</asp:BoundColumn>
																	<asp:EditCommandColumn Visible="False" ButtonType="LinkButton" UpdateText="" CancelText="" EditText="Editar"></asp:EditCommandColumn>
																	<asp:ButtonColumn Text="Borrar" CommandName="Delete"></asp:ButtonColumn>
																</Columns>
															</asp:DataGrid>
														</asp:Panel></TD>
													<TD>
														<asp:Panel id="pldgMaquinas" runat="server" Visible="False">
															<TABLE id="Table7" border="0" cellSpacing="0" cellPadding="0" width="100%">
																<TR>
																	<TD style="HEIGHT: 17px" class="tbl-HorizHeader">Maquina:</TD>
																	<TD style="HEIGHT: 17px">
																		<asp:DropDownList id="ddlmaquina" runat="server" CssClass="textBox" AutoPostBack="True"></asp:DropDownList></TD>
																</TR>
																<TR>
																	<TD style="HEIGHT: 20px" class="tbl-HorizHeader">Modelo Maquina</TD>
																	<TD style="HEIGHT: 20px">
																		<asp:DropDownList id="ddlModeloMaquina" runat="server" CssClass="textBox"></asp:DropDownList></TD>
																</TR>
																<TR>
																	<TD class="tbl-HorizHeader">NºCuerpos:</TD>
																	<TD>
																		<asp:TextBox id="tbnCuerpos" runat="server" CssClass="textBox" Width="80px"></asp:TextBox></TD>
																</TR>
																<TR>
																	<TD class="tbl-HorizHeader">Torre Barniz:</TD>
																	<TD>
																		<asp:RadioButtonList id="rbBarniz" runat="server" CssClass="tbl-HorizHeader">
																			<asp:ListItem Value="0" Selected="True">Si</asp:ListItem>
																			<asp:ListItem Value="1">No</asp:ListItem>
																		</asp:RadioButtonList></TD>
																</TR>
																<TR>
																	<TD class="tbl-HorizHeader">Tipo Plancha:</TD>
																	<TD>
																		<asp:DropDownList id="ddlPlancha" runat="server" CssClass="textBox"></asp:DropDownList></TD>
																</TR>
																<TR>
																	<TD style="HEIGHT: 18px" class="tbl-HorizHeader">Tamaño plancha:</TD>
																	<TD style="HEIGHT: 18px">
																		<asp:TextBox id="tbTamPlancha" runat="server" CssClass="textBox" Width="80px"></asp:TextBox></TD>
																</TR>
																<TR>
																	<TD style="HEIGHT: 20px" class="tbl-HorizHeader">Consumo Plancha:</TD>
																	<TD style="HEIGHT: 20px">
																		<asp:TextBox id="txconsumo" runat="server" CssClass="textBox" Width="80px"></asp:TextBox></TD>
																</TR>
																<TR>
																	<TD style="HEIGHT: 18px" class="tbl-HorizHeader">Con barras</TD>
																	<TD style="HEIGHT: 18px">
																		<asp:RadioButtonList id="rbBarras" runat="server" CssClass="tbl-HorizHeader">
																			<asp:ListItem Value="0" Selected="True">Si</asp:ListItem>
																			<asp:ListItem Value="1">No</asp:ListItem>
																		</asp:RadioButtonList></TD>
																</TR>
																<TR>
																	<TD style="HEIGHT: 17px" colSpan="2">
																		<asp:LinkButton id="LKGrabar" runat="server" CausesValidation="False" ForeColor="Blue">Grabar</asp:LinkButton>&nbsp;
																		<asp:LinkButton id="LkCancelar" runat="server" ForeColor="Blue">Cancelar</asp:LinkButton></TD>
																</TR>
															</TABLE>
														</asp:Panel></TD>
												</TR>
												<TR>
													<TD colSpan="2">
														<asp:LinkButton id="lknuevaMaquina" runat="server" ForeColor="Blue">Nueva Maquina</asp:LinkButton></TD>
												</TR>
											</TABLE>
											<TABLE id="Table5" border="0" cellSpacing="0" cellPadding="0" width="100%">
												<TR>
													<TD style="HEIGHT: 19px" class="tbl-HorizHeader" colSpan="2"></TD>
												</TR>
												<TR>
													<TD style="HEIGHT: 19px" class="tbl-HorizHeader" colSpan="2"></TD>
												</TR>
												<TR>
													<TD style="HEIGHT: 19px" class="tbl-HorizHeader" colSpan="2">Consumo de Mantillas</TD>
												</TR>
												<TR>
													<TD>
														<asp:Panel id="plMantillas" runat="server">
															<asp:DataGrid id="dgMantillas" runat="server" AutoGenerateColumns="False">
																<FooterStyle CssClass="tbl-DataGridFooter"></FooterStyle>
																<AlternatingItemStyle CssClass="tbl-DataGridItemAlternating"></AlternatingItemStyle>
																<ItemStyle CssClass="tbl-DataGridItem"></ItemStyle>
																<HeaderStyle CssClass="tbl-DataGridHeader"></HeaderStyle>
																<Columns>
																	<asp:BoundColumn Visible="False" DataField="cod_cliente" HeaderText="cod_cliente"></asp:BoundColumn>
																	<asp:BoundColumn DataField="des_mantilla" HeaderText="Tipo de mantilla"></asp:BoundColumn>
																	<asp:BoundColumn DataField="consumo" HeaderText="Consumo"></asp:BoundColumn>
																	<asp:EditCommandColumn Visible="False" ButtonType="LinkButton" UpdateText="Update" CancelText="Cancel"
																		EditText="Editar"></asp:EditCommandColumn>
																	<asp:ButtonColumn Text="Borrar" CommandName="Delete"></asp:ButtonColumn>
																</Columns>
															</asp:DataGrid>
														</asp:Panel></TD>
													<TD>
														<asp:Panel id="pldgMantillas" runat="server" Visible="False">
															<TABLE id="Table9" border="0" cellSpacing="0" cellPadding="0">
																<TR>
																	<TD style="HEIGHT: 20px" class="tbl-HorizHeader">TipoMantilla:</TD>
																	<TD style="HEIGHT: 20px">
																		<asp:TextBox id="tbMantilla" runat="server" CssClass="textBox" Width="80px"></asp:TextBox></TD>
																</TR>
																<TR>
																	<TD style="HEIGHT: 18px" class="tbl-HorizHeader">Consumo:</TD>
																	<TD style="HEIGHT: 18px">
																		<asp:TextBox id="tbConMantilla" runat="server" CssClass="textBox" Width="80px"></asp:TextBox></TD>
																</TR>
																<TR>
																	<TD colSpan="2">
																		<asp:LinkButton id="lkGrabar1" runat="server" ForeColor="Blue">Grabar</asp:LinkButton>&nbsp;
																		<asp:LinkButton id="lkCancelar1" runat="server" ForeColor="Blue">Cancelar</asp:LinkButton></TD>
																</TR>
															</TABLE>
														</asp:Panel></TD>
												</TR>
												<TR>
													<TD colSpan="2">
														<asp:LinkButton id="lkNuevamantilla" runat="server" ForeColor="Blue">Nueva Mantilla</asp:LinkButton></TD>
												</TR>
											</TABLE>
											<TABLE id="Table13" border="0" cellSpacing="0" cellPadding="0" width="100%">
												<TR>
													<TD class="tbl-HorizHeader" colSpan="2"></TD>
												</TR>
												<TR>
													<TD class="tbl-HorizHeader" colSpan="2"></TD>
												</TR>
												<TR>
													<TD class="tbl-HorizHeader" colSpan="2">Consumo Mensual de Tintas</TD>
												</TR>
												<TR>
													<TD>
														<asp:Panel id="plTintas" runat="server">
															<asp:DataGrid id="dgTintas" runat="server" AutoGenerateColumns="False">
																<FooterStyle CssClass="tbl-DataGridFooter"></FooterStyle>
																<AlternatingItemStyle CssClass="tbl-DataGridItemAlternating"></AlternatingItemStyle>
																<ItemStyle CssClass="tbl-DataGridItem"></ItemStyle>
																<HeaderStyle CssClass="tbl-DataGridHeader"></HeaderStyle>
																<Columns>
																	<asp:BoundColumn Visible="False" DataField="cod_cliente" HeaderText="cod_cliente"></asp:BoundColumn>
																	<asp:BoundColumn Visible="False" DataField="cod_tinta" HeaderText="cod_tinta"></asp:BoundColumn>
																	<asp:BoundColumn DataField="des_tinta" HeaderText="Tipo de Tinta"></asp:BoundColumn>
																	<asp:BoundColumn DataField="consumo" HeaderText="Consumo Mensual"></asp:BoundColumn>
																	<asp:EditCommandColumn Visible="False" ButtonType="LinkButton" UpdateText="Update" CancelText="Cancel"
																		EditText="Editar"></asp:EditCommandColumn>
																	<asp:ButtonColumn Text="Borrar" CommandName="Delete"></asp:ButtonColumn>
																</Columns>
															</asp:DataGrid>
														</asp:Panel></TD>
													<TD>
														<asp:Panel id="pldgTintas" runat="server" Visible="False">
															<TABLE id="Table10" border="0" cellSpacing="0" cellPadding="0">
																<TR>
																	<TD class="tbl-HorizHeader">Tipo de Tintas :</TD>
																	<TD>
																		<asp:DropDownList id="ddlTintas" runat="server" CssClass="textBox"></asp:DropDownList></TD>
																</TR>
																<TR>
																	<TD class="tbl-HorizHeader">Consumo Mensual :</TD>
																	<TD>
																		<asp:TextBox id="tbConTintas" runat="server" CssClass="textBox" Width="80px"></asp:TextBox></TD>
																</TR>
																<TR>
																	<TD style="HEIGHT: 20px" colSpan="2">
																		<asp:LinkButton id="lkGrabar2" runat="server">Grabar</asp:LinkButton>&nbsp;&nbsp;
																		<asp:LinkButton id="lkCancelar2" runat="server">Cancelar</asp:LinkButton></TD>
																</TR>
															</TABLE>
														</asp:Panel></TD>
												</TR>
												<TR>
													<TD colSpan="2">
														<asp:LinkButton id="lkNuevaTinta" runat="server" ForeColor="Blue">Nueva Tinta</asp:LinkButton></TD>
												</TR>
											</TABLE>
											<TABLE id="Table14" border="0" cellSpacing="0" cellPadding="0" width="100%">
												<TR>
													<TD class="tbl-HorizHeader" colSpan="2"></TD>
												</TR>
												<TR>
													<TD class="tbl-HorizHeader" colSpan="2"></TD>
												</TR>
												<TR>
													<TD class="tbl-HorizHeader" colSpan="2">Papeles</TD>
												</TR>
												<TR>
													<TD colSpan="2">
														<TABLE id="tblForm6" border="0" cellSpacing="0" cellPadding="0" width="100%" runat="server">
															<TR>
																<TD class="tbl-HorizHeader" colSpan="3" align="center"></TD>
															</TR>
															<TR>
																<TD class="tbl-HorizHeader" align="right">Tipo</TD>
																<TD style="WIDTH: 174px"></TD>
																<TD class="tbl-HorizHeader">Consumo</TD>
															</TR>
															<TR>
																<TD align="right">
																	<asp:DropDownList id="ddlpapel1" runat="server" CssClass="textBox"></asp:DropDownList></TD>
																<TD style="WIDTH: 174px"></TD>
																<TD>
																	<asp:TextBox id="TextBox7" runat="server" CssClass="textBox"></asp:TextBox></TD>
															</TR>
															<TR>
																<TD align="right">
																	<asp:DropDownList id="ddlpapel2" runat="server" CssClass="textBox"></asp:DropDownList></TD>
																<TD style="WIDTH: 174px"></TD>
																<TD>
																	<asp:TextBox id="TextBox8" runat="server" CssClass="textBox"></asp:TextBox></TD>
															</TR>
															<TR>
																<TD align="right">
																	<asp:DropDownList id="ddlpapel3" runat="server" CssClass="textBox"></asp:DropDownList></TD>
																<TD style="WIDTH: 174px"></TD>
																<TD>
																	<asp:TextBox id="TextBox9" runat="server" CssClass="textBox"></asp:TextBox></TD>
															</TR>
															<TR>
																<TD></TD>
																<TD style="WIDTH: 174px" align="center">
																	<asp:ImageButton id="btFinalizar" runat="server" ImageUrl="../images/finalizar.jpg"></asp:ImageButton></TD>
																<TD></TD>
															</TR>
														</TABLE>
													</TD>
												</TR>
												<TR>
													<TD></TD>
													<TD></TD>
												</TR>
											</TABLE>
										</TD>
									</TR>
								</TABLE>
							</TD>
						</TR>
					</TABLE>
				</P>
				<P>
					<TABLE id="tblForm2" border="0" cellSpacing="0" cellPadding="0" width="100%" runat="server">
					</TABLE>
				</P>
			</TD>
		</TR>
	</TABLE>
	<P>&nbsp;</P>
	<P>&nbsp;</P>
	<P>&nbsp;</P>
	<P>&nbsp;</P>
	<P>&nbsp;</P>
	<P>&nbsp;</P>
	<P>&nbsp;</P>
</wilson:masterpage>
