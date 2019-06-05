<%@ Page Language="vb" AutoEventWireup="false" Codebehind="default.aspx.vb" Inherits="app.potencial_default" %>
<%@ Register TagPrefix="Wilson" Namespace="Wilson.MasterPages" Assembly="WilsonMasterPages" %>
<wilson:masterpage style="Z-INDEX: 0" id="MPContainer" runat="server">
	<WILSON:CONTENTREGION id="MPTitle" runat="server">Ficha 
clientes</WILSON:CONTENTREGION>
	<WILSON:CONTENTREGION id="MPCaption" runat="server">Ficha clientes</WILSON:CONTENTREGION>
	<SCRIPT language="javascript">
$(document).ready(function()   
{   
    $(".tab_content").hide();  
    
  
<asp:Literal id="Literal1" runat="server"></asp:Literal>

  
    $("ul.tabs li").click(function()   
       {   
        $("ul.tabs li").removeClass("active");   
        $(this).addClass("active");   
        $(".tab_content").hide();   
  
        var activeTab = $(this).find("a").attr("href");   
        $(activeTab).fadeIn();   
        return false;   
    });   
    
    $("#tbPatronCliente").autocomplete("/acClientes.aspx");
    		
}); 
	</SCRIPT>
	<DIV class="pot-contenedor-controles">
		<FIELDSET>
			<LEGEND class="pot-groupbox-potencial">Busqueda de clientes</LEGEND>
			<DIV class="pot-contenedor-controles">
				<UL class="tabs">
					<LI id="op1">
						<A href="#tab1">Ejecutiva comercial</A>
					<LI id="op2">
						<A href="#tab2">Vendedora virtual</A>
					<LI id="op3">
						<A href="#tab3">Cobrador</A>
					<LI id="op4">
						<A href="#tab4">Célula</A>
					<LI id="op5">
						<A href="#tab5">Clasificación Antalis</A>
					<LI id="op6">
						<A href="#tab6">Cliente</A>
					</LI>
				</UL>
				<DIV class="tab_container">
					<DIV id="tab1" class="tab_content">
						<TABLE border="0" cellSpacing="0" cellPadding="0">
							<TR>
								<TD class="nombre-campo">Ejec. comercial:&nbsp;</TD>
								<TD>
									<asp:DropDownList style="Z-INDEX: 0" id="ddlEjecutivaComercial" runat="server"></asp:DropDownList></TD>
								<TD>&nbsp;
									<asp:Button id="bBuscarPorEjecComercial" runat="server" Text="buscar"></asp:Button></TD>
							</TR>
							<TR>
								<TD class="nombre-campo" height="15" colSpan="3"></TD>
							</TR>
						</TABLE>
						<asp:DataGrid id="dgResultadoTab1" runat="server" GridLines="Vertical" CssClass="pot-resultados"
							CellPadding="3" AutoGenerateColumns="False">
							<AlternatingItemStyle CssClass="pot-item-alternado"></AlternatingItemStyle>
							<ItemStyle CssClass="pot-item-normal"></ItemStyle>
							<HeaderStyle CssClass="pot-cabecera"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cod_cliente" HeaderText="Cod.&lt;br&gt;Cliente"></asp:BoundColumn>
								<asp:BoundColumn DataField="nom_cliente" HeaderText="Raz&#243;n Social"></asp:BoundColumn>
								<asp:BoundColumn DataField="nom_vend_virtual" HeaderText="Vend. Virtual"></asp:BoundColumn>
								<asp:BoundColumn DataField="nom_cobrador" HeaderText="Cobrador"></asp:BoundColumn>
								<asp:BoundColumn DataField="celula" HeaderText="C&#233;lula"></asp:BoundColumn>
								<asp:BoundColumn DataField="cod_cla_antalis" HeaderText="Cod.Cla.&lt;br&gt;Antalis"></asp:BoundColumn>
								<asp:BoundColumn DataField="dmc_cla_antalis" HeaderText="Dmc Cla. Antalis"></asp:BoundColumn>
								<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="Update" CancelText="Cancel" EditText="ver"></asp:EditCommandColumn>
							</Columns>
						</asp:DataGrid></DIV>
					<DIV id="tab2" class="tab_content">
						<TABLE border="0" cellSpacing="0" cellPadding="0">
							<TR>
								<TD class="nombre-campo">Vend. virtual:&nbsp;</TD>
								<TD>
									<asp:DropDownList id="ddlVendedoraVirtual" runat="server"></asp:DropDownList></TD>
								<TD>&nbsp;
									<asp:Button id="bBuscarPorVendVirtual" runat="server" Text="buscar"></asp:Button></TD>
							</TR>
							<TR>
								<TD class="nombre-campo" height="15" colSpan="3"></TD>
							</TR>
						</TABLE>
						<asp:DataGrid style="Z-INDEX: 0" id="dgResultadoTab2" runat="server" GridLines="Vertical" CssClass="pot-resultados"
							CellPadding="3" AutoGenerateColumns="False">
							<AlternatingItemStyle CssClass="pot-item-alternado"></AlternatingItemStyle>
							<ItemStyle CssClass="pot-item-normal"></ItemStyle>
							<HeaderStyle CssClass="pot-cabecera"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cod_cliente" HeaderText="Cod.&lt;br&gt;Cliente"></asp:BoundColumn>
								<asp:BoundColumn DataField="nom_cliente" HeaderText="Raz&#243;n Social"></asp:BoundColumn>
								<asp:BoundColumn DataField="nom_ejec_com" HeaderText="Ejec. Comercial"></asp:BoundColumn>
								<asp:BoundColumn DataField="nom_cobrador" HeaderText="Cobrador"></asp:BoundColumn>
								<asp:BoundColumn DataField="celula" HeaderText="C&#233;lula"></asp:BoundColumn>
								<asp:BoundColumn DataField="cod_cla_antalis" HeaderText="Cod.Cla.&lt;br&gt;Antalis"></asp:BoundColumn>
								<asp:BoundColumn DataField="dmc_cla_antalis" HeaderText="Dmc Cla. Antalis"></asp:BoundColumn>
								<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="Update" CancelText="Cancel" EditText="ver"></asp:EditCommandColumn>
							</Columns>
						</asp:DataGrid></DIV>
					<DIV id="tab3" class="tab_content">
						<TABLE border="0" cellSpacing="0" cellPadding="0">
							<TR>
								<TD class="nombre-campo">Cobrador:&nbsp;</TD>
								<TD>
									<asp:DropDownList id="ddlCobrador" runat="server"></asp:DropDownList></TD>
								<TD>&nbsp;
									<asp:Button id="bBuscarPorCobrador" runat="server" Text="buscar"></asp:Button></TD>
							</TR>
							<TR>
								<TD class="nombre-campo" height="15" colSpan="3"></TD>
							</TR>
						</TABLE>
						<asp:DataGrid style="Z-INDEX: 0" id="dgResultadoTab3" runat="server" GridLines="Vertical" CssClass="pot-resultados"
							CellPadding="3" AutoGenerateColumns="False">
							<AlternatingItemStyle CssClass="pot-item-alternado"></AlternatingItemStyle>
							<ItemStyle CssClass="pot-item-normal"></ItemStyle>
							<HeaderStyle CssClass="pot-cabecera"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cod_cliente" HeaderText="Cod. Cliente"></asp:BoundColumn>
								<asp:BoundColumn DataField="nom_cliente" HeaderText="Raz&#243;n Social"></asp:BoundColumn>
								<asp:BoundColumn DataField="nom_ejec_com" HeaderText="Ejec. Comercial"></asp:BoundColumn>
								<asp:BoundColumn DataField="nom_vend_virtual" HeaderText="Vend. Virtual"></asp:BoundColumn>
								<asp:BoundColumn DataField="celula" HeaderText="C&#233;lula"></asp:BoundColumn>
								<asp:BoundColumn DataField="cod_cla_antalis" HeaderText="Cod.Cla.&lt;br&gt;Antalis"></asp:BoundColumn>
								<asp:BoundColumn DataField="dmc_cla_antalis" HeaderText="Dmc Cla. Antalis"></asp:BoundColumn>
								<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="Update" CancelText="Cancel" EditText="ver"></asp:EditCommandColumn>
							</Columns>
						</asp:DataGrid></DIV>
					<DIV id="tab4" class="tab_content">
						<TABLE border="0" cellSpacing="0" cellPadding="0">
							<TR>
								<TD class="nombre-campo">Célula:&nbsp;</TD>
								<TD>
									<asp:DropDownList id="ddlCelula" runat="server"></asp:DropDownList>&nbsp;</TD>
								<TD>
									<asp:Button id="bBuscarPorCelula" runat="server" Text="buscar"></asp:Button></TD>
							</TR>
							<TR>
								<TD class="nombre-campo" height="15" colSpan="3"></TD>
							</TR>
						</TABLE>
						<asp:DataGrid style="Z-INDEX: 0" id="dgResultadoTab4" runat="server" CellPadding="3" AutoGenerateColumns="False">
							<AlternatingItemStyle CssClass="pot-item-alternado"></AlternatingItemStyle>
							<ItemStyle CssClass="pot-item-normal"></ItemStyle>
							<HeaderStyle CssClass="pot-cabecera"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cod_cliente" HeaderText="Cod. Cliente"></asp:BoundColumn>
								<asp:BoundColumn DataField="nom_cliente" HeaderText="Raz&#243;n Social"></asp:BoundColumn>
								<asp:BoundColumn DataField="nom_ejec_com" HeaderText="Ejec. Comercial"></asp:BoundColumn>
								<asp:BoundColumn DataField="nom_vend_virtual" HeaderText="Vend. Virtual"></asp:BoundColumn>
								<asp:BoundColumn DataField="nom_cobrador" HeaderText="Cobrador"></asp:BoundColumn>
								<asp:BoundColumn DataField="cod_cla_antalis" HeaderText="Cod.Cla.&lt;br&gt;Antalis"></asp:BoundColumn>
								<asp:BoundColumn DataField="dmc_cla_antalis" HeaderText="Dmc Cla. Antalis"></asp:BoundColumn>
								<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="Update" CancelText="Cancel" EditText="ver"></asp:EditCommandColumn>
							</Columns>
						</asp:DataGrid></DIV>
					<DIV id="tab5" class="tab_content">
						<TABLE border="0" cellSpacing="0" cellPadding="0">
							<TR>
								<TD class="nombre-campo">Clasific. Antalis:&nbsp;</TD>
								<TD>
									<asp:DropDownList id="ddlClasificacionAntalis" runat="server"></asp:DropDownList></TD>
								<TD>&nbsp;
									<asp:Button id="bBuscarPorClasificacionAntalis" runat="server" Text="buscar"></asp:Button></TD>
							</TR>
							<TR>
								<TD class="nombre-campo" height="15" colSpan="3"></TD>
							</TR>
						</TABLE>
						<asp:DataGrid style="Z-INDEX: 0" id="dgResultadoTab5" runat="server" GridLines="Vertical" CssClass="pot-resultados"
							CellPadding="3" AutoGenerateColumns="False">
							<AlternatingItemStyle CssClass="pot-item-alternado"></AlternatingItemStyle>
							<ItemStyle CssClass="pot-item-normal"></ItemStyle>
							<HeaderStyle CssClass="pot-cabecera"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cod_cliente" HeaderText="Cod. Cliente"></asp:BoundColumn>
								<asp:BoundColumn DataField="nom_cliente" HeaderText="Raz&#243;n Social"></asp:BoundColumn>
								<asp:BoundColumn DataField="nom_ejec_com" HeaderText="Ejec. Comercial"></asp:BoundColumn>
								<asp:BoundColumn DataField="nom_vend_virtual" HeaderText="Vend. Virtual"></asp:BoundColumn>
								<asp:BoundColumn DataField="nom_cobrador" HeaderText="Cobrador"></asp:BoundColumn>
								<asp:BoundColumn DataField="celula" HeaderText="Celula"></asp:BoundColumn>
								<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="Update" CancelText="Cancel" EditText="ver"></asp:EditCommandColumn>
							</Columns>
						</asp:DataGrid></DIV>
					<DIV id="tab6" class="tab_content">
						<TABLE id="Table1" border="0" cellSpacing="0" cellPadding="0">
							<TR>
								<TD class="pot-celda-titulo">Código / Razón social:</TD>
								<TD>
									<asp:TextBox id="tbPatronCliente" runat="server" MaxLength="80" Width="400px"></asp:TextBox></TD>
								<TD>&nbsp;
									<asp:Button id="bBuscarPorPatron" runat="server" Text="buscar"></asp:Button></TD>
							</TR>
							<TR>
								<TD class="pot-celda-titulo" height="15" colSpan="3"></TD>
							</TR>
							<TR>
								<TD colSpan="3">
									<asp:Label id="lMensajeAccionBusqueda" runat="server"></asp:Label></TD>
							</TR>
						</TABLE>
						<asp:DataGrid style="Z-INDEX: 0" id="dgResultadoTab6" runat="server" GridLines="Vertical" CssClass="pot-resultados"
							CellPadding="3" AutoGenerateColumns="False">
							<AlternatingItemStyle CssClass="pot-item-alternado"></AlternatingItemStyle>
							<ItemStyle CssClass="pot-item-normal"></ItemStyle>
							<HeaderStyle CssClass="pot-cabecera"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="cod_cliente" HeaderText="Cod. Cliente"></asp:BoundColumn>
								<asp:BoundColumn DataField="nom_cliente" HeaderText="Raz&#243;n Social"></asp:BoundColumn>
								<asp:BoundColumn DataField="nom_ejec_com" HeaderText="Ejec. Comercial"></asp:BoundColumn>
								<asp:BoundColumn DataField="nom_vend_virtual" HeaderText="Vend. Virtual"></asp:BoundColumn>
								<asp:BoundColumn DataField="nom_cobrador" HeaderText="Cobrador"></asp:BoundColumn>
								<asp:BoundColumn DataField="cod_cla_antalis" HeaderText="Cod.Cla&lt;br&gt;Antalis"></asp:BoundColumn>
								<asp:BoundColumn DataField="dmc_cla_antalis" HeaderText="Dmc Cla. Antalis"></asp:BoundColumn>
								<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="Update" CancelText="Cancel" EditText="ver"></asp:EditCommandColumn>
							</Columns>
						</asp:DataGrid></DIV>
				</DIV>
			</DIV>
		</FIELDSET>
	</DIV>
</wilson:masterpage>
