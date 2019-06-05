<%@ Page Language="vb" AutoEventWireup="false" Codebehind="get_subfamilias.aspx.vb" Inherits="app.get_subfamilias"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Eliga Subfamilia</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../css/andes.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		function Validar(){
			
			var f = document.forms[0];
			if (f.ddlAreas.selectedIndex ==0){
				rfvArea.style.display =  "inline";
			}else if (f.ddlFamilias.selectedIndex ==0){
				rfvFamilia.style.display =  "inline";
			}else if (f.ddlSubfamilia.selectedIndex ==0){
				rfvSubfamilia.style.display =  "inline";
			}else{
				window.opener.focus();
				window.opener.document.forms[0].txSubfam.value = f.ddlSubfamilia[f.ddlSubfamilia.selectedIndex].value
				//window.opener.document.forms[0].submit();
				window.close();
			}
						
		}
		
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="300" align="center" border="0">
				<TR>
					<TD vAlign="bottom">
						<TABLE id="Table4" cellSpacing="0" cellPadding="0" border="0">
							<TR>
								<TD bgColor="whitesmoke"><IMG alt="" src="../images/lupa_small.gif" align="middle"></TD>
								<TD><IMG alt="" src="../images/tab_subfamilia.gif"></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD bgColor="whitesmoke" colSpan="2">
						<TABLE id="tbParams" cellSpacing="0" cellPadding="2" width="340" align="center" border="0">
							<TR>
								<TD class="tbl-HorizHeader" width="5" colSpan="5" height="15"><asp:label id="lbErrors" runat="server" CssClass="txt-FatalMessage"></asp:label></TD>
							</TR>
							<TR>
								<TD class="tbl-HorizHeader" width="5"></TD>
								<TD class="tbl-HorizHeader" width="90">Area</TD>
								<TD class="tbl-HorizHeader" width="2">:</TD>
								<TD><asp:dropdownlist id="ddlAreas" runat="server" AutoPostBack="True" CssClass="listBox"></asp:dropdownlist></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD class="tbl-HorizHeader" width="5" style="HEIGHT: 22px"></TD>
								<TD class="tbl-HorizHeader" width="90" style="HEIGHT: 22px">Familia</TD>
								<TD class="tbl-HorizHeader" style="HEIGHT: 22px">:</TD>
								<TD style="HEIGHT: 22px"><asp:dropdownlist id="ddlFamilias" runat="server" AutoPostBack="True" CssClass="listBox"></asp:dropdownlist></TD>
								<TD style="HEIGHT: 22px"></TD>
							</TR>
							<TR>
								<TD class="tbl-HorizHeader" width="5"></TD>
								<TD class="tbl-HorizHeader" width="90">Subfamilia</TD>
								<TD class="tbl-HorizHeader">:</TD>
								<TD><asp:dropdownlist id="ddlSubfamilia" runat="server" EnableViewState="False" CssClass="listBox"></asp:dropdownlist></TD>
								<TD><INPUT type="image" value="Elegir" src="../images/aceptar.gif" onclick="javascript: Validar();"></TD>
							</TR>
							<TR>
								<TD height="22"></TD>
								<TD colSpan="4" height="22">
									<DIV id="rfvArea" style="DISPLAY: none; WIDTH: 200px; COLOR: red; HEIGHT: 15px" ms_positioning="FlowLayout"
										class="txt-AlertMessage">* Debe selecionar un area.</DIV>
									<DIV id="rfvFamilia" style="DISPLAY: none; WIDTH: 200px; COLOR: red; HEIGHT: 15px" ms_positioning="FlowLayout"
										class="txt-AlertMessage">* Debe selecionar una familia.</DIV>
									<DIV id="rfvSubfamilia" style="DISPLAY: none; WIDTH: 200px; COLOR: red; HEIGHT: 15px"
										ms_positioning="FlowLayout" class="txt-AlertMessage">* Debe selecionar una 
										subfamilia.</DIV>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
