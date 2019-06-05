Imports System.Data.SqlClient
Imports System.Data
Imports Exportador

Public Class maquinasV2
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents MPTitle As Wilson.MasterPages.ContentRegion
    Protected WithEvents MPCaption As Wilson.MasterPages.ContentRegion
    Protected WithEvents MPContainer As Wilson.MasterPages.MasterPage
    Protected WithEvents Panel1 As System.Web.UI.WebControls.Panel
    Protected WithEvents pPorCliente As System.Web.UI.WebControls.Panel
    Protected WithEvents pPorEjecutivo As System.Web.UI.WebControls.Panel
    Protected WithEvents pPorMaquina As System.Web.UI.WebControls.Panel
    Protected WithEvents hfSeleccionVista As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents lbPorCliente As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lbPorEjecutivo As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lbPorMaquina As System.Web.UI.WebControls.LinkButton
    Protected WithEvents ddlEjecutivo As System.Web.UI.WebControls.DropDownList
    Protected WithEvents tbCodigoCliente As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlMaquina As System.Web.UI.WebControls.DropDownList
    Protected WithEvents pResultadoPorCodigo As System.Web.UI.WebControls.Panel
    Protected WithEvents dgMaquinas As System.Web.UI.WebControls.DataGrid
    Protected WithEvents bBuscarPorCodigoCliente As System.Web.UI.WebControls.Button
    Protected WithEvents lRazonSocial As System.Web.UI.WebControls.Label
    Protected WithEvents lTotalOcurrenciasPorCodigo As System.Web.UI.WebControls.Label
    Protected WithEvents lEjecutivaComercial As System.Web.UI.WebControls.Label
    Protected WithEvents dgMaquinasCartera As System.Web.UI.WebControls.DataGrid
    Protected WithEvents bBuscarPorEjecutivo As System.Web.UI.WebControls.Button
    Protected WithEvents bBuscarPorMaquina As System.Web.UI.WebControls.Button
    Protected WithEvents dgClientesMaquina As System.Web.UI.WebControls.DataGrid
    Protected WithEvents pResultadoPorEjecutivoComercial As System.Web.UI.WebControls.Panel
    Protected WithEvents pResultadoPorMaquina As System.Web.UI.WebControls.Panel
    Protected WithEvents lTotalOcurrenciasPorEjecutivo As System.Web.UI.WebControls.Label
    Protected WithEvents lTotalOcurrenciasPorMaquina As System.Web.UI.WebControls.Label
    Protected WithEvents hlNuevoRegistroMaquina As System.Web.UI.WebControls.HyperLink

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Enum eVistaMaquinas
        porCliente
        porEjecutivoComercial
        porMaquina
    End Enum

    Dim codigoClienteAnterior As String = ""
    Dim numeroCliente As Integer = 0


    Const Cnt_JavaScriptWinOpen As String = " window.open('frmMaquina.aspx?accion={0}&idRegistro={1}', 'maq', 'menubar=0,resizable=0,width=407,height=518');"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Session(Constantes.CTE_ANDES_INFO_USUARIO) Is Nothing Then
            Response.Redirect("/login.aspx?action=login")
            Return
        End If


        'Put user code to initialize the page here
        Dim tUserInfo As t_Usuario
        tUserInfo = Session(Constantes.CTE_ANDES_INFO_USUARIO)
        Panel1.Visible = (tUserInfo.codigoPromo = "16")




        If Page.IsPostBack = False Then

            If Request("vista") = "xc" Then
                hfSeleccionVista.Value = eVistaMaquinas.porCliente
                despliegaVista(eVistaMaquinas.porCliente)
                tbCodigoCliente.Text = Request("cc")
                bBuscarPorCodigoCliente_Click(Me, Nothing)
            Else
                hfSeleccionVista.Value = eVistaMaquinas.porCliente
                despliegaVista(eVistaMaquinas.porCliente)
            End If
        Else
            pResultadoPorCodigo.Visible = False
            pResultadoPorEjecutivoComercial.Visible = False
            pResultadoPorMaquina.Visible = False
        End If
    End Sub

    Private Sub despliegaVista(ByVal tipoVista As eVistaMaquinas)

        pPorCliente.Visible = False
        pResultadoPorCodigo.Visible = False

        pPorEjecutivo.Visible = False
        pPorMaquina.Visible = False

        Select Case tipoVista
            Case eVistaMaquinas.porCliente
                pPorCliente.Visible = True
                tbCodigoCliente.Text = ""
                hlNuevoRegistroMaquina.Attributes.Add("onClick", String.Format(Cnt_JavaScriptWinOpen, "ingresar", ""))

            Case eVistaMaquinas.porEjecutivoComercial
                pPorEjecutivo.Visible = True
                Dim ws As cl.gms.aton.orgSrv = New cl.gms.aton.orgSrv
                With ddlEjecutivo
                    .DataSource = ws.listaPromotoras("GMSC")
                    .DataTextField = "nom_promotora_2"
                    .DataValueField = "cod_promotora"
                    .DataBind()
                End With
                ws = Nothing
            Case eVistaMaquinas.porMaquina
                pPorMaquina.Visible = True
                Dim ws As cl.gms.andes.ws.catastroMaquinas.catastroMaquinasSrv = New cl.gms.andes.ws.catastroMaquinas.catastroMaquinasSrv

                Dim msgError As String
                With ddlMaquina
                    .DataSource = ws.obtieneMaquinas("GMSC", msgError)
                    .DataTextField = "dmc_maquina"
                    .DataValueField = "id_maquina"
                    .DataBind()
                End With
                ws = Nothing

        End Select

    End Sub

    Private Sub lbPorCliente_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbPorCliente.Click
        hfSeleccionVista.Value = eVistaMaquinas.porCliente
        despliegaVista(eVistaMaquinas.porCliente)
    End Sub

    Private Sub lbPorEjecutivo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbPorEjecutivo.Click
        hfSeleccionVista.Value = eVistaMaquinas.porEjecutivoComercial
        despliegaVista(eVistaMaquinas.porEjecutivoComercial)
    End Sub

    Private Sub lbPorMaquina_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbPorMaquina.Click
        hfSeleccionVista.Value = eVistaMaquinas.porMaquina
        despliegaVista(eVistaMaquinas.porMaquina)
    End Sub
    Private Sub bBuscarPorCodigoCliente_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bBuscarPorCodigoCliente.Click

        Dim codigoCliente As String = tbCodigoCliente.Text.Trim.ToLower

        Dim wsAndes As cl.gms.andes.ws.clientes.clientesSrv = New cl.gms.andes.ws.clientes.clientesSrv

        Dim cliente As cl.gms.andes.ws.clientes.stCliente = New cl.gms.andes.ws.clientes.stCliente

        cliente = wsAndes.obtieneMaestroCliente("CHI", codigoCliente, False)

        If cliente.codigoCliente <> "" Then

            lRazonSocial.Text = cliente.codigoCliente & "-" & cliente.nombre
            lEjecutivaComercial.Text = cliente.nombrePromotora

            Session("catastroMaquinasSeleccionCliente_codCliente") = cliente.codigoCliente
            Session("catastroMaquinasSeleccionCliente_razonSocial") = cliente.nombre

            pResultadoPorCodigo.Visible = True

            'Cargamos maquinas cliente
            Dim msgError As String

            Dim wsMaquinas As cl.gms.andes.ws.catastroMaquinas.catastroMaquinasSrv = New cl.gms.andes.ws.catastroMaquinas.catastroMaquinasSrv
            Dim dsMaquinas As DataSet = wsMaquinas.obtieneRegistroMaquinasClientes("GMSC", cliente.codigoCliente, msgError)
            Dim cuentaMaquinas As Integer = 0

            If dsMaquinas.Tables.Count > 0 Then
                If dsMaquinas.Tables(0).Rows.Count >= 0 Then
                    With dgMaquinas
                        .DataSource = dsMaquinas.Tables(0)
                        .DataBind()
                    End With
                    cuentaMaquinas = dsMaquinas.Tables(0).Rows.Count
                End If
            End If

            lTotalOcurrenciasPorCodigo.Text = String.Format("Total {0} máquinas encontradas.", cuentaMaquinas)

        Else
            lRazonSocial.Text = String.Format("Cliente {0} no existe.", codigoCliente)
            Session("catastroMaquinasSeleccionCliente_codCliente") = Nothing
            Session("catastroMaquinasSeleccionCliente_razonSocial") = Nothing
            pResultadoPorCodigo.Visible = False
        End If

    End Sub

    Private Sub dgMaquinas_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgMaquinas.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

            'Agregamos javascript para abrir ventana emergente 
            e.Item.Cells(17).Controls.Clear()
            Dim myHyperLink As HyperLink = New HyperLink
            myHyperLink.Text = "editar"
            myHyperLink.NavigateUrl = "#"
            Dim idRegistro As Integer = CType(e.Item.DataItem("id_registro"), Integer)
            myHyperLink.Attributes.Add("onClick", String.Format(Cnt_JavaScriptWinOpen, "editar", idRegistro))
            e.Item.Cells(17).Controls.Add(myHyperLink)

            'Torre de barniz
            If e.Item.Cells(7).Text.Trim = "1" Then
                e.Item.Cells(7).Text = "Sí"
            Else
                e.Item.Cells(7).Text = ""
            End If

            'Agregamos javascript para confirmar operación de borrado
            Dim myLinkButton As LinkButton = e.Item.Cells(18).Controls(0)
            myLinkButton.Attributes.Add("onClick", "if (!confirm('¿Está seguro que desea eliminar este elemento?')){return false;}")

        End If
    End Sub

    Private Sub dgMaquinas_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgMaquinas.DeleteCommand
        Dim idRegistro As Integer = e.Item.Cells(0).Text
        Dim wsMaquinas As cl.gms.andes.ws.catastroMaquinas.catastroMaquinasSrv = New cl.gms.andes.ws.catastroMaquinas.catastroMaquinasSrv

        Dim msgError As String = ""

        wsMaquinas.eliminaRegistroMaquinaCliente(idRegistro, msgError)
        bBuscarPorCodigoCliente_Click(Me, Nothing)
        wsMaquinas = Nothing
    End Sub


    Private Sub bBuscarPorEjecutivo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bBuscarPorEjecutivo.Click
        Dim msgError As String = ""

        Dim ws As cl.gms.andes.ws.catastroMaquinas.catastroMaquinasSrv = New cl.gms.andes.ws.catastroMaquinas.catastroMaquinasSrv

        Dim ds As DataSet = ws.obtieneMaquinasPorCartera("GMSC", ddlEjecutivo.SelectedValue, msgError)
        With dgMaquinasCartera
            .DataSource = ds.Tables(0)
            .DataBind()
        End With

        lTotalOcurrenciasPorEjecutivo.Text = String.Format("Total {0} ocurrencias.", ds.Tables(0).Rows.Count)
        pResultadoPorEjecutivoComercial.Visible = True

        ds = Nothing
        ws = Nothing
    End Sub

    'Private Function obtieneMaquinasPorCartera(ByVal codEjecutivo As String) As DataTable

    '    Dim dbConn As SqlConnection = New SqlConnection(Utiles.obtieneStringDeConexion())

    '    Dim spCall As SqlCommand = New SqlCommand("cma_sel_maquinas_x_cartera", dbConn)

    '    Try
    '        dbConn.Open()

    '        spCall.CommandType = CommandType.StoredProcedure
    '        spCall.Parameters.Add("@resultValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
    '        spCall.Parameters.Add("@cod_sociedad", "GMSC").Direction = ParameterDirection.Input
    '        spCall.Parameters.Add("@cod_promotora", codEjecutivo).Direction = ParameterDirection.Input

    '        Dim da As SqlDataAdapter = New SqlDataAdapter
    '        da.SelectCommand = spCall

    '        da.SelectCommand.ExecuteNonQuery()

    '        Dim dt As DataTable = New DataTable

    '        da.Fill(dt)

    '        Return dt

    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        dbConn.Close()
    '        dbConn = Nothing
    '        spCall = Nothing
    '    End Try
    'End Function

    'Private Function obtieneClientesPorMaquina(ByVal idMaquina As String) As DataTable

    '    Dim dbConn As SqlConnection = New SqlConnection(Utiles.obtieneStringDeConexion())

    '    Dim spCall As SqlCommand = New SqlCommand("cma_sel_clientes_x_maquina", dbConn)

    '    Try
    '        dbConn.Open()

    '        spCall.CommandType = CommandType.StoredProcedure
    '        spCall.Parameters.Add("@resultValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
    '        spCall.Parameters.Add("@cod_sociedad", "GMSC").Direction = ParameterDirection.Input
    '        spCall.Parameters.Add("@id_maquina", idMaquina).Direction = ParameterDirection.Input

    '        Dim da As SqlDataAdapter = New SqlDataAdapter
    '        da.SelectCommand = spCall

    '        da.SelectCommand.ExecuteNonQuery()

    '        Dim dt As DataTable = New DataTable

    '        da.Fill(dt)

    '        Return dt

    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        dbConn.Close()
    '        dbConn = Nothing
    '        spCall = Nothing
    '    End Try
    'End Function

    Private Sub dgMaquinasCartera_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgMaquinasCartera.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then

            If e.Item.DataItem.item("cod_cliente") = codigoClienteAnterior Then
                e.Item.Cells(0).Controls.Clear()
                e.Item.Cells(0).Text = ""
                e.Item.Cells(1).Controls.Clear()
                e.Item.Cells(1).Text = ""
            Else
                numeroCliente += 1
                e.Item.Cells(0).CssClass = "maquinas-celda-limite-datagrid"
                e.Item.Cells(1).CssClass = "maquinas-celda-limite-datagrid"
                e.Item.Cells(2).CssClass = "maquinas-celda-limite-datagrid"
                e.Item.Cells(3).CssClass = "maquinas-celda-limite-datagrid"

                Dim lb As HyperLink = CType(e.Item.Cells(1).Controls(0), HyperLink)
                lb.CssClass = "maquinas-vinculo-t1"

            End If



            codigoClienteAnterior = e.Item.DataItem.item("cod_cliente")

            If numeroCliente Mod 2 = 0 Then
                e.Item.CssClass = "maquinas-dato-datagrid"
            Else
                e.Item.CssClass = "maquinas-dato-datagrid-alternado"
            End If

        End If
    End Sub

    Private Sub bBuscarPorMaquina_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bBuscarPorMaquina.Click
        Dim msgError As String
        Dim ws As cl.gms.andes.ws.catastroMaquinas.catastroMaquinasSrv = New cl.gms.andes.ws.catastroMaquinas.catastroMaquinasSrv

        Dim ds As DataSet = ws.obtieneClientesPorMaquina("GMSC", ddlMaquina.SelectedValue, msgError)
        With dgClientesMaquina
            .DataSource = ds.Tables(0)
            .DataBind()
        End With

        lTotalOcurrenciasPorMaquina.Text = String.Format("Total {0} ocurrencias.", ds.Tables(0).Rows.Count)
        pResultadoPorMaquina.Visible = True
    End Sub

    Private Sub dgClientesMaquina_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgClientesMaquina.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then

            Dim lb As HyperLink = CType(e.Item.Cells(1).Controls(0), HyperLink)
            lb.CssClass = "maquinas-vinculo-t1"
        End If
    End Sub
End Class
