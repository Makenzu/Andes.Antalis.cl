<%@ Page Language="vb" AutoEventWireup="false" Codebehind="DetalleOC.aspx.vb" Inherits="app.WebForm3"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>WebForm3</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<div id="TituloVentanaEmergente-acs">
				<div>Ordenes de compra para el material</div>
				<div><asp:label id="LCodigoProd" runat="server">Label</asp:label><asp:label style="Z-INDEX: 0" id="lNombreProd" runat="server">Label</asp:label></div>
			</div>
			<asp:datagrid id="dgResultado" runat="server" AutoGenerateColumns="False" BorderColor="#CC9966"
				BorderStyle="Solid" BorderWidth="1px" BackColor="White" CellPadding="4" ShowFooter="True"
				Width="100%">
				<FooterStyle ForeColor="#330099" BackColor="#FFFFCC"></FooterStyle>
				<SelectedItemStyle Font-Bold="True" ForeColor="#663399" BackColor="#FFCC66"></SelectedItemStyle>
				<ItemStyle ForeColor="#330099" BackColor="White"></ItemStyle>
				<HeaderStyle Font-Bold="True" ForeColor="#FFFFCC" BackColor="#990000"></HeaderStyle>
				<Columns>
					<asp:BoundColumn DataField="num_oc" HeaderText="N&#176; GNC"></asp:BoundColumn>
					<asp:BoundColumn DataField="f_ingreso_sli" HeaderText="FEC. INGRESO SLI" DataFormatString="{0:dd/MM/yyyy}"></asp:BoundColumn>
					<asp:BoundColumn DataField="cant_solicitada" HeaderText="CANT. SOLICITADA" DataFormatString="{0:#,##0.00}"></asp:BoundColumn>
					<asp:BoundColumn Visible="False" DataField="cod_unid_med" HeaderText="UMED"></asp:BoundColumn>
					<asp:BoundColumn DataField="cant_solicitada_umb" HeaderText="CANT UMB" DataFormatString="{0:#,##0}"></asp:BoundColumn>
					<asp:BoundColumn Visible="False" DataField="umb_cant_solicitada" HeaderText="UMB"></asp:BoundColumn>
				</Columns>
				<PagerStyle HorizontalAlign="Center" ForeColor="#330099" BackColor="#FFFFCC"></PagerStyle>
			</asp:datagrid></form>
	</body>
</HTML>
