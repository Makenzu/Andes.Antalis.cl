<%@ Page Language="vb" AutoEventWireup="false" Codebehind="analisisFrecuenciaCompra.aspx.vb" Inherits="app.analisisFrecuenciaCompra"%>
<%@ Register TagPrefix="Wilson" Namespace="Wilson.MasterPages" Assembly="WilsonMasterPages" %>
<META content="text/html; charset=iso-8859-1" http-equiv="Content-Type">
<LINK href="/css/calendar.css" type="text/css" rel="stylesheet">
<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
<wilson:masterpage id="MPContainer" runat="server">
	<WILSON:CONTENTREGION id="MPTitle" runat="server">Análisis 
Frecuencia Compra</WILSON:CONTENTREGION>
	<WILSON:CONTENTREGION id="MPCaption" runat="server">Análisis Frecuencia Compra</WILSON:CONTENTREGION>
	<TABLE id="Table1" border="0" cellSpacing="0" cellPadding="1" width="100%">
		<TR>
			<TD style="WIDTH: 131px" align="right"></TD>
		</TR>
		<TR>
			<TD align="center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
				<asp:ImageButton id="ibExportar" title="Exportar a Excel" runat="server" Visible="False" ToolTip="Exportar a Excel"
					ImageUrl="/images/exportar.gif" EnableViewState="False"></asp:ImageButton></TD>
		</TR>
		<TR>
			<TD style="HEIGHT: 132px" vAlign="top" align="left">
				<TABLE id="Table2" border="0" cellSpacing="1" cellPadding="2">
					<TR>
						<TD class="txt-SoftMessage ">Periodo:</TD>
						<TD align="left">
							<asp:DropDownList id="ddlPeriodo" runat="server" CssClass="verdana-10-normal"></asp:DropDownList></TD>
						<TD width="22" align="right"></TD>
						<TD align="right"></TD>
					</TR>
					<TR>
						<TD class="txt-SoftMessage ">Ejec. Comercial:</TD>
						<TD align="right">
							<asp:DropDownList id="ddlPromotoras" runat="server" CssClass="verdana-10-normal" Width="200px"></asp:DropDownList></TD>
						<TD width="22" align="right"></TD>
						<TD align="right">
							<asp:Label id="Label1" runat="server" CssClass="txt-SoftMessage "></asp:Label></TD>
					</TR>
					<TR>
						<TD class="txt-SoftMessage ">Frecuencia:</TD>
						<TD align="right">
							<asp:DropDownList id="ddlFrecuencia" runat="server" CssClass="verdana-10-normal" Width="200px">
								<asp:ListItem Value="001">f >= 10</asp:ListItem>
								<asp:ListItem Value="003">6 <= f <= 9</asp:ListItem>
								<asp:ListItem Value="004">f <= 5</asp:ListItem>
								<asp:ListItem Value="*" Selected="True">* TODAS *</asp:ListItem>
							</asp:DropDownList></TD>
						<TD width="22" align="right"></TD>
						<TD style="HEIGHT: 20px" align="right">
							<DIV id="Div1" name="disbtn">&nbsp;</DIV>
						</TD>
					</TR>
					<TR>
						<TD class="txt-SoftMessage ">Dependencia:</TD>
						<TD align="right">
							<asp:DropDownList id="ddlDependencia" runat="server" CssClass="verdana-10-normal" Width="200px"></asp:DropDownList></TD>
						<TD style="WIDTH: 22px" width="22" align="right">
							<asp:ImageButton id="btConsulta" runat="server" ImageUrl="/images/procesar.gif"></asp:ImageButton></TD>
						<TD class="txt-SoftMessage " align="right">
							<asp:Label id="Label2" runat="server">Consultar</asp:Label></TD>
					</TR>
				</TABLE>
			</TD>
		</TR>
		<TR>
			<TD vAlign="top" align="left">
				<asp:DataGrid id="FreqCli" runat="server" AutoGenerateColumns="False" ShowFooter="True">
					<FooterStyle CssClass="tbl-DataGridFooter"></FooterStyle>
					<AlternatingItemStyle CssClass="tbl-DataGridItemAlternating"></AlternatingItemStyle>
					<ItemStyle CssClass="tbl-DataGridItem"></ItemStyle>
					<HeaderStyle CssClass="tbl-DataGridHeader"></HeaderStyle>
					<Columns>
						<asp:BoundColumn DataField="cod_promotora" HeaderText="CodProm"></asp:BoundColumn>
						<asp:BoundColumn DataField="nom_promotora" HeaderText="Promotora"></asp:BoundColumn>
						<asp:BoundColumn DataField="cod_cliente" HeaderText="CodCli"></asp:BoundColumn>
						<asp:BoundColumn DataField="nom_cliente" HeaderText="Cliente"></asp:BoundColumn>
						<asp:BoundColumn Visible="False" DataField="cod_frecuencia_1" HeaderText="CodFreq1"></asp:BoundColumn>
						<asp:BoundColumn Visible="False" DataField="cod_frecuencia_2" HeaderText="CodFreq2">
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn Visible="False" DataField="cod_frecuencia_3" HeaderText="CodFreq3"></asp:BoundColumn>
						<asp:BoundColumn DataField="val_total_prom_1" HeaderText="f &gt;= 10" DataFormatString="{0:#,##0.00}">
							<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="val_total_prom_2" HeaderText="6 &lt;= f &lt;= 9" DataFormatString="{0:#,##0.00}">
							<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="val_total_prom_3" HeaderText="f &lt;= 5" DataFormatString="{0:#,##0.00}">
							<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="val_total_gral" HeaderText="Total Gral" DataFormatString="{0:#,##0.00}">
							<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:ButtonColumn Text="+" ButtonType="PushButton" CommandName="Select"></asp:ButtonColumn>
					</Columns>
				</asp:DataGrid></TD>
			<TD vAlign="top">
				<asp:DataGrid id="FreqProd" runat="server" AutoGenerateColumns="False" ShowFooter="True">
					<FooterStyle CssClass="tbl-DataGridFooter"></FooterStyle>
					<AlternatingItemStyle CssClass="tbl-DataGridItemAlternating"></AlternatingItemStyle>
					<ItemStyle CssClass="tbl-DataGridItem"></ItemStyle>
					<HeaderStyle CssClass="tbl-DataGridHeader"></HeaderStyle>
					<Columns>
						<asp:BoundColumn DataField="cod_producto" HeaderText="CodProd"></asp:BoundColumn>
						<asp:BoundColumn DataField="des_producto" HeaderText="Producto"></asp:BoundColumn>
						<asp:BoundColumn DataField="num_dependencia" HeaderText="Dependencia">
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="val_total_prom_1" HeaderText="f &gt;= 10" DataFormatString="{0:#,##0.00}">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="val_total_prom_2" HeaderText="6 &lt;= f &lt;= 9" DataFormatString="{0:#,##0.00}">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="val_total_prom_3" HeaderText="f &lt;= 5" DataFormatString="{0:#,##0.00}">
							<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<FooterStyle HorizontalAlign="Right"></FooterStyle>
						</asp:BoundColumn>
					</Columns>
				</asp:DataGrid></TD>
		</TR>
		<TR>
			<TD style="WIDTH: 131px"></TD>
			<TD>
				<DIV><FONT size="2" face="Arial"><STRONG></STRONG></FONT>&nbsp;</DIV>
			</TD>
		</TR>
	</TABLE>
</wilson:masterpage>
