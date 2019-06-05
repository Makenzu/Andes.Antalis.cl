<%@ Page Language="vb" AutoEventWireup="false" Codebehind="reposicionStock.aspx.vb" Inherits="app.reposicionStock" %>
<%@ Register TagPrefix="Wilson" Namespace="Wilson.MasterPages" Assembly="WilsonMasterPages" %>
<META content="text/html; charset=iso-8859-1" http-equiv="Content-Type">
<meta content="False" name="vs_snapToGrid">
<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
<wilson:masterpage id="MPContainer" runat="server">
	<WILSON:CONTENTREGION id="MPTitle" runat="server">Reposicion Stock 
Bolivia</WILSON:CONTENTREGION>
	<WILSON:CONTENTREGION id="MPCaption" runat="server">Reposicion Stock Bolivia<BR></WILSON:CONTENTREGION>
	<SCRIPT language="javascript">

	</SCRIPT>
	<TABLE id="Table1" border="0" cellSpacing="0" cellPadding="0" width="95%" align="center">
		<TR>
			<TD width="70%"></TD>
			<TD width="30%" align="right">
				<TABLE id="Table6" border="0" cellSpacing="0" cellPadding="0">
					<TR>
						<TD></TD>
						<TD width="5"></TD>
						<TD><INPUT title="Imprimir" onclick="javascript:imprimir();return false;" alt="Imprimir" src="/images/imprimir.gif"
								type="image"></TD>
					</TR>
				</TABLE>
			</TD>
		</TR>
	</TABLE>
	<TABLE id="Table2" border="0" cellSpacing="0" cellPadding="0" width="98%" align="center">
		<TR>
			<TD style="HEIGHT: 31px" vAlign="bottom" align="left">
				<TABLE id="Table7" border="0" cellSpacing="0" cellPadding="0">
					<TR>
						<TD width="15"></TD>
						<TD>
							<TABLE id="tbParams" border="0" cellSpacing="0" cellPadding="0">
								<TR>
									<TD style="WIDTH: 80px" class="tbl-HorizHeader" width="80">Año :</TD>
									<TD></TD>
									<TD>
										<asp:DropDownList id="ddlAnno" runat="server"></asp:DropDownList></TD>
								</TR>
							</TABLE>
						</TD>
						<TD></TD>
					</TR>
					<TR>
						<TD width="15"></TD>
						<TD>
							<TABLE id="Table3" border="0" cellSpacing="0" cellPadding="0">
								<TR>
									<TD style="WIDTH: 79px" class="tbl-HorizHeader" width="79">Mes :</TD>
									<TD height="30"></TD>
									<TD>
										<asp:DropDownList id="ddlMes" runat="server"></asp:DropDownList></TD>
								</TR>
							</TABLE>
						</TD>
						<TD>
							<asp:ImageButton id="btSend" runat="server" ImageUrl="/images/procesar.gif" CausesValidation="False"
								AlternateText="Revisar Stock De Productos"></asp:ImageButton>
							<DIV style="DISPLAY: none" id="disbtn" name="disbtn"><IMG alt="" src="/images/procesando.gif"></DIV>
						</TD>
					</TR>
				</TABLE>
				<asp:Label id="lbErrors" runat="server" ForeColor="Red" Font-Size="Small" Visible="False">lbErrors</asp:Label></TD>
		</TR>
		<TR>
			<TD bgColor="#a3bad6" height="26" colSpan="3" align="center"></TD>
		</TR>
		<TR>
			<TD colSpan="3" align="center">
				<asp:DataGrid id="dgReposicion" runat="server" CellSpacing="1" AllowSorting="True" CellPadding="2"
					AutoGenerateColumns="False">
					<FooterStyle CssClass="tbl-DataGridFooter"></FooterStyle>
					<AlternatingItemStyle HorizontalAlign="Left" CssClass="tbl-DataGridItemAlternating" VerticalAlign="Middle"></AlternatingItemStyle>
					<ItemStyle ForeColor="Black" CssClass="tbl-DataGridItem"></ItemStyle>
					<HeaderStyle CssClass="tbl-DataGridHeader" BackColor="InactiveCaptionText"></HeaderStyle>
					<Columns>
						<asp:BoundColumn DataField="cod_producto" HeaderText="C&#243;digo">
							<FooterStyle Font-Size="Small"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="des_producto" HeaderText="Descripci&#243;n">
							<HeaderStyle Wrap="False"></HeaderStyle>
							<ItemStyle Wrap="False"></ItemStyle>
							<FooterStyle Wrap="False"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="cod_familia" HeaderText="Fam"></asp:BoundColumn>
						<asp:BoundColumn DataField="cod_subfamilia" HeaderText="SubFam"></asp:BoundColumn>
						<asp:BoundColumn DataField="cod_umb" HeaderText="UMB">
							<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="val_stock_actual_sc" HeaderText="Stk 3200" DataFormatString="{0:#,##0}">
							<HeaderStyle Wrap="False" HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
							<FooterStyle Wrap="False"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="PROMEDIO_SC" HeaderText="Stk Min 3200" DataFormatString="{0:#,##0}">
							<HeaderStyle Wrap="False" HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
							<FooterStyle Wrap="False"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="val_factor" HeaderText="Factor Rep." DataFormatString="{0:#,##0.00}">
							<HeaderStyle Wrap="False" HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
							<FooterStyle Wrap="False"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="STOCK_A_REPONER_SC" HeaderText="Reposici&#243;n 3200" DataFormatString="{0:#,##0}">
							<HeaderStyle Wrap="False" HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
							<FooterStyle Wrap="False"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="val_stock_actual_lp" HeaderText="Stk 3100" DataFormatString="{0:#,##0}">
							<HeaderStyle Wrap="False" HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
							<FooterStyle Wrap="False"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="PROMEDIO_LP" HeaderText="Stk Min 3100" DataFormatString="{0:#,##0}">
							<HeaderStyle Wrap="False" HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
					</Columns>
				</asp:DataGrid></TD>
		</TR>
	</TABLE>
</wilson:masterpage>
