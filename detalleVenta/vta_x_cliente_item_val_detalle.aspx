<%@ Register TagPrefix="Wilson" Namespace="Wilson.MasterPages" Assembly="WilsonMasterPages" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="vta_x_cliente_item_val_detalle.aspx.vb" Inherits="app.vta_x_cliente_item_val_detalle"%>
<META content="text/html; charset=iso-8859-1" http-equiv="Content-Type">
<LINK href="http://localhost/css/andes.css" type="text/css" rel="stylesheet">
<meta content="False" name="vs_snapToGrid">
<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
<wilson:masterpage id="MPContainer" runat="server">
	<WILSON:CONTENTREGION id="MPTitle" runat="server">Ventas por 
Cliente - Item Valor Detalle</WILSON:CONTENTREGION>
	<WILSON:CONTENTREGION id="MPCaption" runat="server">Ventas por Cliente - Item Valor Detalle<BR>
<asp:Label id="lb_Fecha" runat="server"></asp:Label></WILSON:CONTENTREGION>
	<SCRIPT language="javascript">
	</SCRIPT>
	<TABLE id="Table1" border="0" cellSpacing="0" cellPadding="0" width="95%" align="center">
		<TR>
			<TD style="WIDTH: 452px">
				<P>
					<asp:Label id="lbErrores" runat="server" EnableViewState="False" CssClass="txt-FatalMessage"></asp:Label></P>
			</TD>
			<TD width="30%" align="right"><INPUT title="Imprimir" onclick="javascript:imprimir();return false;" alt="Imprimir" src="/images/imprimir.gif"
					type="image">&nbsp;
				<asp:ImageButton id="ibExportar" title="Exportar a Excel" runat="server" EnableViewState="False"
					ImageUrl="/images/exportar.gif" Visible="False" ToolTip="Exportar a Excel"></asp:ImageButton></TD>
		</TR>
	</TABLE>
	<TABLE id="Table2" border="0" cellSpacing="0" cellPadding="0" width="98%" align="center">
		<TR>
			<TD style="HEIGHT: 29px" height="29" colSpan="3" align="center">
				<TABLE id="Table3" border="0" cellSpacing="0" cellPadding="0" width="100%">
					<TR>
						<TD>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
							<asp:Label id="lb_nom_cliente" runat="server" CssClass="tbl-HorizHeader"></asp:Label></TD>
					</TR>
				</TABLE>
			</TD>
		</TR>
		<TR>
			<TD bgColor="#a3bad6" height="25" width="60%" colSpan="3" align="center">
				<asp:Label id="lb_producto" runat="server" CssClass="tbl-HorizItem" Font-Bold="True"></asp:Label></TD>
		</TR>
		<TR>
			<TD height="15" colSpan="3" align="center"></TD>
		</TR>
		<TR>
			<TD colSpan="3" align="center">
				<asp:DataGrid id="dgdetalle" runat="server" AutoGenerateColumns="False" CellPadding="2" AllowSorting="True"
					ShowFooter="True">
					<FooterStyle CssClass="tbl-DataGridFooter"></FooterStyle>
					<AlternatingItemStyle CssClass="tbl-DataGridItemAlternating-2"></AlternatingItemStyle>
					<ItemStyle CssClass="tbl-DataGridItem-2"></ItemStyle>
					<HeaderStyle CssClass="tbl-DataGridHeader-2"></HeaderStyle>
					<Columns>
						<asp:BoundColumn DataField="cod_sucursal" SortExpression="cod_sucursal" HeaderText="Suc"></asp:BoundColumn>
						<asp:BoundColumn DataField="cod_documento" SortExpression="cod_documento" HeaderText="Docto"></asp:BoundColumn>
						<asp:BoundColumn DataField="Num_documento" SortExpression="Num_documento" HeaderText="Numero"></asp:BoundColumn>
						<asp:BoundColumn DataField="fec_documento" SortExpression="fec_documento" ReadOnly="True" HeaderText="Fecha"
							DataFormatString="{0:dd/MMM/yyyy}">
							<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="num_docto_sap" SortExpression="num_docto_sap" ReadOnly="True" HeaderText="Num Docto Sap">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="cantidad" SortExpression="cantidad" HeaderText="Cantidad">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="cod_um" SortExpression="cod_um" HeaderText="UMed">
							<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
							<FooterStyle HorizontalAlign="Center"></FooterStyle>
						</asp:BoundColumn>
					</Columns>
				</asp:DataGrid></TD>
		</TR>
	</TABLE>
</wilson:masterpage>
