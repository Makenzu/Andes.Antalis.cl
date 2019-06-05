Imports System.Web.UI

Public Class encuesta
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents MPTitle As Wilson.MasterPages.ContentRegion
    Protected WithEvents lbFecha As System.Web.UI.WebControls.Label
    Protected WithEvents MPCaption As Wilson.MasterPages.ContentRegion
    Protected WithEvents lbErrors As System.Web.UI.WebControls.Label
    Protected WithEvents ibExportar As System.Web.UI.WebControls.ImageButton
    Protected WithEvents MPContainer As Wilson.MasterPages.MasterPage
    Protected WithEvents tblFiltro As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents rfvCliente As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txCliente As System.Web.UI.WebControls.TextBox
    Protected WithEvents lbRazonSocial As System.Web.UI.WebControls.Label
    Protected WithEvents lbCiudad As System.Web.UI.WebControls.Label
    Protected WithEvents lbComuna As System.Web.UI.WebControls.Label
    Protected WithEvents lbFono As System.Web.UI.WebControls.Label
    Protected WithEvents lbFax As System.Web.UI.WebControls.Label
    Protected WithEvents btSend As System.Web.UI.WebControls.ImageButton
    Protected WithEvents tblForm1 As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents tblEncabezado As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents tblForm2 As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents Table4 As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lbDireccion As System.Web.UI.WebControls.Label
    Protected WithEvents lbMovil As System.Web.UI.WebControls.Label
    Protected WithEvents lbPromotora As System.Web.UI.WebControls.Label
    Protected WithEvents tbPjeVta As System.Web.UI.WebControls.TextBox
    Protected WithEvents Table6 As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents rbPrePrensa As System.Web.UI.WebControls.RadioButtonList
    Protected WithEvents tbVtaMensual As System.Web.UI.WebControls.TextBox
    Protected WithEvents tbDesPre As System.Web.UI.WebControls.TextBox
    Protected WithEvents LkCancelar As System.Web.UI.WebControls.LinkButton
    Protected WithEvents LKGrabar As System.Web.UI.WebControls.LinkButton
    Protected WithEvents txconsumo As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlPlancha As System.Web.UI.WebControls.DropDownList
    Protected WithEvents rbBarniz As System.Web.UI.WebControls.RadioButtonList
    Protected WithEvents tbnCuerpos As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlmaquina As System.Web.UI.WebControls.DropDownList
    Protected WithEvents dgMaquinas As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lkCancelar1 As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lkGrabar1 As System.Web.UI.WebControls.LinkButton
    Protected WithEvents tbConMantilla As System.Web.UI.WebControls.TextBox
    Protected WithEvents dgMantillas As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lkCancelar2 As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lkGrabar2 As System.Web.UI.WebControls.LinkButton
    Protected WithEvents tbConTintas As System.Web.UI.WebControls.TextBox
    Protected WithEvents dgTintas As System.Web.UI.WebControls.DataGrid
    Protected WithEvents TextBox1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents lbContacto1 As System.Web.UI.WebControls.Label
    Protected WithEvents lbContacto2 As System.Web.UI.WebControls.Label
    Protected WithEvents lbContacto3 As System.Web.UI.WebControls.Label
    Protected WithEvents lknuevaMaquina As System.Web.UI.WebControls.LinkButton
    Protected WithEvents plMaquinas As System.Web.UI.WebControls.Panel
    Protected WithEvents pldgMaquinas As System.Web.UI.WebControls.Panel
    Protected WithEvents lkNuevamantilla As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lkNuevaTinta As System.Web.UI.WebControls.LinkButton
    Protected WithEvents btFinalizar As System.Web.UI.WebControls.ImageButton
    Protected WithEvents TextBox9 As System.Web.UI.WebControls.TextBox
    Protected WithEvents TextBox8 As System.Web.UI.WebControls.TextBox
    Protected WithEvents TextBox7 As System.Web.UI.WebControls.TextBox
    Protected WithEvents tblForm6 As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents plMantillas As System.Web.UI.WebControls.Panel
    Protected WithEvents pldgMantillas As System.Web.UI.WebControls.Panel
    Protected WithEvents plTintas As System.Web.UI.WebControls.Panel
    Protected WithEvents pldgTintas As System.Web.UI.WebControls.Panel
    Protected WithEvents tblFicha As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents tbvtaAnual As System.Web.UI.WebControls.TextBox
    Protected WithEvents dgRubros As System.Web.UI.WebControls.DataGrid
    Protected WithEvents plRubro As System.Web.UI.WebControls.Panel
    Protected WithEvents lkbcancelar As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lkGrabarRubro As System.Web.UI.WebControls.LinkButton
    Protected WithEvents tbPjeVenta As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlRubros As System.Web.UI.WebControls.DropDownList
    Protected WithEvents pldgRubro As System.Web.UI.WebControls.Panel
    Protected WithEvents tblRubro As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lkNuevoRubro As System.Web.UI.WebControls.LinkButton
    Protected WithEvents ddlTintas As System.Web.UI.WebControls.DropDownList
    Protected WithEvents tbMantilla As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlpapel1 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlpapel2 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlpapel3 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents rbBarras As System.Web.UI.WebControls.RadioButtonList
    Protected WithEvents tbTamPlancha As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlModeloMaquina As System.Web.UI.WebControls.DropDownList

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Dim tUserInfo As usuario.t_Usuario
    Dim codFilial As String
    Dim codSucursal As String
    Dim codCliente As String

    Structure stRubroCliente
        Dim codigoRubro As String
        Dim pjeVenta As Double
        Dim descripcionRubro As String
    End Structure


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Session(Constantes.CTE_ANDES_INFO_USUARIO) Is Nothing Then
            Response.Redirect("/login.aspx?action=login")
        End If


        If IsNothing(Session(Constantes.CTE_ANDES_USUARIO_AUTENTICADO)) = True Then
            Response.Redirect("/logout.aspx")
            Response.End()
        Else
            tUserInfo = Session(Constantes.CTE_ANDES_INFO_USUARIO)
        End If

        If Page.IsPostBack = False Then

            tblFicha.Visible = False
        End If
        codFilial = tUserInfo.codigoFilial
        codSucursal = tUserInfo.codigoSucursal


    End Sub

    Private Sub btSend_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSend.Click

        Dim codCliente As String
        codCliente = Request("txCliente")

        Dim resultTbl As New DataTable

        tUserInfo = Session(Constantes.CTE_ANDES_INFO_USUARIO)

        Try
            If codCliente = "" Then
                Err.Description = "Faltan parametros para poder ejecutar la consulta."
                Err.Raise(vbObjectError + 512 + 10, "vta_x_cliente_item", Err.Description)
            End If


            resultTbl = clientesCodigo(codFilial, codSucursal, codCliente)
            If resultTbl.Rows.Count <= 0 Then


                Err.Description = "No se encontraron datos para esta consulta."
                Err.Raise(vbObjectError + 512 + 10, "pedidoClienteItem", Err.Description)

            Else
                lbRazonSocial.Text = resultTbl.Rows(0).Item("nom_cliente")
                lbDireccion.Text = resultTbl.Rows(0).Item("direccion")
                lbComuna.Text = resultTbl.Rows(0).Item("des_comuna")
                lbCiudad.Text = resultTbl.Rows(0).Item("des_ciudad")
                lbMovil.Text = resultTbl.Rows(0).Item("movil")
                lbFono.Text = resultTbl.Rows(0).Item("telefono")
                lbFax.Text = resultTbl.Rows(0).Item("fax")
                lbPromotora.Text = resultTbl.Rows(0).Item("nom_promotora")
            End If

            tblFicha.Visible = True

        Catch ex As Exception
            lbErrors.Text = Err.Description

            lbErrors.Visible = True
            Err.Clear()

        End Try
        '*************************************ventas*************************************

        Dim anoPeriodo As String
        Dim mesPeriodo As String
        Dim resultado As DataTable

        anoPeriodo = Now.Year
        mesPeriodo = Now.Month

        Try

            If codCliente = "" Then
                Err.Description = "Faltan parametros para poder ejecutar la consulta."
                Err.Raise(vbObjectError + 512 + 10, "vta_x_cliente_item", Err.Description)
            End If

            resultado = obtieneVentas(anoPeriodo, codFilial, codSucursal, codCliente)


            If resultTbl.Rows.Count <= 0 Then


                tbVtaMensual.Text = ""

                tbvtaAnual.Text = ""
                tbDesPre.Text = ""


            Else
                tbVtaMensual.Text = resultado.Rows(0).Item("vta_mensual")
                tbvtaAnual.Text = resultado.Rows(0).Item("vta_ano_anterior")
                tbDesPre.Text = resultado.Rows(0).Item("tipo_maq_preprensa")

                If resultado.Rows(0).Item("tiene_preprensa") = "SI" Then
                    rbPrePrensa.SelectedValue = 0
                Else
                    rbPrePrensa.SelectedValue = 1
                End If

            End If

        Catch ex As Exception
            lbErrors.Text = Err.Description

            lbErrors.Visible = True
            Err.Clear()

        End Try

        Detallemaquina()
        Detallemantillas()
        DetalleTintas()
        detallerubro()
        DetallePapel()

    End Sub

