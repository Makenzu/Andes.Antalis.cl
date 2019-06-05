<%@ Register TagPrefix="Wilson" Namespace="Wilson.MasterPages" Assembly="WilsonMasterPages" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="vta_mar_subfamilia_item.aspx.vb" Inherits="app.vta_mar_subfamilia_item"%>
<META content="text/html; charset=iso-8859-1" http-equiv="Content-Type">
<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
<wilson:masterpage id="MPContainer" runat="server">
<WILSON:CONTENTREGION id="MPTitle" runat="server">ANDES</WILSON:CONTENTREGION> 
<WILSON:CONTENTREGION id="MPCaption" runat="server">Venta Subfamilia - Items<BR>
<asp:Label id="lbFecha" runat="server"></asp:Label></WILSON:CONTENTREGION>
<SCRIPT language="javascript">
		function Validar(){
			
			var f = document.forms[0];
			var rtn = true;
			
			if(f.grpBuscar){
				if (f.grpBuscar[0].checked){
					if (f.txSubfam.value==""){
						document.all.rfvSubfam.style.display =  "inline";
						document.all.rfvCodProd.style.display =  "none";
						rtn =  false;
					}
				}else	if (f.grpBuscar[1].checked){
					if (f.txCodProd.value==""){
						document.all.rfvCodProd.style.display =  "inline";
						document.all.rfvSubfam.style.display =  "none";
						rtn =  false;
					}
				}
			}
			
			if (rtn) {
				noDblClick('disbtn','btSend');
				f.submit();
			}
		}
		
	</SCRIPT>

<TABLE id="Table2" border="0" cellSpacing="0" cellPadding="1" width="100%">
		<TR>
			<TD colSpan="2" align="left">
				<TABLE id="Table4" border="0" cellSpacing="0" cellPadding="0" width="95%" align="center">
					<TR>
						<TD width="70%">
							<asp:Label id="lbErrors" runat="server" CssClass="txt-FatalMessage"></asp:Label></TD>
						<TD width="30%" align="right"><A href="javascript: imprimir();"><IMG border="0" alt="Imprimir" src="/images/imprimir.gif"></A></TD>
					</TR>
				</TABLE>
			</TD>
		</TR>
		<TR>
			<TD vAlign="bottom" width="40%" align="left">
				<asp:Panel id="plDatos" runat="server" CssClass="tbl-HorizHeader" Visible="False" Width="400px"
					EnableViewState="False">
					<TABLE id="Table3" border="1" cellSpacing="0" cellPadding="3">
						<TR>
							<TD class="tbl-HorizHeader" width="90">
								<asp:Label id="lbSearch" runat="server"></asp:Label>&nbsp;:</TD>
							<TD align="right">
								<asp:Label id="lbData" runat="server" CssClass="tbl-HorizItem"></asp:Label></TD>
						</TR>
					</TABLE>
				</asp:Panel></TD>
			<TD width="60%">
				<asp:Panel id="plParams" runat="server">
					<DIV style="WIDTH: 300px; DISPLAY: none; COLOR: red" id="rfvSubfam" class="txt-AlertMessage"
						align="center">* Debe ingresar un código de Subfamilia.</DIV>
					<DIV style="WIDTH: 300px; DISPLAY: none; COLOR: red" id="rfvCodProd" class="txt-AlertMessage"
						align="center">* Debe ingresar un código de Producto</DIV>
					<TABLE id="tbParams" border="0" cellSpacing="0" cellPadding="1" width="300">
						<TR>
							<TD class="tbl-HorizHeader" height="27" width="150">
								<asp:RadioButton id="rbSubfam" runat="server" Text="Subfamilia :" GroupName="grpBuscar" Checked="True"></asp:RadioButton></TD>
							<TD height="27" width="150">
								<TABLE id="Table6" border="0" cellSpacing="0" cellPadding="0">
									<TR>
										<TD>
											<asp:TextBox id="txSubfam" title="Ingrese código de subfamilia" runat="server" CssClass="textBox"
												Width="70px" MaxLength="4"></asp:TextBox></TD>
										<TD><INPUT title="Buscar código de subfamilia" onclick="javascript: findSubfamilia('txSubfam');return false;"
												src="/images/buscar.gif" type="image"></TD>
									</TR>
								</TABLE>
							</TD>
						</TR>
						<TR>
							<TD class="tbl-HorizHeader" height="27" width="150">
								<asp:RadioButton id="rbCodProd" runat="server" Text="Cod. Producto : " GroupName="grpBuscar"></asp:RadioButton></TD>
							<TD class="tbl-HorizHeader" height="27" width="150">
								<asp:TextBox id="txCodProd" runat="server" CssClass="textBox" Width="80px" MaxLength="12"></asp:TextBox></TD>
						</TR>
					</TABLE>
				</asp:Panel>
				<TABLE id="Table5" border="0" cellSpacing="0" cellPadding="2" width="350">
					<TR>
						<TD class="tbl-HorizHeader" height="40" width="80">Periodo:</TD>
						<TD height="40" colSpan="2">
							<asp:DropDownList id="ddlMes" runat="server" CssClass="listBox"></asp:DropDownList>&nbsp;/
							<asp:DropDownList id="ddlAno" runat="server" CssClass="listBox"></asp:DropDownList></TD>
						<TD height="40" width="90"><INPUT id="btSend" onclick="javascript: Validar();return false;" src="/images/procesar.gif"
								type="image">
							<DIV style="DISPLAY: none" id="disbtn" name="disbtn"><IMG alt="" src="/images/procesando.gif"></DIV>
						</TD>
					</TR>
				</TABLE>
			</TD>
		</TR>
		<TR>
			<TD vAlign="bottom" width="40%" colSpan="2" align="left">
				<asp:Label id="lbNota" runat="server" CssClass="txt-SoftMessage"></asp:Label></TD>
		</TR>
	</TABLE><BR>
