Imports Exportador
Imports System.Data
Imports System.Data.SqlClient



Public Class ido_sel_analisis_precios_cartera_promotora_x_cliente_area_clasificacion
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents MPTitle As Wilson.MasterPages.ContentRegion
    Protected WithEvents MPCaption As Wilson.MasterPages.ContentRegion
    Protected WithEvents lbErrors As System.Web.UI.WebControls.Label
    Protected WithEvents dgResultado As System.Web.UI.WebControls.DataGrid
    Protected WithEvents MPContainer As Wilson.MasterPages.MasterPage

    Dim tUserInfo As usuario.t_Usuario
    Protected WithEvents ibExportar As System.Web.UI.WebControls.ImageButton

    Protected WithEvents lTituloInforme As System.Web.UI.WebControls.Label
    Protected WithEvents tbFechaInicio As System.Web.UI.WebControls.TextBox
    Protected WithEvents tbFechaTermino As System.Web.UI.WebControls.TextBox
    Protected WithEvents bConsultar As System.Web.UI.WebControls.Button
    Protected WithEvents lbNota As System.Web.UI.WebControls.Label
    Protected WithEvents lNoCoincidencias As System.Web.UI.WebControls.Label
    Protected WithEvents ddlEjecutivaComercial As System.Web.UI.WebControls.DropDownList

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim totalVentaUsdAcumulada As Double = 0
    Dim totalMgUsdAcumulado As Double = 0

    Const codigoFilial As String = "CHI"
    Const codigoSucursal As String = "001"
    'Const codigoPromotora As String = "11"
    'Dim fechaInicio As DateTime = New Date(2009, 10, 1)
    'Dim fechaTermino As DateTime = New Date(2009, 10, 30)

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Session(Constantes.CTE_ANDES_USUARIO_AUTENTICADO) <> "SI" Then
            Response.Redirect("/logout.aspx")
            Return
        End If

        If Page.IsPostBack = False Then

            'Cargamos fechas por defecto
            tbFechaInicio.Text = (New Date(Now.Year, 1, 1)).ToString("dd/MM/yyyy")
            tbFechaTermino.Text = Now.ToString("dd/MM/yyyy")

            tUserInfo = Session(Constantes.CTE_ANDES_INFO_USUARIO)

            cargaPromotoras(tUserInfo.codigoFilial, tUserInfo.codigoSucursal)
            If tUserInfo.perfil = "EJEC" Then
                ddlEjecutivaComercial.Enabled = True
            Else
                Try
                    ddlEjecutivaComercial.Items.FindByValue(tUserInfo.codigoPromo).Selected = True
                Catch ex As Exception
                    ddlEjecutivaComercial.Items.Clear()
                    ddlEjecutivaComercial.Items.Add(New ListItem("No se pudo cargar lista de valores", "-"))
                    ddlEjecutivaComercial.Items.FindByValue("-").Selected = True
                End Try

                ddlEjecutivaComercial.Enabled = False
            End If
        End If
    End Sub

    Private Sub cargaAnalisisCarteraClienteAreaClasificacion(ByVal codigoFilial As String, _
                                            ByVal codigoSucursal As String, _
                                            ByVal fechaInicio As DateTime, _
                                            ByVal fechaTermino As DateTime, _
                                            ByVal codigoPromotora As String)

        Dim dt As DataTable = obtieneAnalisisPreciosCartera(codigoFilial, _
                                                            codigoSucursal, _
                                                            fechaInicio, _
                                                            fechaTermino, _
                                                            codigoPromotora)

        With dgResultado
            .DataSource = dt
            .DataBind()
        End With

        Session("DGResultado") = dt


        If dt.Rows.Count = 0 Then
            lNoCoincidencias.Text = String.Format("No se encontraron ventas dentro del período para cartera {0}.", _
                                                    codigoPromotora)
            lNoCoincidencias.Visible = True
        Else
            lNoCoincidencias.Visible = False
        End If

        lTituloInforme.Text = String.Format("Análisis de cartera {0} cliente-area-clasificación<br>Período {1} al {2}", _
                                            codigoPromotora, _
                                            fechaInicio.ToString("dd/MMM/yyyy"), _
                                            fechaTermino.ToString("dd/MMM/yyyy"))

    End Sub

    Private Function obtieneAnalisisPreciosCartera(ByVal codigoFilial As String, _
                                                    ByVal codigoSucursal As String, _
                                                    ByVal fechaInicio As DateTime, _
                                                    ByVal fechaTermino As DateTime, _
                                                    ByVal codigoPromotora As String) As DataTable

        Const spName = "ido_sel_analisis_precios_cartera_promotora_x_cliente_area_clasificacion"
        Dim dbConn As SqlConnection = New SqlConnection
        Dim spCall As SqlCommand = New SqlCommand(spName, dbConn)
        Dim daSql As SqlDataAdapter = New SqlDataAdapter

        Try
            dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
            dbConn.Open()

            spCall.CommandType = CommandType.StoredProcedure

            spCall.Parameters.Add("@resultValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
            spCall.Parameters.Add("@cod_filial", codigoFilial).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_sucursal", codigoSucursal).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_promotora", codigoPromotora).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@fec_inicio", fechaInicio).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@fec_termino", fechaTermino).Direction = ParameterDirection.Input

            daSql.SelectCommand = spCall

            Dim resultDT As DataTable = New DataTable
            daSql.Fill(resultDT)
            Return resultDT

        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            dbConn.Dispose()
            spCall.Dispose()
            daSql.Dispose()
        End Try
    End Function






