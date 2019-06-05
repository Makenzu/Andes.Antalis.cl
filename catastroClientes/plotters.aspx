<%@ Register TagPrefix="Wilson" Namespace="Wilson.MasterPages" Assembly="WilsonMasterPages" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="plotters.aspx.vb" Inherits="app.plotters" %>
<META content="text/html; charset=iso-8859-1" http-equiv="Content-Type">
<LINK rel="stylesheet" type="text/css" href="http://localhost/css/andes.css">
<P>
	<meta name="vs_snapToGrid" content="False">
	<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	<wilson:masterpage style="Z-INDEX: 0" id="MPContainer" runat="server">
		<WILSON:CONTENTREGION id="MPTitle" runat="server">Catastro Clientes</WILSON:CONTENTREGION>
		<WILSON:CONTENTREGION id="MPCaption" runat="server">Plotters</WILSON:CONTENTREGION>
		<P><BR>
			<TABLE id="Table1" border="1" cellSpacing="1" cellPadding="1">
				<TR>
					<TD colSpan="2">
						<DIV class="texto-normal">Ingrese código de cliente y a continuación presione 
							"buscar"</DIV>
					</TD>
				</TR>
				<TR>
					<TD class="tbl-HorizHeader">Cliente:</TD>
					<TD>
						<asp:TextBox id="tbCodigoCliente" runat="server" CssClass="textBox" MaxLength="12"></asp:TextBox>
						<asp:Button style="Z-INDEX: 0" id="bBuscarCliente" runat="server" Text="buscar"></asp:Button></TD>
				</TR>
			</TABLE>
		</P>
		<P>
			<DIV class="texto-normal">
				<asp:Label style="Z-INDEX: 0" id="lRazonSocial" runat="server"></asp:Label></DIV>
		<P></P>
		<asp:Panel id="pDatosCatastro" runat="server">
			<P>
				<asp:DataGrid style="Z-INDEX: 0" id="dgMaquinas" runat="server" AutoGenerateColumns="False" CellPadding="2">
					<AlternatingItemStyle CssClass="tbl-DataGridItemAlternating"></AlternatingItemStyle>
					<ItemStyle CssClass="tbl-DataGridItem"></ItemStyle>
					<HeaderStyle CssClass="tbl-DataGridHeader"></HeaderStyle>
					<Columns>
						<asp:BoundColumn DataField="id_registro" HeaderText="ID Registro">
							<HeaderStyle HorizontalAlign="Center" Width="49px"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="dmc_plotter" HeaderText="Ploter">
							<HeaderStyle Width="190px"></HeaderStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="cant_maquinas" HeaderText="Cantidad">
							<HeaderStyle HorizontalAlign="Right" Width="58px"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="dmc_tipo_tinta" HeaderText="Tipo Tinta"></asp:BoundColumn>
						<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="Update" CancelText="Cancel" EditText="editar">
							<HeaderStyle Width="42px"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
						</asp:EditCommandColumn>
						<asp:ButtonColumn Text="borrar" CommandName="Delete">
							<HeaderStyle Width="42px"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
						</asp:ButtonColumn>
					</Columns>
				</asp:DataGrid></P>
			<P>
				<DIV class="texto-normal">
					<asp:Label style="Z-INDEX: 0" id="lMensaje" runat="server"></asp:Label></DIV>
			<P></P>
			<P>
				<asp:Button style="Z-INDEX: 0" id="bIngresaNuevoPlotter" runat="server" Text="Ingresar nuevo registro"></asp:Button></P>
		</asp:Panel>
	</wilson:masterpage>