#Region "Detallemaquina"
    Private Function Detallemaquina()
        Dim anoPeriodo As String
        Dim mesPeriodo As String
        Dim codCliente As String
        anoPeriodo = Now.Year
        mesPeriodo = Now.Month
        codCliente = Request("txCliente")


        Dim resultado As DataTable
        resultado = MaquinaXCliente(codFilial, codCliente)
        dgMaquinas.DataSource = resultado
        dgMaquinas.DataBind()


        With ddlmaquina
            .DataSource = Maquina(codFilial, codSucursal)
            .DataTextField = "des_maquina"
            .DataValueField = "cod_maquina"
            .DataBind()
            .Items.Add(New ListItem("**Seleccione Maquina**", ""))
            .Items.FindByValue("").Selected = True
        End With

        With ddlPlancha
            .DataSource = Marcas(codFilial)
            .DataTextField = "des_marca"
            .DataValueField = "cod_marca"
            .DataBind()
            .Items.Add(New ListItem("**Seleccione Marca**", ""))
            .Items.FindByValue("").Selected = True
        End With



    End Function

#End Region

#Region "Detallemantillas"
    Private Function Detallemantillas()

        ' llenamos carga de mantillas 
        Dim anoPeriodo As String
        Dim mesPeriodo As String
        Dim codCliente As String
        anoPeriodo = Now.Year
        mesPeriodo = Now.Month
        codCliente = Request("txCliente")

        Dim resultado As DataTable
        resultado = mantillasXcliente(codFilial, codSucursal, codCliente)
        dgMantillas.DataSource = resultado
        dgMantillas.DataBind()



    End Function
