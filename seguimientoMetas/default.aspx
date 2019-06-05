<%@ Page Language="vb" AutoEventWireup="false" Codebehind="default.aspx.vb" Inherits="app.seguimientometas_default" %>
<%@ Register TagPrefix="Wilson" Namespace="Wilson.MasterPages" Assembly="WilsonMasterPages" %>
<wilson:masterpage style="Z-INDEX: 0" id="MPContainer" runat="server">
	<WILSON:CONTENTREGION id="MPTitle" runat="server">Seguimiento de 
metas</WILSON:CONTENTREGION>
	<WILSON:CONTENTREGION id="MPCaption" runat="server">Seguimiento de metas</WILSON:CONTENTREGION> <!-- #dialog is the id of a DIV defined in the code below -->
	<SCRIPT language="javascript">
$(document).ready(function()   
{   
    $(".tab_content").hide(); 
 
    $("ul.tabs li").click(function()   
       {   
        $("ul.tabs li").removeClass("active");   
        $(this).addClass("active");   
        $(".tab_content").hide();   
  
        var activeTab = $(this).find("a").attr("href");   
        $(activeTab).fadeIn();   
        return false;   
    });            
    
	<asp:Literal id="Literal1" runat="server"></asp:Literal>
    
});
	</SCRIPT>
	<DIV class="pot-contenedor-controles">
		<FIELDSET style="Z-INDEX: 0">
			<LEGEND class="pot-groupbox-potencial">Seguimiento de 
