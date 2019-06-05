<%@ Page Language="vb" AutoEventWireup="false" Codebehind="header.aspx.vb" Inherits="app.header" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>header</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
		<meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../css/andes.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body bottomMargin="10" leftMargin="2" topMargin="2">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="1" width="100%" border="0">
				<TR>
					<TD bgColor="#3b4d6b">
						<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" bgColor="#ffffff" border="0">
							<TR>
								<TD class="tbl-HorizHeader" style="WIDTH: 7px; HEIGHT: 20px"></TD>
								<TD class="tbl-HorizHeader" style="HEIGHT: 20px"><STRONG>Código :</STRONG></TD>
								<TD style="HEIGHT: 20px">
									<asp:TextBox id="tbCodigoCliente" runat="server" MaxLength="9" Width="105px" BorderStyle="Groove"
										CssClass="textBox"></asp:TextBox>
									<asp:Button id="Button1" runat="server" Text="buscar"></asp:Button></TD>
							</TR>
							<TR>
								<TD class="tbl-HorizHeader" style="WIDTH: 7px"></TD>
								<TD class="tbl-HorizHeader"><STRONG>Razón Social :</STRONG></TD>
								<TD>
									<asp:TextBox id="tbRazonSocial" runat="server" MaxLength="80" Width="280px" BorderStyle="Groove"
										CssClass="textBox"></asp:TextBox>
									<asp:Button id="Button2" runat="server" Text="buscar"></asp:Button></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			&nbsp;
		</form>
	</body>
</HTML>
