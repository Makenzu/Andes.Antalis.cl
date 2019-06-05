<%@ Page Language="vb" AutoEventWireup="false" Codebehind="listado_productos.aspx.vb" Inherits="app.res_listado_productos"%>
<%@ Register TagPrefix="Wilson" Namespace="Wilson.MasterPages" Assembly="WilsonMasterPages" %>
<META content="text/html; charset=iso-8859-1" http-equiv="Content-Type">
<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
<wilson:masterpage id="MPContainer" runat="server">
	<WILSON:CONTENTREGION id="MPTitle" runat="server">Lista de 
Precios</WILSON:CONTENTREGION>
	<WILSON:CONTENTREGION id="MPCaption" runat="server">Lista de Precios<BR></WILSON:CONTENTREGION>
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
				<asp:ImageButton id="ibExportar" title="Exportar a Excel" runat="server" ImageUrl="/images/exportar.gif"
					ToolTip="Exportar a Excel" Visible="false"></asp:ImageButton></TD>
		</TR>
		<TR>
			<TD style="WIDTH: 970px" width="970" align="left">
				<TABLE id="Table5" border="0" cellSpacing="0" cellPadding="0">
					<TR>
						<TD class="tbl-HorizHeader">Sucursal&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TD>
						<TD colSpan="2">
							<asp:DropDownList id="ddlSucursal" runat="server" CssClass="listBox" AutoPostBack="False" Width="200px"></asp:DropDownList></TD>
						<TD></TD>
					</TR>
					<TR>
						<TD class="tbl-HorizHeader">Familia&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TD>
						<TD colSpan="2">
							<asp:DropDownList id="cmbFamilia" runat="server" CssClass="listBox" AutoPostBack="True" Width="200px"></asp:DropDownList></TD>
						<TD></TD>
					</TR>
					<TR>
						<TD class="tbl-HorizHeader">Sub-Familia&nbsp;</TD>
						<TD colSpan="2">
							<asp:DropDownList id="ddlSubFamilia" runat="server" CssClass="listBox" AutoPostBack="True" Width="200px"></asp:DropDownList>
							<asp:CheckBoxList id="cblSubFamilia" runat="server" Visible="False" CssClass="listBox" AutoPostBack="True"
								BorderStyle="Double" Font-Size="XX-Small" BorderWidth="1px" CellPadding="0" CellSpacing="0" RepeatColumns="1"></asp:CheckBoxList>
							<DIV style="DISPLAY: none" id="disbtn" name="disbtn">&nbsp;</DIV>
						</TD>
						<TD></TD>
					</TR>
					<TR>
						<TD class="tbl-HorizHeader">Fecha&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						</TD>
						<TD colSpan="2" align="left">
							<asp:DropDownList id="ddlMes" runat="server" CssClass="listBox" AutoPostBack="True">
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
							<asp:DropDownList id="ddlYear" runat="server" CssClass="listBox" AutoPostBack="True"></asp:DropDownList></TD>
						<TD align="left"><INPUT id="btSend" onclick="javascript: Validar();return false;" src="/images/procesar.gif"
								type="image" name="btSend" runat="server"></TD>
					</TR>
					<TR>
						<TD class="tbl-HorizHeader"></TD>
						<TD colSpan="2" align="right"></TD>
						<TD align="right"></TD>
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
			<TD style="WIDTH: 902px" align="left"></TD>
		</TR>
		<TR>
			<TD style="WIDTH: 909px" colSpan="2" align="center">
				<asp:Table id="tblResultados" runat="server" Width="650px" CellPadding="2" CellSpacing="1"></asp:Table>
				<asp:Label id="lbErrors" runat="server" CssClass="txt-FatalMessage"></asp:Label></TD>
		</TR>
	</TABLE>
</wilson:masterpage><asp:label id="Label1" runat="server" Visible="False" CssClass="txt-FatalMessage"></asp:label><asp:label id="Label2" runat="server" Visible="False" CssClass="txt-FatalMessage"></asp:label><asp:label id="Label3" runat="server" Visible="False" CssClass="txt-FatalMessage"></asp:label>
