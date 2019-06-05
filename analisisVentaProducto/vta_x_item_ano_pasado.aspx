<%@ Page Language="vb" AutoEventWireup="false" Codebehind="vta_x_item_ano_pasado.aspx.vb" Inherits="app.vta_x_item_pasado"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Detalle</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="css/andes.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="2" width="98%" border="0">
				<TR>
					<TD><asp:label id="lbErrors" runat="server" Visible="False" CssClass="txt-FatalMessage" Width="90%"></asp:label></TD>
				</TR>
				<TR>
					<TD align="center" bgColor="whitesmoke"><asp:label id="lbTitle" runat="server" CssClass="PageTitle">VENTAS AÑO</asp:label><asp:label id="lbFecha" runat="server" CssClass="PageTitle"></asp:label></TD>
				</TR>
				<TR>
					<TD align="center">
						<TABLE id="Table2" cellSpacing="0" cellPadding="4" border="1">
							<TR>
								<TD class="tbl-HorizHeader">Producto:</TD>
								<TD>
									<DIV ms_positioning="FlowLayout"><asp:label id="lbProducto" runat="server" CssClass="tbl-HorizItem"></asp:label></DIV>
								</TD>
							</TR>
							<TR>
								<TD class="tbl-HorizHeader">Descripción:</TD>
								<TD><asp:label id="lbDescripcion" runat="server" CssClass="tbl-HorizItem"></asp:label></TD>
							</TR>
							<TR>
								<TD align="center" colSpan="2"><asp:table id="tbResultado" runat="server" CellPadding="2" BorderWidth="1px"></asp:table></TD>
							</TR>
						</TABLE>
						<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="90%" border="0">
							<TR>
								<TD><IMG alt="Volver" src="images/arrow_blue.gif" align="top">&nbsp;<A href="javascript:history.back();">Volver</A></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
