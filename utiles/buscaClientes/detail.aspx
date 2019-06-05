<%@ Page Language="vb" AutoEventWireup="false" Codebehind="detail.aspx.vb" Inherits="app.detail" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>detail</title>
		<META http-equiv="Content-Type" content="text/html; charset=windows-1252">
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript">
<!--
		function setSelection(code)
		{
		/*
			if(parent.opener.document.forms[0].txCliente){
					parent.opener.document.forms[0].txCliente.value = code;
			}
				
			if (parent.opener.document.forms[0].txICliente){
				parent.opener.document.forms[0].txICliente.value = code;
			}
				*/
			parent.window.returnValue=code;  
			//parent.opener.document.forms[0].submit();
			
			parent.close();
		}	
	
//-->
		</script>
		<LINK href="../../css/andes.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body leftMargin="2" topMargin="2">
		<form id="Form1" method="post" runat="server">
			<asp:Label id="lbMsg" runat="server" EnableViewState="False" CssClass="txt-AlertMessage" DESIGNTIMEDRAGDROP="69"></asp:Label>
			<asp:datagrid id="dgClientes" runat="server" AutoGenerateColumns="False" Width="440px" CellPadding="2">
				<ItemStyle Font-Size="8pt" Font-Names="Verdana"></ItemStyle>
				<HeaderStyle Font-Size="7pt" Font-Names="Verdana" Font-Bold="True" ForeColor="White" BackColor="#3B4D6B"></HeaderStyle>
				<Columns>
					<asp:TemplateColumn>
						<HeaderStyle Width="16px"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<A href="JavaScript:" onclick='setSelection("<%#Trim(Container.DataItem("cod_cliente")) %>");'>
								<IMG height="16" alt="Elegir" src="../../images/check.jpg" width="16" border="0"></A>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn DataField="cod_cliente" ReadOnly="True" HeaderText="CLIENTE">
						<HeaderStyle Width="80px"></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="nom_cliente" ReadOnly="True" HeaderText="RAZON SOCIAL"></asp:BoundColumn>
				</Columns>
			</asp:datagrid><BR>
			<asp:Label id="Label1" runat="server">Para cursar una búsqueda, indique código de cliente GMS o razón social y presione botón BUSCAR.</asp:Label></form>
	</body>
</HTML>
