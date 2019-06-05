<%@ Register TagPrefix="Wilson" Namespace="Wilson.MasterPages" Assembly="WilsonMasterPages" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="res_sinvta_cliente_promo.aspx.vb" Inherits="app.res_sinvta_cliente_promo"%>
<META content="text/html; charset=iso-8859-1" http-equiv="Content-Type">
<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
<wilson:masterpage id="MPContainer" runat="server">
	<WILSON:CONTENTREGION id="MPTitle" runat="server">ANDES</WILSON:CONTENTREGION>
	<WILSON:CONTENTREGION id="MPCaption" runat="server">Resumen Cliente Sin Ventas Mes 
<asp:Label id="lbFecha" runat="server"></asp:Label></WILSON:CONTENTREGION>
	<TABLE id="Table1" border="0" cellSpacing="0" cellPadding="0" width="95%" align="center">
		<TR>
			<TD width="70%">
				<asp:Label id="lbErrors" runat="server" CssClass="txt-FatalMessage"></asp:Label></TD>
			<TD width="30%" align="right">
				<TABLE id="Table4" border="0" cellSpacing="0" cellPadding="0">
					<TR>
						<TD>
							<asp:ImageButton id="ibExportar" title="Exportar a Excel" runat="server" ImageUrl="/images/exportar.gif"
								ToolTip="Exportar a Excel" Visible="False" EnableViewState="False"></asp:ImageButton></TD>
						<TD width="5"></TD>
						<TD><INPUT title="Imprimir" onclick="javascript:imprimir();return false;" alt="Imprimir" src="/images/imprimir.gif"
								type="image"></TD>
					</TR>
				</TABLE>
			</TD>
		</TR>
	</TABLE>
	<TABLE id="Table2" border="0" cellSpacing="0" cellPadding="1" width="95%" align="center"
		DESIGNTIMEDRAGDROP="4">
		<TR>
			<TD vAlign="bottom">
				<TABLE id="Table5" border="0" cellSpacing="0" cellPadding="2">
					<TR>
						<TD class="tbl-HorizHeader" width="15"></TD>
						<TD class="tbl-HorizHeader">Ejec. Comercial:</TD>
						<TD colSpan="2">
							<asp:DropDownList id="ddlPromotora" runat="server" CssClass="listBox" Visible="False"></asp:DropDownList></TD>
						<TD></TD>
					</TR>
					<TR>
						<TD class="tbl-HorizHeader" width="15"></TD>
						<TD class="tbl-HorizHeader">Periodo:</TD>
						<TD colSpan="2">
							<asp:DropDownList id="ddlMes" runat="server" CssClass="listBox"></asp:DropDownList>&nbsp;/
							<asp:DropDownList id="ddlAno" runat="server" CssClass="listBox"></asp:DropDownList></TD>
						<TD><INPUT id="btSend" src="/images/procesar.gif" type="image" name="btSend">
							<DIV style="DISPLAY: none" id="disbtn" name="disbtn"><IMG alt="" src="/images/procesando.gif"></DIV>
						</TD>
					</TR>
				</TABLE>
			</TD>
			<TD></TD>
		</TR>
		<TR>
			<TD height="25" colSpan="2" align="left">
				<asp:Label id="lbNota" runat="server" CssClass="txt-SoftMessage" Visible="False"></asp:Label></TD>
		</TR>
		<TR>
			<TD bgColor="#a3bad6" height="25" colSpan="2" align="center">
				<asp:Label id="lbCodPromo" runat="server" CssClass="tbl-HorizItem"></asp:Label>
				<asp:Label id="lbNomPromo" runat="server" CssClass="tbl-HorizItem"></asp:Label></TD>
		</TR>
		<TR>
			<TD colSpan="2" align="center">
				<asp:DataGrid id="dgResultado" runat="server" Width="100%" ShowFooter="True" AllowSorting="True"
					AutoGenerateColumns="False" CellPadding="3">
					<FooterStyle HorizontalAlign="Right" CssClass="tbl-DataGridFooter"></FooterStyle>
					<AlternatingItemStyle CssClass="tbl-DataGridItemAlternating"></AlternatingItemStyle>
					<ItemStyle CssClass="tbl-DataGridItem"></ItemStyle>
					<HeaderStyle CssClass="tbl-DataGridHeader"></HeaderStyle>
					<Columns>
						<asp:BoundColumn DataField="cod_cliente" SortExpression="cod_cliente desc" ReadOnly="True" HeaderText="Cod. Cli."></asp:BoundColumn>
						<asp:BoundColumn DataField="nom_cliente" SortExpression="nom_cliente desc" ReadOnly="True" HeaderText="--- Raz&#243;n Social ---"></asp:BoundColumn>
						<asp:BoundColumn DataField="fecha_ult_compra" SortExpression="fecha_ult_compra desc" ReadOnly="True"
							HeaderText="Ult. Compra">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="vta_ano_act" SortExpression="vta_ano_act desc" ReadOnly="True" HeaderText="Vta. A&#241;o">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="val_mes_uno" SortExpression="val_mes_uno desc" ReadOnly="True" HeaderText="Vta. Mes-1">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="val_mes_dos" SortExpression="val_mes_dos desc" ReadOnly="True" HeaderText="Vta. Mes-2">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="val_mes_tres" SortExpression="val_mes_tres desc" ReadOnly="True" HeaderText="Vta. Mes-3">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="val_mes_cuatro" SortExpression="val_mes_cuatro desc" ReadOnly="True"
							HeaderText="Vta. Mes-4">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="prm_ano_ant" SortExpression="prm_ano_ant desc" ReadOnly="True" HeaderText="Prom. A&#241;o Ant.">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
					</Columns>
				</asp:DataGrid></TD>
		</TR>
	</TABLE>
</wilson:masterpage>
