<%@ Register TagPrefix="Wilson" Namespace="Wilson.MasterPages" Assembly="WilsonMasterPages" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="vta_x_familia.aspx.vb" Inherits="app.vta_x_familia"%>
<META content="text/html; charset=iso-8859-1" http-equiv="Content-Type">
<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
<wilson:masterpage id="MPContainer" runat="server">
	<WILSON:CONTENTREGION id="MPTitle" runat="server">Análisis Venta 
Por Familia</WILSON:CONTENTREGION>
	<WILSON:CONTENTREGION id="MPCaption" runat="server">Análisis Venta Por Familia<BR>
<asp:Label id="lbFecha" runat="server" EnableViewState="False"></asp:Label></WILSON:CONTENTREGION>
	<SCRIPT language="javascript">
	</SCRIPT>
	<TABLE id="Table5" border="0" cellSpacing="0" cellPadding="0" width="95%" align="center">
		<TR>
			<TD width="70%">
				<asp:Label id="lbErrors" runat="server" CssClass="txt-FatalMessage"></asp:Label></TD>
			<TD width="30%" align="right">
				<TABLE id="Table4" border="0" cellSpacing="0" cellPadding="0">
					<TR>
						<TD>
							<asp:ImageButton id="ibExportar" title="Exportar a Excel" runat="server" EnableViewState="False"
								ImageUrl="/images/exportar.gif" ToolTip="Exportar a Excel" Visible="False"></asp:ImageButton></TD>
						<TD width="5"></TD>
						<TD><INPUT title="Imprimir" onclick="javascript:imprimir();return false;" alt="Imprimir" src="/images/imprimir.gif"
								type="image"></TD>
					</TR>
				</TABLE>
			</TD>
		</TR>
	</TABLE>
	<TABLE id="Table2" border="0" cellSpacing="0" cellPadding="0">
		<TR>
			<TD vAlign="bottom" width="45%">
				<asp:Panel id="plDatos" runat="server" EnableViewState="False" Visible="False" Height="40px"></asp:Panel></TD>
			<TD width="55%">
				<TABLE id="Table1" border="0" cellSpacing="0" cellPadding="2" width="350">
					<TR>
						<TD class="tbl-HorizHeader" height="40" width="80">Periodo:</TD>
						<TD height="40" colSpan="2">
							<asp:DropDownList id="ddlMes" runat="server" CssClass="listBox"></asp:DropDownList>&nbsp;/
							<asp:DropDownList id="ddlAno" runat="server" CssClass="listBox"></asp:DropDownList></TD>
						<TD height="40" width="90"><INPUT id="btSend" onclick="javascript: Validar();return false;" src="/images/procesar.gif"
								type="image" name="btSend">
							<DIV style="DISPLAY: none" id="disbtn" name="disbtn"><IMG alt="" src="/images/procesando.gif"></DIV>
						</TD>
					</TR>
				</TABLE>
			</TD>
		</TR>
		<TR>
			<TD colSpan="2">
				<asp:Label id="lbNota" runat="server" EnableViewState="False" CssClass="txt-SoftMessage" Visible="False"></asp:Label></TD>
		</TR>
		<TR>
			<TD width="50%" colSpan="2">
				<asp:Table id="tbResultado" runat="server" EnableViewState="False" BorderWidth="1px" GridLines="Both"
					CellPadding="1"></asp:Table></TD>
		</TR>
	</TABLE>
</wilson:masterpage>
