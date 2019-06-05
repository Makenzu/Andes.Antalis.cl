<%@ Register TagPrefix="Wilson" Namespace="Wilson.MasterPages" Assembly="WilsonMasterPages" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="vta_fisica_item_12mes.aspx.vb" Inherits="app.vta_fisica_item_12mes" %>
<META content="text/html; charset=iso-8859-1" http-equiv="Content-Type">
<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
<wilson:masterpage id="MPContainer" runat="server">
	<WILSON:CONTENTREGION id="MPTitle" runat="server">Análisis 
Venta&nbsp;Item&nbsp;12 Meses</WILSON:CONTENTREGION>
	<WILSON:CONTENTREGION id="MPCaption" runat="server">Análisis Venta&nbsp;Item 12 Meses<BR>
<asp:Label id="lbFecha" runat="server"></asp:Label></WILSON:CONTENTREGION>
	<SCRIPT language="javascript">
function wasClicked(obj){
	var f = document.forms[0];
	if (obj.id== 'ddlFamilia' || obj.id== 'ddlSubFamilia'){
		if (obj.id != 'rbCodFam') f.grpBuscar[0].checked=true;
	}else if  (obj.id== 'ddlProveedor'){
		if (obj.id != 'rbCodProv') f.grpBuscar[1].checked=true;
	}
}
	</SCRIPT>
	<TABLE id="Table2" border="0" cellSpacing="0" cellPadding="0" width="95%" align="center">
		<TR>
			<TD colSpan="2" align="left">
				<asp:Label id="lbErrors" runat="server" CssClass="txt-FatalMessage"></asp:Label>&nbsp;</TD>
		</TR>
		<TR>
			<TD colSpan="2" align="right">
				<asp:ImageButton id="ibVolver" title="Exportar a Excel" runat="server" EnableViewState="False" Visible="False"
					ImageUrl="/images/volver.jpg" ToolTip="Exportar a Excel"></asp:ImageButton>&nbsp;
				<asp:ImageButton id="ibExportar" title="Exportar a Excel" runat="server" EnableViewState="False"
					Visible="False" ImageUrl="/images/exportar.gif" ToolTip="Exportar a Excel"></asp:ImageButton>&nbsp;
				<INPUT title="Imprimir" onclick="javascript:imprimir();return false;" alt="Imprimir" src="/images/imprimir.gif"
					type="image"></TD>
		</TR>
	</TABLE>
	<TABLE id="Table1" border="0" cellSpacing="0" cellPadding="3" align="center">
		<TR>
			<TD vAlign="bottom"></TD>
			<TD width="725" align="center">
				<asp:Panel id="plFamilia" runat="server">
					<BR>
					<TABLE id="Table12" border="0" cellSpacing="0" cellPadding="0" width="300" align="center">
						<TR>
							<TD vAlign="bottom">
								<TABLE id="Table13" border="0" cellSpacing="0" cellPadding="0">
									<TR>
										<TD bgColor="whitesmoke"><IMG alt="" align="middle" src="/images/lupa_small.gif"></TD>
										<TD><IMG alt="" src="/images/por_familia_on.gif"></TD>
										<TD>
											<asp:ImageButton id="ibFamiliaxProveedor" runat="server" ImageUrl="/images/por_proveedor_off.gif"></asp:ImageButton></TD>
										<TD></TD>
									</TR>
								</TABLE>
							</TD>
							<TD style="WIDTH: 4px" vAlign="baseline" align="center"></TD>
						</TR>
						<TR>
							<TD style="WIDTH: 305px" bgColor="whitesmoke" colSpan="2">
								<TABLE id="tbParams" border="0" cellSpacing="0" cellPadding="2" width="350" align="center">
									<TR>
										<TD class="tbl-HorizHeader" height="15" width="5" colSpan="5"></TD>
									</TR>
									<TR>
										<TD style="WIDTH: 79px" class="tbl-HorizHeader" width="79">Centro</TD>
										<TD style="WIDTH: 6px" class="tbl-HorizHeader" width="6">:</TD>
										<TD colSpan="2">
											<asp:DropDownList id="ddlCentro" runat="server" CssClass="listBox" AutoPostBack="True"></asp:DropDownList></TD>
									</TR>
									<TR>
										<TD style="WIDTH: 79px" class="tbl-HorizHeader" width="79">
											<asp:Label id="Label1" runat="server">Familia</asp:Label></TD>
										<TD style="WIDTH: 6px" class="tbl-HorizHeader" width="6">:</TD>
										<TD colSpan="2">
											<P>
												<asp:DropDownList id="ddlFamilia" runat="server" CssClass="listBox" AutoPostBack="True"></asp:DropDownList></P>
										</TD>
									</TR>
									<TR>
										<TD style="WIDTH: 79px" class="tbl-HorizHeader" width="79">
											<asp:Label id="Label2" runat="server">Sub Familia</asp:Label></TD>
										<TD style="WIDTH: 6px" class="tbl-HorizHeader">:</TD>
										<TD style="WIDTH: 265px" width="265">
											<P>
												<asp:DropDownList id="ddlSubFamilia" runat="server" CssClass="listBox" AutoPostBack="True"></asp:DropDownList></P>
										</TD>
									</TR>
									<TR>
										<TD style="WIDTH: 79px" class="tbl-HorizHeader" width="79" colSpan="3" align="center">
											<P>
												<asp:CheckBoxList id="cblSubFamilia" runat="server" CssClass="listBox" Visible="False" AutoPostBack="True"
													Width="340px" CellPadding="0" BorderStyle="Double" Font-Size="XX-Small" BorderWidth="1px" CellSpacing="0"
													RepeatColumns="1"></asp:CheckBoxList></P>
										</TD>
									</TR>
									<TR>
										<TD style="WIDTH: 79px" class="tbl-HorizHeader" width="79">
											<asp:Label id="Label5" runat="server" CssClass="tbl-HorizHeader">Período</asp:Label></TD>
										<TD style="WIDTH: 6px" class="tbl-HorizHeader">:</TD>
										<TD style="WIDTH: 265px" width="265">
											<asp:DropDownList id="ddlMesFamilia" runat="server" CssClass="listBox"></asp:DropDownList>
											<asp:DropDownList id="ddlAnoFamilia" runat="server" CssClass="listBox"></asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
											<asp:ImageButton id="ibSendFamilia" runat="server" ImageUrl="/images/procesar.gif"></asp:ImageButton></TD>
									</TR>
									<TR>
										<TD class="tbl-HorizHeader" colSpan="3" align="left">
											<asp:CheckBox id="cbIncMesActual" runat="server" CssClass="txt-SoftMessage" Text="Considerar mes actual en cálculo de meses de stock."></asp:CheckBox></TD>
									</TR>
									<TR>
										<TD class="tbl-HorizHeader" colSpan="3" align="left">
											<asp:CheckBox id="cbExcluirVtasIqq" runat="server" CssClass="txt-SoftMessage" Text="Excluir ventas Iquique."></asp:CheckBox></TD>
									</TR>
									<TR>
										<TD class="tbl-HorizHeader" colSpan="3" align="left">
											<TABLE id="Table4" border="0" cellSpacing="0" cellPadding="0" width="100%">
												<TR>
													<TD class="tbl-HorizHeader" vAlign="top">Expresar valores en:</TD>
													<TD>
														<asp:RadioButtonList id="rb1" runat="server" CssClass="txt-SoftMessage" CellPadding="0" BorderWidth="0px"
															CellSpacing="0">
															<asp:ListItem Value="UMB" Selected="True">Unidad Base</asp:ListItem>
															<asp:ListItem Value="UMV">Unidad de Venta</asp:ListItem>
														</asp:RadioButtonList></TD>
												</TR>
											</TABLE>
										</TD>
									</TR>
								</TABLE>
							</TD>
						</TR>
					</TABLE>
				</asp:Panel>
				<P>
					<asp:Panel id="plProveedor" runat="server" Visible="False">
						<BR>
						<TABLE id="Table11" border="0" cellSpacing="0" cellPadding="0" width="300" align="center">
							<TR>
								<TD vAlign="bottom">
									<TABLE id="Table14" border="0" cellSpacing="0" cellPadding="0">
										<TR>
											<TD>
												<asp:ImageButton id="ibProveedorxFamilia" runat="server" ImageUrl="/images/por_familia_off.gif" CausesValidation="False"></asp:ImageButton></TD>
											<TD bgColor="#f5f5f5"><IMG alt="" align="middle" src="/images/lupa_small.gif"></TD>
											<TD bgColor="#f5f5f5"><IMG alt="" src="/images/por_proveedor_on.gif"></TD>
											<TD></TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
							<TR>
								<TD bgColor="#f5f5f5">
									<TABLE id="Table15" border="0" cellSpacing="0" cellPadding="2" width="350">
										<TR>
											<TD height="15" width="5" colSpan="9"></TD>
										</TR>
										<TR>
											<TD class="tbl-HorizHeader" width="73">Centro:</TD>
											<TD width="3">:</TD>
											<TD width="409" colSpan="7">
												<asp:DropDownList id="ddlCentro2" runat="server" CssClass="listBox" AutoPostBack="True"></asp:DropDownList></TD>
										</TR>
										<TR>
											<TD width="73">
												<asp:Label id="Label4" runat="server" CssClass="tbl-HorizHeader">Proveedor</asp:Label></TD>
											<TD width="3">:</TD>
											<TD width="409" colSpan="7">
												<P>
													<asp:DropDownList id="ddlProveedor" runat="server" CssClass="listBox" AutoPostBack="True"></asp:DropDownList></P>
											</TD>
										</TR>
										<TR>
											<TD width="73">
												<asp:Label id="Label3" runat="server" CssClass="tbl-HorizHeader">Período</asp:Label></TD>
											<TD width="3">:</TD>
											<TD width="409" colSpan="7">
												<asp:DropDownList id="ddlMesProveedor" runat="server" CssClass="listBox"></asp:DropDownList>
												<asp:DropDownList id="ddlAnoProveedor" runat="server" CssClass="listBox"></asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
												<asp:ImageButton id="btSendProveedor" runat="server" ImageUrl="/images/procesar.gif"></asp:ImageButton></TD>
										</TR>
										<TR>
											<TD height="15" colSpan="9" align="left">
												<asp:CheckBox id="cbIncMesActual2" runat="server" CssClass="txt-SoftMessage" Text="Considerar mes actual en cálculo de meses de stock."></asp:CheckBox></TD>
										</TR>
										<TR>
											<TD height="15" colSpan="9" align="left">
												<asp:CheckBox id="cbExcluirVtasIqq2" runat="server" CssClass="txt-SoftMessage" Text="Excluir ventas Iquique"></asp:CheckBox></TD>
										</TR>
										<TR>
											<TD height="15" colSpan="9" align="left">
												<TABLE id="Table3" border="0" cellSpacing="0" cellPadding="0" width="100%">
													<TR>
														<TD class="tbl-HorizHeader" vAlign="top">Expresar valores en:</TD>
														<TD>
															<asp:RadioButtonList id="rb2" runat="server" CssClass="txt-SoftMessage" CellPadding="0" BorderWidth="0px"
																CellSpacing="0">
																<asp:ListItem Value="UMB" Selected="True">Unidades Base</asp:ListItem>
																<asp:ListItem Value="UMV">Unidades de Venta</asp:ListItem>
															</asp:RadioButtonList></TD>
													</TR>
												</TABLE>
											</TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
						</TABLE>
					</asp:Panel>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
				</P>
			</TD>
		</TR>
		<TR>
			<TD colSpan="2" align="center">
				<asp:Label id="lbNombreCentro" runat="server" CssClass="listBox" Visible="False"></asp:Label></TD>
		</TR>
		<TR>
			<TD style="WIDTH: 2px; HEIGHT: 54px" colSpan="2">
				<P>
					<asp:Table id="tblResultados" runat="server" Width="800px" CellPadding="2" CellSpacing="1"></asp:Table></P>
				<P>
					<asp:Label id="lbNota" runat="server" CssClass="txt-SoftMessage" Visible="False"></asp:Label></P>
			</TD>
		</TR>
	</TABLE>
</wilson:masterpage>
