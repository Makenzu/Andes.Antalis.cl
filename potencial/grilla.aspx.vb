Imports System.Data.SqlClient
Imports System.Data

Public Class potencial_potencial
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents MPContainer As Wilson.MasterPages.MasterPage
    Protected WithEvents MPTitle As Wilson.MasterPages.ContentRegion
    Protected WithEvents MPCaption As Wilson.MasterPages.ContentRegion
    Protected WithEvents ddlVendedoraVirtual As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlCobrador As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlClasificacionAntalis As System.Web.UI.WebControls.DropDownList
    Protected WithEvents tbPatronCliente As System.Web.UI.WebControls.TextBox
    Protected WithEvents lMensajeAccionBusqueda As System.Web.UI.WebControls.Label
    Protected WithEvents ddlEjecutivaComercial As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlCelula As System.Web.UI.WebControls.DropDownList
    Protected WithEvents bBuscarPorPatron As System.Web.UI.WebControls.Button
    Protected WithEvents bBuscarPorVendVirtual As System.Web.UI.WebControls.Button
    Protected WithEvents bBuscarPorCobrador As System.Web.UI.WebControls.Button
    Protected WithEvents bBuscarPorCelula As System.Web.UI.WebControls.Button
    Protected WithEvents bBuscarPorClasificacionAntalis As System.Web.UI.WebControls.Button
    Protected WithEvents bBuscarPorEjecComercial As System.Web.UI.WebControls.Button
    Protected WithEvents Literal1 As System.Web.UI.WebControls.Literal
    Protected WithEvents pResultado1 As System.Web.UI.WebControls.Panel
    Protected WithEvents pResultado2 As System.Web.UI.WebControls.Panel
    Protected WithEvents pResultado3 As System.Web.UI.WebControls.Panel
    Protected WithEvents pResultado4 As System.Web.UI.WebControls.Panel
    Protected WithEvents pResultado5 As System.Web.UI.WebControls.Panel
    Protected WithEvents pResultado6 As System.Web.UI.WebControls.Panel

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Enum eTipoMensaje
        exito
        fallo
        ninguno
    End Enum

    Dim contador As Integer
    Dim codigoSociedad As String = "GMSC"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Session(Constantes.CTE_ANDES_USUARIO_AUTENTICADO) <> "SI" Then
            Response.Redirect("/logout.aspx")
            Return
        End If

        'Chequeamos que el usuario ejecutando la acción tenga acceso a este módulo
        Dim pu As CPermisoUsuario = Session(Constantes.CTE_OBJ_PERMISOS_USUARIO)
        If Not pu.validaPermiso(Constantes.MODULO_POTENCIAL_CLIENTE, CPermisoUsuario.eTipoAccion.lectura) Then
            Response.Redirect("/")
            Return
        End If

        'Solo ejecutivos comerciales tendrán acceso a todas las vistas.
        Literal1.Text = ""
        If Session(Constantes.CTE_OBJ_USER_INFO_PERFIL) <> "EJEC" Then
            Literal1.Text = "$('#op2').hide();" & vbCrLf & _
                             "$('#op3').hide();" & vbCrLf & _
                             "$('#op4').hide();" & vbCrLf & _
                             "$('#op5').hide();" & vbCrLf & _
                             "$('#op6').hide();" & vbCrLf
        End If

        If Page.IsPostBack = False Then
            Literal1.Text &= "$(""ul.tabs li:first"").addClass(""active"").show();" & vbCrLf & _
                            "$("".tab_content:first"").show();"

            Dim ws As cl.gms.andes.ws.OrgSrv = New cl.gms.andes.ws.OrgSrv
            With ddlEjecutivaComercial
                .DataSource = ws.listaPromotoras(codigoSociedad).Tables(0)
                .DataTextField = "nom_promotora_2"
                .DataValueField = "cod_promotora"
                .DataBind()
            End With

            Dim infoUsuario As t_Usuario = Session(Constantes.CTE_OBJ_USER_INFO)
            If infoUsuario.perfil = "PROM" Then
                If Not IsNothing(ddlEjecutivaComercial.Items.FindByValue(infoUsuario.codigoPromo)) Then
                    ddlEjecutivaComercial.ClearSelection()
                    ddlEjecutivaComercial.Items.FindByValue(infoUsuario.codigoPromo).Selected = True
                    ddlEjecutivaComercial.Enabled = False
                Else
                    Response.Redirect("/logout.aspx")
                End If
            End If

            With ddlVendedoraVirtual
                .DataSource = ws.listaVendedorasVirtuales(codigoSociedad).Tables(0)
                .DataTextField = "nom_vendedora_2"
                .DataValueField = "cod_vend_virtual"
                .DataBind()
            End With
            ws = Nothing

            With ddlClasificacionAntalis
                .DataSource = obtieneClasificacionClientesAntalis()
                .DataTextField = "denominacion2"
                .DataValueField = "cod_clasificacion"
                .DataBind()
            End With

            With ddlCelula
                .DataSource = obtieneCelulas()
                .DataTextField = "celula"
                .DataValueField = "celula"
                .DataBind()
            End With

            With ddlCobrador
                .DataSource = obtieneCobradores()
                .DataTextField = "nom_cobrador_2"
                .DataValueField = "cod_cobrador"
                .DataBind()
            End With

        End If
    End Sub

    Private Function obtieneClasificacionClientesAntalis() As DataTable
        Const spName = "pot_sel_clasificacion_antalis_cliente_x_patron"
        Dim dbConn As New SqlConnection
        Dim spCall As New SqlCommand(spName, dbConn)
        Dim daSql As New SqlDataAdapter
        Dim resultDT As New DataTable

        Try

            dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
            dbConn.Open()

            Dim msgError As String = ""
            Dim resultSQL As Integer

            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@return", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
            spCall.Parameters.Add("@query", "%").Direction = ParameterDirection.Input

            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()

            resultSQL = spCall.Parameters("@return").Value

            If resultSQL >= 0 Then
                daSql.Fill(resultDT)
                Return resultDT
            End If

            Return Nothing

        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            dbConn = Nothing
            spCall = Nothing
            daSql = Nothing
            resultDT = Nothing
        End Try

    End Function

    Private Function obtieneCelulas() As DataTable
        Const spName = "pot_sel_celulas"
        Dim dbConn As New SqlConnection
        Dim spCall As New SqlCommand(spName, dbConn)
        Dim daSql As New SqlDataAdapter
        Dim resultDT As New DataTable

        Try

            dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
            dbConn.Open()

            Dim msgError As String = ""
            Dim resultSQL As Integer

            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@return", SqlDbType.Int).Direction = ParameterDirection.ReturnValue

            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()

            resultSQL = spCall.Parameters("@return").Value

            daSql.Fill(resultDT)
            Return resultDT


        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            dbConn = Nothing
            spCall = Nothing
            daSql = Nothing
            resultDT = Nothing
        End Try

    End Function

    Private Function obtieneCobradores() As DataTable
        Const spName = "pot_sel_cobradores"
        Dim dbConn As New SqlConnection
        Dim spCall As New SqlCommand(spName, dbConn)
        Dim daSql As New SqlDataAdapter
        Dim resultDT As New DataTable

        Try

            dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
            dbConn.Open()

            Dim msgError As String = ""
            Dim resultSQL As Integer

            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@return", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
            spCall.Parameters.Add("@cod_sociedad", codigoSociedad).Direction = ParameterDirection.Input

            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()

            resultSQL = spCall.Parameters("@return").Value

            If resultSQL >= 0 Then
                daSql.Fill(resultDT)
                Return resultDT
            End If

            Return Nothing

        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            dbConn = Nothing
            spCall = Nothing
            daSql = Nothing
            resultDT = Nothing
        End Try

    End Function

    Private Sub bBuscarPorEjecComercial_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bBuscarPorEjecComercial.Click
        generaTabla(obtieneClientesPorEjecComercial(codigoSociedad, ddlEjecutivaComercial.SelectedValue), pResultado1)

        Literal1.Text &= "$(""#op1"").addClass(""active"").show();" & vbCrLf & _
                        "$(""#tab1"").show();"
    End Sub

    Private Sub bBuscarPorVendVirtual_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bBuscarPorVendVirtual.Click

        generaTabla(obtieneClientesPorVendVirtual(codigoSociedad, ddlVendedoraVirtual.SelectedValue), pResultado2)
        Literal1.Text = "$(""#op2"").addClass(""active"").show();" & vbCrLf & _
                        "$(""#tab2"").show();"
    End Sub

    Private Sub bBuscarPorCobrador_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bBuscarPorCobrador.Click

        generaTabla(obtieneClientesPorCobrador(codigoSociedad, ddlCobrador.SelectedValue), pResultado3)

        Literal1.Text = "$(""#op3"").addClass(""active"").show();" & vbCrLf & _
                "$(""#tab3"").show();"
    End Sub

    Private Sub bBuscarPorCelula_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bBuscarPorCelula.Click
        generaTabla(obtieneClientesPorCelula(codigoSociedad, ddlCelula.SelectedValue), pResultado4)

        Literal1.Text = "$(""#op4"").addClass(""active"").show();" & vbCrLf & _
        "$(""#tab4"").show();"
    End Sub

    Private Sub bBuscarPorClasificacionAntalis_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bBuscarPorClasificacionAntalis.Click

        generaTabla(obtieneClientesPorClasificacionAntalis(codigoSociedad, ddlClasificacionAntalis.SelectedValue), pResultado5)

        Literal1.Text = "$(""#op5"").addClass(""active"").show();" & vbCrLf & _
        "$(""#tab5"").show();"
    End Sub

    Private Sub bBuscarPorPatron_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bBuscarPorPatron.Click
        Literal1.Text = "$(""#op6"").addClass(""active"").show();" & vbCrLf & _
        "$(""#tab6"").show();"

        Dim arrStr As String() = tbPatronCliente.Text.Split("::")

        Dim dt As DataTable

        dt = obtieneClientesPorPatron(codigoSociedad, arrStr(0))

        If dt.Rows.Count = 1 Then
            'Vamos directo al cliente
            Response.Redirect("fichaCliente.aspx?cc=" & Trim(dt.Rows(0).Item("cod_cliente")))
            Return
        End If

        generaTabla(dt, pResultado6)

    End Sub

    Private Function obtieneClientesPorCobrador(ByVal codigoSociedad As String, ByVal codigoCobrador As String) As DataTable
        Const spName = "pot_sel_clientes_x_cobrador"
        Dim dbConn As New SqlConnection
        Dim spCall As New SqlCommand(spName, dbConn)
        Dim daSql As New SqlDataAdapter
        Dim resultDT As New DataTable

        Try

            dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
            dbConn.Open()

            Dim msgError As String = ""
            Dim resultSQL As Integer

            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@return", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
            spCall.Parameters.Add("@cod_sociedad", codigoSociedad).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_cobrador", codigoCobrador).Direction = ParameterDirection.Input

            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()

            resultSQL = spCall.Parameters("@return").Value

            If resultSQL >= 0 Then
                daSql.Fill(resultDT)
                Return resultDT
            End If

            Return Nothing

        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            dbConn = Nothing
            spCall = Nothing
            daSql = Nothing
            resultDT = Nothing
        End Try

    End Function

    Private Function obtieneClientesPorPatron(ByVal codigoSociedad As String, ByVal patron As String) As DataTable
        Const spName = "pot_sel_clientes_x_patron_v2"
        Dim dbConn As New SqlConnection
        Dim spCall As New SqlCommand(spName, dbConn)
        Dim daSql As New SqlDataAdapter
        Dim resultDT As New DataTable

        Try

            dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
            dbConn.Open()

            Dim msgError As String = ""
            Dim resultSQL As Integer

            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@return", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
            spCall.Parameters.Add("@cod_sociedad", codigoSociedad).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@query", patron).Direction = ParameterDirection.Input

            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()

            resultSQL = spCall.Parameters("@return").Value

            If resultSQL >= 0 Then
                daSql.Fill(resultDT)
                Return resultDT
            End If

            Return Nothing

        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            dbConn = Nothing
            spCall = Nothing
            daSql = Nothing
            resultDT = Nothing
        End Try

    End Function

    Private Function obtieneClientesPorCelula(ByVal codigoSociedad As String, ByVal codigoCelula As String) As DataTable
        Const spName = "pot_sel_clientes_x_celula"
        Dim dbConn As New SqlConnection
        Dim spCall As New SqlCommand(spName, dbConn)
        Dim daSql As New SqlDataAdapter
        Dim resultDT As New DataTable

        Try

            dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
            dbConn.Open()

            Dim msgError As String = ""
            Dim resultSQL As Integer

            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@return", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
            spCall.Parameters.Add("@cod_sociedad", codigoSociedad).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_celula", codigoCelula).Direction = ParameterDirection.Input

            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()

            resultSQL = spCall.Parameters("@return").Value

            If resultSQL >= 0 Then
                daSql.Fill(resultDT)
                Return resultDT
            End If

            Return Nothing

        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            dbConn = Nothing
            spCall = Nothing
            daSql = Nothing
            resultDT = Nothing
        End Try

    End Function

    Private Function obtieneClientesPorClasificacionAntalis(ByVal codigoSociedad As String, ByVal codigoClasificacionAntalis As String) As DataTable
        Const spName = "pot_sel_clientes_x_clasificacion_antalis"
        Dim dbConn As New SqlConnection
        Dim spCall As New SqlCommand(spName, dbConn)
        Dim daSql As New SqlDataAdapter
        Dim resultDT As New DataTable

        Try

            dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
            dbConn.Open()

            Dim msgError As String = ""
            Dim resultSQL As Integer

            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@return", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
            spCall.Parameters.Add("@cod_sociedad", codigoSociedad).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_cla_antalis", codigoClasificacionAntalis).Direction = ParameterDirection.Input

            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()

            resultSQL = spCall.Parameters("@return").Value

            If resultSQL >= 0 Then
                daSql.Fill(resultDT)
                Return resultDT
            End If

            Return Nothing

        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            dbConn = Nothing
            spCall = Nothing
            daSql = Nothing
            resultDT = Nothing
        End Try

    End Function

    Private Function obtieneClientesPorEjecComercial(ByVal codigoSociedad As String, ByVal codigoEjecComercial As String) As DataTable
        Const spName = "pot_sel_clientes_x_ejec_com"
        Dim dbConn As New SqlConnection
        Dim spCall As New SqlCommand(spName, dbConn)
        Dim daSql As New SqlDataAdapter
        Dim resultDT As New DataTable

        Try

            dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
            dbConn.Open()

            Dim msgError As String = ""
            Dim resultSQL As Integer

            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@return", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
            spCall.Parameters.Add("@cod_sociedad", codigoSociedad).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_ejec_com", codigoEjecComercial).Direction = ParameterDirection.Input

            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()

            resultSQL = spCall.Parameters("@return").Value

            If resultSQL >= 0 Then
                daSql.Fill(resultDT)
                Return resultDT
            End If

            Return Nothing

        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            dbConn = Nothing
            spCall = Nothing
            daSql = Nothing
            resultDT = Nothing
        End Try

    End Function

    Private Function obtieneClientesPorVendVirtual(ByVal codigoSociedad As String, ByVal codigoVendVirtual As String) As DataTable
        Const spName = "pot_sel_clientes_x_vend_virtual"
        Dim dbConn As New SqlConnection
        Dim spCall As New SqlCommand(spName, dbConn)
        Dim daSql As New SqlDataAdapter
        Dim resultDT As New DataTable

        Try

            dbConn.ConnectionString = Utiles.obtieneStringDeConexion()
            dbConn.Open()

            Dim msgError As String = ""
            Dim resultSQL As Integer

            spCall.CommandType = CommandType.StoredProcedure
            spCall.Parameters.Add("@return", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
            spCall.Parameters.Add("@cod_sociedad", codigoSociedad).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_vend_virtual", codigoVendVirtual).Direction = ParameterDirection.Input

            daSql.SelectCommand = spCall
            daSql.SelectCommand.ExecuteNonQuery()

            resultSQL = spCall.Parameters("@return").Value

            If resultSQL >= 0 Then
                daSql.Fill(resultDT)
                Return resultDT
            End If

            Return Nothing

        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
            dbConn = Nothing
            spCall = Nothing
            daSql = Nothing
            resultDT = Nothing
        End Try

    End Function
 

    Private Sub generaTabla(ByVal dtDatos As DataTable, ByVal pResultado As Panel)
        Dim tDatos As Table = New Table
        tDatos.CssClass = "pot-resultados"
        tDatos.CellSpacing = 1
        tDatos.CellPadding = 3

        With tDatos

            .Rows.Add(New TableRow)

            .Rows(.Rows.Count - 1).CssClass = "pot-cabecera"

            .Rows(.Rows.Count - 1).Cells.Add(New TableCell)  'Codigo Cliente
            .Rows(.Rows.Count - 1).Cells.Add(New TableCell)  'Razon social
            .Rows(.Rows.Count - 1).Cells.Add(New TableCell)  'Dmc cla antalis
            .Rows(.Rows.Count - 1).Cells.Add(New TableCell)  'Concepto
            .Rows(.Rows.Count - 1).Cells.Add(New TableCell)  'General
            .Rows(.Rows.Count - 1).Cells.Add(New TableCell)  'PAP
            .Rows(.Rows.Count - 1).Cells.Add(New TableCell)  'CVI
            .Rows(.Rows.Count - 1).Cells.Add(New TableCell)  'PAK
            .Rows(.Rows.Count - 1).Cells.Add(New TableCell)  'EII
            .Rows(.Rows.Count - 1).Cells.Add(New TableCell)  '---
            .Rows(.Rows.Count - 1).Cells.Add(New TableCell)  'imagen

            .Rows(.Rows.Count - 1).Cells(0).Text = "Cod.Cli"
            .Rows(.Rows.Count - 1).Cells(1).Text = "Razón Social"
            .Rows(.Rows.Count - 1).Cells(2).Text = "Dmc clasificación Antalis"
            .Rows(.Rows.Count - 1).Cells(3).Text = "Concepto"
            .Rows(.Rows.Count - 1).Cells(4).Text = "General"
            .Rows(.Rows.Count - 1).Cells(5).Text = "PAP"
            .Rows(.Rows.Count - 1).Cells(6).Text = "CVI"
            .Rows(.Rows.Count - 1).Cells(7).Text = "PAK"
            .Rows(.Rows.Count - 1).Cells(8).Text = "EII"
            .Rows(.Rows.Count - 1).Cells(9).Text = "---"
            .Rows(.Rows.Count - 1).Cells(10).Text = ""
            .Rows(.Rows.Count - 1).Cells(10).Width = New Unit(20, UnitType.Pixel)
        End With

        Dim tbPotencial As TextBox
        Dim pImagen As Panel
        Dim idPanel As String

        Dim codigoCliente As String
        Dim codigoArea As String
        Dim seccion As String

        Dim itemAlternado As Boolean = False
        Dim idObj As String

        For Each dr As DataRow In dtDatos.Rows

            codigoCliente = Trim(dr.Item("cod_cliente"))
            seccion = "potcli"

            With tDatos
                .Rows.Add(New TableRow)
                .Rows(.Rows.Count - 1).ID = seccion & "_" & codigoCliente

                .Rows(.Rows.Count - 1).Attributes.Add("onmouseover", String.Format("$('#{0}').addClass('pot-seleccion-fila');$('#{1}').addClass('pot-seleccion-fila');", .Rows(.Rows.Count - 1).ID, .Rows(.Rows.Count - 1).ID & "_2"))
                If itemAlternado Then
                    .Rows(.Rows.Count - 1).CssClass = "pot-item-alternado"
                    .Rows(.Rows.Count - 1).Attributes.Add("onmouseout", String.Format("$('#{0}').removeClass('pot-seleccion-fila'); " & _
                                                                        "$('#{1}').addClass('pot-item-alternado'); " & _
                                                                        "$('#{2}').removeClass('pot-seleccion-fila'); " & _
                                                                        "$('#{3}').addClass('pot-item-alternado');", _
                                                                        .Rows(.Rows.Count - 1).ID, _
                                                                        .Rows(.Rows.Count - 1).ID, _
                                                                        .Rows(.Rows.Count - 1).ID & "_2", _
                                                                        .Rows(.Rows.Count - 1).ID & "_2"))
                Else
                    .Rows(.Rows.Count - 1).CssClass = "pot-item-normal"
                    .Rows(.Rows.Count - 1).Attributes.Add("onmouseout", String.Format("$('#{0}').removeClass('pot-seleccion-fila'); " & _
                                                                        "$('#{1}').addClass('pot-item-normal'); " & _
                                                                        "$('#{2}').removeClass('pot-seleccion-fila'); " & _
                                                                        "$('#{3}').addClass('pot-item-normal');", _
                                                                        .Rows(.Rows.Count - 1).ID, _
                                                                        .Rows(.Rows.Count - 1).ID, _
                                                                        .Rows(.Rows.Count - 1).ID & "_2", _
                                                                        .Rows(.Rows.Count - 1).ID & "_2"))
                End If

                .Rows(.Rows.Count - 1).Cells.Add(New TableCell)
                .Rows(.Rows.Count - 1).Cells.Add(New TableCell)
                .Rows(.Rows.Count - 1).Cells.Add(New TableCell)
                .Rows(.Rows.Count - 1).Cells.Add(New TableCell)
                .Rows(.Rows.Count - 1).Cells.Add(New TableCell)
                .Rows(.Rows.Count - 1).Cells.Add(New TableCell)
                .Rows(.Rows.Count - 1).Cells.Add(New TableCell)
                .Rows(.Rows.Count - 1).Cells.Add(New TableCell)
                .Rows(.Rows.Count - 1).Cells.Add(New TableCell)
                .Rows(.Rows.Count - 1).Cells.Add(New TableCell)
                .Rows(.Rows.Count - 1).Cells.Add(New TableCell)

                If Not dr("cod_cliente") Is DBNull.Value Then
                    .Rows(.Rows.Count - 1).Cells(0).Text = Trim(dr("cod_cliente"))
                End If
                .Rows(.Rows.Count - 1).Cells(0).RowSpan = 2

                If Not dr("nom_cliente") Is DBNull.Value Then
                    .Rows(.Rows.Count - 1).Cells(1).Text = Trim(dr("nom_cliente"))
                End If
                .Rows(.Rows.Count - 1).Cells(1).RowSpan = 2

                If Not dr("dmc_cla_antalis") Is DBNull.Value Then
                    .Rows(.Rows.Count - 1).Cells(2).Text = Trim(dr("dmc_cla_antalis"))
                End If
                .Rows(.Rows.Count - 1).Cells(2).RowSpan = 2

                .Rows(.Rows.Count - 1).Cells(3).Text = "Potencial :"



                'General
                codigoArea = "GNR"
                tbPotencial = New TextBox
                tbPotencial.ID = seccion & "_" & codigoCliente & "_" & codigoArea
                tbPotencial.CssClass = "pot-cifra"
                If (Not dr("potcli_gnr") Is DBNull.Value) Then
                    tbPotencial.Text = CType(dr("potcli_gnr"), Double).ToString("#,##0.00")
                End If
                tbPotencial.Attributes.Add("onchange", "actualizaIndicadoresVentaCliente('GMSC', '" & seccion & "', '" & codigoCliente & "', '" & codigoArea & "', this);")
                .Rows(.Rows.Count - 1).Cells(4).Controls.Add(tbPotencial)

                'PAP
                codigoArea = "PAP"
                tbPotencial = New TextBox
                tbPotencial.ID = seccion & "_" & codigoCliente & "_" & codigoArea
                tbPotencial.CssClass = "pot-cifra"
                If (Not dr("potcli_pap") Is DBNull.Value) Then
                    tbPotencial.Text = CType(dr("potcli_pap"), Double).ToString("#,##0.00")
                End If
                tbPotencial.Attributes.Add("onchange", "actualizaIndicadoresVentaCliente('GMSC', '" & seccion & "', '" & codigoCliente & "', '" & codigoArea & "', this);")
                .Rows(.Rows.Count - 1).Cells(5).Controls.Add(tbPotencial)

                'CVI
                codigoArea = "CVI"
                tbPotencial = New TextBox
                tbPotencial.ID = seccion & "_" & codigoCliente & "_" & codigoArea
                tbPotencial.CssClass = "pot-cifra"
                If (Not dr("potcli_cvi") Is DBNull.Value) Then
                    tbPotencial.Text = CType(dr("potcli_cvi"), Double).ToString("#,##0.00")
                End If
                tbPotencial.Attributes.Add("onchange", "actualizaIndicadoresVentaCliente('GMSC', '" & seccion & "', '" & codigoCliente & "', '" & codigoArea & "', this);")
                .Rows(.Rows.Count - 1).Cells(6).Controls.Add(tbPotencial)

                'PAK
                codigoArea = "PAK"
                tbPotencial = New TextBox
                tbPotencial.ID = seccion & "_" & codigoCliente & "_" & codigoArea
                tbPotencial.CssClass = "pot-cifra"
                If (Not dr("potcli_pak") Is DBNull.Value) Then
                    tbPotencial.Text = CType(dr("potcli_pak"), Double).ToString("#,##0.00")
                End If
                tbPotencial.Attributes.Add("onchange", "actualizaIndicadoresVentaCliente('GMSC', '" & seccion & "', '" & codigoCliente & "', '" & codigoArea & "', this);")
                .Rows(.Rows.Count - 1).Cells(7).Controls.Add(tbPotencial)

                'EII
                codigoArea = "EII"
                tbPotencial = New TextBox
                tbPotencial.ID = seccion & "_" & codigoCliente & "_" & codigoArea
                tbPotencial.CssClass = "pot-cifra"
                If (Not dr("potcli_eii") Is DBNull.Value) Then
                    tbPotencial.Text = CType(dr("potcli_eii"), Double).ToString("#,##0.00")
                End If
                tbPotencial.Attributes.Add("onchange", "actualizaIndicadoresVentaCliente('GMSC', '" & seccion & "', '" & codigoCliente & "', '" & codigoArea & "', this);")
                .Rows(.Rows.Count - 1).Cells(8).Controls.Add(tbPotencial)

                'EII2
                codigoArea = "ST"
                tbPotencial = New TextBox
                tbPotencial.ID = seccion & "_" & codigoCliente & "_" & codigoArea
                tbPotencial.CssClass = "pot-cifra"
                If (Not dr("potcli_st") Is DBNull.Value) Then
                    tbPotencial.Text = CType(dr("potcli_st"), Double).ToString("#,##0.00")
                End If
                tbPotencial.Attributes.Add("onchange", "actualizaIndicadoresVentaCliente('GMSC', '" & seccion & "', '" & codigoCliente & "', '" & codigoArea & "', this);")
                .Rows(.Rows.Count - 1).Cells(9).Controls.Add(tbPotencial)

                pImagen = New Panel
                pImagen.ID = String.Format("img_{0}_{1}", seccion, codigoCliente)
                .Rows(.Rows.Count - 1).Cells(10).Controls.Add(pImagen)


                'Capacidad instalada
                .Rows.Add(New TableRow)
                .Rows(.Rows.Count - 1).ID = seccion & "_" & codigoCliente & "_2"

                .Rows(.Rows.Count - 1).Attributes.Add("onmouseover", String.Format("$('#{0}').addClass('pot-seleccion-fila');$('#{1}').addClass('pot-seleccion-fila');", .Rows(.Rows.Count - 1).ID, .Rows(.Rows.Count - 1).ID.Replace("_2", "")))
                If itemAlternado Then
                    .Rows(.Rows.Count - 1).CssClass = "pot-item-alternado"
                    .Rows(.Rows.Count - 1).Attributes.Add("onmouseout", String.Format("$('#{0}').removeClass('pot-seleccion-fila'); " & _
                                                                        "$('#{1}').addClass('pot-item-alternado'); " & _
                                                                        "$('#{2}').removeClass('pot-seleccion-fila'); " & _
                                                                        "$('#{3}').addClass('pot-item-alternado');", _
                                                                        .Rows(.Rows.Count - 1).ID, _
                                                                        .Rows(.Rows.Count - 1).ID, _
                                                                        .Rows(.Rows.Count - 1).ID.Replace("_2", ""), _
                                                                        .Rows(.Rows.Count - 1).ID.Replace("_2", "")))
                Else
                    .Rows(.Rows.Count - 1).CssClass = "pot-item-normal"
                    .Rows(.Rows.Count - 1).Attributes.Add("onmouseout", String.Format("$('#{0}').removeClass('pot-seleccion-fila'); " & _
                                                                        "$('#{1}').addClass('pot-item-normal'); " & _
                                                                        "$('#{2}').removeClass('pot-seleccion-fila'); " & _
                                                                        "$('#{3}').addClass('pot-item-normal');", _
                                                                        .Rows(.Rows.Count - 1).ID, _
                                                                        .Rows(.Rows.Count - 1).ID, _
                                                                        .Rows(.Rows.Count - 1).ID.Replace("_2", ""), _
                                                                        .Rows(.Rows.Count - 1).ID.Replace("_2", "")))
                End If

                .Rows(.Rows.Count - 1).Cells.Add(New TableCell)
                .Rows(.Rows.Count - 1).Cells.Add(New TableCell)
                .Rows(.Rows.Count - 1).Cells.Add(New TableCell)
                .Rows(.Rows.Count - 1).Cells.Add(New TableCell)
                .Rows(.Rows.Count - 1).Cells.Add(New TableCell)
                .Rows(.Rows.Count - 1).Cells.Add(New TableCell)
                .Rows(.Rows.Count - 1).Cells.Add(New TableCell)
                .Rows(.Rows.Count - 1).Cells.Add(New TableCell)

                .Rows(.Rows.Count - 1).Cells(0).Text = "Cap. Instalada:"
                seccion = "capinst"

                'General
                codigoArea = "GNR"
                tbPotencial = New TextBox
                tbPotencial.ID = seccion & "_" & codigoCliente & "_" & codigoArea
                tbPotencial.CssClass = "pot-cifra"
                If (Not dr("capinst_gnr") Is DBNull.Value) Then
                    tbPotencial.Text = CType(dr("capinst_gnr"), Double).ToString("#,##0.00")
                End If
                tbPotencial.Attributes.Add("onchange", "actualizaIndicadoresVentaCliente('GMSC', '" & seccion & "', '" & codigoCliente & "', '" & codigoArea & "', this);")
                .Rows(.Rows.Count - 1).Cells(1).Controls.Add(tbPotencial)

                'PAP
                codigoArea = "PAP"
                tbPotencial = New TextBox
                tbPotencial.ID = seccion & "_" & codigoCliente & "_" & codigoArea
                tbPotencial.CssClass = "pot-cifra"
                If (Not dr("capinst_pap") Is DBNull.Value) Then
                    tbPotencial.Text = CType(dr("capinst_pap"), Double).ToString("#,##0.00")
                End If
                tbPotencial.Attributes.Add("onchange", "actualizaIndicadoresVentaCliente('GMSC', '" & seccion & "', '" & codigoCliente & "', '" & codigoArea & "', this);")
                .Rows(.Rows.Count - 1).Cells(2).Controls.Add(tbPotencial)

                'CVI
                codigoArea = "CVI"
                tbPotencial = New TextBox
                tbPotencial.ID = seccion & "_" & codigoCliente & "_" & codigoArea
                tbPotencial.CssClass = "pot-cifra"
                If (Not dr("capinst_cvi") Is DBNull.Value) Then
                    tbPotencial.Text = CType(dr("capinst_cvi"), Double).ToString("#,##0.00")
                End If
                tbPotencial.Attributes.Add("onchange", "actualizaIndicadoresVentaCliente('GMSC', '" & seccion & "', '" & codigoCliente & "', '" & codigoArea & "', this);")
                .Rows(.Rows.Count - 1).Cells(3).Controls.Add(tbPotencial)

                'PAK
                codigoArea = "PAK"
                tbPotencial = New TextBox
                tbPotencial.ID = seccion & "_" & codigoCliente & "_" & codigoArea
                tbPotencial.CssClass = "pot-cifra"
                If (Not dr("capinst_pak") Is DBNull.Value) Then
                    tbPotencial.Text = CType(dr("capinst_pak"), Double).ToString("#,##0.00")
                End If
                tbPotencial.Attributes.Add("onchange", "actualizaIndicadoresVentaCliente('GMSC', '" & seccion & "', '" & codigoCliente & "', '" & codigoArea & "', this);")
                .Rows(.Rows.Count - 1).Cells(4).Controls.Add(tbPotencial)

                'EII
                codigoArea = "EII"
                tbPotencial = New TextBox
                tbPotencial.ID = seccion & "_" & codigoCliente & "_" & codigoArea
                tbPotencial.CssClass = "pot-cifra"
                If (Not dr("capinst_eii") Is DBNull.Value) Then
                    tbPotencial.Text = CType(dr("capinst_eii"), Double).ToString("#,##0.00")
                End If
                tbPotencial.Attributes.Add("onchange", "actualizaIndicadoresVentaCliente('GMSC', '" & seccion & "', '" & codigoCliente & "', '" & codigoArea & "', this);")
                .Rows(.Rows.Count - 1).Cells(5).Controls.Add(tbPotencial)

                'EII2
                codigoArea = "ST"
                tbPotencial = New TextBox
                tbPotencial.ID = seccion & "_" & codigoCliente & "_" & codigoArea
                tbPotencial.CssClass = "pot-cifra"
                If (Not dr("capinst_st") Is DBNull.Value) Then
                    tbPotencial.Text = CType(dr("capinst_st"), Double).ToString("#,##0.00")
                End If
                tbPotencial.Attributes.Add("onchange", "actualizaIndicadoresVentaCliente('GMSC', '" & seccion & "', '" & codigoCliente & "', '" & codigoArea & "', this);")
                .Rows(.Rows.Count - 1).Cells(6).Controls.Add(tbPotencial)

                pImagen = New Panel
                pImagen.ID = String.Format("img_{0}_{1}", seccion, codigoCliente)
                .Rows(.Rows.Count - 1).Cells(7).Controls.Add(pImagen)

            End With

            itemAlternado = Not itemAlternado


        Next

        pResultado.Controls.Clear()
        pResultado.Controls.Add(tDatos)

    End Sub
End Class
