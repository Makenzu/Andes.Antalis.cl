<%@ Register TagPrefix="Wilson" Namespace="Wilson.MasterPages" Assembly="WilsonMasterPages" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="vta_mar_promo_cliente_acu.aspx.vb" Inherits="app.vta_mar_promo_cliente_acu"%>
<META content="text/html; charset=iso-8859-1" http-equiv="Content-Type">
<wilson:masterpage id="MPContainer" runat="server" style="Z-INDEX: 0">
	<WILSON:CONTENTREGION id="MPTitle" runat="server">Ranking Margen 
Venta Cliente</WILSON:CONTENTREGION>
	<WILSON:CONTENTREGION id="MPCaption" runat="server">Ranking Margen Venta Cliente<BR>
<asp:Label id="lbFecha" runat="server"></asp:Label></WILSON:CONTENTREGION>
	<SCRIPT language="javascript">
$(document).ready(function()   
{   

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
	<DIV class="pot-contenedor-controles">
		<UL class="tabs">
			<asp:Literal id="Literal2" runat="server"></asp:Literal></UL> <!---------------------------->
		<DIV class="tab_container">
			<asp:Panel style="DISPLAY: none" id="tab1" class="tab_content" runat="server">
				<TABLE border="0" cellSpacing="0" cellPadding="0" width="980">
					<TR>
						<TD>
							<TABLE id="Table66" border="0" cellSpacing="0" cellPadding="0" align="right">
								<TR>
									<TD>
										<asp:ImageButton id="Imagebutton1" title="Exportar a Excel" runat="server" ImageUrl="/images/exportar.gif"
											ToolTip="Exportar a Excel" Visible="False" EnableViewState="False"></asp:ImageButton></TD>
									<TD width="5"></TD>
									<TD></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD class="contenedor-controles-consulta">
							<TABLE id="Table55" border="0" cellSpacing="0" cellPadding="2">
								<TR>
									<TD class="nombre-campo-v2" colSpan="3"></TD>
								</TR>
								<TR>
									<TD class="nombre-campo-v2">Ejec. Comercial:</TD>
									<TD>
										<asp:DropDownList id="ddlEjecCom" runat="server" CssClass="listBox"></asp:DropDownList></TD>
									<TD></TD>
								</TR>
								<TR>
									<TD class="nombre-campo-v2">Periodo:</TD>
									<TD>
										<asp:DropDownList id="ddlMes" runat="server" CssClass="listBox"></asp:DropDownList>&nbsp;/
										<asp:DropDownList id="ddlAno" runat="server" CssClass="listBox"></asp:DropDownList></TD>
									<TD></TD>
								</TR>
								<TR>
									<TD class="nombre-campo-v2" vAlign="top">Sucursales:</TD>
									<TD>
										<asp:CheckBoxList style="Z-INDEX: 0" id="cbl" runat="server" CssClass="cbl-sucursales"></asp:CheckBoxList></TD>
									<TD vAlign="top">
										<asp:Button style="Z-INDEX: 0" id="bConsultar" runat="server" Text="Consultar"></asp:Button></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD>
							<TABLE border="0" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="titulo-tabla">
										<asp:Label id="lEjecutivoComercial" runat="server"></asp:Label></TD>
								</TR>
								<TR>
									<TD class="titulo-tabla">
										<asp:Label style="Z-INDEX: 0" id="lSucursales" runat="server" Visible="False"></asp:Label></TD>
								</TR>
								<TR>
									<TD align="center">
										<asp:DataGrid style="Z-INDEX: 0" id="dgResultado" runat="server" AllowSorting="True" AutoGenerateColumns="False"
											CellPadding="3" ShowFooter="True">
											<FooterStyle Wrap="False" HorizontalAlign="Right" CssClass="tbl-DataGridFooter"></FooterStyle>
											<AlternatingItemStyle CssClass="tbl-DataGridItemAlternating"></AlternatingItemStyle>
											<ItemStyle CssClass="tbl-DataGridItem"></ItemStyle>
											<HeaderStyle CssClass="tbl-DataGridHeader"></HeaderStyle>
											<Columns>
												<asp:BoundColumn DataField="cod_cliente" SortExpression="cod_cliente desc" ReadOnly="True" HeaderText="Cod. Cli."></asp:BoundColumn>
												<asp:BoundColumn DataField="nom_cliente" SortExpression="nom_cliente desc" ReadOnly="True" HeaderText="--- Raz&#243;n Social ---">
													<HeaderStyle Wrap="False" Width="230px"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="vta_ano" SortExpression="vta_ano desc" ReadOnly="True" HeaderText="Vta. A&#241;o">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="vta_mes_dol" SortExpression="vta_mes_dol desc" ReadOnly="True" HeaderText="Vta. Mes (US$)">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="cos_mes_dol" SortExpression="cos_mes_dol desc" ReadOnly="True" HeaderText="Costo Mes (US$)">
													<HeaderStyle HorizontalAlign="Right" Width="75px"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="mar_mes_dol" SortExpression="mar_mes_dol desc" ReadOnly="True" HeaderText="Margen Mes (US$)">
													<HeaderStyle HorizontalAlign="Right" Width="85px"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="vta_ano_dol" SortExpression="vta_ano_dol desc" ReadOnly="True" HeaderText="Vta. A&#241;o (US$)">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="cos_ano_dol" SortExpression="cos_ano_dol desc" ReadOnly="True" HeaderText="Costo A&#241;o (US$)">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="mar_ano_dol" SortExpression="mar_ano_dol desc" ReadOnly="True" HeaderText="Margen A&#241;o (US$)">
													<HeaderStyle HorizontalAlign="Right" Width="85px"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="prm_ano_dol" SortExpression="prm_ano_dol desc" ReadOnly="True" HeaderText="Prom. A&#241;o"
													DataFormatString="{0:#,##0.00}">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
												</asp:BoundColumn>
											</Columns>
										</asp:DataGrid></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
				</TABLE>
			</asp:Panel>
			<asp:Panel style="DISPLAY: none" id="tab2" class="tab_content" runat="server">
				<TABLE border="0" cellSpacing="0" cellPadding="0" width="785">
					<TR>
						<TD>
							<TABLE id="Table6" border="0" cellSpacing="0" cellPadding="0" align="right">
								<TR>
									<TD>
										<asp:ImageButton id="ibExportar2" title="Exportar a Excel" runat="server" ImageUrl="/images/exportar.gif"
											ToolTip="Exportar a Excel" Visible="False" EnableViewState="False"></asp:ImageButton></TD>
									<TD width="5"></TD>
									<TD></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD class="contenedor-controles-consulta">
							<TABLE id="Table5" border="0" cellSpacing="0" cellPadding="2">
								<TR>
									<TD class="nombre-campo-v2">C�lula:</TD>
									<TD>
										<asp:DropDownList id="ddlCelula" runat="server" CssClass="listBox"></asp:DropDownList></TD>
									<TD></TD>
								</TR>
								<TR>
									<TD class="nombre-campo-v2">Periodo:</TD>
									<TD>
										<asp:DropDownList id="ddlMes2" runat="server" CssClass="listBox"></asp:DropDownList>&nbsp;/
										<asp:DropDownList id="ddlAno2" runat="server" CssClass="listBox"></asp:DropDownList></TD>
									<TD>
										<asp:Button id="bConsultar2" runat="server" Text="Consultar"></asp:Button></TD>
								</TR>
								<TR>
									<TD class="nombre-campo-v2" vAlign="top">Sucursales:</TD>
									<TD>
										<asp:CheckBoxList style="Z-INDEX: 0" id="cbl2" runat="server" CssClass="cbl-sucursales"></asp:CheckBoxList></TD>
									<TD></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD>
							<TABLE border="0" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="titulo-tabla">
										<asp:Label id="lNombreCelula" runat="server"></asp:Label></TD>
								</TR>
								<TR>
									<TD class="titulo-tabla">
										<asp:Label style="Z-INDEX: 0" id="lSucursales2" runat="server" Visible="False"></asp:Label></TD>
								</TR>
								<TR>
									<TD align="center">
										<asp:DataGrid style="Z-INDEX: 0" id="dgResultado2" runat="server" AllowSorting="True" AutoGenerateColumns="False"
											CellPadding="3" ShowFooter="True">
											<FooterStyle Wrap="False" HorizontalAlign="Right" CssClass="tbl-DataGridFooter"></FooterStyle>
											<AlternatingItemStyle CssClass="tbl-DataGridItemAlternating"></AlternatingItemStyle>
											<ItemStyle CssClass="tbl-DataGridItem"></ItemStyle>
											<HeaderStyle CssClass="tbl-DataGridHeader"></HeaderStyle>
											<Columns>
												<asp:BoundColumn DataField="cod_cliente" SortExpression="cod_cliente desc" ReadOnly="True" HeaderText="Cod. Cli."></asp:BoundColumn>
												<asp:BoundColumn DataField="nom_cliente" SortExpression="nom_cliente desc" ReadOnly="True" HeaderText="--- Raz&#243;n Social ---">
													<HeaderStyle Width="230px"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="vta_ano" SortExpression="vta_ano desc" ReadOnly="True" HeaderText="Vta. A&#241;o">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="vta_mes_dol" SortExpression="vta_mes_dol desc" ReadOnly="True" HeaderText="Vta. Mes (US$)">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="cos_mes_dol" SortExpression="cos_mes_dol desc" ReadOnly="True" HeaderText="Costo Mes (US$)">
													<HeaderStyle HorizontalAlign="Right" Width="75px"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="mar_mes_dol" SortExpression="mar_mes_dol desc" ReadOnly="True" HeaderText="Margen Mes (US$)">
													<HeaderStyle HorizontalAlign="Right" Width="85px"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="vta_ano_dol" SortExpression="vta_ano_dol desc" ReadOnly="True" HeaderText="Vta. A&#241;o (US$)">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="cos_ano_dol" SortExpression="cos_ano_dol desc" ReadOnly="True" HeaderText="Costo A&#241;o (US$)">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="mar_ano_dol" SortExpression="mar_ano_dol desc" ReadOnly="True" HeaderText="Margen A&#241;o (US$)">
													<HeaderStyle HorizontalAlign="Right" Width="85px"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="prm_ano_dol" SortExpression="prm_ano_dol desc" ReadOnly="True" HeaderText="Prom. A&#241;o"
													DataFormatString="{0:#,##0.00}">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
												</asp:BoundColumn>
											</Columns>
										</asp:DataGrid></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
				</TABLE>
			</asp:Panel>
			<asp:Panel style="DISPLAY: none" id="tab3" class="tab_content" runat="server">
				<TABLE border="0" cellSpacing="0" cellPadding="0" width="785">
					<TR>
						<TD>
							<TABLE id="Table7" border="0" cellSpacing="0" cellPadding="0" align="right">
								<TR>
									<TD>
										<asp:ImageButton id="ibExportar3" title="Exportar a Excel" runat="server" ImageUrl="/images/exportar.gif"
											ToolTip="Exportar a Excel" Visible="False" EnableViewState="False"></asp:ImageButton></TD>
									<TD width="5"></TD>
									<TD></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD class="contenedor-controles-consulta">
							<TABLE id="Table8" border="0" cellSpacing="0" cellPadding="2">
								<TR>
									<TD class="nombre-campo-v2">Top clientes:</TD>
									<TD>
										<asp:DropDownList id="ddlTopN" runat="server" CssClass="listBox"></asp:DropDownList></TD>
									<TD></TD>
								</TR>
								<TR>
									<TD class="nombre-campo-v2">Concepto:</TD>
									<TD>
										<asp:RadioButton id="rbMgAcumulado" runat="server" CssClass="cbl-sucursales" Text="Mg Acc" GroupName="GRP1"
											Checked="True"></asp:RadioButton>
										<asp:RadioButton id="rbVentaAcumulado" runat="server" CssClass="cbl-sucursales" Text="Vta Acc" GroupName="GRP1"></asp:RadioButton></TD>
									<TD></TD>
								</TR>
								<TR>
									<TD class="nombre-campo-v2">Periodo:</TD>
									<TD>
										<asp:DropDownList id="ddlAno3" runat="server" CssClass="listBox"></asp:DropDownList>&nbsp;/
										<asp:DropDownList id="ddlMes3" runat="server" CssClass="listBox"></asp:DropDownList></TD>
									<TD>
										<asp:Button id="bConsultar3" runat="server" Text="Consultar"></asp:Button></TD>
								</TR>
								<TR>
									<TD class="nombre-campo-v2" vAlign="top">Sucursales:</TD>
									<TD>
										<asp:CheckBoxList style="Z-INDEX: 0" id="cbl3" runat="server" CssClass="cbl-sucursales"></asp:CheckBoxList></TD>
									<TD></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD>
							<TABLE border="0" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="titulo-tabla">
										<asp:Label id="lTopClientes" runat="server"></asp:Label></TD>
								</TR>
								<TR>
									<TD class="titulo-tabla">
										<asp:Label style="Z-INDEX: 0" id="lSucursales3" runat="server" Visible="False"></asp:Label></TD>
								</TR>
								<TR>
									<TD align="center">
										<asp:DataGrid style="Z-INDEX: 0" id="dgResultado3" runat="server" AllowSorting="True" AutoGenerateColumns="False"
											CellPadding="3" ShowFooter="True">
											<FooterStyle Wrap="False" HorizontalAlign="Right" CssClass="tbl-DataGridFooter"></FooterStyle>
											<AlternatingItemStyle CssClass="tbl-DataGridItemAlternating"></AlternatingItemStyle>
											<ItemStyle CssClass="tbl-DataGridItem"></ItemStyle>
											<HeaderStyle CssClass="tbl-DataGridHeader"></HeaderStyle>
											<Columns>
												<asp:BoundColumn DataField="cod_cliente" SortExpression="cod_cliente desc" ReadOnly="True" HeaderText="Cod. Cli."></asp:BoundColumn>
												<asp:BoundColumn DataField="nom_cliente" SortExpression="nom_cliente desc" ReadOnly="True" HeaderText="--- Raz&#243;n Social ---">
													<HeaderStyle Width="230px"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="vta_ano" SortExpression="vta_ano desc" ReadOnly="True" HeaderText="Vta. A&#241;o">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="vta_mes_dol" SortExpression="vta_mes_dol desc" ReadOnly="True" HeaderText="Vta. Mes (US$)">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="cos_mes_dol" SortExpression="cos_mes_dol desc" ReadOnly="True" HeaderText="Costo Mes (US$)">
													<HeaderStyle HorizontalAlign="Right" Width="75px"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="mar_mes_dol" SortExpression="mar_mes_dol desc" ReadOnly="True" HeaderText="Margen Mes (US$)">
													<HeaderStyle HorizontalAlign="Right" Width="85px"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="vta_ano_dol" SortExpression="vta_ano_dol desc" ReadOnly="True" HeaderText="Vta. A&#241;o (US$)">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="cos_ano_dol" SortExpression="cos_ano_dol desc" ReadOnly="True" HeaderText="Costo A&#241;o (US$)">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="mar_ano_dol" SortExpression="mar_ano_dol desc" ReadOnly="True" HeaderText="Margen A&#241;o (US$)">
													<HeaderStyle HorizontalAlign="Right" Width="85px"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="prm_ano_dol" SortExpression="prm_ano_dol desc" ReadOnly="True" HeaderText="Prom. A&#241;o"
													DataFormatString="{0:#,##0.00}">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
												</asp:BoundColumn>
											</Columns>
										</asp:DataGrid></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
				</TABLE>
			</asp:Panel></DIV>
	</DIV> <!---------------------------->
</wilson:masterpage>
