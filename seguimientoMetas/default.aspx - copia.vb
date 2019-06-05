Imports System.Data.SqlClient
Imports System.Data

Public Class seguimientometas_default
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents MPContainer As Wilson.MasterPages.MasterPage
    Protected WithEvents MPTitle As Wilson.MasterPages.ContentRegion
    Protected WithEvents MPCaption As Wilson.MasterPages.ContentRegion
    Protected WithEvents Literal1 As System.Web.UI.WebControls.Literal
    Protected WithEvents bBuscarPorCelula As System.Web.UI.WebControls.Button
    Protected WithEvents ddlCelula As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Panel1 As System.Web.UI.WebControls.Panel
    Protected WithEvents ddlAgente As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlAno2 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlMes2 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents bBuscarPorAgente As System.Web.UI.WebControls.Button
    Protected WithEvents Panel2 As System.Web.UI.WebControls.Panel
    Protected WithEvents DGMetasAnuales As System.Web.UI.WebControls.DataGrid
    Protected WithEvents Button1 As System.Web.UI.WebControls.Button
    Protected WithEvents Panel3 As System.Web.UI.WebControls.Panel
    Protected WithEvents ddlVETEL As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlMesVETEL As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlAnoVETEL As System.Web.UI.WebControls.DropDownList
    Protected WithEvents bBuscarPorVETEL As System.Web.UI.WebControls.Button
    Protected WithEvents ddlAnoVTPUB As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlMesVTPUB As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlAno As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlMes As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlVTPUB As System.Web.UI.WebControls.DropDownList
    Protected WithEvents bBuscarPorVTPUB As System.Web.UI.WebControls.Button
    Protected WithEvents Panel4 As System.Web.UI.WebControls.Panel


    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Const REGISTROS_POR_PAGINA As Integer = 100
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
        porVendedoraTelefono
        porVendedoraPublico
    End Enum


    Dim contador As Integer
    Dim ultimoCodigoCliente As String
    Dim codigoSociedad As String = "GMSC"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Session(Constantes.CTE_ANDES_USUARIO_AUTENTICADO) <> "SI" Then
            Response.Redirect("/logout.aspx")
            Return
        End If

        'Chequeamos que el usuario ejecutando la acción tenga acceso a este módulo
        Dim pu As CPermisoUsuario = Session(Constantes.CTE_OBJ_PERMISOS_USUARIO)
        If Not pu.validaPermiso(Constantes.MODULO_SEGUIMIENTO_METAS, CPermisoUsuario.eTipoAccion.lectura) Then
            Response.Redirect("/")
            Return
        End If

        Dim grupoAgente As String = Session(Constantes.CTE_ANDES_GRUPO_AGENTE)
        Dim codigoAgente As String = Session(Constantes.CTE_ANDES_CODIGO_AGENTE)
        Dim codigoCelula As String = Session(Constantes.CTE_ANDES_CODIGO_CELULA)
        Dim perfilUsuario As String = Session(Constantes.CTE_OBJ_USER_INFO_PERFIL)
        Dim tipoAgente As String = Session(Constantes.CTE_ANDES_CODIGO_TIPO_AGENTE)
        Dim codigoSociedad As String = "GMSC"
        Dim superAgente As Boolean = (Session(Constantes.CTE_ANDES_SUPER_AGENTE) = "X")

        If Page.IsPostBack = False Then
            Panel1.Visible = False

            Dim ws As cl.gms.andes.ws.OrgSrv = New cl.gms.andes.ws.OrgSrv
            Dim i As Integer

            '-------------------------------------------------------
            'Cargamos codigos celulas
            '-------------------------------------------------------
            With ddlCelula
                .DataSource = CCelula.obtieneOcurrencias()
                .DataTextField = "celula"
                .DataValueField = "celula"
                .DataBind()
            End With

            '-------------------------------------------------------
            'Cargamos codigos de agentes ejecutivos comerciales 
            '-------------------------------------------------------
            With ddlAgente
                .DataSource = ws.listaPromotoras("GMSC")
                .DataTextField = "nom_promotora"
                .DataValueField = "cod_promotora"
                .DataBind()
            End With

            '-------------------------------------------------------
            'Caragmos codigos de agentes telemarketing
            '-------------------------------------------------------
            With ddlVETEL
                .DataSource = listaVendedoras("GMSC", "VETEL", "VTE")
                .DataTextField = "nom_vendedora"
                .DataValueField = "cod_vendedora"
                .DataBind()
            End With

            '-------------------------------------------------------
            'Cargamos codigos agentes venta publico
            '-------------------------------------------------------
            With ddlVTPUB
                .DataSource = listaVendedoras("GMSC", "VTPUB", "VPU")
                .DataTextField = "nom_vendedora"
                .DataValueField = "cod_vendedora"
                .DataBind()
            End With

            '-------------------------------------------------------
            'Filtramos combo boxs segun el usuario 
            '-------------------------------------------------------
            If perfilUsuario = "EJEC" Or superAgente Then
                'No filtramos nada. Full acceso-
            Else
                If grupoAgente = "CELULA" Or grupoAgente = "TELEMKTG" Then
                    'Si estoy asociado a una célula, entonces sólo dejamos esa célula                    
                    i = 0
                    While i <= ddlCelula.Items.Count - 1
                        If (grupoAgente = "CELULA" And ddlCelula.Items(i).Value <> codigoCelula) Or _
                            (grupoAgente = "TELEMKTG" And ddlCelula.Items(i).Value <> "TLM") Then
                            ddlCelula.Items.RemoveAt(i)
                        Else
                            i += 1
                        End If
                    End While

                    '-------------------------------------------------------
                    'Si soy CELULA, entonces en SOLAPA solo dejamos los ejecutivos
                    'de esa célula.
                    '-------------------------------------------------------
                    Dim dtEjecomCelula As DataTable = listaEjecutivosComercialesPorCelula(codigoSociedad, codigoCelula)
                    If grupoAgente = "CELULA" Then
                        i = 0
                        Dim elimina As Boolean

                        While i <= ddlAgente.Items.Count - 1

                            elimina = True 'Asumo que voy a eliminar a menos que se determine lo contrario

                            'Busco en la lista de agentes de la célula
                            For Each dr As DataRow In dtEjecomCelula.Rows
                                If ddlAgente.Items(i).Value = Trim(dr("cod_ejec_com")) Then
                                    elimina = False
                                    Exit For
                                End If
                            Next

                            If elimina Then
                                ddlAgente.Items.RemoveAt(i)
                            Else
                                i += 1
                            End If

                        End While
                    ElseIf grupoAgente = "TELEMKTG" Then
                        '-------------------------------------------------------
                        'Si soy TELEMKTG, entonces en solapa CARTERA solo dejo
                        'al ejecutivo.
                        '-------------------------------------------------------
                        i = 0
                        While i <= ddlAgente.Items.Count - 1
                            If ddlAgente.Items(i).Value <> codigoAgente Then
                                ddlAgente.Items.RemoveAt(i)
                            Else
                                i += 1
                            End If
                        End While
                    End If

                    If Not IsNothing(ddlAgente.Items.FindByValue(codigoAgente)) Then
                        ddlAgente.Items.FindByValue(codigoAgente).Selected = True
                    End If

                    'ElseIf grupoAgente = "VENTAS" Then
                    '    If (Not IsNothing(ddlAgente.Items.FindByValue(codigoAgente))) Then
                    '        ddlAgente.Items.FindByValue(codigoAgente).Selected = True
                    '        ddlAgente.Enabled = False
                    '    End If
                ElseIf grupoAgente = "VETEL" Then

                    '------------------------------------------------
                    'Dejamos sólo a la vendedora que corresponda
                    '------------------------------------------------
                    i = 0
                    While i <= ddlVETEL.Items.Count - 1
                        If (codigoAgente <> "041" And codigoAgente <> "048" And ddlVETEL.Items(i).Value <> codigoAgente) Or _
                            ((codigoAgente = "041" Or codigoAgente = "048") And (ddlVETEL.Items(i).Value <> "4148")) Then
                            ddlVETEL.Items.RemoveAt(i)
                        Else
                            i += 1
                        End If
                    End While

                    If codigoAgente = "041" Or codigoAgente = "048" Then
                        If Not IsNothing(ddlVETEL.Items.FindByValue("4148")) Then
                            ddlVETEL.Items.FindByValue("4148").Selected = True
                        End If
                    End If

                ElseIf grupoAgente = "VTPUB" Then

                    '------------------------------------------------
                    'Dejamos sólo a la vendedora que corresponda
                    '------------------------------------------------
                    i = 0
                    While i <= ddlVTPUB.Items.Count - 1
                        If ddlVTPUB.Items(i).Value <> codigoAgente Then
                            ddlVTPUB.Items.RemoveAt(i)
                        Else
                            i += 1
                        End If
                    End While

                    If (Not IsNothing(ddlVTPUB.Items.FindByValue(codigoAgente))) Then
                        ddlVTPUB.Items.FindByValue(codigoAgente).Selected = True
                    End If

                End If
            End If

            For i = 2010 To Now.Year
                ddlAno.Items.Add(New ListItem(i, i))
                ddlAno2.Items.Add(New ListItem(i, i))
                ddlAnoVETEL.Items.Add(New ListItem(i, i))
                ddlAnoVTPUB.Items.Add(New ListItem(i, i))
            Next

            Dim dmcMes As String
            For i = 1 To 12
                Select Case i
                    Case 1
                        dmcMes = "Enero"
                    Case 2
                        dmcMes = "Febrero"
                    Case 3
                        dmcMes = "Marzo"
                    Case 4
                        dmcMes = "Abril"
                    Case 5
                        dmcMes = "Mayo"
                    Case 6
                        dmcMes = "Junio"
                    Case 7
                        dmcMes = "Julio"
                    Case 8
                        dmcMes = "Agosto"
                    Case 9
                        dmcMes = "Septiembre"
                    Case 10
                        dmcMes = "Octubre"
                    Case 11
                        dmcMes = "Noviembre"
                    Case 12
                        dmcMes = "Diciembre"
                End Select
                ddlMes.Items.Add(New ListItem(dmcMes, i))
                ddlMesVETEL.Items.Add(New ListItem(dmcMes, i))
                ddlMesVTPUB.Items.Add(New ListItem(dmcMes, i))
            Next

            'Establecemos valores por defecto
            ddlAno.Items.FindByValue(Now.Year).Selected = True
            ddlAno2.Items.FindByValue(Now.Year).Selected = True
            ddlAnoVETEL.Items.FindByValue(Now.Year).Selected = True
            ddlAnoVTPUB.Items.FindByValue(Now.Year).Selected = True
            ddlMes.Items.FindByValue(Now.Month).Selected = True
            ddlMesVETEL.Items.FindByValue(Now.Month).Selected = True
            ddlMesVTPUB.Items.FindByValue(Now.Month).Selected = True

        End If



        'Ahora indicamos qué solapas verá el usuario.

        If perfilUsuario = "EJEC" Or superAgente Then
            Literal1.Text = "$(""ul.tabs li:first"").addClass(""active"").show();" & vbCrLf & _
                            "$("".tab_content:first"").show();"
        Else
            Literal1.Text = ""

            'Si no es célula ni telemarketing entonces escondemos solapa célula

            Select Case grupoAgente
                Case "CELULA"
                    'Ocultamos solapa Venta Teléfono
                    Literal1.Text &= "$('#op3').hide();" & vbCrLf
                    Literal1.Text &= "$('#tab3').hide();" & vbCrLf

                    'Ocultamos solapa Venta Público
                    Literal1.Text &= "$('#op4').hide();" & vbCrLf
                    Literal1.Text &= "$('#tab4').hide();" & vbCrLf

                    'Dejamos por defecto activa solapa CELULAS
                    Literal1.Text &= "$('#op1').addClass(""active"").show();" & vbCrLf & _
                                    "$('#tab1').show();"
                Case "TELEMKTG"
                    If tipoAgente <> "EJECO" Then
                        'Si no es del tipo ejecutivo comercial, entonces
                        'ocultamos solapa CARTERA
                        Literal1.Text &= "$('#op2').hide();" & vbCrLf
                        Literal1.Text &= "$('#tab2').hide();" & vbCrLf
                    End If

                    'Ocultamos solapa VENTA TELEFONO
                    Literal1.Text &= "$('#op3').hide();" & vbCrLf
                    Literal1.Text &= "$('#tab3').hide();" & vbCrLf

                    'Ocultamos solapa VENTA PUBLICO
                    Literal1.Text &= "$('#op4').hide();" & vbCrLf
                    Literal1.Text &= "$('#tab4').hide();" & vbCrLf

                    'Dejamos activa la solapa CARTERA
                    Literal1.Text &= "$('#op1').addClass('active').show();" & vbCrLf & _
                                    "$('#tab1').show();"

                Case "VETEL"
                    'ocultamos solapa CELULA
                    Literal1.Text &= "$('#op1').hide();" & vbCrLf
                    Literal1.Text &= "$('#tab1').hide();" & vbCrLf

                    'ocultamos solapa CARTERA
                    Literal1.Text &= "$('#op2').hide();" & vbCrLf
                    Literal1.Text &= "$('#tab2').hide();" & vbCrLf

                    'ocultamos solapa VENTA PUBLICO
                    Literal1.Text &= "$('#op4').hide();" & vbCrLf
                    Literal1.Text &= "$('#tab4').hide();" & vbCrLf

                    'Dejamos activa la solapa VENTA TELEFONO
                    Literal1.Text &= "$('#op3').addClass('active').show();" & vbCrLf & _
                                    "$('#tab3').show();"

                Case "VTPUB"
                    'Ocultamos solapa CELULA
                    Literal1.Text &= "$('#op1').hide();" & vbCrLf
                    Literal1.Text &= "$('#tab1').hide();" & vbCrLf

                    'Ocultamos solapa CARTERA
                    Literal1.Text &= "$('#op2').hide();" & vbCrLf
                    Literal1.Text &= "$('#tab2').hide();" & vbCrLf

                    'Ocultamos solapa VENTA TELEFONO
                    Literal1.Text &= "$('#op3').hide();" & vbCrLf
                    Literal1.Text &= "$('#tab3').hide();" & vbCrLf

                    'Dejamos activa la solapa VENTA PUBLICO
                    Literal1.Text &= "$('#op4').addClass('active').show();" & vbCrLf & _
                                    "$('#tab4').show();"

            End Select

        End If
    End Sub

    Private Sub inicializaPaneles()
        Panel1.Controls.Clear()
        Panel1.Visible = False
    End Sub

    Private Function generaTablaResultados(ByVal dtDatos As DataTable, _
                                            ByVal tituloEncabezado As String, _
                                            ByVal paginaActiva As Integer, _
                                            ByVal idObjeto As String, _
                                            ByVal tipo As eTipoConsulta, _
                                            ByVal llave As String, _
                                            ByVal fechaConsulta As DateTime) As Table

        Dim tResultado As Table = New Table

        Select Case tipo
            Case eTipoConsulta.porCelula
                Dim usuarioSesion As t_Usuario = CType(Session(Constantes.CTE_OBJ_USER_INFO), t_Usuario)
                Dim lTexto As Label

                With tResultado
                    .CellPadding = 0
                    .CellSpacing = 1
                    .BorderWidth = New Unit(0, UnitType.Pixel)
                End With

                tResultado.Rows.Add(New TableRow)
                tResultado.Rows(tResultado.Rows.Count - 1).Cells.Add(New TableCell)
                tResultado.Rows(tResultado.Rows.Count - 1).Cells(0).ColumnSpan = 8
                tResultado.Rows(tResultado.Rows.Count - 1).Cells(0).Text = tituloEncabezado
                tResultado.Rows(tResultado.Rows.Count - 1).Cells(0).CssClass = "met-cabecera-titulo"

                tResultado.Rows.Add(New TableRow)
                For k As Integer = 0 To 7
                    tResultado.Rows(tResultado.Rows.Count - 1).Cells.Add(New TableCell)
                Next

                tResultado.Rows(tResultado.Rows.Count - 1).CssClass = "met-cabecera-resultado"

                tResultado.Rows(tResultado.Rows.Count - 1).Cells(0).Text = "GRUPO"
                tResultado.Rows(tResultado.Rows.Count - 1).Cells(1).Text = "TIPO EJECUTIVO"
                tResultado.Rows(tResultado.Rows.Count - 1).Cells(2).Text = "EJECUTIVO"
                tResultado.Rows(tResultado.Rows.Count - 1).Cells(3).Text = "CONCEPTO"
                tResultado.Rows(tResultado.Rows.Count - 1).Cells(4).Text = "NIVEL"
                tResultado.Rows(tResultado.Rows.Count - 1).Cells(5).Text = fechaConsulta.ToString("MMM-yy").ToUpper  'Mes actual
                tResultado.Rows(tResultado.Rows.Count - 1).Cells(6).Text = "META"
                tResultado.Rows(tResultado.Rows.Count - 1).Cells(7).Text = "CUMPLIMIENTO"


                tResultado.Rows(tResultado.Rows.Count - 1).Cells(0).CssClass = "met-cabecera-c0"
                tResultado.Rows(tResultado.Rows.Count - 1).Cells(1).CssClass = "met-cabecera-c1"
                tResultado.Rows(tResultado.Rows.Count - 1).Cells(2).CssClass = "met-cabecera-c2"
                tResultado.Rows(tResultado.Rows.Count - 1).Cells(3).CssClass = "met-cabecera-c3"
                tResultado.Rows(tResultado.Rows.Count - 1).Cells(4).CssClass = "met-cabecera-c4"
                tResultado.Rows(tResultado.Rows.Count - 1).Cells(5).CssClass = "met-cabecera-c5"
                tResultado.Rows(tResultado.Rows.Count - 1).Cells(6).CssClass = "met-cabecera-c6"
                tResultado.Rows(tResultado.Rows.Count - 1).Cells(7).CssClass = "met-cabecera-c6"

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
                Dim agenteAnterior As String = ""
                Dim tipoAgenteAnterior As String = ""
                Dim grupoAgenteAnterior As String = ""

                Dim realMesMGGUSDCVI As Double = 0
                Dim realMesMGGUSDEII As Double = 0
                Dim realMesMGGUSDEST As Double = 0
                Dim realMesMGGUSDPAK As Double = 0
                Dim realMesMGGUSDPAP As Double = 0
                Dim realMesMIXPRTOTAL As Double = 0

                Dim metaMesMGGUSDCVI As Double = 0
                Dim metaMesMGGUSDEII As Double = 0
                Dim metaMesMGGUSDEST As Double = 0
                Dim metaMesMGGUSDPAK As Double = 0
                Dim metaMesMGGUSDPAP As Double = 0
                Dim metaMesMIXPRTOTAL As Double = 0

                Dim valMeta As Double
                Dim valReal As Double

                For iIndice As Integer = iInicio To iTermino
                    dr = dtDatos.Rows(iIndice)

                    tResultado.Rows.Add(New TableRow)

                    If itemAlterno Then
                        tResultado.Rows(tResultado.Rows.Count - 1).CssClass = "met-fila-alterna"
                        tResultado.Rows(tResultado.Rows.Count - 1).Attributes.Add("onmouseout", "this.className='met-fila-alterna';")
                        tResultado.Rows(tResultado.Rows.Count - 1).Attributes.Add("onmouseover", "this.className='met-fila-seleccionada';")
                        itemAlterno = Not itemAlterno
                    Else
                        tResultado.Rows(tResultado.Rows.Count - 1).CssClass = "met-fila-normal"
                        tResultado.Rows(tResultado.Rows.Count - 1).Attributes.Add("onmouseout", "this.className='met-fila-normal';")
                        tResultado.Rows(tResultado.Rows.Count - 1).Attributes.Add("onmouseover", "this.className='met-fila-seleccionada';")
                        itemAlterno = Not itemAlterno
                    End If

                    For k As Integer = 0 To 7
                        tResultado.Rows(tResultado.Rows.Count - 1).Cells.Add(New TableCell)
                    Next

                    tResultado.Rows(tResultado.Rows.Count - 1).Cells(0).CssClass = "met-dato-c0"
                    tResultado.Rows(tResultado.Rows.Count - 1).Cells(1).CssClass = "met-dato-c1"
                    tResultado.Rows(tResultado.Rows.Count - 1).Cells(2).CssClass = "met-dato-c2"
                    tResultado.Rows(tResultado.Rows.Count - 1).Cells(3).CssClass = "met-dato-c3"
                    tResultado.Rows(tResultado.Rows.Count - 1).Cells(4).CssClass = "met-dato-c4"
                    tResultado.Rows(tResultado.Rows.Count - 1).Cells(5).CssClass = "met-dato-c5"
                    tResultado.Rows(tResultado.Rows.Count - 1).Cells(6).CssClass = "met-dato-c6"
                    tResultado.Rows(tResultado.Rows.Count - 1).Cells(7).CssClass = "met-dato-c7"

                    If grupoAgenteAnterior <> Trim(dr("cod_grupo")) Then
                        tResultado.Rows(tResultado.Rows.Count - 1).Cells(0).Text = Trim(dr("cod_grupo"))
                        grupoAgenteAnterior = Trim(dr("cod_grupo"))
                    End If

                    If tipoAgenteAnterior <> Trim(dr("dmc_tipo_agente")) Then
                        tResultado.Rows(tResultado.Rows.Count - 1).Cells(1).Text = Trim(dr("dmc_tipo_agente"))
                        tipoAgenteAnterior = Trim(dr("dmc_tipo_agente"))
                    End If

                    If agenteAnterior <> Trim(dr("nom_ejecom")) Then
                        agenteAnterior = Trim(dr("nom_ejecom"))
                        tResultado.Rows(tResultado.Rows.Count - 1).Cells(2).Text = Trim(dr("nom_ejecom"))
                    End If

                    tResultado.Rows(tResultado.Rows.Count - 1).Cells(3).Text = Trim(dr("cod_concepto_meta"))
                    tResultado.Rows(tResultado.Rows.Count - 1).Cells(4).Text = Trim(dr("cod_nivel"))

                    valMeta = CType(dr("val_meta"), Double)
                    valReal = CType(dr("val_mes_actual"), Double)

                    tResultado.Rows(tResultado.Rows.Count - 1).Cells(5).Text = valReal.ToString("#,##0")
                    tResultado.Rows(tResultado.Rows.Count - 1).Cells(6).Text = valMeta.ToString("#,##0")

                    If valMeta <> 0 Then
                        tResultado.Rows(tResultado.Rows.Count - 1).Cells(7).Text = (valReal / valMeta).ToString("#,##0%")
                    End If
                Next

                tResultado.Rows.Add(New TableRow)
                tResultado.Rows(tResultado.Rows.Count - 1).Cells.Add(New TableCell)
                tResultado.Rows(tResultado.Rows.Count - 1).Cells(0).ColumnSpan = 8
                tResultado.Rows(tResultado.Rows.Count - 1).Cells(0).Text = "&nbsp;"
                tResultado.Rows(tResultado.Rows.Count - 1).Cells(0).CssClass = "met-pie-resultado"

            Case eTipoConsulta.porVendedoraTelefono, eTipoConsulta.porVendedoraPublico
                Dim usuarioSesion As t_Usuario = CType(Session(Constantes.CTE_OBJ_USER_INFO), t_Usuario)
                Dim lTexto As Label

                With tResultado
                    .CellPadding = 0
                    .CellSpacing = 1
                    .BorderWidth = New Unit(0, UnitType.Pixel)
                End With

                tResultado.Rows.Add(New TableRow)
                tResultado.Rows(tResultado.Rows.Count - 1).Cells.Add(New TableCell)
                tResultado.Rows(tResultado.Rows.Count - 1).Cells(0).ColumnSpan = 8
                tResultado.Rows(tResultado.Rows.Count - 1).Cells(0).Text = tituloEncabezado
                tResultado.Rows(tResultado.Rows.Count - 1).Cells(0).CssClass = "met-cabecera-titulo"

                tResultado.Rows.Add(New TableRow)
                For k As Integer = 0 To 7
                    tResultado.Rows(tResultado.Rows.Count - 1).Cells.Add(New TableCell)
                Next

                tResultado.Rows(tResultado.Rows.Count - 1).CssClass = "met-cabecera-resultado"

                tResultado.Rows(tResultado.Rows.Count - 1).Cells(0).Text = "GRUPO"
                tResultado.Rows(tResultado.Rows.Count - 1).Cells(1).Text = "TIPO EJECUTIVO"
                tResultado.Rows(tResultado.Rows.Count - 1).Cells(2).Text = "EJECUTIVO"
                tResultado.Rows(tResultado.Rows.Count - 1).Cells(3).Text = "CONCEPTO"
                tResultado.Rows(tResultado.Rows.Count - 1).Cells(4).Text = "NIVEL"
                tResultado.Rows(tResultado.Rows.Count - 1).Cells(5).Text = fechaConsulta.ToString("MMM-yy").ToUpper  'Mes actual
                tResultado.Rows(tResultado.Rows.Count - 1).Cells(6).Text = "META"
                tResultado.Rows(tResultado.Rows.Count - 1).Cells(7).Text = "CUMPLIMIENTO"


                tResultado.Rows(tResultado.Rows.Count - 1).Cells(0).CssClass = "met-cabecera-c0"
                tResultado.Rows(tResultado.Rows.Count - 1).Cells(1).CssClass = "met-cabecera-c1"
                tResultado.Rows(tResultado.Rows.Count - 1).Cells(2).CssClass = "met-cabecera-c2"
                tResultado.Rows(tResultado.Rows.Count - 1).Cells(3).CssClass = "met-cabecera-c3"
                tResultado.Rows(tResultado.Rows.Count - 1).Cells(4).CssClass = "met-cabecera-c4"
                tResultado.Rows(tResultado.Rows.Count - 1).Cells(5).CssClass = "met-cabecera-c5"
                tResultado.Rows(tResultado.Rows.Count - 1).Cells(6).CssClass = "met-cabecera-c6"
                tResultado.Rows(tResultado.Rows.Count - 1).Cells(7).CssClass = "met-cabecera-c6"

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
                Dim agenteAnterior As String = ""
                Dim tipoAgenteAnterior As String = ""
                Dim grupoAgenteAnterior As String = ""

                Dim realMesMGGUSDCVI As Double = 0
                Dim realMesMGGUSDEII As Double = 0
                Dim realMesMGGUSDEST As Double = 0
                Dim realMesMGGUSDPAK As Double = 0
                Dim realMesMGGUSDPAP As Double = 0
                Dim realMesMIXPRTOTAL As Double = 0

                Dim metaMesMGGUSDCVI As Double = 0
                Dim metaMesMGGUSDEII As Double = 0
                Dim metaMesMGGUSDEST As Double = 0
                Dim metaMesMGGUSDPAK As Double = 0
                Dim metaMesMGGUSDPAP As Double = 0
                Dim metaMesMIXPRTOTAL As Double = 0

                Dim valMeta As Double
                Dim valReal As Double

                For iIndice As Integer = iInicio To iTermino
                    dr = dtDatos.Rows(iIndice)

                    tResultado.Rows.Add(New TableRow)

                    If itemAlterno Then
                        tResultado.Rows(tResultado.Rows.Count - 1).CssClass = "met-fila-alterna"
                        tResultado.Rows(tResultado.Rows.Count - 1).Attributes.Add("onmouseout", "this.className='met-fila-alterna';")
                        tResultado.Rows(tResultado.Rows.Count - 1).Attributes.Add("onmouseover", "this.className='met-fila-seleccionada';")
                        itemAlterno = Not itemAlterno
                    Else
                        tResultado.Rows(tResultado.Rows.Count - 1).CssClass = "met-fila-normal"
                        tResultado.Rows(tResultado.Rows.Count - 1).Attributes.Add("onmouseout", "this.className='met-fila-normal';")
                        tResultado.Rows(tResultado.Rows.Count - 1).Attributes.Add("onmouseover", "this.className='met-fila-seleccionada';")
                        itemAlterno = Not itemAlterno
                    End If

                    For k As Integer = 0 To 7
                        tResultado.Rows(tResultado.Rows.Count - 1).Cells.Add(New TableCell)
                    Next

                    tResultado.Rows(tResultado.Rows.Count - 1).Cells(0).CssClass = "met-dato-c0"
                    tResultado.Rows(tResultado.Rows.Count - 1).Cells(1).CssClass = "met-dato-c1"
                    tResultado.Rows(tResultado.Rows.Count - 1).Cells(2).CssClass = "met-dato-c2"
                    tResultado.Rows(tResultado.Rows.Count - 1).Cells(3).CssClass = "met-dato-c3"
                    tResultado.Rows(tResultado.Rows.Count - 1).Cells(4).CssClass = "met-dato-c4"
                    tResultado.Rows(tResultado.Rows.Count - 1).Cells(5).CssClass = "met-dato-c5"
                    tResultado.Rows(tResultado.Rows.Count - 1).Cells(6).CssClass = "met-dato-c6"
                    tResultado.Rows(tResultado.Rows.Count - 1).Cells(7).CssClass = "met-dato-c7"

                    If grupoAgenteAnterior <> Trim(dr("cod_grupo")) Then
                        tResultado.Rows(tResultado.Rows.Count - 1).Cells(0).Text = Trim(dr("cod_grupo"))
                        grupoAgenteAnterior = Trim(dr("cod_grupo"))
                    End If

                    If tipoAgenteAnterior <> Trim(dr("dmc_tipo_agente")) Then
                        tResultado.Rows(tResultado.Rows.Count - 1).Cells(1).Text = Trim(dr("dmc_tipo_agente"))
                        tipoAgenteAnterior = Trim(dr("dmc_tipo_agente"))
                    End If

                    If agenteAnterior <> Trim(dr("nom_ejecom")) Then
                        agenteAnterior = Trim(dr("nom_ejecom"))
                        tResultado.Rows(tResultado.Rows.Count - 1).Cells(2).Text = Trim(dr("nom_ejecom"))
                    End If

                    tResultado.Rows(tResultado.Rows.Count - 1).Cells(3).Text = Trim(dr("cod_concepto_meta"))
                    tResultado.Rows(tResultado.Rows.Count - 1).Cells(4).Text = Trim(dr("cod_nivel"))

                    valMeta = CType(dr("val_meta"), Double)
                    valReal = CType(dr("val_mes_actual"), Double)

                    tResultado.Rows(tResultado.Rows.Count - 1).Cells(5).Text = valReal.ToString("#,##0")
                    tResultado.Rows(tResultado.Rows.Count - 1).Cells(6).Text = valMeta.ToString("#,##0")

                    If valMeta <> 0 Then
                        tResultado.Rows(tResultado.Rows.Count - 1).Cells(7).Text = (valReal / valMeta).ToString("#,##0%")
                    End If
                Next

                tResultado.Rows.Add(New TableRow)
                tResultado.Rows(tResultado.Rows.Count - 1).Cells.Add(New TableCell)
                tResultado.Rows(tResultado.Rows.Count - 1).Cells(0).ColumnSpan = 8
                tResultado.Rows(tResultado.Rows.Count - 1).Cells(0).Text = "&nbsp;"
                tResultado.Rows(tResultado.Rows.Count - 1).Cells(0).CssClass = "met-pie-resultado"
        End Select

        Return tResultado
    End Function

    Private Function generaTablaResultados(ByVal dtDatos As DataTable, ByVal paginaActiva As Integer) As Table
        Dim usuarioSesion As t_Usuario = CType(Session(Constantes.CTE_OBJ_USER_INFO), t_Usuario)
        Dim lTexto As Label

        Dim tResultado As Table = New Table

        With tResultado
            .CellPadding = 0
            .CellSpacing = 1
            .BorderWidth = New Unit(0, UnitType.Pixel)
        End With

        tResultado.Rows.Add(New TableRow)
        For k As Integer = 0 To 7
            tResultado.Rows(tResultado.Rows.Count - 1).Cells.Add(New TableCell)
        Next

        tResultado.Rows(tResultado.Rows.Count - 1).CssClass = "met-cabecera-resultado"

        tResultado.Rows(tResultado.Rows.Count - 1).Cells(0).Text = "CODCLI"
        tResultado.Rows(tResultado.Rows.Count - 1).Cells(1).Text = "CLIENTE"
        tResultado.Rows(tResultado.Rows.Count - 1).Cells(2).Text = "CONCEPTO"
        tResultado.Rows(tResultado.Rows.Count - 1).Cells(3).Text = "NIVEL"
        tResultado.Rows(tResultado.Rows.Count - 1).Cells(4).Text = "ACTUAL"
        tResultado.Rows(tResultado.Rows.Count - 1).Cells(5).Text = "META"
        tResultado.Rows(tResultado.Rows.Count - 1).Cells(6).Text = "% CUMPL."
        tResultado.Rows(tResultado.Rows.Count - 1).Cells(7).Text = "COMENTARIO"

        tResultado.Rows(tResultado.Rows.Count - 1).Cells(0).CssClass = "met-cabecera-c0"
        tResultado.Rows(tResultado.Rows.Count - 1).Cells(1).CssClass = "met-cabecera-c1"
        tResultado.Rows(tResultado.Rows.Count - 1).Cells(2).CssClass = "met-cabecera-c2"
        tResultado.Rows(tResultado.Rows.Count - 1).Cells(3).CssClass = "met-cabecera-c3"
        tResultado.Rows(tResultado.Rows.Count - 1).Cells(4).CssClass = "met-cabecera-c4"
        tResultado.Rows(tResultado.Rows.Count - 1).Cells(5).CssClass = "met-cabecera-c5"
        tResultado.Rows(tResultado.Rows.Count - 1).Cells(6).CssClass = "met-cabecera-c6"
        tResultado.Rows(tResultado.Rows.Count - 1).Cells(7).CssClass = "met-cabecera-c6"

        Dim itemAlterno As Boolean = True
        Dim filaActual As Integer = 0

        Dim usrResponsable As String = Session(Constantes.CTE_ANDES_USERNAME)
        Dim totalClientes As Integer = 0

        Dim iInicio As Integer = 0 '(paginaActiva - 1) * REGISTROS_POR_PAGINA
        If iInicio > dtDatos.Rows.Count Then
            iInicio = dtDatos.Rows.Count - 1
        End If

        Dim iTermino As Integer = dtDatos.Rows.Count - 1 'iInicio + REGISTROS_POR_PAGINA - 1
        If iTermino >= dtDatos.Rows.Count Then
            iTermino = dtDatos.Rows.Count - 1
        End If

        Dim dr As DataRow
        Dim ClienteAnterior As String = ""
        Dim ConceptoAnterior As String = ""
        Dim valMeta As Double
        Dim valReal As Double

        For iIndice As Integer = iInicio To iTermino
            dr = dtDatos.Rows(iIndice)

            tResultado.Rows.Add(New TableRow)

            If itemAlterno Then
                tResultado.Rows(tResultado.Rows.Count - 1).CssClass = "met-fila-alterna"
                tResultado.Rows(tResultado.Rows.Count - 1).Attributes.Add("onmouseout", "this.className='met-fila-alterna';")
                tResultado.Rows(tResultado.Rows.Count - 1).Attributes.Add("onmouseover", "this.className='met-fila-seleccionada';")
                itemAlterno = Not itemAlterno
            Else
                tResultado.Rows(tResultado.Rows.Count - 1).CssClass = "met-fila-normal"
                tResultado.Rows(tResultado.Rows.Count - 1).Attributes.Add("onmouseout", "this.className='met-fila-normal';")
                tResultado.Rows(tResultado.Rows.Count - 1).Attributes.Add("onmouseover", "this.className='met-fila-seleccionada';")
                itemAlterno = Not itemAlterno
            End If

            For k As Integer = 0 To 7
                tResultado.Rows(tResultado.Rows.Count - 1).Cells.Add(New TableCell)
            Next

            tResultado.Rows(tResultado.Rows.Count - 1).Cells(0).CssClass = "met-dato-c0"
            tResultado.Rows(tResultado.Rows.Count - 1).Cells(1).CssClass = "met-dato-c1"
            tResultado.Rows(tResultado.Rows.Count - 1).Cells(2).CssClass = "met-dato-c2"
            tResultado.Rows(tResultado.Rows.Count - 1).Cells(3).CssClass = "met-dato-c3"
            tResultado.Rows(tResultado.Rows.Count - 1).Cells(4).CssClass = "met-dato-c4"
            tResultado.Rows(tResultado.Rows.Count - 1).Cells(5).CssClass = "met-dato-c5"
            tResultado.Rows(tResultado.Rows.Count - 1).Cells(6).CssClass = "met-dato-c6"
            tResultado.Rows(tResultado.Rows.Count - 1).Cells(7).CssClass = "met-dato-c7"

            If ClienteAnterior <> Trim(dr("cod_cliente")) Then
                tResultado.Rows(tResultado.Rows.Count - 1).Cells(0).Text = Trim(dr("cod_cliente"))
                tResultado.Rows(tResultado.Rows.Count - 1).Cells(1).Text = Trim(dr("nom_cliente"))
                ClienteAnterior = Trim(dr("cod_cliente"))
            End If

            If ConceptoAnterior <> Trim(dr("cod_concepto_meta")) Then
                tResultado.Rows(tResultado.Rows.Count - 1).Cells(2).Text = Trim(dr("cod_concepto_meta"))
                ConceptoAnterior = Trim(dr("cod_concepto_meta"))
            End If

            tResultado.Rows(tResultado.Rows.Count - 1).Cells(3).Text = Trim(dr("cod_nivel"))

            valMeta = CType(dr("val_meta"), Double)
            valReal = CType(dr("val_actual"), Double)

            tResultado.Rows(tResultado.Rows.Count - 1).Cells(4).Text = valReal.ToString("#,##0")
            tResultado.Rows(tResultado.Rows.Count - 1).Cells(5).Text = valMeta.ToString("#,##0")

            If valMeta <> 0 Then
                tResultado.Rows(tResultado.Rows.Count - 1).Cells(6).Text = (valReal / valMeta).ToString("#,##0%")
            End If

            tResultado.Rows(tResultado.Rows.Count - 1).Cells(7).Text = Trim(dr("gls_comentario"))
        Next

        tResultado.Rows.Add(New TableRow)
        tResultado.Rows(tResultado.Rows.Count - 1).Cells.Add(New TableCell)
        tResultado.Rows(tResultado.Rows.Count - 1).Cells(0).ColumnSpan = 8
        tResultado.Rows(tResultado.Rows.Count - 1).Cells(0).Text = "&nbsp;"
        tResultado.Rows(tResultado.Rows.Count - 1).Cells(0).CssClass = "met-pie-resultado"

        Return tResultado
    End Function

    Private Sub bBuscarPorCelula_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bBuscarPorCelula.Click
        inicializaPaneles()

        Dim tituloTabla As String = "Célula: " & ddlCelula.SelectedItem.Text.Trim
        Dim codigoCelula As String = ddlCelula.SelectedValue
        Dim anoPeriodo As Integer = ddlAno.SelectedValue
        Dim mesPeriodo As Integer = ddlMes.SelectedValue

        'Vamos a buscar datos y los guardamos en variable de sesión
        Dim dt As DataTable = obtieneInfoMetasPorCelula(codigoSociedad, codigoCelula, anoPeriodo, mesPeriodo)

        Dim idObj As String = Guid.NewGuid.ToString
        Session(idObj) = dt

        Dim paginaActiva As Integer
        If IsNumeric(Request("p")) Then
            paginaActiva = CType(Request("p"), Integer)
        Else
            paginaActiva = 1
        End If

        Dim fechaConsulta As DateTime = New Date(anoPeriodo, mesPeriodo, 1)

        Panel1.Controls.Add(generaTablaResultados(dt, _
                                                tituloTabla, _
                                                paginaActiva, _
                                                idObj, _
                                                eTipoConsulta.porCelula, _
                                                codigoCelula, fechaConsulta))
        Panel1.Visible = True

        Literal1.Text &= "$('ul.tabs li').removeClass('active'); " & vbCrLf & _
                        "$('.tab_content').hide();" & vbCrLf & _
                        "$('#op1').addClass('active').show();" & vbCrLf & _
                        "$('#tab1').show();"
    End Sub

    Private Sub bBuscarPorAgente_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bBuscarPorAgente.Click
        inicializaPaneles()

        Dim tituloTabla As String = "Agente: " & ddlAgente.SelectedItem.Text.Trim
        Dim codigoAgente As String = ddlAgente.SelectedValue
        Dim anoPeriodo As Integer = ddlAno2.SelectedValue
        'Dim mesPeriodo As Integer = ddlMes2.SelectedValue


        'Vamos a buscar datos y los guardamos en variable de sesión
        Dim dt As DataTable = obtieneInfoMetasPorEjecutivo(codigoSociedad, codigoAgente, anoPeriodo)

        Dim idObj As String = Guid.NewGuid.ToString
        Session(idObj) = dt

        Dim paginaActiva As Integer
        If IsNumeric(Request("p")) Then
            paginaActiva = CType(Request("p"), Integer)
        Else
            paginaActiva = 1
        End If

        'Dim fechaConsulta As DateTime = New Date(anoPeriodo, mesPeriodo, 1)

        Panel2.Controls.Add(generaTablaResultados(dt, paginaActiva))
        Panel2.Visible = True

        Literal1.Text &= "$('ul.tabs li').removeClass('active'); " & vbCrLf & _
                        "$('.tab_content').hide();" & vbCrLf & _
                        "$('#op2').addClass('active').show();" & vbCrLf & _
                        "$('#tab2').show();"

    End Sub

    Private Sub bBuscarPorVETEL_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bBuscarPorVETEL.Click
        inicializaPaneles()

        Dim tituloTabla As String = "Vendedoras Teléfono"
        Dim codigoVETEL As String = ddlVETEL.SelectedValue
        Dim anoPeriodo As Integer = ddlAnoVETEL.SelectedValue
        Dim mesPeriodo As Integer = ddlMesVETEL.SelectedValue

        'Vamos a buscar datos y los guardamos en variable de sesión
        Dim dt As DataTable = obtieneInfoMetasPorVendedora(codigoSociedad, "VETEL", codigoVETEL, anoPeriodo, mesPeriodo)

        Dim idObj As String = Guid.NewGuid.ToString
        Session(idObj) = dt

        Dim paginaActiva As Integer
        If IsNumeric(Request("p")) Then
            paginaActiva = CType(Request("p"), Integer)
        Else
            paginaActiva = 1
        End If

        Dim fechaConsulta As DateTime = New Date(anoPeriodo, mesPeriodo, 1)

        Panel3.Controls.Add(generaTablaResultados(dt, _
                                                tituloTabla, _
                                                paginaActiva, _
                                                idObj, _
                                                eTipoConsulta.porVendedoraTelefono, _
                                                codigoVETEL, fechaConsulta))
        Panel3.Visible = True

        Literal1.Text &= "$('ul.tabs li').removeClass('active'); " & vbCrLf & _
                        "$('.tab_content').hide();" & vbCrLf & _
                        "$('#op3').addClass('active').show();" & vbCrLf & _
                        "$('#tab3').show();"

    End Sub

    Private Sub bBuscarPorVTPUB_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bBuscarPorVTPUB.Click
        inicializaPaneles()

        Dim tituloTabla As String = "Vendedoras Público"
        Dim codigoVTPUB As String = ddlVTPUB.SelectedValue
        Dim anoPeriodo As Integer = ddlAnoVTPUB.SelectedValue
        Dim mesPeriodo As Integer = ddlMesVTPUB.SelectedValue

        'Vamos a buscar datos y los guardamos en variable de sesión
        Dim dt As DataTable = obtieneInfoMetasPorVendedora(codigoSociedad, "VTPUB", codigoVTPUB, anoPeriodo, mesPeriodo)

        Dim idObj As String = Guid.NewGuid.ToString
        Session(idObj) = dt

        Dim paginaActiva As Integer
        If IsNumeric(Request("p")) Then
            paginaActiva = CType(Request("p"), Integer)
        Else
            paginaActiva = 1
        End If

        Dim fechaConsulta As DateTime = New Date(anoPeriodo, mesPeriodo, 1)

        Panel4.Controls.Add(generaTablaResultados(dt, _
                                                tituloTabla, _
                                                paginaActiva, _
                                                idObj, _
                                                eTipoConsulta.porVendedoraTelefono, _
                                                codigoVTPUB, fechaConsulta))
        Panel3.Visible = True

        Literal1.Text &= "$('ul.tabs li').removeClass('active'); " & vbCrLf & _
                        "$('.tab_content').hide();" & vbCrLf & _
                        "$('#op4').addClass('active').show();" & vbCrLf & _
                        "$('#tab4').show();"

    End Sub