#Region " ORDENAMIENTO DE COLUMNAS DATAGRID "

    'The Page-level properties that write to ViewState
    Private Property SortExpression() As String
        Get
            Dim o As Object = Session("SortExpression")
            If o Is Nothing Then
                Return String.Empty
            Else
                Return o.ToString
            End If
        End Get
        Set(ByVal Value As String)
            Session("SortExpression") = Value
        End Set
    End Property


    Private Sub dgResultado_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgResultado.SortCommand
        Try
            Dim ColumnToSort As String
            Dim SortExprs() As String
            Dim i As Integer

            SortExprs = Split(e.SortExpression, " ")
            ColumnToSort = SortExprs(0)

            If Me.SortExpression.ToLower = ColumnToSort & " asc" Then
                ' SortAscending = Not SortAscending
                Me.SortExpression = ColumnToSort & " desc"
            Else
                'SortAscending = True
                Me.SortExpression = ColumnToSort & " asc"
            End If


            Dim dtResult As DataTable = Session("DGResultado")

            Dim dv As DataView = New DataView(dtResult)
            dv.Sort = Me.SortExpression

            dgResultado.DataSource = dv
            dgResultado.DataBind()

            ibExportar.Visible = True

        Catch ex As Exception
            lbErrors.Text = "ERRORES EN PAGINA: " & ex.Message
            Err.Clear()
        End Try

    End Sub
#End Region


    Private Sub bConsultar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bConsultar.Click

        Dim fechaInicio As DateTime
        Dim fechaTermino As DateTime

        Try
            fechaInicio = CDate(tbFechaInicio.Text)
            fechaTermino = CDate(tbFechaTermino.Text)

        Catch ex As Exception
            With dgResultado
                .DataSource = Nothing
                .DataBind()
            End With
            lTituloInforme.Text = ""
            lbErrors.Text = "Formato de fecha de consulta inválida."
            Return
        End Try

        Dim codigoPromotora As String = ddlEjecutivaComercial.SelectedValue

        tUserInfo = Session(Constantes.CTE_ANDES_INFO_USUARIO)

        cargaAnalisisCarteraClienteAreaClasificacion(codigoFilial, codigoSucursal, fechaInicio, fechaTermino, codigoPromotora)

    End Sub

#Region " INICIALIZACION DE CONTROLES FORMULARIO "
    Private Sub cargaPromotoras(ByVal codigoFilial As String, ByVal codigoSucursal As String)
        Dim xDatos As DataTable = Utiles.ObtienePromotoras(codigoFilial)
        With ddlEjecutivaComercial
            .Visible = True
            .DataSource = xDatos
            .DataTextField = "COL2"
            .DataValueField = "COL1"
            .DataBind()
            .Items.Add(New ListItem("TODAS", "*"))
        End With
    End Sub

#End Region

    Private Sub dgResultado_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgResultado.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            totalVentaUsdAcumulada += CType(e.Item.DataItem.item("vta_dolar_acumulada"), Double)
            totalMgUsdAcumulado += CType(e.Item.DataItem.item("mg_dolar_acumulado"), Double)
        ElseIf e.Item.ItemType = ListItemType.Footer Then
            e.Item.Cells(4).Text = totalVentaUsdAcumulada.ToString("#,##0.00")
            e.Item.Cells(5).Text = totalMgUsdAcumulado.ToString("#,##0.00")
            e.Item.Cells(6).Text = (100 * totalMgUsdAcumulado / totalVentaUsdAcumulada).ToString("#,##0.0") & "%"
        End If
    End Sub

    Private Sub ddlEjecutivaComercial_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlEjecutivaComercial.SelectedIndexChanged

    End Sub

    Private Sub ibExportar_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibExportar.Click

    End Sub
End Class