#End Region

#Region "DetalleTintas"
    Private Function DetalleTintas()
        'llenamos consumo de tinta



        Dim anoPeriodo As String
        Dim mesPeriodo As String
        Dim codCliente As String
        anoPeriodo = Now.Year
        mesPeriodo = Now.Month
        codCliente = Request("txCliente")


        Dim resultado As DataTable
        resultado = tintasXcliente(codFilial, codSucursal, codCliente)
        dgTintas.DataSource = resultado
        dgTintas.DataBind()


        With ddlTintas
            .DataSource = tintas(codFilial, codSucursal)
            .DataTextField = "des_tinta"
            .DataValueField = "cod_tinta"
            .DataBind()
            .Items.Add(New ListItem("**Seleccione Tinta**", ""))
            .Items.FindByValue("").Selected = True
        End With

    End Function
#End Region

#Region "detallerubro"
    Private Function detallerubro()


        'ingresamos ventas

        Dim ventas As stventas
        Dim anoPeriodo As String
        Dim mesPeriodo As String


        anoPeriodo = Now.Year
        mesPeriodo = Now.Month



        ventas.anoPeriodo = anoPeriodo
        ventas.mesPeriodo = mesPeriodo
        ventas.codFilial = codFilial
        ventas.codSucursal = codSucursal
        ventas.codCliente = Request("txCliente")
        ventas.vta_mensual = Request("tbVtaMensual")


        If rbPrePrensa.SelectedValue = 0 Then
            ventas.tprepre = "SI"
        Else
            ventas.tprepre = "NO"
        End If

        ventas.tipo_pre = Request("tbDesPre")
        ventas.vta_ano_anterior = Request("tbvtaAnual")

        If ventas.vta_mensual <> "" Then
            ingresaventas(ventas)
        End If









        'PASA A NIVEL RUBROS

        Dim resultado As DataTable

        resultado = SegmentoXCliente(Request("txCliente"))
        dgRubros.DataSource = resultado
        dgRubros.DataBind()

        With ddlRubros
            .DataSource = SegmentoCliente()
            .DataTextField = "des_segmento"
            .DataValueField = "cod_segmento"
            .DataBind()
            .Items.Add(New ListItem("**Seleccione Rubro**", ""))
            .Items.FindByValue("").Selected = True
        End With

    End Function
    Private Sub cargaSegmentos()

        Dim resultado As DataTable
        resultado = SegmentoXCliente(Request("txCliente"))
        dgRubros.DataSource = resultado
        dgRubros.DataBind()
    End Sub


#End Region

