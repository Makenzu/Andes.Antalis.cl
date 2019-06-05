<%@ Register TagPrefix="Wilson" Namespace="Wilson.MasterPages" Assembly="WilsonMasterPages" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="pedidos_x_cliente_item.aspx.vb" Inherits="app.pedidos_x_cliente_item"%>
<wilson:masterpage id="MPContainer" runat="server">
<WILSON:CONTENTREGION id=MPTitle runat="server">Ultimos 10 
pedidos&nbsp;por Cliente-Item</WILSON:CONTENTREGION> 
<WILSON:CONTENTREGION id=MPCaption runat="server">Ultimos 10 pedidos&nbsp;por 
Cliente-Item<BR></WILSON:CONTENTREGION>
<SCRIPT type=text/javascript>
$(document).ready(function(){
	   $("#tbCliente").autocomplete("/acClientes.aspx");
	   $("#tbMaterial").autocomplete("/acMateriales.aspx");
	   <asp:Literal id=Literal1 runat="server"></asp:Literal>
});
	</SCRIPT>

<TABLE id=Table1 border=0 cellSpacing=0 cellPadding=0 width="95%" 
  align=center>
  <TR>
    <TD width="70%"></TD>
    <TD width="30%" align=right>
      <TABLE id=Table6 border=0 cellSpacing=0 cellPadding=5>
        <TR>
          <TD></TD>
          <TD width=5></TD>
          <TD><INPUT title=Imprimir 
            onclick="javascript:imprimir();return false;" alt=Imprimir 
            src="/images/imprimir.gif" 
  type=image></TD></TR></TABLE></TD></TR></TABLE>
<TABLE id=Table2 border=0 cellSpacing=0 cellPadding=0 width="98%" 
  align=center>
  <TR>
    <TD vAlign=bottom align=left>
      <TABLE id=Table4 border=0 cellSpacing=3 cellPadding=1 width=300>
        <TR>
          <TD class=frm-nombre-campo>Cliente:</TD>
          <TD>
<asp:TextBox style="Z-INDEX: 0" id=tbCliente runat="server" MaxLength="10" Width="350px" CssClass="frm-caja-texto"></asp:TextBox></TD>
          <TD></TD></TR>
        <TR>
          <TD class=frm-nombre-campo>Material:</TD>
          <TD>
<asp:TextBox style="Z-INDEX: 0" id=tbMaterial runat="server" MaxLength="10" Width="350px" CssClass="frm-caja-texto"></asp:TextBox></TD>
          <TD>
<asp:Button style="Z-INDEX: 0" id=bBuscar runat="server" cssClass="boton-buscar" Text="buscar"></asp:Button></TD></TR></TABLE></TD></TR>
  <TR>
    <TD height=25 width=1095 colSpan=3 align=center></TD></TR>
  <TR>
    <TD bgColor=#a3bad6 height=26 width=1095 colSpan=3 align=center>
<asp:Label id=lTituloConsulta runat="server" CssClass="tbl-HorizItem" Font-Bold="True"></asp:Label></TD></TR>
  <TR>
    <TD width=1095 colSpan=3 align=center>
<asp:DataGrid id=dgPedCli runat="server" AllowSorting="True" CellPadding="3" AutoGenerateColumns="False">
					<FooterStyle CssClass="tbl-DataGridFooter"></FooterStyle>
					<AlternatingItemStyle CssClass="listado-alterno"></AlternatingItemStyle>
					<ItemStyle CssClass="listado-normal"></ItemStyle>
					<HeaderStyle CssClass="listado-header"></HeaderStyle>
					<Columns>
						<asp:BoundColumn DataField="fec_documento" HeaderText="FECHA DOCTO" DataFormatString="{0:dd/MM/yyyy}">
							<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="cod_documento" HeaderText="TIPO">
							<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="num_documento" HeaderText="N&#186; DOCTO REF">
							<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
						</asp:BoundColumn>
						<asp:TemplateColumn HeaderText="N&#176; DOCTO SAP">
							<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
						</asp:TemplateColumn>
						<asp:BoundColumn DataField="val_precio_dolar" HeaderText="VALOR UNIT. GUSD" DataFormatString="{0:#,##0.0000}">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="val_margen_dolar" HeaderText="MARGEN GUSD" DataFormatString="{0:#,##0.0000}">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="p_margen_dolar" HeaderText="%MG GUSD" DataFormatString="{0:0.00%}">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="cod_umb" HeaderText="UMB">
							<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
						</asp:BoundColumn>
					</Columns>
				</asp:DataGrid></TD></TR></TABLE>
</wilson:masterpage>
