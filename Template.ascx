<%@ Control Language="vb" AutoEventWireup="false" Codebehind="Template.ascx.vb" Inherits="app.Template" targetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="Wilson" Namespace="Wilson.MasterPages" Assembly="WilsonMasterPages" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<HTML>
	<HEAD>
		<title>
			<wilson:contentregion id="MPTitle" runat="server">Title</wilson:contentregion></title>
		<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1">
		<LINK rel="stylesheet" type="text/css" href="/css/andes.css">
		<LINK rel="stylesheet" type="text/css" href="/css/menu.css">
		<LINK rel="stylesheet" type="text/css" href="/css/calendar.css">
		<LINK rel="stylesheet" type="text/css" href="/css/jqModal.css">
		<LINK rel="stylesheet" type="text/css" href="/css/jquery.autocomplete.css">
		<script type="text/javascript" src="/js/jquery-1.5.1.js"></script>
		<script type="text/javascript" src="/js/jquery.autocomplete.js"></script>
		<script type="text/javascript" src="/js/popup.js"></script>
		<script type="text/javascript" src="/js/utiles.js"></script>
		<script type="text/javascript" src="/js/jqModal.js"></script>
	</HEAD>
	<body leftMargin="0" topMargin="0">
		<form id="frmMain" method="post" runat="server">
			<%  If Session("CTE_ANDES_USUARIO_AUTENTICADO") = "SI" Then %>
			<script type="text/javascript" src="/js/common.js"></script>
			<script type="text/javascript" src="/js/coolmenus4.js"></script>
			<script type="text/javascript">
/***
This is the menu creation code - place it right after you body tag
Feel free to add this to a stand-alone js file and link it to your page.
**/

//Menu object creation
oCMenu=new makeCM("oCMenu") //Making the menu object. Argument: menuname

//Menu properties
oCMenu.pxBetween=2
oCMenu.fromLeft=200
oCMenu.fromTop=56
oCMenu.rows=1
oCMenu.menuPlacement="right";
oCMenu.offlineRoot="file:///d|/"
oCMenu.onlineRoot=""
oCMenu.resizeCheck=1
oCMenu.wait=400
oCMenu.fillImg="cm_fill.gif"
oCMenu.zIndex=0

//Background bar properties
oCMenu.useBar=0
oCMenu.barWidth="menu"
oCMenu.barHeight="menu"
oCMenu.barClass="clBar"
oCMenu.barX="menu"
oCMenu.barY="menu"
oCMenu.barBorderX=0
oCMenu.barBorderY=0
oCMenu.barBorderClass=""

//Netscape 4 and Opera form work-around !! !! !! !! !!
if(bw.ns4 || bw.op5 || bw.op6){

  oCMenu.onshow="document.layers?document.layers.formLayer.visibility='hidden':document.getElementById('formDiv').style.visibility='hidden';"
  oCMenu.onhide="document.layers?document.layers.formLayer.visibility='visible':document.getElementById('formDiv').style.visibility='visible';"
}

//Level properties - ALL properties have to be spesified in level 0
oCMenu.level[0]=new cm_makeLevel() //Add this for each new level
oCMenu.level[0].width=110
oCMenu.level[0].height=16
oCMenu.level[0].regClass="clLevel0"
oCMenu.level[0].overClass="clLevel0over"
oCMenu.level[0].borderX=1
oCMenu.level[0].borderY=1
oCMenu.level[0].offsetX=-2
oCMenu.level[0].offsetY=0
oCMenu.level[0].rows=0
oCMenu.level[0].arrow=0
oCMenu.level[0].arrowWidth=0
oCMenu.level[0].arrowHeight=0
oCMenu.level[0].align="bottom"
oCMenu.level[0].borderClass="clLevel0border"


//EXAMPLE SUB LEVEL[1] PROPERTIES - You have to specify the properties you want different from LEVEL[0] - If you want all items to look the same just remove this
oCMenu.level[1]=new cm_makeLevel() //Add this for each new level (adding one to the number)
oCMenu.level[1].width=oCMenu.level[0].width+5
oCMenu.level[1].height=22
oCMenu.level[1].regClass="clLevel1"
oCMenu.level[1].overClass="clLevel1over"
oCMenu.level[1].borderX=1
oCMenu.level[1].borderY=1
oCMenu.level[1].align="left"
oCMenu.level[1].offsetX= -(oCMenu.level[0].width-2)/2+20
oCMenu.level[1].offsetY=0
oCMenu.level[1].borderClass="clLevel1border"


