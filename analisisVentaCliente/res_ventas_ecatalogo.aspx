<%@ Register TagPrefix="Wilson" Namespace="Wilson.MasterPages" Assembly="WilsonMasterPages" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="res_ventas_ecatalogo.aspx.vb" Inherits="app.res_ventas_ecatalogo" %>
<META content="text/html; charset=iso-8859-1" http-equiv="Content-Type">
<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
<wilson:masterpage id="MPContainer" runat="server">
	<WILSON:CONTENTREGION id="MPTitle" runat="server">Resumen ventas 
e-Catalogo</WILSON:CONTENTREGION>
	<WILSON:CONTENTREGION id="MPCaption" runat="server">Resumen ventas e-Catalogo 
<asp:Label id="lbFecha" runat="server"></asp:Label></WILSON:CONTENTREGION>
	<SCRIPT type="text/javascript">
$(document).ready(function()   
{   
    $(".tab_content").hide(); 
 
    $("ul.tabs li").click(function()   
       {   
        $("ul.tabs li").removeClass("active");   
        $(this).addClass("active");   
        $(".tab_content").hide();   
  
        var activeTab = $(this).find("a").attr("href");   
        $(activeTab).fadeIn();   
        return false;   
    });  
    
<asp:Literal id="Literal1" runat="server"></asp:Literal>              
  
});
	</SCRIPT>
	<UL class="tabs">
		<LI id="op1">
			<A href="#tab1">Célula</A>
		<LI id="op2">
			<A href="#tab2">Ejecutivo comercial</A>
		<LI id="op3">
			<A href="#tab3">Vendedora virtual</A>
		</LI>
	</UL>
	<DIV class="tab_container">
		<DIV style="DISPLAY: none" id="tab1" class="tab_content">
			<DIV>
				<TABLE border="0" cellSpacing="0" cellPadding="3">
					<TR>
						<TD class="nombre-celda-resecat">Célula:</TD>
						<TD>
							<asp:DropDownList id="ddlCelula" runat="server"></asp:DropDownList></TD>
						<TD>
							<asp:Button id="bBuscarPorCelula" runat="server" Text="buscar"></asp:Button></TD>
					</TR>
					<TR>
						<TD class="nombre-celda-resecat">Período:</TD>
						<TD>
							<asp:TextBox style="Z-INDEX: 0" id="txtIniPorCelula" runat="server" MaxLength="10" Columns="12"></asp:TextBox>
							<asp:TextBox style="Z-INDEX: 0" id="txtFinPorCelula" runat="server" MaxLength="10" Columns="12"></asp:TextBox></TD>
						<TD></TD>
					</TR>
					<TR>
						<TD class="nombre-celda-resecat" colSpan="3">
							<asp:CheckBox style="Z-INDEX: 0" id="chkSoloClientesEcatalogoCelula" runat="server" Text="Sólo Clientes e-Catalogo"
								CssClass="nombre-celda-resecat"></asp:CheckBox></TD>
					</TR>
				</TABLE>
			</DIV>
			<asp:Panel id="pDatosCelula" runat="server">
				<DIV>
					<TABLE id="Table2" border="0" cellSpacing="0" cellPadding="1" width="95%" align="center">
						<TR>
							<TD vAlign="bottom" width="45%"></TD>
							<TD></TD>
						</TR>
						<TR>
							<TD height="25" colSpan="2" align="left">
								<asp:Label id="lbNota" runat="server" CssClass="txt-SoftMessage" Visible="False"></asp:Label></TD>
						</TR>
						<TR>
							<TD bgColor="#a3bad6" height="25" colSpan="2" align="center">
								<asp:Label id="lbCelula" runat="server" CssClass="tbl-HorizItem" Font-Bold="True"></asp:Label></TD>
						</TR>
						<TR>
							<TD colSpan="2">
								<asp:DataGrid id="dgDatosCelula" runat="server" ShowFooter="True" AllowSorting="True" AutoGenerateColumns="False"
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
									<asp:Label id="lClientesCelula" runat="server">0</asp:Label><BR>
									Total Clientes e-Catálogo :
									<asp:Label id="lClientesEcatalogoCelula" runat="server">0</asp:Label></P>
								<P>&nbsp;</P>
							</TD>
						</TR>
					</TABLE>
				</DIV>
			</asp:Panel></DIV>
		<DIV style="DISPLAY: none" id="tab2" class="tab_content">
			<DIV>
				<TABLE border="0" cellSpacing="0" cellPadding="3">
					<TR>
						<TD class="nombre-celda-resecat">Ejecutivo Comercial:</TD>
						<TD>
							<asp:DropDownList id="ddlEjecutivoComercial" runat="server"></asp:DropDownList></TD>
						<TD>
							<asp:Button id="bBuscarPorEjeCom" runat="server" Text="buscar"></asp:Button></TD>
					</TR>
					<TR>
						<TD class="nombre-celda-resecat">Período:</TD>
						<TD>
							<asp:TextBox style="Z-INDEX: 0" id="txtIniPorEjeCom" runat="server" MaxLength="10" Columns="12"></asp:TextBox>
							<asp:TextBox style="Z-INDEX: 0" id="txtFinPorEjeCom" runat="server" MaxLength="10" Columns="12"></asp:TextBox></TD>
						<TD></TD>
					</TR>
					<TR>
						<TD class="nombre-celda-resecat" colSpan="3">
							<asp:CheckBox style="Z-INDEX: 0" id="chkbSoloClientesEcatalogoEjeCom" runat="server" Text="Sólo Clientes e-Catalogo"
								CssClass="nombre-celda-resecat"></asp:CheckBox></TD>
					</TR>
				</TABLE>
			</DIV>
			<asp:Panel id="pDatosEjeCom" runat="server">
				<DIV>
					<TABLE id="Table2" border="0" cellSpacing="0" cellPadding="1" width="95%" align="center">
						<TR>
							<TD vAlign="bottom" width="45%"></TD>
							<TD></TD>
						</TR>
						<TR>
							<TD height="25" colSpan="2" align="left">
								<asp:Label id="lEjecutivaComercial" runat="server" CssClass="txt-SoftMessage" Visible="False"></asp:Label></TD>
						</TR>
						<TR>
							<TD bgColor="#a3bad6" height="25" colSpan="2" align="center">
								<asp:Label id="lEjecutivoComercial" runat="server" CssClass="tbl-HorizItem" Font-Bold="True"></asp:Label></TD>
						</TR>
						<TR>
							<TD colSpan="2">
								<asp:DataGrid id="dgDatosEjeComercial" runat="server" ShowFooter="True" AllowSorting="True" AutoGenerateColumns="False"
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
									<asp:Label id="lClientesEjeCom" runat="server">0</asp:Label><BR>
									Total Clientes e-Catálogo :
									<asp:Label id="lClientesEcatalogoEjeCom" runat="server">0</asp:Label></P>
								<P>&nbsp;</P>
							</TD>
						</TR>
					</TABLE>
				</DIV>
			</asp:Panel></DIV>
		<DIV style="DISPLAY: none" id="tab3" class="tab_content">
			<DIV>
				<TABLE border="0" cellSpacing="0" cellPadding="3">
					<TR>
						<TD class="nombre-celda-resecat">Vendedora virtual:</TD>
						<TD>
							<asp:DropDownList id="ddlVendedoraVirtual" runat="server"></asp:DropDownList></TD>
						<TD>
							<asp:Button id="bBuscarPorVendVirtual" runat="server" Text="buscar"></asp:Button></TD>
					</TR>
					<TR>
						<TD class="nombre-celda-resecat">Período:</TD>
						<TD>
							<asp:TextBox style="Z-INDEX: 0" id="txtIniPorVendVirt" runat="server" MaxLength="10" Columns="12"></asp:TextBox>
							<asp:TextBox style="Z-INDEX: 0" id="txtFinPorVendVirt" runat="server" MaxLength="10" Columns="12"></asp:TextBox></TD>
						<TD></TD>
					</TR>
					<TR>
						<TD class="nombre-celda-resecat" colSpan="3">
							<asp:CheckBox style="Z-INDEX: 0" id="chkbSoloClientesEcatalogoVendVirtual" runat="server" Text="Sólo Clientes e-Catalogo"></asp:CheckBox></TD>
					</TR>
				</TABLE>
			</DIV>
			<asp:Panel id="pDatosVendVirtual" runat="server">
				<DIV>
					<TABLE id="Table2" border="0" cellSpacing="0" cellPadding="1" width="95%" align="center">
						<TR>
							<TD vAlign="bottom" width="45%"></TD>
							<TD></TD>
						</TR>
						<TR>
							<TD height="25" colSpan="2" align="left">
								<asp:Label id="Label6" runat="server" CssClass="txt-SoftMessage" Visible="False"></asp:Label></TD>
						</TR>
						<TR>
							<TD bgColor="#a3bad6" height="25" colSpan="2" align="center">
								<asp:Label id="lVenderoraVitual" runat="server" CssClass="tbl-HorizItem" Font-Bold="True"></asp:Label></TD>
						</TR>
						<TR>
							<TD colSpan="2">
								<asp:DataGrid id="dgDatosVendVirtual" runat="server" ShowFooter="True" AllowSorting="True" AutoGenerateColumns="False"
									CellPadding="3" Width="100%">
									<FooterStyle HorizontalAlign="Right" CssClass="tbl-DataGridFooter"></FooterStyle>
									<AlternatingItemStyle CssClass="tbl-DataGridItemAlternating"></AlternatingItemStyle>
									<ItemStyle CssClass="tbl-DataGridItem"></ItemStyle>
									<HeaderStyle CssClass="tbl-DataGridHeader"></HeaderStyle>
									<Columns>
										<asp:BoundColumn Visible="False" DataField="Cod_Promotora" SortExpression="Cod_Promotora ASC" HeaderText="C&#243;d.&lt;BR&gt;Promo"></asp:BoundColumn>
										<asp:BoundColumn Visible="False" DataField="nom_vendedora" SortExpression="Nom_vendedora ASC" HeaderText="VENDEDORA VIRT."></asp:BoundColumn>
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
									<asp:Label id="lClientesVendVirtual" runat="server">0</asp:Label><BR>
									Total Clientes e-Catálogo :
									<asp:Label id="lClientesEcatalogoVendVirt" runat="server">0</asp:Label></P>
								<P>&nbsp;</P>
							</TD>
						</TR>
					</TABLE>
				</DIV>
			</asp:Panel></DIV>
	</DIV>
</wilson:masterpage>
