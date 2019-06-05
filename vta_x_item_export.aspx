<%@ Register TagPrefix="Wilson" Namespace="Wilson.MasterPages" Assembly="WilsonMasterPages" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="vta_x_item_export.aspx.vb"%>		<%--Inherits="app.vta_x_item_export"--%>
<META http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
<wilson:masterpage id="MPContainer" runat="server">
	<WILSON:CONTENTREGION id="MPTitle" runat="server">ANDES</WILSON:CONTENTREGION>
	<WILSON:CONTENTREGION id="MPCaption" runat="server">Ventas 
Item<BR>
<asp:Label id="lbFecha" runat="server"></asp:Label></WILSON:CONTENTREGION>
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
	<TABLE id="Table5" cellSpacing="0" cellPadding="0" width="95%" align="center" border="0">
		<TR>
			<TD width="70%">
				<asp:Label id="lbErrors" runat="server" CssClass="txt-FatalMessage"></asp:Label></TD>
			<TD align="right" width="30%">
				<asp:LinkButton id="lbExport" runat="server">Exportar a Excel</asp:LinkButton>&nbsp;
				<IMG onclick="javascript:imprimir();" alt="Imprimir" src="images/imprimir.gif" border="0"><A href="javascript: imprimir();"></A></TD>
		</TR>
	</TABLE>
	<TABLE id="Table2" cellSpacing="0" cellPadding="0" border="0">
		<TR>
			<TD vAlign="bottom" width="45%">
				<asp:Panel id="plDatos" runat="server" Height="40px" Visible="False">
					<TABLE id="Table3" cellSpacing="0" cellPadding="3" border="1">
						<TR>
							<TD class="tbl-HorizHeader">Familia :</TD>
							<TD align="left">&nbsp;
								<asp:Label id="lbData" runat="server" CssClass="tbl-HorizItem"></asp:Label></TD>
						</TR>
					</TABLE>
				</asp:Panel></TD>
			<TD width="55%">
				<asp:Panel id="plParams" runat="server">
					<DIV class="txt-AlertMessage" id="rfvSubfam" style="DISPLAY: none; WIDTH: 300px; COLOR: red"
						align="center">* Debe ingresar un código de Subfamilia.</DIV>
					<DIV class="txt-AlertMessage" id="rfvCodProd" style="DISPLAY: none; WIDTH: 300px; COLOR: red"
						align="center">* Debe ingresar un código de Producto</DIV>
					<DIV class="txt-AlertMessage" id="rfvFamilia" style="DISPLAY: none; WIDTH: 300px; COLOR: red"
						align="center">* Debe ingresar un código de Familia.</DIV>
					<TABLE id="tbParams" cellSpacing="0" cellPadding="1" width="300" border="0">
						<TR>
							<TD class="tbl-HorizHeader" width="150" height="27">
								<asp:RadioButton id="rbCodFam" runat="server" Text="Familia :" GroupName="grpBuscar" Checked="True"></asp:RadioButton></TD>
							<TD width="150" height="27">
								<TABLE id="Table7" cellSpacing="0" cellPadding="0" border="0">
									<TR>
										<TD>
											<asp:TextBox id="txFamilia" runat="server" CssClass="textBox" MaxLength="4" Width="70px"></asp:TextBox></TD>
										<TD><INPUT title="Buscar código de familia" onclick="javascript: findFamilia('txFamilia');return false;"
												type="image" src="images/buscar.gif"></TD>
									</TR>
								</TABLE>
							</TD>
						</TR>
						<TR>
							<TD class="tbl-HorizHeader" width="150" height="27">
								<asp:RadioButton id="rbSubfam" runat="server" Text="Subfamilia :" GroupName="grpBuscar"></asp:RadioButton></TD>
							<TD width="150" height="27">
								<TABLE id="Table6" cellSpacing="0" cellPadding="0" border="0">
									<TR>
										<TD>
											<asp:TextBox id="txSubfam" title="Ingrese código de subfamilia" runat="server" CssClass="textBox"
												MaxLength="4" Width="70px"></asp:TextBox></TD>
										<TD><INPUT title="Buscar código de subfamilia" onclick="javascript: findSubfamilia('txSubfam');return false;"
												type="image" src="images/buscar.gif"></TD>
									</TR>
								</TABLE>
							</TD>
						</TR>
						<TR>
							<TD class="tbl-HorizHeader" width="150" height="27">
								<asp:RadioButton id="rbCodProd" runat="server" Text="Cod. Producto : " GroupName="grpBuscar"></asp:RadioButton></TD>
							<TD class="tbl-HorizHeader" width="150" height="27">
								<asp:TextBox id="txCodProd" runat="server" CssClass="textBox" MaxLength="12" Width="80px"></asp:TextBox></TD>
						</TR>
					</TABLE>
				</asp:Panel>
				<TABLE id="Table1" cellSpacing="0" cellPadding="2" width="350" border="0">
					<TR>
						<TD class="tbl-HorizHeader" width="80" height="40">Periodo:</TD>
						<TD colSpan="2" height="40">
							<asp:DropDownList id="ddlMes" runat="server" CssClass="listBox"></asp:DropDownList>&nbsp;/
							<asp:DropDownList id="ddlAno" runat="server" CssClass="listBox"></asp:DropDownList></TD>
						<TD width="90" height="40"><INPUT id="btSend" onclick="javascript: Validar();return false;" type="image" src="images/aceptar.gif"
								name="btSend">
							<DIV id="disbtn" style="DISPLAY: none" name="disbtn"><IMG alt="" src="images/procesando.gif"></DIV>
						</TD>
					</TR>
				</TABLE>
			</TD>
		</TR>
		<TR>
			<TD colSpan="2">
				<asp:Label id="lbNota" runat="server" CssClass="txt-SoftMessage" Visible="False"></asp:Label></TD>
		</TR>
		<TR>
			<TD width="50%" colSpan="2">
				<asp:DataGrid id="dgResultado" runat="server"></asp:DataGrid></TD>
		</TR>
	</TABLE>
</wilson:masterpage>
