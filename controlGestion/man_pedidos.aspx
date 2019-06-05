<%@ Register TagPrefix="Wilson" Namespace="Wilson.MasterPages" Assembly="WilsonMasterPages" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="man_pedidos.aspx.vb" Inherits="app.res_pedido_couche"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<LINK href="css/calendar.css" type="text/css" rel="stylesheet">
<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
<wilson:masterpage id="MPContainer" runat="server">
<WILSON:CONTENTREGION id="MPTitle" runat="server">Mantenedor de 
Pedidos</WILSON:CONTENTREGION> 
<WILSON:CONTENTREGION id="MPCaption" runat="server">Mantenedor de Pedidos<BR>
<asp:Label id="lbFecha" runat="server"></asp:Label></WILSON:CONTENTREGION>
<SCRIPT language="javascript" src="/js/CalendarPopup.js"></SCRIPT>

<TABLE id="Table3" border="0" cellSpacing="0" cellPadding="0" width="95%" align="center">
		<TR>
			<TD width="70%">
				<asp:Label id="lbErrors" runat="server" EnableViewState="False" CssClass="txt-FatalMessage"></asp:Label></TD>
			<TD width="30%" align="right">
				<asp:ImageButton id="ibExportar" title="Exportar a Excel" runat="server" EnableViewState="False"
					ImageUrl="/images/exportar.gif" Visible="False" ToolTip="Exportar a Excel"></asp:ImageButton><IMG onclick="javascript:imprimir();" border="0" alt="Imprimir" src="/images/imprimir.gif"><A href="javascript: imprimir();"></A></TD>
		</TR>
	</TABLE>
<TABLE id="Table2" border="0" cellSpacing="0" cellPadding="1" width="100%">
		<TR>
			<TD align="center">
				<asp:Panel id="plParams" runat="server" EnableViewState="False">
					<TABLE id="tbParams" border="0" cellSpacing="0" cellPadding="2" width="370" align="center">
						<TBODY>
							<TR>
								<TD class="tbl-HorizHeader" width="115">Reporte:</TD>
								<TD style="WIDTH: 57px" width="57">
									<TABLE id="Table4" border="0" cellSpacing="0" cellPadding="0">
										<TR>
											<TD>
												<asp:DropDownList id="ddlTipoReporte" runat="server">
													<asp:ListItem Value="COUCHE">Couche</asp:ListItem>
												</asp:DropDownList></TD>
											<TD align="right">&nbsp;&nbsp;
											</TD>
										</TR>
									</TABLE>
								</TD>
								<TD width="80">
									<asp:ImageButton id="btSend" runat="server" ImageUrl="/images/procesar.gif"></asp:ImageButton>
									<DIV style="DISPLAY: none" id="disbtn" name="disbtn"><IMG alt="" src="/images/procesando.gif"></DIV>
								</TD></TD>
		</TR>
		<TR>
			<TD class="tbl-HorizHeader" width="115"></TD>
			<TD style="WIDTH: 57px" width="57">
				<asp:Button id="btnNuevo" runat="server" Text="Nuevo"></asp:Button></TD>
			<TD width="80"></TD>
		</TR>
	</TABLE><BR>
