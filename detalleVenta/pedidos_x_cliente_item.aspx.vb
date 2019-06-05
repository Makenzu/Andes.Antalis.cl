Public Class pedidos_x_cliente_item
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents MPTitle As Wilson.MasterPages.ContentRegion
    Protected WithEvents MPCaption As Wilson.MasterPages.ContentRegion
    Protected WithEvents MPContainer As Wilson.MasterPages.MasterPage

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
    Protected WithEvents dgPedCli As System.Web.UI.WebControls.DataGrid
    Protected WithEvents bBuscar As System.Web.UI.WebControls.Button
    Protected WithEvents tbCliente As System.Web.UI.WebControls.TextBox
    Protected WithEvents tbMaterial As System.Web.UI.WebControls.TextBox
    Protected WithEvents Literal1 As System.Web.UI.WebControls.Literal
    Protected WithEvents lTituloConsulta As System.Web.UI.WebControls.Label

    Dim tUserInfo As usuario.t_Usuario

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Session(Constantes.CTE_ANDES_USUARIO_AUTENTICADO) <> "SI" Then
            Response.Redirect("/logout.aspx")
            Return
        End If

        tUserInfo = Session(Constantes.CTE_ANDES_INFO_USUARIO)

    End Sub

    Private Sub bBuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bBuscar.Click

        Dim codigoCliente As String
        Dim codigoFilial As String = tUserInfo.codigoFilial
        Dim codigoSucursal As String = tUserInfo.codigoSucursal
        Dim codigoMaterial As String
        Dim dmcMaterial As String
        Dim razonSocial As String

        Dim arrStr() As String = Split(tbCliente.Text, "::")
        codigoCliente = arrStr(0).Trim()

        arrStr = Split(tbMaterial.Text, "::")
        codigoMaterial = arrStr(0).Trim()

        If codigoMaterial = "" Then
            Literal1.Text = "muestraMensaje('error', 'Debe indicar un material válido', 2000);"
            Return
        Else
            'Traemos datos del material
            Dim ws As cl.gms.andes.ws.materiales.materialesSrv = New cl.gms.andes.ws.materiales.materialesSrv
            Dim ds As DataSet = ws.obtieneMaterial(codigoFilial, codigoMaterial)

            If ds.Tables(0).Rows.Count = 1 Then
                dmcMaterial = Trim(ds.Tables(0).Rows(0).Item("des_producto"))
            Else
                Literal1.Text = "muestraMensaje('error', 'No se encontró material " & codigoMaterial & "', 2000);"
                ws = Nothing
                ds = Nothing
                Return
            End If
        End If

        If codigoCliente = "" Then
            Literal1.Text = "muestraMensaje('error', 'Debe indicar un cliente válido', 2000);"
            Return
        Else
            'Traemos datos del cliente
            Dim ws As cl.gms.andes.ws.clientes.clientesSrv = New cl.gms.andes.ws.clientes.clientesSrv
            Dim cliente As cl.gms.andes.ws.clientes.stCliente = New cl.gms.andes.ws.clientes.stCliente
            cliente = ws.obtieneMaestroCliente(codigoFilial, codigoCliente, False)

            If cliente.codigoCliente <> "" Then
                razonSocial = cliente.nombre
            Else
                Literal1.Text = "muestraMensaje('error', 'No se encontró cliente " & codigoCliente & "', 2000);"
                ws = Nothing
                Return
            End If

            cliente = Nothing
            ws = Nothing

        End If

        Dim resultTbl As DataTable = pedidoClienteItem(codigoFilial, codigoSucursal, codigoCliente, codigoMaterial)

        lTituloConsulta.Text = String.Format("{0} - {1}<br/>{2} - {3}", codigoCliente, dmcMaterial, codigoCliente, razonSocial)

        With dgPedCli
            .DataSource = resultTbl
            .DataBind()
        End With

    End Sub

    Private Sub dgPedCli_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgPedCli.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim hl As HyperLink = New HyperLink

            Dim codigoFilial As String = tUserInfo.codigoFilial
            Dim codigoSucursal As String = tUserInfo.codigoSucursal
            Dim anoPeriodo As Integer = CType(e.Item.DataItem.item("fec_documento"), DateTime).Year
            Dim mesPeriodo As Integer = CType(e.Item.DataItem.item("fec_documento"), DateTime).Month
            Dim numDocumento As String = e.Item.DataItem.item("num_documento")
            Dim codDocumento As String = e.Item.DataItem.item("cod_documento")

            hl.Text = e.Item.DataItem.item("num_docto_sap")
            hl.NavigateUrl = String.Format("/historico/vta_detalle_documento.aspx?ap={0}&mo=DOL&mp={1}&cf={2}&cs={3}&nd={4}&cd={5}", anoPeriodo, mesPeriodo, codigoFilial, codigoSucursal, numDocumento, codDocumento)
            hl.CssClass = "listado-vinculo"
            e.Item.Cells(3).Controls.Add(hl)

        End If
    End Sub
End Class
