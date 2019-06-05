Imports System.Data.SqlClient
Imports System.Data

Public Class catastromaquinas_default
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents MPContainer As Wilson.MasterPages.MasterPage
    Protected WithEvents MPTitle As Wilson.MasterPages.ContentRegion
    Protected WithEvents MPCaption As Wilson.MasterPages.ContentRegion
    Protected WithEvents ddlEjecutivaComercial As System.Web.UI.WebControls.DropDownList
    Protected WithEvents bBuscarPorEjecComercial As System.Web.UI.WebControls.Button
    Protected WithEvents Literal1 As System.Web.UI.WebControls.Literal
    Protected WithEvents Panel1 As System.Web.UI.WebControls.Panel
    Protected WithEvents ddlVendedoraVirtual As System.Web.UI.WebControls.DropDownList
    Protected WithEvents bBuscarPorVendVirtual As System.Web.UI.WebControls.Button
    Protected WithEvents ddlCobrador As System.Web.UI.WebControls.DropDownList
    Protected WithEvents bBuscarPorCobrador As System.Web.UI.WebControls.Button
    Protected WithEvents ddlCelula As System.Web.UI.WebControls.DropDownList
    Protected WithEvents bBuscarPorCelula As System.Web.UI.WebControls.Button
    Protected WithEvents ddlClasificacionAntalis As System.Web.UI.WebControls.DropDownList
    Protected WithEvents bBuscarPorClasificacionAntalis As System.Web.UI.WebControls.Button
    Protected WithEvents tbPatronCliente As System.Web.UI.WebControls.TextBox
    Protected WithEvents bBuscarPorPatron As System.Web.UI.WebControls.Button
    Protected WithEvents lMensajeAccionBusqueda As System.Web.UI.WebControls.Label
    Protected WithEvents Panel2 As System.Web.UI.WebControls.Panel
    Protected WithEvents Panel3 As System.Web.UI.WebControls.Panel
    Protected WithEvents Panel4 As System.Web.UI.WebControls.Panel
    Protected WithEvents Panel5 As System.Web.UI.WebControls.Panel
    Protected WithEvents Panel6 As System.Web.UI.WebControls.Panel
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Panel7 As System.Web.UI.WebControls.Panel
    Protected WithEvents ddlEquipo As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlTipo As System.Web.UI.WebControls.DropDownList
    Protected WithEvents bBuscarPorTipoEquipo As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Const REGISTROS_POR_PAGINA As Integer = 50
    Enum eTipoMensaje
        exito
        fallo
        ninguno
    End Enum

    Enum eTipoConsulta
        porCarteraEjecutivo
        porCarteraVendedoraVirtual
        porCobrador
        porCelula
        porClasificacionAntalis
        porCliente
        porTipoArtefacto
    End Enum


    Dim contador As Integer
    Dim ultimoCodigoCliente As String
    Dim codigoSociedad As String = "GMSC"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Session(Constantes.CTE_ANDES_USUARIO_AUTENTICADO) <> "SI" Then
            Response.Redirect("/logout.aspx")
            Return
        End If

        ''Chequeamos que el usuario ejecutando la acción tenga acceso a este módulo
        'Dim pu As CPermisoUsuario = Session(Constantes.CTE_OBJ_PERMISOS_USUARIO)
        'If Not pu.validaPermiso(Constantes.MODULO_CATASTRO_MAQUINAS, CPermisoUsuario.eTipoAccion.lectura) Then
        '    Response.Redirect("/")
        '    Return
        'End If

        If Page.IsPostBack = False Then
            Panel1.Visible = False

            Literal1.Text = "$(""ul.tabs li:first"").addClass(""active"").show();" & vbCrLf & _
                            "$("".tab_content:first"").show();"

            Dim ws As cl.gms.andes.ws.OrgSrv = New cl.gms.andes.ws.OrgSrv
            With ddlEjecutivaComercial
                .DataSource = ws.listaPromotoras("GMSC").Tables(0)
                .DataTextField = "nom_promotora_2"
                .DataValueField = "cod_promotora"
                .DataBind()
            End With

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

            With ddlTipo
                .DataSource = CTipoArtefacto.obtieneOcurrencias()
                .DataTextField = "dmc_artefacto"
                .DataValueField = "tipo_artefacto"
                .DataBind()
                .Items.Add(New ListItem("* Todos los tipos *", "*"))
                .Items.FindByValue("*").Selected = True
            End With

            With ddlEquipo
                .Items.Add(New ListItem("* Todos los equipos *", "-1"))
                .Items.FindByValue("-1").Selected = True
            End With

            If Request("id") <> "" Then
                If Request("t") = "porCarteraEjecutivo" Then

                    Literal1.Text = "$(""#op1"").addClass(""active"").show();" & vbCrLf & _
                                    "$(""#tab1"").show();"

                    If Not IsNothing(ddlEjecutivaComercial.Items.FindByValue(Request("k"))) Then
                        ddlEjecutivaComercial.ClearSelection()
                        ddlEjecutivaComercial.Items.FindByValue(Request("k")).Selected = True

                        Dim codigoEjecutivo As String = Request("k")
                        Dim paginaActiva As Integer = Request("p")
                        Dim idObj As String = Request("id")
                        Dim dt As DataTable = CType(Session(Request("id")), DataTable)

                        Dim tituloEncabezado As String = ddlEjecutivaComercial.SelectedItem.Text

                        If Not IsNothing(dt) Then
                            Panel1.Controls.Add(generaTablaResultados(dt, tituloEncabezado, paginaActiva, idObj, eTipoConsulta.porCarteraEjecutivo, codigoEjecutivo))
                            Panel1.Visible = True
                        End If
                    End If

                ElseIf Request("t") = "porTipoArtefacto" Then

                    Literal1.Text = "$(""#op7"").addClass(""active"").show();" & vbCrLf & _
                                    "$(""#tab7"").show();"

                    Dim arrKey() As String = Split(Request("k"), ",")
                    If Not IsNothing(ddlTipo.Items.FindByValue(arrKey(0))) Then

                        ddlTipo.ClearSelection()
                        ddlTipo.Items.FindByValue(arrKey(0)).Selected = True

                        Dim idObj As String = Request("id")
                        Dim dt As DataTable = CType(Session(Request("id")), DataTable)
                        Dim paginaActiva As Integer = Request("p")

                        If Not IsNothing(ddlEquipo.Items.FindByValue(arrKey(1))) Then

                            ddlEquipo.ClearSelection()
                            ddlEquipo.Items.FindByValue(arrKey(1)).Selected = True

                            Dim tituloEncabezado As String = "Tipo / Equipo : " & ddlTipo.SelectedItem.Text & " - " & ddlEquipo.SelectedItem.Text

                            If Not IsNothing(dt) Then
                                Panel7.Controls.Add(generaTablaResultados(dt, tituloEncabezado, paginaActiva, idObj, eTipoConsulta.porTipoArtefacto, arrKey(0) & "," & arrKey(1)))
                                Panel7.Visible = True
                            End If


                        End If

                    End If

                ElseIf Request("t") = "porCarteraVendedoraVirtual" Then

                    Literal1.Text = "$(""#op2"").addClass(""active"").show();" & vbCrLf & _
                                    "$(""#tab2"").show();"

                    Dim codigoVendedoraVirtual As String = Request("k")
                    If Not IsNothing(ddlVendedoraVirtual.Items.FindByValue(codigoVendedoraVirtual)) Then

                        ddlVendedoraVirtual.ClearSelection()
                        ddlVendedoraVirtual.Items.FindByValue(codigoVendedoraVirtual).Selected = True

                        Dim idObj As String = Request("id")
                        Dim dt As DataTable = CType(Session(Request("id")), DataTable)
                        Dim paginaActiva As Integer = Request("p")

                        Dim tituloEncabezado As String = "Vendedor Virtual : " & ddlVendedoraVirtual.SelectedItem.Text

                        If Not IsNothing(dt) Then
                            Panel2.Controls.Add(generaTablaResultados(dt, tituloEncabezado, paginaActiva, idObj, eTipoConsulta.porCarteraVendedoraVirtual, codigoVendedoraVirtual))
                            Panel2.Visible = True
                        End If


                    End If
                ElseIf Request("t") = "porCobrador" Then

                    Literal1.Text = "$(""#op3"").addClass(""active"").show();" & vbCrLf & _
                                    "$(""#tab3"").show();"

                    Dim codigoCobrador As String = Request("k")

                    If Not IsNothing(ddlCobrador.Items.FindByValue(codigoCobrador)) Then

                        ddlCobrador.ClearSelection()
                        ddlCobrador.Items.FindByValue(codigoCobrador).Selected = True

                        Dim idObj As String = Request("id")
                        Dim dt As DataTable = CType(Session(Request("id")), DataTable)
                        Dim paginaActiva As Integer = Request("p")

                        Dim tituloEncabezado As String = "Cobrador : " & ddlCobrador.SelectedItem.Text

                        If Not IsNothing(dt) Then
                            Panel3.Controls.Add(generaTablaResultados(dt, tituloEncabezado, paginaActiva, idObj, eTipoConsulta.porCobrador, codigoCobrador))
                            Panel3.Visible = True
                        End If


                    End If
                ElseIf Request("t") = "porCelula" Then

                    Literal1.Text = "$(""#op4"").addClass(""active"").show();" & vbCrLf & _
                                    "$(""#tab4"").show();"

                    Dim codigoCelula As String = Request("k")

                    If Not IsNothing(ddlCelula.Items.FindByValue(codigoCelula)) Then

                        ddlCelula.ClearSelection()
                        ddlCelula.Items.FindByValue(codigoCelula).Selected = True

                        Dim idObj As String = Request("id")
                        Dim dt As DataTable = CType(Session(Request("id")), DataTable)
                        Dim paginaActiva As Integer = Request("p")

                        Dim tituloEncabezado As String = "Célula : " & ddlCelula.SelectedItem.Text

                        If Not IsNothing(dt) Then
                            Panel4.Controls.Add(generaTablaResultados(dt, tituloEncabezado, paginaActiva, idObj, eTipoConsulta.porCelula, codigoCelula))
                            Panel4.Visible = True
                        End If
                    End If

                ElseIf Request("t") = "porClasificacionAntalis" Then

                    Literal1.Text = "$(""#op5"").addClass(""active"").show();" & vbCrLf & _
                                    "$(""#tab5"").show();"

                    Dim codigoClasificacionAntalis As String = Request("k")

                    If Not IsNothing(ddlClasificacionAntalis.Items.FindByValue(codigoClasificacionAntalis)) Then

                        ddlClasificacionAntalis.ClearSelection()
                        ddlClasificacionAntalis.Items.FindByValue(codigoClasificacionAntalis).Selected = True

                        Dim idObj As String = Request("id")
                        Dim dt As DataTable = CType(Session(Request("id")), DataTable)
                        Dim paginaActiva As Integer = Request("p")

                        Dim tituloEncabezado As String = "Clasificación Antalis : " & ddlClasificacionAntalis.SelectedItem.Text

                        If Not IsNothing(dt) Then
                            Panel5.Controls.Add(generaTablaResultados(dt, tituloEncabezado, paginaActiva, idObj, eTipoConsulta.porClasificacionAntalis, codigoClasificacionAntalis))
                            Panel5.Visible = True
                        End If
                    End If

                ElseIf Request("t") = "porCliente" Then

                    Literal1.Text = "$('#op6').addClass('active').show();" & vbCrLf & _
                                    "$('#tab6').show();"

                    Dim patron As String = Request("k")
                    tbPatronCliente.Text = patron

                    Dim idObj As String = Request("id")
                    Dim dt As DataTable = CType(Session(Request("id")), DataTable)
                    Dim paginaActiva As Integer = Request("p")

                    Dim tituloEncabezado As String = "Búsqueda para: " & patron

                    If Not IsNothing(dt) Then
                        Panel6.Controls.Add(generaTablaResultados(dt, tituloEncabezado, paginaActiva, idObj, eTipoConsulta.porCliente, patron))
                        Panel6.Visible = True
                    End If

                End If
            End If

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
        Const spName = "cma_sel_celulas"
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

    Private Sub inicializaPaneles()
        Panel1.Controls.Clear()
        Panel1.Visible = False
        Panel2.Controls.Clear()
        Panel2.Visible = False
        Panel3.Controls.Clear()
        Panel3.Visible = False
        Panel4.Controls.Clear()
        Panel4.Visible = False
        Panel5.Controls.Clear()
        Panel5.Visible = False
        Panel6.Controls.Clear()
        Panel6.Visible = False

    End Sub

    Private Sub bBuscarPorEjecComercial_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bBuscarPorEjecComercial.Click

        inicializaPaneles()

        Dim tituloTabla As String = "Ejecutivo Comercial: " & ddlEjecutivaComercial.SelectedItem.Text.Trim
        Dim codigoEjecutivo As String = ddlEjecutivaComercial.SelectedValue

        'Vamos a buscar datos y los guardamos en variable de sesión
        Dim dt As DataTable = obtieneClientesPorEjecutivoComercial(codigoSociedad, codigoEjecutivo)

        Dim idObj As String = Guid.NewGuid.ToString
        Session(idObj) = dt

        Dim paginaActiva As Integer
        If IsNumeric(Request("p")) Then
            paginaActiva = CType(Request("p"), Integer)
        Else
            paginaActiva = 1
        End If


        Panel1.Controls.Add(generaTablaResultados(dt, _
                                                tituloTabla, _
                                                paginaActiva, _
                                                idObj, _
                                                eTipoConsulta.porCarteraEjecutivo, _
                                                codigoEjecutivo))
        Panel1.Visible = True

        Literal1.Text = "$(""#op1"").addClass(""active"").show();" & vbCrLf & _
                        "$(""#tab1"").show();"
    End Sub

    Private Function generaTablaResultados(ByVal dtDatos As DataTable, _
                                            ByVal tituloEncabezado As String, _
                                            ByVal paginaActiva As Integer, _
                                            ByVal idObjeto As String, _
                                            ByVal tipo As eTipoConsulta, _
                                            ByVal llave As String) As Table

        Dim usuarioSesion As t_Usuario = CType(Session(Constantes.CTE_OBJ_USER_INFO), t_Usuario)
        Dim lTexto As Label

        Dim tResultado As Table = New Table

        With tResultado
            .CellPadding = 0
            .CellSpacing = 1
            .BorderWidth = New Unit(0, UnitType.Pixel)
        End With

        tResultado.Rows.Add(New TableRow)
        tResultado.Rows(tResultado.Rows.Count - 1).Cells.Add(New TableCell)
        tResultado.Rows(tResultado.Rows.Count - 1).Cells(0).ColumnSpan = 3
        tResultado.Rows(tResultado.Rows.Count - 1).Cells(0).Text = tituloEncabezado
        tResultado.Rows(tResultado.Rows.Count - 1).Cells(0).CssClass = "cma-cabecera-titulo"

        'Agregamos panel con opciones
        tResultado.Rows.Add(New TableRow)
        tResultado.Rows(tResultado.Rows.Count - 1).Cells.Add(New TableCell)
        tResultado.Rows(tResultado.Rows.Count - 1).Cells(0).ColumnSpan = 3
        Dim pPanel As Panel = New Panel
        Dim hlOpciones As HyperLink = New HyperLink
        hlOpciones.Text = "Opciones de despliegue"
        hlOpciones.ID = "opcionesDespliegue"
        pPanel.Controls.Add(hlOpciones)

        tResultado.Rows(tResultado.Rows.Count - 1).Cells(0).Controls.Add(pPanel)
        tResultado.Rows(tResultado.Rows.Count - 1).Cells(0).CssClass = "cma-cabecera-opciones"

        pPanel = New Panel
        pPanel.ID = "panelOpcionesDespliegue"
        pPanel.CssClass = "cma-opciones-listado"
        Dim chkOpciones As CheckBox = New CheckBox
        chkOpciones.Text = "Mostrar información adicional para cada cliente."
        chkOpciones.ID = "chkOpcionesMuestraDetalleEquipos"
        pPanel.Controls.Add(chkOpciones)
        tResultado.Rows(tResultado.Rows.Count - 1).Cells(0).Controls.Add(pPanel)




        tResultado.Rows.Add(New TableRow)
        tResultado.Rows(tResultado.Rows.Count - 1).Cells.Add(New TableCell)
        tResultado.Rows(tResultado.Rows.Count - 1).Cells.Add(New TableCell)
        tResultado.Rows(tResultado.Rows.Count - 1).Cells.Add(New TableCell)

        tResultado.Rows(tResultado.Rows.Count - 1).CssClass = "cma-cabecera-resultado"
        tResultado.Rows(tResultado.Rows.Count - 1).Cells(0).Text = "Cod Cliente"
        tResultado.Rows(tResultado.Rows.Count - 1).Cells(0).CssClass = "cma-cabecera-c1"
        tResultado.Rows(tResultado.Rows.Count - 1).Cells(1).Text = "Razon Social"
        tResultado.Rows(tResultado.Rows.Count - 1).Cells(1).CssClass = "cma-cabecera-c2"
        tResultado.Rows(tResultado.Rows.Count - 1).Cells(2).Text = "Equipos"
        tResultado.Rows(tResultado.Rows.Count - 1).Cells(2).CssClass = "cma-cabecera-c3"



        Dim itemAlterno As Boolean = True
        Dim filaActual As Integer = 0

        Dim usrResponsable As String = Session(Constantes.CTE_ANDES_USERNAME)
        Dim totalClientes As Integer = 0

        Dim iInicio As Integer = (paginaActiva - 1) * REGISTROS_POR_PAGINA
        If iInicio > dtDatos.Rows.Count Then
            iInicio = dtDatos.Rows.Count - 1
        End If

        Dim iTermino As Integer = iInicio + REGISTROS_POR_PAGINA - 1
        If iTermino >= dtDatos.Rows.Count Then
            iTermino = dtDatos.Rows.Count - 1
        End If

        Dim dr As DataRow

        For iIndice As Integer = iInicio To iTermino

            dr = dtDatos.Rows(iIndice)

            tResultado.Rows.Add(New TableRow)

            If itemAlterno Then
                tResultado.Rows(tResultado.Rows.Count - 1).CssClass = "cma-fila-alterna"
                tResultado.Rows(tResultado.Rows.Count - 1).Attributes.Add("onmouseout", "this.className='cma-fila-alterna';")
                tResultado.Rows(tResultado.Rows.Count - 1).Attributes.Add("onmouseover", "this.className='cma-fila-seleccionada';")
                itemAlterno = Not itemAlterno
            Else
                tResultado.Rows(tResultado.Rows.Count - 1).CssClass = "cma-fila-normal"
                tResultado.Rows(tResultado.Rows.Count - 1).Attributes.Add("onmouseout", "this.className='cma-fila-normal';")
                tResultado.Rows(tResultado.Rows.Count - 1).Attributes.Add("onmouseover", "this.className='cma-fila-seleccionada';")
                itemAlterno = Not itemAlterno
            End If

            tResultado.Rows(tResultado.Rows.Count - 1).Cells.Add(New TableCell)
            tResultado.Rows(tResultado.Rows.Count - 1).Cells(0).CssClass = "cma-dato-c1"
            tResultado.Rows(tResultado.Rows.Count - 1).Cells.Add(New TableCell)
            tResultado.Rows(tResultado.Rows.Count - 1).Cells(1).CssClass = "cma-dato-c2"
            tResultado.Rows(tResultado.Rows.Count - 1).Cells.Add(New TableCell)
            tResultado.Rows(tResultado.Rows.Count - 1).Cells(2).CssClass = "cma-dato-c3"
            tResultado.Rows(tResultado.Rows.Count - 1).Cells(2).ID = "cont_equipos_" & Trim(dr("cod_cliente"))

            tResultado.Rows(tResultado.Rows.Count - 1).Cells(0).Text = Trim(dr("cod_cliente"))
            tResultado.Rows(tResultado.Rows.Count - 1).Cells(1).Text = Trim(dr("nom_cliente"))


            '-------------------------------------------
            'Buscamos los equipos asociados al cliente
            '-------------------------------------------
            Dim dtEquipos As DataTable = obtieneArtefactosValoresPorCliente(codigoSociedad, Trim(dr("cod_cliente")))

            If tipo = eTipoConsulta.porTipoArtefacto Then
                Dim arrStr() As String = Split(llave, ",")
                Dim tipoArtefactoConsulta As String = arrStr(0)
                Dim idArtefactoConsulta As Integer = arrStr(1)

                If tipoArtefactoConsulta <> "*" Or idArtefactoConsulta > 0 Then
                    'Filtramos y dejamos solamente aquellos equipos que respondan al criterio de 
                    'tipo y artefacto
                    Dim k As Integer = 0
                    While k < dtEquipos.Rows.Count
                        If (tipoArtefactoConsulta <> "*") And (Trim(dtEquipos.Rows(k).Item("tipo_artefacto")) <> tipoArtefactoConsulta) Then
                            dtEquipos.Rows.RemoveAt(k)
                        ElseIf (idArtefactoConsulta > 0) And (dtEquipos.Rows(k).Item("id_artefacto") <> idArtefactoConsulta) Then
                            dtEquipos.Rows.RemoveAt(k)
                        Else
                            k += 1
                        End If
                    End While
                End If
            End If

            If dtEquipos.Rows.Count > 0 Then
                'Generamos subtabla con detalle de equipos del cliente
                Dim tEquipos As Table = New Table
                tEquipos.BorderWidth = New Unit(0, UnitType.Pixel)
                tEquipos.CellSpacing = 1
                tEquipos.CellPadding = 0

                tEquipos.ID = "equipos_" & Trim(dr("cod_cliente"))
                tEquipos.Rows.Add(New TableRow)
                tEquipos.Rows(tEquipos.Rows.Count - 1).Cells.Add(New TableCell)
                tEquipos.Rows(tEquipos.Rows.Count - 1).Cells.Add(New TableCell)
                tEquipos.Rows(tEquipos.Rows.Count - 1).Cells.Add(New TableCell)
                tEquipos.Rows(tEquipos.Rows.Count - 1).Cells.Add(New TableCell)

                tEquipos.Rows(tEquipos.Rows.Count - 1).CssClass = "cma-cabecera-equipos"
                tEquipos.Rows(tEquipos.Rows.Count - 1).Cells(0).Text = "Nombre"
                tEquipos.Rows(tEquipos.Rows.Count - 1).Cells(0).CssClass = "cma-cabecera-equipos-c0"
                tEquipos.Rows(tEquipos.Rows.Count - 1).Cells(1).Text = "Tipo"
                tEquipos.Rows(tEquipos.Rows.Count - 1).Cells(1).CssClass = "cma-cabecera-equipos-c1"
                tEquipos.Rows(tEquipos.Rows.Count - 1).Cells(2).Text = ""
                tEquipos.Rows(tEquipos.Rows.Count - 1).Cells(2).CssClass = "cma-cabecera-equipos-c2"

                Dim ultimoIdRegistroCliente As Integer = 0

                For Each drEquipo As DataRow In dtEquipos.Rows

                    If ultimoIdRegistroCliente <> CType(drEquipo("id_registro_cliente"), Integer) Then

                        ultimoIdRegistroCliente = CType(drEquipo("id_registro_cliente"), Integer)

                        tEquipos.Rows.Add(New TableRow)

                        tEquipos.Rows(tEquipos.Rows.Count - 1).CssClass = "cma-dato-equipos"
                        tEquipos.Rows(tEquipos.Rows.Count - 1).ID = "equipo_" & drEquipo("id_registro_cliente")

                        tEquipos.Rows(tEquipos.Rows.Count - 1).Cells.Add(New TableCell)
                        tEquipos.Rows(tEquipos.Rows.Count - 1).Cells.Add(New TableCell)
                        tEquipos.Rows(tEquipos.Rows.Count - 1).Cells.Add(New TableCell)
                        tEquipos.Rows(tEquipos.Rows.Count - 1).Cells.Add(New TableCell)

                        'Agregamos descripción del artefacto / equipo
                        Dim hlNombreArtefacto As HyperLink = New HyperLink
                        hlNombreArtefacto.Text = Trim(drEquipo("dmc_artefacto"))
                        hlNombreArtefacto.NavigateUrl = "#"
                        hlNombreArtefacto.ID = "cma-artefacto-" & drEquipo("id_registro_cliente")
                        hlNombreArtefacto.CssClass = "cma-nombre-artefacto"
                        hlNombreArtefacto.Attributes.Add("onclick", "if ($('#DETALLE_" & Trim(drEquipo("id_registro_cliente")) & "').is(':visible')) " & _
                                                                    "{this.className='cma-nombre-artefacto';} else {this.className='cma-nombre-artefacto-sel';}" & _
                                                                    "$('#DETALLE_" & Trim(drEquipo("id_registro_cliente")) & "').fadeToggle(); " & _
                                                                    "return false;")

                        tEquipos.Rows(tEquipos.Rows.Count - 1).Cells(0).Controls.Add(hlNombreArtefacto)
                        tEquipos.Rows(tEquipos.Rows.Count - 1).Cells(0).CssClass = "cma-dato-equipos-c0"

                        'Agregamos tipo de artefacto / equipo
                        tEquipos.Rows(tEquipos.Rows.Count - 1).Cells(1).Text = Trim(drEquipo("tipo_artefacto"))
                        tEquipos.Rows(tEquipos.Rows.Count - 1).Cells(1).CssClass = "cma-dato-equipos-c1"

                        If usuarioSesion.perfil = "EJEC" Or usuarioSesion.codigoPromo = Trim(dr("cod_promotora")) Then
                            'Agregamos vínculo para editar registro
                            Dim hlAccion As HyperLink = New HyperLink
                            hlAccion.Text = "editar"
                            hlAccion.NavigateUrl = "#"
                            hlAccion.CssClass = "cma-edita-equipo"
                            hlAccion.Attributes.Add("onclick", String.Format("despliegaDialogo('{0}', '{1}', '{2}', '{3}', {4});return false;", _
                                                                                codigoSociedad, Trim(dr("cod_cliente")), Trim(dr("nom_cliente")).Replace("'", "\'"), "editar", drEquipo("id_registro_cliente")))

                            tEquipos.Rows(tEquipos.Rows.Count - 1).Cells(2).Controls.Add(hlAccion)
                            tEquipos.Rows(tEquipos.Rows.Count - 1).Cells(2).CssClass = "cma-dato-equipos-c2"

                            hlAccion = New HyperLink
                            hlAccion.Text = "eliminar"
                            hlAccion.NavigateUrl = "#"
                            hlAccion.CssClass = "cma-edita-equipo"
                            hlAccion.Attributes.Add("onclick", String.Format("eliminaRegistroCliente('{0}', {1}, '{2}');return false;", _
                                                            codigoSociedad, _
                                                            drEquipo("id_registro_cliente"), _
                                                            usrResponsable))

                            tEquipos.Rows(tEquipos.Rows.Count - 1).Cells(3).Controls.Add(hlAccion)
                            tEquipos.Rows(tEquipos.Rows.Count - 1).Cells(3).CssClass = "cma-dato-equipos-c3"
                        End If



                        'Agregamos valores propiedades si es que hay
                        Dim dvValoresRegistro As DataView = New DataView(dtEquipos)
                        dvValoresRegistro.RowFilter = "id_registro_cliente = " & drEquipo("id_registro_cliente") & " AND valor <> ''"

                        If dvValoresRegistro.Count > 0 Then
                            Dim pTmp As Panel = New Panel
                            pTmp.ID = "DETALLE_" & Trim(drEquipo("id_registro_cliente"))
                            pTmp.CssClass = "cma-detalle-maquina"

                            Dim tDetalleMaquina As Table = New Table
                            tDetalleMaquina.CellPadding = 3
                            tDetalleMaquina.CellSpacing = 0

                            Dim ultimaPropiedad As String = ""
                            For j As Integer = 0 To dvValoresRegistro.Count - 1
                                With tDetalleMaquina
                                    .Rows.Add(New TableRow)
                                    .Rows(.Rows.Count - 1).Cells.Add(New TableCell)
                                    .Rows(.Rows.Count - 1).Cells.Add(New TableCell)

                                    If ultimaPropiedad <> Trim(dvValoresRegistro(j).Item("dmc_propiedad")) Then
                                        .Rows(.Rows.Count - 1).Cells(0).Text = Trim(dvValoresRegistro(j).Item("dmc_propiedad")) & ":"
                                        .Rows(.Rows.Count - 1).Cells(0).Wrap = False
                                        ultimaPropiedad = Trim(dvValoresRegistro(j).Item("dmc_propiedad"))
                                    End If

                                    .Rows(.Rows.Count - 1).Cells(1).Text = Trim(dvValoresRegistro(j).Item("valor"))
                                    .Rows(.Rows.Count - 1).Cells(1).Wrap = False
                                End With
                            Next
                            pTmp.Controls.Add(tDetalleMaquina)
                            tEquipos.Rows(tEquipos.Rows.Count - 1).Cells(0).Controls.Add(pTmp)
                        End If
                        dvValoresRegistro = Nothing
                    End If

                Next
                tResultado.Rows(tResultado.Rows.Count - 1).Cells(2).Controls.Add(tEquipos)
            End If

            If usuarioSesion.perfil = "EJEC" Or usuarioSesion.codigoPromo = Trim(dr("cod_promotora")) Then

                Dim hlAgregarMaquina As HyperLink = New HyperLink
                hlAgregarMaquina.Text = "asociar un equipo al cliente"
                hlAgregarMaquina.CssClass = "cma-agrega-nuevo-equipo"
                hlAgregarMaquina.NavigateUrl = "#"
                hlAgregarMaquina.Attributes.Add("onclick", String.Format("despliegaDialogo('{0}', '{1}', '{2}', '{3}', {4});return false;", _
                codigoSociedad, Trim(dr("cod_cliente")), Trim(dr("nom_cliente")).Replace("'", "\'"), "crear", 0))

                tResultado.Rows(tResultado.Rows.Count - 1).Cells(2).Controls.Add(hlAgregarMaquina)
            End If
        Next

        tResultado.Rows.Add(New TableRow)
        tResultado.Rows(tResultado.Rows.Count - 1).Cells.Add(New TableCell)
        tResultado.Rows(tResultado.Rows.Count - 1).Cells(0).ColumnSpan = 3
        tResultado.Rows(tResultado.Rows.Count - 1).Cells(0).Text = String.Format("Total {0} clientes en esta vista.", iTermino - iInicio + 1)
        tResultado.Rows(tResultado.Rows.Count - 1).Cells(0).CssClass = "cma-pie-resultado"

        Dim pPaginador As Panel = New Panel
        'Determinamos las páginas a desplegar
        Dim numeroPaginas As Integer = dtDatos.Rows.Count / REGISTROS_POR_PAGINA

        If REGISTROS_POR_PAGINA < dtDatos.Rows.Count Then
            numeroPaginas = dtDatos.Rows.Count / REGISTROS_POR_PAGINA
            If numeroPaginas * REGISTROS_POR_PAGINA < dtDatos.Rows.Count Then
                numeroPaginas += 1
            End If
        Else
            numeroPaginas = 1
        End If

        lTexto = New Label
        lTexto.Text = "Páginas:&nbsp;"
        lTexto.CssClass = "cm-pagina-resultado"
        pPaginador.Controls.Add(lTexto)

        For i As Integer = 1 To numeroPaginas

            If i <> paginaActiva Then
                Dim hlPagina As HyperLink = New HyperLink
                hlPagina.Text = i.ToString
                hlPagina.CssClass = "cm-pagina-resultado"
                hlPagina.NavigateUrl = String.Format("default.aspx?id={0}&p={1}&t={2}&k={3}", idObjeto, i, tipo.ToString, llave)
                lTexto = New Label
                lTexto.Text = "&nbsp;"
                lTexto.CssClass = "cm-pagina-resultado"
                pPaginador.Controls.Add(hlPagina)
                pPaginador.Controls.Add(lTexto)
            Else
                lTexto = New Label
                lTexto.Text = String.Format("{0}&nbsp;", i)
                lTexto.CssClass = "cm-pagina-resultado"
                pPaginador.Controls.Add(lTexto)
            End If

        Next

        tResultado.Rows.Add(New TableRow)
        tResultado.Rows(tResultado.Rows.Count - 1).Cells.Add(New TableCell)
        tResultado.Rows(tResultado.Rows.Count - 1).Cells(0).ColumnSpan = 3
        tResultado.Rows(tResultado.Rows.Count - 1).Cells(0).Controls.Add(pPaginador)
        tResultado.Rows(tResultado.Rows.Count - 1).Cells(0).CssClass = "cma-pie-resultado"


        Return tResultado

    End Function

    Private Sub bBuscarPorVendVirtual_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bBuscarPorVendVirtual.Click
        inicializaPaneles()

        Dim tituloTabla As String = "Vendedor Virtual: " & ddlVendedoraVirtual.SelectedItem.Text
        Dim codigoEjecutivo As String = ddlVendedoraVirtual.SelectedValue

        'Vamos a buscar datos y los guardamos en variable de sesión
        Dim dt As DataTable = obtieneClientesPorVendedoraVirtual(codigoSociedad, codigoEjecutivo)

        Dim idObj As String = Guid.NewGuid.ToString
        Session(idObj) = dt

        Dim paginaActiva As Integer
        If IsNumeric(Request("p")) Then
            paginaActiva = CType(Request("p"), Integer)
        Else
            paginaActiva = 1
        End If


        Panel2.Controls.Add(generaTablaResultados(dt, _
                                                tituloTabla, _
                                                paginaActiva, _
                                                idObj, _
                                                eTipoConsulta.porCarteraVendedoraVirtual, _
                                                codigoEjecutivo))
        Panel2.Visible = True

        Literal1.Text = "$(""#op2"").addClass(""active"").show();" & vbCrLf & _
                        "$(""#tab2"").show();"
    End Sub

    Private Sub bBuscarPorCobrador_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bBuscarPorCobrador.Click
        inicializaPaneles()

        Dim tituloTabla As String = "Cobrador: " & ddlCobrador.SelectedItem.Text.Trim
        Dim codigoEjecutivo As String = ddlCobrador.SelectedValue

        'Vamos a buscar datos y los guardamos en variable de sesión
        Dim dt As DataTable = obtieneClientesPorCobrador(codigoSociedad, codigoEjecutivo)

        Dim idObj As String = Guid.NewGuid.ToString
        Session(idObj) = dt

        Dim paginaActiva As Integer
        If IsNumeric(Request("p")) Then
            paginaActiva = CType(Request("p"), Integer)
        Else
            paginaActiva = 1
        End If


        Panel3.Controls.Add(generaTablaResultados(dt, _
                                                tituloTabla, _
                                                paginaActiva, _
                                                idObj, _
                                                eTipoConsulta.porCobrador, _
                                                codigoEjecutivo))
        Panel3.Visible = True

        Literal1.Text = "$(""#op3"").addClass(""active"").show();" & vbCrLf & _
                        "$(""#tab3"").show();"
 
    End Sub

    Private Sub bBuscarPorCelula_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bBuscarPorCelula.Click
        inicializaPaneles()

        Dim tituloTabla As String = "Célula: " & ddlCelula.SelectedItem.Text.Trim
        Dim codigoCelula As String = ddlCelula.SelectedValue

        'Vamos a buscar datos y los guardamos en variable de sesión
        Dim dt As DataTable = obtieneClientesPorCelula(codigoSociedad, codigoCelula)

        Dim idObj As String = Guid.NewGuid.ToString
        Session(idObj) = dt

        Dim paginaActiva As Integer
        If IsNumeric(Request("p")) Then
            paginaActiva = CType(Request("p"), Integer)
        Else
            paginaActiva = 1
        End If


        Panel4.Controls.Add(generaTablaResultados(dt, _
                                                tituloTabla, _
                                                paginaActiva, _
                                                idObj, _
                                                eTipoConsulta.porCelula, _
                                                codigoCelula))
        Panel4.Visible = True

        Literal1.Text = "$(""#op4"").addClass(""active"").show();" & vbCrLf & _
                        "$(""#tab4"").show();"
    End Sub

    Private Sub bBuscarPorClasificacionAntalis_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bBuscarPorClasificacionAntalis.Click
        inicializaPaneles()

        Dim tituloTabla As String = "Clasificación Antalis: " & ddlClasificacionAntalis.SelectedItem.Text.Trim
        Dim codigoClasificacion As String = ddlClasificacionAntalis.SelectedValue

        'Vamos a buscar datos y los guardamos en variable de sesión
        Dim dt As DataTable = obtieneClientesPorClasificacionAntalis(codigoSociedad, codigoClasificacion)

        Dim idObj As String = Guid.NewGuid.ToString
        Session(idObj) = dt

        Dim paginaActiva As Integer
        If IsNumeric(Request("p")) Then
            paginaActiva = CType(Request("p"), Integer)
        Else
            paginaActiva = 1
        End If


        Panel5.Controls.Add(generaTablaResultados(dt, _
                                                tituloTabla, _
                                                paginaActiva, _
                                                idObj, _
                                                eTipoConsulta.porClasificacionAntalis, _
                                                codigoClasificacion))
        Panel5.Visible = True

        Literal1.Text = "$(""#op5"").addClass(""active"").show();" & vbCrLf & _
                        "$(""#tab5"").show();"
    End Sub

    Private Sub bBuscarPorPatron_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bBuscarPorPatron.Click

        Dim arrStr() As String
        arrStr = Split(tbPatronCliente.Text, "::")
        Dim patron As String = arrStr(0)
        Dim busquedaExacta As Boolean

        busquedaExacta = (arrStr.Length = 2)

        inicializaPaneles()

        Dim tituloTabla As String = "Resultados para " & patron & """"

        'Vamos a buscar datos y los guardamos en variable de sesión
        Dim dt As DataTable = obtieneClientesPorPatron(codigoSociedad, patron, busquedaExacta)

        Dim idObj As String = Guid.NewGuid.ToString
        Session(idObj) = dt

        Dim paginaActiva As Integer
        If IsNumeric(Request("p")) Then
            paginaActiva = CType(Request("p"), Integer)
        Else
            paginaActiva = 1
        End If


        Panel6.Controls.Add(generaTablaResultados(dt, _
                                                tituloTabla, _
                                                paginaActiva, _
                                                idObj, _
                                                eTipoConsulta.porCliente, _
                                                patron))
        Panel6.Visible = True

        Literal1.Text = "$(""#op6"").addClass(""active"").show();" & vbCrLf & _
                        "$(""#tab6"").show();"

    End Sub

    Private Function obtieneClientesPorPatron(ByVal codigoSociedad As String, _
                                                ByVal patron As String, _
                                                ByVal busquedaExacta As Boolean) As DataTable
        Const spName = "cma_sel_clientes_x_patron"
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

            If busquedaExacta Then
                spCall.Parameters.Add("@busqueda_exacta", "1").Direction = ParameterDirection.Input
            Else
                spCall.Parameters.Add("@busqueda_exacta", "0").Direction = ParameterDirection.Input
            End If

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


#Region " Funciones de búsqueda "
    Private Function obtieneClientesPorTipoEquipo(ByVal codigoSociedad As String, ByVal tipoEquipo As String, ByVal idArtefacto As String) As DataTable
        Const spName = "cma_sel_clientes_x_tipo_artefacto"
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

            If tipoEquipo = "*" Then
                spCall.Parameters.Add("@tipo_artefacto", DBNull.Value).Direction = ParameterDirection.Input
            Else
                spCall.Parameters.Add("@tipo_artefacto", tipoEquipo).Direction = ParameterDirection.Input
            End If

            spCall.Parameters.Add("@id_artefacto", idArtefacto).Direction = ParameterDirection.Input
            daSql.SelectCommand = spCall

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

    Private Function obtieneClientesPorClasificacionAntalis(ByVal codigoSociedad As String, ByVal clasificacionAntalis As String) As DataTable
        Const spName = "cma_sel_clientes_x_clasif_antalis"
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
            spCall.Parameters.Add("@cod_cla_antalis", clasificacionAntalis).Direction = ParameterDirection.Input
            daSql.SelectCommand = spCall

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

    Private Function obtieneClientesPorCelula(ByVal codigoSociedad As String, ByVal codigoCelula As String) As DataTable
        Const spName = "cma_sel_clientes_x_celula"
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

    Private Function obtieneClientesPorCobrador(ByVal codigoSociedad As String, ByVal codigoCobrador As String) As DataTable
        Const spName = "cma_sel_clientes_x_cobrador"
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

    Private Function obtieneClientesPorVendedoraVirtual(ByVal codigoSociedad As String, ByVal codigoVendVirtual As String) As DataTable
        Const spName = "cma_sel_clientes_x_vend_virtual"
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

    Private Function obtieneClientesPorEjecutivoComercial(ByVal codigoSociedad As String, ByVal codigoEjecComercial As String) As DataTable
        Const spName = "cma_sel_clientes_x_ejec_com"
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


    Private Function obtieneArtefactosValoresPorCliente(ByVal codigoSociedad As String, ByVal codigoCliente As String) As DataTable
        Const spName = "cma_sel_artefactos_valores_x_cliente"
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
            spCall.Parameters.Add("@cod_cliente", codigoCliente).Direction = ParameterDirection.Input

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


#End Region

    Private Sub ddlTipo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlTipo.SelectedIndexChanged
        With ddlEquipo
            .ClearSelection()
            .Items.Clear()
            .DataSource = CArtefacto.obtieneOcurrenciasPorTipo(ddlTipo.SelectedValue, "%")
            .DataTextField = "dmc_artefacto"
            .DataValueField = "id_artefacto"
            .DataBind()

            .ClearSelection()
            .Items.Add(New ListItem("* Todos los equipos *", "-1"))
            .Items.FindByValue("-1").Selected = True
        End With

        Literal1.Text = "$(""#op7"").addClass(""active"").show();" & vbCrLf & _
                "$(""#tab7"").show();"
    End Sub

    Private Sub bBuscarPorTipoEquipo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bBuscarPorTipoEquipo.Click
        inicializaPaneles()

        Dim tituloTabla As String = "Tipo / Equipo : " & ddlTipo.SelectedItem.Text & " - " & ddlEquipo.SelectedItem.Text

        'Vamos a buscar datos y los guardamos en variable de sesión
        Dim dt As DataTable = obtieneClientesPorTipoEquipo(codigoSociedad, ddlTipo.SelectedValue, ddlEquipo.SelectedValue)

        Dim idObj As String = Guid.NewGuid.ToString
        Session(idObj) = dt

        Dim paginaActiva As Integer
        If IsNumeric(Request("p")) Then
            paginaActiva = CType(Request("p"), Integer)
        Else
            paginaActiva = 1
        End If

        Panel7.Controls.Add(generaTablaResultados(dt, _
                                                tituloTabla, _
                                                paginaActiva, _
                                                idObj, _
                                                eTipoConsulta.porTipoArtefacto, _
                                                ddlTipo.SelectedValue & "," & ddlEquipo.SelectedValue))
        Panel7.Visible = True

        Literal1.Text = "$(""#op7"").addClass(""active"").show();" & vbCrLf & _
                        "$(""#tab7"").show();"
    End Sub
End Class
