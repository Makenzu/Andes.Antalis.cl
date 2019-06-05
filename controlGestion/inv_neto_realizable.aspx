<%@ Page Language="vb" AutoEventWireup="false" Codebehind="inv_neto_realizable.aspx.vb" Inherits="app.inv_neto_realizable"%>
<%@ Register TagPrefix="Wilson" Namespace="Wilson.MasterPages" Assembly="WilsonMasterPages" %>
<META content="text/html; charset=iso-8859-1" http-equiv="Content-Type">
<wilson:masterpage id="MPContainer" runat="server">
	<WILSON:CONTENTREGION id="MPTitle" runat="server">ANDES</WILSON:CONTENTREGION>
	<WILSON:CONTENTREGION id="MPCaption" runat="server">Inventario Neto Realizable<BR>
<asp:Label id="lbFecha" runat="server"></asp:Label></WILSON:CONTENTREGION>
	<asp:Label id="lbErrors" runat="server" CssClass="txt-FatalMessage"></asp:Label>
	<TABLE id="Table2" border="0" cellSpacing="0" cellPadding="0" width="100%">
		<TR>
			<TD align="right">
				<asp:ImageButton id="ibExportar" title="Exportar a Excel" runat="server" ToolTip="Exportar a Excel"
					EnableViewState="False" Visible="False" ImageUrl="/images/exportar.gif"></asp:ImageButton></TD>
		</TR>
	</TABLE>
	<TABLE id="Table1" border="0" cellSpacing="0" cellPadding="3" align="center">
		<TR>
			<TD align="right">
				<TABLE id="Table5" border="0" cellSpacing="0" cellPadding="2" width="350" align="center">
					<TR>
						<TD class="tbl-HorizHeader" width="70">Periodo:</TD>
						<TD width="200" colSpan="2">
							<asp:DropDownList id="ddlMes" runat="server" CssClass="listBox"></asp:DropDownList>&nbsp;/
							<asp:DropDownList id="ddlAno" runat="server" CssClass="listBox"></asp:DropDownList></TD>
						<TD width="70">
							<asp:ImageButton id="btSend" runat="server" ImageUrl="/images/procesar.gif"></asp:ImageButton>
							<DIV style="DISPLAY: none" id="disbtn" name="disbtn"><IMG alt="" src="/images/procesando.gif"></DIV>
						</TD>
					</TR>
				</TABLE>
			</TD>
		</TR>
		<TR>
			<TD align="right">
				<asp:Label id="lbNota" runat="server" CssClass="txt-SoftMessage" Visible="False"></asp:Label></TD>
		</TR>
		<TR>
			<TD>
				<TABLE id="Total" border="1" cellSpacing="0" cellPadding="2" width="100%">
					<TR>
						<TD class="tbl-FechaHeader">Inventario General Neto Reutilizable</TD>
						<TD class="tbl-FechaHeader" align="right">Inventario Min</TD>
						<TD class="tbl-FechaHeader" align="right">Inventario Prom</TD>
					</TR>
					<TR>
						<TD class="tbl-DataGridFooter"></TD>
						<TD id="TotalGralInvMin" class="tbl-DataGridFooter" align="right" runat="server"></TD>
						<TD id="TotalGralInvAvg" class="tbl-DataGridFooter" align="right" runat="server"></TD>
					</TR>
				</TABLE>
			</TD>
		<TR height="10" bgColor="black">
			<TD></TD>
		</TR>
		<TR>
			<TD class="tbl-FechaHeader" align="center">Inventario para Productos Comprados Año 
				Actual</TD>
		</TR>
		<TR>
			<TD>
				<asp:DataGrid id="grdComprados" runat="server" AutoGenerateColumns="False" width="100%" ShowFooter="True">
					<AlternatingItemStyle CssClass="tbl-DataGridItemAlternating"></AlternatingItemStyle>
					<ItemStyle CssClass="tbl-DataGridItem"></ItemStyle>
					<HeaderStyle CssClass="tbl-DataGridHeader"></HeaderStyle>
					<FooterStyle CssClass="tbl-DataGridFooter"></FooterStyle>
					<Columns>
						<asp:BoundColumn DataField="cod_area" HeaderText="Area"></asp:BoundColumn>
						<asp:BoundColumn DataField="cod_familia" HeaderText="Familia"></asp:BoundColumn>
						<asp:BoundColumn DataField="cod_subfamilia" HeaderText="Subfamilia"></asp:BoundColumn>
						<asp:BoundColumn DataField="cod_producto" HeaderText="CodProd"></asp:BoundColumn>
						<asp:BoundColumn DataField="des_producto" HeaderText="Producto"></asp:BoundColumn>
						<asp:BoundColumn DataField="val_stock_actual" HeaderText="Stock Actual" DataFormatString="{0:#,##0}">
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="val_costo_gestion" HeaderText="Costo Gestion" DataFormatString="{0:#,##0.0000}">
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="val_mg_avg_area" HeaderText="Mg Prom Area" DataFormatString="{0:#,##0.0000}">
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="val_precio_avg" HeaderText="Precio Prom" DataFormatString="{0:#,##0.0000}">
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="val_inventario_min" HeaderText="Inventario Min" DataFormatString="{0:#,##0.00}">
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="val_inventario_avg" HeaderText="Inventario Prom" DataFormatString="{0:#,##0.00}">
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
					</Columns>
				</asp:DataGrid></TD>
		</TR>
		<TR height="10" bgColor="black">
			<TD></TD>
		</TR>
		<TR>
			<TD class="tbl-FechaHeader" align="center">Inventario para Productos NO Comprados 
				Año Actual</TD>
		</TR>
		<TR>
			<TD>
				<asp:DataGrid id="grdNoComprados" runat="server" AutoGenerateColumns="False" width="100%" ShowFooter="True">
					<FooterStyle CssClass="tbl-DataGridFooter"></FooterStyle>
					<AlternatingItemStyle CssClass="tbl-DataGridItemAlternating"></AlternatingItemStyle>
					<ItemStyle CssClass="tbl-DataGridItem"></ItemStyle>
					<HeaderStyle CssClass="tbl-DataGridHeader"></HeaderStyle>
					<Columns>
						<asp:BoundColumn DataField="cod_area" HeaderText="Area"></asp:BoundColumn>
						<asp:BoundColumn DataField="cod_familia" HeaderText="Familia"></asp:BoundColumn>
						<asp:BoundColumn DataField="cod_subfamilia" HeaderText="Subfamilia"></asp:BoundColumn>
						<asp:BoundColumn DataField="cod_producto" HeaderText="CodProd"></asp:BoundColumn>
						<asp:BoundColumn DataField="des_producto" HeaderText="Producto"></asp:BoundColumn>
						<asp:BoundColumn DataField="val_stock_actual" HeaderText="Stock Actual" DataFormatString="{0:#,##0}">
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="val_costo_gestion" HeaderText="Costo Gestion" DataFormatString="{0:#,##0.0000}">
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="val_mg_avg_area" HeaderText="Mg Prom Area" DataFormatString="{0:#,##0.0000}">
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="val_precio_avg" HeaderText="Precio Prom" DataFormatString="{0:#,##0.0000}">
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="val_inventario_min" HeaderText="Inventario Min" DataFormatString="{0:#,##0.00}">
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="val_inventario_avg" HeaderText="Inventario Prom" DataFormatString="{0:#,##0.00}">
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
					</Columns>
				</asp:DataGrid></TD>
		</TR>
	</TABLE>
</wilson:masterpage>
