<%@ Page Language="vb" AutoEventWireup="false" Codebehind="vta_matriz_cliente_uni_fisicas.aspx.vb" Inherits="app.vta_matriz_cliente_uni_fisicas"%>
<%@ Register TagPrefix="Wilson" Namespace="Wilson.MasterPages" Assembly="WilsonMasterPages" %>
<META content="text/html; charset=iso-8859-1" http-equiv="Content-Type">
<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
<wilson:masterpage id="MPContainer" runat="server">
	<WILSON:CONTENTREGION id="MPTitle" runat="server">Matriz Análisis 
Venta - Cliente</WILSON:CONTENTREGION>
	<WILSON:CONTENTREGION id="MPCaption" runat="server">Matriz Análisis Venta - Cliente<BR>
<asp:Label id="lbFecha" runat="server"></asp:Label></WILSON:CONTENTREGION>
	<TABLE id="Table1" border="0" cellSpacing="0" cellPadding="3" width="100%">
		<TR>
			<TD>
				<TABLE id="Table2" border="0" cellSpacing="0" cellPadding="0" width="100%">
					<TR>
						<TD style="HEIGHT: 17px" colSpan="2">
							<TABLE id="Table5" border="0" cellSpacing="0" cellPadding="0" width="95%" align="center">
								<TR>
									<TD width="70%">
										<asp:Label id="lbErrors" runat="server" CssClass="txt-FatalMessage"></asp:Label></TD>
									<TD width="30%" align="right">
										<TABLE id="Table6" border="0" cellSpacing="0" cellPadding="0">
											<TR>
												<TD>
													<asp:ImageButton id="ibExportar" title="Exportar a Excel" runat="server" Visible="False" ImageUrl="/images/exportar.gif"
														ToolTip="Exportar a Excel"></asp:ImageButton></TD>
												<TD width="5"></TD>
												<TD><INPUT title="Imprimir" onclick="javascript:imprimir();return false;" alt="Imprimir" src="/images/imprimir.gif"
														type="image"></TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD>
							<TABLE id="Table3" border="0" cellSpacing="0" cellPadding="2" width="300">
								<TR>
									<TD style="HEIGHT: 22px" width="15"></TD>
									<TD style="HEIGHT: 22px" class="tbl-HorizHeader">Sociedad:</TD>
									<TD style="HEIGHT: 22px">
										<asp:DropDownList id="ddlSociedad" runat="server" CssClass="listBox" AutoPostBack="True">
											<asp:ListItem Value="GMSC">GMSC</asp:ListItem>
											<asp:ListItem Value="ABOL">ABOL</asp:ListItem>
											<asp:ListItem Value="APER">APER</asp:ListItem>
											<asp:ListItem Value="DGS0">DGS0</asp:ListItem>
										</asp:DropDownList></TD>
									<TD style="HEIGHT: 22px"></TD>
								</TR>
								<TR>
									<TD style="HEIGHT: 22px" width="15"></TD>
									<TD style="HEIGHT: 22px" class="tbl-HorizHeader">Ejec. Comercial:</TD>
									<TD style="HEIGHT: 22px">
										<asp:DropDownList id="ddlPromotora" runat="server" CssClass="listBox"></asp:DropDownList></TD>
									<TD style="HEIGHT: 22px"></TD>
								</TR>
								<TR>
									<TD width="15"></TD>
									<TD class="tbl-HorizHeader">Sector:</TD>
									<TD>
										<asp:DropDownList id="ddlSector" runat="server" CssClass="listBox"></asp:DropDownList></TD>
									<TD></TD>
								</TR>
								<TR>
									<TD height="20" width="15"></TD>
									<TD class="tbl-HorizHeader" height="20">Periodo:</TD>
									<TD height="20" noWrap>
										<asp:DropDownList id="ddlMes" runat="server" CssClass="listBox"></asp:DropDownList>&nbsp;/
										<asp:DropDownList id="ddlAno" runat="server" CssClass="listBox"></asp:DropDownList></TD>
									<TD height="20">
										<asp:ImageButton id="btAceptar" runat="server" ImageUrl="/images/procesar.gif"></asp:ImageButton>
										<DIV style="DISPLAY: none" id="disbtn" name="disbtn"><IMG alt="" src="/images/procesando.gif"></DIV>
									</TD>
								</TR>
							</TABLE>
						</TD>
						<TD></TD>
					</TR>
					<TR>
						<TD colSpan="2">
							<asp:Label id="lbNota" runat="server" CssClass="txt-SoftMessage" Visible="False">* Ventas hasta el último mes cerrado.</asp:Label></TD>
					</TR>
				</TABLE>
				<asp:Label id="lbNomPromo" runat="server" CssClass="tbl-HorizItem"></asp:Label>
				<asp:Label id="lbCodPromo" runat="server" CssClass="tbl-HorizItem"></asp:Label></TD>
		</TR>
		<TR>
			<TD>
				<asp:Table id="tbResultado" runat="server" BorderWidth="1px" GridLines="Both"></asp:Table></TD>
		</TR>
	</TABLE>
</wilson:masterpage>
