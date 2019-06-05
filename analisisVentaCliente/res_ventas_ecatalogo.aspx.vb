Imports Exportador

Public Class res_ventas_ecatalogo
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents MPTitle As Wilson.MasterPages.ContentRegion
    Protected WithEvents lbFecha As System.Web.UI.WebControls.Label
    Protected WithEvents MPCaption As Wilson.MasterPages.ContentRegion
    Protected WithEvents lbNota As System.Web.UI.WebControls.Label
    Protected WithEvents MPContainer As Wilson.MasterPages.MasterPage

    Dim tUserInfo As usuario.t_Usuario
    Protected WithEvents ddlCelula As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlEjecutivoComercial As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlVendedoraVirtual As System.Web.UI.WebControls.DropDownList
    Protected WithEvents bBuscarPorCelula As System.Web.UI.WebControls.Button
    Protected WithEvents bBuscarPorEjeCom As System.Web.UI.WebControls.Button
    Protected WithEvents bBuscarPorVendVirtual As System.Web.UI.WebControls.Button
    Protected WithEvents txtIniPorEjeCom As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtFinPorEjeCom As System.Web.UI.WebControls.TextBox
    Protected WithEvents lbCelula As System.Web.UI.WebControls.Label
    Protected WithEvents lEjecutivoComercial As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents lVenderoraVitual As System.Web.UI.WebControls.Label
    Protected WithEvents pDatosVendVirtual As System.Web.UI.WebControls.Panel
    Protected WithEvents pDatosEjeCom As System.Web.UI.WebControls.Panel
    Protected WithEvents pDatosCelula As System.Web.UI.WebControls.Panel
    Protected WithEvents txtIniPorCelula As System.Web.UI.WebControls.TextBox
    Protected WithEvents chkSoloClientesEcatalogoCelula As System.Web.UI.WebControls.CheckBox
    Protected WithEvents chkbSoloClientesEcatalogoEjeCom As System.Web.UI.WebControls.CheckBox
    Protected WithEvents chkbSoloClientesEcatalogoVendVirtual As System.Web.UI.WebControls.CheckBox
    Protected WithEvents txtFinPorVendVirt As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtIniPorVendVirt As System.Web.UI.WebControls.TextBox
    Protected WithEvents dgDatosCelula As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dgDatosEjeComercial As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dgDatosVendVirtual As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtFinPorCelula As System.Web.UI.WebControls.TextBox
    Protected WithEvents Literal1 As System.Web.UI.WebControls.Literal
    Protected WithEvents lClientesCelula As System.Web.UI.WebControls.Label
    Protected WithEvents lClientesEcatalogoCelula As System.Web.UI.WebControls.Label
    Protected WithEvents lClientesEjeCom As System.Web.UI.WebControls.Label
    Protected WithEvents lClientesEcatalogoEjeCom As System.Web.UI.WebControls.Label
    Protected WithEvents lClientesVendVirtual As System.Web.UI.WebControls.Label
    Protected WithEvents lClientesEcatalogoVendVirt As System.Web.UI.WebControls.Label
    Protected WithEvents lEjecutivaComercial As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    ' Para calculo de totales
    Dim totales(9) As Double
    Dim totRows As Integer

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Session(Constantes.CTE_ANDES_USUARIO_AUTENTICADO) <> "SI" Then
            Response.Redirect("/logout.aspx")
            Return
        End If


        If Page.IsPostBack = False Then

            pDatosCelula.Visible = False
            pDatosEjeCom.Visible = False
            pDatosVendVirtual.Visible = False

            txtIniPorCelula.Text = New DateTime(Now.Year, Now.Month, 1).ToString("dd/MM/yyyy")
            txtFinPorCelula.Text = Now.AddDays(-1).ToString("dd/MM/yyyy")

            txtIniPorEjeCom.Text = New DateTime(Now.Year, Now.Month, 1).ToString("dd/MM/yyyy")
            txtFinPorEjeCom.Text = Now.AddDays(-1).ToString("dd/MM/yyyy")

            txtIniPorVendVirt.Text = New DateTime(Now.Year, Now.Month, 1).ToString("dd/MM/yyyy")
            txtFinPorVendVirt.Text = Now.AddDays(-1).ToString("dd/MM/yyyy")

            'Cargamos células
            Dim ws As cl.gms.andes.ws.OrgSrv = New cl.gms.andes.ws.OrgSrv
            Dim dsAgentes As DataSet = ws.listaAgentesPorTipo("GMSC", "CELL")
            With ddlCelula
                .DataSource = dsAgentes.Tables(0)
                .DataTextField = "nombre"
                .DataValueField = "cod_agente"
                .DataBind()
                .Items.Add(New ListItem("-seleccione-", ""))
            End With

            'Cargamos ejecutivas comerciales
            dsAgentes = ws.listaAgentesPorTipo("GMSC", "EJECO")
            With ddlEjecutivoComercial
                .DataSource = dsAgentes.Tables(0)
                .DataTextField = "nombre"
                .DataValueField = "cod_agente"
                .DataBind()
                .Items.Add(New ListItem("-seleccione-", ""))
            End With

            'Cargamos vendedoras virtuales
            dsAgentes = ws.listaVendedorasVirtuales("GMSC")
            With ddlVendedoraVirtual
                .DataSource = dsAgentes.Tables(0)
                .DataTextField = "nom_vendedora_2"
                .DataValueField = "cod_vend_virtual"
                .DataBind()
                .Items.Add(New ListItem("-seleccione-", ""))
            End With

            Dim grupoAgente As String = Session(Constantes.CTE_ANDES_GRUPO_AGENTE)
            Dim esEjecutivoGerencial As Boolean = (Session(Constantes.CTE_OBJ_USER_INFO_PERFIL) = "EJEC")
            Dim codigoCelula As String = Session(Constantes.CTE_ANDES_CODIGO_CELULA)
            Dim codigoAgente As String = Session(Constantes.CTE_ANDES_CODIGO_AGENTE)
            Dim esSuperUsuarioCelulas As Boolean = (Session(Constantes.CTE_ANDES_SUPER_AGENTE) = "X")
            Dim esEjecutivoVentasCelula As Boolean = (Session(Constantes.CTE_ANDES_CODIGO_TIPO_AGENTE) = "EJEVE")
            Dim esEjecutivoComercialCelula As Boolean = (Session(Constantes.CTE_ANDES_CODIGO_TIPO_AGENTE) = "EJECO")
            Dim esEjecutivoVentaTelefono As Boolean = (Session(Constantes.CTE_ANDES_CODIGO_TIPO_AGENTE) = "VETEL")
            Dim esTecnicoCelula As Boolean = (Session(Constantes.CTE_ANDES_CODIGO_TIPO_AGENTE) = "TECNICO")

            'Seteamos alcances dentro de consultas por célula
            If (grupoAgente = "CELULA") Or (esEjecutivoGerencial) Then

                ddlCelula.ClearSelection()

                'Seteamos valor por defecto de ser posible
                If Not IsNothing(ddlCelula.Items.FindByValue(codigoCelula)) Then
                    ddlCelula.Items.FindByValue(codigoCelula).Selected = True
                Else
                    ddlCelula.Items.FindByValue("").Selected = True
                End If


                If Not esEjecutivoGerencial And Not esSuperUsuarioCelulas Then
                    'Sacamos las celulas que no correspondan al usuario
                    Dim i As Integer = 0
                    While i <= ddlCelula.Items.Count - 1
                        If ddlCelula.Items(i).Value <> codigoCelula Then
                            ddlCelula.Items.Remove(ddlCelula.Items(i))
                            i -= 1
                        End If
                        i += 1
                    End While
                End If

                Literal1.Text = "$(""#op1"").addClass(""active"").show();" & vbCrLf & _
                            "$(""#tab1"").show();"

                bBuscarPorCelula.Enabled = True

            Else
                'No permitimos consultas
                ddlCelula.ClearSelection()
                ddlCelula.Items.FindByValue("").Selected = True
                ddlCelula.Enabled = False
                bBuscarPorCelula.Enabled = False
                txtFinPorCelula.Enabled = False
                txtIniPorCelula.Enabled = False
                chkSoloClientesEcatalogoCelula.Enabled = False
            End If


            'Seteamos alcances dentro de consultas por ejecutivo comercial
            If grupoAgente = "CELULA" Or esEjecutivoGerencial Then

                ddlEjecutivoComercial.ClearSelection()

                'Seteamos valor por defecto si es posible
                If Not IsNothing(ddlEjecutivoComercial.Items.FindByValue(codigoAgente)) Then
                    ddlEjecutivoComercial.Items.FindByValue(Session(Constantes.CTE_ANDES_CODIGO_AGENTE)).Selected = True
                Else
                    ddlEjecutivoComercial.Items.FindByValue("").Selected = True
                End If

                'Permitimos consultar otros ejecutivos comerciales
                If Not esEjecutivoGerencial And Not esEjecutivoVentasCelula Then
                    'Sacamos las celulas que no correspondan al usuario
                    Dim i As Integer = 0
                    While i <= ddlEjecutivoComercial.Items.Count - 1
                        If ddlEjecutivoComercial.Items(i).Value <> codigoAgente Then
                            ddlEjecutivoComercial.Items.Remove(ddlEjecutivoComercial.Items(i))
                            i -= 1
                        End If
                        i += 1
                    End While
                End If

                Literal1.Text = "$(""#op2"").addClass(""active"").show();" & vbCrLf & _
                                "$(""#tab2"").show();"

                bBuscarPorEjeCom.Enabled = True
            Else
                'No permitimos realizar consultas
                ddlEjecutivoComercial.ClearSelection()
                ddlEjecutivoComercial.Items.FindByValue("").Selected = True
                ddlEjecutivoComercial.Enabled = False
                bBuscarPorEjeCom.Enabled = False

                txtFinPorEjeCom.Enabled = False
                txtIniPorEjeCom.Enabled = False
                chkbSoloClientesEcatalogoEjeCom.Enabled = False
            End If

            'definimos alcances en consultas por vendedora virtual
            If esEjecutivoGerencial Or (grupoAgente = "CELULA" And esEjecutivoVentasCelula) Or (grupoAgente = "VETEL") Then

                ddlVendedoraVirtual.ClearSelection()

                'Seteamos valor por defecto de ser posible
                If Not IsNothing(ddlVendedoraVirtual.Items.FindByValue(codigoAgente)) Then
                    ddlVendedoraVirtual.Items.FindByValue(codigoAgente).Selected = True
                Else
                    ddlVendedoraVirtual.Items.FindByValue("").Selected = True
                End If

                If Not esEjecutivoGerencial And Not esSuperUsuarioCelulas Then
                    Dim i As Integer = 0
                    While i <= ddlVendedoraVirtual.Items.Count - 1
                        If ddlVendedoraVirtual.Items(i).Value <> codigoAgente Then
                            ddlVendedoraVirtual.Items.Remove(ddlVendedoraVirtual.Items(i))
                            i -= 1
                        End If
                        i += 1
                    End While
                End If

                Literal1.Text = "$(""#op3"").addClass(""active"").show();" & vbCrLf & _
                                "$(""#tab3"").show();"

                bBuscarPorVendVirtual.Enabled = True
            Else
                'No permitimos realizar consultas
                ddlVendedoraVirtual.ClearSelection()
                ddlVendedoraVirtual.Items.FindByValue("").Selected = True
                ddlVendedoraVirtual.Enabled = False
                bBuscarPorVendVirtual.Enabled = False
                txtFinPorVendVirt.Enabled = False
                txtIniPorVendVirt.Enabled = False
                chkbSoloClientesEcatalogoVendVirtual.Enabled = False
            End If



        End If

    End Sub


