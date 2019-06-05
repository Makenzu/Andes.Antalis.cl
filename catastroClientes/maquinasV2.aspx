<%@ Register TagPrefix="Wilson" Namespace="Wilson.MasterPages" Assembly="WilsonMasterPages" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="maquinasV2.aspx.vb" Inherits="app.maquinasV2"%>
<META content="text/html; charset=iso-8859-1" http-equiv="Content-Type">
<wilson:masterpage style="Z-INDEX: 0" id="MPContainer" runat="server">
	<WILSON:CONTENTREGION id="MPTitle" runat="server">Catastro 
Clientes</WILSON:CONTENTREGION>
	<WILSON:CONTENTREGION id="MPCaption" runat="server">Máquinas</WILSON:CONTENTREGION>
	<asp:Panel id="Panel1" runat="server">[<A href="manMaquinas.aspx">Adm. máquinas</A>] [<A href="manMedidasPlanchas.aspx">Adm. 
			medidas de planchas</A>] [<A href="manMedidasMantillas.aspx">Adm. medidas de 
			mantillas</A>] </asp:Panel>
	<DIV class="texto-normal">
		<TABLE id="Table1" border="0" cellSpacing="0" cellPadding="0" width="300">
			<TR>
				<TD height="28" colSpan="3"></TD>
			</TR>
			<TR>
				<TD>
					<asp:LinkButton id="lbPorCliente" runat="server" CssClass="maquinas-vinculo-t1">Por cliente</asp:LinkButton></TD>
				<TD>
					<asp:LinkButton id="lbPorEjecutivo" runat="server" CssClass="maquinas-vinculo-t1">Por Ejecutivo</asp:LinkButton></TD>
				<TD>
					<asp:LinkButton id="lbPorMaquina" runat="server" CssClass="maquinas-vinculo-t1">Por máquina</asp:LinkButton></TD>
			</TR>
			<TR>
				<TD height="28" colSpan="3"></TD>
			</TR>
		</TABLE>
	</DIV>
	<asp:Panel id="pPorCliente" runat="server">
		<TABLE id="Table2" border="0" cellSpacing="0" cellPadding="0" align="center">
			<TR>
				<TD class="nombre-campo">Digite código de cliente GMS:</TD>
				<TD>
					<asp:TextBox id="tbCodigoCliente" runat="server"></asp:TextBox></TD>
				<TD>
					<asp:Button id="bBuscarPorCodigoCliente" runat="server" Text="buscar"></asp:Button></TD>
			</TR>
			<TR>
				<TD class="nombre-campo" height="18" colSpan="3"></TD>
			</TR>
		</TABLE>
		<asp:Panel id="pResultadoPorCodigo" runat="server">
			<DIV class="maquinas-titulo1">
				<asp:Label id="lRazonSocial" runat="server"></asp:Label></DIV>
			<DIV class="maquinas-titulo2">
				<asp:Label id="lEjecutivaComercial" runat="server"></asp:Label></DIV>
			<asp:DataGrid style="Z-INDEX: 0" id="dgMaquinas" runat="server" AutoGenerateColumns="False" CellPadding="2">
				<AlternatingItemStyle CssClass="tbl-DataGridItemAlternating"></AlternatingItemStyle>
				<ItemStyle CssClass="tbl-DataGridItem"></ItemStyle>
				<HeaderStyle CssClass="tbl-DataGridHeader"></HeaderStyle>
				<Columns>
					<asp:BoundColumn DataField="id_registro" HeaderText="ID Registro">
						<HeaderStyle HorizontalAlign="Center" Width="49px"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn Visible="False" DataField="id_maquina" HeaderText="id_maquina">
						<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="dmc_maquina" HeaderText="M&#225;quina">
						<HeaderStyle Width="190px"></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="cant_maquinas" HeaderText="Cant. M&#225;quinas">
						<HeaderStyle HorizontalAlign="Right" Width="58px"></HeaderStyle>
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn Visible="False" DataField="id_tamanho_maquina" HeaderText="id_tamanho_maquina"></asp:BoundColumn>
					<asp:BoundColumn DataField="dmc_tamanho_maquina" HeaderText="Medida&lt;br&gt;M&#225;quina">
						<HeaderStyle HorizontalAlign="Center" Width="117px"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<FooterStyle HorizontalAlign="Center"></FooterStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="num_cuerpos_impresores" HeaderText="N&#176;&lt;br&gt;Cuerpos&lt;br&gt;Impresores">
						<HeaderStyle HorizontalAlign="Right" Width="72px"></HeaderStyle>
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="con_torre_barniz" HeaderText="Torre&lt;br&gt;Barniz">
						<HeaderStyle HorizontalAlign="Center" Width="70px"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn Visible="False" DataField="id_tamanho_plancha" HeaderText="id_tamanho_plancha"></asp:BoundColumn>
					<asp:BoundColumn DataField="dmc_tamanho_plancha" HeaderText="Medida&lt;br&gt;Planchas">
						<HeaderStyle HorizontalAlign="Right" Width="80px"></HeaderStyle>
						<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="dmc_tamanho_mantilla" HeaderText="Medida&lt;br&gt;Mantilla">
						<HeaderStyle HorizontalAlign="Right" Width="80px"></HeaderStyle>
						<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="consumo_papel" HeaderText="Cons. Papel [Ton/mes]" DataFormatString="{0:#,##0.0}">
						<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="consumo_planchas" HeaderText="Cons. Planchas [Ues/mes]" DataFormatString="{0:#,##0}">
						<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="consumo_tintas_proceso" HeaderText="Cons. Tintas Proceso [Kg/mes]" DataFormatString="{0:#,##0.0}">
						<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="consumo_tintas_pantone" HeaderText="Cons. Tintas Pantone [Kg/mes]" DataFormatString="{0:#,##0.0}">
						<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="consumo_barniz" HeaderText="Consumo Barniz [Kg/mes]" DataFormatString="{0:#,##0.0}">
						<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="consumo_mantillas" HeaderText="Consumo Mantillas [Ues/mes]" DataFormatString="{0:#,##0}">
						<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
					</asp:BoundColumn>
					<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="Update" CancelText="Cancel" EditText="editar">
						<HeaderStyle Width="42px"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
					</asp:EditCommandColumn>
					<asp:ButtonColumn Text="borrar" CommandName="Delete">
						<HeaderStyle Width="42px"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
					</asp:ButtonColumn>
				</Columns>
			</asp:DataGrid>
			<DIV class="maquinas-conteo-registros">
				<asp:Label id="lTotalOcurrenciasPorCodigo" runat="server"></asp:Label></DIV>
			<DIV class="maquinas-conteo-registros">
				<asp:HyperLink id="hlNuevoRegistroMaquina" CssClass="maquinas-vinculo-t1" runat="server" NavigateUrl="#">Ingresar un nuevo registro de máquina para el cliente</asp:HyperLink></DIV>
		</asp:Panel>
	</asp:Panel>
	<asp:Panel style="Z-INDEX: 0" id="pPorEjecutivo" runat="server">
		<TABLE style="Z-INDEX: 0" id="Table3" border="0" cellSpacing="0" cellPadding="0" align="center">
			<TR>
				<TD class="nombre-campo">Seleccione ejecutivo comercial:</TD>
				<TD>
					<asp:DropDownList id="ddlEjecutivo" runat="server"></asp:DropDownList></TD>
				<TD>
					<asp:Button id="bBuscarPorEjecutivo" runat="server" Text="buscar"></asp:Button></TD>
			</TR>
			<TR>
				<TD class="nombre-campo" height="18" colSpan="3"></TD>
			</TR>
		</TABLE>
		<asp:Panel id="pResultadoPorEjecutivoComercial" runat="server">
			<asp:DataGrid style="Z-INDEX: 0" id="dgMaquinasCartera" runat="server" AutoGenerateColumns="False"
				CellPadding="3" GridLines="Vertical" BorderColor="DimGray" BorderStyle="Solid" BorderWidth="1px"
				HorizontalAlign="Center">
				<AlternatingItemStyle CssClass="tbl-DataGridItemAlternating"></AlternatingItemStyle>
				<ItemStyle CssClass="tbl-DataGridItem"></ItemStyle>
				<HeaderStyle CssClass="maquinas-cabecera-datagrid"></HeaderStyle>
				<Columns>
					<asp:BoundColumn DataField="cod_cliente" HeaderText="Cod.Cliente"></asp:BoundColumn>
					<asp:HyperLinkColumn DataNavigateUrlField="cod_cliente" DataNavigateUrlFormatString="maquinasV2.aspx?vista=xc&amp;cc={0}"
						DataTextField="nom_cliente" HeaderText="Raz&#243;n social"></asp:HyperLinkColumn>
					<asp:BoundColumn DataField="dmc_maquina" HeaderText="M&#225;quina"></asp:BoundColumn>
					<asp:BoundColumn DataField="cant_maquinas" HeaderText="Cantidad">
						<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
					</asp:BoundColumn>
				</Columns>
			</asp:DataGrid>
			<DIV class="maquinas-conteo-registros">
				<asp:Label style="Z-INDEX: 0" id="lTotalOcurrenciasPorEjecutivo" runat="server"></asp:Label></DIV>
		</asp:Panel>
	</asp:Panel>
	<asp:Panel id="pPorMaquina" runat="server">
		<TABLE style="Z-INDEX: 0" id="Table4" border="0" cellSpacing="0" cellPadding="0" align="center">
			<TR>
				<TD class="nombre-campo">Seleccione máquina:</TD>
				<TD></TD>
				<TD>
					<asp:DropDownList id="ddlMaquina" runat="server"></asp:DropDownList></TD>
				<TD>
					<asp:Button id="bBuscarPorMaquina" runat="server" Text="buscar"></asp:Button></TD>
			</TR>
			<TR>
				<TD class="nombre-campo" height="18" colSpan="4"></TD>
			</TR>
		</TABLE>
	</asp:Panel>
	<asp:Panel id="pResultadoPorMaquina" runat="server">
		<asp:DataGrid style="Z-INDEX: 0" id="dgClientesMaquina" runat="server" AutoGenerateColumns="False"
			CellPadding="3" GridLines="Vertical" BorderColor="DimGray" BorderStyle="Solid" BorderWidth="1px"
			HorizontalAlign="Center">
			<AlternatingItemStyle CssClass="maquinas-dato-datagrid-alternado"></AlternatingItemStyle>
			<ItemStyle CssClass="maquinas-dato-datagrid"></ItemStyle>
			<HeaderStyle CssClass="maquinas-cabecera-datagrid"></HeaderStyle>
			<Columns>
				<asp:BoundColumn DataField="cod_cliente" HeaderText="Cod.Cliente"></asp:BoundColumn>
				<asp:HyperLinkColumn DataNavigateUrlField="cod_cliente" DataNavigateUrlFormatString="maquinasV2.aspx?vista=xc&amp;cc={0}"
					DataTextField="nom_cliente" HeaderText="Raz&#243;n social"></asp:HyperLinkColumn>
				<asp:BoundColumn DataField="nom_promotora" HeaderText="Ejec. Comercial"></asp:BoundColumn>
				<asp:BoundColumn DataField="cant_maquinas" HeaderText="Cantidad">
					<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
					<ItemStyle HorizontalAlign="Right"></ItemStyle>
				</asp:BoundColumn>
			</Columns>
		</asp:DataGrid>
		<DIV class="maquinas-conteo-registros">
			<asp:Label style="Z-INDEX: 0" id="lTotalOcurrenciasPorMaquina" runat="server"></asp:Label></DIV>
	</asp:Panel>
	<INPUT id="hfSeleccionVista" type="hidden" name="Hidden1" runat="server">
</wilson:masterpage>
