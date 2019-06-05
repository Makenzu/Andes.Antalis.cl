<%@ Page Language="vb" AutoEventWireup="false" Codebehind="vta_x_item_mes_detalle.aspx.vb" Inherits="app.vta_x_item_detalle"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Detalle</title>
		<META http-equiv="Content-Type" content="text/html; charset=windows-1252">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="css/andes.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		function showDatos(cp,an){
			var mywin
			var param
			var winl = (screen.width - 400) / 2;
			var wint = (screen.height - 320) / 2; 
			param = "width=400,height=320,Top="+wint+",Left="+winl+",toolbar=0,scrollbars=1,location=0,statusbar=0,menubar=0,resizable=1";
			 window.open("vta_x_item_ano_pasado.aspx?cp="+cp+"&an="+an,"detalle",param );
		}
		</script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="2" width="98%" border="0">
				<TR>
					<TD><asp:label id="lbErrors" runat="server" Width="90%" CssClass="txt-FatalMessage" Visible="False"></asp:label></TD>
				</TR>
				<TR>
					<TD class="PageTitle" align="center" bgColor="whitesmoke">Vta. ITEM Detalle<BR>
						<asp:label id="lbFecha" runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD align="center">
						<TABLE id="Table2" cellSpacing="0" cellPadding="4" border="1">
							<TR>
								<TD class="tbl-HorizHeader">Producto:</TD>
								<TD>
									<DIV noWrap ms_positioning="FlowLayout"><asp:label id="lbProducto" runat="server" CssClass="tbl-HorizItem"></asp:label></DIV>
								</TD>
							</TR>
							<TR>
								<TD class="tbl-HorizHeader">Descripción:</TD>
								<TD noWrap><asp:label id="lbDescripcion" runat="server" CssClass="tbl-HorizItem"></asp:label></TD>
							</TR>
							<TR>
								<TD align="center" colSpan="2">
									<TABLE id="Table4" cellSpacing="0" cellPadding="0" border="0" align="center">
										<TR>
											<TD align="left">
												<asp:table id="tbResultado" runat="server" GridLines="Both" BorderWidth="1px"></asp:table></TD>
										</TR>
										<TR>
											<TD align="left"><BR>
												<TABLE id="Table3" cellSpacing="0" cellPadding="3" border="1">
													<TR>
														<TD class="tbl-HorizHeader" noWrap>Stock&nbsp;Actual:</TD>
														<TD noWrap align="right">
															<asp:label id="lbStock" runat="server" CssClass="tbl-HorizItem"></asp:label></TD>
													</TR>
													<TR>
														<TD class="tbl-HorizHeader" noWrap>Pedidos Pendientes:</TD>
														<TD noWrap align="right" width="50">
															<asp:label id="lbPend" runat="server" CssClass="tbl-HorizItem"></asp:label></TD>
													</TR>
												</TABLE>
											</TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
						</TABLE>
						<TABLE id="Table5" cellSpacing="0" cellPadding="0" width="90%" border="0">
							<TR>
								<TD align="right"><A 
            href='javascript:showDatos("<% response.write(request("cp"))%>",<% response.write(request("an"))%>);'>ver 
										ventas del año pasado</A><A href="javascript:history.back();"></A></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
