<%@ Page Language="vb" AutoEventWireup="false" Codebehind="res_vta_cliente_promo.aspx.vb" Inherits="app.res_vta_cliente_promo" %>
<%@ Register TagPrefix="Wilson" Namespace="Wilson.MasterPages" Assembly="WilsonMasterPages" %>
<META content="text/html; charset=iso-8859-1" http-equiv="Content-Type">
<wilson:masterpage id="MPContainer" runat="server">
	<WILSON:CONTENTREGION id="MPTitle" runat="server">Resumen 
Facturación&nbsp;por Cliente</WILSON:CONTENTREGION>
	<WILSON:CONTENTREGION id="MPCaption" runat="server">Resumen Facturación&nbsp;por Cliente 
</WILSON:CONTENTREGION>
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
			<asp:Literal id="Literal2" runat="server"></asp:Literal></UL>
		<DIV class="tab_container">
			<asp:Panel style="DISPLAY: none" id="tab1" class="tab_content" runat="server">
				<TABLE border="0" cellSpacing="0" cellPadding="0" width="785">
					<TR>
						<TD>
							<TABLE id="Table66" border="0" cellSpacing="0" cellPadding="0" align="right">
								<TR>
									<TD>
										<asp:ImageButton id="ibExportar" title="Exportar a Excel" runat="server" ToolTip="Exportar a Excel"
											ImageUrl="/images/exportar.gif" Visible="False" EnableViewState="False"></asp:ImageButton></TD>
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
										<asp:DataGrid id="dgResultado" runat="server" ShowFooter="True" AllowSorting="True" AutoGenerateColumns="False"
											CellPadding="3" Width="100%">
											<FooterStyle HorizontalAlign="Right" CssClass="tbl-DataGridFooter"></FooterStyle>
											<AlternatingItemStyle CssClass="tbl-DataGridItemAlternating"></AlternatingItemStyle>
											<ItemStyle CssClass="tbl-DataGridItem"></ItemStyle>
											<HeaderStyle CssClass="tbl-DataGridHeader"></HeaderStyle>
											<Columns>
												<asp:BoundColumn DataField="cod_cliente" SortExpression="cod_cliente desc" ReadOnly="True" HeaderText="C&#211;DIGO"></asp:BoundColumn>
												<asp:BoundColumn DataField="nom_cliente" SortExpression="nom_cliente desc" ReadOnly="True" HeaderText="RAZ&#211;N SOCIAL"></asp:BoundColumn>
												<asp:BoundColumn DataField="vta_mes_act" SortExpression="vta_mes_act desc" ReadOnly="True" DataFormatString="{0:#,##0}">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="vta_ano_act" SortExpression="vta_ano_act desc" ReadOnly="True" DataFormatString="{0:#,##0}">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="val_mes_uno" SortExpression="val_mes_uno desc" ReadOnly="True" DataFormatString="{0:#,##0}">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="val_mes_dos" SortExpression="val_mes_dos desc" ReadOnly="True" DataFormatString="{0:#,##0}">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="val_mes_tres" SortExpression="val_mes_tres desc" ReadOnly="True" DataFormatString="{0:#,##0}">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="prom_ano_act" SortExpression="prom_ano_act desc" ReadOnly="True" HeaderText="Prom. A&#241;o Act."
													DataFormatString="{0:#,##0}">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="prm_ano_ant" SortExpression="prm_ano_ant desc" ReadOnly="True" HeaderText="Prom. A&#241;o Ant."
													DataFormatString="{0:#,##0}">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="nom_ejecom" HeaderText="Ejecutivo"></asp:BoundColumn>
												<asp:BoundColumn DataField="nom_sales_advisor" HeaderText="Sales Advisor"></asp:BoundColumn>
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
							<TABLE id="Table66_3" border="0" cellSpacing="0" cellPadding="0" align="right">
								<TR>
									<TD>
										<asp:ImageButton id="Imagebutton1" title="Exportar a Excel" runat="server" ToolTip="Exportar a Excel"
											ImageUrl="/images/exportar.gif" Visible="False" EnableViewState="False"></asp:ImageButton></TD>
									<TD width="5"></TD>
									<TD></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD class="contenedor-controles-consulta">
							<TABLE id="Table55_3" border="0" cellSpacing="0" cellPadding="2">
								<TR>
									<TD class="nombre-campo-v2" colSpan="3"></TD>
								</TR>
								<TR>
									<TD class="nombre-campo-v2">Sales Advisor:</TD>
									<TD>
										<asp:DropDownList id="ddlSalesAdv" runat="server" CssClass="listBox"></asp:DropDownList></TD>
									<TD></TD>
								</TR>
								<TR>
									<TD class="nombre-campo-v2">Periodo:</TD>
									<TD>
										<asp:DropDownList id="ddlMes3" runat="server" CssClass="listBox"></asp:DropDownList>&nbsp;/
										<asp:DropDownList id="ddlAno3" runat="server" CssClass="listBox"></asp:DropDownList></TD>
									<TD></TD>
								</TR>
								<TR>
									<TD class="nombre-campo-v2" vAlign="top">Sucursales:</TD>
									<TD>
										<asp:CheckBoxList style="Z-INDEX: 0" id="cbl3" runat="server" CssClass="cbl-sucursales"></asp:CheckBoxList></TD>
									<TD vAlign="top">
										<asp:Button style="Z-INDEX: 0" id="bConsultar3" runat="server" Text="Consultar"></asp:Button></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD>
							<TABLE border="0" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="titulo-tabla">
										<asp:Label id="lSalesAdvisor" runat="server"></asp:Label></TD>
								</TR>
								<TR>
									<TD class="titulo-tabla">
										<asp:Label style="Z-INDEX: 0" id="lSucursales3" runat="server" Visible="False"></asp:Label></TD>
								</TR>
								<TR>
									<TD align="center">
										<asp:DataGrid id="dgResultado3" runat="server" ShowFooter="True" AllowSorting="True" AutoGenerateColumns="False"
											CellPadding="3" Width="100%">
											<FooterStyle HorizontalAlign="Right" CssClass="tbl-DataGridFooter"></FooterStyle>
											<AlternatingItemStyle CssClass="tbl-DataGridItemAlternating"></AlternatingItemStyle>
											<ItemStyle CssClass="tbl-DataGridItem"></ItemStyle>
											<HeaderStyle CssClass="tbl-DataGridHeader"></HeaderStyle>
											<Columns>
												<asp:BoundColumn DataField="cod_cliente" SortExpression="cod_cliente desc" ReadOnly="True" HeaderText="C&#211;DIGO"></asp:BoundColumn>
												<asp:BoundColumn DataField="nom_cliente" SortExpression="nom_cliente desc" ReadOnly="True" HeaderText="RAZ&#211;N SOCIAL"></asp:BoundColumn>
												<asp:BoundColumn DataField="vta_mes_act" SortExpression="vta_mes_act desc" ReadOnly="True" DataFormatString="{0:#,##0}">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="vta_ano_act" SortExpression="vta_ano_act desc" ReadOnly="True" DataFormatString="{0:#,##0}">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="val_mes_uno" SortExpression="val_mes_uno desc" ReadOnly="True" DataFormatString="{0:#,##0}">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="val_mes_dos" SortExpression="val_mes_dos desc" ReadOnly="True" DataFormatString="{0:#,##0}">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="val_mes_tres" SortExpression="val_mes_tres desc" ReadOnly="True" DataFormatString="{0:#,##0}">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="prom_ano_act" SortExpression="prom_ano_act desc" ReadOnly="True" HeaderText="Prom. A&#241;o Act."
													DataFormatString="{0:#,##0}">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="prm_ano_ant" SortExpression="prm_ano_ant desc" ReadOnly="True" HeaderText="Prom. A&#241;o Ant."
													DataFormatString="{0:#,##0}">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="nom_ejecom" HeaderText="Ejecutivo"></asp:BoundColumn>
												<asp:BoundColumn DataField="nom_sales_advisor" HeaderText="Sales Advisor"></asp:BoundColumn>
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
										<asp:ImageButton id="ibExportar2" title="Exportar a Excel" runat="server" ToolTip="Exportar a Excel"
											ImageUrl="/images/exportar.gif" Visible="False" EnableViewState="False"></asp:ImageButton></TD>
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
									<TD class="nombre-campo-v2">Célula:</TD>
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
										<asp:DataGrid id="dgResultado2" runat="server" ShowFooter="True" AllowSorting="True" AutoGenerateColumns="False"
											CellPadding="3" Width="100%">
											<FooterStyle HorizontalAlign="Right" CssClass="tbl-DataGridFooter"></FooterStyle>
											<AlternatingItemStyle CssClass="tbl-DataGridItemAlternating"></AlternatingItemStyle>
											<ItemStyle CssClass="tbl-DataGridItem"></ItemStyle>
											<HeaderStyle CssClass="tbl-DataGridHeader"></HeaderStyle>
											<Columns>
												<asp:BoundColumn DataField="cod_cliente" SortExpression="cod_cliente desc" ReadOnly="True" HeaderText="C&#211;DIGO"></asp:BoundColumn>
												<asp:BoundColumn DataField="nom_cliente" SortExpression="nom_cliente desc" ReadOnly="True" HeaderText="RAZ&#211;N SOCIAL"></asp:BoundColumn>
												<asp:BoundColumn DataField="vta_mes_act" SortExpression="vta_mes_act desc" ReadOnly="True" DataFormatString="{0:#,##0}">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="vta_ano_act" SortExpression="vta_ano_act desc" ReadOnly="True" DataFormatString="{0:#,##0}">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="val_mes_uno" SortExpression="val_mes_uno desc" ReadOnly="True" DataFormatString="{0:#,##0}">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="val_mes_dos" SortExpression="val_mes_dos desc" ReadOnly="True" DataFormatString="{0:#,##0}">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="val_mes_tres" SortExpression="val_mes_tres desc" ReadOnly="True" DataFormatString="{0:#,##0}">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="prom_ano_act" SortExpression="prom_ano_act desc" ReadOnly="True" HeaderText="Prom. A&#241;o Act."
													DataFormatString="{0:#,##0}">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="prm_ano_ant" SortExpression="prm_ano_ant desc" ReadOnly="True" HeaderText="Prom. A&#241;o Ant."
													DataFormatString="{0:#,##0}">
													<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
											</Columns>
										</asp:DataGrid></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
				</TABLE>
			</asp:Panel></DIV>
	</DIV>
</wilson:masterpage>
