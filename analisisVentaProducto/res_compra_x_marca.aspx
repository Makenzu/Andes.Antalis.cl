<%@ Page Language="vb" AutoEventWireup="false" Codebehind="res_compra_x_marca.aspx.vb" Inherits="app.res_compra_x_marca"%>
<%@ Register TagPrefix="Wilson" Namespace="Wilson.MasterPages" Assembly="WilsonMasterPages" %>
<LINK href="css/calendar.css" type="text/css" rel="stylesheet">
<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
<wilson:masterpage id="MPContainer" runat="server">
	<WILSON:CONTENTREGION id="MPTitle" runat="server">Resumen Compra 
por Marca</WILSON:CONTENTREGION>
	<WILSON:CONTENTREGION id="MPCaption" runat="server">Resumen Compra por Marca<BR>
<asp:Label id="lbFecha" runat="server"></asp:Label></WILSON:CONTENTREGION>
	<SCRIPT language="javascript" src="/js/CalendarPopup.js"></SCRIPT>
	<TABLE id="Table3" border="0" cellSpacing="0" cellPadding="0" width="95%" align="center">
		<TR>
			<TD width="70%">
				<asp:Label id="lbErrors" runat="server" CssClass="txt-FatalMessage" EnableViewState="False"></asp:Label></TD>
			<TD width="30%" align="right">
				<asp:ImageButton id="ibExportar" title="Exportar a Excel" runat="server" EnableViewState="False"
					ToolTip="Exportar a Excel" Visible="False" ImageUrl="/images/exportar.gif"></asp:ImageButton><IMG onclick="javascript:imprimir();" border="0" alt="Imprimir" src="/images/imprimir.gif"><A href="javascript: imprimir();"></A></TD>
		</TR>
	</TABLE>
	<TABLE id="Table2" border="0" cellSpacing="0" cellPadding="1" width="100%">
		<TR>
			<TD align="center">
				<asp:Panel id="plParams" runat="server" EnableViewState="False" DESIGNTIMEDRAGDROP="489">
					<TABLE id="tbParams" border="0" cellSpacing="0" cellPadding="2" width="370" align="center">
						<TR>
							<TD class="tbl-HorizHeader" width="115">Fecha Inicio:</TD>
							<TD width="120">
								<TABLE id="Table4" border="0" cellSpacing="0" cellPadding="0">
									<TR>
										<TD>
											<asp:TextBox id="tbxFecIni" runat="server" CssClass="textBox" Width="100px"></asp:TextBox></TD>
										<TD align="right">&nbsp;&nbsp;
										</TD>
									</TR>
								</TABLE>
							</TD>
							<TD width="80"></TD>
						</TR>
						<TR>
							<TD class="tbl-HorizHeader" width="115">Fecha Término:</TD>
							<TD width="120">
								<TABLE id="Table1" border="0" cellSpacing="0" cellPadding="0">
									<TR>
										<TD>
											<asp:TextBox id="tbxFecTer" runat="server" CssClass="textBox" Width="100px"></asp:TextBox></TD>
										<TD align="right">&nbsp;&nbsp;
										</TD>
									</TR>
								</TABLE>
							</TD>
							<TD width="80">
								<asp:ImageButton id="btSend" runat="server" ImageUrl="/images/procesar.gif"></asp:ImageButton>
								<DIV style="DISPLAY: none" id="disbtn" name="disbtn"><IMG alt="" src="/images/procesando.gif"></DIV>
							</TD>
						</TR>
						<TR>
							<TD class="tbl-HorizHeader" width="115">Marca :</TD>
							<TD width="120">
								<asp:DropDownList id="ddlmarca" runat="server"></asp:DropDownList></TD>
							<TD width="80"></TD>
						</TR>
					</TABLE>
					<BR>
				</asp:Panel>
				<asp:RequiredFieldValidator id="dsfsd" runat="server" CssClass="txt-AlertMessage" ErrorMessage="* Debe ingresar una fecha de termino.<br>"
					ControlToValidate="tbxFecTer" Display="Dynamic"></asp:RequiredFieldValidator>
				<asp:RequiredFieldValidator id="rfvFecIni" runat="server" CssClass="txt-AlertMessage" ErrorMessage="* Debe ingresar una fecha de inicio.<br>"
					ControlToValidate="tbxFecIni" Display="Dynamic"></asp:RequiredFieldValidator>
				<asp:CompareValidator id="vFechas" runat="server" CssClass="txt-AlertMessage" Visible="False" ErrorMessage="* Fecha Inicio debe ser mayor a Fecha Termino<BR>"
					ControlToValidate="tbxFecTer" Display="Dynamic" EnableClientScript="False" Type="Date" Operator="LessThan"
					ControlToCompare="tbxFecIni"></asp:CompareValidator></TD>
		</TR>
		<TR>
			<TD align="center">
				<asp:Label id="lbNota" runat="server" CssClass="txt-SoftMessage" Visible="False" Width="300px"></asp:Label></TD>
		</TR>
		<TR>
			<TD align="center">
				<asp:DataGrid id="dgResultado" runat="server" CellPadding="3" AutoGenerateColumns="False" AllowSorting="True"
					ShowFooter="True">
					<AlternatingItemStyle CssClass="tbl-DataGridItemAlternating"></AlternatingItemStyle>
					<ItemStyle CssClass="tbl-DataGridItem"></ItemStyle>
					<HeaderStyle CssClass="tbl-DataGridHeader"></HeaderStyle>
					<FooterStyle Font-Bold="True" HorizontalAlign="Right" CssClass="tbl-DataGridFooter"></FooterStyle>
					<Columns>
						<asp:BoundColumn DataField="des_familia" SortExpression="des_familia DESC" ReadOnly="True" HeaderText="Fam">
							<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
							<ItemStyle HorizontalAlign="Left"></ItemStyle>
							<FooterStyle HorizontalAlign="Left"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="des_subfamilia" SortExpression="des_subfamilia DESC" ReadOnly="True"
							HeaderText="SubFam">
							<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
							<ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
							<FooterStyle HorizontalAlign="Left"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="cod_producto" SortExpression="cod_producto DESC" ReadOnly="True" HeaderText="Cod. Prod.">
							<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
							<FooterStyle HorizontalAlign="Center"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="des_producto" SortExpression="des_producto DESC" ReadOnly="True" HeaderText="Descripcion">
							<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
							<ItemStyle HorizontalAlign="Left"></ItemStyle>
							<FooterStyle HorizontalAlign="Center"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="cod_marca" SortExpression="cod_marca DESC" ReadOnly="True" HeaderText="Cod. Marca">
							<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
							<FooterStyle HorizontalAlign="Center"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="val_cantidad_unidad_compra" SortExpression="val_cantidad_unidad_compra DESC"
							ReadOnly="True" HeaderText="Cant.Unid. Compra" DataFormatString="{0:#,##0.00}">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="val_cantidad_unidad_venta" SortExpression="val_cantidad_unidad_venta DESC"
							ReadOnly="True" HeaderText="Cant. Unid. Vta." DataFormatString="{0:#,##0.00}">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="val_precio_unidad_compra_dolar" SortExpression="val_precio_unidad_compra_dolar DESC"
							ReadOnly="True" HeaderText="Precio Unid. Compra" DataFormatString="{0:#,##0.00}">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="val_precio_unidad_venta_dolar" SortExpression="val_precio_unidad_venta_dolar DESC"
							HeaderText="Precio Unid. Vta." DataFormatString="{0:#,##0.00}">
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
					</Columns>
				</asp:DataGrid></TD>
		</TR>
	</TABLE>
	<DIV style="POSITION: absolute; BACKGROUND-COLOR: white; VISIBILITY: hidden; layer-background-color: white"
		id="caldiv1"></DIV>
	<DIV style="POSITION: absolute; BACKGROUND-COLOR: white; VISIBILITY: hidden; layer-background-color: white"
		id="caldiv2"></DIV>
</wilson:masterpage>
