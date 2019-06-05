Public Class detail
    Inherits System.Web.UI.Page
    Protected WithEvents lbTotalRegistros As System.Web.UI.WebControls.Label
    Protected WithEvents dgClientes As System.Web.UI.WebControls.DataGrid

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim codigoCliente, razonSocial As String
        Dim anoPeriodo, mesPeriodo As Integer
        Dim codigoSucursal, codigoAgente, alcance As String

        If (Request.QueryString("myAction") = "search") Then

            codigoCliente = Request.QueryString("cod_cli")
            razonSocial = Request.QueryString("razon_soc")
            codigoSucursal = Request.QueryString("cod_sucursal")
            codigoAgente = Request.QueryString("cod_agente")
            alcance = Request("alcance")


            Dim wsUtils As New Utils.UtilsSrv1()
            Dim dsClientes As New DataSet()
            dsClientes.Merge(wsUtils.buscaClientes(codigoSucursal, _
                                                    codigoAgente, _
                                                    codigoCliente, _
                                                    razonSocial))

            dgClientes.DataSource = dsClientes
            dgClientes.DataMember = "BusquedaClientes"
            dgClientes.DataBind()

            lbTotalRegistros.Text = "Total " & dsClientes.Tables(0).Rows.Count & " coincidencias."

            dsClientes = Nothing
            wsUtils = Nothing


        End If
    End Sub

End Class