#Region " ORDENAMIENTO DE COLUMNAS DATAGRID "



    'The Page-level properties that write to ViewState
    Private Property SortExpression() As String
        Get
            Dim o As Object = viewstate("SortExpression")
            If o Is Nothing Then
                Return String.Empty
            Else
                Return o.ToString
            End If
        End Get
        Set(ByVal Value As String)
            viewstate("SortExpression") = Value
        End Set
    End Property

    Private Property SortAscending() As Boolean
        Get
            Dim o As Object = viewstate("SortAscending")
            If o Is Nothing Then
                Return True
            Else
                Return Convert.ToBoolean(o)
            End If
        End Get
        Set(ByVal Value As Boolean)
            viewstate("SortAscending") = Value
        End Set
    End Property

#End Region

#Region " EXPORTACION A EXCELL "
    'Private Sub ibExportar_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs)

    '    If dgResultado.Items.Count > 0 Then

    '        ' Nueva instancia del Informe
    '        Dim xlsResultado As Table = CType(dgResultado.Controls(0), System.Web.UI.WebControls.Table)

    '        Dim sTableHeader As String
    '        sTableHeader = "Ejec. comercial: " & Me.lbCodPromo.Text.Trim & " - " & Me.lbNomPromo.Text.Trim

    '        ' Agregar encabezado del informe
    '        Utiles.AddReportHeader(xlsResultado, String.Empty, String.Empty, sTableHeader)

    '        Dim Exportar As New Exports.OpenExcel(Server.MapPath(ConfigurationSettings.AppSettings("OpenExcel.DefaultXml")))

    '        ' Configuracion de impresion
    '        Exportar.ExcelXml.PreserveWhitespace = False
    '        Exportar.PageScale = 80
    '        Exportar.PageLayout = "Portrait"

    '        ' Encabezado y Pie de Pagina
    '        Exportar.RightHeader = Server.HtmlDecode("&amp;Z&amp;&quot;Arial,Negrita&quot;" & COMPANYNAME & "&amp;DResumen Venta Cliente - " & lbFecha.Text.Trim)
    '        Exportar.LeftFooter = Server.HtmlDecode("&amp;D&amp;P/&amp;#")

    '        ' Exportar
    '        Exportar.TableToExcel(xlsResultado)
    '        Exportar.SaveToClient(Response)
    '    End If
    'End Sub

