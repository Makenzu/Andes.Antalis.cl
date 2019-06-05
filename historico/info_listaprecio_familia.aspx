<%@ Page Language="vb" AutoEventWireup="false" Codebehind="info_listaprecio_familia.aspx.vb" Inherits="app.info_listaprecio_familia"%>
<%@ Register TagPrefix="Wilson" Namespace="Wilson.MasterPages" Assembly="WilsonMasterPages" %>
<META content="text/html; charset=iso-8859-1" http-equiv="Content-Type">
<LINK href="css/calendar.css" type="text/css" rel="stylesheet">
<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
<wilson:masterpage id="MPContainer" runat="server" DESIGNTIMEDRAGDROP="376">
	<WILSON:CONTENTREGION id="MPTitle" runat="server">ANDES</WILSON:CONTENTREGION>
	<WILSON:CONTENTREGION id="MPCaption" runat="server">Lista de Precios<BR>
<asp:Label id="lbFecha" runat="server"></asp:Label></WILSON:CONTENTREGION>
	<SCRIPT language="javascript" src="/js/CalendarPopup.js"></SCRIPT>
	<SCRIPT language="javascript">
			var cal1 = new CalendarPopup(('caldiv1'));		
			
			function Validar(){
				var myObj 
				//myObj = MM_findObj('cblSubFamilias')
				if 	(document.forms[0].cblSubFamilias_0.checked){alert();}
				return false;
			}
			
	</SCRIPT>
	<TABLE id="Table2" border="0" cellSpacing="0" cellPadding="0" width="100%">
		<TR>
			<TD width="50%" colSpan="2">
				<TABLE id="Table5" border="0" cellSpacing="0" cellPadding="0" width="95%" align="center">
					<TR>
						<TD width="70%">
							<asp:Label id="lbErrors" runat="server" CssClass="txt-FatalMessage"></asp:Label></TD>
						<TD width="30%" align="right"><IMG onclick="javascript:imprimir();" border="0" alt="Imprimir" src="/images/imprimir.gif"><A href="javascript: imprimir();"></A></TD>
					</TR>
				</TABLE>
			</TD>
		</TR>
		<TR>
			<TD style="HEIGHT: 223px" vAlign="bottom" width="45%" colSpan="2" align="center"><TABLE id="tbParams" border="0" cellSpacing="0" cellPadding="2">
					<TR>
						<TD class="tbl-HorizHeader" colSpan="2" align="left">
							<TABLE id="Table6" border="0" cellSpacing="0" cellPadding="2" width="300">
								<TR>
									<TD class="tbl-HorizHeader" width="70">Familia :
									</TD>
									<TD width="220">
										<asp:dropdownlist id="ddlFamilias" runat="server" CssClass="listBox" AutoPostBack="True"></asp:dropdownlist></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD class="tbl-HorizHeader" height="27" colSpan="2">
							<TABLE id="Table7" border="0" cellSpacing="0" cellPadding="1" width="100%" bgColor="#f5f5f5">
								<TR>
									<TD colSpan="3">
										<asp:CheckBoxList id="cblSubFamilias" runat="server" CssClass="tbl-HorizItem" CellPadding="1" RepeatColumns="4"
											RepeatDirection="Horizontal" CellSpacing="1"></asp:CheckBoxList></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD class="tbl-HorizHeader" colSpan="2" align="left">
							<TABLE id="Table1" border="0" cellSpacing="0" cellPadding="1" width="300">
								<TR>
									<TD class="tbl-HorizHeader" width="70">Fecha :
									</TD>
									<TD width="150" noWrap>
										<TABLE id="Table4" border="0" cellSpacing="0" cellPadding="0" width="125">
											<TR>
												<TD width="100">
													<asp:TextBox id="txFecha" runat="server" CssClass="textBox" Width="100px" ReadOnly="True"></asp:TextBox></TD>
												<TD width="25" align="center"><IMG id="imgDesde" onclick="cal1.select(document.forms[0].txFecha,'imgDesde','dd / MM / yyyy'); return false;"
														name="imgDesde" alt="Elejir fecha" src="/images/calendar.gif" width="17" height="16"></TD>
											</TR>
										</TABLE>
									</TD>
									<TD width="70" align="center"><INPUT id="btSend" onclick="javascript: Validar();return false;" src="/images/aceptar.gif"
											type="image" name="btSend">
										<DIV style="DISPLAY: none" id="disbtn" name="disbtn"><IMG alt="" src="/images/procesando.gif"></DIV>
									</TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
				</TABLE>
				<DIV style="WIDTH: 200px; DISPLAY: none; HEIGHT: 15px; COLOR: red" id="rfvFamilia" class="txt-AlertMessage"
					ms_positioning="FlowLayout">* Debe seleccionar una familia.</DIV>
				<asp:Label id="rfvSubFamilia" runat="server" CssClass="txt-AlertMessage" Visible="False" ForeColor="Red"
					EnableViewState="False">* Debe seleccionar por lo menos una subfamilia.</asp:Label></TD>
		</TR>
		<TR>
			<TD vAlign="bottom" width="45%" colSpan="2">
				<asp:Panel id="plDatos" runat="server" Visible="False">
					<TABLE id="Table3" border="1" cellSpacing="0" cellPadding="3">
						<TR>
							<TD class="tbl-HorizHeader">Familia :</TD>
							<TD align="left">&nbsp;
								<asp:Label id="lbData" runat="server" CssClass="tbl-HorizItem"></asp:Label></TD>
						</TR>
					</TABLE>
				</asp:Panel></TD>
		</TR>
		<TR>
			<TD style="HEIGHT: 17px" width="50%" colSpan="2">
				<asp:Label id="lbNota" runat="server" CssClass="txt-SoftMessage" Visible="False"></asp:Label></TD>
		</TR>
		<TR>
			<TD width="50%" colSpan="2">
				<asp:Table id="tbResultado" runat="server" CellPadding="1" GridLines="Both" BorderWidth="1px"></asp:Table></TD>
		</TR>
	</TABLE> <!-- CALENDARIO -->
	<DIV style="POSITION: absolute; BACKGROUND-COLOR: white; VISIBILITY: hidden; layer-background-color: white"
		id="caldiv1"></DIV>
</wilson:masterpage>
