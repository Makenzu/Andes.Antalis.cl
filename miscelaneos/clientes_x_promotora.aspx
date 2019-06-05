<%@ Register TagPrefix="Wilson" Namespace="Wilson.MasterPages" Assembly="WilsonMasterPages" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="clientes_x_promotora.aspx.vb" Inherits="app.clientes_x_promotora" %>
<META content="text/html; charset=iso-8859-1" http-equiv="Content-Type">
<LINK href="css/calendar.css" type="text/css" rel="stylesheet">
<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
<wilson:masterpage id="MPContainer" runat="server">
	<WILSON:CONTENTREGION id="MPTitle" runat="server">Clientes 
por&nbsp;Ejecutivo Comercial</WILSON:CONTENTREGION>
	<WILSON:CONTENTREGION id="MPCaption" runat="server">Clientes por&nbsp;Ejecutivo 
Comercial</WILSON:CONTENTREGION>
	<TABLE id="Table1" border="0" cellSpacing="0" cellPadding="1" width="100%">
		<TR>
			<TD style="WIDTH: 131px; HEIGHT: 20px" align="right"></TD>
			<TD style="HEIGHT: 20px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
				&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<INPUT title="Imprimir" onclick="javascript:imprimir();return false;" alt="Imprimir" src="/images/imprimir.gif"
					type="image">&nbsp;&nbsp;
				<asp:ImageButton id="ibExportar2" title="Exportar a Excel" runat="server" Visible="false" ToolTip="Exportar a Excel"
					ImageUrl="/images/exportar.gif"></asp:ImageButton></TD>
		</TR>
		<TR>
			<TD style="WIDTH: 131px" align="right"></TD>
			<TD></TD>
		</TR>
		<TR>
			<TD style="WIDTH: 131px" vAlign="top" align="left">
				<TABLE id="Table2" border="0" cellSpacing="0" cellPadding="0" width="100%">
					<TR>
						<TD style="HEIGHT: 15px" class="txt-SoftMessage "></TD>
						<TD style="HEIGHT: 15px" align="right"></TD>
					</TR>
					<TR>
						<TD class="txt-SoftMessage " colSpan="2">
							<P>Ejec.Comercial:&nbsp;
								<asp:DropDownList id="ddlPromotoras" runat="server" Font-Size="XX-Small" Font-Names="Arial" AutoPostBack="True"></asp:DropDownList></P>
						</TD>
					</TR>
				</TABLE>
			</TD>
			<TD vAlign="top" align="left">
				<P>&nbsp;</P>
				<P>&nbsp;</P>
				<P>
					<asp:Label id="lbErrors" runat="server" Visible="False"></asp:Label></P>
			</TD>
		</TR>
		<TR>
			<TD colSpan="2">
				<asp:Table id="mytable" runat="server" HorizontalAlign="Center" Width="100%" CellSpacing="1"></asp:Table></TD>
		</TR>
	</TABLE>
</wilson:masterpage>
