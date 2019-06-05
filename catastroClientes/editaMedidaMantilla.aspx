<%@ Page Language="vb" AutoEventWireup="false" Codebehind="editaMedidaMantilla.aspx.vb" Inherits="app.editaMedidaMantilla"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Registro de Máquina de Cliente</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href="../css/andes.css">
		<asp:Literal id="Literal1" runat="server"></asp:Literal>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<fieldset><legend style="Z-INDEX: 0">
					<P>Edita Medida Mantilla</P>
				</legend>
				<BR>
				<TABLE id="Table1" border="0" cellSpacing="1" cellPadding="3">
					<TR>
						<TD class="tbl-HorizHeader">Descripción :</TD>
						<TD>
							<asp:TextBox id="tbDmcMedidaMantilla" runat="server" Width="296px"></asp:TextBox></TD>
					</TR>
				</TABLE>
			</fieldset>
			<P></P>
			<P><asp:button style="Z-INDEX: 0" id="bGrabaMaquina" runat="server" Text="grabar"></asp:button><asp:button style="Z-INDEX: 0" id="bCancelar" runat="server" Text="cerrar ventana"></asp:button><INPUT id="hfAccion" type="hidden" name="hfAccion" runat="server">&nbsp;<asp:label id="lMensajeError" runat="server"></asp:label><INPUT style="Z-INDEX: 0" id="hfIdMedidaPlancha" type="hidden" name="hfIdMedidaPlancha"
					runat="server">
			</P>
		</form>
	</body>
</HTML>
