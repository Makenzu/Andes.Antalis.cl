<%@ Page Language="vb" AutoEventWireup="false" Codebehind="vta_res_facturacion_mensual.aspx.vb" Inherits="app.vta_res_facturacion_mensual"%>
<%@ Register TagPrefix="Wilson" Namespace="Wilson.MasterPages" Assembly="WilsonMasterPages" %>
<META content="text/html; charset=iso-8859-1" http-equiv="Content-Type">
<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
<wilson:masterpage id="MPContainer" runat="server">
	<WILSON:CONTENTREGION id="MPTitle" runat="server">ANDES</WILSON:CONTENTREGION>
	<WILSON:CONTENTREGION id="MPCaption" runat="server">Resumen de Facturación ABCD<BR>
<asp:Label id="lbFecha" runat="server"></asp:Label></WILSON:CONTENTREGION>
	<SCRIPT language="javascript">
			function doublecheck(){
				 document.all.disbtn.style.display='inline';
				 document.all.btSend.style.display='none';
			}
	</SCRIPT>
	<TABLE id="Table2" border="0" cellSpacing="0" cellPadding="0" width="100%">
		<TR>
			<TD width="70%">
				<asp:Label id="lbErrors" runat="server" CssClass="txt-FatalMessage"></asp:Label></TD>
			<TD width="30%" align="right"><IMG onclick="javascript:imprimir();" border="0" alt="Imprimir" src="/images/imprimir.gif"></TD>
		</TR>
	</TABLE>
	<TABLE id="Table1" border="0" cellSpacing="0" cellPadding="3" align="center">
		<TR>
			<TD align="center">
				<TABLE id="tbParams" border="0" cellSpacing="0" cellPadding="2" width="300">
					<TR>
						<TD style="HEIGHT: 17px" class="tbl-HorizHeader" width="74">Centro:</TD>
						<TD style="HEIGHT: 17px">
							<asp:DropDownList id="ddlCentro" runat="server" CssClass="listBox"></asp:DropDownList></TD>
						<TD style="HEIGHT: 17px"></TD>
					</TR>
					<TR>
						<TD style="HEIGHT: 17px" class="tbl-HorizHeader" width="74">Mes:</TD>
						<TD style="HEIGHT: 17px">
							<asp:DropDownList id="ddlMes" runat="server" CssClass="listBox"></asp:DropDownList></TD>
						<TD style="HEIGHT: 17px">
							<asp:ImageButton id="btSend" runat="server" ImageUrl="/images/procesar.gif"></asp:ImageButton>
							<DIV style="DISPLAY: none" id="disbtn" name="disbtn"><IMG alt="" src="/images/procesando.gif"></DIV>
						</TD>
					</TR>
					<TR>
						<TD class="tbl-HorizHeader" width="74">Moneda:</TD>
						<TD>
							<asp:DropDownList id="ddlMoneda" runat="server" CssClass="listBox">
								<asp:ListItem Value="DOL" Selected="True">Dolares</asp:ListItem>
								<asp:ListItem Value="PES">Pesos</asp:ListItem>
							</asp:DropDownList></TD>
						<TD></TD>
					</TR>
				</TABLE>
			</TD>
		</TR>
		<TR>
			<TD>
				<asp:Label id="lbNota" runat="server" CssClass="txt-SoftMessage" Visible="False"></asp:Label></TD>
		</TR>
		<TR>
			<TD>
				<asp:DataGrid id="dgResultado" runat="server" Width="100%" AllowSorting="True" AutoGenerateColumns="False"
					CellPadding="3" ShowFooter="True">
					<FooterStyle Font-Bold="True" HorizontalAlign="Right" CssClass="tbl-DataGridFooter"></FooterStyle>
					<AlternatingItemStyle CssClass="tbl-DataGridItemAlternating"></AlternatingItemStyle>
					<ItemStyle CssClass="tbl-DataGridItem"></ItemStyle>
					<HeaderStyle CssClass="tbl-DataGridHeader"></HeaderStyle>
					<Columns>
						<asp:BoundColumn DataField="Dia" ReadOnly="True" HeaderText="Dia">
							<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="A$" ReadOnly="True" HeaderText="A$">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="B$" ReadOnly="True" HeaderText="B$">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="C$" ReadOnly="True" HeaderText="C$">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="D$" ReadOnly="True" HeaderText="D$">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="D/B*100$" ReadOnly="True" HeaderText="D/B*100$">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="B/A*100$" ReadOnly="True" HeaderText="B/A*100$">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="NUM_FACTURAS" ReadOnly="True" HeaderText="N&#176;. Fac.">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn Visible="False" ReadOnly="True" HeaderText="Vtas. Mes"></asp:BoundColumn>
						<asp:BoundColumn Visible="False" ReadOnly="True" HeaderText="Pagos Mes"></asp:BoundColumn>
					</Columns>
				</asp:DataGrid></TD>
		</TR>
		<TR>
			<TD class="PageTitle" bgColor="whitesmoke" align="center">
				<asp:Label id="lbTitulo2" runat="server" Visible="False">Resumen de Meses Anteriores</asp:Label></TD>
		</TR>
		<TR>
			<TD>
				<asp:DataGrid id="dgResultado2" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="3"
					ShowFooter="True">
					<FooterStyle Font-Bold="True" CssClass="tbl-DataGridFooter"></FooterStyle>
					<AlternatingItemStyle CssClass="tbl-DataGridItemAlternating"></AlternatingItemStyle>
					<ItemStyle CssClass="tbl-DataGridItem"></ItemStyle>
					<HeaderStyle CssClass="tbl-DataGridHeader"></HeaderStyle>
					<Columns>
						<asp:TemplateColumn HeaderText="Mes">
							<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
							<ItemTemplate>
								<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.mes") &"/"& DataBinder.Eval(Container, "DataItem.ano") %>'>
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:BoundColumn DataField="AU$" ReadOnly="True" HeaderText="AUS$">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="BU$" ReadOnly="True" HeaderText="BUS$">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="CU$" ReadOnly="True" HeaderText="CUS$">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="DU$" ReadOnly="True" HeaderText="DUS$">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:TemplateColumn HeaderText="D/B*100$">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<ItemTemplate>
								<asp:Label runat="server" Text='d/b*100$'></asp:Label>
							</ItemTemplate>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="B/A*100$">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<ItemTemplate>
								<asp:Label runat="server" Text='b/a*100$'></asp:Label>
							</ItemTemplate>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:TemplateColumn>
						<asp:BoundColumn DataField="num_facturas" ReadOnly="True" HeaderText="N&#176;. Fac">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn Visible="False" HeaderText="Vtas. Mes">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn Visible="False" HeaderText="Pagos Mes">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
					</Columns>
				</asp:DataGrid></TD>
		</TR>
	</TABLE>
</wilson:masterpage>
