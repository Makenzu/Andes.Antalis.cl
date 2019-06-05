<%@ Page Language="vb" AutoEventWireup="false" Codebehind="get_password.aspx.vb" Inherits="app.get_password"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Detalle</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../css/andes.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="2" width="98%" border="0">
				<TR>
					<TD><asp:label id="lbErrors" runat="server" Visible="False" CssClass="txt-FatalMessage" Width="90%"></asp:label></TD>
				</TR>
				<TR>
					<TD align="center" bgColor="whitesmoke"><asp:label id="lbTitle" runat="server" CssClass="PageTitle">Recuperar Contraseña</asp:label><BR>
						<asp:label id="lbFecha" runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD align="center">
						<TABLE id="Table2" cellSpacing="0" cellPadding="4" width="400" border="1">
							<TR>
								<TD class="tbl-HorizHeader" colSpan="2"></TD>
							</TR>
							<TR>
								<TD class="STD-TEXT" style="TEXT-ALIGN: justify" align="center">Ingrese&nbsp;el 
									nombre de usuario que utiliza para ingresar a ANDES luego presione el botton 
									aceptar y su contraseña será enviada a su casilla de correo.</TD>
								<TD align="center" width="200">
									<TABLE id="Table3" cellSpacing="0" cellPadding="3" border="0">
										<TR>
											<TD class="tbl-HorizHeader">Usuario:</TD>
											<TD align="right" colSpan="2"><asp:textbox id="txUsuario" runat="server" Width="150px"></asp:textbox></TD>
											<TD align="right"><asp:imagebutton id="ibtAceptar" runat="server" ToolTip="Aceptar" ImageUrl="../images/aceptar.gif"></asp:imagebutton></TD>
										</TR>
										<TR>
											<TD class="tbl-HorizHeader" colSpan="4"></TD>
										</TR>
									</TABLE>
									<asp:requiredfieldvalidator id="rfvCliente" runat="server" Width="100%" ErrorMessage="* Por favor ingrese su nombre de usuario."
										Display="Dynamic"></asp:requiredfieldvalidator></TD>
							</TR>
							<TR>
								<TD align="center" colSpan="2"></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