#Region " Funciones para DDL's"
    Private Function listaVendedoras(ByVal codigoSociedad As String, ByVal codigoTipoAgente As String, ByVal codigoCanalVenta As String) As DataTable
        Const spName = "aws_sel_vendedoras_canal"
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
            spCall.Parameters.Add("@cod_tipo_agente", codigoTipoAgente).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@ind_canal_venta", codigoCanalVenta).Direction = ParameterDirection.Input

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

    Private Function listaEjecutivosComercialesPorCelula(ByVal codigoSociedad As String, _
                                                            ByVal codigoCelula As String) As DataTable
        Const spName = "aws_sel_ejec_com_x_celula2"
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
#End Region

#Region " Funciones de búsqueda "
    Private Function obtieneInfoMetasPorEjecutivo(ByVal codigoSociedad As String, _
                                             ByVal codigoAgente As String, _
                                             ByVal anoPeriodo As Integer _
                                             ) As DataTable

        Const spName = "ido_sel_info_meta_anual_x_agente"
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
            spCall.Parameters.Add("@cod_agente", codigoAgente).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@ano_periodo", anoPeriodo).Direction = ParameterDirection.Input

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

    Private Function obtieneInfoMetasPorEjecutivo(ByVal codigoSociedad As String, _
                                             ByVal codigoAgente As String, _
                                             ByVal anoPeriodo As Integer, _
                                                ByVal mesPeriodo As Integer) As DataTable

        Const spName = "ido_sel_info_metas_x_agente"
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
            spCall.Parameters.Add("@cod_agente", codigoAgente).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@ano_periodo", anoPeriodo).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@mes_periodo", mesPeriodo).Direction = ParameterDirection.Input

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

    Private Function obtieneInfoMetasPorCelula(ByVal codigoSociedad As String, _
                                             ByVal codigocelula As String, _
                                             ByVal anoPeriodo As Integer, _
                                            ByVal mesPeriodo As Integer) As DataTable

        Const spName = "ido_sel_info_metas_x_celula"
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
            spCall.Parameters.Add("@cod_celula", codigocelula).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@ano_periodo", anoPeriodo).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@mes_periodo", mesPeriodo).Direction = ParameterDirection.Input

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

    Private Function obtieneInfoMetasPorVendedora(ByVal codigoSociedad As String, _
                                            ByVal codigoTipoAgente As String, _
                                            ByVal codigoVendedora As String, _
                                            ByVal anoPeriodo As Integer, _
                                            ByVal mesPeriodo As Integer) As DataTable

        Const spName = "ido_sel_info_metas_x_vendedora"
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
            spCall.Parameters.Add("@cod_tipo_agente", codigoTipoAgente).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@cod_vendedora", codigoVendedora).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@ano_periodo", anoPeriodo).Direction = ParameterDirection.Input
            spCall.Parameters.Add("@mes_periodo", mesPeriodo).Direction = ParameterDirection.Input

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
#End Region
End Class