//EXAMPLE SUB LEVEL[2] PROPERTIES - You have to specify the properties you want different from LEVEL[1] OR LEVEL[0] - If you want all items to look the same just remove this
oCMenu.level[2]=new cm_makeLevel() //Add this for each new level (adding one to the number)
oCMenu.level[2].width=155
oCMenu.level[2].height=30
oCMenu.level[2].offsetX=0
oCMenu.level[2].offsetY=0
oCMenu.level[2].regClass="clLevel2"
oCMenu.level[2].overClass="clLevel2over"
oCMenu.level[2].borderClass="clLevel2border"
			</script>
			<script language="javascript">

			<% response.write(LoadMenu()) %>

// ESTE SIEMPRE AL ULTIMO


oCMenu.makeMenu('top20','','|&nbsp;Salir&nbsp;|','/logout.aspx','','47','18','','')

//Leave this line - it constructs the menu
oCMenu.construct()
			</script>
			<% 	End If %>
			<TABLE border="0" cellSpacing="0" cellPadding="0" width="100%" align="center">
				<TBODY>
					<TR>
						<td bgColor="#1d6db1" background="images/bkg_header_iddeo.gif">
							<TABLE id="Table12" border="0" cellSpacing="0" cellPadding="4">
								<TR>
									<TD><IMG alt="" src="/images/logo_Andes.gif" width="177" height="43"></TD>
								</TR>
							</TABLE>
						</td>
						<TD bgColor="#1c6db0" vAlign="top" width="16%" align="center">
							<TABLE id="Table2" border="0" cellSpacing="0" cellPadding="0" width="449" align="right">
								<TR>
									<TD background="/images/top_header_iddeo.jpg">
										<DIV><IMG alt="" src="/images/transparent.gif" width="10" height="55"></DIV>
									</TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<!--
					<TR>
						<TD bgColor="#ffc511" colSpan="2" height="1"></TD>
					</TR> //-->
					<TR>
						<TD style="BORDER-BOTTOM: black 1px solid; BORDER-TOP: black 1px solid" bgColor="#75a3ce"
							colSpan="2">
							<%  If Session("CTE_ANDES_USUARIO_AUTENTICADO") = "SI" Then %>
							<TABLE id="Table3" border="0" cellSpacing="0" cellPadding="0" width="100%" bgColor="lightsteelblue"
								height="19">
								<TR>
									<td><IMG vspace="1" align="textTop" src="/images/ico_usuario_small.gif" width="11" height="17">&nbsp;<asp:label id="lbUsuario" runat="server" CssClass="usuario" ForeColor="black"></asp:label></td>
									<TD></TD>
								</TR>
							</TABLE>
							<asp:label id="lbfilialActual" runat="server" CssClass="usuario" ForeColor="black">FILIAL</asp:label>
							<asp:dropdownlist id="ddlFilial" runat="server" CssClass="listBox" AutoPostBack="True"></asp:dropdownlist>
							<%end if%>
						</TD>
					</TR>
				</TBODY>
			</TABLE>
			<div align="center">
				<!-- Contenido Starts //-->
				<TABLE id="Table1" border="0" cellSpacing="0" cellPadding="0" width="100%">
					<TR>
						<TD style="BORDER-BOTTOM: darkgray 1px solid; HEIGHT: 23px" bgColor="#eeeeee" height="23"
							vAlign="top" align="center">
							<DIV class="PageTitle"><wilson:contentregion id="MPCaption" runat="server">Page 
      Caption</wilson:contentregion></DIV>
						</TD>
					</TR>
					<TR>
						<TD vAlign="top" align="center"><wilson:contentregion id="MPContent" runat="server">Default 
      Content</wilson:contentregion>
						</TD>
					</TR>
				</TABLE>
				<!-- Contenido ENDS //--></div>
			<TABLE border="0" cellSpacing="0" cellPadding="0" width="100%" align="center">
				<TR>
					<TD background="images/rell_puntos_gris.gif"><IMG src="images/rell_puntos_gris.gif" width="2" height="1"></TD>
				</TR>
				<TR>
					<TD vAlign="middle">
						<TABLE border="0" cellSpacing="2" cellPadding="0" width="100%">
							<TR>
								<TD style="BORDER-BOTTOM: azure thin inset" height="20" colSpan="2"></TD>
							</TR>
							<TR>
								<TD width="40%">
									<DIV class="PageFooter">Santa Filomena 66 Santiago - Chile.<BR>
										Teléfono:(56-2) 730 00 00
										<BR>
										<FONT color="#0000FF"><B>Antalis</B></FONT></DIV>
									<%--<DIV class="txt-verdana-negro-bold"><A class="verde" href="">info@gms.cl</A></DIV>--%>
								</TD>
								<TD width="60%" align="right"><B class="PageFooter">MIS Antalis</B>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
