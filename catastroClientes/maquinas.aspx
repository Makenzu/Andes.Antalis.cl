<%@ Register TagPrefix="Wilson" Namespace="Wilson.MasterPages" Assembly="WilsonMasterPages" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="maquinas.aspx.vb" Inherits="app.maquinas"%>
<META content="text/html; charset=iso-8859-1" http-equiv="Content-Type">
<LINK rel="stylesheet" type="text/css" href="http://localhost/css/andes.css">
<P>
	<meta name="vs_snapToGrid" content="False">
	<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	<wilson:masterpage style="Z-INDEX: 0" id="MPContainer" runat="server">
		<WILSON:CONTENTREGION id="MPTitle" runat="server">Catastro Clientes</WILSON:CONTENTREGION>
		<WILSON:CONTENTREGION id="MPCaption" runat="server">Máquinas</WILSON:CONTENTREGION>
		<P style="Z-INDEX: 0">
			<asp:Panel id="Panel1" runat="server">
				<P>[<A href="manMaquinas.aspx">Adm. máquinas</A>] [<A href="manMedidasPlanchas.aspx">Adm. 
						medidas de planchas</A>] [<A href="manMedidasMantillas.aspx">Adm. medidas de 
						mantillas</A>]</P>
			</asp:Panel>
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
		<P></P>
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
						<asp:BoundColumn Visible="False" DataField="id_maquina" HeaderText="id_maquina">
							<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="dmc_maquina" HeaderText="M&#225;quina">
							<HeaderStyle Width="190px"></HeaderStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="cant_maquinas" HeaderText="Cant. M&#225;quinas">
							<HeaderStyle HorizontalAlign="Right" Width="58px"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn Visible="False" DataField="id_tamanho_maquina" HeaderText="id_tamanho_maquina"></asp:BoundColumn>
						<asp:BoundColumn DataField="dmc_tamanho_maquina" HeaderText="Medida&lt;br&gt;M&#225;quina">
							<HeaderStyle HorizontalAlign="Center" Width="117px"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
							<FooterStyle HorizontalAlign="Center"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="num_cuerpos_impresores" HeaderText="N&#176;&lt;br&gt;Cuerpos&lt;br&gt;Impresores">
							<HeaderStyle HorizontalAlign="Right" Width="72px"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="con_torre_barniz" HeaderText="Torre&lt;br&gt;Barniz">
							<HeaderStyle HorizontalAlign="Center" Width="70px"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn Visible="False" DataField="id_tamanho_plancha" HeaderText="id_tamanho_plancha"></asp:BoundColumn>
						<asp:BoundColumn DataField="dmc_tamanho_plancha" HeaderText="Medida&lt;br&gt;Planchas">
							<HeaderStyle HorizontalAlign="Right" Width="80px"></HeaderStyle>
							<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="dmc_tamanho_mantilla" HeaderText="Medida&lt;br&gt;Mantilla">
							<HeaderStyle HorizontalAlign="Right" Width="80px"></HeaderStyle>
							<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="consumo_papel" HeaderText="Cons. Papel [Ton/mes]" DataFormatString="{0:#,##0.0}">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="consumo_planchas" HeaderText="Cons. Planchas [Ues/mes]" DataFormatString="{0:#,##0}">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="consumo_tintas_proceso" HeaderText="Cons. Tintas Proceso [Kg/mes]" DataFormatString="{0:#,##0.0}">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="consumo_tintas_pantone" HeaderText="Cons. Tintas Pantone [Kg/mes]" DataFormatString="{0:#,##0.0}">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="consumo_barniz" HeaderText="Consumo Barniz [Kg/mes]" DataFormatString="{0:#,##0.0}">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="consumo_mantillas" HeaderText="Consumo Mantillas [Ues/mes]" DataFormatString="{0:#,##0}">
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
