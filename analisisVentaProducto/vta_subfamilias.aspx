<%@ Register TagPrefix="Wilson" Namespace="Wilson.MasterPages" Assembly="WilsonMasterPages" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="vta_subfamilias.aspx.vb" Inherits="app.vta_subfamilias"%>
<META content="text/html; charset=iso-8859-1" http-equiv="Content-Type">
<wilson:masterpage id="MPContainer" runat="server">
	<WILSON:CONTENTREGION id="MPTitle" runat="server">ANDES</WILSON:CONTENTREGION>
	<WILSON:CONTENTREGION id="MPCaption" runat="server">Ventas por SubFamilias<BR>
<asp:Label id="lbFecha" runat="server"></asp:Label></WILSON:CONTENTREGION>
	<SCRIPT language="javascript">
function showPop(sf){
var mywin
	var param 
	param = "width=480,height=500,Top=50,Left=50,toolbar=0,scrollbars=1,location=0,statusbar=0,menubar=0,resizable=1";
	mywin = window.open("vta_subfamilia_prod.aspx?sf="+sf,"Detalle_Anual",param );
	mywin.focus();
}
	</SCRIPT>
	<asp:Label id="lbErrors" runat="server" CssClass="txt-FatalMessage"></asp:Label>
	<TABLE id="Table1" border="0" cellSpacing="0" cellPadding="3">
		<TR>
			<TD align="right">
				<TABLE id="Table5" border="0" cellSpacing="0" cellPadding="2" width="350" align="center">
					<TR>
						<TD class="tbl-HorizHeader" width="70">Periodo:</TD>
						<TD width="200" colSpan="2">
							<asp:DropDownList id="ddlMes" runat="server" CssClass="listBox"></asp:DropDownList>&nbsp;/
							<asp:DropDownList id="ddlAno" runat="server" CssClass="listBox"></asp:DropDownList></TD>
						<TD width="70">
							<asp:ImageButton id="btSend" runat="server" ImageUrl="/images/procesar.gif"></asp:ImageButton>
							<DIV style="DISPLAY: none" id="disbtn" name="disbtn"><IMG alt="" src="/images/procesando.gif"></DIV>
						</TD>
					</TR>
				</TABLE>
			</TD>
		</TR>
		<TR>
			<TD align="right">
				<asp:Label id="lbNota" runat="server" CssClass="txt-SoftMessage" Visible="False"></asp:Label></TD>
		</TR>
		<TR>
			<TD>
				<asp:DataGrid id="dgResultado" runat="server" ShowFooter="True" AutoGenerateColumns="False" CellPadding="2">
					<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
					<AlternatingItemStyle CssClass="tbl-DataGridItemAlternating"></AlternatingItemStyle>
					<ItemStyle CssClass="tbl-DataGridItem"></ItemStyle>
					<HeaderStyle CssClass="tbl-DataGridHeader"></HeaderStyle>
					<Columns>
						<asp:TemplateColumn HeaderText="SubFamilia">
							<ItemTemplate>
								<a href='vta_subfamilia_prod.aspx?sf=<%# DataBinder.Eval(Container, "DataItem.cod_subfamilia") %>&ms=<%# Request("ddlMes")%>&an=<%# Request("ddlAno")%>'>
									<font size="1">(<%# trim(DataBinder.Eval(Container, "DataItem.cod_subfamilia"))%>)</font>
									<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.des_subfamilia") %>' ID="Label1">
									</asp:Label></a>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn>
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<ItemTemplate>
								<asp:Label runat="server" Text='<%#String.Format( "{0:N0}", CInt( DataBinder.Eval(Container, "DataItem.mes_uno"))) %>' ID="Label2">
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn>
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<ItemTemplate>
								<asp:Label runat="server" Text='<%# String.Format( "{0:N0}", CInt(DataBinder.Eval(Container, "DataItem.mes_dos"))) %>' ID="Label3">
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn>
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<ItemTemplate>
								<asp:Label runat="server" Text='<%# String.Format( "{0:N0}", CInt(DataBinder.Eval(Container, "DataItem.mes_tres"))) %>' ID="Label4">
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn>
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<ItemTemplate>
								<asp:Label runat="server" Text='<%#String.Format( "{0:N0}", CInt(DataBinder.Eval(Container, "DataItem.mes_cuatro")) ) %>' ID="Label5">
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn>
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<ItemTemplate>
								<asp:Label runat="server" Text='<%#  String.Format( "{0:N0}", CInt(DataBinder.Eval(Container, "DataItem.mes_cinco"))) %>' ID="Label6">
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn>
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<ItemTemplate>
								<asp:Label runat="server" Text='<%#  String.Format( "{0:N0}", CInt(DataBinder.Eval(Container, "DataItem.mes_seis"))) %>' ID="Label7">
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn>
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<ItemTemplate>
								<asp:Label runat="server" Text='<%#  String.Format( "{0:N0}", CInt(DataBinder.Eval(Container, "DataItem.mes_siete"))) %>' ID="Label8" NAME="Label1">
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn>
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<ItemTemplate>
								<asp:Label runat="server" Text='<%# String.Format( "{0:N0}", CInt( DataBinder.Eval(Container, "DataItem.mes_ocho"))) %>' ID="Label9" NAME="Label2">
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn>
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<ItemTemplate>
								<asp:Label runat="server" Text='<%#  String.Format( "{0:N0}", CInt(DataBinder.Eval(Container, "DataItem.mes_nueve"))) %>' ID="Label10">
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn>
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<ItemTemplate>
								<asp:Label runat="server" Text='<%#  String.Format( "{0:N0}", CInt(DataBinder.Eval(Container, "DataItem.mes_diez"))) %>' ID="Label11" NAME="Label3">
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn>
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<ItemTemplate>
								<asp:Label runat="server" Text='<%# String.Format( "{0:N0}", CInt( DataBinder.Eval(Container, "DataItem.mes_once"))) %>' ID="Label12" NAME="Label4">
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn>
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<ItemTemplate>
								<asp:Label runat="server" Text='<%# String.Format( "{0:N0}", CInt( DataBinder.Eval(Container, "DataItem.mes_doce"))) %>' ID="Label13" NAME="Label5">
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
					</Columns>
				</asp:DataGrid></TD>
		</TR>
		<TR>
			<TD></TD>
		</TR>
	</TABLE>
</wilson:masterpage>
