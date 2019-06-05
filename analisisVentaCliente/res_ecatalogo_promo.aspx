<%@ Register TagPrefix="Wilson" Namespace="Wilson.MasterPages" Assembly="WilsonMasterPages" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="res_ecatalogo_promo.aspx.vb" Inherits="app.res_ecatalogo_promo" %>
<META content="text/html; charset=iso-8859-1" http-equiv="Content-Type">
<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
<wilson:masterpage id="MPContainer" runat="server">
	<WILSON:CONTENTREGION id="MPTitle" runat="server">Resumen 
e-Catalogo por Ejec. Comercial</WILSON:CONTENTREGION>
	<WILSON:CONTENTREGION id="MPCaption" runat="server">Resumen eCatalogo 
por&nbsp;Ejec.&nbsp;Comercial&nbsp; 
<asp:Label id="lbFecha" runat="server"></asp:Label></WILSON:CONTENTREGION>
	<TABLE id="Table1" border="0" cellSpacing="0" cellPadding="0" width="95%" align="center">
		<TR>
			<TD width="70%">
				<asp:Label id="lbErrors" runat="server" CssClass="txt-FatalMessage"></asp:Label></TD>
			<TD width="30%" align="right">
				<TABLE id="Table6" border="0" cellSpacing="0" cellPadding="0">
					<TR>
						<TD>
							<asp:ImageButton id="ibExportar" title="Exportar a Excel" runat="server" ToolTip="Exportar a Excel"
								ImageUrl="/images/exportar.gif" Visible="False" EnableViewState="False"></asp:ImageButton></TD>
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
						<TD style="WIDTH: 119px" class="tbl-HorizHeader" width="119">Ejec. Comercial:</TD>
						<TD colSpan="2">
							<asp:DropDownList id="ddlPromotora" runat="server" CssClass="listBox"></asp:DropDownList></TD>
						<TD></TD>
					</TR>
					<TR>
						<TD class="tbl-HorizHeader" width="15"></TD>
						<TD style="WIDTH: 119px" class="tbl-HorizHeader" width="119">Periodo:</TD>
						<TD>
							<asp:TextBox id="txtIni" runat="server" MaxLength="10" Columns="12"></asp:TextBox>/
							<asp:TextBox id="txtFin" runat="server" MaxLength="10" Columns="12"></asp:TextBox></TD>
						<TD><INPUT id="btSend" onclick="javascript: Validar();return false;" src="/images/procesar.gif"
								type="image" name="btSend" runat="server">
							<DIV style="DISPLAY: none" id="disbtn" name="disbtn"><IMG alt="" src="/images/procesando.gif"></DIV>
						</TD>
					</TR>
					<TR>
						<TD></TD>
						<TD class="tbl-HorizHeader" colSpan="2">&nbsp;
							<asp:CheckBox id="chkEcat" runat="server" TextAlign="Left" Text="Sólo Clientes e-Catalogo"></asp:CheckBox></TD>
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
				<asp:DataGrid id="dgResultado" runat="server" ShowFooter="True" AllowSorting="True" AutoGenerateColumns="False"
					CellPadding="3" Width="100%">
					<FooterStyle HorizontalAlign="Right" CssClass="tbl-DataGridFooter"></FooterStyle>
					<AlternatingItemStyle CssClass="tbl-DataGridItemAlternating"></AlternatingItemStyle>
					<ItemStyle CssClass="tbl-DataGridItem"></ItemStyle>
					<HeaderStyle CssClass="tbl-DataGridHeader"></HeaderStyle>
					<Columns>
						<asp:BoundColumn Visible="False" DataField="Cod_Promotora" SortExpression="Cod_Promotora ASC" HeaderText="C&#243;d.&lt;BR&gt;Promo"></asp:BoundColumn>
						<asp:BoundColumn Visible="False" DataField="Nom_Promotora" SortExpression="Nom_Promotora ASC" HeaderText="PROMOTORA"></asp:BoundColumn>
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
