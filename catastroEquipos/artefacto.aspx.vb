Imports System.Data.SqlClient
Imports System.Data

Public Class artefacto
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents phPropiedades As System.Web.UI.WebControls.PlaceHolder
    Protected WithEvents Literal1 As System.Web.UI.WebControls.Literal
    Protected WithEvents bGuardar As System.Web.UI.WebControls.Button
    Protected WithEvents pBotonera As System.Web.UI.WebControls.Panel
    Protected WithEvents pValores As System.Web.UI.WebControls.Panel
    Protected WithEvents hfIdValor As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hfIdRegistroCliente As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents pNombreEquipo As System.Web.UI.WebControls.Panel
    Protected WithEvents tbMaquina As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlTipoArtefacto As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Form1 As System.Web.UI.HtmlControls.HtmlForm
    Protected WithEvents hfRazonSocial As System.Web.UI.HtmlControls.HtmlInputHidden

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Literal1.Text = ""

        If Page.IsPostBack = False Then
            With ddlTipoArtefacto
                .DataSource = CTipoArtefacto.obtieneOcurrencias()
                .DataTextField = "dmc_artefacto"
                .DataValueField = "tipo_artefacto"
                .DataBind()

                .Items.Add(New ListItem("-Seleccione-", "-1"))
                If Not IsNothing(.Items.FindByValue("-1")) Then
                    .ClearSelection()
                    .Items.FindByValue("-1").Selected = True
                End If
            End With

            pValores.Visible = False
            pBotonera.Visible = False
            pNombreEquipo.Visible = False

            If Request("accion") = "editar" And IsNumeric(Request("id")) Then
                Dim idRegistroCliente As Integer = CType(Request("id"), Integer)
                cargaValores(idRegistroCliente, True)
            Else
                hfIdRegistroCliente.Value = "0"
            End If

        End If
    End Sub


    Private Sub ddlTipoArtefacto_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlTipoArtefacto.SelectedIndexChanged
        Dim idRegistroCliente As Integer = 0
        If Request("accion") = "editar" And IsNumeric(Request("id")) Then
            idRegistroCliente = Request("id")
            hfIdRegistroCliente.Value = idRegistroCliente
        End If

        If ddlTipoArtefacto.SelectedValue <> "" Then
            cargaValores(idRegistroCliente, False)
        End If

    End Sub

    Private Sub cargaValores(ByVal idRegistroCliente As Integer, ByVal cargaDatosIniciales As Boolean)

        Dim dtRegistroCliente As DataTable
        hfIdRegistroCliente.Value = idRegistroCliente

        If ((idRegistroCliente > 0) And (cargaDatosIniciales)) Then

            dtRegistroCliente = CClienteArtefacto.obtieneRegistroCliente(idRegistroCliente)

            If dtRegistroCliente.Rows.Count = 1 Then
                hfRazonSocial.Value = Server.UrlEncode(Trim(dtRegistroCliente.Rows(0).Item("nom_cliente")))
                If Not IsNothing(ddlTipoArtefacto.Items.FindByValue(dtRegistroCliente.Rows(0).Item("tipo_artefacto"))) Then
                    'Seteamos la opción en el drop down list de tipos de equipos/artefactos
                    ddlTipoArtefacto.ClearSelection()
                    ddlTipoArtefacto.Items.FindByValue(dtRegistroCliente.Rows(0).Item("tipo_artefacto")).Selected = True
                End If
            End If
        Else
            'Datos vacios
            dtRegistroCliente = New DataTable
            dtRegistroCliente.Rows.Clear()
        End If

        phPropiedades.Controls.Clear()

        'Obtenemos todas las propiedades definidas para el tipo de equipo/artefacto seleccionado
        Dim dtPropiedad As DataTable = CTipoArtefacto.obtienePropiedades(ddlTipoArtefacto.SelectedValue)

        Dim pPropiedades As Table = New Table
        With pPropiedades
            .BorderStyle = BorderStyle.None
            .BorderWidth = New Unit(0)
            .CellPadding = 3
            .CellSpacing = 0
        End With

        Dim tbValorEscalarPropiedad As TextBox
        Dim lbValorMultiplePropiedad As ListBox
        Dim hfValores As System.Web.UI.HtmlControls.HtmlInputHidden
        Dim imgAccion As HyperLink
        Dim lTexto As Label
        Dim rbOpcion As RadioButton
        Dim ddlOpcion As DropDownList
        Dim bAccion As Button

        If ((cargaDatosIniciales) And (dtRegistroCliente.Rows.Count > 0)) Then
            'Seteamos el nombre del equipo/artefacto e impedimos
            'que cambie su tipo.
            tbMaquina.Text = Trim(dtRegistroCliente.Rows(0).Item("dmc_artefacto"))
            ddlTipoArtefacto.Enabled = False
        Else
            If Not IsNothing(Request("tbMaquina")) Then
                tbMaquina.Text = Request("tbMaquina")
            End If
        End If


        'traemos propiedades asociadas al tipo de artefacto
        For Each dr As DataRow In dtPropiedad.Rows

            Dim propiedad As String = Trim(dr("propiedad"))
            Dim dmcPropiedad As String = Trim(dr("dmc_propiedad"))
            Dim texto2 As String = Trim(dr("texto2"))
            Dim formato As String = Trim(dr("formato"))
            Dim srvDominio As String = Trim(dr("srv_domval"))
            Dim cardinalidad As String = Trim(dr("cardinalidad"))
            Dim mensaje As String = Trim(dr("mensaje"))


            pPropiedades.Rows.Add(New TableRow)
            pPropiedades.Rows(pPropiedades.Rows.Count - 1).Cells.Add(New TableCell)
            pPropiedades.Rows(pPropiedades.Rows.Count - 1).Cells.Add(New TableCell)
            pPropiedades.Rows(pPropiedades.Rows.Count - 1).Cells.Add(New TableCell)

            pPropiedades.Rows(pPropiedades.Rows.Count - 1).Cells(0).Text = dmcPropiedad & ":"
            pPropiedades.Rows(pPropiedades.Rows.Count - 1).Cells(0).Wrap = False
            pPropiedades.Rows(pPropiedades.Rows.Count - 1).Cells(0).CssClass = "cma-nombre-campo-chico"


            If cardinalidad = "escalar" Then

                Dim tTmp As Table = New Table
                tTmp.BorderWidth = New Unit(0, UnitType.Pixel)
                tTmp.CellPadding = 0
                tTmp.CellSpacing = 0
                tTmp.Rows.Add(New TableRow)
                tTmp.Rows(0).Cells.Add(New TableCell)
                tTmp.Rows(0).Cells.Add(New TableCell)


                tbValorEscalarPropiedad = New TextBox
                tbValorEscalarPropiedad.ID = "tb_" & propiedad
                tbValorEscalarPropiedad.CssClass = "cma-cifra"

                'Si se esta editando, entonces buscamos valores para el campo
                If ((cargaDatosIniciales) And (idRegistroCliente > 0)) Then
                    Dim dtValores As DataTable = CClienteArtefacto.obtieneValorRegistroCliente(idRegistroCliente, propiedad)
                    If dtValores.Rows.Count = 1 Then
                        tbValorEscalarPropiedad.Text = Trim(dtValores.Rows(0).Item("valor"))
                    Else
                        tbValorEscalarPropiedad.Text = ""
                    End If
                Else
                    If Not IsNothing(Request("tb_" & propiedad)) Then
                        tbValorEscalarPropiedad.Text = Request("tb_" & propiedad)
                    End If
                End If

                tTmp.Rows(0).Cells(0).Controls.Add(tbValorEscalarPropiedad)
                tTmp.Rows(0).Cells(1).text = texto2
                tTmp.Rows(0).Cells(1).CssClass = "cma-nombre-campo-chico"

                If srvDominio <> "" Then
                    Literal1.Text &= "$(""#" & tbValorEscalarPropiedad.ID & """).autocomplete(""" & srvDominio & """);" & vbCrLf
                End If

                pPropiedades.Rows(pPropiedades.Rows.Count - 1).Cells(1).Controls.Add(tTmp)

            ElseIf cardinalidad = "multiple" Then
                Dim tTmp As Table = New Table
                tTmp.BorderWidth = New Unit(0, UnitType.Pixel)
                tTmp.CellPadding = 0
                tTmp.CellSpacing = 0

                tTmp.Rows.Add(New TableRow)
                tTmp.Rows.Add(New TableRow)

                tTmp.Rows(0).Cells.Add(New TableCell)
                tTmp.Rows(1).Cells.Add(New TableCell)

                lbValorMultiplePropiedad = New ListBox
                lbValorMultiplePropiedad.CssClass = "cma-medida"
                lbValorMultiplePropiedad.ID = "lb_" & propiedad

                hfValores = New System.Web.UI.HtmlControls.HtmlInputHidden
                hfValores.ID = "hf_" & propiedad

                'Si se esta editando, entonces buscamos valores para el campo
                If ((cargaDatosIniciales) And (idRegistroCliente > 0)) Then
                    Dim dtValores As DataTable = CClienteArtefacto.obtieneValorRegistroCliente(idRegistroCliente, propiedad)
                    Dim txtValores As String = ""
                    For Each dr2 As DataRow In dtvalores.Rows
                        lbValorMultiplePropiedad.Items.Add(Trim(dr2("valor")))
                        txtValores = txtValores & Trim(dr2("valor")) & "||"

                    Next
                    hfValores.Value = txtValores
                Else
                    If Not IsNothing(Request("hf_" & propiedad)) Then
                        hfValores.Value = Request("hf_" & propiedad)
                        Dim arrStr() As String = Split(Request("hf_" & propiedad), "||")
                        For Each str As String In arrStr
                            lbValorMultiplePropiedad.Items.Add(str)
                        Next
                    End If
                End If

                Dim pEditor As Panel = New Panel
                pEditor.ID = "pn_" & propiedad

                Dim tTmp2 As Table = New Table

                tTmp2.Rows.Add(New TableRow)
                tTmp2.Rows.Add(New TableRow)
                tTmp2.Rows.Add(New TableRow)
                tTmp2.Rows.Add(New TableRow)
                tTmp2.Rows.Add(New TableRow)
                tTmp2.Rows.Add(New TableRow)
                tTmp2.Rows.Add(New TableRow)
                tTmp2.Rows.Add(New TableRow)

                tTmp2.Rows(0).Cells.Add(New TableCell)
                tTmp2.Rows(1).Cells.Add(New TableCell)
                tTmp2.Rows(2).Cells.Add(New TableCell)
                tTmp2.Rows(3).Cells.Add(New TableCell)
                tTmp2.Rows(4).Cells.Add(New TableCell)
                tTmp2.Rows(5).Cells.Add(New TableCell)
                tTmp2.Rows(6).Cells.Add(New TableCell)
                tTmp2.Rows(7).Cells.Add(New TableCell)

                lTexto = New Label
                lTexto.Text = "Nuevo valor:"
                tTmp2.Rows(0).Cells(0).Controls.Add(lTexto)
                tTmp2.Rows(0).Cells(0).CssClass = "cma-nuevo-valor"

                If propiedad = "MEDMANT" Then

                    'Agregamos tipo de mantilla
                    lTexto = New Label
                    lTexto.CssClass = "cma-op-texto1"
                    lTexto.Text = "Tipo:"

                    ddlOpcion = New DropDownList
                    ddlOpcion.ID = "ddl_tm_" & propiedad
                    ddlOpcion.CssClass = "cma-op-texto1"
                    ddlOpcion.Items.Add(New ListItem("impresión", "impresión"))
                    ddlOpcion.Items.Add(New ListItem("barniz", "barniz"))
                    ddlOpcion.Items.Add(New ListItem("no aplica", ""))
                    ddlOpcion.Items.FindByValue("").Selected = True
                    tTmp2.Rows(1).Cells(0).Controls.Add(lTexto)
                    tTmp2.Rows(1).Cells(0).Controls.Add(ddlOpcion)

                    'Agregamos con barra y sin barra
                    rbOpcion = New RadioButton
                    rbOpcion.CssClass = "cma-op-texto1"
                    rbOpcion.Text = "CB"
                    rbOpcion.GroupName = "grp_" & propiedad
                    rbOpcion.ID = "rb_cb_" & propiedad
                    tTmp2.Rows(2).Cells(0).Controls.Add(rbOpcion)

                    rbOpcion = New RadioButton
                    rbOpcion.CssClass = "cma-op-texto1"
                    rbOpcion.Text = "SB"
                    rbOpcion.GroupName = "grp_" & propiedad
                    rbOpcion.ID = "rb_sb_" & propiedad
                    tTmp2.Rows(2).Cells(0).Controls.Add(rbOpcion)

                End If

                lTexto = New Label
                lTexto.CssClass = "cma-op-texto1"
                lTexto.Text = "Valor:"
                tTmp2.Rows(3).Cells(0).Controls.Add(lTexto)

                tbValorEscalarPropiedad = New TextBox
                tbValorEscalarPropiedad.ID = "tb_" & propiedad
                tbValorEscalarPropiedad.CssClass = "cma-cifra"
                tTmp2.Rows(3).Cells(0).Controls.Add(tbValorEscalarPropiedad)


                lTexto = New Label
                lTexto.Text = mensaje
                tTmp2.Rows(4).Cells(0).Controls.Add(lTexto)
                tTmp2.Rows(4).Cells(0).CssClass = "cma-mensaje-valor-multiple"


                Dim pTmp As Panel = New Panel
                lTexto = New Label
                lTexto.Text = "Si no hay sugeridos, digite el nuevo valor y será creado automáticamente cuando grabe los datos."
                pTmp.Controls.Add(lTexto)
                pTmp.CssClass = "cma-texto-chico"
                tTmp2.Rows(5).Cells(0).Controls.Add(pTmp)



                bAccion = New Button
                bAccion.Text = "agregar"
                bAccion.CssClass = "cma-boton-accion-secundaria"
                bAccion.Attributes.Add("onclick", "agregaValorMultiple('" & propiedad & "'); return false;")
                tTmp2.Rows(5).Cells(0).Controls.Add(bAccion)

                bAccion = New Button
                bAccion.Text = "remover"
                bAccion.CssClass = "cma-boton-accion-secundaria"
                bAccion.Attributes.Add("onclick", "remueveValorMultiple('" & propiedad & "'); return false;")

                tTmp2.Rows(5).Cells(0).Controls.Add(bAccion)

                bAccion = New Button
                bAccion.CssClass = "cma-boton-accion-secundaria"
                bAccion.Attributes.Add("onclick", "$('#" & pEditor.ID & "').slideUp(); " & _
                                                "$('#CONT_" & propiedad & "_1').css('background-color', '#FFFFFF'); " & _
                                                "$('#CONT_" & propiedad & "_1').css('border', 'solid 1px #FFFFFF'); " & _
                                                "return false;")
                bAccion.Text = "cancelar"

                tTmp2.Rows(5).Cells(0).Controls.Add(bAccion)

                If srvDominio <> "" Then
                    Literal1.Text &= "$('#" & tbValorEscalarPropiedad.ID & "').autocomplete('" & srvDominio & "');" & vbCrLf
                End If

                Literal1.Text &= "$(""#pn_" & propiedad & """).hide();" & vbCrLf

                Literal1.Text &= vbCrLf & vbCrLf
                Literal1.Text &= "$('#lb_" & propiedad & "').dblclick(function(){" & _
                                                                    "$('[id^=pn_]').hide(); " & _
                                                                    "$('#pn_" & propiedad & "').slideToggle(); " & _
                                                                    "$('[id^=CONT_]').css('background-color', '#FFFFFF');" & _
                                                                    "$('[id^=CONT_]').css('border', 'solid 1px #FFFFFF');" & _
                                                                    "if($('#pn_" & propiedad & "').is(':visible')) " & _
                                                                    "{    " & _
                                                                    "     $('#tb_" & propiedad & "').focus(); " & _
                                                                    "     $('#CONT_" & propiedad & "_1').css('background-color', '#FFFF99');" & _
                                                                    "     $('#CONT_" & propiedad & "_1').css('border', 'solid 1px #FFEB99');" & _
                                                                    "} " & _
                                                                    "});"

                pEditor.Controls.Add(tTmp2)

                tTmp.Rows(0).Cells(0).Controls.Add(lbValorMultiplePropiedad)
                tTmp.Rows(0).Cells(0).Controls.Add(hfValores)
                tTmp.Rows(1).Cells(0).Controls.Add(pEditor)

                pPropiedades.Rows(pPropiedades.Rows.Count - 1).Cells(1).Controls.Add(tTmp)
                pPropiedades.Rows(pPropiedades.Rows.Count - 1).Cells(1).ID = "CONT_" & propiedad & "_1"

            End If
        Next

        phPropiedades.Controls.Add(pPropiedades)

        pValores.Visible = True
        pBotonera.Visible = True
        pNombreEquipo.Visible = True
    End Sub

    Private Sub validaDatosEntrada()
        'Debe haber indicado un tipo de artefacto
        If ddlTipoArtefacto.SelectedValue = "" Then
            Throw New Exception("Debe indicar un tipo de equipo")
        End If

        If tbMaquina.Text.Trim = "" Then
            Throw New Exception("Debe indicar el nombre del equipo")
        End If

    End Sub


    Private Sub bGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bGuardar.Click

        'Validamos datos de entrada
        Try
            validaDatosEntrada()
        Catch ex As Exception
            Literal1.Text = "window.parent.muestraMensaje('error', '" & ex.Message & "', 2000);"
            cargaValores(CType(hfIdRegistroCliente.Value, Integer), False)
            Return
        End Try

        Dim codigoSociedad As String = Request("cs")
        Dim codigoCliente As String = Request("cc")
        Dim tipoArtefacto As String = ddlTipoArtefacto.SelectedValue
        Dim dmcArtefacto As String = tbMaquina.Text.Trim.ToUpper
        Dim idArtefacto As Integer
        Dim usrResponsable As String = Session(Constantes.CTE_ANDES_USERNAME)

        'Determinamos el ID de la máquina en base a su descripción
        idArtefacto = CArtefacto.obtieneId(tipoArtefacto, dmcArtefacto)
        Dim usuarioSesion As t_Usuario = CType(Session(Constantes.CTE_OBJ_USER_INFO), t_Usuario)

        If idArtefacto <= 0 Then
            'No se encontró el ID, agregamos el nuevo artefacto
            idArtefacto = CArtefacto.ingresa(tipoArtefacto, dmcArtefacto, usrResponsable)
        End If

        If idArtefacto <= 0 Then
            Response.Write("Error en CArtefacto.ingresa()")
            Return
        End If

        Dim idRegistroCliente As Integer = 0

        If IsNumeric(hfIdRegistroCliente.Value) Then
            idRegistroCliente = CType(hfIdRegistroCliente.Value, Integer)
        End If

        If idRegistroCliente > 0 Then

            'Actualizamos cabecera del registro
            CClienteArtefacto.actualizaRegistroCliente(idRegistroCliente, idArtefacto, usrResponsable)

            'Actualizamos valores de propiedades
            Dim dt As DataTable = CTipoArtefacto.obtienePropiedades(tipoArtefacto)
            For Each dr As DataRow In dt.Rows

                If (Trim(dr("cardinalidad"))).ToLower = "escalar" Then
                    Dim propiedad As String = Trim(dr("propiedad"))
                    Dim valor As String = Request("tb_" + propiedad)
                    'Actualizamos el valor 
                    CClienteArtefacto.actualizaValorPropiedadRegistroCliente(idRegistroCliente, _
                                                                                propiedad, _
                                                                                valor, _
                                                                                usrResponsable)
                ElseIf (Trim(dr("cardinalidad"))).ToLower = "multiple" Then
                    'Aqui las sentencias par la actualizacion de campos de valores múltiples.
                    'Aqui las sentencias para ingresar datos multiples
                    Dim propiedad As String = Trim(dr("propiedad"))
                    Dim valor As String = Request("hf_" + propiedad)
                    Dim arrStr As String() = Split(valor, "||")

                    'Eliminamos todos los valores para la propiedad e ingresamos los nuevos.
                    CClienteArtefacto.eliminaValoresPropiedadRegistroCliente(idRegistroCliente, propiedad, usrResponsable)

                    'Ingresamos uno a uno los valores de la propiedad
                    For Each subValor As String In arrStr
                        If subValor <> "" Then
                            If ((propiedad = "MEDMANT") Or (propiedad = "MEDPLAN")) Then
                                'Si no existe la medida de mantilla o plancha, entonces la creamos antes de actualizar
                                'el registro del cliente.
                                Dim subValorSugerido() As String = Split(subValor, "(")
                                If Not CArtefacto.existeValorPropiedad(propiedad, subValorSugerido(0)) Then
                                    CArtefacto.ingresaValorPropiedad(propiedad, subValorSugerido(0), usuarioSesion.codigo)
                                End If

                            End If

                            CClienteArtefacto.ingresaValorPropiedadRegistroCliente(idRegistroCliente, _
                                                                                    propiedad, _
                                                                                    subValor, _
                                                                                    usrResponsable)
                        End If
                    Next
                End If
            Next
            Literal1.Text = "window.parent.muestraMensaje('exito', 'Datos guardados correctamente', 2000);"
            cargaValores(idRegistroCliente, False)
        Else
            'Ingresamos registro cabecera
            idRegistroCliente = CClienteArtefacto.ingresaRegistroCliente(codigoSociedad, codigoCliente, idArtefacto, usrResponsable)

            If idRegistroCliente > 0 Then
                'obtenemos listado de propiedades definidas para el tipo de artefacto
                Dim dt As DataTable = CTipoArtefacto.obtienePropiedades(tipoArtefacto)
                For Each dr As DataRow In dt.Rows
                    If (Trim(dr("cardinalidad"))).ToLower = "escalar" Then
                        Dim propiedad As String = Trim(dr("propiedad"))
                        Dim valor As String = Request("tb_" + propiedad)

                        'Ingresamos el valor al registro cliente
                        CClienteArtefacto.ingresaValorPropiedadRegistroCliente(idRegistroCliente, _
                                                                                propiedad, _
                                                                                valor, _
                                                                                usrResponsable)
                    ElseIf (Trim(dr("cardinalidad"))).ToLower = "multiple" Then
                        'Aqui las sentencias para ingresar datos multiples
                        Dim propiedad As String = Trim(dr("propiedad"))
                        Dim valor As String = Request("hf_" + propiedad)
                        Dim arrStr As String() = Split(valor, "||")

                        For Each subValor As String In arrStr
                            If subValor <> "" Then
                                If ((propiedad = "MEDMANT") Or (propiedad = "MEDPLAN")) Then
                                    'Si no existe la medida de mantilla o plancha, entonces la creamos antes de actualizar
                                    'el registro del cliente.
                                    Dim subValorSugerido() As String = Split(subValor, "(")
                                    If Not CArtefacto.existeValorPropiedad(propiedad, subValorSugerido(0)) Then
                                        CArtefacto.ingresaValorPropiedad(propiedad, subValorSugerido(0), usuarioSesion.codigo)
                                    End If

                                End If

                                CClienteArtefacto.ingresaValorPropiedadRegistroCliente(idRegistroCliente, _
                                                                                        propiedad, _
                                                                                        subValor, _
                                                                                        usrResponsable)
                            End If
                        Next
                    End If
                Next

                Literal1.Text = "window.parent.muestraMensaje('exito', 'Registro ingresado correctamente.', 2000);" & vbCrLf
                Literal1.Text &= String.Format("window.parent.agregaFilaDatos('{0}', '{1}', '{2}', '{3}', '{4}', {5});", _
                                                codigoSociedad, _
                                                codigoCliente, _
                                                "razon social", _
                                                dmcArtefacto, _
                                                tipoArtefacto, _
                                                idRegistroCliente)
                hfIdRegistroCliente.Value = idRegistroCliente
                cargaValores(idRegistroCliente, False)
            Else
                Literal1.Text = "window.parent.muestraMensaje('error', 'Error ingresando registro.', 2000);"
            End If

        End If
    End Sub

End Class
