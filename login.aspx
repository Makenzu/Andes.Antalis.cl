<%@ Register TagPrefix="Wilson" Namespace="Wilson.MasterPages" Assembly="WilsonMasterPages" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="login.aspx.vb" Inherits="app.login"%>
<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
<meta http-equiv="Pragma" content="no-cache">
<meta http-equiv="Expires" content="-1">
<meta http-equiv="CACHE-CONTROL" content="NO-CACHE">
<wilson:masterpage id="MPContainer" runat="server">
	<wilson:contentregion id="MPTitle" runat="server">Inicio de Sessión 
ANDES</wilson:contentregion>
	<wilson:contentregion id="MPCaption" runat="server">Inicio de Sessión ANDES</wilson:contentregion>
	<SCRIPT language="javascript">
function showPop(){
	var mywin
	var winl = (screen.width - 430) / 2;
	var wint = (screen.height - 185) / 2; 
	var param ;
	param = "width=430,height=185,Top="+wint+",Left="+winl+",toolbar=0,scrollbars=1,location=0,statusbar=0,menubar=0,resizable=1";
	 OpenWin('utiles/get_password.aspx','Recuperar',param );
	//mywin.focus();
}
	</SCRIPT>
	<TABLE border="0" cellSpacing="0" cellPadding="3" width="100%">
		<TR> <!--columna 2--> <!--fin columna 2--> <!--columna 3--> <!--fin columna 3--> <!--columna 4-->
			<TD vAlign="top" width="50%" align="center">
				<TABLE border="0" cellSpacing="3" cellPadding="0" width="98%" align="center">
					<TR vAlign="middle">
						<TD colSpan="2">
							<DIV class="txt-verdana-gris" align="justify">
								<P><BR>
									<TABLE border="0" cellSpacing="0" cellPadding="0" width="100%">
										<TR>
											<TD width="85%" align="left">
												<TABLE border="0" cellSpacing="0" cellPadding="1" width="400" align="center">
													<TR>
														<TD bgColor="#708598" width="75">
															<TABLE border="0" cellSpacing="0" cellPadding="0" width="100%" height="202">
																<TR>
																	<TD bgColor="#eeeeee"><IMG src="images/andes_gms.gif" width="82" height="202"></TD>
																</TR>
															</TABLE>
														</TD>
														<TD bgColor="#708598" width="321">
															<TABLE border="0" cellSpacing="0" cellPadding="0" width="100%" background="images/key.gif"
																bgColor="#eeeeee" height="202">
																<TR>
																	<TD>
																		<TABLE id="TABLE1" border="0" cellSpacing="0" cellPadding="1" width="227" align="center">
																			<TR>
																				<TD class="login" width="115" noWrap>Usuario</TD>
																				<TD class="label-login" width="11">:</TD>
																				<TD width="120" align="left">
																					<asp:TextBox id="tbUser" runat="server" Width="100px" CssClass="textBox"></asp:TextBox></TD>
																			</TR>
																			<TR>
																				<TD class="login" noWrap>Contraseña</TD>
																				<TD class="label-login">:</TD>
																				<TD>
																					<asp:TextBox id="tbPass" runat="server" Width="100px" CssClass="textBox" TextMode="Password"></asp:TextBox></TD>
																			</TR>
																			<TR>
																				<TD noWrap>&nbsp;</TD>
																				<TD>&nbsp;</TD>
																				<TD height="40">
																					<DIV align="center">
																						<asp:ImageButton id="ibEntrar" runat="server" ImageUrl="images/entrar.gif"></asp:ImageButton>&nbsp;
																					</DIV>
																				</TD>
																			</TR>
																		</TABLE>
																	</TD>
																</TR>
																<TR>
																	<TD align="left"><BR>
																		<asp:Label id="lError" runat="server" CssClass="txt-AlertMessage"></asp:Label></TD>
																</TR>
															</TABLE>
														</TD>
													</TR> <!-- <TR>
														<TD align="center" bgColor="#708598" colSpan="2"><A class="STD-TEXTSMALL" style="COLOR: white" href="javascript: showPop();">Se 
																me olvidó mi contraseña.</A></TD>
													</TR> //--></TABLE>
												<BR>
											</TD>
										</TR>
									</TABLE>
									<DIV class="txt-verdana-gris">
										<P>&nbsp;</P> <!--url's used in the movie--> <!--text used in the movie--> <!--
		CARGANDO
		--></DIV>
							</DIV>
						</TD>
					</TR>
				</TABLE>
				<P>&nbsp;</P>
				<P>&nbsp;</P>
			</TD> <!--fin columna 4--> <!--columna 5--> <!--fin columna 5--></TR>
	</TABLE>
</wilson:masterpage>