#End Region

#Region " INICIALIZACION DE CONTROLES FORMULARIO "
    'Private Sub cargaPromotoras(ByVal codigoFilial As String, ByVal codigoSucursal As String)
    '    Dim xDatos As DataTable = Utiles.ObtienePromotoras(codigoFilial)
    '    With ddlPromotora
    '        .Visible = True
    '        .DataSource = xDatos
    '        .DataTextField = "COL2"
    '        .DataValueField = "COL1"
    '        .DataBind()
    '        .Items.Add(New ListItem("TODAS", "*"))
    '    End With
    'End Sub

#End Region

    Private Sub btSend_ServerClick(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs)

    End Sub

    Private Sub bBuscarPorCelula_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bBuscarPorCelula.Click
        For i As Integer = 0 To 9
            totales(9) = 0
        Next

        pDatosCelula.Visible = True
        pDatosEjeCom.Visible = False
        pDatosVendVirtual.Visible = False


        Dim fecha_ini As DateTime = CType(txtIniPorCelula.Text, DateTime)
        Dim fecha_fin As DateTime = CType(txtFinPorCelula.Text, DateTime)

        Dim codAgente As String = ddlCelula.SelectedValue

        tUserInfo = Session(Constantes.CTE_ANDES_INFO_USUARIO)
        Dim cod_filial As String = tUserInfo.codigoFilial
        Dim cod_sucursal As String = tUserInfo.codigoSucursal

        ' Muestra o no el codigo y nombre de la promotora
        dgDatosEjeComercial.Columns(0).Visible = (codAgente = "*")
        dgDatosEjeComercial.Columns(1).Visible = (codAgente = "*")

        Dim dtResult As DataTable

        dtResult = ventas.resumenVentasEcatalogoCelula(codAgente, _
                                                            fecha_ini, _
                                                            fecha_fin, _
                                                            cod_filial, _
                                                            cod_sucursal)
        Dim dvResult As DataView = New DataView(dtResult)

        If chkSoloClientesEcatalogoCelula.Checked Then
            dvResult.RowFilter = "Es_Ecat=1"
        End If

        Session("DGResultado") = dtResult

        dgDatosCelula.DataSource = dvResult
        dgDatosCelula.DataBind()


        Literal1.Text = "$(""#op1"").addClass(""active"").show();" & vbCrLf & _
        "$(""#tab1"").show();"

    End Sub

    Private Sub bBuscarPorEjeCom_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bBuscarPorEjeCom.Click
        For i As Integer = 0 To 9
            totales(9) = 0
        Next

        pDatosCelula.Visible = False
        pDatosEjeCom.Visible = True
        pDatosVendVirtual.Visible = False


        Dim fecha_ini As DateTime = CType(txtIniPorEjeCom.Text, DateTime)
        Dim fecha_fin As DateTime = CType(txtFinPorEjeCom.Text, DateTime)

        Dim codAgente As String = ddlEjecutivoComercial.SelectedValue

        tUserInfo = Session(Constantes.CTE_ANDES_INFO_USUARIO)
        Dim cod_filial As String = tUserInfo.codigoFilial
        Dim cod_sucursal As String = tUserInfo.codigoSucursal

        ' Muestra o no el codigo y nombre de la promotora
        dgDatosEjeComercial.Columns(0).Visible = (codAgente = "*")
        dgDatosEjeComercial.Columns(1).Visible = (codAgente = "*")

        Dim dtResult As DataTable

        dtResult = ventas.resumenEcatalogoCarteraPromotora(codAgente, "", _
                                                            fecha_ini, _
                                                            fecha_fin, _
                                                            cod_filial, _
                                                            cod_sucursal)

        Dim dvResult As DataView = New DataView(dtResult)

        If chkbSoloClientesEcatalogoEjeCom.Checked Then
            dvResult.RowFilter = "Es_Ecat=1"
        End If

        Session("DGResultado") = dtResult

        dgDatosEjeComercial.DataSource = dvResult
        dgDatosEjeComercial.DataBind()


        Literal1.Text = "$(""#op2"").addClass(""active"").show();" & vbCrLf & _
        "$(""#tab2"").show();"

    End Sub

    Private Sub bBuscarPorVendVirtual_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bBuscarPorVendVirtual.Click

        For i As Integer = 0 To 9
            totales(9) = 0
        Next

        pDatosCelula.Visible = False
        pDatosEjeCom.Visible = False
        pDatosVendVirtual.Visible = True

        Dim fecha_ini As DateTime = CType(txtIniPorVendVirt.Text, DateTime)
        Dim fecha_fin As DateTime = CType(txtFinPorVendVirt.Text, DateTime)

        Dim codAgente As String = ddlVendedoraVirtual.SelectedValue

        tUserInfo = Session(Constantes.CTE_ANDES_INFO_USUARIO)
        Dim cod_filial As String = tUserInfo.codigoFilial
        Dim cod_sucursal As String = tUserInfo.codigoSucursal

        ' Muestra o no el codigo y nombre de la promotora
        dgDatosVendVirtual.Columns(0).Visible = (codAgente = "*")
        dgDatosVendVirtual.Columns(1).Visible = (codAgente = "*")

        Dim dtResult As DataTable

        dtResult = ventas.resumenEcatalogoCarteraVirtual(codAgente, _
                                                            fecha_ini, _
                                                            fecha_fin, _
                                                            cod_filial, _
                                                            cod_sucursal)

        Dim dvResult As DataView = New DataView(dtResult)

        If chkbSoloClientesEcatalogoEjeCom.Checked Then
            dvResult.RowFilter = "Es_Ecat=1"
        End If

        Session("DGResultado") = dtResult

        dgDatosVendVirtual.DataSource = dvResult
        dgDatosVendVirtual.DataBind()


        Literal1.Text = "$(""#op3"").addClass(""active"").show();" & vbCrLf & _
                         "$(""#tab3"").show();"
    End Sub

    Private Sub dgDatosEjeComercial_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgDatosEjeComercial.ItemDataBound
        Dim i As Int16

        '  DG HEADER  CODE
        If e.Item.ItemType = ListItemType.Header Then

        End If

        ' DG  ITEM CODE
        Dim cl As New System.Globalization.CultureInfo("es-CL")
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then

            If (Not chkbSoloClientesEcatalogoEjeCom.Checked) Or _
                    (chkbSoloClientesEcatalogoEjeCom.Checked And e.Item.Cells(17).Text() = "1") Then

                totRows = totRows + 1

                ' Calcula totales...
                ' Total Vta Dolar eCat
                totales(0) += CDbl(e.Item.Cells(5).Text())
                ' Total Vta Dolar GMS
                totales(1) += CDbl(e.Item.Cells(6).Text())
                ' Total mg dolar ecat
                totales(2) += CDbl(e.Item.Cells(8).Text())
                ' Total mg dolar gms
                totales(3) += CDbl(e.Item.Cells(9).Text())
                ' Total # ped eCat
                totales(4) += CDbl(e.Item.Cells(11).Text())
                ' Total # ped gms
                totales(5) += CDbl(e.Item.Cells(12).Text())
                ' Total saldo epesos
                totales(6) += CDbl(e.Item.Cells(14).Text())
                ' Total # epesos utilizados
                totales(7) += CDbl(e.Item.Cells(15).Text())
                ' Cantidad de Registros que pertenecen a eCatalogo...
                If e.Item.Cells(17).Text = "1" Then
                    totales(8) += 1
                End If

            End If

            ' *** CODIGO PARA HIGHLIGHT  -START- ******
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='#FFFF00';")
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='#E8F0FF'")
            End If

            If e.Item.ItemType = ListItemType.Item Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='AliceBlue'")
            End If

            ' *** CODIGO PARA HIGHLIGHT  -END- ******
        End If

        ' DG  FOOTER CODE
        'Use the footer to display the summary row.
        If e.Item.ItemType = ListItemType.Footer Then
            ''Muestra totales
            e.Item.Cells(4).Text() = "Totales : "
            e.Item.Cells(5).Text() = String.Format(cl, "{0:N2}", totales(0))
            e.Item.Cells(6).Text() = String.Format(cl, "{0:N2}", totales(1))
            ' Promedio valores anteriores...
            e.Item.Cells(7).Text() = String.Format(cl, "{0:##0.00%}", totales(0) / totales(1))

            e.Item.Cells(8).Text() = String.Format(cl, "{0:N2}", totales(2))
            e.Item.Cells(9).Text() = String.Format(cl, "{0:N2}", totales(3))
            ' Promedio valores anteriores...
            e.Item.Cells(10).Text() = String.Format(cl, "{0:##0.00%}", totales(2) / totales(3))

            e.Item.Cells(11).Text() = String.Format(cl, "{0:N0}", totales(4))
            e.Item.Cells(12).Text() = String.Format(cl, "{0:N0}", totales(5))
            ' Promedio valores anteriores...
            e.Item.Cells(13).Text() = String.Format(cl, "{0:##0.00%}", totales(4) / totales(5))

            e.Item.Cells(14).Text() = String.Format(cl, "{0:N0}", totales(6))
            e.Item.Cells(15).Text() = String.Format(cl, "{0:N0}", totales(7))

            lClientesEjeCom.Text = String.Format(cl, "{0:N0}", totRows)
            lClientesEcatalogoEjeCom.Text = String.Format(cl, "{0:N0}", totales(8))

        End If
    End Sub

    Private Sub dgDatosEjeComercial_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgDatosEjeComercial.SortCommand
        Try
            Dim ColumnToSort As String
            Dim SortExprs() As String
            Dim i As Integer

            ' Limpiamos totales, en caso contrario se duplican
            For i = 0 To 9
                totales(i) = 0
            Next

            SortExprs = Split(e.SortExpression, " ")
            ColumnToSort = SortExprs(0)

            If e.SortExpression.ToLower = Me.SortExpression.ToLower Then
                ' SortAscending = Not SortAscending
                Me.SortExpression = ColumnToSort & " DESC"
            Else
                'SortAscending = True
                Me.SortExpression = ColumnToSort & " ASC"
            End If


            Dim dtResult As DataTable = Session("DGResultado")

            Dim dv As DataView = New DataView(dtResult)
            dv.Sort = Me.SortExpression

            If chkbSoloClientesEcatalogoEjeCom.Checked Then
                dv.RowFilter = "Es_Ecat=1"
            End If

            dgDatosEjeComercial.DataSource = dv
            dgDatosEjeComercial.DataBind()



        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub dgDatosVendVirtual_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgDatosVendVirtual.ItemDataBound
        Dim i As Int16

        '  DG HEADER  CODE
        If e.Item.ItemType = ListItemType.Header Then

        End If

        ' DG  ITEM CODE
        Dim cl As New System.Globalization.CultureInfo("es-CL")
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then

            If (Not chkbSoloClientesEcatalogoVendVirtual.Checked) Or _
                    (chkbSoloClientesEcatalogoVendVirtual.Checked And e.Item.Cells(17).Text() = "1") Then

                totRows = totRows + 1

                ' Calcula totales...
                ' Total Vta Dolar eCat
                totales(0) += CDbl(e.Item.Cells(5).Text())
                ' Total Vta Dolar GMS
                totales(1) += CDbl(e.Item.Cells(6).Text())
                ' Total mg dolar ecat
                totales(2) += CDbl(e.Item.Cells(8).Text())
                ' Total mg dolar gms
                totales(3) += CDbl(e.Item.Cells(9).Text())
                ' Total # ped eCat
                totales(4) += CDbl(e.Item.Cells(11).Text())
                ' Total # ped gms
                totales(5) += CDbl(e.Item.Cells(12).Text())
                ' Total saldo epesos
                totales(6) += CDbl(e.Item.Cells(14).Text())
                ' Total # epesos utilizados
                totales(7) += CDbl(e.Item.Cells(15).Text())
                ' Cantidad de Registros que pertenecen a eCatalogo...
                If e.Item.Cells(17).Text = "1" Then
                    totales(8) += 1
                End If

            End If

            ' *** CODIGO PARA HIGHLIGHT  -START- ******
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='#FFFF00';")
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='#E8F0FF'")
            End If

            If e.Item.ItemType = ListItemType.Item Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='AliceBlue'")
            End If

            ' *** CODIGO PARA HIGHLIGHT  -END- ******
        End If

        ' DG  FOOTER CODE
        'Use the footer to display the summary row.
        If e.Item.ItemType = ListItemType.Footer Then
            ''Muestra totales
            e.Item.Cells(4).Text() = "Totales : "
            e.Item.Cells(5).Text() = String.Format(cl, "{0:N2}", totales(0))
            e.Item.Cells(6).Text() = String.Format(cl, "{0:N2}", totales(1))
            ' Promedio valores anteriores...
            e.Item.Cells(7).Text() = String.Format(cl, "{0:##0.00%}", totales(0) / totales(1))

            e.Item.Cells(8).Text() = String.Format(cl, "{0:N2}", totales(2))
            e.Item.Cells(9).Text() = String.Format(cl, "{0:N2}", totales(3))
            ' Promedio valores anteriores...
            e.Item.Cells(10).Text() = String.Format(cl, "{0:##0.00%}", totales(2) / totales(3))

            e.Item.Cells(11).Text() = String.Format(cl, "{0:N0}", totales(4))
            e.Item.Cells(12).Text() = String.Format(cl, "{0:N0}", totales(5))
            ' Promedio valores anteriores...
            e.Item.Cells(13).Text() = String.Format(cl, "{0:##0.00%}", totales(4) / totales(5))

            e.Item.Cells(14).Text() = String.Format(cl, "{0:N0}", totales(6))
            e.Item.Cells(15).Text() = String.Format(cl, "{0:N0}", totales(7))

            lClientesVendVirtual.Text = String.Format(cl, "{0:N0}", totRows)
            lClientesEcatalogoVendVirt.Text = String.Format(cl, "{0:N0}", totales(8))

        End If
    End Sub

    Private Sub dgDatosVendVirtual_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgDatosVendVirtual.SortCommand
        Try
            Dim ColumnToSort As String
            Dim SortExprs() As String
            Dim i As Integer

            ' Limpiamos totales, en caso contrario se duplican
            For i = 0 To 9
                totales(i) = 0
            Next

            SortExprs = Split(e.SortExpression, " ")
            ColumnToSort = SortExprs(0)

            If e.SortExpression.ToLower = Me.SortExpression.ToLower Then
                ' SortAscending = Not SortAscending
                Me.SortExpression = ColumnToSort & " DESC"
            Else
                'SortAscending = True
                Me.SortExpression = ColumnToSort & " ASC"
            End If


            Dim dtResult As DataTable = Session("DGResultado")

            Dim dv As DataView = New DataView(dtResult)
            dv.Sort = Me.SortExpression

            If chkbSoloClientesEcatalogoEjeCom.Checked Then
                dv.RowFilter = "Es_Ecat=1"
            End If

            dgDatosVendVirtual.DataSource = dv
            dgDatosVendVirtual.DataBind()



        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub dgDatosCelula_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgDatosCelula.ItemDataBound
        Dim i As Int16

        '  DG HEADER  CODE
        If e.Item.ItemType = ListItemType.Header Then

        End If

        ' DG  ITEM CODE
        Dim cl As New System.Globalization.CultureInfo("es-CL")
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then

            If (Not chkSoloClientesEcatalogoCelula.Checked) Or _
                    (chkSoloClientesEcatalogoCelula.Checked And e.Item.Cells(17).Text() = "1") Then

                totRows = totRows + 1

                ' Calcula totales...
                ' Total Vta Dolar eCat
                totales(0) += CDbl(e.Item.Cells(5).Text())
                ' Total Vta Dolar GMS
                totales(1) += CDbl(e.Item.Cells(6).Text())
                ' Total mg dolar ecat
                totales(2) += CDbl(e.Item.Cells(8).Text())
                ' Total mg dolar gms
                totales(3) += CDbl(e.Item.Cells(9).Text())
                ' Total # ped eCat
                totales(4) += CDbl(e.Item.Cells(11).Text())
                ' Total # ped gms
                totales(5) += CDbl(e.Item.Cells(12).Text())
                ' Total saldo epesos
                totales(6) += CDbl(e.Item.Cells(14).Text())
                ' Total # epesos utilizados
                totales(7) += CDbl(e.Item.Cells(15).Text())
                ' Cantidad de Registros que pertenecen a eCatalogo...
                If e.Item.Cells(17).Text = "1" Then
                    totales(8) += 1
                End If

            End If

            ' *** CODIGO PARA HIGHLIGHT  -START- ******
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='#FFFF00';")
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='#E8F0FF'")
            End If

            If e.Item.ItemType = ListItemType.Item Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='AliceBlue'")
            End If

            ' *** CODIGO PARA HIGHLIGHT  -END- ******
        End If

        ' DG  FOOTER CODE
        'Use the footer to display the summary row.
        If e.Item.ItemType = ListItemType.Footer Then
            ''Muestra totales
            e.Item.Cells(4).Text() = "Totales : "
            e.Item.Cells(5).Text() = String.Format(cl, "{0:N2}", totales(0))
            e.Item.Cells(6).Text() = String.Format(cl, "{0:N2}", totales(1))
            ' Promedio valores anteriores...
            e.Item.Cells(7).Text() = String.Format(cl, "{0:##0.00%}", totales(0) / totales(1))

            e.Item.Cells(8).Text() = String.Format(cl, "{0:N2}", totales(2))
            e.Item.Cells(9).Text() = String.Format(cl, "{0:N2}", totales(3))
            ' Promedio valores anteriores...
            e.Item.Cells(10).Text() = String.Format(cl, "{0:##0.00%}", totales(2) / totales(3))

            e.Item.Cells(11).Text() = String.Format(cl, "{0:N0}", totales(4))
            e.Item.Cells(12).Text() = String.Format(cl, "{0:N0}", totales(5))
            ' Promedio valores anteriores...
            e.Item.Cells(13).Text() = String.Format(cl, "{0:##0.00%}", totales(4) / totales(5))

            e.Item.Cells(14).Text() = String.Format(cl, "{0:N0}", totales(6))
            e.Item.Cells(15).Text() = String.Format(cl, "{0:N0}", totales(7))

            lClientesCelula.Text = String.Format(cl, "{0:N0}", totRows)
            lClientesEcatalogoCelula.Text = String.Format(cl, "{0:N0}", totales(8))

        End If
    End Sub

    Private Sub dgDatosCelula_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgDatosCelula.SortCommand
        Try
            Dim ColumnToSort As String
            Dim SortExprs() As String
            Dim i As Integer

            ' Limpiamos totales, en caso contrario se duplican
            For i = 0 To 9
                totales(i) = 0
            Next

            SortExprs = Split(e.SortExpression, " ")
            ColumnToSort = SortExprs(0)

            If e.SortExpression.ToLower = Me.SortExpression.ToLower Then
                ' SortAscending = Not SortAscending
                Me.SortExpression = ColumnToSort & " DESC"
            Else
                'SortAscending = True
                Me.SortExpression = ColumnToSort & " ASC"
            End If


            Dim dtResult As DataTable = Session("DGResultado")

            Dim dv As DataView = New DataView(dtResult)
            dv.Sort = Me.SortExpression

            If chkbSoloClientesEcatalogoEjeCom.Checked Then
                dv.RowFilter = "Es_Ecat=1"
            End If

            dgDatosCelula.DataSource = dv
            dgDatosCelula.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
End Class
