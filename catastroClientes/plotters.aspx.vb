Public Class plotters
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents MPTitle As Wilson.MasterPages.ContentRegion
    Protected WithEvents MPCaption As Wilson.MasterPages.ContentRegion
    Protected WithEvents MPContainer As Wilson.MasterPages.MasterPage
    Protected WithEvents bBuscarCliente As System.Web.UI.WebControls.Button
    Protected WithEvents tbCodigoCliente As System.Web.UI.WebControls.TextBox
    Protected WithEvents lRazonSocial As System.Web.UI.WebControls.Label
    Protected WithEvents pDatosCatastro As System.Web.UI.WebControls.Panel
    Protected WithEvents dgMaquinas As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lMensaje As System.Web.UI.WebControls.Label
    Protected WithEvents bIngresaNuevoPlotter As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim msgError As String

    Const Cnt_JavaScriptWinOpen As String = " window.open('frmPlotter.aspx?accion={0}&idRegistro={1}', 'maq', 'menubar=0,resizable=0,width=550,height=259');"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Not Page.IsPostBack Then
            bIngresaNuevoPlotter.Attributes.Add("onClick", String.Format(Cnt_JavaScriptWinOpen, "ingresar", ""))
            pDatosCatastro.Visible = False

            If Request("accion") = "buscar" Then
                If Request("codCliente") <> "" Then
                    tbCodigoCliente.Text = Request("codCliente")
                    despliegaPlottersCliente(Request("codCliente"))
                End If
            End If
        End If
    End Sub

    Private Sub bBuscarCliente_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bBuscarCliente.Click
        Dim codigoCliente As String = tbCodigoCliente.Text.Trim
        If codigoCliente <> "" Then
            despliegaPlottersCliente(codigoCliente)
        End If

    End Sub

    Private Sub despliegaPlottersCliente(ByVal codigoCliente As String)
        Dim wsAndes As cl.gms.andes.ws.clientes.clientesSrv = New cl.gms.andes.ws.clientes.clientesSrv

        Dim cliente As cl.gms.andes.ws.clientes.stCliente = New cl.gms.andes.ws.clientes.stCliente

        cliente = wsAndes.obtieneMaestroCliente("CHI", codigoCliente, False)

        Dim myTableRow As TableRow
        Dim myTableCell As TableCell

        If cliente.codigoCliente <> "" Then

            lRazonSocial.Text = cliente.codigoCliente & "-" & cliente.nombre
            Session("catastroPlottersSeleccionCliente_codCliente") = cliente.codigoCliente
            Session("catastroPlottersSeleccionCliente_razonSocial") = cliente.nombre

            pDatosCatastro.Visible = True

            'Cargamos maquinas cliente
            Dim wsPlotters As cl.gms.andes.ws.catastroPlotters.catastroPlottersSrv = New cl.gms.andes.ws.catastroPlotters.catastroPlottersSrv
            Dim dsMaquinas As DataSet = wsPlotters.obtieneRegistroPlottersClientes("GMSC", cliente.codigoCliente, msgError)
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

            lMensaje.Text = String.Format("Total {0} máquinas encontradas.", cuentaMaquinas)

        Else
            lRazonSocial.Text = String.Format("Cliente {0} no existe.", codigoCliente)
            Session("catastroPlottersSeleccionCliente_codCliente") = Nothing
            Session("catastroPlottersSeleccionCliente_razonSocial") = Nothing
            pDatosCatastro.Visible = False
        End If
    End Sub


    Private Sub dgMaquinas_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgMaquinas.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

            'Agregamos javascript para abrir ventana emergente 
            Dim myLinkButton As LinkButton = e.Item.Cells(4).Controls(0)
            Dim idRegistro As Integer = e.Item.Cells(0).Text
            myLinkButton.Attributes.Add("onClick", String.Format(Cnt_JavaScriptWinOpen, "editar", idRegistro))

            'Agregamos javascript para confirmar operación de borrado
            myLinkButton = e.Item.Cells(5).Controls(0)

            myLinkButton.Attributes.Add("onClick", "return confirm('¿Está seguro que desea eliminar este elemento?');")

        End If
    End Sub


    Private Sub dgMaquinas_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgMaquinas.DeleteCommand
        Dim idRegistro As Integer = e.Item.Cells(0).Text
        Dim wsPlotters As cl.gms.andes.ws.catastroPlotters.catastroPlottersSrv = New cl.gms.andes.ws.catastroPlotters.catastroPlottersSrv

        wsPlotters.eliminaRegistroMaquinaCliente(idRegistro, msgError)

        despliegaPlottersCliente(tbCodigoCliente.Text)

        wsPlotters = Nothing
    End Sub

    Private Sub bIngresaNuevoPlotter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bIngresaNuevoPlotter.Click

    End Sub
End Class