<asp:DataGrid id="dgResultado" runat="server" CellPadding="3" AutoGenerateColumns="False" AllowSorting="True"
		ShowFooter="True">
		<FooterStyle Font-Bold="True" HorizontalAlign="Right" CssClass="tbl-DataGridFooter"></FooterStyle>
		<AlternatingItemStyle CssClass="tbl-DataGridItemAlternating"></AlternatingItemStyle>
		<ItemStyle CssClass="tbl-DataGridItem"></ItemStyle>
		<HeaderStyle CssClass="tbl-DataGridHeader"></HeaderStyle>
		<Columns>
			<asp:BoundColumn DataField="cod_producto" SortExpression="cod_producto desc" ReadOnly="True" HeaderText="Cod. Prod."></asp:BoundColumn>
			<asp:BoundColumn DataField="des_producto" SortExpression="des_producto desc" ReadOnly="True" HeaderText="Nom. Prod."></asp:BoundColumn>
			<asp:BoundColumn DataField="val_cantidad" SortExpression="val_cantidad desc" ReadOnly="True" HeaderText="Qta. Mes">
				<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
				<ItemStyle HorizontalAlign="Right"></ItemStyle>
			</asp:BoundColumn>
			<asp:BoundColumn DataField="val_precio_dolar" SortExpression="val_precio_dolar desc" ReadOnly="True"
				HeaderText="Vta. Mes (US$)">
				<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
				<ItemStyle HorizontalAlign="Right"></ItemStyle>
			</asp:BoundColumn>
			<asp:BoundColumn DataField="val_precio_pesos" SortExpression="val_precio_pesos desc" ReadOnly="True"
				HeaderText="Vta. Mes ($)">
				<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
				<ItemStyle HorizontalAlign="Right"></ItemStyle>
			</asp:BoundColumn>
			<asp:BoundColumn DataField="val_costo_dolar" SortExpression="val_costo_dolar desc" ReadOnly="True"
				HeaderText="Costo Mes (US$)">
				<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
				<ItemStyle HorizontalAlign="Right"></ItemStyle>
			</asp:BoundColumn>
			<asp:BoundColumn DataField="val_costo_pesos" SortExpression="val_costo_pesos desc" ReadOnly="True"
				HeaderText="Costo Mes ($)">
				<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
				<ItemStyle HorizontalAlign="Right"></ItemStyle>
			</asp:BoundColumn>
			<asp:BoundColumn DataField="val_cantidad_ac" SortExpression="val_cantidad_ac desc" ReadOnly="True"
				HeaderText="Qta. A&#241;o">
				<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
				<ItemStyle HorizontalAlign="Right"></ItemStyle>
			</asp:BoundColumn>
			<asp:BoundColumn DataField="val_precio_dolar_ac" SortExpression="val_precio_dolar_ac desc" ReadOnly="True"
				HeaderText="Vta. A&#241;o (US$)">
				<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
				<ItemStyle HorizontalAlign="Right"></ItemStyle>
			</asp:BoundColumn>
			<asp:BoundColumn DataField="val_costo_pesos_ac" SortExpression="val_costo_pesos_ac desc" ReadOnly="True"
				HeaderText="Vta. A&#241;o ($)">
				<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
				<ItemStyle HorizontalAlign="Right"></ItemStyle>
			</asp:BoundColumn>
			<asp:BoundColumn DataField="stock_actual" ReadOnly="True" HeaderText="Stock Act.">
				<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
				<ItemStyle HorizontalAlign="Right"></ItemStyle>
			</asp:BoundColumn>
			<asp:BoundColumn DataField="stock_pend" ReadOnly="True" HeaderText="Stock Pend.">
				<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
				<ItemStyle HorizontalAlign="Right"></ItemStyle>
			</asp:BoundColumn>
		</Columns>
	</asp:DataGrid></TD></TR><TR>
		<TD></TD>
	</TR></TBODY></TABLE></wilson:masterpage>
