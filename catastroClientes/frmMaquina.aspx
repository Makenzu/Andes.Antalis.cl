<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frmMaquina.aspx.vb" Inherits="app.ingresaMaquina"%>
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
			<P><asp:literal id="Literal1" runat="server"></asp:literal></P>
			<P style="Z-INDEX: 0">Cliente:<asp:label style="Z-INDEX: 0" id="lCliente" runat="server"></asp:label></P>
			<fieldset><legend>Registro máquina</legend><BR>
				<TABLE id="Table1" border="0" cellSpacing="1" cellPadding="3">
					<TR>
						<TD class="tbl-HorizHeader">Máquina:</TD>
						<TD><asp:dropdownlist id="ddlMaquina" runat="server" CssClass="listBox"></asp:dropdownlist></TD>
					</TR>
					<TR>
						<TD class="tbl-HorizHeader">Tamaño:</TD>
						<TD><asp:dropdownlist style="Z-INDEX: 0" id="ddlTamanhoMaquina" runat="server" CssClass="listBox"></asp:dropdownlist></TD>
					</TR>
					<TR>
						<TD class="tbl-HorizHeader" noWrap>N° máquinas:</TD>
						<TD><asp:textbox style="Z-INDEX: 0" id="tbNumeroMaquinas" runat="server" CssClass="textBox" MaxLength="3"
								Width="37px"></asp:textbox><INPUT style="Z-INDEX: 0" id="hfIdRegistro" type="hidden" name="hfIdRegistro" runat="server"></TD>
					</TR>
					<TR>
						<TD class="tbl-HorizHeader" noWrap>N° de cuerpos
							<BR>
							impresores:</TD>
						<TD><asp:textbox id="tbCuerposImpresores" runat="server" CssClass="textBox" MaxLength="3" Width="37px"></asp:textbox><INPUT style="Z-INDEX: 0" id="hfAccion" type="hidden" name="hfAccion" runat="server"></TD>
					</TR>
					<TR>
						<TD class="tbl-HorizHeader">Torre barniz:</TD>
						<TD><asp:checkbox id="chkbTorreBarniz" runat="server" CssClass="tbl-HorizItem" Text="Con torre de barniz."></asp:checkbox></TD>
					</TR>
					<TR>
						<TD class="tbl-HorizHeader">Medida&nbsp;plancha:</TD>
						<TD><asp:dropdownlist style="Z-INDEX: 0" id="ddlTamanoPlancha" runat="server" CssClass="listBox"></asp:dropdownlist></TD>
					</TR>
					<TR>
						<TD class="tbl-HorizHeader">Medida&nbsp;mantilla</TD>
						<TD>
							<asp:dropdownlist style="Z-INDEX: 0" id="ddlTamanoMantilla" runat="server" CssClass="listBox"></asp:dropdownlist></TD>
					</TR>
				</TABLE>
			</fieldset>
			<P></P>
			<fieldset><legend>Consumo Mensual</legend><BR>
				<TABLE id="Table2" border="0" cellSpacing="1" cellPadding="3">
					<TR>
						<TD class="tbl-HorizHeader">Papel:</TD>
						<TD class="tbl-HorizItem"><asp:textbox style="Z-INDEX: 0" id="tbConsumoPapel" runat="server" CssClass="textBox" MaxLength="9"
								Width="80px"></asp:textbox>&nbsp;Ton&nbsp;/ mes</TD>
					</TR>
					<TR>
						<TD class="tbl-HorizHeader">Planchas:</TD>
						<TD class="tbl-HorizItem"><asp:textbox style="Z-INDEX: 0" id="tbConsumoPlanchas" runat="server" CssClass="textBox" MaxLength="9"
								Width="80px"></asp:textbox>&nbsp;Ues / mes</TD>
					</TR>
					<TR>
						<TD class="tbl-HorizHeader">Tintas proceso:</TD>
						<TD class="tbl-HorizItem"><asp:textbox style="Z-INDEX: 0" id="tbConsumoProceso" runat="server" CssClass="textBox" MaxLength="9"
								Width="80px"></asp:textbox>&nbsp;Kg / mes</TD>
					</TR>
					<TR>
						<TD class="tbl-HorizHeader">Tintas pantone:</TD>
						<TD class="tbl-HorizItem"><asp:textbox style="Z-INDEX: 0" id="tbConsumoPantone" runat="server" CssClass="textBox" MaxLength="9"
								Width="80px"></asp:textbox>&nbsp;Kg / mes</TD>
					</TR>
					<TR>
						<TD class="tbl-HorizHeader">Barniz:</TD>
						<TD class="tbl-HorizItem"><asp:textbox style="Z-INDEX: 0" id="tbConsumoBarniz" runat="server" CssClass="textBox" MaxLength="9"
								Width="80px"></asp:textbox>&nbsp;Kg / mes</TD>
					</TR>
					<TR>
						<TD class="tbl-HorizHeader">Mantillas:</TD>
						<TD class="tbl-HorizItem"><asp:textbox style="Z-INDEX: 0" id="tbConsumoMantillas" runat="server" CssClass="textBox" MaxLength="9"
								Width="80px"></asp:textbox>&nbsp;Ues / mes</TD>
					</TR>
				</TABLE>
			</fieldset>
			<P><asp:button style="Z-INDEX: 0" id="bGrabaMaquina" runat="server" Text="grabar"></asp:button><asp:button style="Z-INDEX: 0" id="bCancelar" runat="server" Text="cerrar ventana"></asp:button>&nbsp;<asp:label id="lMensajeError" runat="server"></asp:label></P>
		</form>
	</body>
</HTML>
