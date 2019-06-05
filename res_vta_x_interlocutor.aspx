<%@ Register TagPrefix="Wilson" Namespace="Wilson.MasterPages" Assembly="WilsonMasterPages" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="res_vta_x_interlocutor.aspx.vb"%>		 <%--Inherits="app.res_vta_x_interlocutor"--%>
<META http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<LINK href="css/calendar.css" type="text/css" rel="stylesheet">
<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
<wilson:masterpage id="MPContainer" runat="server">
<WILSON:CONTENTREGION id="MPTitle" runat="server">ANDES</WILSON:CONTENTREGION> 
<WILSON:CONTENTREGION id="MPCaption" runat="server">Resumen Ventas por Interlocutor<BR>
<asp:Label id="lbFecha" runat="server"></asp:Label></WILSON:CONTENTREGION>&nbsp; 
<asp:Label id="lbErrors" runat="server" CssClass="txt-FatalMessage" EnableViewState="False"></asp:Label>
<SCRIPT language="javascript" src="js/CalendarPopup.js"></SCRIPT>

<SCRIPT language="javascript">
			var cal1 = new CalendarPopup('caldiv1');		
			var cal2 = new CalendarPopup('caldiv2');		
	</SCRIPT>

<TABLE id="Table2" cellSpacing="0" cellPadding="1" width="100%" border="0">
		<TR>
			<TD align="center">
				<asp:Panel id="plParams" runat="server" EnableViewState="False" DESIGNTIMEDRAGDROP="489">
					<TABLE id="tbParams" cellSpacing="0" cellPadding="2" width="370" align="center" border="0">
						<TR>
							<TD class="tbl-HorizHeader" width="115">Fecha Inicio:</TD>
							<TD width="120">
								<TABLE id="Table4" cellSpacing="0" cellPadding="0" border="0">
									<TR>
										<TD>
											<asp:TextBox id="txFecIni" runat="server" CssClass="textBox" ReadOnly="True" Width="100px"></asp:TextBox></TD>
										<TD align="right">&nbsp; <IMG id="imgDesde" onclick="javascript: cal1.select(document.forms[0].txFecIni,'imgDesde','dd / MM / yyyy'); return false;"
												alt="Elejir fecha" src="images/calendar.gif" name="imgDesde">
										</TD>
									</TR>
								</TABLE>
							</TD>
							<TD width="80"></TD>
						</TR>
						<TR>
							<TD class="tbl-HorizHeader" width="115">Fecha Término:</TD>
							<TD width="120">
								<TABLE id="Table1" cellSpacing="0" cellPadding="0" border="0">
									<TR>
										<TD>
											<asp:TextBox id="txFecTer" runat="server" CssClass="textBox" ReadOnly="True" Width="100px"></asp:TextBox></TD>
										<TD align="right">&nbsp; <IMG id="imgHasta" onclick="cal2.select(document.forms[0].txFecTer,'imgHasta','dd / MM / yyyy'); return false;"
												alt="Elejir fecha" src="images/calendar.gif" name="imgHasta">
										</TD>
									</TR>
								</TABLE>
							</TD>
							<TD width="80">
								<asp:ImageButton id="btSend" runat="server" ImageUrl="images/aceptar.gif"></asp:ImageButton>
								<DIV id="disbtn" style="DISPLAY: none" name="disbtn"><IMG alt="" src="images/procesando.gif"></DIV>
							</TD>
						</TR>
					</TABLE>
					<BR>
				</asp:Panel>
				<asp:RequiredFieldValidator id="rfvFecTer" runat="server" CssClass="txt-AlertMessage" ErrorMessage="* Debe ingresar una fecha de termino.<br>"
					ControlToValidate="txFecTer" Display="Dynamic"></asp:RequiredFieldValidator>
				<asp:RequiredFieldValidator id="rfvFecIni" runat="server" CssClass="txt-AlertMessage" ErrorMessage="* Debe ingresar una fecha de inicio.<br>"
					ControlToValidate="txFecIni" Display="Dynamic"></asp:RequiredFieldValidator>
				<asp:CompareValidator id="cvFechas" runat="server" CssClass="txt-AlertMessage" ErrorMessage="* Fecha Inicio debe ser mayor a Fecha Termino<BR>"
					ControlToValidate="txFecTer" Display="Dynamic" EnableClientScript="False" Type="Date" Operator="LessThan"
					ControlToCompare="txFecIni" Visible="False"></asp:CompareValidator></TD>
		</TR>
		<TR>
			<TD align="center">
				<asp:Label id="lbNota" runat="server" CssClass="txt-SoftMessage" Width="300px" Visible="False"></asp:Label></TD>
		</TR>
		<TR>
			<TD align="center">
				<asp:DataGrid id="dgResultado" runat="server" CellPadding="3" AutoGenerateColumns="False" AllowSorting="True"
					ShowFooter="True">
					<FooterStyle Font-Bold="True" CssClass="tbl-DataGridFooter"></FooterStyle>
					<AlternatingItemStyle CssClass="tbl-DataGridItemAlternating"></AlternatingItemStyle>
					<ItemStyle CssClass="tbl-DataGridItem"></ItemStyle>
					<HeaderStyle CssClass="tbl-DataGridHeader"></HeaderStyle>
					<Columns>
						<asp:BoundColumn DataField="cod_vendedora" ReadOnly="True" HeaderText="Cod. Vend."></asp:BoundColumn>
						<asp:BoundColumn DataField="nom_vendedora" SortExpression="nom_vendedora DESC" ReadOnly="True" HeaderText="Nom. Vend.">
							<ItemStyle Wrap="False"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="NUM_FACTURAS" SortExpression="NUM_FACTURAS DESC" ReadOnly="True" HeaderText="Documentos">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="VAL_FACTURAS" SortExpression="VAL_FACTURAS DESC" ReadOnly="True" HeaderText="Venta ($)">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
					</Columns>
				</asp:DataGrid></TD>
		</TR>
	</TABLE>
<DIV id="caldiv1" style="VISIBILITY: hidden; POSITION: absolute; BACKGROUND-COLOR: white; layer-background-color: white"></DIV>
<DIV id="caldiv2" style="VISIBILITY: hidden; POSITION: absolute; BACKGROUND-COLOR: white; layer-background-color: white"></DIV></wilson:masterpage>
