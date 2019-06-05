<%@ Register TagPrefix="Wilson" Namespace="Wilson.MasterPages" Assembly="WilsonMasterPages" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="vta_detalle_control_factura.aspx.vb" Inherits="app.vta_detalle_control_factura"%>
<META content="text/html; charset=iso-8859-1" http-equiv="Content-Type">
<LINK href="css/calendar.css" type="text/css" rel="stylesheet">
<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
<wilson:masterpage id="MPContainer" runat="server">
	<WILSON:CONTENTREGION id="MPTitle" runat="server">ANDES</WILSON:CONTENTREGION>
	<WILSON:CONTENTREGION id="MPCaption" runat="server">Detalle Control Facturación ABCD<BR>
<asp:Label id="lbFecha" runat="server"></asp:Label></WILSON:CONTENTREGION>
	<SCRIPT language="javascript" src="/js/CalendarPopup.js"></SCRIPT>
	<SCRIPT language="javascript">
			var cal1 = new CalendarPopup(('caldiv1'));		
	</SCRIPT>
	<asp:Label id="lbErrors" runat="server" CssClass="txt-FatalMessage"></asp:Label>
	<TABLE id="Table1" border="0" cellSpacing="0" cellPadding="3">
		<TR>
			<TD>
				<TABLE id="Table2" border="0" cellSpacing="0" cellPadding="1" width="100%">
					<TR>
						<TD vAlign="bottom" width="1118" align="right">
							<asp:ImageButton id="ibExportar" title="Exportar a Excel" runat="server" ToolTip="Exportar a Excel"
								ImageUrl="/images/exportar.gif" EnableViewState="False" Visible="False"></asp:ImageButton><INPUT title="Imprimir" onclick="javascript:imprimir();return false;" alt="Imprimir" src="/images/imprimir.gif"
								type="image"></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 1118px" align="left">
							<asp:Panel id="plParams" runat="server" EnableViewState="False" DESIGNTIMEDRAGDROP="489" HorizontalAlign="Right">
								<TABLE id="tbParams" border="0" cellSpacing="0" cellPadding="2" width="300" align="center">
									<TR>
										<TD class="tbl-HorizHeader" width="74">Centro:</TD>
										<TD>
											<asp:DropDownList id="ddlCentro" runat="server" CssClass="listBox"></asp:DropDownList></TD>
										<TD></TD>
									</TR>
									<TR>
										<TD class="tbl-HorizHeader" width="74">Fecha:</TD>
										<TD>
											<TABLE id="Table4" border="0" cellSpacing="0" cellPadding="0">
												<TR>
													<TD>
														<asp:TextBox id="txFecha" runat="server" CssClass="textBox" ReadOnly="True" Width="100px"></asp:TextBox></TD>
													<TD align="right">&nbsp; <IMG id="imgDesde" onclick="cal1.select(document.forms[0].txFecha,'imgDesde','dd / MM / yyyy'); return false;"
															name="imgDesde" alt="Elejir fecha" src="/images/calendar.gif">
													</TD>
												</TR>
											</TABLE>
										</TD>
										<TD></TD>
									</TR>
									<TR>
										<TD class="tbl-HorizHeader" width="74">Moneda:</TD>
										<TD>
											<asp:DropDownList id="ddlMoneda" runat="server" CssClass="listBox">
												<asp:ListItem Value="DOL" Selected="True">Dolares</asp:ListItem>
												<asp:ListItem Value="PES">Pesos</asp:ListItem>
											</asp:DropDownList></TD>
										<TD>
											<DIV id="calPos" name="calPos">
												<asp:ImageButton id="btSend" runat="server" ImageUrl="/images/procesar.gif"></asp:ImageButton>
												<DIV style="DISPLAY: none" id="disbtn" name="disbtn"><IMG alt="" src="/images/procesando.gif"></DIV>
											</DIV>
										</TD>
									</TR>
								</TABLE>
								<BR>
							</asp:Panel>
							<asp:RequiredFieldValidator id="rfvPromo" runat="server" CssClass="txt-AlertMessage" Width="300px" Display="Dynamic"
								ControlToValidate="txFecha" ErrorMessage="* Debe ingresar una fecha."></asp:RequiredFieldValidator></TD>
						<TD align="right"></TD>
					</TR>
				</TABLE>
				<asp:Label id="lbNota" runat="server" CssClass="txt-SoftMessage" Visible="False"></asp:Label></TD>
		</TR>
		<TR>
			<TD align="center">
				<asp:DataGrid id="dgResultado" runat="server" ShowFooter="True" CellPadding="3" AutoGenerateColumns="False"
					AllowSorting="True">
					<FooterStyle Font-Bold="True" HorizontalAlign="Right" CssClass="tbl-DataGridFooter"></FooterStyle>
					<AlternatingItemStyle CssClass="tbl-DataGridItemAlternating"></AlternatingItemStyle>
					<ItemStyle CssClass="tbl-DataGridItem"></ItemStyle>
					<HeaderStyle CssClass="tbl-DataGridHeader"></HeaderStyle>
					<Columns>
						<asp:BoundColumn DataField="cod_filial" HeaderText="Fil"></asp:BoundColumn>
						<asp:BoundColumn DataField="cod_sucursal" HeaderText="Suc"></asp:BoundColumn>
						<asp:BoundColumn DataField="COD_DOCUMENTO" ReadOnly="True" HeaderText="ID."></asp:BoundColumn>
						<asp:BoundColumn DataField="NUM_DOCUMENTO" ReadOnly="True" HeaderText="N&#176;. Doc.">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="A$" SortExpression="A$ DESC" ReadOnly="True" HeaderText="A$">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="B$" SortExpression="B$ DESC" ReadOnly="True" HeaderText="B$">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="C$" SortExpression="C$ DESC" ReadOnly="True" HeaderText="C$">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="D$" SortExpression="D$ DESC" ReadOnly="True" HeaderText="D$">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:TemplateColumn HeaderText="D$/B$*100">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<ItemTemplate>
								<asp:Label runat="server" Text='0'></asp:Label>
							</ItemTemplate>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:TemplateColumn>
						<asp:BoundColumn DataField="ITEMS" ReadOnly="True" HeaderText="Itemes">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:TemplateColumn SortExpression="cod_cliente desc" HeaderText="&lt;--- C l i e n t e ---&gt;">
							<ItemTemplate>
								<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COD_CLIENTE") & " -  " & DataBinder.Eval(Container, "DataItem.NOM_CLIENTE") %>'>
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:BoundColumn DataField="COD_PROMOTORA" ReadOnly="True" HeaderText="Pr."></asp:BoundColumn>
						<asp:BoundColumn DataField="COD_VENDEDORA" ReadOnly="True" HeaderText="Ven."></asp:BoundColumn>
					</Columns>
				</asp:DataGrid></TD>
		</TR>
		<TR>
			<TD><INPUT id="hdCodSucursal" type="hidden" name="hdCodSucursal" runat="server"><INPUT id="hdCodFilial" type="hidden" name="hdCodFilial" runat="server"></TD>
		</TR>
	</TABLE> <!-- CALENDARIO -->
	<DIV style="POSITION: absolute; BACKGROUND-COLOR: white; VISIBILITY: hidden; layer-background-color: white"
		id="caldiv1"></DIV>
</wilson:masterpage>