<asp:Panel id="pnlNuevo" runat="server" Visible="False">
		<TABLE id="Table1" border="1" cellSpacing="1" cellPadding="1" width="300">
			<TR>
				<TD style="WIDTH: 117px" class="tbl-HorizHeader">Código Producto</TD>
				<TD>
					<asp:TextBox id="txtCodProducto" runat="server"></asp:TextBox></TD>
			</TR>
			<TR>
				<TD style="WIDTH: 117px" class="tbl-HorizHeader">Gramaje</TD>
				<TD>
					<asp:TextBox id="txtValGramos" runat="server"></asp:TextBox></TD>
			</TR>
			<TR>
				<TD style="WIDTH: 117px" class="tbl-HorizHeader">Ancho</TD>
				<TD>
					<asp:TextBox id="txtValAncho" runat="server"></asp:TextBox></TD>
			</TR>
			<TR>
				<TD style="WIDTH: 117px" class="tbl-HorizHeader">Largo</TD>
				<TD>
					<asp:TextBox id="txtValLargo" runat="server"></asp:TextBox></TD>
			</TR>
			<TR>
				<TD style="WIDTH: 117px" class="tbl-HorizHeader">Color</TD>
				<TD>
					<asp:DropDownList id="ddlCodColor" runat="server">
						<asp:ListItem Value="B">Brillante</asp:ListItem>
						<asp:ListItem Value="M">Mate</asp:ListItem>
					</asp:DropDownList></TD>
			</TR>
		</TABLE>
		<BR>
		<asp:Button id="btnGrabar" runat="server" Text="Grabar"></asp:Button>
	</asp:Panel></asp:Panel></TD></TR>
  <TR>
		<TD align="center">
			<asp:Label id="lbNota" runat="server" CssClass="txt-SoftMessage" Visible="False" Width="300px"></asp:Label></TD>
	</TR>
  <TR>
		<TD align="center">
			<asp:DataGrid id="dgResultado" runat="server" AllowSorting="True" AutoGenerateColumns="False"
				CellPadding="3">
				<FooterStyle Font-Bold="True" HorizontalAlign="Right" CssClass="tbl-DataGridFooter"></FooterStyle>
				<AlternatingItemStyle CssClass="tbl-DataGridItemAlternating"></AlternatingItemStyle>
				<ItemStyle CssClass="tbl-DataGridItem"></ItemStyle>
				<HeaderStyle CssClass="tbl-DataGridHeader"></HeaderStyle>
				<Columns>
					<asp:BoundColumn DataField="cod_grupo" SortExpression="cod_grupo" ReadOnly="True" HeaderText="Cod Grupo">
						<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<FooterStyle HorizontalAlign="Center"></FooterStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="des_grupo" ReadOnly="True" HeaderText="Desc Grupo">
						<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
						<ItemStyle HorizontalAlign="Left"></ItemStyle>
						<FooterStyle HorizontalAlign="Center"></FooterStyle>
					</asp:BoundColumn>
					<asp:TemplateColumn SortExpression="cod_producto" HeaderText="Cod Prod">
						<HeaderStyle HorizontalAlign="Left" Width="55px"></HeaderStyle>
						<ItemStyle HorizontalAlign="Left"></ItemStyle>
						<ItemTemplate>
							<%# DataBinder.Eval(Container.DataItem, "cod_producto") %>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox id=txtCodProductoEdit Font-Names="Arial, Helvetica, sans-serif" Font-Size="9px" Text='<%# DataBinder.Eval(Container.DataItem, "cod_producto") %>' Width="55px" Runat="server">
							</asp:TextBox>
						</EditItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn DataField="des_producto" ReadOnly="True" HeaderText="Producto">
						<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
						<ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
						<FooterStyle HorizontalAlign="Left"></FooterStyle>
					</asp:BoundColumn>
					<asp:TemplateColumn HeaderText="Gramos">
						<HeaderStyle HorizontalAlign="Right" Width="25px"></HeaderStyle>
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
						<ItemTemplate>
							<%# DataBinder.Eval(Container.DataItem, "val_gramos") %>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox ID="txtValGramosEdit" Width="35px" Font-Names="Arial, Helvetica, sans-serif" Font-Size="9px" Text='<%# DataBinder.Eval(Container.DataItem, "val_gramos", "{0:#,##0.00}") %>' Runat="server">
							</asp:TextBox>
						</EditItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Ancho">
						<HeaderStyle HorizontalAlign="Right" Width="25px"></HeaderStyle>
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
						<ItemTemplate>
							<%# DataBinder.Eval(Container.DataItem, "val_ancho") %>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox ID="txtValAnchoEdit" Width="30px" Font-Names="Arial, Helvetica, sans-serif" Font-Size="9px" Text='<%# DataBinder.Eval(Container.DataItem, "val_ancho", "{0:#,##0.00}") %>' Runat="server">
							</asp:TextBox>
						</EditItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Largo">
						<HeaderStyle HorizontalAlign="Right" Width="25px"></HeaderStyle>
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
						<ItemTemplate>
							<%# DataBinder.Eval(Container.DataItem, "val_largo") %>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox ID="txtValLargoEdit" Width="30px" Font-Names="Arial, Helvetica, sans-serif" Font-Size="9px" Text='<%# DataBinder.Eval(Container.DataItem, "val_largo", "{0:#,##0.00}") %>' Runat="server">
							</asp:TextBox>
						</EditItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Color">
						<HeaderStyle HorizontalAlign="Left" Width="25px"></HeaderStyle>
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
						<ItemTemplate>
							<%# DataBinder.Eval(Container.DataItem, "cod_color") %>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox ID="txtCodColorEdit" Width="15px" Font-Names="Arial, Helvetica, sans-serif" Font-Size="9px" Text='<%# DataBinder.Eval(Container.DataItem, "cod_color", "{0:#,##0.00}") %>' Runat="server">
							</asp:TextBox>
						</EditItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn DataField="val_pags_por_ton_propio" ReadOnly="True" HeaderText="Hojas/Ton">
						<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
						<ItemStyle HorizontalAlign="Left"></ItemStyle>
						<FooterStyle HorizontalAlign="Center"></FooterStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="val_pags_por_ton_sap" ReadOnly="True" HeaderText="Hojas/Ton SAP">
						<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
						<ItemStyle HorizontalAlign="Left"></ItemStyle>
						<FooterStyle HorizontalAlign="Center"></FooterStyle>
					</asp:BoundColumn>
					<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="Actualizar" CancelText="Cancelar" EditText="Editar"></asp:EditCommandColumn>
				</Columns>
			</asp:DataGrid></TD>
	</TR></TBODY></TABLE>
<DIV style="POSITION: absolute; BACKGROUND-COLOR: white; VISIBILITY: hidden; layer-background-color: white"
		id="caldiv1"></DIV>
<DIV style="POSITION: absolute; BACKGROUND-COLOR: white; VISIBILITY: hidden; layer-background-color: white"
		id="caldiv2"></DIV></wilson:masterpage>
