<%@ Register TagPrefix="Wilson" Namespace="Wilson.MasterPages" Assembly="WilsonMasterPages" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="vta_x_cliente_item.aspx.vb" Inherits="app.vta_x_cliente_item"%>
<wilson:masterpage id="MPContainer" runat="server">
	<WILSON:CONTENTREGION id="MPTitle" runat="server">Ventas por 
Cliente - Item</WILSON:CONTENTREGION>
	<WILSON:CONTENTREGION id="MPCaption" runat="server">Ventas por Cliente - Item</WILSON:CONTENTREGION>
	<SCRIPT language="javascript">
function showPop(cp,cc,ms,an){
var mywin
	var param 
	param = "width=450,height=460,Top=50,Left=50,toolbar=0,scrollbars=1,location=0,statusbar=0,menubar=0,resizable=1";
	mywin = window.open("vta_x_cliente_item_detalle.aspx?cc="+cc+"&cp="+cp+"&ms="+ms+"&an="+an,"Detalle_Anual",param );
	mywin.focus();
}	

	$(document).ready(function()   
	{      
		$("#txCliente").autocomplete("/acClientes.aspx", {   
			extraParams: {
					cs: function() { 
						var sucursal = $('#ddlSucursal').val();
						if  (sucursal == '001')
							return 'GMSC';
						if  ((sucursal == '002') || (sucursal == '022'))
							return 'APER';
						if  ((sucursal == '003') || (sucursal == '004'))
							return 'ABOL';
						if  (sucursal == '005')
							return 'DGS0';												
						if  (sucursal == '007')
							return 'DGS0';	
							
						if (sucursal == '-100')
						{
							if ($('#ddlFilial').val() == 'CHI')
								return 'GMSC';
							if ($('#ddlFilial').val() == 'BOL')
								return 'ABOL';
							if ($('#ddlFilial').val() == 'PER')
								return 'APER';
								
								
						}
					}	  
				} 
		});		
	}); 

	</SCRIPT>
	<TABLE id="Table1" border="0" cellSpacing="0" cellPadding="0" width="95%" align="center">
		<TR>
			<TD width="714">
				<P align="left">
					<asp:Label id="lbErrors" runat="server" EnableViewState="False" CssClass="txt-FatalMessage"></asp:Label></P>
			</TD>
			<TD width="30%" align="right"><INPUT title="Imprimir" onclick="javascript:imprimir();return false;" alt="Imprimir" src="/images/imprimir.gif"
					type="image">&nbsp;&nbsp;&nbsp;
				<asp:ImageButton id="ibExportar" title="Exportar a Excel" runat="server" EnableViewState="False"
					ImageUrl="/images/exportar.gif" Visible="False" ToolTip="Exportar a Excel"></asp:ImageButton></TD>
		</TR>
	</TABLE>
	<TABLE id="Table2" border="0" cellSpacing="0" cellPadding="0" width="98%" align="center">
		<TR>
			<TD vAlign="top" align="left">
				<TABLE id="Table7" border="0" cellSpacing="0" cellPadding="0">
					<TR>
						<TD width="15"></TD>
						<TD>
							<TABLE id="tbParams" border="0" cellSpacing="0" cellPadding="3">
								<TR>
									<TD class="tbl-HorizHeader" align="left">Sucursal:</TD>
									<TD align="left">
										<asp:DropDownList id="ddlSucursal" runat="server" CssClass="listBox" Width="120px"></asp:DropDownList></TD>
								</TR>
								<TR>
									<TD class="tbl-HorizHeader" noWrap align="left">Cliente:</TD>
									<TD align="left">
										<asp:TextBox id="txCliente" runat="server" CssClass="textBox" Width="300px" MaxLength="80"></asp:TextBox><INPUT id="hfCodigoCliente" type="hidden" runat="server"></TD>
								</TR>
								<TR>
									<TD class="tbl-HorizHeader" align="left">Período:</TD>
									<TD align="left">
										<asp:DropDownList style="Z-INDEX: 0" id="ddlMes" runat="server" CssClass="listBox"></asp:DropDownList>&nbsp;/ 
										&nbsp;
										<asp:DropDownList style="Z-INDEX: 0" id="ddlAno" runat="server" CssClass="listBox"></asp:DropDownList></TD>
								</TR>
							</TABLE>
							<INPUT id="hfCodigoSociedad" type="hidden" runat="server"></TD>
						<TD width="180">
							<asp:RadioButtonList style="Z-INDEX: 0" id="rbUnidadMedida" runat="server" CssClass="txt-SoftMessage">
								<asp:ListItem Value="UMV">Unidad de Venta</asp:ListItem>
								<asp:ListItem Value="UMB" Selected="True">Unidad Base</asp:ListItem>
								<asp:ListItem Value="VOL">Unidad de Volumen</asp:ListItem>
							</asp:RadioButtonList></TD>
						<TD>
							<asp:Button id="bBuscar" runat="server" Text="buscar"></asp:Button></TD>
					</TR>
				</TABLE>
			</TD>
			<TD vAlign="top">
				<TABLE id="tblFiltro" border="0" cellSpacing="0" cellPadding="1" runat="server">
					<TR>
						<TD class="tbl-HorizHeader" colSpan="3" align="left">Filtrar por :</TD>
						<TD class="tbl-HorizHeader"></TD>
					</TR>
					<TR>
						<TD class="tbl-HorizItem" width="2"></TD>
						<TD class="tbl-HorizItem" align="left">Material:</TD>
						<TD align="left">
							<asp:TextBox id="txItem" runat="server" CssClass="textBox" Width="90px" MaxLength="12"></asp:TextBox>&nbsp;
						</TD>
						<TD>
							<asp:ImageButton id="btItem" runat="server" ImageUrl="/images/procesar.gif"></asp:ImageButton></TD>
					</TR>
					<TR>
						<TD class="tbl-HorizItem"></TD>
						<TD class="tbl-HorizItem" align="left">Descripción:</TD>
						<TD align="left">
							<asp:TextBox id="txDesc" runat="server" CssClass="textBox" Width="120px" MaxLength="50"></asp:TextBox>&nbsp;&nbsp;&nbsp;</TD>
						<TD>
							<asp:ImageButton id="btDesc" runat="server" ImageUrl="/images/procesar.gif"></asp:ImageButton></TD>
					</TR>
				</TABLE>
			</TD>
		</TR>
		<TR>
			<TD height="25" colSpan="3" align="center"></TD>
		</TR>
		<TR>
			<TD bgColor="#a3bad6" height="25" colSpan="3" align="center">
				<asp:Label id="lbNomCli" runat="server" CssClass="tbl-HorizItem" Font-Bold="True"></asp:Label></TD>
		</TR>
		<TR>
			<TD colSpan="3" align="center">
				<asp:DataGrid id="dgCliItem12" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="2"
					AllowSorting="True" ShowFooter="True">
					<FooterStyle CssClass="tbl-DataGridFooter"></FooterStyle>
					<AlternatingItemStyle CssClass="tbl-DataGridItemAlternating"></AlternatingItemStyle>
					<ItemStyle CssClass="tbl-DataGridItem"></ItemStyle>
					<HeaderStyle CssClass="tbl-DataGridHeader"></HeaderStyle>
					<Columns>
						<asp:BoundColumn DataField="cod_subfamilia" SortExpression="cod_subfamilia" HeaderText="SubF">
							<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
							<ItemStyle HorizontalAlign="Left"></ItemStyle>
							<FooterStyle HorizontalAlign="Left"></FooterStyle>
						</asp:BoundColumn>
						<asp:TemplateColumn SortExpression="cod_producto DESC" HeaderText="C&#243;digo">
							<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
							<ItemStyle Font-Bold="True" HorizontalAlign="Left"></ItemStyle>
							<ItemTemplate>
								<A href='javascript:showPop("<%# trim(DataBinder.Eval(Container, "DataItem.cod_producto")) %>","<%# trim(DataBinder.Eval(Container, "DataItem.cod_cliente")) %>",<%# request("ddlMes") %>,<%# request("ddlAno") %>)'>
									<asp:Label id=Label1 runat="server" Text='<%# trim(DataBinder.Eval(Container, "DataItem.cod_producto")) %>'>
									</asp:Label></A>
							</ItemTemplate>
							<FooterStyle HorizontalAlign="Left"></FooterStyle>
						</asp:TemplateColumn>
						<asp:BoundColumn DataField="des_producto" SortExpression="des_producto desc" ReadOnly="True" HeaderText="Descripci&#243;n">
							<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
							<ItemStyle HorizontalAlign="Left"></ItemStyle>
							<FooterStyle HorizontalAlign="Left"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="cod_um" SortExpression="cod_umb" ReadOnly="True" HeaderText="UM">
							<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="mes_actual" SortExpression="can_mes_act" ReadOnly="True" HeaderText="Mes Consulta"
							DataFormatString="{0:#,##0}">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="mes_uno" SortExpression="mes_uno" HeaderText="mes_uno" DataFormatString="{0:#,##0}">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="mes_dos" SortExpression="mes_dos" ReadOnly="True" HeaderText="mes_dos"
							DataFormatString="{0:#,##0}">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="mes_tres" SortExpression="mes_tres" ReadOnly="True" HeaderText="mes_tres"
							DataFormatString="{0:#,##0}">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="mes_cuatro" SortExpression="mes_cuatro" ReadOnly="True" HeaderText="mes_cuatro"
							DataFormatString="{0:#,##0}">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="mes_cinco" SortExpression="mes_cinco" ReadOnly="True" HeaderText="mes_cinco"
							DataFormatString="{0:#,##0}">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="mes_seis" SortExpression="mes_seis" ReadOnly="True" HeaderText="mes_seis"
							DataFormatString="{0:#,##0}">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="mes_siete" SortExpression="mes_siete" ReadOnly="True" HeaderText="mes_siete"
							DataFormatString="{0:#,##0}">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="mes_ocho" SortExpression="mes_ocho" ReadOnly="True" HeaderText="mes_ocho"
							DataFormatString="{0:#,##0}">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="mes_nueve" SortExpression="mes_nueve" ReadOnly="True" HeaderText="mes_nueve"
							DataFormatString="{0:#,##0}">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="mes_diez" SortExpression="mes_diez" ReadOnly="True" HeaderText="mes_diez"
							DataFormatString="{0:#,##0}">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="mes_once" SortExpression="mes_once" ReadOnly="True" HeaderText="mes_once"
							DataFormatString="{0:#,##0}">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="mes_doce" SortExpression="mes_doce" ReadOnly="True" HeaderText="mes_doce"
							DataFormatString="{0:#,##0}">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="val_mes_act" SortExpression="val_mes_act" ReadOnly="True" HeaderText="$ Vta. Mes"
							DataFormatString="{0:#,##0}">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn Visible="False" DataField="cod_producto" HeaderText="cod_producto">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
					</Columns>
				</asp:DataGrid></TD>
		</TR>
	</TABLE>
</wilson:masterpage>
