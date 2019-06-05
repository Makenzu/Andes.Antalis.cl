<%@ Page Language="vb" AutoEventWireup="false" Codebehind="header.aspx.vb"%>    <%--Inherits="Otello.header"--%>   <%--Inecesario si no hay namespace--%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>header</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../Styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" bgColor="#ffffff" border="0">
				<TR>
					<TD class="label-campo" vAlign="top" noWrap bgColor="#ff9900"><STRONG><asp:radiobuttonlist id="rblTipoBusqueda" runat="server" Width="152px" CellPadding="0" CellSpacing="0"
								CssClass="valor-campo">
								<asp:ListItem Value="PC">Por c&#243;digo</asp:ListItem>
								<asp:ListItem Value="RS" Selected="True">Por Raz&#243;n Social</asp:ListItem>
							</asp:radiobuttonlist></STRONG></TD>
					<TD vAlign="top">
						<TABLE id="Table3" cellSpacing="1" cellPadding="1" border="0" width="100%">
							<TR>
								<TD bgColor="#ffad34"><asp:textbox id="tbPatron" runat="server" CssClass="valor-campo" BorderStyle="Groove" Width="150px"
										MaxLength="32"></asp:textbox><asp:button id="Button2" runat="server" Text="buscar"></asp:button></TD>
							</TR>
							<TR>
								<TD bgColor="#ffc168"><asp:checkbox id="chAlcanceBusqueda" runat="server" Text="Buscar en todo el universo de clientes."
										Font-Size="XX-Small" Font-Names="Arial" Checked="True"></asp:checkbox></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="texto-hint" vAlign="top" colSpan="2">
						Utilice comodin * para reemplazar por cualquier palabra. Ejemplo <EM><STRONG>imprenta*</STRONG></EM>
						busca todos aquellas&nbsp;coincidencias que&nbsp;comienzan&nbsp;con la palabra <EM>imprenta</EM>.</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
