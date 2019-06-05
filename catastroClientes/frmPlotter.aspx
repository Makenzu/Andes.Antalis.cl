<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frmPlotter.aspx.vb" Inherits="app.frmPlotter" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Registro de Máquina de Cliente</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href="../css/andes.css">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<P>
				<asp:Literal id="Literal1" runat="server"></asp:Literal>
				<TABLE id="Table1" border="1" cellSpacing="1" cellPadding="1">
					<TR>
						<TD class="tbl-HorizHeader">Cliente:</TD>
						<TD class="tbl-HorizItem"><asp:label id="lCliente" runat="server"></asp:label></TD>
					</TR>
					<TR>
						<TD class="tbl-HorizHeader">Plotter:</TD>
						<TD><asp:dropdownlist id="ddlPlotter" runat="server" CssClass="listBox"></asp:dropdownlist></TD>
					</TR>
					<TR>
						<TD style="Z-INDEX: 0" class="tbl-HorizHeader">Cantidad:</TD>
						<TD><asp:textbox style="Z-INDEX: 0" id="tbNumeroMaquinas" runat="server" CssClass="textBox" Width="37px"
								MaxLength="3"></asp:textbox></TD>
					</TR>
					<TR>
						<TD class="tbl-HorizHeader">Tipo Tinta:</TD>
						<TD><asp:dropdownlist style="Z-INDEX: 0" id="ddlTipoTinta" runat="server" CssClass="listBox"></asp:dropdownlist></TD>
					</TR>
					<TR>
						<TD colSpan="2" align="center"><asp:button id="bGrabaMaquina" runat="server" Text="grabar"></asp:button>&nbsp;
							<asp:button style="Z-INDEX: 0" id="bCancelar" runat="server" Text="cerrar ventana"></asp:button><INPUT id="hfAccion" type="hidden" name="hfAccion" runat="server"></TD>
					</TR>
				</TABLE>
				<INPUT style="Z-INDEX: 0" id="hfIdRegistro" type="hidden" name="hfIdRegistro" runat="server">
			</P>
			<asp:Label id="lMensajeError" runat="server"></asp:Label></form>
	</body>
</HTML>
