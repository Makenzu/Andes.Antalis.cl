<%@ Register TagPrefix="Wilson" Namespace="Wilson.MasterPages" Assembly="WilsonMasterPages" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="opciones_busqueda.aspx.vb" Inherits="app.WebForm3" %>
<META http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<LINK href="css/calendar.css" type="text/css" rel="stylesheet">
<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
<wilson:masterpage id="MPContainer" runat="server">
	<WILSON:CONTENTREGION id="MPTitle" runat="server">MAILING</WILSON:CONTENTREGION>
	<WILSON:CONTENTREGION id="MPCaption" runat="server">Búsqueda de Entidades</WILSON:CONTENTREGION>
	<asp:Label id="lbError" runat="server" CssClass="txt-FatalMessage"></asp:Label>
	<SCRIPT language="javascript" src="js/CalendarPopup.js"></SCRIPT>
	<SCRIPT language="javascript">
			var cal1 = new CalendarPopup(('caldiv1'));		
			var cal2 = new CalendarPopup(('caldiv2'));		
	</SCRIPT>
	<TABLE id="Table1" cellSpacing="0" cellPadding="3" width="100%" border="0">
		<TR>
			<TD align="center">
				<asp:Panel id="plDirecta" runat="server">
					<BR>
					<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="300" align="center" border="0">
						<TR>
							<TD vAlign="bottom">
								<TABLE id="Table4" cellSpacing="0" cellPadding="0" border="0">
									<TR>
										<TD bgColor="whitesmoke"><IMG alt="" src="images/lupa_small.gif" align="middle"></TD>
										<TD><IMG alt="" src="images\directa_on.gif"></TD>
										<TD>
											<asp:ImageButton id="ibDirectaxPromotora" runat="server" ImageUrl="images\por_promotora_off.gif"></asp:ImageButton></TD>
										<TD>
											<asp:ImageButton id="ibDirectaxAgente" runat="server" ImageUrl="images\por_afinidad_off.gif" CausesValidation="False"></asp:ImageButton></TD>
									</TR>
								</TABLE>
							</TD>
							<TD vAlign="baseline" align="center"></TD>
						</TR>
						<TR>
							<TD bgColor="whitesmoke" colSpan="2">
								<TABLE id="tbParams" cellSpacing="0" cellPadding="2" width="350" align="center" border="0">
									<TR>
										<TD class="tbl-HorizHeader" width="5" colSpan="5" height="15"></TD>
									</TR>
									<TR>
										<TD class="tbl-HorizHeader" width="5"></TD>
										<TD class="tbl-HorizHeader" width="120">
											<asp:RadioButton id="rbCodCli" runat="server" Checked="True" Text="Código" GroupName="Grupo_Check_1"></asp:RadioButton></TD>
										<TD class="tbl-HorizHeader" width="2">:</TD>
										<TD colSpan="2">
											<TABLE id="Table11" cellSpacing="0" cellPadding="0" border="0">
												<TR>
													<TD>
														<asp:TextBox id="txCodigo" tabIndex="1" runat="server" CssClass="textBox" Width="90px" MaxLength="12"></asp:TextBox></TD>
													<TD></TD>
												</TR>
											</TABLE>
										</TD>
									</TR>
									<TR>
										<TD class="tbl-HorizHeader" width="5"></TD>
										<TD class="tbl-HorizHeader" width="120">
											<asp:RadioButton id="rbRut" runat="server" Text="Rut" GroupName="Grupo_Check_1"></asp:RadioButton></TD>
										<TD class="tbl-HorizHeader">:</TD>
										<TD width="150">
											<TABLE id="Table2" cellSpacing="0" cellPadding="0" border="0">
												<TR>
													<TD>
														<asp:TextBox id="txRut" tabIndex="2" runat="server" CssClass="textBox" Width="100px"></asp:TextBox></TD>
													<TD align="center">&nbsp;
													</TD>
												</TR>
											</TABLE>
										</TD>
										<TD width="70"></TD>
									</TR>
									<TR>
										<TD class="tbl-HorizHeader" width="5"></TD>
										<TD class="tbl-HorizHeader" width="120">
											<asp:RadioButton id="rbRazonSocial" runat="server" Text="Razón Social" GroupName="Grupo_Check_1"></asp:RadioButton></TD>
										<TD class="tbl-HorizHeader">:</TD>
										<TD>
											<TABLE id="Table2" cellSpacing="0" cellPadding="0" border="0">
												<TR>
													<TD>
														<asp:TextBox id="txRazonSocial" tabIndex="3" runat="server" CssClass="textBox" Width="100px"></asp:TextBox></TD>
													<TD align="center">&nbsp;</TD>
												</TR>
											</TABLE>
										</TD>
										<TD></TD>
									</TR>
									<TR>
										<TD height="20"></TD>
										<TD align="right" colSpan="4" height="20">
											<asp:ImageButton id="btDirecta" tabIndex="4" runat="server" ImageUrl="images/aceptar.gif"></asp:ImageButton></TD>
									</TR>
								</TABLE>
							</TD>
						</TR>
					</TABLE>
				</asp:Panel>
				<asp:Panel id="plxAgente" runat="server" Visible="False">
					<BR>
					<TABLE id="Table5" cellSpacing="0" cellPadding="0" width="300" align="center" border="0">
						<TR>
							<TD style="WIDTH: 411px" vAlign="bottom">
								<TABLE id="Table6" cellSpacing="0" cellPadding="0" border="0">
									<TR>
										<TD>
											<asp:ImageButton id="ibAgentexDirecta" runat="server" ImageUrl="images\directa_off.gif" CausesValidation="False"></asp:ImageButton></TD>
										<TD>
											<asp:ImageButton id="ibAgentexPromotora" runat="server" ImageUrl="images\por_promotora_off.gif" CausesValidation="False"></asp:ImageButton></TD>
										<TD bgColor="#f5f5f5"><IMG alt="" src="images/lupa_small.gif" align="middle"></TD>
										<TD bgColor="whitesmoke"><IMG height="23" alt="" src="images\por_afinidad_on.gif"></TD>
									</TR>
								</TABLE>
							</TD>
						</TR>
						<TR>
							<TD style="WIDTH: 411px" bgColor="#f5f5f5">
								<TABLE id="Table7" cellSpacing="0" cellPadding="2" width="350" border="0">
									<TR>
										<TD style="WIDTH: 348px" width="348" colSpan="10" height="15"></TD>
									</TR>
									<TR>
										<TD style="WIDTH: 71px; HEIGHT: 25px" width="71">Agente&nbsp;</TD>
										<TD style="WIDTH: 6px; HEIGHT: 25px" width="6">:</TD>
										<TD style="WIDTH: 276px; HEIGHT: 25px" noWrap align="left" width="276" colSpan="5">
											<asp:DropDownList id="dlAgente" tabIndex="5" runat="server"></asp:DropDownList></TD>
									</TR>
									<TR>
										<TD style="WIDTH: 348px" align="right" width="348" colSpan="8"></TD>
									</TR>
									<TR>
										<TD style="WIDTH: 348px" align="right" width="348" colSpan="8"></TD>
									</TR>
									<TR>
										<TD style="WIDTH: 348px" align="right" width="348" colSpan="8"></TD>
									</TR>
									<TR>
										<TD style="WIDTH: 348px" align="right" width="348" colSpan="8">
											<asp:ImageButton id="btxAgente" tabIndex="6" runat="server" ImageUrl="images/aceptar.gif"></asp:ImageButton></TD>
									</TR>
									<TR>
										<TD style="WIDTH: 348px" align="right" width="348" colSpan="8"></TD>
									</TR>
								</TABLE>
							</TD>
						</TR>
					</TABLE>
				</asp:Panel>
				<asp:Panel id="plxPromotora" runat="server" Visible="False">
					<BR>
					<TABLE id="Table8" cellSpacing="0" cellPadding="0" width="300" align="center" border="0">
						<TR>
							<TD vAlign="bottom">
								<TABLE id="Table9" cellSpacing="0" cellPadding="0" border="0">
									<TR>
										<TD>
											<asp:ImageButton id="ibPromotoraxDirecta" runat="server" ImageUrl="images\directa_off.gif" CausesValidation="False"></asp:ImageButton></TD>
										<TD bgColor="#f5f5f5"><IMG alt="" src="images/lupa_small.gif" align="middle"></TD>
										<TD bgColor="#f5f5f5"><IMG alt="" src="images\por_promotora_on.gif"></TD>
										<TD>
											<asp:ImageButton id="ibPromotoraxAgente" runat="server" ImageUrl="images\por_afinidad_off.gif" CausesValidation="False"></asp:ImageButton></TD>
									</TR>
								</TABLE>
							</TD>
						</TR>
						<TR>
							<TD bgColor="#f5f5f5">
								<TABLE id="Table10" cellSpacing="0" cellPadding="2" width="350" border="0">
									<TR>
										<TD style="WIDTH: 574px" width="574" colSpan="9" height="15"></TD>
									</TR>
									<TR>
										<TD style="WIDTH: 13px" width="13" height="15">Promotora</TD>
										<TD style="WIDTH: 3px" width="3" height="15">:</TD>
										<TD style="WIDTH: 409px" width="409" colSpan="7" height="15">
											<asp:DropDownList id="dlPromotora" tabIndex="7" runat="server"></asp:DropDownList></TD>
									</TR>
									<TR>
										<TD style="WIDTH: 574px" align="right" width="574" colSpan="9" height="15"></TD>
									</TR>
									<TR>
										<TD style="WIDTH: 574px" align="right" width="574" colSpan="9" height="15"></TD>
									</TR>
									<TR>
										<TD style="WIDTH: 574px" align="right" width="574" colSpan="9" height="15"></TD>
									</TR>
									<TR>
										<TD style="WIDTH: 574px" align="right" width="574" colSpan="9" height="15">
											<asp:ImageButton id="btxPromotora" tabIndex="8" runat="server" ImageUrl="images/aceptar.gif"></asp:ImageButton></TD>
									</TR>
									<TR>
										<TD style="WIDTH: 574px" width="574" colSpan="9" height="15"></TD>
									</TR>
								</TABLE>
							</TD>
						</TR>
					</TABLE>
				</asp:Panel>
				<asp:Label id="lbResult" runat="server">Label</asp:Label></TD>
		</TR>
		<TR>
			<TD align="center" colSpan="2">
				<asp:DataGrid id="dgResultado" runat="server" SelectedIndex="0" AutoGenerateColumns="False" CellPadding="3">
					<AlternatingItemStyle CssClass="tbl-DataGridItemAlternating"></AlternatingItemStyle>
					<ItemStyle CssClass="tbl-DataGridItem"></ItemStyle>
					<HeaderStyle CssClass="tbl-DataGridHeader"></HeaderStyle>
					<Columns>
						<asp:BoundColumn DataField="tipo_entidad" HeaderText="TIPO"></asp:BoundColumn>
						<asp:BoundColumn DataField="cod_entidad" HeaderText="C&#211;DIGO"></asp:BoundColumn>
						<asp:HyperLinkColumn DataNavigateUrlField="url_edit" DataNavigateUrlFormatString="edit_entidad.aspx?{0}"
							DataTextField="nom_entidad" HeaderText="NOMBRE / RAZ&#211;N SOCIAL"></asp:HyperLinkColumn>
						<asp:BoundColumn DataField="rut_entidad" HeaderText="RUT"></asp:BoundColumn>
					</Columns>
				</asp:DataGrid></TD>
		</TR>
		<TR>
			<TD align="center" colSpan="2"></TD>
		</TR>
	</TABLE>
	<DIV id="caldiv1" style="VISIBILITY: hidden; POSITION: absolute; BACKGROUND-COLOR: white; layer-background-color: white"></DIV>
	<DIV id="caldiv2" style="VISIBILITY: hidden; POSITION: absolute; BACKGROUND-COLOR: white; layer-background-color: white"></DIV>
</wilson:masterpage>