#Region "DetallePapel"
    Private Function DetallePapel()
        'llenamos consumo de tinta



        Dim anoPeriodo As String
        Dim mesPeriodo As String
        Dim codCliente As String
        anoPeriodo = Now.Year
        mesPeriodo = Now.Month
        codCliente = Request("txCliente")


        Dim resultado As DataTable
        resultado = papelXcliente(codFilial, codSucursal, codCliente)


        With ddlpapel1
            .DataSource = papel(codFilial, codSucursal)
            .DataTextField = "des_papel"
            .DataValueField = "cod_papel"
            .DataBind()
            .Items.Add(New ListItem("**Seleccione Papel**", ""))
            .Items.FindByValue("").Selected = True
        End With

        With ddlpapel2
            .DataSource = papel(codFilial, codSucursal)
            .DataTextField = "des_papel"
            .DataValueField = "cod_papel"
            .DataBind()
            .Items.Add(New ListItem("**Seleccione Papel**", ""))
            .Items.FindByValue("").Selected = True
        End With

        With ddlpapel3
            .DataSource = papel(codFilial, codSucursal)
            .DataTextField = "des_papel"
            .DataValueField = "cod_papel"
            .DataBind()
            .Items.Add(New ListItem("**Seleccione Papel**", ""))
            .Items.FindByValue("").Selected = True
        End With

    End Function
#End Region


#Region "SEGMENTO"
    '********************************************SEGMENTO***************************************************************

    Private Sub lkNuevoRubro_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lkNuevoRubro.Click
        plRubro.Visible = False
        pldgRubro.Visible = True
    End Sub

    Private Sub lkbcancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lkbcancelar.Click
        plRubro.Visible = False
        pldgRubro.Visible = True
    End Sub

    Private Sub lkGrabarRubro_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lkGrabarRubro.Click


        Dim rubro As stSegmentos

        rubro.cod_cliente = Request("txCliente")
        rubro.porcentajes = Request("tbPjeVenta")
        rubro.cod_segmento = ddlRubros.SelectedValue
        ingresaSegmento(rubro)

        cargaSegmentos()

        plRubro.Visible = True
        pldgRubro.Visible = False

    End Sub
    Private Sub dgRubros_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgRubros.DeleteCommand
        Dim codigoCliente As String = e.Item.Cells(0).Text
        Dim codigoRubro As String = e.Item.Cells(1).Text

        eliminaSegmento(codigoRubro, codigoCliente)
        cargaSegmentos()
    End Sub

    Private Sub dgRubros_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgRubros.EditCommand
        Dim codigoCliente As String = e.Item.Cells(0).Text
        Dim segmento As String = e.Item.Cells(1).Text

        cargaSegmentos()
    End Sub

#End Region

#Region "MAQUINAS"
    '******************************************************MAQUINAS*********************************************************

    Private Sub lknuevaMaquina_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lknuevaMaquina.Click
        plMaquinas.Visible = False
        pldgMaquinas.Visible = True

    End Sub

    Private Sub LkCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LkCancelar.Click
        plMaquinas.Visible = True
        pldgMaquinas.Visible = False
    End Sub


    Private Sub dgMaquinas_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgMaquinas.DeleteCommand
        Dim codigoCliente As String = e.Item.Cells(0).Text
        Dim codigoMaquina As String = e.Item.Cells(1).Text

        eliminaMaquina(codFilial, codigoCliente, codigoMaquina)
        cargaMaquinas()
    End Sub

    Private Sub dgMaquinas_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgMaquinas.EditCommand
        Dim codigoCliente As String = e.Item.Cells(0).Text
        Dim codigoMaquina As String = e.Item.Cells(1).Text

        cargaMaquinas()
    End Sub

    Private Sub LKGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LKGrabar.Click
        Dim maquina As stMaquinas

        maquina.codFilial = codFilial
        maquina.codCliente = Request("txCliente")
        maquina.codMaquina = ddlmaquina.SelectedValue
        maquina.modeloMaquina = Request("ddlModeloMaquina")
        maquina.nCuerpos = Request("tbnCuerpos")

        If rbBarniz.SelectedValue = 0 Then
            maquina.tBarniz = "SI"
        Else
            maquina.tBarniz = "NO"
        End If

        maquina.TipoPlancha = Request("ddlPlancha").Trim
        maquina.tamanoPlancha = Request("tbTamPlancha")
        maquina.consumoMensual = Request("txconsumo")

        If rbBarras.SelectedValue = 0 Then
            maquina.tBarra = "SI"
        Else
            maquina.tBarra = "NO"
        End If



        cargaMaquinas()

        plMaquinas.Visible = True
        pldgMaquinas.Visible = False
    End Sub
    Private Sub cargaMaquinas()
        Dim resultado As DataTable
        Dim codCliente As String
        codCliente = Request("txcliente")
        resultado = MaquinaXCliente(codFilial, codCliente)
        dgMaquinas.DataSource = resultado
        dgMaquinas.DataBind()

    End Sub
