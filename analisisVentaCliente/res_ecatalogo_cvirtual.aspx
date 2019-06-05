<%@ Register TagPrefix="Wilson" Namespace="Wilson.MasterPages" Assembly="WilsonMasterPages" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="res_ecatalogo_cvirtual.aspx.vb" Inherits="app.res_ecatalogo_vvirtual" %>
<META content="text/html; charset=iso-8859-1" http-equiv="Content-Type">
<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
<wilson:masterpage id="MPContainer" runat="server">
	<WILSON:CONTENTREGION id="MPTitle" runat="server">Resumen 
e-Catalogo cartera virtual.</WILSON:CONTENTREGION>
	<WILSON:CONTENTREGION id="MPCaption" runat="server">Resumen eCatalogo&nbsp;cartera virtual&nbsp; 
<asp:Label id="lbFecha" runat="server"></asp:Label></WILSON:CONTENTREGION>
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
			<TD vAlign="bottom" width="45%">
				<TABLE id="Table5" border="0" cellSpacing="0" cellPadding="2">
					<TR>
						<TD class="tbl-HorizHeader" width="15"></TD>
						<TD style="WIDTH: 46px" class="tbl-HorizHeader" width="46">Vendedor:</TD>
						<TD colSpan="2">
							<asp:DropDownList id="ddlVendedora" runat="server" CssClass="listBox"></asp:DropDownList></TD>
						<TD></TD>
					</TR>
					<TR>
						<TD class="tbl-HorizHeader" width="15"></TD>
						<TD style="WIDTH: 46px" class="tbl-HorizHeader" width="46">Periodo:</TD>
						<TD>
							<asp:TextBox id="txtIni" runat="server" Columns="12" MaxLength="10"></asp:TextBox>/
							<asp:TextBox id="txtFin" runat="server" Columns="12" MaxLength="10"></asp:TextBox></TD>
						<TD><INPUT id="btSend" onclick="javascript: Validar();return false;" src="/images/procesar.gif"
								type="image" name="btSend" runat="server">
							<DIV style="DISPLAY: none" id="disbtn" name="disbtn"><IMG alt="" src="/images/procesando.gif"></DIV>
						</TD>
					</TR>
					<TR>
						<TD></TD>
						<TD class="tbl-HorizHeader" colSpan="2">&nbsp;
							<asp:CheckBox id="chkEcat" runat="server" Text="Sólo Clientes e-Catalogo" TextAlign="Left"></asp:CheckBox></TD>
						<TD></TD>
						<TD></TD>
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
				<asp:Label id="lbCodPromo" runat="server" CssClass="tbl-HorizItem" Font-Bold="True"></asp:Label>
				<asp:Label id="lbNomPromo" runat="server" CssClass="tbl-HorizItem" Font-Bold="True"></asp:Label></TD>
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
						<asp:BoundColumn Visible="False" DataField="cod_vend_virtual" SortExpression="cod_vend_virtual ASC"
							HeaderText="COD.&lt;BR&gt;VEND."></asp:BoundColumn>
						<asp:BoundColumn Visible="False" DataField="nom_vendedora" SortExpression="nom_vendedora ASC" HeaderText="VENDEDORA"></asp:BoundColumn>
						<asp:BoundColumn DataField="Cod_Cliente" SortExpression="Cod_Cliente ASC" HeaderText="C&#243;d.&lt;BR&gt;Cliente"></asp:BoundColumn>
						<asp:BoundColumn DataField="Nom_Cliente" SortExpression="Nom_Cliente ASC" HeaderText="Raz&#243;n Social"></asp:BoundColumn>
						<asp:BoundColumn DataField="fec_Ingreso" SortExpression="fec_Ingreso ASC" HeaderText="Fecha Activaci&#243;n&lt;BR&gt;e-Cat&#225;logo"
							DataFormatString="{0:dd/MM/yyyy}">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="GUSD_eCat" SortExpression="GUSD_eCat ASC" HeaderText="Vta. D&#243;lar&lt;BR&gt;e-Cat&#225;logo"
							DataFormatString="{0:##,###,##0.00}">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="GUSD_Total" SortExpression="GUSD_Total ASC" HeaderText="Vta. D&#243;lar&lt;BR&gt;GMS"
							DataFormatString="{0:##,###,##0.00}">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="Pct_Vta_eCat" SortExpression="Pct_Vta_eCat ASC" HeaderText="% Vta.&lt;BR&gt;e-Cat&#225;logo"
							DataFormatString="{0:##0.00%}">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="mg_gusd_ecat" SortExpression="mg_gusd_ecat ASC" HeaderText="Mg D&#243;lar&lt;BR&gt;e-Cat&#225;logo"
							DataFormatString="{0:##,###,##0.00}">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="mg_gusd_total" SortExpression="mg_gusd_total ASC" HeaderText="Mg D&#243;lar&lt;BR&gt;GMS"
							DataFormatString="{0:##,###,##0.00}">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="Pct_mg_ecat" SortExpression="Pct_mg_ecat ASC" HeaderText="% Mg&lt;BR&gt;e-Cat&#225;logo"
							DataFormatString="{0:##0.00%}">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="Num_Ped_eCat" SortExpression="Num_Ped_eCat ASC" HeaderText="# Pedidos&lt;BR&gt;e-Cat&#225;logo"
							DataFormatString="{0:#,##0}">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="Num_Ped_Total" SortExpression="Num_Ped_Total ASC" HeaderText="# Pedidos&lt;BR&gt;GMS"
							DataFormatString="{0:#,##0}">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="Pct_Ped_eCat" SortExpression="Pct_Ped_eCat ASC" HeaderText="% Pedidos&lt;BR&gt;e-Cat&#225;logo"
							DataFormatString="{0:##0.00%}">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="Saldo_ePesos" SortExpression="Saldo_ePesos ASC" HeaderText="Saldo&lt;BR&gt;e-Pesos"
							DataFormatString="{0:##,###,##0}">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="ePesos_Utilizados" SortExpression="ePesos_Utilizados ASC" HeaderText="e-Pesos&lt;BR&gt;Utilizados"
							DataFormatString="{0:##,###,##0}">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="Freq_eCat" SortExpression="Freq_eCat ASC" HeaderText="Frec. Uso&lt;BR&gt;e-Cat&#225;logo"
							DataFormatString="{0:##0.00%}">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn Visible="False" DataField="Es_eCat" HeaderText="Cli. eCat."></asp:BoundColumn>
						<asp:BoundColumn DataField="cod_promotora" SortExpression="cod_promotora ASC" HeaderText="Promo">
							<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
							<FooterStyle HorizontalAlign="Center"></FooterStyle>
						</asp:BoundColumn>
					</Columns>
				</asp:DataGrid>
				<P class="tbl-HorizHeader">Total Clientes Cartera :
					<asp:Label id="lblCartera" runat="server">0</asp:Label><BR>
					Total Clientes e-Catálogo :
					<asp:Label id="lblEcatalogo" runat="server">0</asp:Label></P>
				<P>&nbsp;</P>
			</TD>
		</TR>
	</TABLE>
</wilson:masterpage>
