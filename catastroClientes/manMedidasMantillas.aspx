<%@ Register TagPrefix="Wilson" Namespace="Wilson.MasterPages" Assembly="WilsonMasterPages" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="manMedidasMantillas.aspx.vb" Inherits="app.manMedidaMantillas"%>
<META content="text/html; charset=iso-8859-1" http-equiv="Content-Type">
<LINK rel="stylesheet" type="text/css" href="http://localhost/css/andes.css">
<P>
	<meta name="vs_snapToGrid" content="False">
	<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	<wilson:masterpage style="Z-INDEX: 0" id="MPContainer" runat="server">
		<WILSON:CONTENTREGION id="MPTitle" runat="server">Catastro Clientes</WILSON:CONTENTREGION>
		<WILSON:CONTENTREGION id="MPCaption" runat="server">Catastro Máquinas - Medidas de 
mantillas</WILSON:CONTENTREGION>
		<P>
			<asp:Panel id="Panel1" runat="server">
				<P>[<A href="manMaquinas.aspx">Adm. máquinas</A>] [<A href="manMedidasPlanchas.aspx">Adm. 
						medidas de planchas</A>] [<A href="manMedidasMantillas.aspx">Adm. medidas de 
						mantillas</A>]</P>
			</asp:Panel>
			<DIV class="texto-normal">
				<asp:Label id="Label1" runat="server">Label</asp:Label></DIV>
			<asp:Panel id="pDatosCatastro" runat="server">
				<P>
					<asp:DataGrid style="Z-INDEX: 0" id="dgMedidasMantillas" runat="server" AutoGenerateColumns="False"
						CellPadding="2">
						<AlternatingItemStyle CssClass="tbl-DataGridItemAlternating"></AlternatingItemStyle>
						<ItemStyle CssClass="tbl-DataGridItem"></ItemStyle>
						<HeaderStyle CssClass="tbl-DataGridHeader"></HeaderStyle>
						<Columns>
							<asp:BoundColumn DataField="id_tamanho_mantilla" HeaderText="ID">
								<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
							</asp:BoundColumn>
							<asp:BoundColumn DataField="dmc_tamanho_mantilla" HeaderText="Medida mantilla">
								<HeaderStyle Width="190px"></HeaderStyle>
							</asp:BoundColumn>
							<asp:BoundColumn DataField="num_maquinas_asign" HeaderText="Maquinas asignadas" DataFormatString="{0:#,##0}">
								<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
								<ItemStyle HorizontalAlign="Right"></ItemStyle>
							</asp:BoundColumn>
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
					<asp:Button style="Z-INDEX: 0" id="bIngresaNuevaMaquina" runat="server" Text="Ingresar nuevo registro"></asp:Button></P>
			</asp:Panel>
	</wilson:masterpage>
