Imports System.Data.SqlClient
Imports System.Data

Public Class potencial_default
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
    Protected WithEvents dgResultadoTab1 As System.Web.UI.WebControls.DataGrid
    Protected WithEvents Literal1 As System.Web.UI.WebControls.Literal
    Protected WithEvents dgResultadoTab3 As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dgResultadoTab4 As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dgResultadoTab5 As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dgResultadoTab6 As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dgResultadoTab2 As System.Web.UI.WebControls.DataGrid

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


            If Not (Session(Constantes.CTE_OBJ_USER_INFO_PERFIL) = "EJEC" Or Session(Constantes.CTE_ANDES_GRUPO_AGENTE) = "CELULA") Then
                Literal1.Text &= "$('#op1').hide();"
            End If

            If Not (Session(Constantes.CTE_OBJ_USER_INFO_PERFIL) = "EJEC" Or Session(Constantes.CTE_ANDES_GRUPO_AGENTE) = "VENTAS") Then
                Literal1.Text &= "$('#op2').hide();"
            End If


            Dim ws As cl.gms.andes.ws.OrgSrv = New cl.gms.andes.ws.OrgSrv
            With ddlEjecutivaComercial
                .DataSource = ws.listaPromotoras("GMSC").Tables(0)
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
                .DataSource = ws.listaVendedorasVirtuales("GMSC").Tables(0)
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
            daSql.Fill(resultDT)

            resultSQL = spCall.Parameters("@return").Value

            If resultSQL >= 0 Then
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
            daSql.Fill(resultDT)

            resultSQL = spCall.Parameters("@return").Value

            If resultSQL >= 0 Then
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
            spCall.Parameters.Add("@cod_sociedad", "GMSC").Direction = ParameterDirection.Input

            daSql.SelectCommand = spCall
            daSql.Fill(resultDT)

            resultSQL = spCall.Parameters("@return").Value

            If resultSQL >= 0 Then
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
        With dgResultadoTab1
            contador = 0
            .DataSource = obtieneClientesPorEjecComercial("GMSC", ddlEjecutivaComercial.SelectedValue)
            .DataBind()
        End With

        Literal1.Text &= "$(""#op1"").addClass(""active"").show();" & vbCrLf & _
                        "$(""#tab1"").show();"
    End Sub

    Private Sub bBuscarPorVendVirtual_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bBuscarPorVendVirtual.Click

        With dgResultadoTab2
            .DataSource = obtieneClientesPorVendVirtual("GMSC", ddlVendedoraVirtual.SelectedValue)
            .DataBind()
        End With

        Literal1.Text = "$(""#op2"").addClass(""active"").show();" & vbCrLf & _
                        "$(""#tab2"").show();"
    End Sub

    Private Sub bBuscarPorCobrador_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bBuscarPorCobrador.Click

        With dgResultadoTab3
            .DataSource = obtieneClientesPorCobrador("GMSC", ddlCobrador.SelectedValue)
            .DataBind()
        End With

        Literal1.Text = "$(""#op3"").addClass(""active"").show();" & vbCrLf & _
                "$(""#tab3"").show();"
    End Sub

    Private Sub bBuscarPorCelula_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bBuscarPorCelula.Click

        With dgResultadoTab4
            .DataSource = obtieneClientesPorCelula("GMSC", ddlCelula.SelectedValue)
            .DataBind()
        End With

        Literal1.Text = "$(""#op4"").addClass(""active"").show();" & vbCrLf & _
        "$(""#tab4"").show();"
    End Sub

    Private Sub bBuscarPorClasificacionAntalis_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bBuscarPorClasificacionAntalis.Click

        With dgResultadoTab5
            .DataSource = obtieneClientesPorClasificacionAntalis("GMSC", ddlClasificacionAntalis.SelectedValue)
            .DataBind()
        End With

        Literal1.Text = "$(""#op5"").addClass(""active"").show();" & vbCrLf & _
        "$(""#tab5"").show();"
    End Sub

    Private Sub bBuscarPorPatron_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bBuscarPorPatron.Click
        Literal1.Text = "$(""#op6"").addClass(""active"").show();" & vbCrLf & _
        "$(""#tab6"").show();"

        Dim arrStr As String() = tbPatronCliente.Text.Split("::")

        Dim dt As DataTable

        dt = obtieneClientesPorPatron("GMSC", arrStr(0))

        If dt.Rows.Count = 1 Then
            'Vamos directo al cliente
            Response.Redirect("fichaCliente.aspx?cc=" & Trim(dt.Rows(0).Item("cod_cliente")))
            Return
        End If

        With dgResultadoTab6
            .DataSource = dt
            .DataBind()
        End With
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
            daSql.Fill(resultDT)

            resultSQL = spCall.Parameters("@return").Value

            If resultSQL >= 0 Then
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
            daSql.Fill(resultDT)

            resultSQL = spCall.Parameters("@return").Value

            If resultSQL >= 0 Then
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
            daSql.Fill(resultDT)

            resultSQL = spCall.Parameters("@return").Value

            If resultSQL >= 0 Then
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
            daSql.Fill(resultDT)

            resultSQL = spCall.Parameters("@return").Value

            If resultSQL >= 0 Then
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
            daSql.Fill(resultDT)

            resultSQL = spCall.Parameters("@return").Value

            If resultSQL >= 0 Then
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
            daSql.Fill(resultDT)

            resultSQL = spCall.Parameters("@return").Value

            If resultSQL >= 0 Then
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

    Private Sub dgResultadoTab1_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgResultadoTab1.DeleteCommand

    End Sub

    Private Sub dgResultadoTab1_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgResultadoTab1.EditCommand
        Response.Redirect("fichaCliente.aspx?cc=" & e.CommandArgument)
    End Sub


    Private Sub dgResultadoTab1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgResultadoTab1.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim lb As LinkButton = CType(e.Item.Cells(7).Controls(0), LinkButton)
            lb.CommandArgument = Trim(e.Item.DataItem.item("cod_cliente"))

            e.Item.Attributes.Add("onmouseover", "this.className='pot-seleccion-fila';")

            If contador Mod 2 = 0 Then
                e.Item.Attributes.Add("onmouseout", "this.className='" & dgResultadoTab1.ItemStyle.CssClass & "';")
            Else
                e.Item.Attributes.Add("onmouseout", "this.className='" & dgResultadoTab1.AlternatingItemStyle.CssClass & "';")
            End If
            contador += 1
        End If
    End Sub

    Private Sub dgResultadoTab2_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgResultadoTab2.EditCommand
        Response.Redirect("fichaCliente.aspx?cc=" & e.CommandArgument)
    End Sub

    Private Sub dgResultadoTab2_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgResultadoTab2.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim lb As LinkButton = CType(e.Item.Cells(7).Controls(0), LinkButton)
            lb.CommandArgument = Trim(e.Item.DataItem.item("cod_cliente"))
        End If
    End Sub

    Private Sub dgResultadoTab3_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgResultadoTab3.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim lb As LinkButton = CType(e.Item.Cells(7).Controls(0), LinkButton)
            lb.CommandArgument = Trim(e.Item.DataItem.item("cod_cliente"))
        End If
    End Sub

    Private Sub dgResultadoTab3_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgResultadoTab3.EditCommand
        Response.Redirect("fichaCliente.aspx?cc=" & e.CommandArgument)
    End Sub

    Private Sub dgResultadoTab4_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgResultadoTab4.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim lb As LinkButton = CType(e.Item.Cells(7).Controls(0), LinkButton)
            lb.CommandArgument = Trim(e.Item.DataItem.item("cod_cliente"))
        End If
    End Sub

    Private Sub dgResultadoTab4_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgResultadoTab4.EditCommand
        Response.Redirect("fichaCliente.aspx?cc=" & e.CommandArgument)
    End Sub

    Private Sub dgResultadoTab5_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgResultadoTab5.EditCommand
        Response.Redirect("fichaCliente.aspx?cc=" & e.CommandArgument)
    End Sub

    Private Sub dgResultadoTab6_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgResultadoTab6.EditCommand
        Response.Redirect("fichaCliente.aspx?cc=" & e.CommandArgument)
    End Sub

    Private Sub dgResultadoTab5_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgResultadoTab5.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim lb As LinkButton = CType(e.Item.Cells(6).Controls(0), LinkButton)
            lb.CommandArgument = Trim(e.Item.DataItem.item("cod_cliente"))
        End If
    End Sub

    Private Sub dgResultadoTab6_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgResultadoTab6.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim lb As LinkButton = CType(e.Item.Cells(7).Controls(0), LinkButton)
            lb.CommandArgument = Trim(e.Item.DataItem.item("cod_cliente"))
        End If
    End Sub
End Class
