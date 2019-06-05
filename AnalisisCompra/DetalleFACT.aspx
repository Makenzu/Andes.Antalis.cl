<%@ Page Language="vb" AutoEventWireup="false" Codebehind="DetalleFACT.aspx.vb" Inherits="app.WebForm5"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>WebForm5</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<div id="TituloVentanaEmergente-acs">
				<div>Facturas para el material</div>
				<div>
					<asp:Label id="lCodigoprod" runat="server">Label</asp:Label>
					<asp:Label id="lNombreProd" runat="server">Label</asp:Label>
				</div>
			</div>
			<asp:DataGrid id="dgResultado" runat="server" Width="100%" AutoGenerateColumns="False" BorderColor="#CC9966"
				BorderStyle="Solid" BorderWidth="1px" BackColor="White" CellPadding="4" ShowFooter="True">
				<FooterStyle ForeColor="#330099" BackColor="#FFFFCC"></FooterStyle>
				<SelectedItemStyle Font-Bold="True" ForeColor="#663399" BackColor="#FFCC66"></SelectedItemStyle>
				<ItemStyle ForeColor="#330099" BackColor="White"></ItemStyle>
				<HeaderStyle Font-Bold="True" ForeColor="#FFFFCC" BackColor="#990000"></HeaderStyle>
				<Columns>
					<asp:BoundColumn DataField="origen" HeaderText="ORIGEN"></asp:BoundColumn>
					<asp:BoundColumn DataField="documento" HeaderText="N&#176; DOCUMENTO"></asp:BoundColumn>
					<asp:BoundColumn DataField="f_documento" HeaderText="FECHA DOCUMENTO" DataFormatString="{0:dd/MM/yyyy}"></asp:BoundColumn>
					<asp:BoundColumn DataField="cant_documento" HeaderText="CANTIDAD" DataFormatString="{0:##,0}"></asp:BoundColumn>
					<asp:BoundColumn Visible="False" DataField="um_cant_documento" HeaderText="UMED"></asp:BoundColumn>
					<asp:BoundColumn DataField="cant_documento_umb" HeaderText="CANTIDAD UMB" DataFormatString="{0:##,0}"></asp:BoundColumn>
					<asp:BoundColumn Visible="False" DataField="umb_cant_documento" HeaderText="UMB"></asp:BoundColumn>
				</Columns>
				<PagerStyle HorizontalAlign="Center" ForeColor="#330099" BackColor="#FFFFCC"></PagerStyle>
			</asp:DataGrid>
		</form>
	</body>
</HTML>