metas</LEGEND>
			<DIV class="pot-contenedor-controles">
				<UL class="tabs"> <!-- LI id="op1">
						<A href="#tab1">Célula</A>
					<LI id="op2">
						<A href="#tab2">Cartera</A -->
					<LI id="op3">
						<A href="#tab3">Venta Teléfono</A>
					<LI id="op4">
						<A href="#tab4">Venta Público</A>
					<LI id="op5">
						<A href="#tab5">KAM/TeleMarketing</A>
					<LI id="op6">
						<A href="#tab5">Sales Advisor</A>
					</LI>
				</UL>
				<DIV class="tab_container"><!-- DIV style="DISPLAY: none" id="tab1" class="tab_content">
						<TABLE border="0" cellSpacing="0" cellPadding="0">
							<TR>
								<TD class="nombre-campo">Célula:&nbsp;</TD>
								<TD align="left">
									<asp:DropDownList id="ddlCelula" runat="server"></asp:DropDownList></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD class="nombre-campo">Año:</TD>
								<TD align="left">
									<asp:DropDownList style="Z-INDEX: 0" id="ddlAno" runat="server"></asp:DropDownList></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD class="nombre-campo">Mes:</TD>
								<TD>
									<asp:DropDownList style="Z-INDEX: 0" id="ddlMes" runat="server"></asp:DropDownList></TD>
								<TD>
									<asp:Button style="Z-INDEX: 0" id="bBuscarPorCelula" runat="server" Text="buscar"></asp:Button></TD>
							</TR>
							<TR>
								<TD class="nombre-campo" height="15" colSpan="3"></TD>
							</TR>
						</TABLE>
						<asp:Panel style="Z-INDEX: 0" id="Panel1" runat="server"></asp:Panel></DIV>
					<DIV style="DISPLAY: none" id="tab2" class="tab_content">
						<TABLE border="0" cellSpacing="0" cellPadding="0">
							<TR>
								<TD class="nombre-campo">Agente:&nbsp;</TD>
								<TD align="left">
									<asp:DropDownList id="ddlAgente" runat="server"></asp:DropDownList></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD class="nombre-campo">Año:</TD>
								<TD align="left">
									<asp:DropDownList style="Z-INDEX: 0" id="ddlAno2" runat="server"></asp:DropDownList></TD>
								<TD>
									<asp:Button style="Z-INDEX: 0" id="bBuscarPorAgente" runat="server" Text="buscar"></asp:Button></TD>
							</TR>
							<TR>
								<TD class="nombre-campo" height="15" colSpan="3"></TD>
							</TR>
						</TABLE>
						<asp:Panel style="Z-INDEX: 0" id="Panel2" runat="server"></asp:Panel></DIV -->
					<DIV style="DISPLAY: none" id="tab3" class="tab_content">
						<TABLE border="0" cellSpacing="0" cellPadding="0">
							<TR>
								<TD class="nombre-campo">Agente:&nbsp;</TD>
								<TD align="left">
									<asp:DropDownList id="ddlVETEL" runat="server"></asp:DropDownList></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD class="nombre-campo">Año:</TD>
								<TD align="left">
									<asp:DropDownList style="Z-INDEX: 0" id="ddlAnoVETEL" runat="server"></asp:DropDownList></TD>
							</TR>
							<TR>
								<TD class="nombre-campo">Mes:</TD>
								<TD align="left">
									<asp:DropDownList style="Z-INDEX: 0" id="ddlMesVETEL" runat="server"></asp:DropDownList></TD>
								<TD>
									<asp:Button style="Z-INDEX: 0" id="bBuscarPorVETEL" runat="server" Text="buscar"></asp:Button></TD>
							</TR>
							<TR>
								<TD class="nombre-campo" height="15" colSpan="3"></TD>
							</TR>
						</TABLE>
						<asp:Panel style="Z-INDEX: 0" id="Panel3" runat="server"></asp:Panel></DIV>
					<DIV style="DISPLAY: none" id="tab4" class="tab_content">
						<TABLE border="0" cellSpacing="0" cellPadding="0">
							<TR>
								<TD class="nombre-campo">Agente:&nbsp;</TD>
								<TD align="left">
									<asp:DropDownList id="ddlVTPUB" runat="server"></asp:DropDownList></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD class="nombre-campo">Año:</TD>
								<TD align="left">
									<asp:DropDownList style="Z-INDEX: 0" id="ddlAnoVTPUB" runat="server"></asp:DropDownList></TD>
							</TR>
							<TR>
								<TD class="nombre-campo">Mes:</TD>
								<TD align="left">
									<asp:DropDownList style="Z-INDEX: 0" id="ddlMesVTPUB" runat="server"></asp:DropDownList></TD>
								<TD>
									<asp:Button style="Z-INDEX: 0" id="bBuscarPorVTPUB" runat="server" Text="buscar"></asp:Button></TD>
							</TR>
							<TR>
								<TD class="nombre-campo" height="15" colSpan="3"></TD>
							</TR>
						</TABLE>
						<asp:Panel style="Z-INDEX: 0" id="Panel4" runat="server"></asp:Panel></DIV>
					<DIV style="DISPLAY: none" id="tab5" class="tab_content">
						<TABLE border="0" cellSpacing="0" cellPadding="0">
							<TR>
								<TD class="nombre-campo">Agente:&nbsp;</TD>
								<TD align="left">
									<asp:DropDownList id="ddlTLM" runat="server"></asp:DropDownList></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD class="nombre-campo">Año:</TD>
								<TD align="left">
									<asp:DropDownList style="Z-INDEX: 0" id="ddlAnoTLM" runat="server"></asp:DropDownList></TD>
							</TR>
							<TR>
								<TD class="nombre-campo">Mes:</TD>
								<TD align="left">
									<asp:DropDownList style="Z-INDEX: 0" id="ddlMesTLM" runat="server"></asp:DropDownList></TD>
								<TD>
									<asp:Button style="Z-INDEX: 0" id="bBuscarPorTLM" runat="server" Text="buscar"></asp:Button></TD>
							</TR>
							<TR>
								<TD class="nombre-campo" height="15" colSpan="3"></TD>
							</TR>
						</TABLE>
						<asp:Panel style="Z-INDEX: 0" id="Panel5" runat="server"></asp:Panel></DIV>
					<DIV style="DISPLAY: none" id="tab6" class="tab_content">
						<TABLE border="0" cellSpacing="0" cellPadding="0">
							<TR>
								<TD class="nombre-campo">Agente:&nbsp;</TD>
								<TD align="left">
									<asp:DropDownList id="ddlSalesAdv" runat="server"></asp:DropDownList></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD class="nombre-campo">Año:</TD>
								<TD align="left">
									<asp:DropDownList style="Z-INDEX: 0" id="ddlAnoSalesAdv" runat="server"></asp:DropDownList></TD>
							</TR>
							<TR>
								<TD class="nombre-campo">Mes:</TD>
								<TD align="left">
									<asp:DropDownList style="Z-INDEX: 0" id="ddlMessalesAdv" runat="server"></asp:DropDownList></TD>
								<TD>
									<asp:Button style="Z-INDEX: 0" id="bBuscarPorSalesAdv" runat="server" Text="buscar"></asp:Button></TD>
							</TR>
							<TR>
								<TD class="nombre-campo" height="15" colSpan="3"></TD>
							</TR>
						</TABLE>
						<asp:Panel style="Z-INDEX: 0" id="Panel6" runat="server"></asp:Panel></DIV>
				</DIV>
			</DIV>
		</FIELDSET>
	</DIV>
</wilson:masterpage>
