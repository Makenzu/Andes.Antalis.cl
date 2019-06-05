<%@ Page Language="vb" AutoEventWireup="false" Codebehind="listado_productos_total.aspx.vb" Inherits="app.listado_productos_total" %>
<%@ Register TagPrefix="Wilson" Namespace="Wilson.MasterPages" Assembly="WilsonMasterPages" %>
<META content="text/html; charset=iso-8859-1" http-equiv="Content-Type">
<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
<wilson:masterpage id="MPContainer" runat="server">
	<WILSON:CONTENTREGION id="MPTitle" runat="server">Lista de Precios 
Totales</WILSON:CONTENTREGION>
	<WILSON:CONTENTREGION id="MPCaption" runat="server">Lista de Precios Totales<BR></WILSON:CONTENTREGION>
	<SCRIPT language="javascript">
function Validar(){
	var f = document.forms[0]
	var msgObj = MM_findObj('rfvPromo')

	if (MM_trim(f.txPromo.value) == '') {
		msgObj.style.display =  "inline";
		return false;
	}
	else{
		//msgObj.style.display =  "none";
		noDblClick('disbtn','btSend');
		f.submit();
		return true;
	}
}
	</SCRIPT>
	<TABLE id="Table1" border="0" cellSpacing="0" cellPadding="0" width="95%" align="center">
		<TR>
			<TD style="WIDTH: 970px; HEIGHT: 25px" width="970" align="right"><INPUT title="Imprimir" onclick="javascript:imprimir();return false;" alt="Imprimir" src="/images/imprimir.gif"
					type="image">
				<asp:ImageButton id="ibExportar2" title="Exportar a Excel" runat="server" ImageUrl="/images/exportar.gif"
					ToolTip="Exportar a Excel" Visible="false"></asp:ImageButton></TD>
		</TR>
		<TR>
			<TD style="WIDTH: 970px" width="970" align="left">
				<TABLE id="Table5" border="0" cellSpacing="0" cellPadding="0">
					<TR>
						<TD class="tbl-HorizHeader">Sucursal&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TD>
						<TD colSpan="2">
							<asp:DropDownList id="ddlSucursal" runat="server" Width="200px" CssClass="listBox" AutoPostBack="False"></asp:DropDownList></TD>
						<TD></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 75px; HEIGHT: 17px" class="tbl-HorizHeader">Fecha&nbsp;&nbsp; :</TD>
						<TD style="HEIGHT: 17px" colSpan="2" align="left">
							<asp:DropDownList id="ddlMeses" runat="server" CssClass="listBox" AutoPostBack="True">
								<asp:ListItem Value="1" Selected="True">Enero</asp:ListItem>
								<asp:ListItem Value="2">Febrero</asp:ListItem>
								<asp:ListItem Value="3">Marzo</asp:ListItem>
								<asp:ListItem Value="4">Abril</asp:ListItem>
								<asp:ListItem Value="5">Mayo</asp:ListItem>
								<asp:ListItem Value="6">Junio</asp:ListItem>
								<asp:ListItem Value="7">Julio</asp:ListItem>
								<asp:ListItem Value="8">Agosto</asp:ListItem>
								<asp:ListItem Value="9">Septiembre</asp:ListItem>
								<asp:ListItem Value="10">Octubre</asp:ListItem>
								<asp:ListItem Value="11">Noviembre</asp:ListItem>
								<asp:ListItem Value="12">Diciembre</asp:ListItem>
							</asp:DropDownList>/
							<asp:DropDownList id="ddlanos" runat="server" CssClass="listBox" AutoPostBack="True"></asp:DropDownList></TD>
						<TD style="HEIGHT: 17px" align="left"><INPUT id="btenvia" onclick="javascript: Validar();return false;" src="/images/procesar.gif"
								type="image" name="btSend" runat="server"></TD>
					</TR>
				</TABLE>
				&nbsp;
				<asp:label id="Label4" runat="server" CssClass="txt-FatalMessage"></asp:label></TD>
			<TD style="WIDTH: 1px"></TD>
		</TR>
		<TR>
		</TR>
	</TABLE>
	<TABLE id="Table2" border="0" cellSpacing="0" cellPadding="1" align="left">
		<TR>
			<TD style="WIDTH: 2px" vAlign="bottom" width="2" align="left"></TD>
			<TD style="WIDTH: 902px" align="center">
				<asp:Table id="mitabla" runat="server" HorizontalAlign="Center" CellSpacing="1" CellPadding="2"></asp:Table></TD>
		</TR>
		<TR>
			<TD style="WIDTH: 909px" colSpan="2" align="center">
				<asp:Label id="lbErrors" runat="server" CssClass="txt-FatalMessage"></asp:Label></TD>
		</TR>
	</TABLE>
</wilson:masterpage><asp:label id="Label1" runat="server" Visible="False" CssClass="txt-FatalMessage"></asp:label><asp:label id="Label2" runat="server" Visible="False" CssClass="txt-FatalMessage"></asp:label><asp:label id="Label3" runat="server" Visible="False" CssClass="txt-FatalMessage"></asp:label>
