Imports Exportador
Imports System.Data
Imports System.Data.SqlClient

Public Class comparativo_precios_cartera_promotora_np
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

    Protected WithEvents ddlFamilia As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlSubfamilia As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlArea As System.Web.UI.WebControls.DropDownList
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
            'Cargamos jerarquia por defecto
            cargaAreas(codigoFilial)
            ddlArea.ClearSelection()
            ddlArea.Items.FindByValue("CVI").Selected = True

            cargaFamilias(codigoFilial, ddlArea.SelectedValue)
            ddlFamilia.ClearSelection()
            ddlFamilia.Items.FindByValue("300").Selected = True

            cargaSubfamilias(codigoFilial, ddlArea.SelectedValue, ddlFamilia.SelectedValue)
            ddlSubfamilia.ClearSelection()
            ddlSubfamilia.Items.FindByValue("APHJ").Selected = True

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

    Private Sub cargaComparativoPreciosCarteraNP(ByVal codigoFilial As String, _
                                            ByVal codigoSucursal As String, _
                                            ByVal fechaInicio As DateTime, _
                                            ByVal fechaTermino As DateTime, _
                                            ByVal codigoPromotora As String, _
                                            ByVal codigoArea As String, _
                                            ByVal codigoFamilia As String, _
                                            ByVal codigoSubfamilia As String)

        Dim dt As DataTable = obtieneComparativoPreciosCartera(codigoFilial, _
                                                            codigoSucursal, _
                                                            fechaInicio, _
                                                            fechaTermino, _
                                                            codigoPromotora, _
                                                            codigoSubfamilia)

        With dgResultado
            .DataSource = dt
            .DataBind()
        End With

        Session("DGResultado") = dt


        If dt.Rows.Count = 0 Then
            lNoCoincidencias.Text = String.Format("No se encontraron ventas dentro del período para cartera {0} y materiales {1}-{2}-{3}.", _
                                                    codigoPromotora, _
                                                    codigoArea, _
                                                    codigoFamilia, _
                                                    codigoSubfamilia)
            lNoCoincidencias.Visible = True
        Else
            lNoCoincidencias.Visible = False
        End If

        lTituloInforme.Text = String.Format("Análisis de precios cartera {0} para materiales {1}-{2}-{3}<br>del {4} al {5}", _
                                            codigoPromotora, _
                                            ddlArea.SelectedValue, _
                                            ddlFamilia.SelectedValue, _
                                            ddlSubfamilia.SelectedValue, _
                                            fechaInicio.ToString("dd/MMM/yyyy"), _
                                            fechaTermino.ToString("dd/MMM/yyyy"))

    End Sub

    Private Function obtieneComparativoPreciosCartera(ByVal codigoFilial As String, _
                                                    ByVal codigoSucursal As String, _
                                                    ByVal fechaInicio As DateTime, _
                                                    ByVal fechaTermino As DateTime, _
                                                    ByVal codigoPromotora As String, _
                                                    ByVal codigoSubfamilia As String) As DataTable

        Const spName = "ido_sel_comparativo_precios_cartera_promotora_np"
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
            spCall.Parameters.Add("@cod_subfamilia", codigoSubfamilia).Direction = ParameterDirection.Input

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




    Private Sub ddlFamilia_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlFamilia.SelectedIndexChanged
        cargaSubfamilias(codigoFilial, ddlArea.SelectedValue, ddlFamilia.SelectedValue)
        ddlSubfamilia.ClearSelection()
        ddlSubfamilia.Items(0).Selected = True
    End Sub

    Private Sub ddlArea_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlArea.SelectedIndexChanged
        cargaFamilias(codigoFilial, ddlArea.SelectedValue)
        ddlFamilia.ClearSelection()
        ddlFamilia.Items(0).Selected = True

        cargaSubfamilias(codigoFilial, ddlArea.SelectedValue, ddlFamilia.SelectedValue)
        ddlSubfamilia.ClearSelection()
        ddlSubfamilia.Items(0).Selected = True

    End Sub

    Private Sub cargaAreas(ByVal codigoFilial As String)
        'Cargamos areas
        Dim wsAndes As cl.gms.andes.ws.materiales.materialesSrv = New cl.gms.andes.ws.materiales.materialesSrv
        With ddlArea
            .DataSource = wsAndes.obtieneAreas(codigoFilial)
            .DataValueField = "cod_area"
            .DataTextField = "cod_area"
            .DataBind()
        End With
        wsAndes = Nothing
    End Sub

    Private Sub cargaFamilias(ByVal codigoFilial As String, ByVal codigoArea As String)
        'Cargamos familias
        Dim wsAndes As cl.gms.andes.ws.materiales.materialesSrv = New cl.gms.andes.ws.materiales.materialesSrv
        With ddlFamilia
            .DataSource = wsAndes.obtieneFamilias(codigoFilial, codigoArea)
            .DataValueField = "cod_familia"
            .DataTextField = "cod_familia"
            .DataBind()
        End With
        wsAndes = Nothing
    End Sub

    Private Sub cargaSubfamilias(ByVal codigoFilial As String, ByVal codigoArea As String, ByVal codigoFamilia As String)
        'Cargamos subfamilias
        Dim wsAndes As cl.gms.andes.ws.materiales.materialesSrv = New cl.gms.andes.ws.materiales.materialesSrv
        With ddlSubfamilia
            .DataSource = wsAndes.obtieneSubFamilias(codigoFilial, codigoArea, codigoFamilia)
            .DataValueField = "cod_subfamilia"
            .DataTextField = "cod_subfamilia"
            .DataBind()
        End With
        wsAndes = Nothing

    End Sub



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
            Dim arrFecha() As String = Split(tbFechaInicio.Text, "/")
            fechaInicio = New Date(Integer.Parse(arrFecha(2)), Integer.Parse(arrFecha(1)), Integer.Parse(arrFecha(0)))

            arrFecha = Split(tbFechaTermino.Text, "/")
            fechaTermino = New Date(Integer.Parse(arrFecha(2)), Integer.Parse(arrFecha(1)), Integer.Parse(arrFecha(0)))
        Catch ex As Exception
            With dgResultado
                .DataSource = Nothing
                .DataBind()
            End With
            lTituloInforme.Text = ""
            lbErrors.Text = "Formato de fecha de consulta inválida."
            Return
        End Try


        Dim codigoArea As String = ddlArea.SelectedValue
        Dim codigoFamilia As String = ddlFamilia.SelectedValue
        Dim codigoSubfamilia As String = ddlSubfamilia.SelectedValue
        Dim codigoPromotora As String = ddlEjecutivaComercial.SelectedValue

        tUserInfo = Session(Constantes.CTE_ANDES_INFO_USUARIO)

        cargaComparativoPreciosCarteraNP(codigoFilial, codigoSucursal, fechaInicio, fechaTermino, codigoPromotora, codigoArea, codigoFamilia, codigoSubfamilia)

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

    Private Sub dgResultado_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgResultado.SelectedIndexChanged

    End Sub

    Private Sub dgResultado_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgResultado.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            If e.Item.Cells(13).Text.Trim = "MANUAL" Then
                e.Item.Cells(13).Text &= "<br>" & e.Item.Cells(14).Text.Trim
            End If
        End If
    End Sub
End Class
