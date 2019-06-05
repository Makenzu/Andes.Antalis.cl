<%@ Page Language="vb" AutoEventWireup="false" Codebehind="analisis_precios_cartera_promotora.aspx.vb" Inherits="app.analisis_venta_productos_cartera" %>
<%@ Register TagPrefix="Wilson" Namespace="Wilson.MasterPages" Assembly="WilsonMasterPages" %>
<META content="text/html; charset=iso-8859-1" http-equiv="Content-Type">
<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
<wilson:masterpage id="MPContainer" runat="server">
	<WILSON:CONTENTREGION id="MPTitle" runat="server">Análisis de 
precios cartera&nbsp;ejec. comercial&nbsp;por subfamilia</WILSON:CONTENTREGION>
	<WILSON:CONTENTREGION id="MPCaption" runat="server">Análisis de precios 
cartera&nbsp;ejec comercial&nbsp;por subfamilia</WILSON:CONTENTREGION>
	<TABLE id="Table1" border="0" cellSpacing="0" cellPadding="0" width="95%" align="center">
		<TR>
			<TD width="70%">
				<asp:Label id="lbErrors" runat="server" CssClass="txt-FatalMessage"></asp:Label></TD>
			<TD width="30%" align="right">
				<TABLE id="Table6" border="0" cellSpacing="0" cellPadding="0">
					<TR>
						<TD>
							<asp:ImageButton id="ibExportar" title="Exportar a Excel" runat="server" EnableViewState="False"
								Visible="False" ImageUrl="/images/exportar.gif" ToolTip="Exportar a Excel"></asp:ImageButton></TD>
						<TD width="5"></TD>
						<TD><INPUT title="Imprimir" onclick="javascript:imprimir();return false;" alt="Imprimir" src="/images/imprimir.gif"
								type="image"></TD>
					</TR>
				</TABLE>
				<A href="javascript: imprimir();"></A>
			</TD>
		</TR>
	</TABLE>
	<TABLE id="Table2" border="0" cellSpacing="0" cellPadding="1" width="95%" align="center">
		<TR>
			<TD vAlign="bottom" width="45%" colSpan="2">
				<TABLE id="Table3" border="0" cellSpacing="1" cellPadding="1">
					<TR>
						<TD class="tbl-HorizHeader">Ejec. Comercial:</TD>
						<TD class="tbl-HorizHeader">
							<asp:DropDownList id="ddlEjecutivaComercial" runat="server" CssClass="listBox"></asp:DropDownList></TD>
						<TD class="tbl-HorizHeader"></TD>
					</TR>
					<TR>
						<TD class="tbl-HorizHeader" vAlign="top">Período:</TD>
						<TD style="FONT-FAMILY: Arial; COLOR: dimgray; FONT-SIZE: 9px">
							<P>
								<asp:TextBox id="tbFechaInicio" runat="server" MaxLength="10"></asp:TextBox>&nbsp;al 
								&nbsp;
								<asp:TextBox id="tbFechaTermino" runat="server" MaxLength="10"></asp:TextBox><BR>
								Ejemplo: 01/01/2009 al 31/03/2009<BR>
							</P>
						</TD>
						<TD style="FONT-FAMILY: Arial; COLOR: dimgray; FONT-SIZE: 9px"></TD>
					</TR>
					<TR>
						<TD class="tbl-HorizHeader">Jerarquía:</TD>
						<TD>
							<asp:DropDownList id="ddlArea" runat="server" CssClass="listBox" AutoPostBack="True"></asp:DropDownList>-
							<asp:DropDownList id="ddlFamilia" runat="server" CssClass="listBox" AutoPostBack="True"></asp:DropDownList>-
							<asp:DropDownList id="ddlSubfamilia" runat="server" CssClass="listBox"></asp:DropDownList></TD>
						<TD>
							<asp:Button id="bConsultar" runat="server" Text="Consultar"></asp:Button></TD>
					</TR>
				</TABLE>
			</TD>
		</TR>
		<TR>
			<TD height="25" colSpan="2" align="left">
				<asp:Label id="lbNota" runat="server" CssClass="txt-SoftMessage" Visible="False"></asp:Label></TD>
		</TR>
		<TR>
			<TD bgColor="#a3bad6" height="25" colSpan="2" align="center">
				<asp:Label id="lTituloInforme" runat="server" CssClass="tbl-HorizItem" Font-Bold="True"></asp:Label></TD>
		</TR>
		<TR>
			<TD colSpan="2">
				<asp:DataGrid id="dgResultado" runat="server" Width="100%" CellPadding="3" AutoGenerateColumns="False"
					AllowSorting="True" ShowFooter="True">
					<FooterStyle HorizontalAlign="Right" CssClass="tbl-DataGridFooter"></FooterStyle>
					<AlternatingItemStyle CssClass="tbl-DataGridItemAlternating"></AlternatingItemStyle>
					<ItemStyle CssClass="tbl-DataGridItem"></ItemStyle>
					<HeaderStyle CssClass="tbl-DataGridHeader"></HeaderStyle>
					<Columns>
						<asp:BoundColumn DataField="cod_cliente" SortExpression="cod_cliente DESC" HeaderText="Cod.&lt;br&gt;Cliente"></asp:BoundColumn>
						<asp:BoundColumn DataField="nom_cliente" SortExpression="nom_cliente DESC" HeaderText="Raz&#243;n Social"></asp:BoundColumn>
						<asp:BoundColumn DataField="cod_producto" SortExpression="cod_producto DESC" HeaderText="Codigo&lt;br&gt;Producto"></asp:BoundColumn>
						<asp:BoundColumn DataField="des_producto" SortExpression="des_producto DESC" HeaderText="Denominaci&#243;n producto"></asp:BoundColumn>
						<asp:BoundColumn DataField="vta_dolar_acumulada" SortExpression="vta_dolar_acumulada DESC" HeaderText="Vta&lt;br&gt;USD&lt;br&gt;Acum"
							DataFormatString="{0:#,##0.0000}">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="mg_dolar_acumulado" SortExpression="mg_dolar_acumulado DESC" HeaderText="Mg&lt;br&gt;USD&lt;br&gt;Acum"
							DataFormatString="{0:#,##0.0000}">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="pje_mg_acumulado" SortExpression="pje_mg_acum DESC" HeaderText="%Mg&lt;br&gt;Acum"
							DataFormatString="{0:#,##0.0%}">
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="precio_dolar_promedio" SortExpression="precio_dolar_promedio DESC" HeaderText="Precio&lt;br&gt;USD&lt;br&gt;Prom"
							DataFormatString="{0:#,##0.0000}">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="precio_dolar_maximo" SortExpression="precio_dolar_maximo DESC" HeaderText="Precio&lt;br&gt;USD&lt;br&gt;Max"
							DataFormatString="{0:#,##0.0000}">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="precio_dolar_minimo" SortExpression="precio_dolar_minimo DESC" HeaderText="Precio&lt;br&gt;USD&lt;br&gt;Min"
							DataFormatString="{0:#,##0.0000}">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="precio_lista_dolar_promedio" SortExpression="precio_lista_dolar_promedio DESC"
							HeaderText="Prec Lista&lt;br&gt;USD&lt;br&gt;Prom" DataFormatString="{0:#,##0.0000}">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="pje_dcto" SortExpression="pje_dcto DESC" HeaderText="% Dcto" DataFormatString="{0:#,##0.0%}">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
					</Columns>
				</asp:DataGrid>
				<P>
					<asp:Label id="lNoCoincidencias" runat="server"></asp:Label></P>
			</TD>
		</TR>
	</TABLE>
</wilson:masterpage>
