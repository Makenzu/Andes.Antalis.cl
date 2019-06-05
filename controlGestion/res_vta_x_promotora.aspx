<%@ Page Language="vb" AutoEventWireup="false" Codebehind="res_vta_x_promotora.aspx.vb" Inherits="app.res_vta_x_promotora" %>
<%@ Register TagPrefix="Wilson" Namespace="Wilson.MasterPages" Assembly="WilsonMasterPages" %>
<META content="text/html; charset=iso-8859-1" http-equiv="Content-Type">
<LINK href="css/calendar.css" type="text/css" rel="stylesheet">
<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
<wilson:masterpage id="MPContainer" runat="server">
	<WILSON:CONTENTREGION id="MPTitle" runat="server">Resumen Ventas 
por Ejec. Comercial</WILSON:CONTENTREGION>
	<WILSON:CONTENTREGION id="MPCaption" runat="server">Resumen Ventas por Ejec. Comercial<BR>
<asp:Label id="lbFecha" runat="server"></asp:Label></WILSON:CONTENTREGION>
	<SCRIPT language="javascript" src="/js/CalendarPopup.js"></SCRIPT>
	<SCRIPT language="javascript">
			var cal1 = new CalendarPopup('caldiv1');		
			var cal2 = new CalendarPopup('caldiv2');		
	</SCRIPT>
	<TABLE id="Table3" border="0" cellSpacing="0" cellPadding="0" width="95%" align="center">
		<TR>
			<TD width="70%">
				<asp:Label id="lbErrors" runat="server" EnableViewState="False" CssClass="txt-FatalMessage"></asp:Label></TD>
			<TD width="30%" align="right"><IMG onclick="javascript:imprimir();" border="0" alt="Imprimir" src="/images/imprimir.gif"><A href="javascript: imprimir();"></A></TD>
		</TR>
	</TABLE>
	<TABLE id="Table2" border="0" cellSpacing="0" cellPadding="1" width="100%">
		<TR>
			<TD align="center">
				<asp:Panel id="plParams" runat="server" EnableViewState="False" DESIGNTIMEDRAGDROP="489">
					<TABLE id="tbParams" border="0" cellSpacing="0" cellPadding="2" width="370" align="center">
						<TR>
							<TD class="tbl-HorizHeader" width="115">Fecha Inicio:</TD>
							<TD width="120">
								<TABLE id="Table4" border="0" cellSpacing="0" cellPadding="0">
									<TR>
										<TD>
											<asp:TextBox id="txFecIni" runat="server" CssClass="textBox" Width="100px" ReadOnly="True"></asp:TextBox></TD>
										<TD align="right">&nbsp; <IMG id="imgDesde" onclick="javascript: cal1.select(document.forms[0].txFecIni,'imgDesde','dd / MM / yyyy'); return false;"
												name="imgDesde" alt="Elejir fecha" src="/images/calendar.gif">
										</TD>
									</TR>
								</TABLE>
							</TD>
							<TD width="80"></TD>
						</TR>
						<TR>
							<TD class="tbl-HorizHeader" width="115">Fecha Término:</TD>
							<TD width="120">
								<TABLE id="Table1" border="0" cellSpacing="0" cellPadding="0">
									<TR>
										<TD>
											<asp:TextBox id="txFecTer" runat="server" CssClass="textBox" Width="100px" ReadOnly="True"></asp:TextBox></TD>
										<TD align="right">&nbsp; <IMG id="imgHasta" onclick="cal2.select(document.forms[0].txFecTer,'imgHasta','dd / MM / yyyy'); return false;"
												name="imgHasta" alt="Elejir fecha" src="/images/calendar.gif">
										</TD>
									</TR>
								</TABLE>
							</TD>
							<TD width="80">
								<asp:ImageButton id="btSend" runat="server" ImageUrl="/images/procesar.gif"></asp:ImageButton>
								<DIV style="DISPLAY: none" id="disbtn" name="disbtn"><IMG alt="" src="/images/procesando.gif"></DIV>
							</TD>
						</TR>
					</TABLE>
					<BR>
				</asp:Panel>
				<asp:RequiredFieldValidator id="rfvFecTer" runat="server" CssClass="txt-AlertMessage" Display="Dynamic" ControlToValidate="txFecTer"
					ErrorMessage="* Debe ingresar una fecha de termino.<br>"></asp:RequiredFieldValidator>
				<asp:RequiredFieldValidator id="rfvFecIni" runat="server" CssClass="txt-AlertMessage" Display="Dynamic" ControlToValidate="txFecIni"
					ErrorMessage="* Debe ingresar una fecha de inicio.<br>"></asp:RequiredFieldValidator>
				<asp:CompareValidator id="cvFechas" runat="server" CssClass="txt-AlertMessage" Display="Dynamic" ControlToValidate="txFecTer"
					ErrorMessage="* Fecha Inicio debe ser mayor a Fecha Termino<BR>" Visible="False" ControlToCompare="txFecIni"
					Operator="LessThan" Type="Date" EnableClientScript="False"></asp:CompareValidator></TD>
		</TR>
		<TR>
			<TD align="center">
				<asp:Label id="lbNota" runat="server" CssClass="txt-SoftMessage" Width="300px" Visible="False"></asp:Label></TD>
		</TR>
		<TR>
			<TD align="center">
				<asp:DataGrid id="dgResultado" runat="server" ShowFooter="True" AllowSorting="True" AutoGenerateColumns="False"
					CellPadding="3">
					<FooterStyle Font-Bold="True" HorizontalAlign="Right" CssClass="tbl-DataGridFooter"></FooterStyle>
					<AlternatingItemStyle CssClass="tbl-DataGridItemAlternating"></AlternatingItemStyle>
					<ItemStyle CssClass="tbl-DataGridItem"></ItemStyle>
					<HeaderStyle CssClass="tbl-DataGridHeader"></HeaderStyle>
					<Columns>
						<asp:BoundColumn DataField="cod_int" ReadOnly="True" HeaderText="Cod. Ejec."></asp:BoundColumn>
						<asp:BoundColumn DataField="nom_int" SortExpression="nom_int DESC" ReadOnly="True" HeaderText="Nombre Ejecutivo">
							<ItemStyle Wrap="False"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="NUM_FAC" SortExpression="NUM_FAC DESC" ReadOnly="True" HeaderText="# FAC"
							DataFormatString="{0:#,##0}">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="val_venta_clp_fac" SortExpression="val_venta_clp_fac DESC" ReadOnly="True"
							HeaderText="$ FAC" DataFormatString="{0:#,##0}">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="num_ncc" SortExpression="num_ncc desc" ReadOnly="True" HeaderText="# NCC"
							DataFormatString="{0:#,##0}">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="val_venta_clp_ncc" SortExpression="val_venta_clp_ncc desc" ReadOnly="True"
							HeaderText="$ NCC" DataFormatString="{0:#,##0}">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
					</Columns>
				</asp:DataGrid></TD>
		</TR>
	</TABLE>
	<DIV style="POSITION: absolute; BACKGROUND-COLOR: white; VISIBILITY: hidden; layer-background-color: white"
		id="caldiv1"></DIV>
	<DIV style="POSITION: absolute; BACKGROUND-COLOR: white; VISIBILITY: hidden; layer-background-color: white"
		id="caldiv2"></DIV>
</wilson:masterpage>
