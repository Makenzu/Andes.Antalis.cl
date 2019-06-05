<%@ Page Language="vb" AutoEventWireup="false" Codebehind="WebForm1.aspx.vb" Inherits="app.WebForm1"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>WebForm1</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<asp:DataGrid id="DataGrid1" style="Z-INDEX: 101; LEFT: 520px; POSITION: absolute; TOP: 288px"
				runat="server" AutoGenerateColumns="False" Width="200px">
				<Columns>
					<asp:BoundColumn DataField="columna_A" HeaderText="Columna A" DataFormatString="{0:#,##0.00}"></asp:BoundColumn>
					<asp:TemplateColumn HeaderText="Columna B">
						<ItemStyle HorizontalAlign=Left></ItemStyle>
						<EditItemTemplate>
							<asp:TextBox id="TextBox1" Width="60" CssClass="textBox" runat="server" Font-Names="Arial, Helvetica, sans-serif" Font-Size="9px"></asp:TextBox>
						</EditItemTemplate>
					</asp:TemplateColumn>
				</Columns>
			</asp:DataGrid>
		</form>
	</body>
</HTML>
