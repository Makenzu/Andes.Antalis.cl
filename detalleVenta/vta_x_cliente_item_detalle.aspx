<%@ Page Language="vb" AutoEventWireup="false" Codebehind="vta_x_cliente_item_detalle.aspx.vb" Inherits="app.vta_x_cliente_item_detalle"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Detalle</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/css/andes.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body topmargin="0" leftmargin="0" bottommargin="0" rightmargin="0">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="2" width="98%" border="0">
				<TR>
					<TD><asp:label id="lbErrors" runat="server" Width="90%" CssClass="txt-FatalMessage" Visible="False"></asp:label></TD>
				</TR>
				<TR>
					<TD align="center" bgColor="whitesmoke">
						<asp:Label id="lbTitle" runat="server" CssClass="PageTitle"></asp:Label><BR>
						<asp:Label id="lbFecha" runat="server"></asp:Label></TD>
				</TR>
				<TR>
					<TD align="center">
						<TABLE id="Table2" cellSpacing="0" cellPadding="4" border="1">
							<TR>
								<TD class="tbl-HorizHeader">Producto:</TD>
								<TD>
									<DIV ms_positioning="FlowLayout">
										<asp:Label id="lbProducto" runat="server" CssClass="tbl-HorizItem"></asp:Label></DIV>
								</TD>
							</TR>
							<TR>
								<TD class="tbl-HorizHeader">Descripción:</TD>
								<TD>
									<asp:Label id="lbDescripcion" runat="server" CssClass="tbl-HorizItem"></asp:Label>
								</TD>
							</TR>
							<TR>
								<TD align="center" colSpan="2">
									<asp:datagrid id="dgDetalle" runat="server" AutoGenerateColumns="False" AllowSorting="True" ToolTip="Ventas Ultimos 12 Meses"
										CellPadding="3">
										<FooterStyle CssClass="tbl-DataGridFooter"></FooterStyle>
										<AlternatingItemStyle CssClass="tbl-DataGridItemAlternating"></AlternatingItemStyle>
										<ItemStyle CssClass="tbl-DataGridItem"></ItemStyle>
										<HeaderStyle CssClass="tbl-DataGridHeader"></HeaderStyle>
										<Columns>
											<asp:BoundColumn DataField="mes" ReadOnly="True" HeaderText="Mes"></asp:BoundColumn>
											<asp:BoundColumn DataField="can_vta" ReadOnly="True" HeaderText="Q. Vta.">
												<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="val_vta" ReadOnly="True" HeaderText="$ Vta.">
												<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
											</asp:BoundColumn>
										</Columns>
									</asp:datagrid></TD>
							</TR>
							<TR>
								<TD align="center" colSpan="2">
									<TABLE id="Table3" cellSpacing="0" cellPadding="3" border="1">
										<TR>
											<TD style="WIDTH: 80px"></TD>
											<TD align="right" class="tbl-HorizHeader">Q. Vta</TD>
											<TD align="right" class="tbl-HorizHeader">$ Vta.</TD>
										</TR>
										<TR>
											<TD style="WIDTH: 80px" class="tbl-HorizHeader">Mes Actual:</TD>
											<TD align="right">
												<asp:Label id="lbCanMesAct" runat="server" CssClass="tbl-HorizItem"></asp:Label></TD>
											<TD align="right">
												<asp:Label id="lbValMesAct" runat="server" CssClass="tbl-HorizItem"></asp:Label></TD>
										</TR>
										<TR>
											<TD style="WIDTH: 80px" class="tbl-HorizHeader">Año Actual:</TD>
											<TD align="right">
												<asp:Label id="lbCanAnoAct" runat="server" CssClass="tbl-HorizItem"></asp:Label></TD>
											<TD align="right">
												<asp:Label id="lbValAnoAct" runat="server" CssClass="tbl-HorizItem"></asp:Label></TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
