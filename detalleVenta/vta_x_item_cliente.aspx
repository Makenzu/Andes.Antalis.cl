<%@ Page Language="vb" AutoEventWireup="false" Codebehind="vta_x_item_cliente.aspx.vb" Inherits="app.vta_x_item_cliente"%>
<%@ Register TagPrefix="Wilson" Namespace="Wilson.MasterPages" Assembly="WilsonMasterPages" %>
<META content="text/html; charset=iso-8859-1" http-equiv="Content-Type">
<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
<wilson:masterpage id="MPContainer" runat="server">
	<P>
		<WILSON:CONTENTREGION id="MPTitle" runat="server">Ventas por&nbsp;Item - 
Cliente</WILSON:CONTENTREGION>
		<WILSON:CONTENTREGION id="MPCaption" runat="server">Ventas por&nbsp;Item - Cliente<BR>
<asp:Label id="lbFecha" runat="server"></asp:Label></WILSON:CONTENTREGION>
		<SCRIPT language="javascript">
function Validar(){
	var f = document.forms[0];
	var valObj , msObj
	valObj = MM_findObj('txCodProd')
	msObj = MM_findObj('rfvCodProd')
			
	if (valObj.value == '') {
		msObj.style.display =  "inline";
		return false;
	}else{
		msObj.style.display =  "none";
		noDblClick('disbtn','btSend');
		f.submit();
		return true;
	}

}
		</SCRIPT>
	</P>
	<TABLE id="Table2" border="0" cellSpacing="0" cellPadding="0" width="98%" align="center">
		<TR>
			<TD style="WIDTH: 582px">
				<asp:Label id="lbErrors" runat="server" CssClass="txt-FatalMessage"></asp:Label></TD>
			<TD>
				<TABLE id="Table5" border="0" cellSpacing="0" cellPadding="0" width="95%" align="center">
					<TR>
						<TD width="70%"></TD>
						<TD width="30%" align="right">
							<TABLE id="Table9" border="0" cellSpacing="0" cellPadding="0">
								<TR>
									<TD>
										<asp:ImageButton id="ibExportar" title="Exportar a Excel" runat="server" EnableViewState="False"
											ImageUrl="/images/exportar.gif" ToolTip="Exportar a Excel" Visible="False"></asp:ImageButton></TD>
									<TD width="5"></TD>
									<TD><INPUT title="Imprimir" onclick="javascript:imprimir();return false;" alt="Imprimir" src="/images/imprimir.gif"
											type="image"></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
				</TABLE>
			</TD>
		</TR>
		<TR>
			<TD colSpan="2">
				<TABLE id="Table6" border="0" cellSpacing="0" cellPadding="0">
					<TR>
						<TD width="15"></TD>
						<TD>
							<TABLE id="tbParams" border="0" cellSpacing="0" cellPadding="0">
								<TR>
									<TD class="tbl-HorizHeader" height="27" width="70" colSpan="2">Material:</TD>
									<TD class="tbl-HorizHeader" height="27">
										<asp:TextBox id="txCodProd" runat="server" CssClass="textBox" ToolTip="Ingrese código de producto"
											MaxLength="12" Width="80px"></asp:TextBox></TD>
									<TD class="tbl-HorizHeader" height="27">&nbsp;
										<DIV style="WIDTH: 300px; DISPLAY: none; COLOR: red" id="rfvCodProd" class="txt-AlertMessage"
											align="center">* Debe ingresar un código de Producto</DIV>
									</TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD width="15"></TD>
						<TD>
							<TABLE id="Table1" border="0" cellSpacing="0" cellPadding="0" align="left" height="20">
								<TR>
									<TD class="tbl-HorizHeader" width="70">Periodo:</TD>
									<TD>
										<TABLE id="Table4" border="0" cellSpacing="0" cellPadding="0">
											<TR>
												<TD align="right">
													<asp:DropDownList id="ddlMes" runat="server" CssClass="listBox"></asp:DropDownList></TD>
												<TD width="20" align="center">/</TD>
												<TD>
													<asp:DropDownList id="ddlAno" runat="server" CssClass="listBox"></asp:DropDownList></TD>
											</TR>
										</TABLE>
									</TD>
									<TD><INPUT id="btSend" title="Aceptar" onclick="javascript: Validar();return false;" src="/images/procesar.gif"
											type="image" name="btSend">
										<DIV style="DISPLAY: none" id="disbtn" name="disbtn"><IMG alt="" src="/images/procesando.gif"></DIV>
									</TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
				</TABLE>
			</TD>
		</TR>
		<TR>
			<TD height="25" colSpan="2" align="center"></TD>
		</TR>
		<TR>
			<TD bgColor="#a3bad6" height="25" colSpan="2" align="center">
				<asp:Label id="lbCodProd" runat="server" CssClass="tbl-HorizItem" Font-Bold="True"></asp:Label></TD>
		</TR>
		<TR>
			<TD width="50%" colSpan="2">
				<asp:Label id="lbNota" runat="server" CssClass="txt-SoftMessage" Visible="False"></asp:Label></TD>
		</TR>
		<TR>
			<TD colSpan="2" align="center">
				<asp:DataGrid id="dgResultado" runat="server" Width="100%" ShowFooter="True" CellPadding="2" AutoGenerateColumns="False"
					AllowSorting="True">
					<FooterStyle Wrap="False" CssClass="tbl-DataGridFooter"></FooterStyle>
					<SelectedItemStyle Wrap="False"></SelectedItemStyle>
					<EditItemStyle Wrap="False"></EditItemStyle>
					<AlternatingItemStyle Wrap="False" CssClass="tbl-DataGridItemAlternating"></AlternatingItemStyle>
					<ItemStyle Wrap="False" CssClass="tbl-DataGridItem"></ItemStyle>
					<HeaderStyle Wrap="False" CssClass="tbl-DataGridHeader"></HeaderStyle>
					<Columns>
						<asp:BoundColumn DataField="cod_cliente" SortExpression="cod_cliente" HeaderText="COD.CLI.">
							<HeaderStyle Wrap="False"></HeaderStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="nom_cliente" SortExpression="nom_cliente" HeaderText="RAZON SOCIAL">
							<HeaderStyle Wrap="False"></HeaderStyle>
							<ItemStyle Wrap="False"></ItemStyle>
							<FooterStyle Wrap="False"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="mes_actual" SortExpression="mes_actual" ReadOnly="True" HeaderText="Mes  Act."
							DataFormatString="{0:#,##0}">
							<HeaderStyle Wrap="False" HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="mes_uno" SortExpression="mes_uno" ReadOnly="True" HeaderText="mes uno"
							DataFormatString="{0:#,##0}">
							<HeaderStyle Wrap="False" HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
							<FooterStyle Wrap="False" HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="mes_dos" SortExpression="mes_dos" ReadOnly="True" HeaderText="mes dos"
							DataFormatString="{0:#,##0}">
							<HeaderStyle Wrap="False" HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
							<FooterStyle Wrap="False" HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="mes_tres" SortExpression="mes_tres" ReadOnly="True" HeaderText="mes tres"
							DataFormatString="{0:#,##0}">
							<HeaderStyle Wrap="False" HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
							<FooterStyle Wrap="False" HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="mes_cuatro" SortExpression="mes_cuatro" ReadOnly="True" HeaderText="mes_cuatro"
							DataFormatString="{0:#,##0}">
							<HeaderStyle Wrap="False" HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="mes_cinco" SortExpression="mes_cinco" ReadOnly="True" HeaderText="mes cinco"
							DataFormatString="{0:#,##0}">
							<HeaderStyle Wrap="False" HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="mes_seis" SortExpression="mes_seis" ReadOnly="True" HeaderText="mes seis"
							DataFormatString="{0:#,##0}">
							<HeaderStyle Wrap="False" HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="mes_siete" SortExpression="mes_siete" ReadOnly="True" HeaderText="mes_siete"
							DataFormatString="{0:#,##0}">
							<HeaderStyle Wrap="False" HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="mes_ocho" SortExpression="mes_ocho" ReadOnly="True" HeaderText="mes ocho"
							DataFormatString="{0:#,##0}">
							<HeaderStyle Wrap="False" HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="mes_nueve" SortExpression="mes_nueve" ReadOnly="True" HeaderText="mes nueve"
							DataFormatString="{0:#,##0}">
							<HeaderStyle Wrap="False" HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="mes_diez" SortExpression="mes_diez" ReadOnly="True" HeaderText="mes diez"
							DataFormatString="{0:#,##0}">
							<HeaderStyle Wrap="False" HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="mes_once" SortExpression="mes_once" ReadOnly="True" HeaderText="mes once"
							DataFormatString="{0:#,##0}">
							<HeaderStyle Wrap="False" HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="mes_doce" SortExpression="mes_doce" ReadOnly="True" HeaderText="mes doce"
							DataFormatString="{0:#,##0}">
							<HeaderStyle Wrap="False" HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
							<FooterStyle Wrap="False" HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
					</Columns>
					<PagerStyle Wrap="False"></PagerStyle>
				</asp:DataGrid></TD>
		</TR>
	</TABLE>
	<P>&nbsp;</P>
	<P>&nbsp;</P>
</wilson:masterpage>
