<%@ Register TagPrefix="Wilson" Namespace="Wilson.MasterPages" Assembly="WilsonMasterPages" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="analisisCarteraClientePromotora.aspx.vb" Inherits="app.analisisCarteraClientePromotora"%>
<META content="text/html; charset=iso-8859-1" http-equiv="Content-Type">
<LINK href="/css/calendar.css" type="text/css" rel="stylesheet">
<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
<wilson:masterpage id="MPContainer" runat="server">
	<WILSON:CONTENTREGION id="MPTitle" runat="server">Análisis Cartera 
Ejec. Comercial</WILSON:CONTENTREGION>
	<WILSON:CONTENTREGION id="MPCaption" runat="server">Análisis Cartera Ejec. Comercial</WILSON:CONTENTREGION>
	<TABLE id="Table1" border="0" cellSpacing="0" cellPadding="1" width="100%">
		<TR>
			<TD style="WIDTH: 131px" align="right"></TD>
			<TD></TD>
		</TR>
		<TR>
			<TD style="WIDTH: 131px" align="right"></TD>
			<TD align="center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
				<asp:ImageButton id="ibExportar" title="Exportar a Excel" runat="server" EnableViewState="False"
					ImageUrl="/images/exportar.gif" ToolTip="Exportar a Excel" Visible="False"></asp:ImageButton></TD>
		</TR>
		<TR>
			<TD style="HEIGHT: 132px" vAlign="top" colSpan="2" align="left">
				<TABLE id="Table2" border="0" cellSpacing="1" cellPadding="2">
					<TR>
						<TD class="txt-SoftMessage ">Periodo:</TD>
						<TD align="left">
							<asp:DropDownList id="ddlPeriodo" runat="server" CssClass="verdana-10-normal"></asp:DropDownList></TD>
						<TD width="22" align="right"></TD>
						<TD align="right"></TD>
					</TR>
					<TR>
						<TD class="txt-SoftMessage ">Ejec. Comercial:</TD>
						<TD align="right">
							<asp:DropDownList id="ddlPromotoras" runat="server" CssClass="verdana-10-normal" Width="200px"></asp:DropDownList></TD>
						<TD width="22" align="right"></TD>
						<TD align="right">
							<asp:Label id="Label1" runat="server" CssClass="txt-SoftMessage "></asp:Label></TD>
					</TR>
					<TR>
						<TD class="txt-SoftMessage ">Area:</TD>
						<TD align="right">
							<asp:DropDownList id="ddlArea" runat="server" CssClass="verdana-10-normal" Width="200px">
								<asp:ListItem Value="PAP">PAPELES</asp:ListItem>
								<asp:ListItem Value="CVI">COM VISUAL</asp:ListItem>
								<asp:ListItem Value="EII">EQUIPOS E INSUMOS</asp:ListItem>
								<asp:ListItem Value="ZZZ">OTROS</asp:ListItem>
								<asp:ListItem Value="*" Selected="True">* TODAS *</asp:ListItem>
							</asp:DropDownList></TD>
						<TD width="22" align="right"></TD>
						<TD style="HEIGHT: 20px" align="right">
							<DIV id="Div1" name="disbtn">&nbsp;</DIV>
						</TD>
					</TR>
					<TR>
						<TD class="txt-SoftMessage ">Concepto:</TD>
						<TD align="right">
							<asp:DropDownList id="ddlConcepto" runat="server" CssClass="verdana-10-normal" Width="200px">
								<asp:ListItem Value="VGUSD" Selected="True">VENTA USD</asp:ListItem>
								<asp:ListItem Value="MGUSD">MARGEN USD</asp:ListItem>
								<asp:ListItem Value="VOL">VOLUMEN</asp:ListItem>
								<asp:ListItem Value="UMB">ITEMS</asp:ListItem>
							</asp:DropDownList></TD>
						<TD style="WIDTH: 22px" width="22" align="right">
							<asp:ImageButton id="btConsulta" runat="server" ImageUrl="/images/procesar.gif"></asp:ImageButton></TD>
						<TD class="txt-SoftMessage " align="right">
							<asp:Label id="Label2" runat="server">Consultar</asp:Label></TD>
					</TR>
					<TR id="filaFiltroCliente" runat="server">
						<TD class="txt-SoftMessage ">Clientes:</TD>
						<TD align="right">
							<asp:DropDownList id="ddlFiltroCliente" runat="server" CssClass="verdana-10-normal" Width="200px">
								<asp:ListItem Value="TODOS" Selected="True">TODOS</asp:ListItem>
								<asp:ListItem Value="CARTERA">CARTERA</asp:ListItem>
							</asp:DropDownList></TD>
						<TD width="22" align="right"></TD>
						<TD style="HEIGHT: 20px" align="right">
							<DIV id="Div1" name="disbtn">&nbsp;</DIV>
						</TD>
					</TR>
				</TABLE>
			</TD>
		</TR>
		<TR>
			<TD style="WIDTH: 131px; HEIGHT: 100px" bgColor="#ffffff" vAlign="top" align="left">
				<asp:Table id="tblFamilias" runat="server" Width="100%" CellPadding="1" CellSpacing="0"></asp:Table></TD>
			<TD style="HEIGHT: 100px" vAlign="top" align="left">
				<asp:Table id="tblDetalleAnalisis" runat="server" Width="100%" CellPadding="2" CellSpacing="1"></asp:Table>
				<P>&nbsp;</P>
				<P>
					<asp:Label id="lb1" runat="server" Visible="False" CssClass="txt-SoftMessage " Font-Size="Smaller"
						Font-Names="Arial">- (*):  Cliente actualmente vigentes dentro de cartera ejec. comercial.</asp:Label></P>
				<P><FONT size="2" face="Arial"><STRONG>
							<asp:Label id="lb2" runat="server" Visible="False" CssClass="txt-SoftMessage " ForeColor="Red">- Razones sociales en rojo:</asp:Label>
							<asp:Label id="lb3" runat="server" Visible="False" CssClass="txt-SoftMessage " Font-Names="Arial">clientes que pertenecieron a cartera en el periodo 12 meses.</asp:Label></P>
				<DIV>
					<asp:Label id="lb4" runat="server" Visible="False" CssClass="txt-SoftMessage " ForeColor="Red">- Cifras en rojo: </asp:Label>
					<asp:Label id="lb5" runat="server" Visible="False" CssClass="txt-SoftMessage " Font-Names="Arial">ventas de clientes que en el mes respectivo no pertenecian a cartera.</asp:Label></DIV>
				</STRONG></FONT></TD>
		</TR>
		<TR>
			<TD style="WIDTH: 131px"></TD>
			<TD>
				<DIV><FONT size="2" face="Arial"><STRONG></STRONG></FONT>&nbsp;</DIV>
			</TD>
		</TR>
	</TABLE>
</wilson:masterpage>
