<%@ Page Language="vb" AutoEventWireup="false" Codebehind="DetalleProf.aspx.vb" Inherits="app.WebForm4"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>WebForm4</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<div id="TituloVentanaEmergente-acs">
				<div>Proformas para el material</div>
				<div><asp:label id="LCodigoProd" runat="server">Label</asp:label><asp:label id="lNombreProd" runat="server">Label</asp:label></div>
			</div>
			<asp:datagrid id="dgResultado" runat="server" CellPadding="4" BackColor="White" BorderWidth="1px"
				BorderStyle="Solid" BorderColor="#CC9966" ShowFooter="True" AutoGenerateColumns="False">
				<FooterStyle ForeColor="#330099" BackColor="#FFFFCC"></FooterStyle>
				<SelectedItemStyle Font-Bold="True" ForeColor="#663399" BackColor="#FFCC66"></SelectedItemStyle>
				<ItemStyle ForeColor="#330099" BackColor="White"></ItemStyle>
				<HeaderStyle Font-Bold="True" ForeColor="#FFFFCC" BackColor="#990000"></HeaderStyle>
				<Columns>
					<asp:BoundColumn DataField="origen" HeaderText="ORIGEN"></asp:BoundColumn>
					<asp:BoundColumn DataField="documento" HeaderText="DOCUMENTO"></asp:BoundColumn>
					<asp:BoundColumn DataField="num_documento" HeaderText="N&#176; DOCUMENTO"></asp:BoundColumn>
					<asp:BoundColumn DataField="f_documento" HeaderText="FEC. DOCUMENTO" DataFormatString="{0:dd/MM/yyyy}"></asp:BoundColumn>
					<asp:BoundColumn DataField="f_confirmacion" HeaderText="FEC. CONFIRMACI&#211;N" DataFormatString="{0:dd/MM/yyyy}"></asp:BoundColumn>
					<asp:BoundColumn DataField="cant_documento" HeaderText="CANTIDAD" DataFormatString="{0:##,0}"></asp:BoundColumn>
					<asp:BoundColumn Visible="False" DataField="um_cant_documento" HeaderText="CANTIDAD UM"></asp:BoundColumn>
					<asp:BoundColumn DataField="cant_documento_umb" HeaderText="CANTIDAD UMB" DataFormatString="{0:##,0}"></asp:BoundColumn>
					<asp:BoundColumn Visible="False" DataField="umb_cant_documento" HeaderText="CANTIDAD UMB(DCTO)"></asp:BoundColumn>
					<asp:BoundColumn DataField="num_oc_vinc" HeaderText="OC VINC."></asp:BoundColumn>
					<asp:BoundColumn DataField="num_prof_vinc" HeaderText="PROF. VINC"></asp:BoundColumn>
				</Columns>
				<PagerStyle HorizontalAlign="Center" ForeColor="#330099" BackColor="#FFFFCC"></PagerStyle>
			</asp:datagrid></form>
	</body>
</HTML>
