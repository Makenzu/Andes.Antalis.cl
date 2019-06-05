<%@ Page Language="vb" AutoEventWireup="false" Codebehind="vta_subfamilia_prod.aspx.vb" Inherits="app.vta_subfamilia_prod"%>
<%@ Register TagPrefix="Wilson" Namespace="Wilson.MasterPages" Assembly="WilsonMasterPages" %>
<META content="text/html; charset=iso-8859-1" http-equiv="Content-Type">
<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
<wilson:masterpage id="MPContainer" runat="server">
	<WILSON:CONTENTREGION id="MPTitle" runat="server">ANDES</WILSON:CONTENTREGION>
	<WILSON:CONTENTREGION id="MPCaption" runat="server">Ventas por SubFamilia - Item<BR>
<asp:Label id="lbFecha" runat="server"></asp:Label></WILSON:CONTENTREGION>
	<SCRIPT language="javascript">
function showPop(cp,cc){
var mywin
	var param 
	param = "width=480,height=500,Top=50,Left=50,toolbar=0,scrollbars=1,location=0,statusbar=0,menubar=0,resizable=1";
	mywin = window.open("vta_x_item_cliente.aspx?cc="+cc+"&cp="+cp,"Detalle_Anual",param );
	mywin.focus();
}
	</SCRIPT>
	<asp:Label id="lbErrors" runat="server" CssClass="txt-FatalMessage"></asp:Label>
	<TABLE id="Table1" border="0" cellSpacing="0" cellPadding="3">
		<TR>
			<TD>
				<TABLE id="Table3" border="1" cellSpacing="0" cellPadding="3">
					<TR>
						<TD>
							<TABLE id="Table2" border="0" cellSpacing="0" cellPadding="0">
								<TR>
									<TD><IMG alt="Volver" src="/images/arrow_blue.gif"></TD>
									<TD>&nbsp;
										<asp:HyperLink id="hlVtaSubFam" runat="server">Ventas por Subfamilia</asp:HyperLink></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
				</TABLE>
				<TABLE id="Table4" border="1" cellSpacing="0" cellPadding="3">
					<TR>
						<TD class="tbl-HorizHeader">Subfamilia:</TD>
						<TD align="left">
							<asp:Label id="lbSubfamilia" runat="server" CssClass="tbl-HorizItem"></asp:Label></TD>
					</TR>
				</TABLE>
			</TD>
		</TR>
		<TR>
			<TD>
				<asp:DataGrid id="dgResultado" runat="server" ShowFooter="True" AutoGenerateColumns="False" CellPadding="2">
					<FooterStyle HorizontalAlign="Right" CssClass="tbl-DataGridFooter"></FooterStyle>
					<AlternatingItemStyle CssClass="tbl-DataGridItemAlternating"></AlternatingItemStyle>
					<ItemStyle CssClass="tbl-DataGridItem"></ItemStyle>
					<HeaderStyle CssClass="tbl-DataGridHeader"></HeaderStyle>
					<Columns>
						<asp:TemplateColumn HeaderText="Producto">
							<ItemTemplate>
								<asp:Label runat="server" Text='<%# "<b>" & DataBinder.Eval(Container, "DataItem.cod_producto") & "</b> - " &  DataBinder.Eval(Container, "DataItem.des_producto")   %>' ID="Label1">
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:BoundColumn DataField="mes_uno" HeaderText="mes uno">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="mes_dos" HeaderText="mes dos">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="mes_tres" HeaderText="mes tres">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="mes_cuatro" HeaderText="mes_cuatro">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="mes_cinco" HeaderText="mes cinco">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="mes_seis" HeaderText="mes seis">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="mes_siete" HeaderText="mes_siete">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="mes_ocho" HeaderText="mes ocho">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="mes_nueve" HeaderText="mes nueve">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="mes_diez" HeaderText="mes diez">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="mes_once" HeaderText="mes once">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="mes_doce" HeaderText="mes doce">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
					</Columns>
				</asp:DataGrid></TD>
		</TR>
		<TR>
			<TD></TD>
		</TR>
	</TABLE>
</wilson:masterpage>
