<%@ Register TagPrefix="Wilson" Namespace="Wilson.MasterPages" Assembly="WilsonMasterPages" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="res_descto_manual.aspx.vb" Inherits="app.res_descto_manual"%>
<META content="text/html; charset=iso-8859-1" http-equiv="Content-Type">
<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
<wilson:masterpage id="MPContainer" runat="server">
	<WILSON:CONTENTREGION id="MPTitle" runat="server">Control De 
Descuentos Manuales Modificados</WILSON:CONTENTREGION>
	<WILSON:CONTENTREGION id="MPCaption" runat="server">Control De Descuentos Manuales Modificados<BR>
<asp:Label id="lbFecha" runat="server"></asp:Label></WILSON:CONTENTREGION>
	<LINK rel="stylesheet" type="text/css" href="css/calendar.css">
	<SCRIPT language="javascript" src="/js/CalendarPopup.js"></SCRIPT>
	<SCRIPT language="javascript">
			var cal1 = new CalendarPopup(('caldiv1'));		
	</SCRIPT>
	<TABLE id="Table3" border="0" cellSpacing="0" cellPadding="0" width="95%" align="center">
		<TR>
			<TD width="70%">
				<asp:Label id="lbErrors" runat="server" CssClass="txt-FatalMessage"></asp:Label></TD>
			<TD width="30%" align="right">
				<TABLE id="Table4" border="0" cellSpacing="0" cellPadding="0">
					<TR>
						<TD>
							<asp:ImageButton id="ibExportar" title="Exportar a Excel" runat="server" ImageUrl="/images/exportar.gif"
								Visible="False" ToolTip="Exportar a Excel" EnableViewState="False"></asp:ImageButton></TD>
						<TD width="5"></TD>
						<TD><INPUT title="Imprimir" onclick="javascript:imprimir();return false;" alt="Imprimir" src="/images/imprimir.gif"
								type="image"></TD>
					</TR>
				</TABLE>
			</TD>
		</TR>
	</TABLE>
	<TABLE id="Table1" border="0" cellSpacing="0" cellPadding="3" align="center">
		<TR>
			<TD vAlign="bottom"></TD>
			<TD>
				<asp:RequiredFieldValidator id="rfvSubFam" runat="server" CssClass="txt-AlertMessage" Display="Dynamic" ErrorMessage="* Debe ingresar una fecha."
					ControlToValidate="txFecha"></asp:RequiredFieldValidator>
				<TABLE id="Table2" border="0" cellSpacing="0" cellPadding="2" width="300" align="center">
					<TR>
						<TD class="tbl-HorizHeader" width="74">Fecha:</TD>
						<TD style="WIDTH: 143px">
							<TABLE id="Table5" border="0" cellSpacing="0" cellPadding="0">
								<TR>
									<TD>
										<asp:TextBox id="txFecha" runat="server" CssClass="textBox" ReadOnly="True" Width="100px"></asp:TextBox></TD>
									<TD align="right">&nbsp;<IMG id="imgDesde" onclick="cal1.select(document.forms[0].txFecha,'imgDesde','dd / MM / yyyy'); return false;"
											name="imgDesde" alt="Elegir fecha" src="/images/calendar.gif">
									</TD>
								</TR>
							</TABLE>
						</TD>
						<TD align="center">
							<asp:ImageButton id="btSend" runat="server" ImageUrl="/images/procesar.gif"></asp:ImageButton>
							<DIV style="DISPLAY: none" id="disbtn" name="disbtn"><IMG alt="" src="/images/procesando.gif"></DIV>
						</TD>
					</TR>
				</TABLE>
			</TD>
		</TR>
		<TR>
			<TD colSpan="2">
				<asp:Label id="lbNota" runat="server" CssClass="txt-SoftMessage" Visible="False"></asp:Label>
				<asp:Table id="tbResultado" runat="server" CellSpacing="0" GridLines="Both" BorderWidth="1px"
					CellPadding="2"></asp:Table></TD>
		</TR>
	</TABLE>
	<DIV style="POSITION: absolute; BACKGROUND-COLOR: white; VISIBILITY: hidden; layer-background-color: white"
		id="caldiv1"></DIV>
</wilson:masterpage>
