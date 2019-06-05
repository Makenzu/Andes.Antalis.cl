Imports Exportador

Public Class vta_x_cliente_item
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents MPContainer As Wilson.MasterPages.MasterPage
    Protected WithEvents dgCliItem12 As System.Web.UI.WebControls.DataGrid
    Protected MPTitle As Wilson.MasterPages.ContentRegion
    Protected MPCaption As Wilson.MasterPages.ContentRegion

    Protected WithEvents HyperLink1 As System.Web.UI.WebControls.HyperLink
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
    Protected WithEvents ImageButton1 As System.Web.UI.WebControls.ImageButton
    Protected WithEvents lbErrors As System.Web.UI.WebControls.Label
    Protected WithEvents lbNomCli As System.Web.UI.WebControls.Label
    Protected WithEvents ddlMes As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlAno As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txItem As System.Web.UI.WebControls.TextBox
    Protected WithEvents btItem As System.Web.UI.WebControls.ImageButton
    Protected WithEvents txDesc As System.Web.UI.WebControls.TextBox
    Protected WithEvents btDesc As System.Web.UI.WebControls.ImageButton
    Protected WithEvents tblFiltro As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents ibExportar As System.Web.UI.WebControls.ImageButton


    Protected WithEvents rbUnidadMedida As System.Web.UI.WebControls.RadioButtonList
    Protected WithEvents txCliente As System.Web.UI.WebControls.TextBox
    Protected WithEvents bBuscar As System.Web.UI.WebControls.Button
    Protected WithEvents hfCodigoSociedad As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hfCodigoCliente As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents ddlSucursal As System.Web.UI.WebControls.DropDownList

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim totales(14) As Integer
    Dim arrMeses(14) As Integer
    Dim arrAnos(14) As Integer

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Session(Constantes.CTE_ANDES_USUARIO_AUTENTICADO) <> "SI" Then
            Response.Redirect("/logout.aspx")
            Return
        End If

        Dim usuarioSesion As t_Usuario = Session(Constantes.CTE_ANDES_INFO_USUARIO)
        lbErrors.Text = ""
        Page.Server.ScriptTimeout = 90

        If Not Page.IsPostBack Then
            tblFiltro.Visible = False
            For anho As Integer = Year(Date.Now) To (Year(Date.Now) - 5) Step -1
                ddlAno.Items.Add(anho)
            Next



            'AGS-20080620: Agregar listbox con sucursales
            With ddlSucursal
                .DataSource = Utiles.obtieneSucursales(usuarioSesion.codigoFilial, Utiles.eModoLista.Todos)
                .DataTextField = "des_sucursal"
                .DataValueField = "cod_sucursal"
                .DataBind()
            End With


            Dim i As Integer
            Dim x As Integer = 12
            For i = 0 To 11
                Dim newListItem As New ListItem
                If (Month(Date.Now) - i) >= 1 Then
                    newListItem.Text = MonthName(Month(Date.Now) - i)
                    newListItem.Value = Month(Date.Now) - i
                Else
                    newListItem.Text = MonthName(Month(Date.Now) + x)
                    newListItem.Value = Month(Date.Now) + x
                End If
                ddlMes.Items.Add(newListItem)

                If Month(Date.Now) - i = Month(Date.Now) Then ddlMes.Items(i).Selected = True
                x -= 1
            Next
        End If


        If Trim(Request("cc")) <> "" Then ' CONSULTA DESDE  OTRA PAGINA

            Dim codigoCliente As String = Request("cc").Trim
            Dim razonSocial As String = ""

            Dim anoPeriodo As Integer = Now.Year
            Dim mesPeriodo As Integer = Now.Month
            Dim umed As String = "UMB"
            Dim codigoFilial As String = usuarioSesion.codigoFilial
            Dim codigoSucursal As String = usuarioSesion.codigoSucursal

            rbUnidadMedida.Items(1).Selected = True

            Dim dtResult As DataTable = ventas.vta_x_cliente_item(codigoCliente, _
                                                                    "", _
                                                                    "", _
                                                                    mesPeriodo, _
                                                                    anoPeriodo, _
                                                                    codigoFilial, _
                                                                    codigoSucursal, _
                                                                    umed)

            cargaGrilla(dtResult)
        End If

    End Sub


    Private Sub dgCliItem12_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgCliItem12.SortCommand
        Try
            Dim ColumnToSort As String
            Dim SortExprs() As String

            SortExprs = Split(e.SortExpression, " ")
            ColumnToSort = SortExprs(0)

            If e.SortExpression.ToLower = Me.SortExpression.ToLower Then
                ' SortAscending = Not SortAscending
                Me.SortExpression = ColumnToSort & " ASC"
            Else
                'SortAscending = True
                Me.SortExpression = ColumnToSort & " DESC"
            End If

            Dim dv As DataView = New DataView(dgCliItem12.DataSource)
            dv.Sort = Me.SortExpression
            inicializaTotales()
            dgCliItem12.DataSource = dv
            dgCliItem12.DataBind()
        Catch ex As Exception
            lbErrors.Text = ex.Message
            Err.Clear()
        End Try
    End Sub


    Private Sub dgCliItem12_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgCliItem12.ItemDataBound
        Dim cl As New System.Globalization.CultureInfo("es-CL")
        Dim i, j As Integer

        Dim codigoCliente As String = txCliente.Text.Trim

        If e.Item.ItemType = ListItemType.Header Then
            Dim dateToShow As Date = New Date(ddlAno.SelectedValue, ddlMes.SelectedValue, 1)
            For i = 0 To 12
                e.Item.Cells(i + 4).Text = Format(dateToShow.AddMonths(-i), ("MMM/yy"))
                arrMeses(i) = dateToShow.AddMonths(-i).Month
                arrAnos(i) = dateToShow.AddMonths(-i).Year
            Next

        ElseIf (e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item) Then

            'definicion vinculo de detalle a pagina de facturacion
            Dim tipoConsulta As String = Request("rbUnidadMedida")
            For i = 4 To 16
                If Double.Parse(e.Item.Cells(i).Text) > 0 Then
                    e.Item.Cells(i).Text = String.Format("<a href=""vta_x_cliente_item_val_detalle.aspx?cf={0}&cs={1}&ap={2}&mp={3}&cc={4}&cp={5}&dp={6}&nc={7}&tc={8}"">{9}</a>", _
                                                            Trim(e.Item.DataItem.item("cod_filial")), _
                                                            Trim(ddlSucursal.SelectedValue), _
                                                            arrAnos(i - 4), _
                                                            arrMeses(i - 4), _
                                                            Trim(e.Item.DataItem.item("cod_cliente")), _
                                                            Trim(e.Item.DataItem.item("cod_producto")), _
                                                            Server.UrlEncode(Trim(e.Item.DataItem.item("des_producto"))), _
                                                            Server.UrlEncode(Trim(e.Item.DataItem.item("nom_cliente"))), _
                                                            tipoConsulta, _
                                                            e.Item.Cells(i).Text)
                End If
            Next

            totales(0) += CType(e.Item.DataItem.item("mes_actual"), Double)
            totales(1) += CType(e.Item.DataItem.item("mes_uno"), Double)
            totales(2) += CType(e.Item.DataItem.item("mes_dos"), Double)
            totales(3) += CType(e.Item.DataItem.item("mes_tres"), Double)
            totales(4) += CType(e.Item.DataItem.item("mes_cuatro"), Double)
            totales(5) += CType(e.Item.DataItem.item("mes_cinco"), Double)
            totales(6) += CType(e.Item.DataItem.item("mes_seis"), Double)
            totales(7) += CType(e.Item.DataItem.item("mes_siete"), Double)
            totales(8) += CType(e.Item.DataItem.item("mes_ocho"), Double)
            totales(9) += CType(e.Item.DataItem.item("mes_nueve"), Double)
            totales(10) += CType(e.Item.DataItem.item("mes_diez"), Double)
            totales(11) += CType(e.Item.DataItem.item("mes_once"), Double)
            totales(12) += CType(e.Item.DataItem.item("mes_doce"), Double)
            totales(13) += CType(e.Item.DataItem.item("val_mes_act"), Double)


            ' *** CODIGO PARA HIGHLIGHT  -START- ******
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='#FFFF00';")
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='#E8F0FF'")
            End If

            If e.Item.ItemType = ListItemType.Item Then
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='AliceBlue'")
            End If

        ElseIf e.Item.ItemType = ListItemType.Footer Then

            e.Item.Cells(2).Text = "TOTALES:"
            e.Item.Cells(2).HorizontalAlign = HorizontalAlign.Right

            For i = 0 To 13
                e.Item.Cells(i + 4).Text() = String.Format(cl, "{0:N0}", totales(i))
                e.Item.Cells(i + 4).HorizontalAlign = HorizontalAlign.Right
            Next
        End If
    End Sub

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

    Private Sub inicializaTotales()
        Dim i As Integer
        For i = 0 To totales.Length - 1
            totales(i) = 0
        Next
    End Sub

    Private Sub btItem_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btItem.Click
        Dim dv As DataView

        Try
            Dim Filter As String

            Filter = Replace(txItem.Text.Trim, "*", "%")
            txDesc.Text = ""

            dv = New DataView(dgCliItem12.DataSource)

            dv.RowFilter = "cod_producto like '" & Filter & "'"
            inicializaTotales()
            dgCliItem12.DataSource = dv
            dgCliItem12.DataBind()
        Catch ex As Exception
            lbErrors.Text = ex.Message
            lbErrors.Visible = True
            Err.Clear()
        Finally
            dv.Dispose()
        End Try
    End Sub

    Private Sub btDesc_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btDesc.Click
        Dim dv As DataView

        Try
            Dim Filter As String

            Filter = Replace(txDesc.Text.Trim, "*", "%")
            txItem.Text = ""

            dv = New DataView(dgCliItem12.DataSource)
            dv.RowFilter = "des_producto like '" & Filter & "'"
            inicializaTotales()
            dgCliItem12.DataSource = dv
            dgCliItem12.DataBind()
        Catch ex As Exception
            lbErrors.Text = ex.Message
            lbErrors.Visible = True
            Err.Clear()
            dv.Dispose()
        End Try
    End Sub

    Private Sub ibExportar_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibExportar.Click

        Dim codigoCliente As String = hfCodigoCliente.Value
        Dim nombreCliente As String = ""
        Dim codigoEjecutivaComercial As String = ""
        Dim mesPeriodo As Integer = CType(ddlMes.SelectedValue, Integer)
        Dim anoPeriodo As Integer = CType(ddlAno.SelectedValue, Integer)

        Dim usuarioSesion As t_Usuario = Session(Constantes.CTE_ANDES_INFO_USUARIO)

        Dim dtResult As DataTable = ventas.vta_x_cliente_item(codigoCliente, _
                                        "", _
                                        codigoEjecutivaComercial, _
                                        mesPeriodo, _
                                        anoPeriodo, _
                                        usuarioSesion.codigoFilial, _
                                        usuarioSesion.codigoSucursal, _
                                        Request("rbUnidadMedida"))

        If dtResult.Rows.Count > 0 Then
            Dim Exportar As New Exports.OpenExcel(Server.MapPath(ConfigurationSettings.AppSettings("OpenExcel.DefaultXml")))
            ' Configuracion de impresion
            Exportar.PageScale = 85
            Exportar.PageLayout = "Portrait"

            ' Encabezado y Pie de Pagina
            Exportar.RightHeader = ""
            Exportar.LeftFooter = Server.HtmlDecode("&amp;D&amp;P/&amp;#")

            ' Exportar
            codigoCliente = Trim(dtResult.Rows(0).Item("cod_cliente"))
            Dim razonSocial As String = Trim(dtResult.Rows(0).Item("nom_cliente"))

            Exportar.TableToExcel(preparaTabla(codigoCliente, razonSocial, dtResult))
            Exportar.SaveToClient(Response)

            Exportar = Nothing
        End If

    End Sub


    Private Sub cargaGrilla(ByVal dtResult As DataTable)
        inicializaTotales()
        With dgCliItem12
            .DataSource = dtResult
            .DataBind()
        End With

        If dtResult.Rows.Count > 0 Then
            tblFiltro.Visible = True
            ibExportar.Visible = True
            lbNomCli.Text = String.Format("{0} :: {1}", Trim(dtResult.Rows(0).Item("cod_cliente")), Trim(dtResult.Rows(0).Item("nom_cliente")))
        Else
            tblFiltro.Visible = False
            ibExportar.Visible = False
            lbNomCli.Text = ""
        End If
    End Sub

    Private Function preparaTabla(ByVal codigoCliente As String, _
                                ByVal razonSocial As String, _
                                ByVal dt As DataTable) As Table

        Dim totales(17) As Double
        For i As Integer = 0 To 17
            totales(i) = 0
        Next

        Dim tDatos As Table = New Table

        tDatos.Rows.Add(New TableRow)
        tDatos.Rows(tDatos.Rows.Count - 1).Cells.Add(New TableCell)
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(0).ColumnSpan = 18
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(0).Text = "VENTAS POR CLIENTE-ITEM"

        tDatos.Rows.Add(New TableRow)
        tDatos.Rows(tDatos.Rows.Count - 1).Cells.Add(New TableCell)
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(0).ColumnSpan = 18
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(0).Text = "Fecha:" & Now.ToString("dd/MMMM/yyyy HH:mm")

        tDatos.Rows.Add(New TableRow)
        tDatos.Rows(tDatos.Rows.Count - 1).Cells.Add(New TableCell)
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(0).ColumnSpan = 18
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(0).Text = String.Format("Cliente: {0} :: {1}", codigoCliente, razonSocial)

        tDatos.Rows.Add(New TableRow)
        tDatos.Rows(tDatos.Rows.Count - 1).Cells.Add(New TableCell)
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(0).ColumnSpan = 18
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(0).Text = ""

        tDatos.Rows.Add(New TableRow)
        tDatos.Rows(tDatos.Rows.Count - 1).Cells.Add(New TableCell)
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(0).ColumnSpan = 18
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(0).Text = ""


        tDatos.Rows.Add(New TableRow)
        tDatos.Rows(tDatos.Rows.Count - 1).Cells.Add(New TableCell)
        tDatos.Rows(tDatos.Rows.Count - 1).Cells.Add(New TableCell)
        tDatos.Rows(tDatos.Rows.Count - 1).Cells.Add(New TableCell)
        tDatos.Rows(tDatos.Rows.Count - 1).Cells.Add(New TableCell)

        tDatos.Rows(tDatos.Rows.Count - 1).Cells(0).Text = "SubF"
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(1).Text = "Código"
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(2).Text = "Descripción"
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(3).Text = "UM"


        Dim dateToShow As Date = New Date(ddlAno.SelectedValue, ddlMes.SelectedValue, 1)
        For i As Integer = 0 To 12
            tDatos.Rows(tDatos.Rows.Count - 1).Cells.Add(New TableCell)
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(4 + i).Text = Format(dateToShow.AddMonths(-i), ("MMM/yy"))
        Next

        tDatos.Rows(tDatos.Rows.Count - 1).Cells.Add(New TableCell)
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(17).Text = "$ Vta. Mes"


        For Each dr As DataRow In dt.Rows
            tDatos.Rows.Add(New TableRow)

            For i As Integer = 1 To 18
                tDatos.Rows(tDatos.Rows.Count - 1).Cells.Add(New TableCell)
            Next

            tDatos.Rows(tDatos.Rows.Count - 1).Cells(0).Text = Trim(dr("cod_subfamilia"))
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(1).Text = "'" & Trim(dr("cod_producto"))
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(2).Text = Trim(dr("des_producto"))
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(3).Text = Trim(dr("cod_um"))

            tDatos.Rows(tDatos.Rows.Count - 1).Cells(4).Text = CType(dr("mes_actual"), Double).ToString("##0.0")
            totales(0) += dr("mes_actual")
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(5).Text = CType(dr("mes_uno"), Double).ToString("##0.0")
            totales(1) += dr("mes_uno")
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(6).Text = CType(dr("mes_dos"), Double).ToString("##0.0")
            totales(2) += dr("mes_dos")
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(7).Text = CType(dr("mes_tres"), Double).ToString("##0.0")
            totales(3) += dr("mes_tres")
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(8).Text = CType(dr("mes_cuatro"), Double).ToString("##0.0")
            totales(4) += dr("mes_cuatro")
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(9).Text = CType(dr("mes_cinco"), Double).ToString("##0.0")
            totales(5) += dr("mes_cinco")
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(10).Text = CType(dr("mes_seis"), Double).ToString("##0.0")
            totales(6) += dr("mes_seis")
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(11).Text = CType(dr("mes_siete"), Double).ToString("##0.0")
            totales(7) += dr("mes_siete")
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(12).Text = CType(dr("mes_ocho"), Double).ToString("##0.0")
            totales(8) += dr("mes_ocho")
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(13).Text = CType(dr("mes_nueve"), Double).ToString("##0.0")
            totales(9) += dr("mes_nueve")
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(14).Text = CType(dr("mes_diez"), Double).ToString("##0.0")
            totales(10) += dr("mes_diez")
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(15).Text = CType(dr("mes_once"), Double).ToString("##0.0")
            totales(11) += dr("mes_once")
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(16).Text = CType(dr("mes_doce"), Double).ToString("##0.0")
            totales(12) += dr("mes_doce")
            tDatos.Rows(tDatos.Rows.Count - 1).Cells(17).Text = CType(dr("val_mes_act"), Double).ToString("##0")
            totales(13) += dr("val_mes_act")
        Next

        tDatos.Rows.Add(New TableRow)

        For i As Integer = 1 To 18
            tDatos.Rows(tDatos.Rows.Count - 1).Cells.Add(New TableCell)
        Next

        tDatos.Rows(tDatos.Rows.Count - 1).Cells(4).Text = "TOTALES"
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(4).Text = totales(0).ToString("#,##0.0")
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(5).Text = totales(1).ToString("#,##0.0")
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(6).Text = totales(2).ToString("#,##0.0")
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(7).Text = totales(3).ToString("#,##0.0")
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(8).Text = totales(4).ToString("#,##0.0")
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(9).Text = totales(5).ToString("#,##0.0")
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(10).Text = totales(6).ToString("#,##0.0")
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(11).Text = totales(7).ToString("#,##0.0")
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(12).Text = totales(8).ToString("#,##0.0")
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(13).Text = totales(9).ToString("#,##0.0")
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(14).Text = totales(10).ToString("#,##0.0")
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(15).Text = totales(11).ToString("#,##0.0")
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(16).Text = totales(12).ToString("#,##0.0")
        tDatos.Rows(tDatos.Rows.Count - 1).Cells(17).Text = totales(13).ToString("#,##0")

        Return tDatos

    End Function


    Private Sub bBuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bBuscar.Click

        If txCliente.Text.Trim = "" Then
            lbErrors.Text = "Debe indicar un cliente"
            Return
        End If

        Dim arrStr() = Split(txCliente.Text, "::")

        Dim codigoCliente As String = arrStr(0)
        hfCodigoCliente.Value = codigoCliente

        Dim infoUsuario As t_Usuario = CType(Session(Constantes.CTE_OBJ_USER_INFO), t_Usuario)

        Dim mesPeriodo As Integer = CType(ddlMes.SelectedValue, Integer)
        Dim anoPeriodo As Integer = CType(ddlAno.SelectedValue, Integer)
        Dim codigoFilial As String = infoUsuario.codigoFilial
        Dim codigoSucursal As String = ddlSucursal.SelectedValue

        Try
            Dim dtResult As DataTable = ventas.vta_x_cliente_item(codigoCliente, _
                                                                    "", _
                                                                    "", _
                                                                    mesPeriodo, _
                                                                    anoPeriodo, _
                                                                    codigoFilial, _
                                                                    codigoSucursal, _
                                                                    Request("rbUnidadMedida"))

            cargaGrilla(dtResult)

        Catch ex As Exception
            lbErrors.Text = "No se encontraron datos para el mes / cliente en consulta."
            Return
        End Try
    End Sub
End Class