#End Region

#Region "MANTILLA"
    '**********************************************MANTILLA*****************************************************************

    Private Sub lkNuevamantilla_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lkNuevamantilla.Click
        plMantillas.Visible = False
        pldgMantillas.Visible = True
    End Sub

    Private Sub dgMantillas_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgMantillas.DeleteCommand
        Dim codigoCliente As String = e.Item.Cells(0).Text
        Dim desMantilla As String = e.Item.Cells(1).Text

        eliminaMantilla(codFilial, codigoCliente, desMantilla)
        cargaMantilla()
    End Sub

    Private Sub dgMantillas_EditCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)
        Dim codigoCliente As String = e.Item.Cells(0).Text
        Dim codigoMantilla As String = e.Item.Cells(1).Text

        cargaMantilla()
    End Sub

    Private Sub lkGrabar1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lkGrabar1.Click
        Dim mantilla As stMantilla


        mantilla.codFilial = codFilial
        mantilla.codSucursal = codSucursal
        mantilla.codCliente = Request("txCliente")
        mantilla.consumo = Request("tbConMantilla")
        mantilla.codMantilla = Request("tbMantilla")

        ingresaMantilla(mantilla)
        cargaMantilla()


        plMantillas.Visible = True
        pldgMantillas.Visible = False
    End Sub


    Private Sub lkCancelar1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lkCancelar1.Click
        plMantillas.Visible = True
        pldgMantillas.Visible = False
    End Sub

    Private Sub cargaMantilla()
        Dim resultado As DataTable
        Dim codCliente As String
        codCliente = Request("txcliente")
        resultado = mantillasXcliente(codFilial, codSucursal, codCliente)
        dgMantillas.DataSource = resultado
        dgMantillas.DataBind()


    End Sub
#End Region

#Region "TINTA"
    '***********************************************TINTA**************************************************
    Private Sub lkNuevaTinta_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lkNuevaTinta.Click
        plTintas.Visible = False
        pldgTintas.Visible = True
    End Sub

    Private Sub lkCancelar2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lkCancelar2.Click
        plTintas.Visible = True
        pldgTintas.Visible = False
    End Sub
    Private Sub dgTintas_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgTintas.DeleteCommand
        Dim codigoCliente As String = e.Item.Cells(0).Text
        Dim codigoTinta As String = e.Item.Cells(1).Text

        eliminaTinta(codFilial, codigoCliente, codigoTinta)
        cargaTinta()
    End Sub


    Private Sub dgTintas_EditCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)
        Dim codigoCliente As String = e.Item.Cells(0).Text
        Dim codigoTinta As String = e.Item.Cells(1).Text

        cargaTinta()
    End Sub


    Private Sub lkGrabar2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lkGrabar2.Click
        Dim Tinta As stTinta

        Tinta.codFilial = codFilial
        Tinta.codSucursal = codSucursal
        Tinta.codCliente = Request("txCliente")
        Tinta.consumo = Request("tbConTintas")
        Tinta.codTinta = Request("ddltintas")

        ingresaTinta(Tinta)
        cargaTinta()

        plTintas.Visible = True
        pldgTintas.Visible = False
    End Sub

    Private Sub cargaTinta()
        Dim resultado As DataTable
        Dim codCliente As String
        codCliente = Request("txcliente")
        resultado = tintasXcliente(codFilial, codSucursal, codCliente)
        dgTintas.DataSource = resultado
        dgTintas.DataBind()
    End Sub


#End Region

    Private Sub btFinalizar_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Response.Redirect("/catastroClientes/encuesta.aspx")
    End Sub

    Private Sub ddlmaquina_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlmaquina.SelectedIndexChanged

        Dim cod_maquina As String
        cod_maquina = Request("ddlmaquina").Trim

        With ddlModeloMaquina
            .DataSource = ModeloMaquina(cod_maquina)
            .DataTextField = "modelo_maquina"
            .DataValueField = "modelo_maquina"
            .DataBind()
            .Items.Add(New ListItem("**Seleccione Modelo Maquina**", ""))
            .Items.FindByValue("").Selected = True
        End With
    End Sub

    Private Sub ddlPlancha_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlPlancha.SelectedIndexChanged

    End Sub
End Class
