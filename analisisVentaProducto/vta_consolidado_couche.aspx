<%@ Register TagPrefix="Wilson" Namespace="Wilson.MasterPages" Assembly="WilsonMasterPages" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="vta_consolidado_couche.aspx.vb" Inherits="app.vta_consolidado_couche" %>
<META content="text/html; charset=iso-8859-1" http-equiv="Content-Type">
<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
<wilson:masterpage id="MPContainer" runat="server">
	<WILSON:CONTENTREGION id="MPTitle" runat="server">Análisis Venta 
Consolidado Couché</WILSON:CONTENTREGION>
	<WILSON:CONTENTREGION id="MPCaption" runat="server">Análisis Venta Consolidado Couché<BR>
<asp:Label id="lbFecha" runat="server" EnableViewState="False"></asp:Label></WILSON:CONTENTREGION>
	<SCRIPT language="javascript">
function Validar(){

	var f = document.forms[0];
	var rtn = true;

	msPrd = MM_findObj('rfvCodProd')	
	msFam = MM_findObj('rfvFamilia')	
	msSub = MM_findObj('rfvSubfam')	
				
	if (f.grpBuscar[0].checked){
		valFam = MM_findObj('txFamilia')
		if (MM_trim(valFam.value)==''){
			msFam.style.display =  "inline";
			msSub.style.display =  "none";
			msPrd.style.display =  "none";
			rtn=false;
		}
	}else	if (f.grpBuscar[1].checked){
		valSub = MM_findObj('txSubfam')
		if (MM_trim(valSub.value)==''){
			msFam.style.display =  "none";
			msSub.style.display =  "inline";
			msPrd.style.display =  "none";
			rtn=false;
		}
	}else	if (f.grpBuscar[2].checked){
		valSub = MM_findObj('txCodProd')
		if (MM_trim(valSub.value)==''){
			msFam.style.display =  "none";
			msSub.style.display =  "none";
			msPrd.style.display =  "inline";
			rtn=false;
		}
	}
	
	if(rtn){
		noDblClick('disbtn','btSend');
		f.submit();
	}
	
	return rtn;

}

function wasClicked(obj){
	var f = document.forms[0];
	if (obj.name== 'txFamilia' || obj.id== 'rbCodFam'){
		if (obj.id != 'rbCodFam') f.grpBuscar[0].checked=true;
		MM_findObj('txCodProd').value='';
		MM_findObj('txSubfam').value='';
	}else if  (obj.name== 'txSubfam' || obj.id== 'rbSubfam'){
		if (obj.id != 'rbSubfam') f.grpBuscar[1].checked=true;
		MM_findObj('txFamilia').value='';
		MM_findObj('txCodProd').value='';
	}else if  (obj.name== 'txCodProd' || obj.id== 'rbCodProd'){
		if (obj.id != 'rbCodProd') f.grpBuscar[2].checked=true;
		MM_findObj('txSubfam').value='';
		MM_findObj('txFamilia').value='';
	}
}

function showDatos(cp,ms,an){
	var mywin
	var param
	var winl = (screen.width - 400) / 2;
	var wint = (screen.height - 330) / 2; 
	param = "width=400,height=370,Top="+wint+",Left="+winl+",toolbar=0,scrollbars=1,location=0,statusbar=0,menubar=0,resizable=1";
	mywin = window.open("vta_x_item_mes_detalle.aspx?cp="+cp+"&ms="+ms+"&an="+an,"detalle",param );
	mywin.focus();
}

function doHide(sf, n,obj){
	var vRow
	for (i=0 ;i<=n ;i++){
		vRow = MM_findObj(sf + i)
		if (vRow.style.display ==  "none"){
			vRow.style.display =  "inline";
			//obj.src="images/up_arrow_gray.gif"
		}else{
			vRow.style.display =  "none";	
			//obj.src="images/down_arrow_gray.gif";
		}
	}
}
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
								Visible="False" ToolTip="Exportar a Excel" ImageUrl="/images/exportar.gif"></asp:ImageButton></TD>
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
						<TD class="tbl-HorizHeader" height="40" width="80">Centro:</TD>
						<TD height="40" colSpan="2">
							<asp:DropDownList id="ddlCentro" runat="server" CssClass="listBox"></asp:DropDownList></TD>
						<TD height="40" width="90"></TD>
					</TR>
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
				<asp:Table id="tbResultado" runat="server" EnableViewState="False" CellPadding="1" GridLines="Both"
					BorderWidth="1px"></asp:Table></TD>
		</TR>
	</TABLE>
</wilson:masterpage>
